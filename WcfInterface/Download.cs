using System;

namespace WcfInterface
{
    public class Download
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public DateTime ModificationTimeStamp { get; set; }
        public byte[] Content { get; set; }
    }
}