﻿using NGU_Helper.Data;
using NGU_Helper.Model;
using NGU_Helper.Repo;
using NGU_Helper.Scenarios.ItemList.ItemCard;
using NGU_Helper.Scenarios.ItemList.StatCard;
using NGU_Helper.Scenarios.ItemList.Models;
using NGU_Helper.Scenarios.ItemList.ZoneCard;
using NGU_Helper.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.ItemList
{
    public class ItemListPresenter
    {
        private readonly ItemListViewModel _viewmodel;
        private readonly ItemListView _view;

        private readonly ItemListRepo _repo;

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
            _repo = new ItemListRepo();

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
            var presenter = new ZoneCardPresenter(_view, _repo);
            presenter.SaveComplete += OnZoneAdded;
            presenter.Show();
        }

        private void OnZoneAdded(object sender, Zone_ItemList e)
        {
            _viewmodel.Zones.Add(e);
            SortZones();
            _viewmodel.SelectedZone = e;
        }

        private void EditZoneAction()
        {
            if (_viewmodel.SelectedZone == null) return;

            var presenter = new ZoneCardPresenter(_view, _repo);
            presenter.SaveComplete += OnZoneEdited;
            presenter.Show(_viewmodel.SelectedZone);
        }

        private void OnZoneEdited(object sender, Zone_ItemList e)
        {
            var zone = _viewmodel.SelectedZone;
            var item = _viewmodel.SelectedItem;
            var stat = _viewmodel.SelectedStat;
            SortZones();
            _viewmodel.SelectedZone = zone;
            _viewmodel.SelectedItem = item;
            _viewmodel.SelectedStat = stat;
        }

        private void DeleteZoneAction()
        {
            var zone = _viewmodel.SelectedZone;
            if (zone == null) return;

            MessageBoxResult result = MessageBox.Show(_view, $"Delete Zone \"{_viewmodel.SelectedZone.Name}\"?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.Cancel) return;

            _repo.DeleteZone(zone.Id);

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

        private void SortZones()
        {
            var list = _viewmodel.Zones.OrderBy(x => x.Order).ToList();
            _viewmodel.Zones.Clear();
            list.ForEach(x => _viewmodel.Zones.Add(x));
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
            var presenter = new ItemCardPresenter(_view, _repo);
            presenter.SaveComplete += OnItemAdded;
            presenter.Show(_viewmodel.SelectedZone.Id);
        }

        private void OnItemAdded(object sender, Item_ItemList e)
        {
            _viewmodel.SelectedZone.AddItem(e);
            SortItems();
            _viewmodel.SelectedItem = e;
        }

        private void EditItemAction()
        {
            if (_viewmodel.SelectedItem == null) return;

            var presenter = new ItemCardPresenter(_view, _repo);
            presenter.SaveComplete += OnItemEdited;
            presenter.Show(_viewmodel.SelectedItem);
        }

        private void OnItemEdited(object sender, Item_ItemList e)
        {
            var item = _viewmodel.SelectedItem;
            var stat = _viewmodel.SelectedStat;
            SortItems();
            _viewmodel.SelectedItem = item;
            _viewmodel.SelectedStat = stat;
        }

        private void DeleteItemAction()
        {
            var item = _viewmodel.SelectedItem;
            if (item == null) return;

            MessageBoxResult result = MessageBox.Show(_view, $"Delete Item \"{_viewmodel.SelectedItem.Name}\"?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.Cancel) return;

            _repo.DeleteItem(item.Id);

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

        private void SortItems()
        {
            var list = _viewmodel.Items.OrderBy(x => x.Type.Type).ToList();
            _viewmodel.Items.Clear();
            list.ForEach(x => _viewmodel.Items.Add(x));
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
            var presenter = new StatCardPresenter(_view, _repo);
            presenter.SaveComplete += OnStatAdded;
            presenter.Show(_viewmodel.SelectedItem);
        }

        private void OnStatAdded(object sender, Stat_ItemList e)
        {            
            _viewmodel.SelectedItem.AddStat(e);
            SortStats();
            _viewmodel.SelectedStat = e;
        }

        private void EditStatAction()
        {
            if (_viewmodel.SelectedStat == null) return;

            var presenter = new StatCardPresenter(_view, _repo);
            presenter.SaveComplete += OnStatEdited;
            presenter.Show(_viewmodel.SelectedStat);
        }

        private void OnStatEdited(object sender, Stat_ItemList e)
        {
            var stat = _viewmodel.SelectedStat;
            SortStats();
            _viewmodel.SelectedStat = stat;
        }

        private void DeleteStatAction()
        {
            var stat = _viewmodel.SelectedStat;
            if (stat == null) return;

            MessageBoxResult result = MessageBox.Show(_view, $"Delete Stat \"{_viewmodel.SelectedStat.Type}\"?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.Cancel) return;

            _repo.DeleteStat(stat.Id);

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

        private void SortStats()
        {
            var list = _viewmodel.Stats.OrderBy(x => x.Type.Type).ToList();
            _viewmodel.Stats.Clear();
            list.ForEach(x => _viewmodel.Stats.Add(x));
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
            _viewmodel.Zones = _repo.GetAllZones();
        }
    }
}
