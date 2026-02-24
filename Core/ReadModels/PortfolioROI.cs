using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReadModels
{
    public class PortfolioROI
    {
        public decimal TotalCost { get; }
        public decimal TotalNet { get; }          // sum of ALL transactions (positive + negative)
        public decimal RoiPercent { get; }        // TotalNet / TotalCost * 100

        public PortfolioROI(decimal totalCost, decimal totalNet)
        {
            TotalCost = totalCost;
            TotalNet = totalNet;
            RoiPercent = totalCost <= 0m ? 0m : (totalNet / totalCost) * 100m;
        }
    }

}
