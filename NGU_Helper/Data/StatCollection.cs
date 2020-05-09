using NGU_Helper.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NGU_Helper.Data
{
    public class StatCollection : ObservableCollection<StatModel>
    {
        public StatCollection() { }

        public StatCollection(IEnumerable<Stat> list, int level)
        {
            foreach (var stat in list.OrderBy(x => x.StatType))
            {
                base.Add(new StatModel(stat, level));
            }
        }

        /// <summary>
        /// добавляет stat с учетом сортировки
        /// </summary>
        public new void Add(StatModel stat)
        {
            //найдем ближайший stat, который имеет больший тип (мы должны вставить новый stat перед ним)
            var nextStat = this.FirstOrDefault(x => x.Type.Type > stat.Type.Type);
            //если такого не окажется, просто вставим (в конец)
            if (nextStat == null)
                base.Add(stat);
            //вставим перед ним
            else
            {
                var index = IndexOf(nextStat);
                Insert(index, stat);
            }
        }

        /// <summary>
        /// ставит stat на свое место с учетом сортировки
        /// (он будет удален из коллекции, так что придется перевыбрать его в случае необходимости)
        /// </summary>
        public void Replace(StatModel stat)
        {
            Remove(stat);
            Add(stat);
        }
    }
}
