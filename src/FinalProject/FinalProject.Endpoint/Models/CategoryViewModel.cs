﻿namespace FinalProject.Endpoint.Models
{
    public class CategoryViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string Description { get; set; } = null!;
        public int? ParentCategoryId { get; init; }
        public string? ImageAddress { get; set; }
    }
}
