using System;
using System.Linq;
using System.Collections.Generic;

namespace Homework.Models
{
	public  class ClientTypeRepository : EFRepository<ClientType>, IClientTypeRepository
	{

	}

	public  interface IClientTypeRepository : IRepository<ClientType>
	{

	}
}