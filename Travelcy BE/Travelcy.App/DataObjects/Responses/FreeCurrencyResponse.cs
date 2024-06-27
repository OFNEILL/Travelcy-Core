using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelcy.App.DataObjects.Objects;

namespace Travelcy.App.DataObjects.Responses
{
    public class FreeCurrencyResponse
    {
        [Key]
        public ApiData Data { get; set; }
    }
}
