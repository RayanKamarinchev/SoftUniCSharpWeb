using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Rents.Models;

namespace HouseRenting.Core.Rents
{
    public interface IRentService
    {
        IEnumerable<RentServiceModel> All();
    }
}
