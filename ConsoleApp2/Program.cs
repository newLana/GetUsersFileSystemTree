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
                var symbol = Console.ReadLine();
                var treeGetter = new GetterOfSystemTree(symbol);

                if (treeGetter.WriteAndCompressTree())
                    Console.WriteLine($"Дерево успешно записано в файл {treeGetter.ResFileName} на рабочем столе.");
                if (treeGetter.DecompressTreeFile())
                    Console.WriteLine($"Файл успешно разархивирован.");
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
