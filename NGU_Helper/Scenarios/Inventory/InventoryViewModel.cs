using NGU_Helper.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public ICommand UnequipCommand => new DelegateCommand((param) => _model.UnEquip((InventoryItem)param));
    }
}
