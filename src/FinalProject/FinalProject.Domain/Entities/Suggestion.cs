using FinalProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities;

public class Suggestion : IAuditableEntity
{
    #region Properties
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public bool IsRead { get; set; }
    #endregion
    #region Navigational Properties 
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int? ExpertId { get; set; }
    public Expert? Expert { get; set; }
    #endregion
}

