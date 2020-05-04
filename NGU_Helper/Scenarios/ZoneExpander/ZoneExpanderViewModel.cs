using NGU_Helper.Scenarios.Inventory;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Scenarios.ZoneExpander
{
    public class ZoneExpanderViewModel
    {
        private readonly Zone_inventory _model;
        public ZoneExpanderViewModel(Zone_inventory model)
        {
            _model = model;
        }

        public string Header => _model.Name;
        public List<ItemViewModel> Items => _model.Items;
    }
}
