using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelcy.App.DataObjects.Responses;

namespace Travelcy.App.Interfaces
{
    public interface IBusinessRepository
    {
        public Task<HandleConversionResponse> HandleConversionAsync(float amount, string currency);
    }
}
