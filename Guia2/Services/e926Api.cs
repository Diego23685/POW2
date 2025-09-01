using System.Net.Http.Json;
using System.Text.Json;
using Guia2.Models;

namespace Guia2.Services
{
    public class E926Api : Ie926Api
    {
        private readonly IHttpClientFactory _factory;

        private static readonly JsonSerializerOptions J = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // Lista de tags que NO queremos mostrar (violencia, sexual, etc.)
        private static readonly HashSet<string> _bannedTags = new(StringComparer.OrdinalIgnoreCase)
        {
            "gore","blood","violence","weapon","injury","dead","death",
            "nudity","implied_nudity","nsfw","fetish","explicit","sexual",
            "rape","torture","smoking","drug","alcohol","cigarette",
            // añade cualquiera que detectes no apto en pruebas
        };

        public E926Api(IHttpClientFactory factory) => _factory = factory;

        public async Task<PostIndexResponse> SearchAsync(string tags, int limit = 24, int page = 1, CancellationToken ct = default)
        {
            // Forzamos rating seguro y aplicamos la búsqueda del usuario
            var q = string.IsNullOrWhiteSpace(tags) ? "rating:s" : $"{tags} rating:s";

            var client = _factory.CreateClient("e926");
            var url = $"/posts.json?tags={Uri.EscapeDataString(q)}&limit={limit}&page={page}";
            using var resp = await client.GetAsync(url, ct);
            resp.EnsureSuccessStatusCode();

            var data = await resp.Content.ReadFromJsonAsync<PostIndexResponse>(J, ct)
                       ?? new PostIndexResponse { posts = Array.Empty<Post>() };

            // Filtrar rating no seguro (por si acaso) y limpiar tags
            data.posts = data.posts
                .Where(p => string.Equals(p.rating, "s", StringComparison.OrdinalIgnoreCase))
                .Select(CleanPost)
                .ToArray();

            return data;
        }

        public async Task<Post?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var client = _factory.CreateClient("e926");
            using var resp = await client.GetAsync($"/posts/{id}.json", ct);
            if (!resp.IsSuccessStatusCode) return null;

            var wrapper = await resp.Content.ReadFromJsonAsync<PostShowResponse>(J, ct);
            var post = wrapper?.post;
            if (post is null) return null;

            if (!string.Equals(post.rating, "s", StringComparison.OrdinalIgnoreCase))
                return null;

            return CleanPost(post);
        }

        private Post CleanPost(Post p)
        {
            if (p.tags != null)
            {
                p.tags.general = p.tags.general.Where(t => !_bannedTags.Contains(t)).ToArray();
                p.tags.species = p.tags.species.Where(t => !_bannedTags.Contains(t)).ToArray();
                p.tags.character = p.tags.character.Where(t => !_bannedTags.Contains(t)).ToArray();
                p.tags.artist = p.tags.artist.Where(t => !_bannedTags.Contains(t)).ToArray();
                // meta suele incluir info técnica; no la mostramos y no hace falta limpiarla para público infantil
            }
            return p;
        }
    }
}
