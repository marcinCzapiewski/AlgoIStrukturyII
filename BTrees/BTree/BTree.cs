namespace BTree
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class BTree<TK> where TK : IComparable<TK>
    {
        public BTree(int degree)
        {
            if (degree < 2)
            {
                throw new ArgumentException("BTree degree must be at least 2", "degree");
            }

            Root = new Node<TK>(degree);
            Degree = degree;
            Height = 1;
        }

        public Node<TK> Root { get; private set; }

        public int Degree { get; private set; }

        public int Height { get; private set; }

        public Entry<TK> Search(TK key)
        {
            return SearchInternal(Root, key);
        }

        public void Insert(TK newKey)
        {
            // there is space in the root node
            if (!Root.HasReachedMaxEntries)
            {
                InsertNonFull(Root, newKey);
                return;
            }

            // need to create new node and have it split
            Node<TK> oldRoot = Root;
            Root = new Node<TK>(Degree);
            Root.Children.Add(oldRoot);
            SplitChild(Root, 0, oldRoot);
            InsertNonFull(Root, newKey);

            Height++;
        }

        public void Delete(TK keyToDelete)
        {
            DeleteInternal(Root, keyToDelete);

            // if root's last entry was moved to a child node, remove it
            if (Root.Entries.Count == 0 && !Root.IsLeaf)
            {
                Root = Root.Children.Single();
                Height--;
            }
        }

        private void DeleteInternal(Node<TK> node, TK keyToDelete)
        {
            int i = node.Entries.TakeWhile(entry => keyToDelete.CompareTo(entry.Key) > 0).Count();

            // found key in node, so delete if from it
            if (i < node.Entries.Count && node.Entries[i].Key.CompareTo(keyToDelete) == 0)
            {
                DeleteKeyFromNode(node, keyToDelete, i);
                return;
            }

            // delete key from subtree
            if (!node.IsLeaf)
            {
                DeleteKeyFromSubtree(node, keyToDelete, i);
            }
        }

        private void DeleteKeyFromSubtree(Node<TK> parentNode, TK keyToDelete, int subtreeIndexInNode)
        {
            Node<TK> childNode = parentNode.Children[subtreeIndexInNode];

            // node has reached min # of entries, and removing any from it will break the btree property,
            // so this block makes sure that the "child" has at least "degree" # of nodes by moving an 
            // entry from a sibling node or merging nodes
            if (childNode.HasReachedMinEntries)
            {
                int leftIndex = subtreeIndexInNode - 1;
                Node<TK> leftSibling = subtreeIndexInNode > 0 ? parentNode.Children[leftIndex] : null;

                int rightIndex = subtreeIndexInNode + 1;
                Node<TK> rightSibling = subtreeIndexInNode < parentNode.Children.Count - 1
                                                ? parentNode.Children[rightIndex]
                                                : null;
                
                if (leftSibling != null && leftSibling.Entries.Count > Degree - 1)
                {
                    // left sibling has a node to spare, so this moves one node from left sibling 
                    // into parent's node and one node from parent into this current node ("child")
                    childNode.Entries.Insert(0, parentNode.Entries[subtreeIndexInNode]);
                    parentNode.Entries[subtreeIndexInNode] = leftSibling.Entries.Last();
                    leftSibling.Entries.RemoveAt(leftSibling.Entries.Count - 1);

                    if (!leftSibling.IsLeaf)
                    {
                        childNode.Children.Insert(0, leftSibling.Children.Last());
                        leftSibling.Children.RemoveAt(leftSibling.Children.Count - 1);
                    }
                }
                else if (rightSibling != null && rightSibling.Entries.Count > Degree - 1)
                {
                    // right sibling has a node to spare, so this moves one node from right sibling 
                    // into parent's node and one node from parent into this current node ("child")
                    childNode.Entries.Add(parentNode.Entries[subtreeIndexInNode]);
                    parentNode.Entries[subtreeIndexInNode] = rightSibling.Entries.First();
                    rightSibling.Entries.RemoveAt(0);

                    if (!rightSibling.IsLeaf)
                    {
                        childNode.Children.Add(rightSibling.Children.First());
                        rightSibling.Children.RemoveAt(0);
                    }
                }
                else
                {
                    // this block merges either left or right sibling into the current node "child"
                    if (leftSibling != null)
                    {
                        childNode.Entries.Insert(0, parentNode.Entries[subtreeIndexInNode]);
                        var oldEntries = childNode.Entries;
                        childNode.Entries = leftSibling.Entries;
                        childNode.Entries.AddRange(oldEntries);
                        if (!leftSibling.IsLeaf)
                        {
                            var oldChildren = childNode.Children;
                            childNode.Children = leftSibling.Children;
                            childNode.Children.AddRange(oldChildren);
                        }

                        parentNode.Children.RemoveAt(leftIndex);
                        parentNode.Entries.RemoveAt(subtreeIndexInNode);
                    }
                    else
                    {
                        Debug.Assert(rightSibling != null, "Node should have at least one sibling");
                        childNode.Entries.Add(parentNode.Entries[subtreeIndexInNode]);
                        childNode.Entries.AddRange(rightSibling.Entries);
                        if (!rightSibling.IsLeaf)
                        {
                            childNode.Children.AddRange(rightSibling.Children);
                        }

                        parentNode.Children.RemoveAt(rightIndex);
                        parentNode.Entries.RemoveAt(subtreeIndexInNode);
                    }
                }
            }

            DeleteInternal(childNode, keyToDelete);
        }
        
        private void DeleteKeyFromNode(Node<TK> node, TK keyToDelete, int keyIndexInNode)
        {
            // if leaf, just remove it from the list of entries (we're guaranteed to have
            // at least "degree" # of entries, to BTree property is maintained
            if (node.IsLeaf)
            {
                node.Entries.RemoveAt(keyIndexInNode);
                return;
            }

            Node<TK> predecessorChild = node.Children[keyIndexInNode];
            if (predecessorChild.Entries.Count >= Degree)
            {
                Entry<TK> predecessor = DeletePredecessor(predecessorChild);
                node.Entries[keyIndexInNode] = predecessor;
            }
            else
            {
                Node<TK> successorChild = node.Children[keyIndexInNode + 1];
                if (successorChild.Entries.Count >= Degree)
                {
                    Entry<TK> successor = DeleteSuccessor(predecessorChild);
                    node.Entries[keyIndexInNode] = successor;
                }
                else
                {
                    predecessorChild.Entries.Add(node.Entries[keyIndexInNode]);
                    predecessorChild.Entries.AddRange(successorChild.Entries);
                    predecessorChild.Children.AddRange(successorChild.Children);

                    node.Entries.RemoveAt(keyIndexInNode);
                    node.Children.RemoveAt(keyIndexInNode + 1);

                    DeleteInternal(predecessorChild, keyToDelete);
                }
            }
        }

        private Entry<TK> DeletePredecessor(Node<TK> node)
        {
            if (node.IsLeaf)
            {
                var result = node.Entries[node.Entries.Count - 1];
                node.Entries.RemoveAt(node.Entries.Count - 1);
                return result;
            }

            return DeletePredecessor(node.Children.Last());
        }
        private Entry<TK> DeleteSuccessor(Node<TK> node)
        {
            if (node.IsLeaf)
            {
                var result = node.Entries[0];
                node.Entries.RemoveAt(0);
                return result;
            }

            return DeletePredecessor(node.Children.First());
        }

        private Entry<TK> SearchInternal(Node<TK> node, TK key)
        {
            int i = node.Entries.TakeWhile(entry => key.CompareTo(entry.Key) > 0).Count();

            if (i < node.Entries.Count && node.Entries[i].Key.CompareTo(key) == 0)
            {
                return node.Entries[i];
            }

            return node.IsLeaf ? null : SearchInternal(node.Children[i], key);
        }

        private void SplitChild(Node<TK> parentNode, int nodeToBeSplitIndex, Node<TK> nodeToBeSplit)
        {
            var newNode = new Node<TK>(Degree);

            parentNode.Entries.Insert(nodeToBeSplitIndex, nodeToBeSplit.Entries[Degree - 1]);
            parentNode.Children.Insert(nodeToBeSplitIndex + 1, newNode);

            newNode.Entries.AddRange(nodeToBeSplit.Entries.GetRange(Degree, Degree - 1));
            
            // remove also Entries[Degree - 1], which is the one to move up to the parent
            nodeToBeSplit.Entries.RemoveRange(Degree - 1, Degree);

            if (!nodeToBeSplit.IsLeaf)
            {
                newNode.Children.AddRange(nodeToBeSplit.Children.GetRange(Degree, Degree));
                nodeToBeSplit.Children.RemoveRange(Degree, Degree);
            }
        }

        private void InsertNonFull(Node<TK> node, TK newKey)
        {
            int positionToInsert = node.Entries.TakeWhile(entry => newKey.CompareTo(entry.Key) >= 0).Count();

            // leaf node
            if (node.IsLeaf)
            {
                node.Entries.Insert(positionToInsert, new Entry<TK>() { Key = newKey});
                return;
            }

            // non-leaf
            Node<TK> child = node.Children[positionToInsert];
            if (child.HasReachedMaxEntries)
            {
                SplitChild(node, positionToInsert, child);
                if (newKey.CompareTo(node.Entries[positionToInsert].Key) > 0)
                {
                    positionToInsert++;
                }
            }

            InsertNonFull(node.Children[positionToInsert], newKey);
        }

        public void Traverse()
        {
            if (Root != null) Root.Traverse();
        }

        public void Print()
        {
            var thisLevel = new List<Node<TK>>
            {
                Root
            };

            while (thisLevel != null && thisLevel.Any())
            {
                var nextLevel = new List<Node<TK>>();
                var output = "";

                foreach (var node in thisLevel)
                {
                    if (node.Children.Any())
                    {
                        nextLevel.AddRange(node.Children);
                    }

                    foreach (var entry in node.Entries)
                    {
                        output += entry.Key + " ";
                    }
                }

                Console.WriteLine(output);

                thisLevel = nextLevel;
            }
        }
    }
}
