using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Core.Models.Houses
{
    public class HouseIndexServiceModel : IHouseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }
}
