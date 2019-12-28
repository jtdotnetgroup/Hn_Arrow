using hn.DataAccess.dal.LHModel;

namespace hn.Client.Service.LH
{
    public interface IAuditor<T>
    {
        string UserID { get; set; }
        

        string AuditBILL(T bill, AuditEnums auditType);
    }
}