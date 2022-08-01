using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.DataTransferObjects.Expert
{
    public class ExpertPublicProfileDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public List<CommentDto>? Comments { get; set; }
        public List<int>? WorkSamples { get; set; }
        public float? Rating { get; set; }
    }
}
