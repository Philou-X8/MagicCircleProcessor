using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDL.Chips
{
    enum StateRegister
    {
        idle = 0,
        read1 = 1,
        read2 = 2,
        write = 3,
        passthrought1 = 4,
        passthrought2 = 5,
        clean = 6,
    }
    enum RegisterCode
    {
        zero = 0,
        open = 1,
        reserved = 11,
        ones = 15
    }

    internal class ChipRegister : ChipBase
    {
        // List<List<int>> registers;
        List<int> register;

        public ChipRegister() : base(2, 1)
        {
            chipName = "Register";
            /*
            registers = new List<List<int>>();
            for (int i = 0; i < 16; i++)
            {
                registers.Add(new List<int>() { 0, 0, 0, 0});
            }
            */
            register = new List<int>();
        }

        protected override void ComputeChip()
        {
            // IN pins:
            // 0 - active mode (read, write, passthrough) (generated from instruction and cycle inner manager)
            // 1 - active read channel
            // 1 - write channel index
            // 2 - read channel 1 index
            // 3 - read channel 2 index 
            // 4 - write channel data

            // OUT pins:
            // 0 - read channel 1
            // 1 - read channel 2

            StateRegister registerMode = (StateRegister)activePins[0];
            int readIndex1 = activePins[1];
            int readIndex2 = activePins[2];
            int writeIndex = activePins[3];
            int writeData = activePins[4];

            StateReadBuffer activeReadBuffer = 0; // set if channel 1 or 2 should be read from 

            switch (registerMode)
            {
                default:
                case StateRegister.idle: // do nothing

                    break;
                case StateRegister.read1: // read 1
                    resultPins[0] = register[readIndex1];
                    break;
                case StateRegister.read2: // read 2
                    resultPins[1] = register[readIndex2];
                    break;
                case StateRegister.write: // write
                    register[writeIndex] = writeData;
                    break;
                case StateRegister.passthrought1: // used for immediate value loading
                    resultPins[0] = readIndex1;
                    break;
                case StateRegister.passthrought2: // used for immediate value loading
                    resultPins[1] = readIndex2;
                    break;
                case StateRegister.clean: // 
                    resultPins[0] = register[(int)RegisterCode.zero];
                    resultPins[1] = register[(int)RegisterCode.zero];
                    break;

            }
        }

    }
}
