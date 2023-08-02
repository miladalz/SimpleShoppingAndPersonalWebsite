namespace MyWeb.Common
{
    public static class ListHelper
    {
        public static bool AddIfNotExists<T>(this List<T> list, T value)
        {

            if (!list.Contains(value))
            {

                list.Add(value);
                return true;
            }
            return false;
        }
    }
}
