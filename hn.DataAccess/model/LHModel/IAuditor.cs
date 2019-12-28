namespace hn.DataAccess.dal.LHModel
{
    public interface IAuditor<T>
    {
        string UserID { get; set; }
        AuditEnums AuditType { get; set; }

        string AuditBILL(T bill);
    }
}