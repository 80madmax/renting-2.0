using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReadModels
{
    public class UnitROI
    {
        public string Unit { get; }
        public decimal RoiPercent { get; }

        public UnitROI(string unit, decimal roiPercent)
        {
            Unit = unit;
            RoiPercent = roiPercent;
        }
    }
}
