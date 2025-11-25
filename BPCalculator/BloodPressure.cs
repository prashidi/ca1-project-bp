using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name = "Low Blood Pressure")] Low,
        [Display(Name = "Ideal Blood Pressure")] Ideal,
        [Display(Name = "Pre-High Blood Pressure")] PreHigh,
        [Display(Name = "High Blood Pressure")] High
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        // [Display(Name = "Systolic Value")]
        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                int syst = this.Systolic;
                int diast = this.Diastolic;

                // High: systolic ≥ 140 OR diastolic ≥ 90
                if (syst >= 140 || diast >= 90)
                {
                    return BPCategory.High;
                }

                // Pre-high: systolic 120–139 OR diastolic 80–89
                if ((syst >= 120 && syst <= 139) || (diast >= 80 && diast <= 89))
                {
                    return BPCategory.PreHigh;
                }

                // Ideal: systolic 90–119 AND diastolic 40–79
                if ((syst >= 90 && syst <= 119) && (diast >= 40 && diast <= 79))
                {
                    return BPCategory.Ideal;
                }
                //Low: everything below ideal thresholds
                return BPCategory.Low;
            }
        }
    }
}
