using NGU_Helper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.ItemList.ZoneCard
{
    public class ZoneCardViewModel : ViewModelBase
    {
        private string _name;
        public string Name 
        { 
            get => _name;
            set 
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _order;
        public int Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }
    }
}
