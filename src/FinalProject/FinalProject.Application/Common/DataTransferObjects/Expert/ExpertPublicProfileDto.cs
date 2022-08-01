using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.DataTransferObjects
{
    public class ExpertPublicProfileDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public IEnumerable<CommentDto>? Comments { get; set; }
        public List<int>? WorkSamples { get; set; }
        public float? Rating { get; set; }
        public List<ServiceDto> Services { get; set; } = new List<ServiceDto>();
        public int? ProfilePictureId { get; set; }

    }
}
