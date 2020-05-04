using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Scenarios.Inventory
{
    public class Stat_inventory
    {
        private readonly double _baseValue;
        public Stat_inventory(double value)
        {
            _baseValue = value;
        }

        public StatType Type { get; set; }
        public int Level { get; set; }
        public double Value => _baseValue * (1 + Level / 100);
    }
}
