using System.ComponentModel;

namespace BlazorMovies.Server.Helpers
{
    public class LocalStorageService : IFileStorageService
    {
        // to get directory in the hard drive of the wwwroot folder where we will save files
        private readonly IWebHostEnvironment env;
        // allows us to read requested url
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }        

        public async Task<string> SaveFile(byte[] content, string extention, string containerName)
        {
            // give unique name of the file
            var filename = $"{Guid.NewGuid()}.{extention}";
            // folder where we are going to store the file in
            string folder = Path.Combine(env.WebRootPath, containerName);
            // create directory if not exists
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            // save path
            string savePath = Path.Combine(folder, filename);
            // save file
            await File.WriteAllBytesAsync(savePath, content);
            // generate url stored into DB
            var currentUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlForDb = Path.Combine(currentUrl, containerName, filename);
            return urlForDb;
        }

        public async Task<string> EditFile(byte[] content, string extention, string containerName, string fileRoute)
        {
            if (!string.IsNullOrEmpty(fileRoute))
            {
                await DeleteFile(fileRoute, containerName);
            }

            return await SaveFile(content, extention, containerName);
        }


        public Task DeleteFile(string fileRoute, string containerName)
        {
            // get file name
            var fileName = Path.GetFileName(fileRoute);
            // get file directory
            string fileDirectory = Path.Combine(env.WebRootPath, containerName, fileName);
            // delete file if exists
            if(File.Exists(fileDirectory))
            {
                File.Delete(fileDirectory);
            }
            // because method has return type of Task need to return something
            return Task.FromResult(0);
        }
    }
}
