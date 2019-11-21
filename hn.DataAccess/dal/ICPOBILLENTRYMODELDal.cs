using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hn.DataAccess.dal
{
    public class ICPOBILLENTRYMODELDal : BaseRepository<ICPOBILLENTRYMODEL>
    {
        public static ICPOBILLENTRYMODELDal Instance
        {
            get { return SingletonProvider<ICPOBILLENTRYMODELDal>.Instance; }
        }
    }
}
