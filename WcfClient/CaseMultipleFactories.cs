using System;
using System.ServiceModel;

namespace WcfClient
{
    internal sealed class CaseMultipleFactories<T> : CaseBase<T>, ITestCase where T : class
    {
        private readonly Action<T> _getFileAction;

        public CaseMultipleFactories(int port, SecurityMode securityMode, Action<T> getFileAction, TransferMode transferMode, Func<SecurityMode, TransferMode, ChannelFactory<T>> factoryFunc, Func<ChannelFactory<T>, int, T> channelFunc) : base(port, securityMode, transferMode, factoryFunc, channelFunc)
        {
            _getFileAction = getFileAction;
        }

        public void Run()
        {
            var channelFactory = GetChannelFactory();
            var client = GetClient(channelFactory);
            _getFileAction(client);
            ((IClientChannel)client).Dispose();
            ((IDisposable)channelFactory).Dispose();
        }

        public void Dispose()
        {
        }
    }
}