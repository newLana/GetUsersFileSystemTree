using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTree
{
    class GetterOfSystemTree
    {
        public string UsersDocsPath => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string ResFileName => Path.GetFileName(usersDesktopPath);

        private string usersDesktopPath;
        private StringBuilder sb;

        public GetterOfSystemTree(string symbol)
        {
            usersDesktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Environment.UserName + "Docs.txt");
            sb = new StringBuilder();
            GetTree(new DirectoryInfo(UsersDocsPath), sb, symbol);
        }       

        private void GetTree(DirectoryInfo directoryInfo, StringBuilder sb, string shift)
        {
            if(directoryInfo != null)
            {
                var checkTime = DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0));
                var allFileSystemInfos = directoryInfo.EnumerateFileSystemInfos().Where(f => (!f.Attributes.HasFlag(FileAttributes.ReparsePoint)) && (f.CreationTime < checkTime));
                if (allFileSystemInfos != null)
                {
                    foreach (var item in allFileSystemInfos)
                    {
                        sb.Append(shift+item.Name + Environment.NewLine);
                        if(item is DirectoryInfo)
                        {
                            GetTree(item as DirectoryInfo, sb, shift + shift[0]);
                        }
                    }
                }
            }
        }

        public bool WriteTree()
        {
            bool isWrited = false;
            if(sb != null)
            {                
                using (StreamWriter fi = new StreamWriter(usersDesktopPath, false, Encoding.Default))
                {
                    if (!File.Exists(usersDesktopPath))
                        File.Create(usersDesktopPath);
                    fi.Write(sb.ToString());
                }
                isWrited = true;
            }
            return isWrited;
        }
    }
}
