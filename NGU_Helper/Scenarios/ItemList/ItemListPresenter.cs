using NGU_Helper.Data;
using NGU_Helper.Repo;
using NGU_Helper.Scenarios.ItemList.ItemCard;
using NGU_Helper.Scenarios.ItemList.StatCard;
using NGU_Helper.Scenarios.ItemList.ZoneCard;
using NGU_Helper.Utils;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.ItemList
{
    public class ItemListPresenter
    {
        private readonly ItemListViewModel _viewmodel;
        private readonly ItemListView _view;

        private readonly ZoneRepo _zoneRepo;
        private readonly ItemRepo _itemRepo;
        private readonly StatRepo _statRepo;

        private DelegateCommand _editZoneCommand;
        private DelegateCommand _deleteZoneCommand;

        private DelegateCommand _addItemCommand;
        private DelegateCommand _editItemCommand;
        private DelegateCommand _deleteItemCommand;

        private DelegateCommand _addStatCommand;
        private DelegateCommand _editStatCommand;
        private DelegateCommand _deleteStatCommand;

        public ItemListPresenter()
        {
            _zoneRepo = new ZoneRepo();
            _itemRepo = new ItemRepo();
            _statRepo = new StatRepo();

            _deleteZoneCommand = new DelegateCommand(DeleteZoneAction, ZoneEnable);
            _editZoneCommand = new DelegateCommand(EditZoneAction, ZoneEnable);

            _addItemCommand = new DelegateCommand(AddItemAction, ZoneEnable);
            _deleteItemCommand = new DelegateCommand(DeleteItemAction, ItemEnable);
            _editItemCommand = new DelegateCommand(EditItemAction, ItemEnable);

            _addStatCommand = new DelegateCommand(AddStatAction, ItemEnable);
            _deleteStatCommand = new DelegateCommand(DeleteStatAction, StatEnable);
            _editStatCommand = new DelegateCommand(EditStatAction, StatEnable);

            _viewmodel = new ItemListViewModel()
            {
                AddZoneCommand = new DelegateCommand(AddZoneAction),
                EditZoneCommand = _editZoneCommand,
                DeleteZoneCommand = _deleteZoneCommand,

                AddItemCommand = _addItemCommand,
                EditItemCommand = _editItemCommand,
                DeleteItemCommand = _deleteItemCommand,

                AddStatCommand = _addStatCommand,
                EditStatCommand = _editStatCommand,
                DeleteStatCommand = _deleteStatCommand
            };
            _viewmodel.PropertyChanged += OnPropertyChanged;

            _view = new ItemListView() { DataContext = _viewmodel };

            _view.ZoneList.MouseDoubleClick += ZoneClicked;
            _view.ItemList.MouseDoubleClick += ItemClicked;
            _view.StatList.MouseDoubleClick += StatClicked;
        }

        public void Show(Window owner)
        {
            _init();

            _view.Owner = owner;
            _view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _view.Width = owner.ActualWidth;
            _view.Height = owner.ActualHeight;
            _view.ShowDialog();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case (nameof(_viewmodel.SelectedZone)):
                    _deleteZoneCommand?.RaiseCanExecuteChanged();
                    _editZoneCommand?.RaiseCanExecuteChanged();
                    _addItemCommand?.RaiseCanExecuteChanged();
                    break;
                case (nameof(_viewmodel.SelectedItem)):
                    _deleteItemCommand?.RaiseCanExecuteChanged();
                    _editItemCommand?.RaiseCanExecuteChanged();
                    _addStatCommand?.RaiseCanExecuteChanged();
                    break;
                case (nameof(_viewmodel.SelectedStat)):
                    _deleteStatCommand?.RaiseCanExecuteChanged();
                    _editStatCommand?.RaiseCanExecuteChanged();
                    break;
                default:
                    break;
            }
        }

        #region Zone
        private void AddZoneAction()
        {
            var presenter = new ZoneCardPresenter(_view, _zoneRepo);
            presenter.SaveComplete += OnZoneAdded;
            presenter.Show();
        }

        private void OnZoneAdded(object sender, ZoneModel e)
        {
            _viewmodel.Zones.Add(e);
            _viewmodel.SelectedZone = e;
        }

        private void EditZoneAction()
        {
            if (_viewmodel.SelectedZone == null) return;

            var presenter = new ZoneCardPresenter(_view, _zoneRepo);
            presenter.SaveComplete += OnZoneEdited;
            presenter.Show(_viewmodel.SelectedZone);
        }

        private void OnZoneEdited(object sender, ZoneModel e)
        {
            var zone = _viewmodel.SelectedZone;
            var item = _viewmodel.SelectedItem;
            var stat = _viewmodel.SelectedStat;
            _viewmodel.Zones.Replace(e);
            _viewmodel.SelectedZone = zone;
            _viewmodel.SelectedItem = item;
            _viewmodel.SelectedStat = stat;
        }

        private void DeleteZoneAction()
        {
            var zone = _viewmodel.SelectedZone;
            if (zone == null) return;

            MessageBoxResult result = MessageBox.Show(_view, $"Delete Zone \"{_viewmodel.SelectedZone.Name}\"?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel) return;

            _zoneRepo.DeleteZone(zone.Id);

            var index = _viewmodel.Zones.IndexOf(zone);
            _viewmodel.Zones.Remove(zone);
            if (_viewmodel.Zones.Count > 0)
            {
                if (_viewmodel.Zones.Count > index)
                    _viewmodel.SelectedZone = _viewmodel.Zones[index];
                else
                    _viewmodel.SelectedZone = _viewmodel.Zones.Last();
            }
        }

        private bool ZoneEnable(object sender)
        {
            return _viewmodel.SelectedZone != null;
        }

        private void ZoneClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;
            if (ZoneEnable(sender))
                EditZoneAction();
        }
        #endregion

        #region Item
        private void AddItemAction()
        {
            if (_viewmodel.SelectedZone == null) return;
            var presenter = new ItemCardPresenter(_view, _itemRepo);
            presenter.SaveComplete += OnItemAdded;
            presenter.Show(_viewmodel.SelectedZone.Id);
        }

        private void OnItemAdded(object sender, ItemModel e)
        {
            _viewmodel.SelectedZone.AddItem(e);
            _viewmodel.SelectedItem = e;
        }

        private void EditItemAction()
        {
            if (_viewmodel.SelectedItem == null) return;

            var presenter = new ItemCardPresenter(_view, _itemRepo);
            presenter.SaveComplete += OnItemEdited;
            presenter.Show(_viewmodel.SelectedItem);
        }

        private void OnItemEdited(object sender, ItemModel e)
        {
            var item = _viewmodel.SelectedItem;
            var stat = _viewmodel.SelectedStat;
            _viewmodel.Items.Replace(e);
            _viewmodel.SelectedItem = item;
            _viewmodel.SelectedStat = stat;
        }

        private void DeleteItemAction()
        {
            var item = _viewmodel.SelectedItem;
            if (item == null) return;

            MessageBoxResult result = MessageBox.Show(_view, $"Delete Item \"{_viewmodel.SelectedItem.Name}\"?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel) return;

            _itemRepo.DeleteItem(item.Id);

            var index = _viewmodel.Items.IndexOf(item);
            _viewmodel.SelectedZone.RemoveItem(item);
            if (_viewmodel.Items.Count > 0)
            {
                if (_viewmodel.Items.Count > index)
                    _viewmodel.SelectedItem = _viewmodel.Items[index];
                else
                    _viewmodel.SelectedItem = _viewmodel.Items.Last();
            }
        }

        private bool ItemEnable(object sender)
        {
            return _viewmodel.SelectedItem != null;
        }

        private void ItemClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;
            if (ItemEnable(sender))
                EditItemAction();
        }
        #endregion

        #region Stat
        private void AddStatAction()
        {
            if (_viewmodel.SelectedItem == null) return;
            var presenter = new StatCardPresenter(_view, _statRepo);
            presenter.SaveComplete += OnStatAdded;
            presenter.Show(_viewmodel.SelectedItem.Id);
        }

        private void OnStatAdded(object sender, StatModel e)
        {            
            _viewmodel.SelectedItem.AddStat(e);
            _viewmodel.SelectedStat = e;
        }

        private void EditStatAction()
        {
            if (_viewmodel.SelectedStat == null) return;

            var presenter = new StatCardPresenter(_view, _statRepo);
            presenter.SaveComplete += OnStatEdited;
            presenter.Show(_viewmodel.SelectedStat);
        }

        private void OnStatEdited(object sender, StatModel e)
        {
            var stat = _viewmodel.SelectedStat;
            _viewmodel.Stats.Replace(e);
            _viewmodel.SelectedStat = stat;
        }

        private void DeleteStatAction()
        {
            var stat = _viewmodel.SelectedStat;
            if (stat == null) return;

            MessageBoxResult result = MessageBox.Show(_view, $"Delete Stat \"{_viewmodel.SelectedStat.Type}\"?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel) return;

            _statRepo.DeleteStat(stat.Id);

            var index = _viewmodel.Stats.IndexOf(stat);
            _viewmodel.SelectedItem.RemoveStat(stat);
            if (_viewmodel.Stats.Count > 0)
            {
                if (_viewmodel.Stats.Count > index)
                    _viewmodel.SelectedStat = _viewmodel.Stats[index];
                else
                    _viewmodel.SelectedStat = _viewmodel.Stats.Last();
            }
        }

        private bool StatEnable(object sender)
        {
            return _viewmodel.SelectedStat != null;
        }

        private void StatClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;
            if (StatEnable(sender))
                EditStatAction();
        }
        #endregion

        private void _init()
        {
            _viewmodel.Zones = _zoneRepo.GetAllZones();
        }
    }
}
