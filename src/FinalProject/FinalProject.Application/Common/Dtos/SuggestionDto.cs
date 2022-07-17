using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Dtos;

public class SuggestionDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public bool IsRead { get; set; }
    public int? CustomerId { get; set; }
    public int? ExpertId { get; set; }
}

