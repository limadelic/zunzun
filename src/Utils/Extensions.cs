using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Zunzun.Utils {

    public static class Extensions {
        
        static readonly List<string> AcceptedProtocols = new List<string> { "http", "https", "ftp" };
        
        public static bool IsUrl(this string Text) {return
            Uri.IsWellFormedUriString(Text, UriKind.Absolute)
            && AcceptedProtocols.Contains(UriScheme(Text))
        ;}

        static string UriScheme(string Text) { return new Uri(Text).Scheme; }

        public static void ForEach<T>(this IEnumerable<T> Items, Action<T> Action) {
            foreach (var Item in Items) Action(Item);
        }

        public static void InsertAtTop<T>(this ObservableCollection<T> Items, List<T> NewItems) {
            for (var i = NewItems.Count - 1; i >= 0; i--) 
                Items.Insert(0, NewItems[i]);
        }

        public static void InsertAtTop<T>(this List<T> Items, List<T> NewItems)
        {
            if(Items.Count > 0)
                Items.InsertRange(0, NewItems);
            else 
                Items = NewItems;
        }

        public static void Debug(this object obj) { Debugger.Launch(); }
    }
}