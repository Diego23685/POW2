using Guia2.Models;
using Guia2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Guia2.Controllers
{
    public class PostsController : Controller
    {
        private readonly Ie926Api _api;

        public PostsController(Ie926Api api) => _api = api;

        // 🔥 Forzamos artista por defecto
        private const string DefaultArtist = "darkalcaline";

        // GET /Posts
        public async Task<IActionResult> Index(int page = 1, CancellationToken ct = default)
        {
            var data = await _api.SearchAsync(DefaultArtist, limit: 24, page: page, ct);
            ViewData["Query"] = DefaultArtist;
            ViewData["Page"] = page;
            return View(data.posts);
        }

        // GET /Posts/Details/ID
        public async Task<IActionResult> Details(int id, CancellationToken ct = default)
        {
            var post = await _api.GetByIdAsync(id, ct);
            if (post == null) return NotFound();
            return View(post);
        }
    }
}
