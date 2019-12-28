using System.Collections.Generic;

namespace hn.DataAccess.model.Common
{
    public class PageResult<T>
    {
        public int Total { get; set; }
        public List<T> Result { get; set; }
    }
}