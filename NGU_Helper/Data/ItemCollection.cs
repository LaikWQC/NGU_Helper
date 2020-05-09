using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// /// если zone на своей позиции то вернем true
        /// </summary>
        public bool CheckPosition(ItemModel item)
        {
            var index = IndexOf(item);
            var prevItem = index == 0 ? null : this[index - 1];
            var nextItem = index == this.Count - 1 ? null : this[index + 1];
            //если на своем месте, то вернем true
            if ((prevItem == null || prevItem.Type.Type <= item.Type.Type) &&
                (nextItem == null || nextItem.Type.Type >= item.Type.Type))
                return true;
            //иначе удалим и вставим куда надо
            else
            {
                Remove(item);
                Add(item);
                return false;
            }
        }
    }
}
