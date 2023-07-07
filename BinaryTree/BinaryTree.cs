using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    internal class BinaryTree<T>
        where T : IComparable<T>
    {
        public TreeNode<T>? Root { get; private set; }

        public int Count { get; private set; }

        public BinaryTree()
        {
        }
        public BinaryTree(T root)
        {
            Root = new TreeNode<T>(root);
            Count = 1;
        }
        public BinaryTree(ICollection<T> collection)
        {
            foreach (T item in collection)
                Add(item);
        }

        public bool Add(T data)
        {
            if (data == null)
                return false;
            if (Root == null)
            {
                Root = new TreeNode<T>(data);
                Count = 1;
                return true;
            }

            bool addResult = Root.Add(data);
            if (addResult)
                Count++;
            return addResult;
        }

        //preorder  data - left - right
        private void PreOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                Console.Write(node.Data.ToString() + " ");
                PreOrder(node.Left);
                PreOrder(node.Right);
            }
        }

        public void PrintPreOrder() { PreOrder(Root); Console.WriteLine(); }
        //postorder left - right - data
        private void PostOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                PostOrder(node.Left);
                PostOrder(node.Right);
                Console.Write(node.Data.ToString() + " ");
            }
        }

        public void PrintPostOrder() { PostOrder(Root); Console.WriteLine(); }
        //inorder left - data - right
        private void InOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                InOrder(node.Left);
                Console.Write(node.Data.ToString() + " ");
                InOrder(node.Right);
            }
        }

        public void PrintInOrder() { InOrder(Root); Console.WriteLine(); }
        //across 
        public void Across()
        {
            Queue<TreeNode<T>> queue = new();
            queue.Enqueue(Root);
            Console.Write(queue.Peek().Data.ToString() + " ");
            while (queue.Count > 0)
            {
                if (queue.Peek().Left != null)
                {
                    Console.Write(queue.Peek().Left.Data + " ");
                    queue.Enqueue(queue.Peek().Left);
                }

                if (queue.Peek().Right != null)
                {
                    Console.Write(queue.Peek().Right.Data + " ");
                    queue.Enqueue(queue.Peek().Right);
                }
                queue.Dequeue();
            }
            Console.WriteLine();
        }

        //search return data == value ? data : () => {data < value ? search(right) : search(left);} 
        public TreeNode<T> Search(T _value)
        {
            TreeNode<T> current = Root;
            while (current != null)
            {
                int compareResult = _value.CompareTo(current.Data);
                current = compareResult switch
                {
                    < 0 => current.Left,
                    > 0 => current.Right,
                    0 => current
                };
            }
            return current;
        }

        //Удаление элемента. Сначала найти узел, который будем удалять, и потом удаляем.
        //1. Если нет правого поддерева. Просто помещаем сюда левого потомка
        //2. Если есть правое поддерево, у которого нет левого поддерева. Просто помещаем сюда правого потомка. Аналогично для левого
        //3. Если есть правое поддерево, у которого есть левое поддерево. Сначала нужно найти минимальный узел и поместить его. Обратить внимание на возможную утечку данных!
        public void Delete(T Value)
        {
            Root = Remover(Root, Value);
        }

        private TreeNode<T> Remover(TreeNode<T> node, T key)
        {
            if (node == null)
                return node;
            int compareResult = key.CompareTo(node.Data);
            if (compareResult < 0)
                node.Left = Remover(node.Left, key);
            else if (compareResult > 0)
            {
                node.Right = Remover(node.Right, key);
            }
            else
            {
                //Нет потомков
                if (node.Left == null && node.Right == null)
                {
                    node = null;
                }
                //Оба потомка
                else if (node.Left != null && node.Right != null)
                {
                    var minNode = FindMin(node.Right);
                    node.Data = minNode.Data;
                    node.Right = Remover(node.Right, minNode.Data);
                }
                else
                {
                    //Один потомок
                    node = node.Left ?? node.Right;
                }
            }

            return node;
        }

        private static TreeNode<T> FindMin(TreeNode<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }
        private static TreeNode<T> RotateLeft(TreeNode<T> aNode) // малое левое вращение
        {
            TreeNode<T> bNode = aNode.Right;
            if (bNode == null) return aNode;
            aNode.Right = bNode.Left;
            bNode.Left = aNode;
            return bNode;
        }
        private static TreeNode<T> RotateRight(TreeNode<T> aNode) // малое правое вращение
        {
            TreeNode<T> bNode = aNode.Left;
            if (bNode == null) return aNode;
            aNode.Left = bNode.Right;
            bNode.Right = aNode;
            return bNode;
        }
        public void Balance() // балансировка дерева
        {
            int countOfOperations = 0;
            Root = Balancer(Root);
            while (countOfOperations != 0)
            {
                countOfOperations = 0;
                Root = Balancer(Root);
            }

            TreeNode<T> Balancer(TreeNode<T> p)
            {
                if (p == null) return null; //Базовый вариант
                if (p.Left != null) p.Left = Balancer(p.Left); //Рекурсивно вызываем для обоих потомков
                if (p.Right != null) p.Right = Balancer(p.Right);
                if (p.BalanceFactor > 1)
                {
                    countOfOperations++;
                    return RotateRight(p);
                }
                if (p.BalanceFactor < -1)
                {
                    countOfOperations++;
                    return RotateLeft(p);
                }
                return p; // балансировка не нужна
            }
        }
    }
}
