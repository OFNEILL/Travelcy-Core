using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelcy.App.DataObjects.Responses
{
    public class ResponseBase
    {
        public ResponseBase()
        {
            IsSuccessful = true;
            ErrorMessage = null;
            ResponseCode = 200;
        }

        [Key]
        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }

        public int ResponseCode { get; set; }
    }
}
