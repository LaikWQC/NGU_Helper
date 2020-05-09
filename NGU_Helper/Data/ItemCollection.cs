using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NGU_Helper.Model;

namespace NGU_Helper.Data
{
    public class ItemCollection : ObservableCollection<ItemModel>
    {
        public ItemCollection() { }

        public ItemCollection(IEnumerable<Item> list)
        {
            foreach (var item in list.OrderBy(x => x.ItemType))
            {
                base.Add(new ItemModel(item));
            }
        }

        /// <summary>
        /// добавляет item с учетом сортировки
        /// </summary>
        public new void Add(ItemModel item)
        {
            //найдем ближайший item, который имеет больший тип (мы должны вставить новый item перед ним)
            var nextItem = this.FirstOrDefault(x => x.Type.Type > item.Type.Type);
            //если такого не окажется, просто вставим (в конец)
            if (nextItem == null)
                base.Add(item);
            //вставим перед ним
            else
            {
                var index = IndexOf(nextItem);
                Insert(index, item);
            }
        }

        /// <summary>
        /// ставит item на свое место с учетом сортировки
        /// (он будет удален из коллекции, так что придется перевыбрать его в случае необходимости)
        /// </summary>
        public void Replace(ItemModel item)
        {
            Remove(item);
            Add(item);
        }
    }
}
