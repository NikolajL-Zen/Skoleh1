namespace Basic
{

    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(CtoF(0));    // "T = 32F"
            //Console.WriteLine(CtoF(100));  // "T = 212F"
            //Console.WriteLine(CtoF(-300)); // "Temperature below absolute zero!"
            //Console.WriteLine(IsResultTheSame(2 + 2, 2 * 2)); // Output: true
            //Console.WriteLine(IsResultTheSame(9 / 3, 16 - 1)); // Output: false
            //Console.WriteLine(ModuloOperations(8, 5, 2)); // Output: 1
            //Console.WriteLine(CubeOf(2)); // Output: 8
            //Console.WriteLine(CubeOf(-5.5)); // Output: -166.375
            
        }

        static int AddAndMultiply(int a, int b, int c)
        {
            return (a + b) * c;
        }


        class TemperatureConverter
        {
            static string CtoF(double celsius)
            {

                if (celsius < -271.15)
                {
                    return "Temperature below absolute zero!";
                }
                else
                {
                    double fahrenheit = (celsius * 9 / 5) + 32;
                    return $"T = {fahrenheit:F0}F";
                }
            }


        }

        class ElementaryOperations
        {
            static (double, double, double, double) Calculate(int a, int b)
            {
                // Check for division by zero
                if (b == 0)
                {
                    throw new DivideByZeroException("Cannot divide by zero!");
                }


                double addition = a + b;

                double subtraction = a - b;

                double multiplication = a * b;

                double division = (double)a / b;

                // Return the results
                return (addition, subtraction, multiplication, division);
            }
        }


        static bool IsResultTheSame(double operation1, double operation2)
        {
            return (operation1 == operation2);
        }


        static int ModuloOperations(int a, int b, int c)
        {
            return ((a % b) % c);
        }


        static double CubeOf(double number)
        {
            return Math.Pow(number, 3);
        }


        static string SwapTwoNumbers(int a, int b)
        {
            int temp = a;
            a = b;
            b = temp;

            return $"Before: a = {a - (a - b) - (b - temp)}, b = {b - (b - temp)} - {temp - a}; After: a = {b}, b = {a}";
        }


    }

}