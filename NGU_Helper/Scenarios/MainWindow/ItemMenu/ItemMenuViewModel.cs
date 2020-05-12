using NGU_Helper.Utils;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.MainWindow.ItemMenu
{
    public class ItemMenuViewModel : ViewModelBase
    {
        public ItemMenuViewModel()
        {
            
        }

        private int _level;
        public int Level
        {
            get => _level;
            set
            {
                if (value < 0)
                    _level = 0;
                else if (value > 100)
                    _level = 100;
                else
                    _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public string Title { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }
    }
}
