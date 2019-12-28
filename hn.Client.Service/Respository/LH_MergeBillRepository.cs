using DataAccess;
using DataAccess.CustomEnums;
using hn.DataAccess.dal.LHModel;

namespace hn.Client.Service.Respository
{
    public class LH_MergeBillRepository:AbstractCRUDRepository<LH_MergeBill>
    {
        public LH_MergeBillRepository(DBTypeEnums dbType=DBTypeEnums.ORACLE) : base(dbType)
        {
        }
    }
}