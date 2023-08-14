namespace MTAIntranet.MVC.Models.ViewModels;

using MTAIntranet.SQL.API;
using MTAIntranet.SQL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TicketSubTypeViewModel
{
    public string Category { get; set; } = null!;

    [MaxLength(20)]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Cclist { get; set; } = null!;

    public string NeedsApproval { get; set; } = null!;

    [NotMapped]
    public IEnumerable<Category>? CategoriesList { get; set; } = null!;

    public void FillLists(MtaticketsContext context)
    {
        CategoriesList = context.Categories;
    }
}
