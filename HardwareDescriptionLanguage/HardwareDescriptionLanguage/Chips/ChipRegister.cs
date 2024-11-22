using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDL.Chips
{
    internal class ChipRegister : ChipBase
    {
        List<List<int>> registers;
        List<int> register;

        public ChipRegister() : base(2, 1)
        {
            registers = new List<List<int>>();
            for (int i = 0; i < 16; i++)
            {
                registers.Add(new List<int>() { 0, 0, 0, 0});
            }

            register = new List<int>();
        }

        protected override void ComputeChip()
        {
            // IN pins:
            // 0 - active mode (read 1, read 2, write, passthrough) (generated from instruction and cycle inner manager)
            // 1 - write channel index
            // 2 - read channel 1 index
            // 3 - read channel 2 index 
            // 4 - write channel data

            // OUT pins:
            // 0 - read channel 1
            // 1 - read channel 2

            int registerMode = activePins[0];
            int readIndex1 = activePins[1];
            int readIndex2 = activePins[2];
            int writeIndex = activePins[3];
            int writeData = activePins[4];

            switch (registerMode)
            {
                default:
                case 0: // do nothing

                    break;
                case 1: // read 1
                    resultPins[0] = register[readIndex1];
                    break;
                case 2: // read 2
                    resultPins[1] = register[readIndex2];
                    break;
                case 3: // write
                    register[writeIndex] = writeData;
                    break;
                case 4: // passthrough channel 1
                    resultPins[0] = readIndex1;
                    break;
                case 5: // passthrough channel 2
                    resultPins[1] = readIndex2;
                    break;
            }
        }

    }
}
