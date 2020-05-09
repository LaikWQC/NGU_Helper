using NGU_Helper.Data;
using NGU_Helper.Repo;
using NGU_Helper.Utils;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NGU_Helper.Scenarios.ItemList.StatCard
{
    public class StatCardPresenter : DialogBase
    {
        private readonly StatCardViewModel _viewModel;
        private readonly StatCardView _view;
        private readonly StatRepo _repo;

        private StatModel _model;
        private bool _isCreate;
        private DelegateCommand _saveCommand;

        public override ContentControl ViewContent => _view;
        public StatCardPresenter(Window owner, StatRepo repo) : base(owner, "Stat")
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

        public void Show(Guid id)
        {
            _isCreate = true;
            _model = new StatModel(id);
            _viewModel.Type = _viewModel.Types.First();
            IsChanged = false;
            ShowDialog();
        }

        public void Show(StatModel model)
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
            _model.BaseValue = _viewModel.Value;
            _model.Type = _viewModel.Type;
            if (_isCreate)
            {
                _model.Id = Guid.NewGuid();
                _repo.CreateStat(_model);
            }
            else
            {
                _repo.EditStat(_model);
            }
            CloseWindow();
            SaveComplete?.Invoke(this, _model);
        }

        public event EventHandler<StatModel> SaveComplete;
    }
}
