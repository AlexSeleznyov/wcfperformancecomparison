﻿using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Xml;
using Common.Logging;
using DataSource;
using WcfInterface;

namespace WcfClient
{
    internal static class Program
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(Program).Name);

        static void Main(string[] args)
        {

            ServicePointManager.ServerCertificateValidationCallback += ServerCertificateValidationCallback;

            var cases = new Func<CaseBase>[]
            {
                () => new CaseOneFactoryOneChannel<IDownload1>(6666, SecurityMode.None, Call, TransferMode.Buffered, TcpNetChannelFactory<IDownload1>, TcpNetChannel),
                () => new CaseOneFactory<IDownload1>(6666, SecurityMode.None, Call, TransferMode.Buffered, TcpNetChannelFactory<IDownload1>, TcpNetChannel),
                () => new CaseMultipleFactories<IDownload1>(6666, SecurityMode.None, Call, TransferMode.Buffered, TcpNetChannelFactory<IDownload1>, TcpNetChannel),

                () => new CaseOneFactoryOneChannel<IDownload1>(6667, SecurityMode.Transport, Call, TransferMode.Buffered, TcpNetChannelFactory<IDownload1>, TcpNetChannel),
                () => new CaseOneFactory<IDownload1>(6667, SecurityMode.Transport, Call, TransferMode.Buffered, TcpNetChannelFactory<IDownload1>, TcpNetChannel),
                () => new CaseMultipleFactories<IDownload1>(6667, SecurityMode.Transport, Call, TransferMode.Buffered, TcpNetChannelFactory<IDownload1>, TcpNetChannel),

                () => new CaseOneFactoryOneChannel<IDownload1>(6668, SecurityMode.Message, Call, TransferMode.Buffered, TcpNetChannelFactory<IDownload1>, TcpNetChannel),
                () => new CaseOneFactory<IDownload1>(6668, SecurityMode.Message, Call, TransferMode.Buffered, TcpNetChannelFactory<IDownload1>, TcpNetChannel),
                () => new CaseMultipleFactories<IDownload1>(6668, SecurityMode.Message, Call, TransferMode.Buffered, TcpNetChannelFactory<IDownload1>, TcpNetChannel),

                () => new CaseOneFactoryOneChannel<IDownload1>(6669, SecurityMode.None, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload1>, TcpNetChannel),
                () => new CaseOneFactory<IDownload1>(6669, SecurityMode.None, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload1>, TcpNetChannel),
                () => new CaseMultipleFactories<IDownload1>(6669, SecurityMode.None, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload1>, TcpNetChannel),

                () => new CaseOneFactoryOneChannel<IDownload1>(6670, SecurityMode.Transport, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload1>, TcpNetChannel),
                () => new CaseOneFactory<IDownload1>(6670, SecurityMode.Transport, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload1>, TcpNetChannel),
                () => new CaseMultipleFactories<IDownload1>(6670, SecurityMode.Transport, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload1>, TcpNetChannel),

                () => new CaseOneFactoryOneChannel<IDownload2>(7666, SecurityMode.None, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload2>, TcpNetChannel),
                () => new CaseOneFactory<IDownload2>(7666, SecurityMode.None, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload2>, TcpNetChannel),
                () => new CaseMultipleFactories<IDownload2>(7666, SecurityMode.None, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload2>, TcpNetChannel),

                () => new CaseOneFactoryOneChannel<IDownload2>(7667, SecurityMode.Transport, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload2>, TcpNetChannel),
                () => new CaseOneFactory<IDownload2>(7667, SecurityMode.Transport, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload2>, TcpNetChannel),
                () => new CaseMultipleFactories<IDownload2>(7667, SecurityMode.Transport, Call, TransferMode.StreamedResponse, TcpNetChannelFactory<IDownload2>, TcpNetChannel),

                // below case is not supported for streaming mode
                //() => new CaseOneFactoryOneChannel<IDownload2>(7668, SecurityMode.Message, Call, TransferMode.StreamedResponse),
                //() => new CaseOneFactory<IDownload2>(7668, SecurityMode.Message, Call, TransferMode.StreamedResponse),
                //() => new CaseMultipleFactories<IDownload2>(7668, SecurityMode.Message, Call, TransferMode.StreamedResponse),

                () => new CaseOneFactoryOneChannel<IDownload1, BasicHttpSecurityMode>(6766, BasicHttpSecurityMode.None, Call, TransferMode.Buffered, HttpNetChannelFactory<IDownload1>, HttpNetChannel),
                () => new CaseOneFactory<IDownload1, BasicHttpSecurityMode>(6766, BasicHttpSecurityMode.None, Call, TransferMode.Buffered, HttpNetChannelFactory<IDownload1>, HttpNetChannel),
                () => new CaseMultipleFactories<IDownload1, BasicHttpSecurityMode>(6766, BasicHttpSecurityMode.None, Call, TransferMode.Buffered, HttpNetChannelFactory<IDownload1>, HttpNetChannel),

                () => new CaseOneFactoryOneChannel<IDownload2, BasicHttpSecurityMode>(7766, BasicHttpSecurityMode.None, Call, TransferMode.Buffered, HttpNetChannelFactory<IDownload2>, HttpNetChannel),
                () => new CaseOneFactory<IDownload2, BasicHttpSecurityMode>(7766, BasicHttpSecurityMode.None, Call, TransferMode.Buffered, HttpNetChannelFactory<IDownload2>, HttpNetChannel),
                () => new CaseMultipleFactories<IDownload2, BasicHttpSecurityMode>(7766, BasicHttpSecurityMode.None, Call, TransferMode.Buffered, HttpNetChannelFactory<IDownload2>, HttpNetChannel),

                () => new CaseOneFactoryOneChannel<IDownload2, BasicHttpSecurityMode>(7769, BasicHttpSecurityMode.None, Call, TransferMode.StreamedResponse, HttpNetChannelFactory<IDownload2>, HttpNetChannel),
                () => new CaseOneFactory<IDownload2, BasicHttpSecurityMode>(7769, BasicHttpSecurityMode.None, Call, TransferMode.StreamedResponse, HttpNetChannelFactory<IDownload2>, HttpNetChannel),
                () => new CaseMultipleFactories<IDownload2, BasicHttpSecurityMode>(7769, BasicHttpSecurityMode.None, Call, TransferMode.StreamedResponse, HttpNetChannelFactory<IDownload2>, HttpNetChannel),

                () => new CaseOneFactoryOneChannel<IDownload1, BasicHttpSecurityMode>(6866, BasicHttpSecurityMode.TransportCredentialOnly, Call, TransferMode.Buffered, BasicHttpChannelFactory<IDownload1>, BasicHttpChannel),
                () => new CaseOneFactory<IDownload1, BasicHttpSecurityMode>(6866, BasicHttpSecurityMode.TransportCredentialOnly, Call, TransferMode.Buffered, BasicHttpChannelFactory<IDownload1>, BasicHttpChannel),
                () => new CaseMultipleFactories<IDownload1, BasicHttpSecurityMode>(6866, BasicHttpSecurityMode.TransportCredentialOnly, Call, TransferMode.Buffered, BasicHttpChannelFactory<IDownload1>, BasicHttpChannel),

                () => new CaseOneFactoryOneChannel<IDownload1, BasicHttpsSecurityMode>(7866, BasicHttpsSecurityMode.Transport, Call, TransferMode.Buffered, BasicHttpsChannelFactory<IDownload1>, BasicHttpsChannel),
                () => new CaseOneFactory<IDownload1, BasicHttpsSecurityMode>(7866, BasicHttpsSecurityMode.Transport, Call, TransferMode.Buffered, BasicHttpsChannelFactory<IDownload1>, BasicHttpsChannel),
                () => new CaseMultipleFactories<IDownload1, BasicHttpsSecurityMode>(7866, BasicHttpsSecurityMode.Transport, Call, TransferMode.Buffered, BasicHttpsChannelFactory<IDownload1>, BasicHttpsChannel),

            };

            // warm up WCF infrastructure
            for (var i = 0; i < cases.Length; i++)
            {
                var testCase = cases[i]();
                ((ITestCase)testCase).Run();
                ((IDisposable)testCase).Dispose();
            }

            // do the measurements
            var r = new string[cases.Length];
            for (var i = 0; i < cases.Length; i++)
            {
                r[i] = RunMultipleTimes(cases[i]);
            }
            s_log.Info("All tests were executed:");
            for (var i = 0; i < r.Length; i++)
            {
                s_log.InfoFormat("#{0,2}: {1,40} {2}", i, cases[i]().ToString(), r[i]);
            }
        }

        /// <summary>
        /// Accept any certificate regardless of its nature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private static string RunMultipleTimes(Func<CaseBase> testCaseFunc)
        {
            const int runCount = TestRunParams.RunCount;
            var result = default(string);
            using (new TimeMeasurer(_ =>
            {
                result = $"Elapsed total: {_}, per call {TimeSpan.FromTicks(_.Ticks / runCount)}";
                s_log.Info(result);
            }))
            {
                var testCase = testCaseFunc();
                for (var i = 0; i < runCount; i++)
                {
                    ((ITestCase)testCase).Run();
                }
                ((IDisposable)testCase).Dispose();
            }
            return result;
        }

        private static ChannelFactory<T> TcpNetChannelFactory<T>(SecurityMode securityMode, TransferMode transferMode)
        {
            return new ChannelFactory<T>(new NetTcpBinding
            {
                MaxReceivedMessageSize = Data.FileLength << 1,
                Security = new NetTcpSecurity { Mode = securityMode },
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = Data.FileLength << 1,
                },
                TransferMode = transferMode,
            });
        }

        private static T TcpNetChannel<T>(ChannelFactory<T> cf, int port)
        {
            return cf.CreateChannel(new EndpointAddress("net.tcp://localhost:" + port));
        }

        private static ChannelFactory<T> HttpNetChannelFactory<T>(BasicHttpSecurityMode securityMode, TransferMode transferMode)
        {
            return new ChannelFactory<T>(new NetHttpBinding
            {
                MaxReceivedMessageSize = Data.FileLength << 1,
                Security = new BasicHttpSecurity { Mode = securityMode },
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = Data.FileLength << 1,
                },
                TransferMode = transferMode,
            });
        }

        private static T BasicHttpChannel<T>(ChannelFactory<T> cf, int port)
        {
            return cf.CreateChannel(new EndpointAddress("http://localhost:" + port));
        }

        private static ChannelFactory<T> BasicHttpChannelFactory<T>(BasicHttpSecurityMode securityMode, TransferMode transferMode)
        {
            return new ChannelFactory<T>(new BasicHttpBinding
            {
                MaxReceivedMessageSize = Data.FileLength << 1,
                Security = new BasicHttpSecurity
                {
                    Mode = securityMode,
                },
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = Data.FileLength << 1,
                },
                TransferMode = transferMode,
            });
        }

        private static T BasicHttpsChannel<T>(ChannelFactory<T> cf, int port)
        {
            return cf.CreateChannel(new EndpointAddress("https://localhost:" + port));
        }

        private static ChannelFactory<T> BasicHttpsChannelFactory<T>(BasicHttpsSecurityMode securityMode, TransferMode transferMode)
        {
            return new ChannelFactory<T>(new BasicHttpsBinding
            {
                MaxReceivedMessageSize = Data.FileLength << 1,
                Security = new BasicHttpsSecurity
                {
                    Mode = securityMode,
                    Transport = new HttpTransportSecurity
                    {
                        ClientCredentialType = HttpClientCredentialType.Windows
                    }
                },
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = Data.FileLength << 1,
                },
                TransferMode = transferMode,
            });
        }

        private static T HttpNetChannel<T>(ChannelFactory<T> cf, int port)
        {
            return cf.CreateChannel(new EndpointAddress("http://localhost:" + port));
        }

        private static void Call(IDownload1 client)
        {
            var file = client.GetFile(Guid.Empty);
            s_log.InfoFormat("Downloaded: {0} ({1}) {2} bytes long, modified on {3}", file.FileName, file.MimeType, file.Content.Length, file.ModificationTimeStamp);
        }

        private static void Call(IDownload2 client)
        {
            var file = client.GetFile(new GetFileRequest {Guid = Guid.Empty});
            var buf = new byte[Data.FileLength];
            file.Content.Read(buf, 0, buf.Length);
            s_log.InfoFormat("Downloaded: {0} ({1}) {2} bytes long, modified on {3}", file.FileName, file.MimeType, buf.Length, file.ModificationTimeStamp);
        }
    }
}
