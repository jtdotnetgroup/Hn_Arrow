namespace hn.Core
{
    public static class StringExtension
    {
        public static bool Empty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string If(this string str,bool condition)
        {
            return condition ? str : string.Empty;
        }
    }
}