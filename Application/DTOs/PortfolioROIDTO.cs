using Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PortfolioROIDTO
    {
        public PortfolioROI Data { get; init; } = new PortfolioROI(0m, 0m);
    }
}
