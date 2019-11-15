using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class v_lhproducts_policyDal : BaseRepository<v_lhproducts_policyModel>
    {
        public static v_lhproducts_policyDal Instance
        {
            get { return SingletonProvider<v_lhproducts_policyDal>.Instance; }
        } 
    }
}
