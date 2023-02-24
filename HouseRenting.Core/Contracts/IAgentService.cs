using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Core.Contracts
{
    public interface IAgentService
    {
        bool ExistsById(string userId);
        bool PersonWithPhoneNumberExists(string phoneNumber);
        bool UserHasRents(string userId);
        void Create(string userID, string phoneNumber);
        int GetAgentId(string userId);
    }
}
