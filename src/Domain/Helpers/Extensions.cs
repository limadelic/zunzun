using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Zunzun.Domain.Helpers {
    public static class Extensions {
        
        public static void ForEach<T>(this IEnumerable<T> Items, Action<T> Action) {
            foreach (var Item in Items) Action(Item);
        }

        public static void InsertAtTop<T>(this ObservableCollection<T> Items, List<T> NewItems) {
            for (var i = NewItems.Count - 1; i >= 0; i--) 
                Items.Insert(0, NewItems[i]);
        }
    }
}