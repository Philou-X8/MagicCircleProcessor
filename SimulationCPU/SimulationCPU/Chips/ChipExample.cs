using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationCPU.Chips
{
    public class ChipExample : ChipModule
    {
        
        public ChipExample(int readSize, int writeSize) : base(readSize, writeSize) { }

        protected override void ComputeChip()
        {
            resultPins[0] = activePins[1];
            resultPins[1] = activePins[2];
            resultPins[2] = activePins[3];
            resultPins[3] = activePins[0];
        }
    }
}
