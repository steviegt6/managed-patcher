using System.IO;

namespace ManagedPatcher.Utilities
{
    public static class InOutUtils
    {
        public static void CopyDirectory(DirectoryInfo from, DirectoryInfo to, bool recurse)
        {
            if (to.Exists)
                to.Delete(true);

            DirectoryInfo[] dirs = from.GetDirectories();
            to.Create();

            foreach (FileInfo file in from.GetFiles())
            {
                string target = Path.Combine(to.FullName, file.Name);
                file.CopyTo(target);
            }

            if (!recurse)
                return;
            
            foreach (DirectoryInfo subDir in dirs)
            {
                string destDir = Path.Combine(to.FullName, subDir.Name);
                CopyDirectory(subDir, new DirectoryInfo(destDir), true);
            }
        }
    }
}