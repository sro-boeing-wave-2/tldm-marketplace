using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlaceBackend.Models
{
    public class Application
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Developer { get; set; }
        public string AppUrl { get; set; }
        public string LogoUrl { get; set; }
    }
}
