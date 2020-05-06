using Microsoft.Win32;
using NGU_Helper.Data;
using NGU_Helper.Repo;
using NGU_Helper.Scenarios.ItemList.Models;
using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NGU_Helper.Scenarios.ItemList.ItemCard
{
    public class ItemCardPresenter : DialogBase
    {
        private readonly ItemCardViewModel _viewModel;
        private readonly ItemCardView _view;
        private readonly ItemListRepo _repo;

        private Item_ItemList _model;
        private bool _isCreate;
        private DelegateCommand _saveCommand;

        public override ContentControl ViewContent => _view;
        public ItemCardPresenter(Window owner, ItemListRepo repo) : base(owner, "Item")
        {
            _repo = repo;

            _saveCommand = new DelegateCommand(SaveAction, CanSave);
            OkCommand = _saveCommand;

            _viewModel = new ItemCardViewModel()
            {
                SaveCommand = _saveCommand,
                CloseCommand = new DelegateCommand(() => CloseWindow()),
                ImageSelectComand = new DelegateCommand(SelectImageAction)
            };
            _viewModel.PropertyChanged += OnPropertyChanged;

            _view = new ItemCardView() { DataContext = _viewModel };
        }

        public void Show(Guid id)
        {
            _isCreate = true;
            _model = new Item_ItemList();
            _model.ZoneId = id;
            _viewModel.Type = _viewModel.Types.First();
            IsChanged = false;
            ShowDialog();
        }

        public void Show(Item_ItemList model)
        {
            _isCreate = false;
            _model = model;
            _viewModel.Name = _model.Name;
            _viewModel.Level = _model.Level;
            _viewModel.Type = _model.Type;
            _viewModel.Url = _model.Url;
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
            _model.Level = _viewModel.Level;
            _model.Type = _viewModel.Type;
            _model.Url = _viewModel.Url;
            if (_isCreate)
            {
                _model.Id = _repo.CreateItem(_model);                
            }
            else
            {
                _repo.EditItem(_model);
            }
            CloseWindow();
            SaveComplete?.Invoke(this, _model);
        }

        private void SelectImageAction()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select image";
            op.Filter = "Portable Network Graphic (*.png)|*.png";
            op.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            if (op.ShowDialog() == true)
            {
                _viewModel.Url = op.FileName.Split('\\').Last();
                SetName(_viewModel.Url);
            }
        }

        private void SetName(string url)
        {
            if (string.IsNullOrEmpty(url)) return;
            url = url.Replace("_", " ");
            url = url.Replace(".png", "");

            //если имя начинается с номера и '-', то удалим эту часть
            var splitted = url.Split('-');
            if (splitted.Length > 1 && Int32.TryParse(splitted[0],out _)) 
            {
                url = string.Join("-", splitted.Skip(1).ToArray()).Trim();
            }

            _viewModel.Name = url;
        }

        public event EventHandler<Item_ItemList> SaveComplete;
    }
}
