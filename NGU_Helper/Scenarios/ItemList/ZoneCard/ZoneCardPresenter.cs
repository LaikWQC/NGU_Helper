using NGU_Helper.Data;
using NGU_Helper.Repo;
using NGU_Helper.Utils;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace NGU_Helper.Scenarios.ItemList.ZoneCard
{
    public class ZoneCardPresenter : DialogBase
    {
        private readonly ZoneCardViewModel _viewModel;
        private readonly ZoneCardView _view;
        private readonly ZoneRepo _repo;

        private ZoneModel _model;
        private bool _isCreate;
        private DelegateCommand _saveCommand;

        public override ContentControl ViewContent => _view;
        public ZoneCardPresenter(Window owner, ZoneRepo repo) : base(owner, "Zone")
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
            _model = new ZoneModel();
            IsChanged = false;
            ShowDialog();
        }

        public void Show(ZoneModel model)
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
                _model.Id = Guid.NewGuid();
                _repo.CreateZone(_model);
            }
            else
            {
                _repo.EditZone(_model);
            }
            CloseWindow();
            SaveComplete?.Invoke(this, _model);
        }

        public event EventHandler<ZoneModel> SaveComplete;
    }
}
