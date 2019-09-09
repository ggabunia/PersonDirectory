using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PersonDirectory.HelperClasses
{
    public static class FileHandler
    {
        private const string mediaPath = "~/Media";
        public static string GetMediaPath()
        {
            return mediaPath;
        }
        public static string GetRelativeMediaPath(string fileName)
        {
            return string.Format("{0}/{1}", mediaPath, fileName);
        }
  
        public static string GetAbsolutePath(string relativePath, string fileName)
        {
            return System.IO.Path.Combine(HostingEnvironment.MapPath(relativePath), fileName);
            
        }
        public static string SaveFile(HttpPostedFileBase file)
        {
            if (file == null) return null;
            try
            {
                var result = Guid.NewGuid().ToString();
                var fileExtension = System.IO.Path.GetExtension(file.FileName);
                result = String.Format("{0}{1}", result, fileExtension);
                var path = GetAbsolutePath(GetMediaPath(), result);
                file.SaveAs(path);
                return GetRelativeMediaPath(result);
            }
            catch
            {
                return null;
            }
        }
        public static bool RemoveFile(string fileName)
        {
            try
            {
                var path = GetAbsolutePath(GetMediaPath(), fileName);
                System.IO.File.Delete(path);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}