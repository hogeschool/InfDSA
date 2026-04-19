
// class for a Tree Node
public class TreeNode<T> where T : IComparable<T>
{
    public T Value { get; set; }

    public TreeNode<T>? Left { get; set; }
    public TreeNode<T>? Right { get; set; }

    public TreeNode<T>? Parent { get; set; }

    public TreeNode(T value, TreeNode<T>? parent = null, 
                    TreeNode<T>? left = null, 
                    TreeNode<T>? right = null)
    {
        Value = value;
        Left = left;
        Right = right;
        Parent = parent;
    }
}
