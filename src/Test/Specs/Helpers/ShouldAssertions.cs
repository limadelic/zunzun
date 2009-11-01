using System;
using System.Collections.Generic;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zunzun.Specs.Helpers {
    
    public static class ShouldAssertions {
    
        public static void ShouldNotBeEmpty<T>(this List<T> Items) {
            
            Assert.IsTrue(Items.Count > 0, "Unexpected empty list of " + typeof (T));
        }

        public static void ShouldContain(this string Whole, string Part) {
            
            Whole.Contains(Part).ShouldBeTrue();
        }
        
        public static void ShouldContain<T>(this List<T> Items, T Item) {
            
            Assert.IsTrue(Items.Contains(Item), "Item is not in List");
        }

        public static void ShouldNotBeEmpty(this string String) {
            
            Assert.IsFalse(String.IsNullOrEmpty(String), "Unexpected Empty String");
        }

        public static void ShouldBeA<T>(this Object Obj) {
            
            (Obj is T).ShouldBeTrue();            
        }
    }
}