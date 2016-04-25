﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RedDog.Search.Model;

namespace RedDog.Search.Portal.Controllers
{
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        private readonly IndexQueryClient _searchClient;

        public SearchController(IndexQueryClient searchClient)
        {
            _searchClient = searchClient;
        }

        /// <summary>
        /// GET api/search.
        /// </summary>
        /// <returns>Search an index.</returns>
        [Route("{indexName}")]
        public async Task<HttpResponseMessage> Get(string indexName, [FromUri]SearchQuery searchQuery)
        {
            var result = await _searchClient.SearchAsync(indexName, searchQuery);
            if (!result.IsSuccess)
                return Request.CreateResponse(result.StatusCode, result);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
