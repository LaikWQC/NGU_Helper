using NGU_Helper.Repo;
using NGU_Helper.Scenarios.ItemList;
using NGU_Helper.Scenarios.ZoneExpander;
using NGU_Helper.Utils;

namespace NGU_Helper.Scenarios.MainWindow
{
    public class MainWindowPresenter
    {
        private readonly MainWindowViewModel _viewmodel;
        private readonly MainWindowView _view;

        private readonly InventoryRepo _repo;

        public MainWindowPresenter()
        {
            _repo = new InventoryRepo();

            _viewmodel = new MainWindowViewModel()
            {
                OpenItemsListCommand = new DelegateCommand(OpenItemsListAction)
            };

            _view = new MainWindowView() { DataContext = _viewmodel };
        }

        public void StartApplication()
        {
            _refresh();
            _view.Show();
        }

        private void _refresh()
        {
            var zones = _repo.GetZones();
            _viewmodel.Zones.Clear();
            zones.ForEach(x => _viewmodel.Zones.Add(new ZoneExpanderPresenter(x)));
        }

        private void OpenItemsListAction()
        {
            var ItemsPresenter = new ItemListPresenter();
            ItemsPresenter.Show(_view);
            _refresh();
        }
    }
}
