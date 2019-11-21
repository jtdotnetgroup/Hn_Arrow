using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.dal.LHModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hn.DataAccess.dal.LHDal
{
    public class LH_OUTBOUNDORDERDETAILEDDal : BaseRepository<LH_OUTBOUNDORDERDETAILEDModel>
    {
        public static LH_OUTBOUNDORDERDETAILEDDal Instance
        {
            get { return SingletonProvider<LH_OUTBOUNDORDERDETAILEDDal>.Instance; }
        } 
    }
}
