using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NGU_Helper.Scenarios.ItemList.ItemCard
{
    public class ItemCardViewModel : ViewModelBase
    {
        public ItemCardViewModel()
        {
            Types = ItemTypeBl.GetAllTypes();
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public BitmapImage Image => ImageCreator.Create(Url);

        private ItemTypeBl _type;
        public ItemTypeBl Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private int _number;
        public int Number 
        { 
            get => _number;
            set
            { 
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        public List<ItemTypeBl> Types { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ImageSelectComand { get; set; }
    }
}
