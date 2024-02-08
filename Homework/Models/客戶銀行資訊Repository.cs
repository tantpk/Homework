using System;
using System.Linq;
using System.Collections.Generic;

namespace Homework.Models
{
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(a=>a.是否已刪除 == false);
        }
        public override void Delete(客戶銀行資訊 entity)
        {
            entity.是否已刪除 = true;
        }
        public 客戶銀行資訊 Find(int id)
        {
            return All().FirstOrDefault(a => a.Id == id);
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}