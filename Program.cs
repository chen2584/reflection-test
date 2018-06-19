using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

class Program
{

    static IReadOnlyCollection<string> Property1 => new ReadOnlyCollection<string>(new List<string> { "1", "2" });
    static IReadOnlyCollection<string> Property2 => new List<string> { "1", "2" };

    static IReadOnlyList<int> IReadOnlyList = (new List<int> { 1, 2 }).AsReadOnly();

    static int[] numList = new int[] { 1, 2, 3, 4, 5 };

    static void Main(string[] args)
    {
        //Invalid Cast exception
        //((List<string>)Property1).Add("3");

        //Adble to add the value in withou any exception.
        //((List<string>)Property2).Add("3");

        //((List<int>)IReadOnlyList).Add(555);

        foreach (var chen in IReadOnlyList)
        {
            Console.WriteLine(chen);
        }

        Console.WriteLine("---------------------");

        //Ref memory location
        ref int index = ref TestRef(2, numList);
        index = 55555;
        Console.WriteLine("index value = " + numList[1]);

        Console.WriteLine("--- Test Trim ---");

        string trimStr = "   ?Hello, My Name? is Chen?";

        Console.WriteLine(trimStr.TrimStart(new char[] { ',', '?', ' ' }));

        Console.WriteLine("--- Test Convert ---");
        int numStr = Convert.ToInt32("     1");
        Console.WriteLine(numStr);
        string sNumbers = " 1, 2,3, 4,5";
        var numbers = sNumbers.Split(',').Select(Int32.Parse).ToList();
        foreach (var num in numbers)
        {
            Console.Write(num);
        }

        Console.WriteLine("--- Test Sequence Equal ---");
        SequenceEqualEx1();

    }

    static ref int TestRef(int i, int[] num)
    {
        return ref num[1];
    }

    public static void SequenceEqualEx1()
    {
        Pet pet1 = new Pet { Name = "Turbo", Age = 2 };
        Pet pet2 = new Pet { Name = "Peanut", Age = 8 };

        // Create two lists of pets.
        List<Pet> pets1 = new List<Pet> { pet1, pet2 };
        List<Pet> pets2 = new List<Pet> { pet2, pet1 };
        //pets2 = pets2.OrderBy(x => x.Age).ToList();

        bool equal = pets1.SequenceEqual(pets2);

        Console.WriteLine(
            "The lists {0} equal.",
            equal ? "are" : "are not");
    }
}

class RunProgram : Chen
{

    public void Run()
    {
        base.Name = "Worameth Base";
        Name = "Worameth Chiled";

        Console.WriteLine($"{base.Name} {Name}");

    }
}

class Chen
{
    public string Name { get; set; }
}

class Pet
{
    public string Name { get; set; }
    public int Age { get; set; }
}
