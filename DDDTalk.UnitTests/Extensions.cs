using System;
using System.Collections.Generic;

namespace DDDTalk.UnitTests
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (list != null && action != null)
            {
                foreach (var item in list)
                {
                    action.Invoke(item);
                }
            }
        }
    }
}