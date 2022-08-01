using FinalProject.Endpoint.Models;

namespace FinalProject.Endpoint.Areas.Expert.Models
{
    public class ExpertPublicProfileViewModel
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public List<CommentListViewModel>? Comments { get; set; }
        public List<int>? WorkSamples { get; set; }
        public float? Rating { get; set; }
    }
}
