
namespace Solution;


public class BST<T> : IBST<T> where T : IComparable<T>
{
    public TreeNode<T>? Root { get; set; }

    public void Insert(T value) => Insert(value, Root);
    public void InsertIterative(T value)
    {
        if(Root == null){
            Root = new TreeNode<T>(value);
            return;
        }

        var curr = Root;

        while(true){ 

            if(curr.Value.CompareTo(value) == 0) return;

            else if(curr.Value.CompareTo(value) > 0) {

                if(curr.Left == null){
                    curr.Left = new TreeNode<T>(value, curr);
                    return;
                }
                
                curr = curr.Left; // left child
            }

            else {

                if(curr.Right == null){
                    curr.Right = new TreeNode<T>(value, curr);
                    return ;
                }
                 
                curr = curr.Right; // right child
            }  
 
        }
    }

    private void Insert(T value, TreeNode<T>? node)
    {
        // case Root is null
        if(Root == null){
            Root = new TreeNode<T>(value);
            return;
        }

        if(node.Value.CompareTo(value) > 0) {
            if(node.Left == null){
               node.Left = new TreeNode<T>(value, node);
               return;
            }
            
            Insert(value, node.Left);
        }

        else if(node.Value.CompareTo(value) < 0){
            if(node.Right == null){
               node.Right = new TreeNode<T>(value, node);
               return;
            }
            
            Insert(value, node.Right);
        } 

        else return;
    }

    #region Traversal

    public string  BreadthFirstTraversal() => BreadthFirstTraversal(Root);
    public string  BreadthFirstTraversal(TreeNode<T>? currNode) 
    {
       var res = "";

       Queue<TreeNode<T>?> q = new Queue<TreeNode<T>?>();
       q.Enqueue(currNode);
       
       while(q.Count > 0)
       {
           var node = q.Dequeue();
           res += node!.Value + " ";
           if(node.Left != null)
              q.Enqueue(node.Left);
           if(node.Right != null)
              q.Enqueue(node.Right);
       }

       return res; 
    }

    public string PreOrderTraversal() => PreOrderTraversal(Root);
    
    //Using Recursion  
    private string PreOrderTraversal_(TreeNode<T>? currNode)
    {
        if(currNode == null) return "";

        return currNode.Value + " " +
               PreOrderTraversal_(currNode.Left) +
               PreOrderTraversal_(currNode.Right);
    }

    //Using Stack
    public string PreOrderTraversal(TreeNode<T>? currNode) 
    {
       var res = "";

       Stack<TreeNode<T>?> s = new Stack<TreeNode<T>?>();
       s.Push(currNode);
       
       while(s.Count > 0)
       {
           var node = s.Pop();
           res += node!.Value + " ";
           
           //Push in reverse order:
           if(node.Right != null) //First Right
              s.Push(node.Right);
           if(node.Left != null)  //then Left
              s.Push(node.Left);
       }

       return res; 
    }

    public string InOrderTraversal() => InOrderTraversal(Root);
    private string InOrderTraversal(TreeNode<T>? currNode)
    {
        if(currNode == null) return "";
        
        return InOrderTraversal(currNode.Left) + 
               currNode.Value + " " +
               InOrderTraversal(currNode.Right); 
    }

    public string PostOrderTraversal() => PostOrderTraversal(Root);
    private string PostOrderTraversal(TreeNode<T>? currNode)
    {
        if(currNode == null) return "";

        return PostOrderTraversal(currNode.Left) + 
               PostOrderTraversal(currNode.Right) +
               currNode.Value + " ";
    }
    #endregion

    public bool Contains(T value) => Search(Root, value) == null ? false : true;

    private TreeNode<T> Search(TreeNode<T>? node, T value)
    {
        if (node == null) // node does not exist
            return null;

        if (value.CompareTo(node.Value) == 0) // value in the node is the same we are looking for
            return node;

        if (value.CompareTo(node.Value) > 0) // value in the node is smaller than the one we are looking for
            return Search(node.Right, value);

        return Search(node.Left, value);
    }

    #region  Remove Delete
    public bool Remove(T value) => DeleteValue(Root, value);//DeleteValue(this, value);

