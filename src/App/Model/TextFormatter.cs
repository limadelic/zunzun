using System.Collections.Generic;
using System.Windows.Documents;

namespace Zunzun.App.Model {

    public interface TextFormatter {
    
        List<Inline> TokensFrom(string Text);
    }
}