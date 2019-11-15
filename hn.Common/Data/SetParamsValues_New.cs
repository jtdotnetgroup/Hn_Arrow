using Omu.ValueInjecter;
using Oracle.ManagedDataAccess.Client;

namespace hn.Common.Data
{
    public class SetParamsValues_New: KnownTargetValueInjection<OracleCommand>
    {
        protected override void Inject(object source, ref OracleCommand target)
        {
            throw new System.NotImplementedException();
        }
    }
}