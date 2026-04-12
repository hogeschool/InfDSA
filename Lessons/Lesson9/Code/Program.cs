
using Solution;


var HTsize = 10;
//Not present key:
var table_Trial = new HashTable<int?, string>(HTsize);
for(int i = 1; i < HTsize - 1; ++i)
{
    table_Trial.Add(i, i.ToString());
}

var test_Delete_Not_Present = table_Trial.Delete(0);
var test_Find_Not_Present = table_Trial.Find(0) == null;

//No duplications:
var table_ = new HashTable<int?, string>(HTsize);
for(int i = 0; i < HTsize - 1; ++i)
{
    table_.Add(i, i.ToString());
}
table_.Add(10, "10");
table_.Delete(0);
var test = table_.Add(10, "10");

var actualTable = new HashTable<int?, string>(HTsize);
var randomNum = new Random();
var n_0 = randomNum.Next(1, 7);
var n_1 = HTsize + n_0;
var n_2 = 2*HTsize + n_0;

actualTable.Add(n_0, $"Hello {n_0}");
actualTable.Add(n_1, $"Hello {n_1}");
actualTable.Add(n_2, $"Hello {n_2}");

actualTable.Delete(n_1);
test_Find_Not_Present = actualTable.Find(n_1) == null;

actualTable.Add(n_2, $"Hello {n_2}");
actualTable.Add(n_1, $"NEW Insertion Hello {n_1}");


int n = 10;
for(int i = 0; i < n/2; ++i){
    actualTable.Add(i, $"Hello {i}");
}

//Wrap around (if as begin) Collisions
for(int i = 0; i < n/2; ++i){
    actualTable.Add(i*n + i, $"Hello {i}");
}

for(int i = n/2; i < n - 3; ++i){
    actualTable.Add(i, $"Hello {i}");
}

actualTable.Add(10, $"Hello {10}");
actualTable.Add(11, $"Hello {11}");


actualTable.Delete(0);
var res = actualTable.Add(10, $"Hello {10}");
actualTable.Delete(12);
for(int i = 0; i < n/2; ++i){
    actualTable.Delete(i);
}

res = actualTable.Add(0, $"Hello {0}");
res = actualTable.Delete(0);
res = actualTable.Add(10, $"Hello {10}");
res = actualTable.Add(20, $"Hello {20}");
res = actualTable.Add(30, $"Hello {30}");
res = actualTable.Delete(10);
res = actualTable.Add(40, $"Hello {40}");
res = actualTable.Add(50, $"Hello {50}");
res = actualTable.Add(10, $"Hello {10}");

var people = new Person[] {
                        new Person(25, "John", "Doe"),
                        new Person(23, "Jane", "Doe"),
                        new Person(65, "Kurt", "Russell"),
                        new Person(57, "Dolph", "Lundgren"),
                        new Person(28, "Tim", "Smith"),
                        new Person(35, "Jack", "Doe"),
                        new Person(23, "Jane", "Doe"),
                        new Person(63, "Ralph", "Lundgren"),
                        new Person(25, "Jane", "Smith"),
                        new Person(23, "Laura", "Doe"),
                        new Person(43, "Laura", "Lundgren"),
};

Person p1 = people[0];
Person p2 = null;
Person p3 = null;
Person p4 = people[0];
Person p5 = people[1];

var t1 = p1 == p2;
var t2 = p2 == p1;
var t3 = p2 == p3;
var t4 = p4 == p1;
t4 = p4.Equals(p1);
t4 = p1.Equals(p4);
var t5 = p5 == new Person(p5);



int minVal = 1000;
int maxVal = 9000;
var rand = new Random();
var keys = new string[people.Length];
for(int i = 0; i < keys.Length; ++i) {
   keys[i] = "08" + rand.Next(minVal, maxVal);
}
//----ADD---
var data = new Entry<string, Person>[people.Length];
var tmpTable = new HashTable<string, Person>(people.Length);

for(int i = 0; i < keys.Length/2; ++i) {
    tmpTable.Add(keys[i], people[i]);
}

data = tmpTable.data.ToArray();
data[0] = new Entry<string, Person>("123", people[0]);
var table1 = new HashTable<string, Person>(10);//data);

for(int i = 0; i < keys.Length * 0.7; ++i) {
    table1.Add(keys[i], people[i]);
}

//---ADD---

var table = new HashTable<string, Person>(people.Length);

int keyIdx = 0;
foreach(var p in people) {
    table.Add(keys[keyIdx++], p);
    if(keyIdx == keys.Length)
      System.Console.WriteLine($"Key: {keys[keyIdx - 1]} deleted? {(table.Delete(keys[keyIdx - 1]) ? "YES" : "NO")}");
}

var testAdd1 = table.Add(keys[keys.Length - 1], new Person(43, "Laura", "Lundgren"));
var testAdd2 = table.Add("08" + rand.Next(maxVal, maxVal + 900), new Person(43, "Laura", "Lundgren"));

var indices = Enumerable.Range(0, keys.Length).OrderBy(x => rand.Next()).ToArray();
var flag = true;
for(int i = 0; i < keys.Length / 2; ++i) {
    var r1 = table.Find(keys[indices[rand.Next(0, keys.Length)]]);
    var key = keys[indices[rand.Next(0, keys.Length)]];
    if( flag && table.Find(key) != null) {
        table.Delete(key);
        flag = false;
    }

}
var delKey = keys[indices[rand.Next(0, keys.Length)]];
var testDelete = table.Delete(delKey);
var r2 = table.Find(delKey);
testDelete = table.Delete(delKey);

System.Console.WriteLine();


