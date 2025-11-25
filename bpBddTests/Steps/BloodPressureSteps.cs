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
                _ => Then_the_result_should_be("High")
            );
        }

        [Scenario]
        [Label("BP-02")]
        public void High_blood_pressure_by_diastolic()
        {
            Runner.RunScenario(
                _ => Given_a_systolic_pressure_of(120),
                _ => And_a_diastolic_pressure_of(95),
                _ => When_I_calculate_the_blood_pressure(),
                _ => Then_the_result_should_be("High")
            );
        }

        [Scenario]
        [Label("BP-03")]
        public void Pre_high_by_systolic()
        {
            Runner.RunScenario(
                _ => Given_a_systolic_pressure_of(130),
                _ => And_a_diastolic_pressure_of(75),
                _ => When_I_calculate_the_blood_pressure(),
                _ => Then_the_result_should_be("PreHigh")
            );
        }

        [Scenario]
        [Label("BP-04")]
        public void Pre_high_by_diastolic()
        {
            Runner.RunScenario(
                _ => Given_a_systolic_pressure_of(118),
                _ => And_a_diastolic_pressure_of(85),
                _ => When_I_calculate_the_blood_pressure(),
                _ => Then_the_result_should_be("PreHigh")
            );
        }

        [Scenario]
        [Label("BP-05")]
        public void Ideal_blood_pressure()
        {
            Runner.RunScenario(
                _ => Given_a_systolic_pressure_of(110),
                _ => And_a_diastolic_pressure_of(70),
                _ => When_I_calculate_the_blood_pressure(),
                _ => Then_the_result_should_be("Ideal")

            );

        }

        [Scenario]
        [Label("BP-06")]
        public void Ideal_at_lower_boundary()
        {
            Runner.RunScenario(
                _ => Given_a_systolic_pressure_of(100),
                _ => And_a_diastolic_pressure_of(40),
                _ => When_I_calculate_the_blood_pressure(),
                _ => Then_the_result_should_be("Ideal")
            );
        }
        [Scenario]
        [Label("BP-07")]
        public void Low_by_systolic()
        {
            Runner.RunScenario(
                _ => Given_a_systolic_pressure_of(85),
                _ => And_a_diastolic_pressure_of(55),
                _ => When_I_calculate_the_blood_pressure(),
                _ => Then_the_result_should_be("Low")
            );
        }

        [Scenario]
        [Label("BP-08")]
        public void Low_by_diastolic()
        {
            Runner.RunScenario(
                _ => Given_a_systolic_pressure_of(100),
                _ => And_a_diastolic_pressure_of(35),
                _ => When_I_calculate_the_blood_pressure(),
                _ => Then_the_result_should_be("Low")
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
            result = bp.Category.ToString();
        }

        private void Then_the_result_should_be(string expected)
        {
            Assert.AreEqual(expected, result);
        }

    }
}