using System;
using System.IO;

namespace FSTree
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            { 
                Console.WriteLine("Введите символ разделителя");
                var symbol = Console.ReadLine()[0].ToString();
                var treeGetter = new GetterOfSystemTree(symbol);
            
                if(treeGetter.WriteTree())
                    Console.WriteLine($"Дерево успешно записано в файл {treeGetter.ResFileName} на рабочем столе.");
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
