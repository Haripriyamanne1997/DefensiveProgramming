using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefensivePrograming.Infrastructure.DataModels
{
    public class WithOutUsers
    {
        public int Id { get; set; }  // Primary key

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
