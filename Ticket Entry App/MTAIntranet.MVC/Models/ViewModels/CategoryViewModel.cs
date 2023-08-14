using System.ComponentModel.DataAnnotations;

namespace MTAIntranet.MVC.Models.ViewModels;

public partial class CategoryViewModel
{
    //public string TicketTypeId { get; set; } = null!;

    [MaxLength(20)]
    public string Name { get; set; } = null!;

    //public virtual ICollection<Process> Processes { get; set; } = new List<Process>();

    //public virtual ICollection<TicketSubType> TicketSubTypes { get; set; } = new List<TicketSubType>();

    //public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
