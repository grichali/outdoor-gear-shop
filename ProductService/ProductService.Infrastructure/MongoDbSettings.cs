using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure
{
    public class MongoDbSettings
    {
        public string MongoDbConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
