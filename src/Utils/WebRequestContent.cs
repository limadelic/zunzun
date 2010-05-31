namespace Zunzun.Utils {

    public interface WebRequestContent {
    
        void Append(string Name, string Value);
        void AppendData(string fileHeader, string ContentType, byte[] Value);
        void AppendHeader();
        void AppendFooter();
        
        string Boundary { get; }
        byte[] Data { get; }
        int Length { get; }
    }
}