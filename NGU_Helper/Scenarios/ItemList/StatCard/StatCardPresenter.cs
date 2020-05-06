using NGU_Helper.Data;
using NGU_Helper.Repo;
using NGU_Helper.Scenarios.ItemList.Models;
using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NGU_Helper.Scenarios.ItemList.StatCard
{
    public class StatCardPresenter : DialogBase
    {
        private readonly StatCardViewModel _viewModel;
        private readonly StatCardView _view;
        private readonly ItemListRepo _repo;

        private Stat_ItemList _model;
        private bool _isCreate;
        private DelegateCommand _saveCommand;

        public override ContentControl ViewContent => _view;
        public StatCardPresenter(Window owner, ItemListRepo repo) : base(owner, "Stat")
        {
            _repo = repo;

            _saveCommand = new DelegateCommand(SaveAction, CanSave);
            OkCommand = _saveCommand;

            _viewModel = new StatCardViewModel()
            {
                SaveCommand = _saveCommand,
                CloseCommand = new DelegateCommand(() => CloseWindow())
            };
            _viewModel.PropertyChanged += OnPropertyChanged;

            _view = new StatCardView() { DataContext = _viewModel };
        }

        public void Show(Item_ItemList item)
        {
            _isCreate = true;
            _model = new Stat_ItemList();
            _model.ItemId = item.Id;
            _viewModel.Type = _viewModel.Types.First();
            IsChanged = false;
            ShowDialog();
        }

        public void Show(Stat_ItemList model)
        {
            _isCreate = false;
            _model = model;
            _viewModel.Value = _model.Value;
            _viewModel.Type = _model.Type;
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
            if (_isCreate) return _viewModel.Value > 0;
            else return IsChanged;
        }

        private void SaveAction()
        {
            IsChanged = false;
            _model.Value = _viewModel.Value;
            _model.Type = _viewModel.Type;
            if (_isCreate)
            {
                _model.Id = _repo.CreateStat(_model);
            }
            else
            {
                _repo.EditStat(_model);
            }
            CloseWindow();
            SaveComplete?.Invoke(this, _model);
        }

        public event EventHandler<Stat_ItemList> SaveComplete;
    }
}
