using NGU_Helper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Data
{
    public class ZoneList: ObservableCollection<ZoneModel>
    {
        public ZoneList(IEnumerable<Zone> list)
        {
            foreach(var zone in list.OrderBy(x=>x.Order))
            {
                Add(new ZoneModel(zone));
            }
        }

        /// <summary>
        /// добавляет zone с учетом сортировки
        /// </summary>
        public new void Add(ZoneModel zone)
        {
            //найдем ближайшую zone, которая имеет больший order (мы должны вставить новую zone перед ней)
            var nextZone = this.FirstOrDefault(x => x.Order > zone.Order);
            //если такой не окажется, просто вставим (в конец)
            if (nextZone == null)
                base.Add(zone);
            //вставим перед ней
            else
            {
                var index = IndexOf(nextZone);
                Insert(index, zone);
            }
        }

        /// <summary>
        /// ставит zone на свое место с учетом сортировки
        /// (она будет удален из коллекции, так что придется перевыбрать ее в случае необходимости)
        /// </summary>
        public void CheckPosition(ZoneModel zone)
        {
            var index = IndexOf(zone);
            var prevZone = index == 0 ? null : this[index - 1];
            var nextZone = index == this.Count + 1 ? null : this[index + 1];
            //если на своем месте, то оставим
            if ((prevZone == null || prevZone.Order <= zone.Order) &&
                (nextZone == null || nextZone.Order >= zone.Order))
                return;
            //иначе удалим и вставим куда надо
            else
            {
                Remove(zone);
                Add(zone);
            }
        }
    }    
}
