using System.Data;
using DataAccess;
using DataAccess.CustomEnums;

namespace hn.Client.Service.Respository
{
    public class DefaultRepository<T>:AbstractCRUDRepository<T> where T : class, new()
    {
        public DefaultRepository(DBTypeEnums dbType) : base(dbType)
        {

        }
    }
}