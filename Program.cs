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

        Console.WriteLine("\n--- Test Sequence Equal ---");
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
        List<Pet> pets1 = new List<Pet> { pet2, pet1 };
        List<Pet> pets2 = new List<Pet> { pet2, pet1, pet2 };
        //pets2 = pets2.OrderBy(x => x.Age).ToList();

        var values = pets2.Distinct();
        bool equal = pets1.SequenceEqual(pets2);
        //var equal = pets2.Except(pets1).Any();
        foreach (var result in values)
        {
            Console.WriteLine($"{result.Name} {result.Age}");
        }

        Console.WriteLine(
            "The lists {0} equal.",
            equal ? "are" : "are not");

        Console.WriteLine("\n--- Test IEquatable ---");

        List<Position> numList1 = new List<Position>() { new Position { Id = 555, Name = "Chen" } };
        List<Position> numList2 = new List<Position>() { new Position { Id = 555, Name = "Chen" } };
        Console.WriteLine(numList1[0].Id == numList2[0].Id);
        Console.WriteLine(numList1[0].Equals((object)numList2[0]));
        Console.WriteLine($"Hash numList1[0]: {numList1[0].GetHashCode()}, numList2[0]: {numList2[0].GetHashCode()}");
        Console.WriteLine("Equal Object: " + Object.Equals(numList1[0], numList2[0]));
        Console.WriteLine($"Hash numList1[0].Id = {numList1[0].Id.GetHashCode()} numList2[0].Id = {numList2[0].Id.GetHashCode()}");

        int num1 = 555;
        int num2 = 555;
        string name = "Chen";
        Console.WriteLine($"{num1.GetHashCode()} : {num2.GetHashCode()} {name.GetHashCode()}");

        Console.WriteLine("Position Equal: " + numList1[0].Equals(numList2[0]));
        Console.WriteLine("Object Equal: " + Object.Equals(numList1[0], numList2[0]));
        Console.WriteLine("Object Index: " + numList1.IndexOf(new Position { Id = 555, Name = "Chen" }));
        Console.WriteLine("Object Contain: " + numList1.Contains(new Position { Id = 555, Name = "Chen" }));

        //numList1.Remove(new Position { Id = 555, Name = "Chen" });
        Console.WriteLine("Remove numList1. Remain: " + numList1.Count);
        int num = 555;
        Console.WriteLine("Test ValueType: " + numList1[0].Equals(num));

        /*Console.WriteLine("--- Test Struct ---");
        List<PositionStruct> numList1Strcut = new List<PositionStruct>() { new PositionStruct { Id = 555, Name = "Chen" } };
        List<PositionStruct> numList2Struct = new List<PositionStruct>() { new PositionStruct { Id = 555, Name = "Chen" } };
        Console.WriteLine("Position Equal: " + numList1Strcut[0].Equals(numList2Struct[0]));
        Console.WriteLine("Object Equal: " + Object.Equals(numList1Strcut[0], numList2Struct[0]));
        Console.WriteLine("Object Index: " + numList1Strcut.IndexOf(new PositionStruct { Id = 555, Name = "Chen" }));
        Console.WriteLine("Object Contain: " + numList1Strcut.Contains(new PositionStruct { Id = 555, Name = "Chen" }));

        numList1Strcut.Remove(new PositionStruct { Id = 555, Name = "Chen" });
        Console.WriteLine("Remove numList1. Remain: " + numList1Strcut.Count);*/

    }
}

class Position : IEquatable<PositionStruct>, IEquatable<Position> // Will force use equals position
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool Equals(Position otherPosition)
    {
        //Console.WriteLine("Custom Equals");
        return Name.Equals(otherPosition.Name);
    }

    public override bool Equals(object obj)
    {
        Console.WriteLine("Override Equals: " + obj.GetType().IsValueType);
        var position = obj as Position;
        if (position == null)
            return false;
        return Equals(position);
    }

    public bool Equals(PositionStruct other)
    {
        throw new NotImplementedException();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

struct PositionStruct : IEquatable<PositionStruct>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool Equals(PositionStruct otherPosition)
    {
        //Console.WriteLine("Custom Equals");
        return Name.Equals(otherPosition.Name);
    }

    public override bool Equals(object obj)
    {
        Console.WriteLine("Override Equals: " + obj.GetType().IsValueType);
        var position = (PositionStruct)obj;
        return Equals(position);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
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
