using DataAccess;
using DataAccess.CustomEnums;
using hn.DataAccess.Model;

namespace hn.Client.Service.Respository
{
    public class ExpressCompanyRepository:AbstractCRUDRepository<TB_EXPRESSCOMPANYModel>
    {
        public ExpressCompanyRepository(DBTypeEnums dbType=DBTypeEnums.ORACLE) : base(dbType)
        {
        }
    }
}