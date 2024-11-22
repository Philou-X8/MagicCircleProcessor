// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using HDL.Chips;

namespace HDL
{
    public class PinInstance
    {
        public int pin;
        public PinInstance() { pin = 0; }
        public override string ToString()
        {
            return pin.ToString();
        }
    }

    public class PinConnector
    {
        public PinInstance pin; // pin is shared 
        private int entryPinIndex;
        private int exitPinIndex;
        public PinConnector(ChipBase entry, int inId, ChipBase exit, int outId)
        {
            pin = new PinInstance();
            entryPinIndex = inId;
            exitPinIndex = outId;
            entry.LinkWritePin(pin, inId);
            exit.LinkReadPin(pin, outId);
        }
        public void ConnectChips(ChipBase entry, int inId, ChipBase exit, int outId)
        {
            entryPinIndex = inId;
            exitPinIndex = outId;
            entry.LinkWritePin(pin, inId);
            exit.LinkReadPin(pin, outId);
        }
    }

    public class PinConnectorArray
    {
        public List<PinConnector> pins;
        private int ConnectorCount;
        public PinConnectorArray(ChipBase entry, ChipBase exit, int size)
        {
            pins = new List<PinConnector>(size);
            ConnectorCount = size;
            for (int i = 0; i < size; i++)
            {
                pins.Add(new PinConnector(entry, i, exit, i));
            }
        }
    }
}