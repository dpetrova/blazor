namespace BlazorMovies.Server.Helpers
{
    public interface IFileStorageService
    {
        // content -> bytes[] array
        // extention -> file extention
        // containerName -> folder where to save file in
        // fileRoute -> file path
        Task<string> SaveFile(byte[] content, string extention, string containerName);
        Task<string> EditFile(byte[] content, string extention, string containerName, string fileRoute);
        Task DeleteFile(string fileRoute, string containerName);
    }
}
