﻿using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.Entities;
/// <summary>
/// entity containing addresses and their respective city and provinces
/// also has a category showing what user it belongs to
/// </summary>
public class Address : ISoftDeletable, IBaseEntity
{
    #region Properties
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public AddressCategory AddressCategory { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigational Properties
    public string? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public string? ExpertId { get; set; }
    public virtual Expert? Expert { get; set; }
    public int CityId { get; set; }
    public virtual City City { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    #endregion
}