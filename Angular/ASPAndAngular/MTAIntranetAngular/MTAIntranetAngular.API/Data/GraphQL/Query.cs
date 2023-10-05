using System.Diagnostics.Metrics;
using MTAIntranetAngular.API.Data.Models;

namespace MTAIntranetAngular.API.Data.GraphQL
{
    public class Query
    {
        /// <summary>
        /// Gets all Cities.
        /// </summary>
        [Serial]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Ticket> GetTickets(
        [Service] MtaticketsContext context)
        => context.Tickets;
        /// <summary>
        /// Gets all Countries.
        /// /// </summary>
        [Serial]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Category> GetCategories(
        [Service] MtaticketsContext context)
        => context.Categories;
    }
}
