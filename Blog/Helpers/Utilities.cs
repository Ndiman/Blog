using Blog.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace Blog.Helpers
{
    public class Utilities
    {
        public static string MakeAbstract(string body)
        {
            if(body != null)
            {
                return body.Split('.')[0];
            }
            else
            {
                return "Has no body";
            }
        }

        public static List<BlogPost> GetRecentPosts()
        {
            var db = new ApplicationDbContext();
            return db.BlogPosts.OrderByDescending(b => b.Created).Where(p => p.Published == true).Take(3).ToList();
        }
    }
    public static class ImageUploadValidator
    {
        public static bool IsWebFriendlyImage(HttpPostedFileBase file)
        {
            if (file == null)
                return false;
            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
                return false;
            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                           ImageFormat.Png.Equals(img.RawFormat) ||
                           ImageFormat.Gif.Equals(img.RawFormat);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}