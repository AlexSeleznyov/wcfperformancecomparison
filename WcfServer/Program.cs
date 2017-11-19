using System;
using System.ServiceModel;
using WcfInterface;

namespace WcfServer
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            //var host = new ServiceHost(typeof(Download1Service));
            var host1 = new ServiceHost(new Download1Service());
            host1.AddServiceEndpoint(typeof(IDownload1), new NetTcpBinding(SecurityMode.None), new Uri("net.tcp://localhost:6666"));
            host1.AddServiceEndpoint(typeof(IDownload1), new NetTcpBinding(SecurityMode.Transport), new Uri("net.tcp://localhost:6667"));
            host1.AddServiceEndpoint(typeof(IDownload1), new NetTcpBinding(SecurityMode.Message), new Uri("net.tcp://localhost:6668"));
            host1.AddServiceEndpoint(typeof(IDownload1), new NetTcpBinding(SecurityMode.None) { TransferMode = TransferMode.StreamedResponse }, new Uri("net.tcp://localhost:6669"));
            host1.AddServiceEndpoint(typeof(IDownload1), new NetTcpBinding(SecurityMode.Transport) { TransferMode = TransferMode.StreamedResponse }, new Uri("net.tcp://localhost:6670"));
            host1.AddServiceEndpoint(typeof(IDownload1), new NetHttpBinding(BasicHttpSecurityMode.None, false), new Uri("http://localhost:6766"));

            host1.Open();
            Console.WriteLine("Host for " + host1.SingletonInstance.GetType().Name + " is open");

            var host2 = new ServiceHost(new Download2Service());

            host2.AddServiceEndpoint(typeof(IDownload2), new NetTcpBinding(SecurityMode.None) {TransferMode = TransferMode.StreamedResponse}, new Uri("net.tcp://localhost:7666"));
            host2.AddServiceEndpoint(typeof(IDownload2), new NetTcpBinding(SecurityMode.Transport) {TransferMode = TransferMode.StreamedResponse}, new Uri("net.tcp://localhost:7667"));
            // not supported
            //host2.AddServiceEndpoint(typeof(IDownload2), new NetTcpBinding(SecurityMode.Message) {TransferMode = TransferMode.StreamedResponse}, new Uri("net.tcp://localhost:7668"));
            host2.AddServiceEndpoint(typeof(IDownload2), new NetHttpBinding(BasicHttpSecurityMode.None, false){TransferMode = TransferMode.Buffered}, new Uri("http://localhost:7766"));

            host2.AddServiceEndpoint(typeof(IDownload2), new NetHttpBinding(BasicHttpSecurityMode.None, false){TransferMode = TransferMode.StreamedResponse}, new Uri("http://localhost:7769"));

            host2.Open();
            Console.WriteLine("Host for " + host2.SingletonInstance.GetType().Name + " is open");

            //host.AddServiceEndpoint(typeof(IDownload1), new NetTcpBinding(SecurityMode.TransportWithMessageCredential), new Uri("net.tcp://localhost:6669"));
            Console.ReadLine();
            host1.Close();
        }
    }
}
