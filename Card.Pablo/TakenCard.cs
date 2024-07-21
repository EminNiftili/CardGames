using Card.Logic.Models;
using Card.Logic.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Pablo
{
    public class TakenCard
    {
        public DeckCard Card { get; set; }

        public List<PabloAction> Actions { get; set; }
    }
}
