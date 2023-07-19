using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer
{
   public class APIResponseBaseModel
    {
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
    }
    public class APIResponseModel : APIResponseBaseModel
    {
        
        public object Data { get; set; }
        public object Error { get; set; }
    }
}
