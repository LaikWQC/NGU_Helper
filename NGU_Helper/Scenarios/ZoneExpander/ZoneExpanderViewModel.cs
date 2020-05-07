using NGU_Helper.Data;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.ZoneExpander
{
    public class ZoneExpanderViewModel
    {
        private readonly ZoneModel _model;
        public ZoneExpanderViewModel(ZoneModel model)
        {
            _model = model;
        }

        public string Header => _model.Name;
        public ItemCollection Items => _model.Items;

        public ICommand EquipCommand { get; set; }
    }
}
