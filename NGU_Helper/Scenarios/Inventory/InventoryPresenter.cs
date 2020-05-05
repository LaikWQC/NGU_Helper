using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NGU_Helper.Scenarios.Inventory
{
    public class InventoryPresenter
    {
        private readonly InventoryViewModel _viewmodel;
        private readonly InventoryView _view;

        public InventoryPresenter(Inventory model)
        {
            _viewmodel = new InventoryViewModel(model);

            _view = new InventoryView() { DataContext = _viewmodel };
        }

        public ContentControl ViewContent => _view;
    }
}
