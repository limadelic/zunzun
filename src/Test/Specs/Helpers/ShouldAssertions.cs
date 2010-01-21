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
        
        public static void ShouldStartWith(this string Whole, string Start) {
            
            Whole.StartsWith(Start).ShouldBeTrue();
        }
        
        public static void ShouldEndWith(this string Whole, string End) {
            
            Whole.EndsWith(End).ShouldBeTrue();
        }
        
        public static void ShouldContain<T>(this List<T> Items, T Item) {
            
            Assert.IsTrue(Items.Contains(Item), "Item is not in List");
        }

        public static void ShouldBe<T>(this List<T> Ones, List<T> Others) {
            
            Ones.Count.ShouldBe(Others.Count);
            
            for (var i = 0; i < Ones.Count; i++)
                Ones[i].ShouldBe(Others[i]);
        }

        public static void ShouldNotBeEmpty(this string String) {
            
            Assert.IsFalse(String.IsNullOrEmpty(String), "Unexpected Empty String");
        }

        public static void ShouldBeA<T>(this Object Obj) {
            
            (Obj is T).ShouldBeTrue();            
        }

        public static void ShouldBeGreaterThan(this int Bigger, int Smaller) {
            
            (Bigger > Smaller).ShouldBeTrue();            
        }

        public static void ShouldBeLessThan(this int Smaller, int Bigger) {
            
            (Smaller < Bigger).ShouldBeTrue();            
        }
    }
}