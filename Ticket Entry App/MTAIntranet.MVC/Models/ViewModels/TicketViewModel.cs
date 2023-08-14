using MTAIntranet.SQL.API;
using MTAIntranet.SQL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTAIntranet.MVC.Models.ViewModels
{
    public partial class TicketViewModel
    {
        //public string TicketId { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string SubType { get; set; } = null!;

        public string Impact { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public string ApprovalState { get; set; } = null!;

        public string ApprovedBy { get; set; } = null!;

        public DateTime DateEntered { get; set; }

        public DateTime DateLastUpdated { get; set; }

        public string EnteredByUser { get; set; } = null!;

        public string? ReasonForRejection { get; set; } = null!;

        // List of ApprovalStates
        [NotMapped]
        public IEnumerable<ApprovalState>? ApprovalStatesList { get; set; }

        // List of Impacts
        [NotMapped]
        public IEnumerable<Impact>? ImpactList { get; set; }

        // List of TicketTypes
        [NotMapped]
        public IEnumerable<Category>? CategoriesList { get; set; }

        // List of TicketSubTypes
        [NotMapped]
        public IEnumerable<TicketSubType>? TicketSubTypeList { get; set; }

        public void FillLists(MtaticketsContext context)
        {
            CategoriesList = context.Categories;
            TicketSubTypeList = context.TicketSubTypes;
            ApprovalStatesList = context.ApprovalStates;
            ImpactList = context.Impacts;
        }
    }

}