using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Model
{
    public class Stat
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public StatType StatType { get; set; }
        public double Value { get; set; }
    }
}
