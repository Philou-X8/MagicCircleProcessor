using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationCPU.Chips
{
    internal class ChipReadBuffer : ChipModule
    {
        public ChipReadBuffer() : base (3,2) { }
        protected override void ComputeChip()
        {
            // IN pins:
            // 0 - write channel 1
            // 1 - write channel 2
            // 2 - controller

            switch (activePins[2]) // choose what buffer to update
            {
                default:
                case 0: // do nothing

                    break;
                case 1: // read 1
                    resultPins[0] = activePins[0];
                    break;
                case 2: // read 2
                    resultPins[1] = activePins[1];
                    break;
                case 3:
                    resultPins[0] = activePins[0];
                    resultPins[1] = activePins[1];
                    break;
            }
        }
    }
}
