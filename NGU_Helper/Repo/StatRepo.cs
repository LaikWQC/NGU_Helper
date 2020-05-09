using NGU_Helper.Dao;
using NGU_Helper.Data;
using NGU_Helper.Model;
using System;

namespace NGU_Helper.Repo
{
    public class StatRepo
    {
        private readonly StatsDao _dao;
        public StatRepo()
        {
            _dao = new StatsDao();
        }

        public void CreateStat(StatModel model)
        {
            _dao.CreateStat(Convert(model));
        }

        public void EditStat(StatModel model)
        {
            _dao.EditStat(Convert(model));
        }

        public void DeleteStat(Guid id)
        {
            _dao.DeleteStat(id);
        }

        private Stat Convert(StatModel model)
        {
            return new Stat()
            {
                Id = model.Id,
                ItemId = model.ItemId,
                Value = model.BaseValue,
                StatType = model.Type.Type
            };
        }
    }
}
