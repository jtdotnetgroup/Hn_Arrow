using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace hn.DataAccess
{
    public class DataValidation
    {
        public static List<string> Validata<T>(T data)
        {
            var t = typeof(T);
            var pis = t.GetProperties();

            List<string> result=null;

            foreach (var pi in pis)
            {
                var attrs = pi.GetCustomAttributes(true).Where(p => p is ValidationAttribute).ToList();

                var value = pi.GetValue(data);

                foreach (var attr in attrs)
                {
                    var a = attr as ValidationAttribute;
                    if (!a.IsValid(value))
                    {
                        if (result == null)
                        {
                            result = new List<string>();
                        }

                        result.Add(a.ErrorMessage);
                    }
                }
            }

            return result;
        }
    }
}