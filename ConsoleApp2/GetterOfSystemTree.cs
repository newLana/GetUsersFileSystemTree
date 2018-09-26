using System;
using System.IO;
using System.Linq;
using System.Text;
using System.IO.Compression;

namespace FSTree
{
    class GetterOfSystemTree
    {
        public string UsersDocsPath => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string ResFileName => Path.GetFileName(usersDesktopPath);

        private string usersDesktopPath;
        private StringBuilder sb;
        private char symbol;

        public GetterOfSystemTree(string symbol)
        {
            usersDesktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Environment.UserName + "Docs.gz");
            sb = new StringBuilder();
            this.symbol = string.IsNullOrEmpty(symbol)?' ':symbol[0];
            GetTree(new DirectoryInfo(UsersDocsPath));
        }

        private bool GetTree(DirectoryInfo directoryInfo)
        {
            bool flag = false;
            int shift = directoryInfo.FullName.Remove(0, UsersDocsPath.Length).Count(c => c == '\\');
            sb.AppendLine(new string(symbol, shift) + directoryInfo.Name);
            foreach (var directory in directoryInfo.EnumerateDirectories().Where(d => !d.Attributes.HasFlag(FileAttributes.ReparsePoint)))
            {
                flag = GetTree(directory);
            }
            foreach (var file in directoryInfo.EnumerateFiles().Where(f => (!f.Attributes.HasFlag(FileAttributes.ReparsePoint)) && (f.CreationTimeUtc < DateTime.Now.ToUniversalTime().Subtract(new TimeSpan(15, 0, 0, 0)))))
            {
                sb.AppendLine(new string(symbol, shift + 1) + file.Name);
                flag = true;
            }              
            if(flag != true)
            { 
                var index = sb.ToString().TrimEnd(new char[] { '\n', '\r' }).LastIndexOf(Environment.NewLine);
                sb.Remove(index + 1, sb.Length - index - 2);
            }
            return flag;
        }

        public bool WriteAndCompressTree()
        {
            if(sb != null)
            {
                using (var mw = new MemoryStream(Encoding.Default.GetBytes(sb.ToString())))
                {
                    using (var fi = File.OpenWrite(usersDesktopPath))
                    {
                        using (var compressionStream = new GZipStream(fi, CompressionMode.Compress))
                        {
                            mw.CopyTo(compressionStream);
                        }
                    }
                }                    
                return true;
            }
            return false;
        }

        public bool DecompressTreeFile()
        {
            if (!File.Exists(usersDesktopPath))
                return false;
            using (var sourceStream = new FileStream(usersDesktopPath, FileMode.Open))
            {
                using (var targetStream = File.OpenWrite(Path.ChangeExtension(usersDesktopPath, "txt")))
                {
                    using (var decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        return true;
                    }
                }
            }
        }
    }
}
