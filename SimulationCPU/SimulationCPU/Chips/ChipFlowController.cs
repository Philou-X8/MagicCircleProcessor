using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationCPU.Chips
{
    internal class ChipFlowController : ChipModule
    {
        public ChipFlowController() : base(4, 4) {
            flowProgress = 0;
            controlModes = new List<int> { 0, 0, 0, 0 };
        }

        private int flowProgress;
        private List<int> controlModes;

        protected override void ComputeChip()
        {

            // IN pins:
            // 0 - asm instruction

            // OUT pins:
            // 0 - register mode
            // 1 - read buffer mode
            int instruction = activePins[0];

            // choose what the register and ect are doing based on operator and progress of current instruction 
            switch (instruction) // example
            {
                case 0:
                    controlModes = new List<int>() { 0, 1, 0, 0 }; // example
                    break;
                case 1:
                    controlModes = new List<int>() { 1, 2, 0, 0 }; // example
                    break;
            }
        }

        private void SetOutput()
        {
            resultPins = controlModes;
        }
    }
}
