using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDL.Chips
{
    public struct MonitorChannel
    {
        public bool read = false;
        public bool active = false;
        public bool result = false;
        public bool write = false;
        public MonitorChannel()
        {
            read = false;
            active = false;
            result = false;
            write = false;
        }
    }
    public class ChipBase
    {
        protected string chipName;
        protected List<PinInstance?> readPins;
        protected List<PinInstance?> writePins;
        protected List<int> activePins;
        protected List<int> resultPins;

        protected int readPinCount;
        protected int writePinCount;

        public ChipBase()
        {
            chipName = "Base Chip";
            readPinCount = 0;
            writePinCount = 0;
            readPins = new List<PinInstance?>();
            writePins = new List<PinInstance?>();
            activePins = new List<int>();
            resultPins = new List<int>();
        }
        public ChipBase(int readSize, int writeSize)
        {
            chipName = "Base Chip";
            readPinCount = readSize;
            writePinCount = writeSize;
            readPins = new List<PinInstance?>(readSize);
            writePins = new List<PinInstance?>(writeSize);
            activePins = new List<int>(readSize);
            resultPins = new List<int>(writeSize);
            for (int i = 0; i < readPinCount; i++)
            {
                readPins.Add(null);
                activePins.Add(0);
            }
            for (int i = 0; i < writePinCount; i++)
            {
                writePins.Add(null);
                resultPins.Add(0);
            }
        }
        public virtual void LinkReadPin(PinInstance connectedPin, int id)
        {
            if (id < readPinCount)
            {
                readPins[id] = connectedPin ?? null;
            }
        }
        public virtual void LinkWritePin(PinInstance connectedPin, int id)
        {
            if (id < writePinCount)
            {
                writePins[id] = connectedPin ?? null;
            }
        }

        public void UpdatePins()
        {
            for(int i = 0; i < readPinCount; i++)
            {
                activePins[i] = (readPins[i]?.pin) ?? activePins[i];
            }
        }
        public void UpdateCore()
        {
            // run the chip logic
            ComputeChip();

            // expose the result
            for (int i = 0; i < writePinCount; i++)
            {
                if (writePins[i] != null)
                {
                    writePins[i].pin = resultPins[i];
                }
            }

        }
        protected virtual void ComputeChip() // child must overload this function
        {
            for (int i = 0; i < writePinCount; i++)
            {
                resultPins[i] = activePins[i];
            }
        }

        public MonitorChannel monitoring;
        public List<List<int>> GetPinState()
        {
            List<List<int>> retList = new List<List<int>>();
            List<int> buffList = new List<int>();
            
            foreach (PinInstance? pin in readPins)
            {
                buffList.Add((pin?.pin) ?? -1);
            }
            retList.Add(buffList);

            return retList;
        }
    }
}
