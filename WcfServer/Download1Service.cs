using System;
using System.IO;
using System.ServiceModel;
using Common.Logging;
using DataSource;
using WcfInterface;

namespace WcfServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal class Download1Service : IDownload1
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(Download1Service).Name);

        public Download GetFile(Guid guid)
        {
            var securityContext = OperationContext.Current.ServiceSecurityContext;
            if (securityContext != null)
            {
                s_log.InfoFormat("Request for {0} is served. Requestor identity is {1} ({2})", guid, securityContext.IsAnonymous ? "Anonymous" : securityContext.PrimaryIdentity?.Name, securityContext.PrimaryIdentity?.AuthenticationType);
            }
            else
            {
                s_log.InfoFormat("Request for {0} is served. No security context is available", guid);
            }
            return new Download
            {
                Content = Data.FileContent,
                FileName = Data.FileName,
                MimeType = Data.MimeType,
                ModificationTimeStamp = DateTime.Now,
            };
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal class Download2Service : IDownload2
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(Download2Service).Name);

        public GetFileResponse GetFile(GetFileRequest request)
        {
            var securityContext = OperationContext.Current.ServiceSecurityContext;
            if (securityContext != null)
            {
                s_log.InfoFormat("Request for {0} is served. Requestor identity is {1} ({2})", request.Guid, securityContext.IsAnonymous ? "Anonymous" : securityContext.PrimaryIdentity?.Name, securityContext.PrimaryIdentity?.AuthenticationType);
            }
            else
            {
                s_log.InfoFormat("Request for {0} is served. No security context is available", request.Guid);
            }
            return new GetFileResponse
            {
                Content = new MemoryStream(Data.FileContent, false),
                FileName = Data.FileName,
                MimeType = Data.MimeType,
                ModificationTimeStamp = DateTime.Now,
            };
        }
    }
}
