using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParametersInResponse<T>(this HttpContext httpContext, IQueryable<T> queryable, int recordsPerpage)
        {
            if(httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            double count = await queryable.CountAsync();
            double totatPages = Math.Ceiling(count / recordsPerpage);
            httpContext.Response.Headers.Add("totalPages", totatPages.ToString());
        }
    }
}
