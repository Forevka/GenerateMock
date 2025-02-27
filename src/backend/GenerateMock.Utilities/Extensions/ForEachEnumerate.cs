﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateMock.Utilities.Extensions
{
    public static class ForEachEnumerate
    {
        public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }
    }
}
