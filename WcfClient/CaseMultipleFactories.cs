using System;
using System.ServiceModel;

namespace WcfClient
{
    internal class CaseMultipleFactories<T> : CaseMultipleFactories<T, SecurityMode> where T : class
    {
        public CaseMultipleFactories(int port, SecurityMode securityMode, Action<T> getFileAction, TransferMode transferMode, Func<SecurityMode, TransferMode, ChannelFactory<T>> factoryFunc, Func<ChannelFactory<T>, int, T> channelFunc) : base(port, securityMode, getFileAction, transferMode, factoryFunc, channelFunc)
        {
        }
    }

    internal class CaseMultipleFactories<T, TSecMode> : CaseBase<T, TSecMode>, ITestCase where T : class
    {
        private readonly Action<T> _getFileAction;

        public CaseMultipleFactories(int port, TSecMode securityMode, Action<T> getFileAction, TransferMode transferMode, Func<TSecMode, TransferMode, ChannelFactory<T>> factoryFunc, Func<ChannelFactory<T>, int, T> channelFunc) : base(port, securityMode, transferMode, factoryFunc, channelFunc)
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