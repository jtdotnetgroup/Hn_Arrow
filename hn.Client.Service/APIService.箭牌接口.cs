using hn.ArrowInterface.Entities;
using hn.ArrowInterface.RequestParams;
using hn.ArrowInterface.WebCommon;

namespace hn.Client.Service
{
    public partial class APIService
    {
        public bool SaleOrderUpload(SaleOrderUploadParam pars)
        {
            var token = CommonToken.GetToken();


            return true;
        }

        public bool AcctOaStatus(AcctOAStatusParam pars)
        {
            throw new System.NotImplementedException();
        }

        public bool obOrderUpload(ObOrderUploadParam pars)
        {
            throw new System.NotImplementedException();
        }
    }
}