using FinalProject.Application.Common.DataTransferObjects;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Administration.Models;
public class CommentListViewModel
{
    [Display(Name = "شناسه")]
    public int Id { get; init; }
    [Display(Name = "محتویات")]
    public string Content { get; set; } = null!;
    [Display(Name = "رای ها")]
    public int Votes { get; set; }
    [Display(Name = "شناسه مشتری")]
    public string CustomerId { get; init; } = null!;
    [Display(Name = "شناسه متخصص")]
    public string ExpertId { get; init; } = null!;
    public string? ImageAddress { get; set; }
    [Display(Name = "تایید شده")]
    public bool IsApproved { get; set; }
}

