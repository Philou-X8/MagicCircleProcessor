using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDL.Chips
{
    /**
     * Manages the progess of events within a single cycle. 
     * In particular, when to read or write to Registers.
     */ 
    internal class ChipSequencer : ChipBase
    {
        // needs to have an internal clock driven by the simulation's framerate
        // it then generate the CPU's "internal clock" 
        public ChipSequencer() : base(1, 1) { }
    }
}
