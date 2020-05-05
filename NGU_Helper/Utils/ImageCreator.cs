using NGU_Helper.Utils.Enums;
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

        public static BitmapImage FreeSlotImage(ItemType type)
        {
            switch (type)
            {
                case ItemType.Weapon:
                    return new BitmapImage(new Uri("/Utils/Images/Empty_Weapon.png", UriKind.Relative));
                case ItemType.Head:
                    return new BitmapImage(new Uri("/Utils/Images/Empty_Head.png", UriKind.Relative));
                case ItemType.Chest:
                    return new BitmapImage(new Uri("/Utils/Images/Empty_Chest.png", UriKind.Relative));
                case ItemType.Legs:
                    return new BitmapImage(new Uri("/Utils/Images/Empty_Legs.png", UriKind.Relative));
                case ItemType.Boots:
                    return new BitmapImage(new Uri("/Utils/Images/Empty_Boots.png", UriKind.Relative));
                case ItemType.Accessory:
                    return new BitmapImage(new Uri("/Utils/Images/Empty_Accessory.png", UriKind.Relative));
                default:
                    return new BitmapImage(new Uri("/Utils/Images/no_image.png", UriKind.Relative));
            }
            
        }

        private static BitmapImage ReturnNoImage()
        {
            return new BitmapImage(new Uri("/Utils/Images/no_image.png", UriKind.Relative));
        }
    }
}
