using System;

namespace WcfClient
{
    internal interface ITestCase : IDisposable
    {
        void Run();
    }
}