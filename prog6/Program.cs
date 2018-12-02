using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog6
{
    class Program
    {
        static void InputHandler(out double result, string hellostr = "Введите число: ", string errstr = "Ошибка, введите число повторно",
            double min = double.MinValue, double max = double.MaxValue)
        {
            bool ok = false;
            do
            {
                ok = double.TryParse(Console.ReadLine(), out result);
                if (!ok || result<min || result>max)
                {
                    ok = false;
                    Console.WriteLine(errstr);
                }
            } while (!ok);
        }
        static void InputHandler(out int result, 
            int min = int.MinValue, int max = int.MaxValue, string hellostr = "Введите число: ", string errstr = "Ошибка, введите число повторно")
        {
            bool ok = false;
            do
            {
                Console.Write(hellostr);
                ok = int.TryParse(Console.ReadLine(), out result);
                if (!ok || result < min || result > max)
                {
                    ok = false;
                    Console.WriteLine(errstr);
                }
            } while (!ok);
        }

        static void ArrayMenu(ref double[,] array)
        {
            int answer = 4;
            do
            {
                Console.WriteLine("" +
                "1. Создание двумерного массива\n" +
                "2. Печать двумерного массива\n" +
                "3. Удалить первый столбец, в котором есть число, совпадающее с минимальным элементом.\n" +
                "4. Назад");
                InputHandler(out answer, 1, 4);

                switch (answer)
                {
                    case 1:
                        break;
                }
            } while (answer != 4);

        }
        static void StringMenu(ref string str)
        {
            int answer = 4;
            do
            {
                Console.WriteLine("" +
                "1. Создание строки\n" +
                "2. Печать строки\n" +
                "3. Перевернуть все слова в предложении и отсортировать слова по убыванию длин слов.\n" +
                "4. Назад");
                InputHandler(out answer, 1, 4);

                switch (answer)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
            } while (answer != 4);
        }

        static void Main(string[] args)
        {
            double[,] array = new double[0, 0];
            string str = "";
            int answer;
            // Главное меню
            do
            {
                Console.WriteLine("" +
                "1. Работа с классом Array\n" +
                "2. Работа с классом string\n" +
                "3. Выход");
                InputHandler(out answer, 1, 3);
                switch (answer)
                {
                    case 1:
                        ArrayMenu(ref array);
                        break;
                    case 2:
                        StringMenu(ref str);
                        break;
                }
            } while (answer != 3);
        }
    }
}
