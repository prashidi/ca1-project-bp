using BPCalculator;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.MsTest3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bpBddTests.Steps
{
    [TestClass]
    [FeatureDescription("Blood Pressure Category Calculation")]
    public class BloodPressureSteps : FeatureFixture
    {
        private BloodPressure bp = new BloodPressure();
        private string result = string.Empty;
        [Scenario]
        [Label("BP-01")]
        public void High_blood_pressure_systolic()
        {
            Runner.RunScenario(
                _ => Given_a_systolic_pressure_of(150),
                _ => And_a_diastolic_pressure_of(70),
                _ => When_I_calculate_the_blood_pressure(),
                _ => Then_the_result_should_be("High Blood Pressure")
            );
        }

        [Scenario]
        [Label("BP-02")]
        public void Ideal_blood_pressure()
        {
            Runner.RunScenario(
                _ => Given_a_systolic_pressure_of(110),
                _ => And_a_diastolic_pressure_of(70),
                _ => When_I_calculate_the_blood_pressure(),
                _ => Then_the_result_should_be("Ideal Blood Pressure")

            );

        }
        private void Given_a_systolic_pressure_of(int value)
        {
            bp = new BloodPressure();
            bp.Systolic = value;
        }

        private void And_a_diastolic_pressure_of(int value)

        {
            bp.Diastolic = value;
        }

        private void When_I_calculate_the_blood_pressure()
        {
            result = FormatCategory(bp.Category);
        }

        private void Then_the_result_should_be(string expected)
        {
            Assert.AreEqual(expected, result);
        }

        private string FormatCategory(BPCategory category)
        {
            return category switch {
                BPCategory.Ideal => "Ideal Blood Pressure",
                BPCategory.High => "High Blood Pressure",
                BPCategory.PreHigh => "Pre-High Blood Pressure",
                BPCategory.Low => "Low Blood Pressure",
                _ => ""

            };
        }
    }
}