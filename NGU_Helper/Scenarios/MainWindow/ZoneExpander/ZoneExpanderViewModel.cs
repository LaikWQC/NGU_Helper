using NGU_Helper.Data;
using NGU_Helper.Scenarios.Tooltip;
using System.Collections.Generic;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.MainWindow.ZoneExpander
{
    public class ZoneExpanderViewModel
    {
        public ZoneExpanderViewModel()
        {
            
        }

        public string Header { get; set; }
        public List<ExItemModel> Items { get; set; }

        public ICommand EquipCommand { get; set; }
    }
}
