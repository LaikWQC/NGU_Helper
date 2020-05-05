using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Scenarios.Inventory
{
    public class InventoryViewModel
    {
        private readonly Inventory _model;

        public InventoryViewModel(Inventory model)
        {
            _model = model;
        }

        public List<InventoryItem> Outfit => _model.Outfit;
        public List<InventoryItem> Accessories => _model.Accessories;
    }
}
