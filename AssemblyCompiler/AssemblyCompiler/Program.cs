// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

class LineInterpreter
{
    private string lineRead = "";
    private int lineIndex = 0;

    private string compiledLine = "";

    public string ReadLine(string line)
    {
        lineRead = line.ToLower();
        lineIndex = 0;

        char lastChar = lineRead.ElementAt(lineIndex);
        while (lineIndex < lineRead.Length)
        {
            


            lineIndex++;
        }

        return compiledLine;
    }
    private string ReadWord()
    {
        string ret = "";
        char lastChar = lineRead.ElementAt(lineIndex);
        while(lastChar != ',' && lastChar != ';')
        {
            ret += lastChar;
            lineIndex++;
            lastChar = lineRead.ElementAt(lineIndex);
        }
        return ret;
    }

}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("test");

        LineInterpreter interpreter = new LineInterpreter();
        interpreter.ReadLine("add, $t1, $t6, $a2;");
        return;
    }
}