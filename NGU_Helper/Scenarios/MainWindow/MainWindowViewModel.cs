using NGU_Helper.Scenarios.ZoneExpander;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.MainWindow
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Zones = new ObservableCollection<ZoneExpanderPresenter>();
        }

        public ObservableCollection<ZoneExpanderPresenter> Zones { get; set; }

        public ICommand OpenItemsListCommand { get; set; }
    }
}
