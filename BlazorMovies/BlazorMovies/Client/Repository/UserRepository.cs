using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTO;

namespace BlazorMovies.Client.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/users";

        public UserRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        //get list of users
        public async Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO paginationDTO)
        {
            return await httpService.GetHelper<List<UserDTO>>(url, paginationDTO);
        }

        //get list of roles
        public async Task<List<RoleDTO>> GetRoles()
        {
            return await httpService.GetHelper<List<RoleDTO>>($"{url}/roles");
        }

        //assign role to user
        public async Task AssignRole(EditRoleDTO editRole)
        {
            var response = await httpService.Post($"{url}/assignRole", editRole);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }
        }

        //remove role from user
        public async Task RemoveRole(EditRoleDTO editRole)
        {
            var response = await httpService.Post($"{url}/removeRole", editRole);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }
        }
    }
}
