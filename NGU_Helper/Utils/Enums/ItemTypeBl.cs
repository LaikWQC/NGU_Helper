using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Utils.Enums
{
    public class ItemTypeBl
    {
        public ItemTypeBl(ItemType type)
        {
            Type = type;
        }

        public ItemType Type { get; set; }

        public override string ToString()
        {
            return Type.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ItemTypeBl)) return false;
            return (Type == ((ItemTypeBl)obj).Type);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public static List<ItemTypeBl> GetAllTypes()
        {
            return Enum.GetValues(typeof(ItemType)).Cast<ItemType>().Select(x=>new ItemTypeBl(x)).ToList();
        }
    }
}
