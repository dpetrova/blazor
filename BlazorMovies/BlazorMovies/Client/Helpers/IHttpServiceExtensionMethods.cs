using BlazorMovies.Shared.DTO;

namespace BlazorMovies.Client.Helpers
{
    public static class IHttpServiceExtensionMethods
    {
        public static async Task<T> GetHelper<T>(this IHttpService httpService, string url)
        {
            var response = await httpService.Get<T>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }

            return response.Response;
        }

        public static async Task<PaginatedResponse<T>> GetHelper<T>(this IHttpService httpService, string url, PaginationDTO paginationDTO)
        {
            string newUrl = "";
            if(url.Contains("?"))
            {
                newUrl = $"{url}&page={paginationDTO.Page}&recordsPerPage={paginationDTO.RecordsPerPage}";
            }
            else
            {
                newUrl = $"{url}?page={paginationDTO.Page}&recordsPerPage={paginationDTO.RecordsPerPage}";
            }

            var httpResponse = await httpService.Get<T>(newUrl);
            var totalPages = int.Parse(httpResponse.HttpResponseMessage.Headers.GetValues("totalPages").FirstOrDefault());
            var paginatedResponse = new PaginatedResponse<T>()
            {
                Response = httpResponse.Response,
                TotalPages = totalPages
            };

            return paginatedResponse;
        }
    }
}
