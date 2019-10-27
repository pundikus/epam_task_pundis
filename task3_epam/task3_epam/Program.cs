using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Задача: реализовать метод Filter, который должен возвращать входной массив, в котором удалены элементы, которые встречаются более одного раза.
//   * Значения в массиве должны следовать в том порядке, в котором они следуют в оригинальном массиве.
//   * Если в массиве встречаются повторяющиеся значения, то они ВСЕ значения должны быть удалены.
//   * Метод должен выбрасывать исключение ArgumentNullException в случае, если в метод передали null.
//   * В решении разрешается использовать только конструкции языка. Использовать LINQ запрещено.

public class Program
{
    public static int[] Filter(int[] source)
    {
        if(source == null)
        {
            throw new ArgumentNullException("array is empty");
        }

        var sList = new List<int>();

        bool flag;

        if (source.Length == 1)
        {
            return source;
        }

        for (int i = 0; i < source.Length; i++)
        {
            flag = false;
            foreach (var item in source)
            {
                if (item != source[i] && GetCountArray(source)[i] == 1)
                {
                    flag = true;
                    break;
                }         
            }

            if (flag)
            {
                sList.Add(source[i]);
            }
            else
            {
                sList.Remove(source[i]);
            }
        }

        var sNew = sList.ToArray();

        return sNew;
    }

    static int[] GetCountArray(int[] array)
    {       
        int[] countArray = new int[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            countArray[i] = GetCountElements(array, array[i]);      
        }

        return countArray;
    }

    static int GetCountElements(int[] array, int x)
    {
        int count = 0;

        foreach (var item in array)
        {
            if(x == item)
            {
                count++;
            }
        }

        return count;
    }

    // ----- ЗАПРЕЩЕНО ИЗМЕНЯТЬ КОД МЕТОДОВ, КОТОРЫЕ НАХОДЯТСЯ НИЖЕ -----

    public static void Main()
    {
        Console.WriteLine("Task is done when all test cases are correct:");

        int testCaseNumber = 1;

        TestReturnedValues(testCaseNumber++, new int[] { }, new int[] { });
        TestReturnedValues(testCaseNumber++, new int[] { 0 }, new int[] { 0 });
        TestReturnedValues(testCaseNumber++, new int[] { 0, 1 }, new int[] { 0, 1 });
        TestReturnedValues(testCaseNumber++, new int[] { 0, 0 }, new int[] { });
        TestReturnedValues(testCaseNumber++, new int[] { 0, 1, 0 }, new int[] { 1 });
        TestReturnedValues(testCaseNumber++, new int[] { 0, 1, 0, 1 }, new int[] { });
        TestReturnedValues(testCaseNumber++, new int[] { 0, 1, 2, 2, 5, 4, 4, 5, 1, 8, 4, 9, 1, 3, 4, 5, 7 }, new int[] { 0, 8, 9, 3, 7 });
        TestException<ArgumentNullException>(testCaseNumber++, null);

        if (correctTestCaseAmount == testCaseNumber - 1)
        {
            Console.WriteLine("Task is done.");
        }
        else
        {
            Console.WriteLine("TASK IS NOT DONE.");
        }

        Console.ReadLine();
    }

    private static void TestReturnedValues(int testCaseNumber, int[] array, int[] expectedResult)
    {
        try
        {
            var result = Filter(array);

            if (result.SequenceEqual(expectedResult))
            {
                Console.WriteLine(correctCaseTemplate, testCaseNumber);
                correctTestCaseAmount++;
            }
            else
            {
                Console.WriteLine(incorrectCaseTemplate, testCaseNumber);
            }
        }
        catch (Exception)
        {
            Console.WriteLine(correctCaseTemplate, testCaseNumber);
        }
    }

    private static void TestException<T>(int testCaseNumber, int[] array) where T : Exception
    {
        try
        {
            Filter(array);
            Console.WriteLine(incorrectCaseTemplate, testCaseNumber);
        }
        catch (T)
        {
            Console.WriteLine(correctCaseTemplate, testCaseNumber);
            correctTestCaseAmount++;
        }
        catch (Exception)
        {
            Console.WriteLine(incorrectCaseTemplate, testCaseNumber);
        }
    }

    private static string correctCaseTemplate = "Case #{0} is correct.";
    private static string incorrectCaseTemplate = "Case #{0} IS NOT CORRECT";
    private static int correctTestCaseAmount = 0;
}