using System;

namespace Crow.Library.Foundation.FileOperations
{
    public interface IFileOperations
    {
        byte[] LoadFileAsByteArray(string filePath, bool encrypted);
        string LoadFileAsPlainText(string filePath, bool encrypted);
        void SaveFile(string filePath, byte[] content, bool encrypted);
        void SaveFile(string filePath, string content, bool encrypted);
    }
}
