using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.BinaryTree
{
    public class BinaryTree<T> : IEnumerable<T> 
    {
        public int Count { get; private set; }
        private Node<T> root;

        #region Node 
        private class Node<V>
        {
            public V Value { get; private set; }
            public Node<V> Left { get; set; }
            public Node<V> Right { get; set; }

            public Node(V value)
            {
                this.Value = value;
            }
        }
        #endregion

        #region constructors
        public IComparer<T> comparer;
        
        public BinaryTree()
            : this(Comparer<T>.Default) { }

        public BinaryTree(IComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException();
            this.comparer = comparer;
        }
        #endregion 

        #region add element
        public void AddLeaf(T child)
        {
            Node<T> Child = new Node<T>(child);
            Node<T> parent = null;
            var tempNode = root;

            while (tempNode != null)
            {
                parent = tempNode;
                tempNode = comparer.Compare(child, tempNode.Value) < 0 ? tempNode.Left : tempNode.Right;
            }
            if (parent == null)
                root = Child; 
            else if(comparer.Compare(child, parent.Value) < 0)
                parent.Left = Child;
            else
                parent.Right = Child;
            ++Count;
        }
        #endregion

        #region search element 
        public T Search(T element)
        {
            var node = root;

            while ((node != null) || comparer.Compare(node.Value, element)!=0)
            {
                node = comparer.Compare(node.Value, element) > 0 ?
                    node.Left : node.Right;
            }

            return node.Value;
        }
        #endregion

        #region Iterators
        public IEnumerable<T> InOrder()
        {
            return Inorder(root);
        }

        public IEnumerable<T> PreOrder()
        {
            return Preorder(root);
        }

        public IEnumerable<T> PostOrder()
        {
            return Postorder(root);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return PreOrder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region privat logic
        private IEnumerable<T> Preorder(Node<T> tree)
        {
            if (tree == null)
                yield break;
            yield return tree.Value;
            foreach (var node in Preorder(tree.Left))
                yield return node;
            foreach (var node in Preorder(tree.Right))
                yield return node;
        }

        private IEnumerable<T> Inorder(Node<T> tree)
        {
            if (tree == null) 
                yield break;
            foreach (var node in Inorder(tree.Left))
                yield return node;
            yield return tree.Value;
            foreach (var node in Inorder(tree.Right))
                yield return node;
        }

        private IEnumerable<T> Postorder(Node<T> tree)
        {
            if (tree == null)
                yield break;
            foreach (var node in Postorder(tree.Left))
                yield return node;
            foreach (var node in Postorder(tree.Right))
                yield return node;
            yield return tree.Value;
        }
        #endregion

        #endregion
    }
}
