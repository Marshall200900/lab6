using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                Console.Write(hellostr);
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
            InputHandler(out int answer, 1, 3);
            // Ввод количества строк и столбцов в массиве, который мы будем создавать
            int strings = 0, columns = 0;

            switch (answer)
            {
                case 1:
                    InputHandler(out strings, 1, int.MaxValue, "Введите количество строк массива: ", "Ошибка, размер должен быть натуральным числом");
                    InputHandler(out columns, 1, int.MaxValue, "Введите количество столбцов массива: ", "Ошибка, размер должен быть натуральным числом");
                    CreateArrayRandom(ref array, strings, columns); // Рандомный ввод чисел
                    break;
                case 2:
                    InputHandler(out strings, 1, int.MaxValue, "Введите количество строк массива: ", "Ошибка, размер должен быть натуральным числом");
                    InputHandler(out columns, 1, int.MaxValue, "Введите количество столбцов массива: ", "Ошибка, размер должен быть натуральным числом");
                    CreateArrayManual(ref array, strings, columns); // Ручной ввод чисел
                    break;
                case 3:
                    return;
            }

        }

        // Функция для ввода произвольных чисел в массив
        static void CreateArrayRandom(ref double[,] array, int strings, int columns)
        {
            array = new double[strings, columns];
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
        static void CreateArrayManual(ref double[,] array, int strings, int columns)
        {
            array = new double[strings, columns];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    InputHandler(out array[i, j], double.MinValue, double.MaxValue, $"Введите число массива {i+1}-й строки {j+1}-го столбца: ");
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
                    Console.Write(String.Format("{0:0.#}", array[i, j]) + " ");
                }
                Console.WriteLine();
            }
        }
        // Нахождение минимального элемента
        static double FindMinElement(double[,] array)
        {
            double min = array[0, 0];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] < min)
                    {
                        min = array[i, j];
                    }
                }
            }
            return min;
        }

        // Нахождение индекса столбца, в котором содержится минимальное число
        static int FindIndexOfMin(double[,] array)
        {
            int index = 0;
            double min = FindMinElement(array);
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
            return index;

        }

        // Удаление первого столбца массива, в котором находится число, совпадающее с миниимальным элементом
        static int DeleteColumn(ref double[,] array)
        {
            int index = FindIndexOfMin(array);
            double[,] temp = new double[array.GetLength(0), array.GetLength(1)-1]; // Создание временного массива

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
            return index + 1;
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
                            Console.WriteLine("Удален столбец с позицией "+ DeleteColumn(ref array)); // Удаление столбца
                        }
                        break;
                }
            } while (answer != 4);
        }

        // Сортировка массива пузырьком
        static void BubbleSort(ref string[] sentence)
        {
            for (int i = 0; i < sentence.Length; i++)
            {
                for (int k = 0; k < sentence.Length - 1; k++)
                {
                    if (sentence[k].Length < sentence[k + 1].Length)
                    {
                        string temp = sentence[k];
                        sentence[k] = sentence[k + 1];
                        sentence[k + 1] = temp;
                    }
                }
            }
        }

        // Замена слов в строке на перевернутые
        static void ReplaceWords(ref string str, string[] sentence, string[] copyOfSentence)
        {
            
            for (int i = 0; i < sentence.Length; i++)
            {
                string s = copyOfSentence[i];
                Regex reg = new Regex(@"\b" + s + @"\b");
                str = reg.Replace(str, sentence[i]);
                Console.WriteLine(str);

            }


        }

        // Переворачивание слов
        static void ChangeCharOrder(string[] sentence)
        {
            for (int i = 0; i < sentence.Length; i++)
            {
                char[] temp = sentence[i].ToCharArray(); // Преобразование слова в массив символов
                Array.Reverse(temp); // Переворачивание массива символов
                sentence[i] = string.Join("", temp); // Присвоение перевернутого слова
            }
        }

        static void ChangeString(ref string str)
        {
            char[] marks = { ' ', ',', '!', '?', ':', '—', '.'}; // Массив символов
            char[] seperators = { '.', '!', '?' };
            string[] sentences = str.Split(seperators); // Создание массива предложений
            
            //Проход цикла по всем предложениям
            for(int j = 0; j < sentences.Length; j++)
            {
                string[] sentence = sentences[j].Trim().Split(marks, StringSplitOptions.RemoveEmptyEntries); // Создание массива слов
                
                string[] copyOfSentence = new string[sentence.Length]; // Создание второго массива
                Array.Copy(sentence, copyOfSentence, sentence.Length); // Копирование массива слов во второй массив
                
                ChangeCharOrder(sentence);

                BubbleSort(ref sentence);
                //foreach(string e in sentence)
                //{
                //    Console.Write(e + " ");
                //}
                ReplaceWords(ref str, sentence, copyOfSentence);
                
               
            }
            
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

                string[] arrayOfStrings = { "Hello user! Please enter your password.", "Today is a beautiful day. Let's go for a walk" };
                switch (answer)
                {
                    case 1:
                        Console.WriteLine("Выберите один из готовых вариантов или создайте строку самостоятельно");
                        Console.WriteLine("" +
                                $"1. {arrayOfStrings[0]}\n" +
                                $"2. {arrayOfStrings[1]}\n" +
                                "3. Ввести строку самостоятельно\n");
                        InputHandler(out answer, 1, 3, "Ваш ввод: ", "Ошибка, повторите еще раз.");
                        switch (answer)
                        {
                            case 1:
                                str = arrayOfStrings[0];
                                break;
                            case 2:
                                str = arrayOfStrings[1];
                                break;
                            case 3:
                                Console.Write("Введите строку: ");
                                str = Console.ReadLine();
                                break;
                        }
                        break;
                    case 2:
                        if(str.Length == 0)
                        {
                            Console.WriteLine("Сначала создайте строку");
                        }
                        else
                        {
                            Console.WriteLine($"'{str}'");
                        }
                        break;
                    case 3:
                        if (str.Length == 0)
                        {
                            Console.WriteLine("Сначала создайте строку");
                        }
                        else
                        {
                            
                            ChangeString(ref str);
                        }
                        break;
                    case 4:
                        return;
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
