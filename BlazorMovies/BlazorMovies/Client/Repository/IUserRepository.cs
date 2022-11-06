using BlazorMovies.Shared.DTO;

namespace BlazorMovies.Client.Repository
{
    public interface IUserRepository
    {        
        Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO paginationDTO);
        Task<List<RoleDTO>> GetRoles();
        Task AssignRole(EditRoleDTO editRole);
        Task RemoveRole(EditRoleDTO editRole);
    }
}
