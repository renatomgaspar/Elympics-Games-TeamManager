using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elympics_Games.Mobile.Models
{
    public class Team
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required int ElementsNumber { get; set; }
    }
}
