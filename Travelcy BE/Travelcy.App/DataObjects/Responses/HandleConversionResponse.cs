using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelcy.App.DataObjects.Responses
{
    public class HandleConversionResponse : ResponseBase
    {
        [Key]
        public float ConvertedAmount { get; set; }

        public float ConversionCharge { get; set; }
    }
}