using NGU_Helper.Repo;
using NGU_Helper.Scenarios.Inventory;
using NGU_Helper.Scenarios.ItemList;
using NGU_Helper.Scenarios.ZoneExpander;
using NGU_Helper.Utils;
using System;

namespace NGU_Helper.Scenarios.MainWindow
{
    public class MainWindowPresenter
    {
        private readonly MainWindowViewModel _viewmodel;
        private readonly MainWindowView _view;

        private readonly InventoryPresenter _inventoryPresenter;
        private readonly Inventory.Inventory _inventory;

        private readonly ZoneRepo _repo;


        public MainWindowPresenter()
        {
            _repo = new ZoneRepo();

            _inventory = new Inventory.Inventory(10);
            _inventoryPresenter = new InventoryPresenter(_inventory);

            _viewmodel = new MainWindowViewModel()
            {
                OpenItemsListCommand = new DelegateCommand(OpenItemsListAction)
            };

            _view = new MainWindowView()
            {
                DataContext = _viewmodel,
                Inventory = { Content = _inventoryPresenter.ViewContent }
            };
        }

        public void StartApplication()
        {
            _refresh();
            _view.Show();
        }

        private void _refresh()
        {
            var zones = _repo.GetAllZones();
            _viewmodel.Zones.Clear();
            foreach(var zone in zones)
            {
                var presenter = new ZoneExpanderPresenter(zone);
                presenter.EquipChanged += (sender,e) => _inventory.Equip(e);
                _viewmodel.Zones.Add(presenter);
            }
        }

        private void OpenItemsListAction()
        {
            var ItemsPresenter = new ItemListPresenter();
            ItemsPresenter.Show(_view);
            _refresh();
        }
    }
}
