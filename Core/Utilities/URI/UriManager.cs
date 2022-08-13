using Core.Entity.Concrete;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.URI
{
    public class UriManager : IUriService
    {
        private readonly string _baseUri;
        public UriManager(string baseUri)
        {
            _baseUri = baseUri;
        }
        /// <summary>
        /// Get page uri from request
        /// </summary>
        /// <param name="filter">pageSize,pageNumber</param>
        /// <param name="route">API endpoint without base uri</param>
        /// <returns>Request uri with pagination</returns>
        public Uri GeneratePageRequestUri(PaginationFilter filter, string route)
        {
            System.Uri endPointUri = new System.Uri(string.Concat(_baseUri, route));
            string modifiedUri = QueryHelpers.AddQueryString(endPointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new System.Uri(modifiedUri);
        }
    }
}
