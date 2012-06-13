using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizConnect.Data;

namespace BizConnect.Services
{
    public interface IClientService : IService<Client>
    {
    }

    public class ClientService: Service<Client,BizConnectEntities>, IClientService
    {
        public ClientService(BizConnectEntities dbContext)
            : base(dbContext)
        {
        }
    }
}
