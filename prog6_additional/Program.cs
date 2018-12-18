using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace prog6_additional
{
    class Program
    {
        static bool isColourCode(string str)
        {
            Regex reg = new Regex(@"^#([0-9a-fA-F]){6}$");
            return reg.IsMatch(str);
        }
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string str = Console.ReadLine();
            if (isColourCode(str))
            {
                Console.WriteLine("Строка является идентификатором цвета");
            }
            else
            {
                Console.WriteLine("Строка не является идентификатором цвета");
            }

        }
    }
}
