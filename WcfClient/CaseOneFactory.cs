using System;
using System.ServiceModel;

namespace WcfClient
{
    internal sealed class CaseOneFactory<T> : CaseBase<T>, ITestCase where T : class
    {
        private readonly Action<T> _getFileAction;

        private readonly ChannelFactory<T> _channelFactory;

        public CaseOneFactory(int port, SecurityMode securityMode, Action<T> getFileAction, TransferMode transferMode, Func<SecurityMode, TransferMode, ChannelFactory<T>> factoryFunc, Func<ChannelFactory<T>, int, T> channelFunc) : base(port, securityMode, transferMode, factoryFunc, channelFunc)
        {
            _getFileAction = getFileAction;
            _channelFactory = GetChannelFactory();
        }

        public void Run()
        {
            var client = GetClient(_channelFactory);
            _getFileAction(client);
            ((IClientChannel)client).Dispose();
        }

        public void Dispose()
        {
            ((IDisposable)_channelFactory).Dispose();
        }
    }
}