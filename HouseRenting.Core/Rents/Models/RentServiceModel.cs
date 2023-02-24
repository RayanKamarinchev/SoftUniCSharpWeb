using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Core.Rents.Models
{
    public class RentServiceModel
    {
        public string HouseTitle { get; set; }
        public string HouseImageUrl { get; set; }
        public string AgentFullName { get; set; }
        public string AgentEmail { get; set; }
        public string RenterFullName { get; set; }
        public string RenterEmail { get; set; }
    }
}
