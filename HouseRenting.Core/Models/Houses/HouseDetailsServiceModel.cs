using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Models.Agents;

namespace HouseRenting.Core.Models.Houses
{
    public class HouseDetailsServiceModel : HouseServiceModel
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public AgentServiceModel Agent { get; set; }
    }
}
