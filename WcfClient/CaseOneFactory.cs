using System;
using System.ServiceModel;

namespace WcfClient
{
    internal sealed class CaseOneFactory<T> : CaseOneFactory<T, SecurityMode> where T : class
    {
        public CaseOneFactory(int port, SecurityMode securityMode, Action<T> getFileAction, TransferMode transferMode, Func<SecurityMode, TransferMode, ChannelFactory<T>> factoryFunc, Func<ChannelFactory<T>, int, T> channelFunc) : base(port, securityMode, getFileAction, transferMode, factoryFunc, channelFunc)
        {
        }
    }

    internal class CaseOneFactory<T, TSecMode> : CaseBase<T, TSecMode>, ITestCase where T : class
    {
        private readonly Action<T> _getFileAction;

        private readonly ChannelFactory<T> _channelFactory;

        public CaseOneFactory(int port, TSecMode securityMode, Action<T> getFileAction, TransferMode transferMode, Func<TSecMode, TransferMode, ChannelFactory<T>> factoryFunc, Func<ChannelFactory<T>, int, T> channelFunc) : base(port, securityMode, transferMode, factoryFunc, channelFunc)
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