    public bool DeleteValue(T value) 
    { 
        if(Root == null)
            return false;

        //var nodeToDelete = Search(start, value);
        var nodeToDelete = Search(Root, value);

        if(nodeToDelete == null)
            return false;
   
        var parent = nodeToDelete.Parent;
        
        if(nodeToDelete.Left == null || nodeToDelete.Right == null){

            //One
            if(nodeToDelete.Left != null && nodeToDelete.Right == null) {

                if(nodeToDelete.Value.CompareTo(Root.Value)==0){ //nodeToDelete == Root
                    Root = Root.Left;
                    Root.Parent = null;
                    return true;
                }

                if(isLeft(nodeToDelete, parent)) {
                    parent.Left = nodeToDelete.Left;
                }
                else {
                    parent.Right = nodeToDelete.Left;   
                }
                nodeToDelete.Left.Parent = parent;
                return true;

            }

            if(nodeToDelete.Right != null && nodeToDelete.Left == null) {
                
                if(nodeToDelete.Value.CompareTo(Root.Value)==0){ //nodeToDelete == Root
                    Root = Root.Right;
                    Root.Parent = null;
                    return true;
                }

                if(isLeft(nodeToDelete, parent)) {
                    parent.Left = nodeToDelete.Right;
                }
                else {
                    parent.Right = nodeToDelete.Right;
                }
                nodeToDelete.Right.Parent = parent;
                return true;
            }

            //None
            if(nodeToDelete.Value.CompareTo(Root.Value)==0){ //nodeToDelete == Root
                Root = null;
                return true;
            }

            if(isLeft(nodeToDelete, parent)) {
                parent.Left = null;
            }
            else {
                parent.Right = null;
            }

            return true;

        }
/*
        //Find successor node:
        var successor = findInOrderSucc(nodeToDelete);
        nodeToDelete.Value = successor.Value;
        successor.Value = value;

        //Left and Right Recursive step:
        //return DeleteValue(successor, value);
 
        //Left and Right One step: (successor.Left == null -> true)
      
        if(isLeft(successor, successor.Parent)) {
            successor.Parent.Left = successor.Right;
        }
        else { //successor is first node right 
            successor.Parent.Right = successor.Right;   
        }
        if(successor.Right != null)
            successor.Right.Parent = successor.Parent;
        
*/        
        //Find predecessor node:
        var predecessor = findInOrderPredecessor(nodeToDelete);
        nodeToDelete.Value = predecessor.Value;
        predecessor.Value = value;

        //Left and Right One step: (predecessor.Right == null -> true)
 
        if(isLeft(predecessor, predecessor.Parent)) {
            predecessor.Parent.Left = predecessor.Left;
        }
        else { // predecessor is first node left (predecessor.Right == null)
            predecessor.Parent.Right =  predecessor.Left;   
        }
        if(predecessor.Left != null)
           predecessor.Left.Parent = predecessor.Parent;
        
        return true;

        //throw new NotImplementedException();
    }
 
    public bool DeleteValue(TreeNode<T> startNode, T value) 
    { 
        if(startNode == null)
            return false;

        var nodeToDelete = Search(startNode, value);

        if(nodeToDelete == null)
            return false;
   
        var parent = nodeToDelete.Parent;
        
        if(nodeToDelete.Left == null || nodeToDelete.Right == null){

            //One
            if(nodeToDelete.Left != null && nodeToDelete.Right == null) {

                if(nodeToDelete.Value.CompareTo(Root.Value)==0){ //nodeToDelete == Root
                    Root = Root.Left;
                    Root.Parent = null;
                    return true;
                }

                if(isLeft(nodeToDelete, parent)) {
                    parent.Left = nodeToDelete.Left;
                }
                else {
                    parent.Right = nodeToDelete.Left;   
                }
                nodeToDelete.Left.Parent = parent;
                return true;

            }

            if(nodeToDelete.Right != null && nodeToDelete.Left == null) {
                
                if(nodeToDelete.Value.CompareTo(Root.Value)==0){ //nodeToDelete == Root
                    Root = Root.Right;
                    Root.Parent = null;
                    return true;
                }

                if(isLeft(nodeToDelete, parent)) {
                    parent.Left = nodeToDelete.Right;
                }
                else {
                    parent.Right = nodeToDelete.Right;
                }
                nodeToDelete.Right.Parent = parent;
                return true;
            }

            //None
            if(nodeToDelete.Value.CompareTo(Root.Value)==0){ //nodeToDelete == Root
                Root = null;
                return true;
            }

            if(isLeft(nodeToDelete, parent)) {
                parent.Left = null;
            }
            else {
                parent.Right = null;
            }

            return true;

        }
/*
        //Find successor node:
        var successor = findInOrderSucc(nodeToDelete);
        nodeToDelete.Value = successor.Value;
        successor.Value = value;

        //Left and Right Recursive step:
        //return DeleteValue(successor, value);
 
        //Left and Right One step: (successor.Left == null -> true)
      
        if(isLeft(successor, successor.Parent)) {
            successor.Parent.Left = successor.Right;
        }
        else { //successor is first node right 
            successor.Parent.Right = successor.Right;   
        }
        if(successor.Right != null)
            successor.Right.Parent = successor.Parent;
        
*/        
        var successor = findInOrderSucc(nodeToDelete);
        DeleteValue(successor, successor.Value);
        nodeToDelete.Value = successor.Value;

        return true;

        //throw new NotImplementedException();
    }
  
    private bool DeleteValue(BST<T>? tree, T value)
    { 
        if (tree.Root == null) return false;
        // special case if the value to delete is in the root (and the root has 0 children or 1 child)
        if (value.CompareTo(tree.Root.Value) == 0)
        {
            // there are no children:
            if (tree.Root.Left == null && tree.Root.Right == null)
            {
                tree.Root = null;
                return true;
            }
            // there is only left child, the right does not exist
            else if (tree.Root.Left != null && tree.Root.Right == null)
            {
                tree.Root = tree.Root.Left;
                tree.Root.Parent = null;
                return true;
            }
            // there is only right child, the left does not exist
            else if (tree.Root.Left == null && tree.Root.Right != null)
            {
                tree.Root = tree.Root.Right;
                tree.Root.Parent = null;
                return true;
            }
        }

        // all other cases. Find first the tree.Root corresponding to the value we want to delete
        TreeNode<T> nodeToDelete = Search(tree.Root, value);
        // actually perform the deletion
        return delete(nodeToDelete);
    }

