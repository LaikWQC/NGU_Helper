using NGU_Helper.Dao;
using NGU_Helper.Model;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Repo
{
    public class CriteriaRepo
    {
        private readonly CriteriaDao _dao;
        public CriteriaRepo()
        {
            _dao = new CriteriaDao();
        }

        public void CreateDefault()
        {
            var criterias = Enum.GetValues(typeof(StatType)).Cast<StatType>().Select(x => _createDefault(x)).ToList();
            _dao.CreateDefault(criterias);
        }

        public List<Criteria> GetAll()
        {
            return _dao.GetAll();
        }

        private Criteria _createDefault(StatType type)
        {
            return new Criteria()
            {
                Id = Guid.NewGuid(),
                Name = new StatTypeBl(type).ToString(),
                Type = CriteriaType.StatAggregator,
                IsActive = true,
                IsActiveForTooltip = true,
                Order = (int)type,
                StatType = type
            };
        }
    }
}
