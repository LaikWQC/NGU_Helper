using NGU_Helper.Data;
using NGU_Helper.Repo;
using NGU_Helper.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NGU_Helper.Scenarios.MainWindow.ItemMenu
{
    public class ItemMenuPresenter : DialogBase
    {
        private readonly ItemMenuViewModel _viewModel;
        private readonly ItemMenuView _view;
        private readonly ItemRepo _repo;

        private ItemModel _model;
        private DelegateCommand _saveCommand;

        public override ContentControl ViewContent => _view;
        public ItemMenuPresenter(Window owner, ItemRepo repo) :base (owner, "")
        {
            _repo = repo;

            _saveCommand = new DelegateCommand(SaveAction);
            OkCommand = _saveCommand;

            _viewModel = new ItemMenuViewModel()
            {
                SaveCommand = _saveCommand,
                CloseCommand = new DelegateCommand(() => CloseWindow()),
            };

            _view = new ItemMenuView() { DataContext = _viewModel };
        }

        public void Show(ItemModel model)
        {
            _model = model;
            _viewModel.Level = _model.Level;
            _viewModel.Title = $"{(_model.Number)} {_model.Name}";
            ShowDialog();
        }

        private void SaveAction()
        {
            _model.Level = _viewModel.Level;
            _repo.EditItem(_model);
            CloseWindow();
            //SaveComplete?.Invoke(this, null);
        }
    }
}
