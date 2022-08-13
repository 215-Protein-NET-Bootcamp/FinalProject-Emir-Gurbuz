using Core.Entity.Concrete;
using Core.Utilities.Result;
using Core.Utilities.URI;

namespace NTech.Business.Helpers
{
    public static class PaginationHelper
    {
        public static PaginatedResult<IEnumerable<T>> CreatePaginatedResponse<T>(IQueryable<T> data, PaginationFilter paginationFilter, int totalRecords, IUriService uriService, string route)
        {
            data = data.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);
            int roundedTotalPages;
            var response = new PaginatedResult<IEnumerable<T>>(data, paginationFilter.PageNumber, paginationFilter.PageSize);

            double totalPage = totalRecords / (double)paginationFilter.PageSize;
            if (paginationFilter.PageNumber <= 0 || paginationFilter.PageSize <= 0)
            {
                roundedTotalPages = 1;
                paginationFilter.PageNumber = 1;
                paginationFilter.PageSize = 1;
            }
            else
                roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPage));

            response.NextPage = paginationFilter.PageNumber >= 1 && paginationFilter.PageNumber < roundedTotalPages
                ? uriService.GeneratePageRequestUri(
                    new PaginationFilter(paginationFilter.PageNumber + 1, paginationFilter.PageSize), route)
                : null;

            response.PreviousPage =
                paginationFilter.PageNumber - 1 >= 1 && paginationFilter.PageNumber <= roundedTotalPages
                ? uriService.GeneratePageRequestUri(
                    new PaginationFilter(paginationFilter.PageNumber - 1, paginationFilter.PageSize), route)
                : null;

            response.FirstPage =
                uriService.GeneratePageRequestUri(new PaginationFilter(1, paginationFilter.PageSize), route);
            response.LastPage =
                uriService.GeneratePageRequestUri(new PaginationFilter(roundedTotalPages, paginationFilter.PageSize), route);
            
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;
        }
    }
}
