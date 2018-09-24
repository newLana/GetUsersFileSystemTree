using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите символ разделителя");
            var symbol = Console.ReadLine()[0].ToString();
            var treeGetter = new GetterOfSystemTree(symbol);
            
            if(treeGetter.WriteTree())
                Console.WriteLine($"Дерево успешно записано в файл {treeGetter.ResFileName} на рабочем столе.");

            Console.ReadKey();
        }
    }
}
