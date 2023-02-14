using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Core.Contracts;
using Core.Models;
using Newtonsoft.Json;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration config;
        /// <summary>
        /// IOC
        /// </summary>
        /// <param name="_config">App config</param>
        public ProductService(IConfiguration _config)
        {
            config = _config;
        }
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            string dataPath = config.GetSection("DataFiles:Products").Value;
            string data = await File.ReadAllTextAsync(dataPath);
            return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(data);
        }
    }
}
