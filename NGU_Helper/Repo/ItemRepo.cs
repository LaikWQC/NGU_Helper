using NGU_Helper.Dao;
using NGU_Helper.Data;
using NGU_Helper.Model;
using System;

namespace NGU_Helper.Repo
{
    public class ItemRepo
    {
        private readonly ItemsDao _dao;
        public ItemRepo()
        {
            _dao = new ItemsDao();
        }

        public void CreateItem(ItemModel model)
        {
            _dao.CreateItem(Convert(model));
        }

        public void EditItem(ItemModel model)
        {
            _dao.EditItem(Convert(model));
        }

        public void DeleteItem(Guid id)
        {
            _dao.DeleteItem(id);
        }

        private Item Convert(ItemModel model)
        {
            return new Item()
            {
                Id = model.Id,
                ZoneId = model.ZoneId,
                Name = model.Name,
                Level = model.Level,
                ItemType = model.Type.Type,
                ImageUrl = model.Url
            };
        }
    }
}
