namespace Guia2.Models
{
    public class PostIndexResponse
    {
        public Post[] posts { get; set; } = Array.Empty<Post>();
    }

    public class PostShowResponse
    {
        public Post? post { get; set; }
    }

    public class Post
    {
        public int id { get; set; }
        public string? description { get; set; }
        public Score? score { get; set; }
        public string? rating { get; set; }   // "s/q/e" (en e926 debería ser "s")
        public FileInfo? file { get; set; }
        public Preview? preview { get; set; }
        public Sample? sample { get; set; }
        public Tags? tags { get; set; }
        public string? created_at { get; set; }
    }

    public class Score
    {
        public int up { get; set; }
        public int down { get; set; }
        public int total { get; set; }
    }

    public class FileInfo
    {
        public string? url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string? ext { get; set; }
        public int size { get; set; } // bytes
    }

    public class Preview
    {
        public string? url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Sample
    {
        public string? url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Tags
    {
        public string[] general { get; set; } = Array.Empty<string>();
        public string[] species { get; set; } = Array.Empty<string>();
        public string[] character { get; set; } = Array.Empty<string>();
        public string[] artist { get; set; } = Array.Empty<string>();
        public string[] meta { get; set; } = Array.Empty<string>();
    }
}
