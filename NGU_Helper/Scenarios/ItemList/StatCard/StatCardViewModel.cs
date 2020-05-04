using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.ItemList.StatCard
{
    public class StatCardViewModel : ViewModelBase
    {
        public StatCardViewModel()
        {
            Types = StatTypeBl.GetAllTypes();
        }

        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        private StatTypeBl _type;
        public StatTypeBl Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public List<StatTypeBl> Types { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }
    }
}
