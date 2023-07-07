using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

//Описать класс двоичного дерева поиска. Описать класс одного узла.
//Добавление, поиск элемента в дереве, обход дерева в ширину, в префиксном, постфиксном и инфиксном порядках.
//!!!!!Дописать метод удаления узла
namespace BinarySearchTree
{
    internal class Program
    {
        static void Main()
        {
            BinaryTree<int> tree = new();
            int choose = -1;
            while (choose != 0)
            {
                Console.Clear();
                Console.WriteLine("1 - добавить элемент");
                Console.WriteLine("2 - удалить элемент");
                Console.WriteLine("3 - инфиксный обход");
                Console.WriteLine("4 - постфиксный обход");
                Console.WriteLine("5 - префиксный обход");
                Console.WriteLine("6 - обход в ширину");
                Console.WriteLine("7 - сбалансировать дерево");
                Console.WriteLine("0 - выход");
                if (int.TryParse(Console.ReadLine(), out choose))
                {
                    int ch;
                    switch (choose)
                    {
                        case 1:
                            Console.WriteLine("Введите ключ элемента");
                            if (int.TryParse(Console.ReadLine(), out ch))
                                if (tree.Add(ch))
                                    Console.WriteLine("Успешно");
                                else Console.WriteLine("Не удалось");
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.WriteLine("Введите ключ элемента");
                            if (int.TryParse(Console.ReadLine(), out ch))
                                tree.Delete(ch);
                            Console.WriteLine($"Попытались удалить {ch}");
                            Console.ReadKey();
                            break;
                        case 3:
                            tree.PrintInOrder();
                            Console.ReadKey();
                            break;
                        case 4:
                            tree.PrintPostOrder();
                            Console.ReadKey();
                            break;
                        case 5:
                            tree.PrintPreOrder();
                            Console.ReadKey();
                            break;
                        case 6:
                            tree.Across();
                            Console.ReadKey();
                            break;
                        case 7:
                            tree.Balance();
                            Console.WriteLine("Дерево сбалансировано");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }
    }
}