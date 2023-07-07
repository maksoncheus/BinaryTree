using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    internal class TreeNode<N>
        where N : IComparable<N>
    {
        public N Data { get; set; }
        public int BalanceFactor { get { return GetSubTreeHeight(Left) - GetSubTreeHeight(Right); } }
        public TreeNode<N>? Left { get; set; } = null;
        public TreeNode<N>? Right { get; set; } = null;
        public TreeNode(N data)
        {
            Data = data;
        }

        public TreeNode(N data, TreeNode<N> left, TreeNode<N> right) : this(data)
        {
            Left = left;
            Right = right;
        }

        public bool Add(N data)
        {
            if (data == null)
                return false;
            int compareResult = data.CompareTo(Data);
            if (compareResult < 0)
            {
                if (Left == null)
                    Left = new TreeNode<N>(data);
                else
                    return Left.Add(data);
            }
            if (compareResult >= 0)
            {
                if (Right == null)
                    Right = new TreeNode<N>(data);
                else
                    return Right.Add(data);
            }
            return true;
        }

        public int GetSubTreeHeight(TreeNode<N> node) => node == null ? 0 : 1 + Math.Max(GetSubTreeHeight(node.Left), GetSubTreeHeight(node.Right));

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
