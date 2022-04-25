using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Task_One
{
    public class Tree
    {
        public List<int> SimpleFoundInDepth = new List<int>();
        public List<int> SimpleFoundInWidth = new List<int>();

        //20.03.22 
        public Node Root
        {
            get;set;
        }
       
        public Tree()
        {
            Root = null;
        }

        public void Add(int value)
        {
            if (Root == null)
            {
                Root = new Node(value);
                return;
            }

            else
            {
                Node newNode = new Node(value);
                Node currentNode = Root;

                while (currentNode != null)
                {
                    if (currentNode.Value > newNode.Value)
                    {
                        if (currentNode.Left == null)
                        {
                            currentNode.Left = newNode;
                            break;
                        }
                        else
                        {
                            currentNode = currentNode.Left;
                        }
                    }
                    if (currentNode.Value <= newNode.Value)
                    {
                        if (currentNode.Right == null)
                        {
                            currentNode.Right = newNode;
                            break;
                        }
                        else
                        {
                            currentNode = currentNode.Right;
                        }
                    }
                }
            }
        }

        public void Save(Node node)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Node));
            using (FileStream fs = File.Create("tree.xml"))
            {
                serializer.Serialize(fs, node);
            }
        }

        public void VGlubinu(Node node)
        {
            if (node != null)
            {
                Console.Write(node.Value + " ");

                if (IsPeek(node) && IsSimple(node))
                {
                    SimpleFoundInDepth.Add(node.Value);
                }

                VGlubinu(node.Left);
                VGlubinu(node.Right);

            }
        }

        public void VShirinu(Node node)
        {
            Queue<Node> queue = new Queue<Node>();

            if (node != null)
                queue.Enqueue(node);
            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                if (IsPeek(node) && IsSimple(node))
                    SimpleFoundInWidth.Add(node.Value);
                if (node.Left != null) { queue.Enqueue(node.Left); }
                if (node.Right != null) { queue.Enqueue(node.Right); }
                Console.Write(node.Value + " ");
            }

        }

        public bool IsPeek(Node node)
        {
            if (node.Left != null || node.Right != null)
            {
                return true;
            }
            return false;
        }

        public bool IsSimple(Node node)
        {
            if (node.Value == 1)
            {
                return false;
            }
            for (int div = 2; div * div <= node.Value; div++)
            {
                if (node.Value % div == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
