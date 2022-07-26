using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinalProject.Application.Common.DataTransferObjects;

public record BidDto
{
    public int Id { get; init; }
    public string? Notes { get; init; }
    public decimal Price { get; init; }
    public int OrderId { get; init; }
    public string? ExpertId { get; init; }
}

