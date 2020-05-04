using NGU_Helper.Data;
using NGU_Helper.Repo;
using NGU_Helper.Scenarios.ItemList.Models;
using NGU_Helper.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NGU_Helper.Scenarios.ItemList.ZoneCard
{
    public class ZoneCardPresenter : DialogBase
    {
        private readonly ZoneCardViewModel _viewModel;
        private readonly ZoneCardView _view;
        private readonly ItemListRepo _repo;

        private Zone_ItemList _model;
        private bool _isCreate;
        private DelegateCommand _saveCommand;

        public override ContentControl ViewContent => _view;
        public ZoneCardPresenter(Window owner, ItemListRepo repo) : base(owner, "Zone")
        {
            _repo = repo;

            _saveCommand = new DelegateCommand(SaveAction, CanSave);
            OkCommand = _saveCommand;

            _viewModel = new ZoneCardViewModel()
            {
                SaveCommand = _saveCommand,
                CloseCommand = new DelegateCommand(() => CloseWindow())
            };
            _viewModel.PropertyChanged += OnPropertyChanged;

            _view = new ZoneCardView() { DataContext = _viewModel };            
        }

        public void Show()
        {
            _isCreate = true;
            _model = new Zone_ItemList();
            IsChanged = false;
            ShowDialog();
        }

        public void Show(Zone_ItemList model)
        {
            _isCreate = false;
            _model = model;
            _viewModel.Name = _model.Name;
            _viewModel.Order = _model.Order;
            IsChanged = false;
            ShowDialog();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsChanged = true;
            _saveCommand?.RaiseCanExecuteChanged();
        }

        private bool CanSave(object obj)
        {
            if (_isCreate) return !string.IsNullOrWhiteSpace(_viewModel.Name);
            else return IsChanged;
        }

        private void SaveAction()
        {
            IsChanged = false;
            _model.Name = _viewModel.Name;
            _model.Order = _viewModel.Order;
            if (_isCreate)
            {
                _model.Id = _repo.CreateZone(_model);
                CloseWindow();
                SaveComplete?.Invoke(this, _model);
            }
            else
            {
                _repo.EditZone(_model);
                CloseWindow();
                EditComplete?.Invoke(this, null);
            }
        }

        public event EventHandler<Zone_ItemList> SaveComplete;
        public event EventHandler EditComplete;
    }
}
