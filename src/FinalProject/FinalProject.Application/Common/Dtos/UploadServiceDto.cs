using FinalProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Dtos
{
    public record UploadServiceDto
    {
        public int Id { get; init; }
        public string FileName { get; init; } = null!;
        public long FileSize { get; init; }
        public FileCategory FileCategory { get; init; }
        public int? ExpertId { get; init; }
        public int? CategoryId { get; init; }
        public int? CustomerId { get; init; }
        public int? CommentId { get; init; }
        public IFormFile UploadedFile { get; init; } = null!;
    }
}
