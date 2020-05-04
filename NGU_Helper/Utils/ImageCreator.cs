using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NGU_Helper.Utils
{
    public static class ImageCreator
    {
        public static BitmapImage Create(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                return ReturnNoImage();
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", imageName);
                return new BitmapImage(new Uri(path));
            }
            catch
            {
                return ReturnNoImage();
            }
        }

        private static BitmapImage ReturnNoImage()
        {
            return new BitmapImage(new Uri("/Utils/Images/no_image.png", UriKind.Relative));
        }
    }
}
