using NGU_Helper.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NGU_Helper.Data
{
    public class ZoneCollection: ObservableCollection<ZoneModel>
    {
        public ZoneCollection(IEnumerable<Zone> list)
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
        public void Replace(ZoneModel zone)
        {
            Remove(zone);
            Add(zone);
        }
    }    
}
