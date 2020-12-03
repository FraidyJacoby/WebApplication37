using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication37.Data;

namespace WebApplication37.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Person> People { get; set; }
    }
}
