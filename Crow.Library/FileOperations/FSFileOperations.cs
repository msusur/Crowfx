using System.IO;
using Crow.Library.Foundation.FileOperations;

namespace Crow.Library.FileOperations
{
    public class FSFileOperations : IFileOperations
    {
        public byte[] LoadFileAsByteArray(string filePath, bool encrypted)
        {
            return File.ReadAllBytes(filePath);
        }
        public string LoadFileAsPlainText(string filePath, bool encrypted)
        {
            return File.ReadAllText(filePath);
        }
        public void SaveFile(string filePath, string content, bool encrypted)
        {
            File.WriteAllText(filePath, content);
        }
        public void SaveFile(string filePath, byte[] content, bool encrypted)
        {
            File.WriteAllBytes(filePath, content);
        }
    }
}