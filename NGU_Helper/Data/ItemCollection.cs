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
            if (list == null) return;
            foreach (var item in list.OrderBy(x => x.ItemType).ThenBy(x=>x.Number))
            {
                base.Add(new ItemModel(item));
            }
        }

        /// <summary>
        /// добавляет item с учетом сортировки
        /// </summary>
        public new void Add(ItemModel item)
        {
            //найдем список item'ов такого же типа
            var items = this.Where(x => x.Type.Equals(item.Type)).ToList();
            if(items.Any())
            {
                //найдем ближайший item, который имеет больший номер 
                var nextItem = items.FirstOrDefault(x => x.Number > item.Number);
                //если такого не окажется, вставим в конец этого списка
                if (nextItem == null)
                {
                    var index = IndexOf(items.Last()) + 1;
                    Insert(index, item);
                }                    
                //вставим перед ним
                else
                {
                    var index = IndexOf(nextItem);
                    Insert(index, item);
                }
            }
            //если их не оказалось, вставим перед следующим типом
            else
            {
                //найдем ближайший item, который имеет больший тип 
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
