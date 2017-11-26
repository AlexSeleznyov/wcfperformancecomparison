using System;
using System.ServiceModel;

namespace WcfClient
{
    internal abstract class CaseBase
    {
        protected int Port { get; }
        protected TransferMode TransferMode { get; }

        protected CaseBase(int port, TransferMode transferMode)
        {
            Port = port;
            TransferMode = transferMode;
        }

        public override string ToString()
        {
            return $"port: {Port, 4}, transferMode: {TransferMode,20}";
        }
    }

    internal abstract class CaseBase<TSecMode> : CaseBase
    {
        protected TSecMode Mode { get; }

        protected CaseBase(int port, TSecMode securityMode, TransferMode transferMode) : base(port, transferMode)
        {
            Mode = securityMode;
        }

        public override string ToString()
        {
            return $"port: {Port, 4}, securityMode: {Mode,10}, transferMode: {TransferMode,20}";
        }
    }

    internal abstract class CaseBase<T, TSecMode> : CaseBase<TSecMode> where T: class
    {
        private readonly Func<TSecMode, TransferMode, ChannelFactory<T>> _factoryFunc;
        private readonly Func<ChannelFactory<T>, int, T> _channelFunc;

        protected CaseBase(int port, TSecMode securityMode, TransferMode transferMode, Func<TSecMode, TransferMode, ChannelFactory<T>> factoryFunc, Func<ChannelFactory<T>, int, T> channelFunc) 
            : base(port, securityMode, transferMode)
        {
            _factoryFunc = factoryFunc;
            _channelFunc = channelFunc;
        }

        protected ChannelFactory<T> GetChannelFactory()
        {
            return _factoryFunc(Mode, TransferMode);
        }

        protected T GetClient(ChannelFactory<T> cf)
        {
            return _channelFunc(cf, Port);
        }

        public override string ToString()
        {
            var s = $"{GetType().Name}<{typeof(T).Name}>";
            return $"{s,45} {base.ToString()}";
        }
    }
}
