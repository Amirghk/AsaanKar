﻿using FinalProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities;
/// <summary>
/// each expert can make a bid on an order with a price in it and a note by the expert
/// each order has many bids 
/// </summary>
public class Bid : IAuditableEntity
{
    #region Properties
    public int Id { get; set; }
    public string? Notes { get; set; }
    public decimal Price { get; set; }
    #endregion
    #region Navigational Properties 
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public string? ExpertId { get; set; }
    public Expert? Expert { get; set; }
    #endregion
}

