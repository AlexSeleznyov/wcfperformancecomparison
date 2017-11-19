using System;
using System.Web;

namespace DataSource
{
    public static class Data
    {
        public const int FileLength = 1024 * 1024;
        public static readonly byte[] FileContent;
        public const string FileName = "AFile.zip";
        public static readonly string MimeType;

        static Data()
        {
            FileContent = new byte[FileLength];
            new Random().NextBytes(FileContent);

            MimeType = MimeMapping.GetMimeMapping(FileName);
        }
    }
}
