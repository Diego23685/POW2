using System.Threading;
using Guia2.Models;

namespace Guia2.Services
{
    public interface Ie926Api
    {
        Task<PostIndexResponse> SearchAsync(string tags, int limit = 24, int page = 1, CancellationToken ct = default);
        Task<Post?> GetByIdAsync(int id, CancellationToken ct = default);
    }
}
