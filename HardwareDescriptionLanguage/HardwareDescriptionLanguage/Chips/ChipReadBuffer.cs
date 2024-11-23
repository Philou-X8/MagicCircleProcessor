using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDL.Chips
{
    enum StateReadBuffer // what channel from the register should be read
    {
        idle = 0,
        read1 = 1,
        read2 = 2,
        readBoth = 3,
    }

    internal class ChipReadBuffer : ChipBase
    {
        public ChipReadBuffer() : base (3,2) { }
        protected override void ComputeChip()
        {
            // IN pins:
            // 0 - write channel 1
            // 1 - write channel 2
            // 2 - controller
            StateReadBuffer readChannel = (StateReadBuffer)activePins[2];

            switch (readChannel) // choose what buffer to update
            {
                default:
                case StateReadBuffer.idle: // do nothing

                    break;
                case StateReadBuffer.read1: // read 1
                    resultPins[0] = activePins[0];
                    break;
                case StateReadBuffer.read2: // read 2
                    resultPins[1] = activePins[1];
                    break;
                case StateReadBuffer.readBoth:
                    resultPins[0] = activePins[0];
                    resultPins[1] = activePins[1];
                    break;
            }
        }
    }
}
