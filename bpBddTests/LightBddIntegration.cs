using Microsoft.VisualStudio.TestTools.UnitTesting;
using LightBDD.Framework;
using LightBDD.MsTest3;

namespace bpBddTests
{
    [TestClass]
    public class LightBddIntegration
    {
        [AssemblyInitialize]
        public static void Setup(TestContext testContext)
        {
            LightBddScope.Initialize();
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            LightBddScope.Cleanup();
        }
    }

}