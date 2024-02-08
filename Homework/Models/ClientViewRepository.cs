using System;
using System.Linq;
using System.Collections.Generic;

namespace Homework.Models
{
	public  class ClientViewRepository : EFRepository<ClientView>, IClientViewRepository
	{

	}

	public  interface IClientViewRepository : IRepository<ClientView>
	{

	}
}