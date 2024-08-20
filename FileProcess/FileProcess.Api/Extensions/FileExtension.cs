namespace FileProcess.Api.Extensions
{
    public static class FileExtension
    {
        public static bool IsValidJsonFile(this IFormFile file) => 
            file is IFormFile && Path.GetExtension(file.FileName) == ".json";
    }
}
