using System;

namespace DataAccess.CustomAttributes
{
    public class NotNullAttribute:Attribute
    {
        public string ErrorMessage { get; set; }

        public NotNullAttribute(string msg = "字段不能为NULL")
        {
            ErrorMessage = msg;
        }
    }
}