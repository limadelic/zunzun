using System.Diagnostics;
using fit;

namespace Zunzun.Specs.Fixtures {

    public class DebugFixture : Fixture {

        public DebugFixture() { Debugger.Launch(); }
    }
}