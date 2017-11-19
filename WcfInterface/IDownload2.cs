using System;
using System.IO;
using System.ServiceModel;

namespace WcfInterface
{
    [ServiceContract]
    public interface IDownload2
    {
        [OperationContract]
        GetFileResponse GetFile(GetFileRequest guid);
    }

    [MessageContract]
    public class GetFileRequest
    {
        [MessageHeader]
        public Guid Guid { get; set; }
    }

    [MessageContract]
    public class GetFileResponse
    {
        [MessageHeader]
        public string FileName { get; set; }
        [MessageHeader]
        public string MimeType { get; set; }
        [MessageHeader]
        public DateTime ModificationTimeStamp { get; set; }
        [MessageBodyMember]
        public Stream Content { get; set; }
    }
}