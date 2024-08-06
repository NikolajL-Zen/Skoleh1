using System;

namespace Conditional_Statements;

class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine(AbsoluteValue(6832)); // Output: 6832
        //Console.WriteLine(AbsoluteValue(-392)); // Output: 392
        //Console.WriteLine(DivisibleBy2Or3(15, 30)); // Output: 450
        //Console.WriteLine(DivisibleBy2Or3(2, 90)); // Output: 180
        //Console.WriteLine(DivisibleBy2Or3(7, 12)); // Output: 19
        //Console.WriteLine(IfConsistsOfUppercaseLetters("xyz")); // Output: false
        //Console.WriteLine(IfConsistsOfUppercaseLetters("DOG")); // Output: true
        //Console.WriteLine(IfConsistsOfUppercaseLetters("L9#")); // Output: false
        //Console.WriteLine(IfGreaterThanThirdOne(new int[] { 2, 7, 12 })); // Output: true
        //Console.WriteLine(IfGreaterThanThirdOne(new int[] { -5, -8, 50 })); // Output: false
        //Console.WriteLine(IfNumberIsEven(721)); // Output: false
        //Console.WriteLine(IfNumberIsEven(1248)); // Output: true
        //Console.WriteLine(IfSortedAscending(new int[] { 3, 7, 10 })); // Output: true
        //Console.WriteLine(IfSortedAscending(new int[] { 74, 62, 99 })); // Output: false
        //Console.WriteLine(PositiveNegativeOrZero(5.24)); // Output: positive
        //Console.WriteLine(PositiveNegativeOrZero(0.0)); // Output: zero
        //Console.WriteLine(PositiveNegativeOrZero(-994.53)); // Output: negative
        //Console.WriteLine(IfYearIsLeap(2016)); // Output: true
        //Console.WriteLine(IfYearIsLeap(2018)); // Output: false
    }



    static int AbsoluteValue(int num)
    {
        if (num < 0)
        {
            return -num;
        }
        else
        {
            return num;
        }
    }


    static int DivisibleBy2Or3(int num1, int num2)
    {
        if ((num1 % 2 == 0 || num1 % 3 == 0) && (num2 % 2 == 0 || num2 % 3 == 0))
        {
            return num1 * num2;
        }
        else
        {
            return num1 + num2;
        }
    }


    static bool IfConsistsOfUppercaseLetters(string str)
    {
        if (str.Length != 3)
        {
            throw new ArgumentException("Input string must be 3 characters long");
        }

        foreach (char c in str)
        {
            if (!char.IsUpper(c))
            {
                return false;
            }
        }

        return true;
    }


    static bool IfGreaterThanThirdOne(int[] arr)
    {
        if (arr.Length != 3)
        {
            throw new ArgumentException("Input array must have 3 elements");
        }

        int product = arr[0] * arr[1];
        int sum = arr[0] + arr[1];

        return product > arr[2] || sum > arr[2];
    }


    static bool IfNumberIsEven(int num)
    {
        if(num % 2 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    static bool IfSortedAscending(int[] arr)
    {
        if (arr[0] <= arr[1] && arr[1] <= arr[2])
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    static string PositiveNegativeOrZero(double num)
    {
        if (num > 0)
        {
            return "positive";
        }
        else if (num < 0)
        {
            return "negative";
        }
        else
        {
            return "zero";
        }
    }


    static bool IfYearIsLeap(int year)
    {
        if (year % 4 == 0)
        {
            if (year % 100 == 0)
            {
                if (year % 400 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
































}

