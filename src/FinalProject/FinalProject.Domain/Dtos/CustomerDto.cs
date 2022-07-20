using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinalProject.Domain.Dtos;

public record CustomerDto
{
    public int Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public int? FileInfoId { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? CustomerId { get; init; }
    public bool IsDeleted { get; init; }
}

