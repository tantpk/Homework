using System;
using System.Linq;
using System.Collections.Generic;

namespace Homework.Models
{
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(a => a.是否已刪除 == false);
        }
        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
        }
        public bool IsEmailDuplicated(string emailAddress, int id)
        {
            return All().FirstOrDefault(a => a.Email == emailAddress && a.客戶Id == id) != null;
        }
        public 客戶聯絡人 Find(int id)
        {
            return All().FirstOrDefault(a => a.Id == id);
        }
        public IQueryable<客戶聯絡人> FilterByClientTitle(string clientTitle)
        {
            return All().Where(a => clientTitle == string.Empty ? true : a.職稱 == clientTitle);
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}