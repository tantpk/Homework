using System;
using System.Linq;
using System.Collections.Generic;

namespace Homework.Models
{
	public  class sysdiagramRepository : EFRepository<sysdiagram>, IsysdiagramRepository
	{

	}

	public  interface IsysdiagramRepository : IRepository<sysdiagram>
	{

	}
}