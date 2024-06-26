﻿namespace PokedexAPI.Extensions
{
    internal static class FileExtension
    {
        public static void SafeCreate(string path)
        {
            if (!File.Exists(path))
            {
                var fileInfo = new FileInfo(path);
                if (!Directory.Exists(fileInfo.Directory.FullName))
                {
                    Directory.CreateDirectory(fileInfo.Directory.FullName);
                }
                var file = File.Create(path);
                file.Close();
                file.Dispose();
            }
        }
    }
}
