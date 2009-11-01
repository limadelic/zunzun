using System;
using System.Collections.Generic;

namespace Zunzun.Domain.Helpers {
    public static class Extensions {
        
        public static void ForEach<T>(this IEnumerable<T> Items, Action<T> Action) {
            foreach (var Item in Items) Action(Item);
        }
    }
}