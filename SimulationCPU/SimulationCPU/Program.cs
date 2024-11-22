// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using SimulationCPU;
using SimulationCPU.Chips;
using System.Drawing;

namespace SimulationCPU
{
    public class PinInstance
    {
        public int pin;
        public PinInstance() {  pin = 0; }
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
        public PinConnector(ChipModule entry, int inId, ChipModule exit, int outId)
        {
            pin = new PinInstance();
            entryPinIndex = inId;
            exitPinIndex = outId;
            entry.LinkWritePin(pin, inId);
            exit.LinkReadPin(pin, outId);
        }
        public void ConnectChips(ChipModule entry, int inId, ChipModule exit, int outId)
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
        public PinConnectorArray(ChipModule entry, ChipModule exit, int size)
        {
            pins = new List<PinConnector>(size);
            ConnectorCount = size;
            for (int i = 0; i<size; i++)
            {
                pins.Add( new PinConnector(entry, i, exit, i) );
            }
        }
    }

}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("test");


        ChipExample testChip1 = new ChipExample(4, 4); 
        Console.WriteLine("cut 1");
        ChipExample testChip2 = new ChipExample(4, 4);

        PinConnectorArray connector1 = new PinConnectorArray(testChip1, testChip2, 4);

        List<PinInstance> inputPinList = new List<PinInstance>(4) { new PinInstance(), new PinInstance(), new PinInstance(), new PinInstance() };
        List<PinInstance> outputPinList = new List<PinInstance>(4) { new PinInstance(), new PinInstance(), new PinInstance(), new PinInstance() };

        for (int i = 0; i < 4; i++)
        {
            inputPinList[i].pin = i + 1;
            testChip1.LinkReadPin(inputPinList[i], i);
            testChip2.LinkWritePin(outputPinList[i], i);
        }

        Console.Write("Input array: ");
        for (int i = 0; i < 4; i++)
        {
            Console.Write(inputPinList[i] + " ");
        }
        Console.Write("\nResult array: ");
        for (int i = 0; i < 4; i++)
        {
            Console.Write(outputPinList[i] + " ");
        }

        Console.WriteLine("\n--- compute clock cycle ---");
        testChip1.UpdatePins();
        testChip2.UpdatePins();
        testChip1.UpdateCore();
        testChip2.UpdateCore();

        Console.Write("\nResult array: ");
        for (int i = 0; i < 4; i++)
        {
            Console.Write(outputPinList[i] + " ");
        }

        Console.WriteLine("\n--- compute clock cycle ---");
        testChip1.UpdatePins();
        testChip2.UpdatePins();
        testChip1.UpdateCore();
        testChip2.UpdateCore();
        
        Console.Write("\nResult array: ");
        for (int i = 0; i < 4; i++)
        {
            Console.Write(outputPinList[i] + " ");
        }


        /*
        pinConnector secondPin = new pinConnector();
        secondPin.pin = 55;

        ref pinConnector firstPin = ref secondPin; // connect pin
        //firstPin.pin = secondPin.pin;

        Console.WriteLine("initial values: " + firstPin.pin + " , " + secondPin.pin);

        firstPin.pin = 20;

        Console.WriteLine("after change values: " + firstPin.pin + " , " + secondPin.pin);

        secondPin.pin = 33;

        Console.WriteLine("after change values: " + firstPin.pin + " , " + secondPin.pin);
        */

        ChipTest test1 = new ChipTest();
        test1.val = 12;
        TestingStuff testContainer = new TestingStuff();
        testContainer.LinkA(test1);
        testContainer.LinkB(test1);
        testContainer.chip2.val = 13;
        testContainer.printVal();

        TestingStuff testContainer2 = new TestingStuff();
        testContainer2.LinkA(test1);
        testContainer2.ChangeVal();
        testContainer.printVal();
        testContainer2.printVal();

    }
}

class ChipTest
{
    public int val = 0;
}
class TestingStuff
{
    public ChipTest chip1;
    public ChipTest chip2;
    public TestingStuff()
    {
        chip1 = new ChipTest(); 
        chip2 = new ChipTest(); 
    }
    public TestingStuff(ChipTest A, ChipTest B)
    {
        chip1 = A; 
        chip2 = B; 
    }
    public void LinkA(ChipTest passedChip)
    {
        chip1 = passedChip;
    }
    public void LinkB(ChipTest passedChip)
    {
        chip2 = passedChip;
    }
    public void ChangeVal()
    {
        chip1.val = 15;
    }
    public void printVal()
    {
        Console.WriteLine("chip1: " + chip1.val + " , chip 2: " + chip2.val);
    }
}