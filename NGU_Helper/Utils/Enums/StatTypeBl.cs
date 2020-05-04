using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Utils.Enums
{
    public class StatTypeBl
    {
        public StatTypeBl(StatType type)
        {
            Type = type;
        }

        public StatType Type { get; set; }

        public override string ToString()
        {
            return Type.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StatTypeBl)) return false;
            return (Type == ((StatTypeBl)obj).Type);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public static List<StatTypeBl> GetAllTypes()
        {
            return Enum.GetValues(typeof(StatType)).Cast<StatType>().Select(x => new StatTypeBl(x)).ToList();
        }
    }
}
