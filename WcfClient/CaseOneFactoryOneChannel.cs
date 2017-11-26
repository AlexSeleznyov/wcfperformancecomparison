using System;
using System.ServiceModel;

namespace WcfClient
{
    internal class CaseOneFactoryOneChannel<T> : CaseOneFactoryOneChannel<T, SecurityMode> where T : class
    {
        public CaseOneFactoryOneChannel(int port, SecurityMode securityMode, Action<T> getFileAction, TransferMode transferMode, Func<SecurityMode, TransferMode, ChannelFactory<T>> factoryFunc, Func<ChannelFactory<T>, int, T> channelFunc) : base(port, securityMode, getFileAction, transferMode, factoryFunc, channelFunc)
        {
        }
    }

    internal class CaseOneFactoryOneChannel<T, TSecMode> : CaseBase<T, TSecMode>, ITestCase where T:class
    {
        private readonly Action<T> _getFileAction;

        private readonly T _client;
        private readonly ChannelFactory<T> _channelFactory;

        public CaseOneFactoryOneChannel(int port, TSecMode securityMode, Action<T> getFileAction, TransferMode transferMode, Func<TSecMode, TransferMode, ChannelFactory<T>> factoryFunc, Func<ChannelFactory<T>, int, T> channelFunc) : base(port, securityMode, transferMode, factoryFunc, channelFunc)
        {
            _getFileAction = getFileAction;
            _channelFactory = GetChannelFactory();
            _client = GetClient(_channelFactory);
        }

        public void Run()
        {
            var download1 = _client;
            _getFileAction(download1);
        }

        public void Dispose()
        {
            ((IClientChannel)_client).Dispose();
            ((IDisposable)_channelFactory).Dispose();
        }
    }
}