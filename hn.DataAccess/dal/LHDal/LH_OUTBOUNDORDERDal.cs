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
    public class LH_OUTBOUNDORDERDal : BaseRepository<LH_OUTBOUNDORDERModel>
    {
        public static LH_OUTBOUNDORDERDal Instance
        {
            get { return SingletonProvider<LH_OUTBOUNDORDERDal>.Instance; }
        }
    }
}
