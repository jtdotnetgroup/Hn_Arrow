using System;

namespace DataAccess.CustomAttributes
{
    public class JIT_TableAttribute:TableNameAttribute
    {
        public JIT_TableAttribute(string tableName) : base(tableName)
        {
        }
    }
}