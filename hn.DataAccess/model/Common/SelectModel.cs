using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model.Common
{
    public class SelectModel
    {
        public string FID { get; set; }
        public string FName { get; set; }
        public override string ToString()
        {
            return FName;
        }
    }
}
