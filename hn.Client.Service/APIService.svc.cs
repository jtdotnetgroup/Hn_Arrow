﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using hn.DataAccess.Model;
using hn.Core.Dal;
using hn.Core.Model;
using hn.DataAccess.model;

namespace hn.Client.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class APIService : IAPIService
    {

    }
}
