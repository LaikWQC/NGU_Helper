using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Model
{
    public class Zone
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<Item> Items {get;set;}
        public int Order { get; set; }
    }
}
