using System;
using System.Linq;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Homework.Models
{
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(a => a.是否已刪除 == false);
        }
        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;
        }
        public 客戶資料 Find(int id)
        {
            return All().FirstOrDefault(a => a.Id == id);
        }
        public IQueryable<客戶資料> FilterByClientType(string clientType)
        {
            return All().Where(a => clientType == string.Empty ? true : a.客戶分類 == clientType);
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}