using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using fit;

namespace Zunzun.Specs.Helpers {

    public class Error : ColumnFixture {
    
        string Spec { get; set; }
        string Step { get; set; }
        string Message { get; set; }

        static readonly List<Error> log = new List<Error>();

        public string Log { get { return 
            log.Count == 0 ? Passed : LogTable
        ;}}

        const string Passed = "<span class=\"pass\" style=\"color: black;\">no errors found</span>";
        
        string LogTable { get { return log.Aggregate(
            "<table style=\"color: black;\">" + 
                HeaderRow, 
                (Rows, Error) => Rows + NewRow(Error)) + 
            "</table>"
        ;}}

        string HeaderRow { get { return
            "<tr><td>Spec</td><td>Step</td><td>Error</td></tr>"
        ;}}
        
        string NewRow(Error Error) { return
            "<tr><td>" + 
                Error.Spec + 
            "</td><td>" + 
                Error.Step + 
            "</td><td class=\"fail\">" + 
                Error.Message + 
            "</td></tr>"
        ;}

        public static void Add(Exception Exception) {
            var Step = MethodInFixture;
            
            log.Add(new Error {
                Spec = Step.DeclaringType.Name,
                Step = Step.Name,
                Message = Exception.Message,
            });
        }

        static MethodBase MethodInFixture { get { return 
            new StackTrace().GetFrame(3).GetMethod()
        ;}}
        
        public static void Add(string Spec, string Step, string Message) { log.Add(new Error {
            Spec = Spec,
            Step = Step,
            Message = Message,
        });}
    }
}