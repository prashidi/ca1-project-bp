using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPCalculator;

namespace UnitTestBP;

[TestClass]
public sealed class BPUnitTests
{
    [TestMethod]
    public void TestIdealPB()
    {
        BloodPressure bp = new BloodPressure()
        {
            Systolic = 110,
            Diastolic = 70
        };
        Assert.AreEqual(BPCategory.Ideal, bp.Category);
    }
}
