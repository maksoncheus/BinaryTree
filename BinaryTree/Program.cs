namespace BinaryTree
{
    internal class Program
    {
        class TreeNode<T> where T : IComparable<T>
        {
            public T Data { get; private set; }
            public TreeNode<T>? Left { get; private set; }
            public TreeNode<T>? Right { get; private set; }

            public TreeNode(T data)
            {
                Data = data;
            }

            public TreeNode(T data, TreeNode<T> left, TreeNode<T> right)
            {
                Data = data;
                Left = left;
                Right = right;
            }

            public bool Add(T data)
            {
                if (data == null)
                {
                    return false;
                }

                var compareResult = data.CompareTo(Data);

                if (compareResult < 0)
                {
                    if (Left == null)
                    {
                        Left = new TreeNode<T>(data);
                    }
                    else
                    {
                        return Left.Add(data);
                    }
                }
                else if (compareResult == 0)
                {
                    return false;
                }
                else
                {
                    if (Right == null)
                    {
                        Right = new TreeNode<T>(data);

                    }
                    else
                    {
                        return Right.Add(data);
                    }
                }

                return true;
            }

            public override string? ToString()
            {
                return Data.ToString();
            }
        }
        class BinaryTree<T> where T : IComparable<T>
        {
            public TreeNode<T>? Root { get; private set; }
            public int Count { get; private set; }
            public bool Add(T data)
            {
                if (data == null)
                {
                    return false;
                }

                if (Root == null)
                {
                    Root = new TreeNode<T>(data);
                    Count = 1;
                    return true;
                }

                var addResult = Root.Add(data);
                if (addResult)
                {
                    Count++;
                }

                return addResult;
            }

            public void PreOrder(TreeNode<T> node)
            {
                if (node != null)
                {
                    Console.WriteLine(node.Data);
                    PreOrder(node.Left);
                    PreOrder(node.Right);
                }
            }

            public void Across(TreeNode<T> node)
            {
                var queue = new Queue<TreeNode<T>>();
                queue.Enqueue(node);
                Console.WriteLine(queue.Peek().Data);
                while (queue.Count != 0)
                {
                    if (queue.Peek().Left != null)
                    {
                        Console.WriteLine(queue.Peek().Left.Data);
                        queue.Enqueue(queue.Peek().Left);
                    }
                    if (queue.Peek().Right != null)
                    {
                        Console.WriteLine(queue.Peek().Right.Data);
                        queue.Enqueue(queue.Peek().Right);
                    }
                    queue.Dequeue();
                }
            }
        }
        static void Main(string[] args)
        {
            var tree = new BinaryTree<int>();
            tree.Add(10);
            tree.Add(7);
            tree.Add(15);
            tree.Add(17);
            tree.Add(5);
            tree.Add(3);
            tree.PreOrder(tree.Root);
            Console.WriteLine("/////");
            tree.Across(tree.Root);
        }
    }
}