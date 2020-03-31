using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Extentions
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            if (list == null)
                return true;

            return list.Count == 0;
        }
    }
}
