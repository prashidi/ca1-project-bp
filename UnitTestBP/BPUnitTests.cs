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
    [DataTestMethod]
    // Ideal
    [DataRow(100, 70, BPCategory.Ideal)]      
    [DataRow(110, 60, BPCategory.Ideal)]      
    // Low
    [DataRow(85, 55, BPCategory.Low)]
    [DataRow(100, 45, BPCategory.Low)]
    // Pre-High
    [DataRow(125, 75, BPCategory.PreHigh)]
    [DataRow(118, 85, BPCategory.PreHigh)]
    // High
    [DataRow(145, 70, BPCategory.High)]       
    [DataRow(120, 95, BPCategory.High)]       
    public void TestBPCategories(int systolic, int diastolic, BPCategory expected)
    {
        BloodPressure bp = new BloodPressure()
        {
            Systolic = systolic,
            Diastolic = diastolic
        };

        Assert.AreEqual(expected, bp.Category);
        
    }
}
