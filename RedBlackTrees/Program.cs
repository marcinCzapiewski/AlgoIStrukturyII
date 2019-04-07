using System;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new RedBlackTree();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(1);
            tree.Insert(9);
            tree.Insert(11);
            tree.Insert(6);

            tree.Root.Print();

            Console.WriteLine();
            Console.ReadLine();
        }
    }
    
    class RedBlackTree
    {
        private Node _root;

        public Node Root => _root;

        public void DisplayTree()
        {
            if (_root == null)
            {
                Console.WriteLine("Drzewo jest puste");
                return;
            }
            
            DisplaySubtrees(_root);
        }

        public void Insert(int value)
        {
            var newNode = new Node(value);

            //jeżeli drzewo jest puste to wstawiamy nowy element jako root
            if (_root == null)
            {
                _root = newNode;
                _root.Colour = Colour.Black;
                return;
            }

            //schodzimy w dół dopóki nie dojdziemy do liścia,
            //X będzie aktualnym liściem, a Y poprzednim
            //kiedy trafimy na null to znaczy, że doszliśmy do liścia
            Node y = null;
            Node x = _root;
            while (x != null)
            {
                y = x;
                if (newNode.Value < x.Value)
                {
                    x = x.LeftChild;
                }
                else
                {
                    x = x.RightChild;
                }
            }

            newNode.Parent = y;

            if (newNode.Value < y.Value)
            {
                y.LeftChild = newNode;
            }
            else
            {
                y.RightChild = newNode;
            }

            //ustawiamy wartowniki i kolor
            newNode.LeftChild = null;
            newNode.RightChild = null;
            newNode.Colour = Colour.Red;

            RepairTreeAfterInsert(newNode);
        }

        //wzorowane na https://inf.ug.edu.pl/~pmp/Z/ASDwyklad/czczWUd.pdf 8 slajd
        private void RepairTreeAfterInsert(Node item)
        {
            while (item != _root && item.Parent.Colour == Colour.Red)
            {
                if (item.Parent == item.Parent.Parent.LeftChild)
                {
                    Node y = item.Parent.Parent.RightChild;

                    if (y != null && y.Colour == Colour.Red)//Przypadek 1: wujek jest czerwony
                    {
                        item.Parent.Colour = Colour.Black;
                        y.Colour = Colour.Black;
                        item.Parent.Parent.Colour = Colour.Red;
                        item = item.Parent.Parent;
                    }

                    else //Przypadek 2
                    {
                        if (item == item.Parent.RightChild) 
                        {
                            item = item.Parent;
                            LeftRotate(item);
                        }
                        
                        //Przypadek 3: zmiana koloru i rotacja
                        item.Parent.Colour = Colour.Black;
                        item.Parent.Parent.Colour = Colour.Red;
                        RightRotate(item.Parent.Parent);
                    }
                }

                else //prawe symetrycznie
                { 
                    Node x = item.Parent.Parent.LeftChild;

                    if (x != null && x.Colour == Colour.Black)//Przypadek 1
                    {
                         item.Parent.Colour = Colour.Red;
                         x.Colour = Colour.Red;
                         item.Parent.Parent.Colour = Colour.Black;
                         item = item.Parent.Parent;
                    }

                    else //Przypadek 2
                    {
                        if (item == item.Parent.LeftChild)
                        {
                            item = item.Parent;
                            RightRotate(item);
                        }

                        //Przypadek 3
                        item.Parent.Colour = Colour.Black;
                        item.Parent.Parent.Colour = Colour.Red;
                        LeftRotate(item.Parent.Parent);
                    }
                }

                _root.Colour = Colour.Black;
            }
        }

        private void RightRotate(Node y)
        {
            //przypisanie prawego poddrzewa X jako lewe poddrzewo Y 
            Node x = y.LeftChild;
            y.LeftChild = x.RightChild;

            if (x.RightChild != null)
            {
                x.RightChild.Parent = y;
            }

            //jeżeli węzeł wokół którego była rotacja jest rootem
            //to musimy ustawić nowego roota
            if (y.Parent == null)
            {
                _root = x;
            }

            //ustawiamy X jako nowe prawe/lewe dziecko dotychczasowego Y
            if (y == y.Parent.RightChild)
            {
                 y.Parent.RightChild = x;
            }
            else if(y == y.Parent.LeftChild)
            {
                 y.Parent.LeftChild = x;
            }
 
            x.RightChild = y;
            y.Parent = x;
        }

        private void LeftRotate(Node x)
        {
            //przypisanie lewego poddrzewa Y jako prawego poddrzewa X 
            Node y = x.RightChild;
            x.RightChild = y.LeftChild;

            if (y.LeftChild != null)
            {
                y.LeftChild.Parent = x;
            }

            //ojciec dotychczasowego X staje się ojcem Y
            y.Parent = x.Parent;

            //jeżeli węzeł wokół którego była rotacja jest rootem
            //to musimy ustawić nowego roota
            if (x.Parent == null)
            {
                _root = y;
            }
            
            //ustawiamy Y jako nowe prawe/lewe dziecko dotychczasowego X
            if (x == x.Parent.LeftChild)
            {
                x.Parent.LeftChild = y;
            }
            else
            {
                x.Parent.RightChild = y;
            }

            y.LeftChild = x;
            x.Parent = y;
        }

        private void DisplaySubtrees(Node current)
        {
            if (current != null)
            {
                DisplaySubtrees(current.LeftChild);
                Console.Write("({0}) ", current.Value);
                DisplaySubtrees(current.RightChild);
            }
        }
    }
}
