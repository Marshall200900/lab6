using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog6
{
    class Program
    {
        // Статическая функция для ручного ввода вещественных чисел
        static void InputHandler(out double result, double min = double.MinValue, double max = double.MaxValue, string hellostr = "Введите число: ", string errstr = "Ошибка, введите число повторно")
        {
            bool ok = false;
            do
            {
                ok = double.TryParse(Console.ReadLine(), out result);
                if (!ok || result<min || result>max) // Проверка, является ли введенная строка числом, и принадлежит ли заданному интервалу
                {
                    ok = false;
                    Console.WriteLine(errstr);
                }
            } while (!ok);
        }

        // Статическая функция для ручного ввода целых чисел
        static void InputHandler(out int result, 
            int min = int.MinValue, int max = int.MaxValue, string hellostr = "Введите число: ", string errstr = "Ошибка, введите число повторно")
        {
            bool ok = false;
            do
            {
                Console.Write(hellostr);
                ok = int.TryParse(Console.ReadLine(), out result);
                if (!ok || result < min || result > max) // Проверка, является ли введенная строка числом, и принадлежит ли заданному интервалу
                {
                    ok = false;
                    Console.WriteLine(errstr);
                }
            } while (!ok);
        }
        
        //Меню создания двумерного массива
        static void CreateArrayMenu(ref double[,] array)
        {
            Console.WriteLine("" +
                              "1. Создание с помощью датчика случайных чисел\n" +
                              "2. Создание массива вручню\n" +
                              "3. Назад");
            // Ввод нужного пункта меню
            InputHandler(out int answer, 1, 2);
            // Ввод количества строк и столбцов в массиве, который мы будем создавать
            InputHandler(out int size1, 1, int.MaxValue, "Введите количество строк массива: ", "Ошибка, размер должен быть натуральным числом");
            InputHandler(out int size2, 1, int.MaxValue, "Введите количество столбцов массива: ", "Ошибка, размер должен быть натуральным числом");

            switch (answer)
            {
                case 1:
                    CreateArrayRandom(ref array, size1, size2); // Рандомный ввод чисел
                    break;
                case 2:
                    CreateArrayManual(ref array, size1, size2); // Ручной ввод чисел
                    break;
                case 3:
                    return;
            }

        }

        // Функция для ввода произвольных чисел в массив
        static void CreateArrayRandom(ref double[,] array, int size1, int size2)
        {
            array = new double[size1, size2];
            Random rnd = new Random();
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for(int j = 0; j < array.GetLength(1); j++)
                {
                    // Произвольное вещественное число в промежутке от 0.0 до 1.0 умножается 
                    // на произвольное целое число в промежутке от -10 до +10
                    array[i, j] = rnd.NextDouble() * rnd.Next(-10, 11); 
                }
            }
        }

        // Функция для создания двумерного массива вручную
        static void CreateArrayManual(ref double[,] array, int size1, int size2)
        {
            array = new double[size1, size2];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    InputHandler(out array[i, j], double.MinValue, double.MaxValue, $"Введите {i + 1}-е число массива:");
                }
            }
            
        }

        // Функция для печати двумерного массива
        static void PrintArray(double[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    // Форматирование и вывод элемента массива
                    // Каждый элемент имеет не более 3-х знаков после запятой
                    Console.Write(String.Format("{0:0.###}", array[i, j]) + " ");
                }
                Console.WriteLine();
            }
        }

        // Удаление первого столбца массива, в котором находится число, совпадающее с миниимальным элементом
        static void DeleteColumn(ref double[,] array)
        {
            double min = array[0, 0];
            int index = 0;
            double[,] temp = new double[array.GetLength(0), array.GetLength(1)-1]; // Создание временного массива

            // Нахождение минимального элемента
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if(array[i, j] < min)
                    {
                        min = array[i, j];
                    }
                }
            }

            // Нахождение индекса столбца, в котором содержится минимальное число
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == min)
                    {
                        index = j;
                    }
                }
            }

            // Присвоение элементов основного массива временному массиву
            int s = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                s = 0;
                for(int j = 0; j < index; j++)
                {
                    temp[i, s] = array[i, j];
                    s++;
                }
                for(int j = index + 1; j < array.GetLength(1); j++)
                {
                    temp[i, s] = array[i, j];
                    s++;
                }
            }
            array = temp;

        }


        // Меню двумерного массива
        static void ArrayMenu(ref double[,] array)
        {
            int answer = 4;
            do
            {
                Console.WriteLine("\n" +
                "1. Создание двумерного массива\n" +
                "2. Печать двумерного массива\n" +
                "3. Удалить первый столбец, в котором есть число, совпадающее с минимальным элементом.\n" +
                "4. Назад");
                InputHandler(out answer, 1, 4);

                switch (answer)
                {
                    case 1:
                        CreateArrayMenu(ref array); // Переход к меню создания массива
                        break;
                    case 2:
                        if(array.Length == 0)
                        {
                            Console.WriteLine("Массив пуст, выберите первый пункт, чтобы его создать.");
                        }
                        else
                        {
                            PrintArray(array); // Печать массива
                        }
                        break;
                    case 3:
                        if (array.Length == 0)
                        {
                            Console.WriteLine("Массив пуст, выберите первый пункт, чтобы его создать.");
                        }
                        else
                        {
                            DeleteColumn(ref array); // Удаление столбца
                        }
                        break;
                }
            } while (answer != 4);
        }

        // Меню строк
        static void StringMenu(ref string str)
        {
            int answer = 4;
            do
            {
                Console.WriteLine("\n" +
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
            // Начальная инициализация переменных
            double[,] array = new double[0, 0];
            string str = "";
            int answer;

            // Главное меню
            do
            {
                Console.WriteLine("\n" +
                "1. Работа с классом Array\n" +
                "2. Работа с классом string\n" +
                "3. Выход");
                InputHandler(out answer, 1, 3);
                switch (answer)
                {
                    case 1:
                        ArrayMenu(ref array); // Переход к меню массива
                        break;
                    case 2:
                        StringMenu(ref str); // Переход к меню строк
                        break;
                }
            } while (answer != 3);
        }
    }
}
