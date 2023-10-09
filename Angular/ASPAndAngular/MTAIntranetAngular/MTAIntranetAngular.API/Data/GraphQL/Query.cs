using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
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
        public IQueryable<ApprovalState> GetApprovalStates(
        [Service] MtaticketsContext context)
        => context.ApprovalStates;


        /// <summary>
        /// Gets all Cities.
        /// </summary>
        [Serial]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Category> GetCategories(
        [Service] MtaticketsContext context)
        => context.Categories;

        /// <summary>
        /// Gets all Cities.
        /// </summary>
        [Serial]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Impact> GetImpacts(
        [Service] MtaticketsContext context)
        => context.Impacts;

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
        /// Gets all Cities.
        /// </summary>
        [Serial]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<TicketSubType> GetTicketSubTypes(
        [Service] MtaticketsContext context)
        => context.TicketSubTypes;


        /// <summary>
        /// Gets all Cities (with ApiResult and DTO support).
        /// </summary>
        [Serial]
        public async Task<ApiResult<CategoryDTO>> GetCategoriesApiResult(
        [Service] MtaticketsContext context,
        int pageIndex = 0,
        int pageSize = 10,
        string? sortColumn = null,
        string? sortOrder = null,
        string? filterColumn = null,
        string? filterQuery = null)
        {
            return await ApiResult<CategoryDTO>.CreateAsync(
            context.Categories.AsNoTracking()
            .Select(c => new CategoryDTO()
            {
                CategoryId = c.CategoryId,
                Name = c.Name
            }),
            pageIndex,
            pageSize,
            sortColumn,
            sortOrder,
            filterColumn,
            filterQuery);
        }

        /// <summary>
        /// Gets all Cities (with ApiResult and DTO support).
        /// </summary>
        [Serial]
        public async Task<ApiResult<ApprovalStateDTO>> GetApprovalStatesApiResult(
        [Service] MtaticketsContext context,
        int pageIndex = 0,
        int pageSize = 10,
        string? sortColumn = null,
        string? sortOrder = null,
        string? filterColumn = null,
        string? filterQuery = null)
        {
            return await ApiResult<ApprovalStateDTO>.CreateAsync(
            context.ApprovalStates.AsNoTracking()
            .Select(c => new ApprovalStateDTO()
            {
                ApprovalStateId = c.ApprovalStateId,
                Name = c.Name
            }),
            pageIndex,
            pageSize,
            sortColumn,
            sortOrder,
            filterColumn,
            filterQuery);
        }

        /// <summary>
        /// Gets all Cities (with ApiResult and DTO support).
        /// </summary>
        [Serial]
        public async Task<ApiResult<ImpactDTO>> GetImpactsApiResult(
        [Service] MtaticketsContext context,
        int pageIndex = 0,
        int pageSize = 10,
        string? sortColumn = null,
        string? sortOrder = null,
        string? filterColumn = null,
        string? filterQuery = null)
        {
            return await ApiResult<ImpactDTO>.CreateAsync(
            context.Impacts.AsNoTracking()
            .Select(c => new ImpactDTO()
            {
                ImpactId = c.ImpactId,
                Description = c.Description
            }),
            pageIndex,
            pageSize,
            sortColumn,
            sortOrder,
            filterColumn,
            filterQuery);
        }

        /// <summary>
        /// Gets all Cities (with ApiResult and DTO support).
        /// </summary>
        [Serial]
        public async Task<ApiResult<TicketDTO>> GetTicketsApiResult(
        [Service] MtaticketsContext context,
        int pageIndex = 0,
        int pageSize = 10,
        string? sortColumn = null,
        string? sortOrder = null,
        string? filterColumn = null,
        string? filterQuery = null)
        {
            return await ApiResult<TicketDTO>.CreateAsync(
            context.Tickets.AsNoTracking()
            .Select(c => new TicketDTO()
            {
                TicketId = c.TicketId,
                ApprovalStateId = c.ApprovalStateId,
                ImpactId = c.ImpactId,
                CategoryId = c.CategoryId,
                SubTypeId = c.SubTypeId,
                ApprovedBy = c.ApprovedBy,
                EnteredByUser = c.EnteredByUser,
                DateEntered = c.DateEntered,
                DateLastUpdated = c.DateLastUpdated,
                ReasonForRejection = c.ReasonForRejection,
                Summary = c.Summary,
                CategoryName = c.Category.Name,
                SubTypeName = c.SubType.Name,
                ImpactName = c.Impact.Description,
                ApprovalStateName = c.ApprovalState.Name
            }),
            pageIndex,
            pageSize,
            sortColumn,
            sortOrder,
            filterColumn,
            filterQuery);
        }

        /// <summary>
        /// Gets all Cities (with ApiResult and DTO support).
        /// </summary>
        [Serial]
        public async Task<ApiResult<TicketSubTypeDTO>> GetTicketSubTypesApiResult(
        [Service] MtaticketsContext context,
        int pageIndex = 0,
        int pageSize = 10,
        string? sortColumn = null,
        string? sortOrder = null,
        string? filterColumn = null,
        string? filterQuery = null)
        {
            return await ApiResult<TicketSubTypeDTO>.CreateAsync(
            context.TicketSubTypes.AsNoTracking()
            .Select(c => new TicketSubTypeDTO()
            {
                TicketSubTypeId = c.TicketSubTypeId,
                CategoryId = c.CategoryId,
                NeedsApproval = c.NeedsApproval,
                Cclist = c.Cclist,
                Name = c.Name,
                Description = c.Description,
                CategoryName = c.Category.Name
            }),
            pageIndex,
            pageSize,
            sortColumn,
            sortOrder,
            filterColumn,
            filterQuery);
        }
    }
}
