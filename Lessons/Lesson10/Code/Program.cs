
//Lesson 10 Trees, BST

using Solution;

var bst = new BST<int>();

bst.Insert(10);
bst.Insert(20);
bst.Insert(15);
bst.Insert(5);
bst.Insert(7);

int[] actual = new int[] {bst.Root.Value, bst.Root.Left.Value, bst.Root.Right.Value,
                            bst.Root.Left.Right.Value, bst.Root.Right.Left.Value};
int[] expected = new int[] { 10, 5, 20, 7, 15 };

bst.Insert(3);
bst.Insert(25);

var bft = bst.BreadthFirstTraversal();
var inOrder = bst.InOrderTraversal();
var preOrder = bst.PreOrderTraversal();
var postOrder = bst.PostOrderTraversal();
System.Console.WriteLine("Before removing 10");
System.Console.WriteLine(bft);
System.Console.WriteLine(inOrder);
System.Console.WriteLine(preOrder);
System.Console.WriteLine(postOrder);
bst.Remove(10);
System.Console.WriteLine("After removing 10 with successor");
bft = bst.BreadthFirstTraversal();
inOrder = bst.InOrderTraversal();
preOrder = bst.PreOrderTraversal();
postOrder = bst.PostOrderTraversal();
System.Console.WriteLine(bft);
System.Console.WriteLine(inOrder);
System.Console.WriteLine(preOrder);
System.Console.WriteLine(postOrder);
bst = new BST<int>();

bst.Insert(10);
bst.Insert(20);
bst.Insert(15);
bst.Insert(5);
bst.Insert(7);
bst.Insert(3);
bst.Insert(25);

bft = bst.BreadthFirstTraversal();
inOrder = bst.InOrderTraversal();
preOrder = bst.PreOrderTraversal();
postOrder = bst.PostOrderTraversal();
System.Console.WriteLine("Before removing 10");
System.Console.WriteLine(bft);
System.Console.WriteLine(inOrder);
System.Console.WriteLine(preOrder);
System.Console.WriteLine(postOrder);
bst.DeleteValue(10);
System.Console.WriteLine("After removing 10 with predecessor");
bft = bst.BreadthFirstTraversal();
inOrder = bst.InOrderTraversal();
preOrder = bst.PreOrderTraversal();
postOrder = bst.PostOrderTraversal();
System.Console.WriteLine(bft);
System.Console.WriteLine(inOrder);
System.Console.WriteLine(preOrder);
System.Console.WriteLine(postOrder);


var bstRoot = new BST<int>();
bstRoot.Insert(10);
bstRoot.Remove(10);

var bstProduct = new BST<Product>();

var products = new Product[] {
                        new Product(35, "Golf", "VW"),
                        new Product(45, "TestaRossa", "Ferrari"),
                        new Product(40, "ID.3", "VW"),
                        new Product(30, "CinqueCento", "Fiat"),
                        new Product(32, "ID.2", "VW"),
                        new Product(36, "Panda", "Fiat"),
                        new Product(23, "Delta", "Lancia"),
                        new Product(63, "Miura", "Lamborghini"),
                        new Product(25, "Punto", "Fiat"),
                        new Product(24, "Ypsilon", "Lancia"),
                        new Product(43, "Flaminia", "Lancia"),
};

for(int i = 0; i < 5; ++i)
    bstProduct.Insert(products[i]); 

var actualProductList = new Product[] {bstProduct.Root.Value, bstProduct.Root.Left.Value, bstProduct.Root.Right.Value,
                            bstProduct.Root.Left.Right.Value, bstProduct.Root.Right.Left.Value};
var expectedProductList = new Product[] { products[0], products[3], products[1], products[4], products[2] };

//Testing

var correct = actual.Length == expected.Length;
for(int i = 0; correct && i < actual.Length; ++i)
    correct = actual[i] == expected[i];
System.Console.WriteLine($"\n actual == expected : {correct}");

correct = actualProductList.Length == expectedProductList.Length;
for(int i = 0; correct && i < actualProductList.Length; ++i)
    correct = actualProductList[i] == expectedProductList[i];
System.Console.WriteLine($"\n actualProductList == expectedProductList : {correct}");


//Example with random values:

int minVal = 1000;
int maxVal = 9000;
var rand = new Random();
var size = rand.Next(9, 20);
var values = new string[size];
for(int i = 0; i < size; ++i) {
    values[i] = "08" + rand.Next(minVal, maxVal);
}

var stringBST = new BST<string>();

values[1] = values[0]; //what happens in this case? 

for(int i = 0; i < size; ++i) {
    stringBST.Insert(values[i]);
}

preOrder= stringBST.PreOrderTraversal();
inOrder = stringBST.InOrderTraversal();
postOrder = stringBST.PostOrderTraversal();

var pre_order = stringBST.TraversalGeneric(TraversalOrder.PreOrder);
var in_order = stringBST.TraversalGeneric(TraversalOrder.InOrder);
var post_order = stringBST.TraversalGeneric(TraversalOrder.PostOrder);

pre_order = stringBST.Traversal(TraversalOrder.PreOrder);
in_order = stringBST.Traversal(TraversalOrder.InOrder);
post_order = stringBST.Traversal(TraversalOrder.PostOrder);

System.Console.WriteLine($"\nBefore removing {values[0]}");
System.Console.WriteLine($"PreOrder: {preOrder}");
System.Console.WriteLine($"InOrder: {inOrder}");
System.Console.WriteLine($"PostOrder: {postOrder}");
var removeTest = stringBST.Remove(values[0]);
var containTest = stringBST.Contains(values[0]);
System.Console.WriteLine($"{values[0]} removed?: {(removeTest? "YES" : "NO")}");
System.Console.WriteLine($"{values[0]} present in BST: {(containTest? "YES" : "NO")}");
System.Console.WriteLine($"{values[2]} present in BST: {(stringBST.Contains(values[2])? "YES" : "NO")}");

preOrder= stringBST.PreOrderTraversal();
inOrder = stringBST.InOrderTraversal();
postOrder = stringBST.PostOrderTraversal();
System.Console.WriteLine($"\nAfter removing {values[0]}");
System.Console.WriteLine($"PreOrder: {preOrder}");
System.Console.WriteLine($"InOrder: {inOrder}");
System.Console.WriteLine($"PostOrder: {postOrder}");


System.Console.WriteLine();