    private bool delete(TreeNode<T> nodeToDelete)
    {

        // CASE 1 : LEAF
        if (nodeToDelete.Left == null && nodeToDelete.Right == null)
        {
            var parent = nodeToDelete.Parent;

            if (isLeft(nodeToDelete, parent))
                parent.Left = null;
            else
                parent.Right = null;

            return true;
        }

        // CASE 2 : ONE CHILD
        if (nodeToDelete.Left == null || nodeToDelete.Right == null)
        {
            var parent = nodeToDelete.Parent;

            if (nodeToDelete.Left != null)
            {
                if (isLeft(nodeToDelete, parent))
                    parent.Left = nodeToDelete.Left;
                else
                    parent.Right = nodeToDelete.Left;
                nodeToDelete.Left.Parent = parent;
            }
            else
            {
                if (isLeft(nodeToDelete, parent))
                    parent.Left = nodeToDelete.Right;
                else
                    parent.Right = nodeToDelete.Right;
                nodeToDelete.Right.Parent = parent;
            }

            return true;
        }

        // CASE 3 : TWO CHILDREN

        // find inordersucc == smallest element of right subtree
        var inOrdSucc = findInOrderSucc(nodeToDelete);

        // copy value to nodeToDelete
        nodeToDelete.Value = inOrdSucc.Value;

        // call recursively delete on inordersucc 
        return delete(inOrdSucc);

    }

    // this methods finds the in order successor of the node given as parameter
    private TreeNode<T>? findInOrderSucc(TreeNode<T> node)
    {
        var currNode = node.Right;
        while (currNode != null && currNode.Left != null)
            currNode = currNode.Left;

        return currNode;
    }

    // this methods finds the in order predecessor of the node given as parameter
    private TreeNode<T>? findInOrderPredecessor(TreeNode<T> node)
    {
        var currNode = node.Left;
        while (currNode != null && currNode.Right != null)
            currNode = currNode.Right;

        return currNode;
    }
 
    // this methods checks if the node given as first parameter is the left child 
    // of the node given as second parameter ("parent"). 
    // Remember to do a comparison based on the values of the nodes.
    private bool isLeft(TreeNode<T> node, TreeNode<T> parent)
    {
        return parent.Left != null && parent.Left.Value.CompareTo(node.Value) == 0;
    }


    public List<T>? Traversal(TraversalOrder traversalOrder)
    {
        switch(traversalOrder){
            case TraversalOrder.InOrder:
                return TraversalInOrder(Root, new List<T>());
            case TraversalOrder.PreOrder:
                return TraversalPreOrder(Root, new List<T>());
            case TraversalOrder.PostOrder:
                return TraversalPostOrder(Root, new List<T>());            
            default: return default;
        }
    }

    public List<T>? TraversalGeneric(TraversalOrder traversalOrder) => 
                Traversal(traversalOrder, Root, new List<T>());

    List<T>? Traversal(TraversalOrder traversalOrder,TreeNode<T>? currNode, List<T> res)
    {
        if (currNode == null)
            return default;
        
        if(traversalOrder == TraversalOrder.InOrder || traversalOrder == TraversalOrder.PostOrder)
            Traversal(traversalOrder, currNode.Left, res);
        if(traversalOrder == TraversalOrder.PostOrder)
            Traversal(traversalOrder, currNode.Right, res);
        
        res.Add(currNode.Value);
        
        if(traversalOrder == TraversalOrder.PreOrder)
            Traversal(traversalOrder, currNode.Left, res);
        if(traversalOrder == TraversalOrder.InOrder || traversalOrder == TraversalOrder.PreOrder)
            Traversal(traversalOrder, currNode.Right, res);
        return res;
    }

    List<T>? TraversalInOrder(TreeNode<T>? currNode, List<T> res)
    {
        if (currNode == null)
            return default;

        TraversalInOrder(currNode.Left, res);
        res.Add(currNode.Value);
        TraversalInOrder(currNode.Right, res);
        return res;
    }
   
    List<T>? TraversalPreOrder(TreeNode<T>? currNode, List<T> res)
    {
        if (currNode == null)
            return default;

        res.Add(currNode.Value);
        TraversalPreOrder(currNode.Left, res);
        TraversalPreOrder(currNode.Right, res);
        return res;
    }
  
    List<T>? TraversalPostOrder(TreeNode<T>? currNode, List<T> res)
    {
        if (currNode == null)
            return default;

        TraversalPostOrder(currNode.Left, res);
        TraversalPostOrder(currNode.Right, res);
        res.Add(currNode.Value);
        return res;

    }
    #endregion
}