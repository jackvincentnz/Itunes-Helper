using System.IO;

namespace ItunesHelper.Helpers
{
    public static class DirectoryHelper
    {
        public static bool HasChildFolderWithFiles (string path)
        {
            if (Directory.Exists(path))
            {
                string[] subdirectoryEntries = Directory.GetDirectories(path);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    if (ContainsFiles(subdirectory))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool ContainsFiles (string path)
        {
            if (Directory.Exists(path))
            {
                string[] fileEntries = Directory.GetFiles(path);
                if (fileEntries.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsDirectories (string path)
        {
            if (Directory.Exists(path))
            {
                string[] directories = Directory.GetDirectories(path);
                if (directories.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
