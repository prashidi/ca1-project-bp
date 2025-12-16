using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPCalculator;

namespace UnitTestBP
{
    [TestClass]
    public class EmergencyTests
    {
        [TestMethod]
        public void EmergencyTrueWhenSystolicAbove180()
        {
            var bp = new BloodPressure { Systolic = 185, Diastolic = 90 };
            Assert.IsTrue(bp.IsEmergency);
        }
        [TestMethod]
        public void EmergencyTrueWhenDiastolicAbove120()
        {
            var bp = new BloodPressure { Systolic = 130, Diastolic = 125 };
            Assert.IsTrue(bp.IsEmergency);
        }
        [TestMethod]
        public void EmergencyFalseForNormalValues()
        {
            var bp = new BloodPressure { Systolic = 120, Diastolic = 80 };
            Assert.IsFalse(bp.IsEmergency);
        }
        [TestMethod]
        public void EmergencyFalseForPreHigh()
        {
            var bp = new BloodPressure { Systolic = 135, Diastolic = 85 };
            Assert.IsFalse(bp.IsEmergency);
        }
    }
}