
using ToDo;

var HTsize = 10;
var actualTable_ = new HashTable<int, string>(HTsize);
var randomNum = new Random();
var n_0 = randomNum.Next(1, 7);
var n_1 = HTsize + n_0;
var n_2 = 2*HTsize + n_0;

actualTable_.Add(n_0, $"Hello {n_0}");
actualTable_.Add(n_1, $"Hello {n_1}");
actualTable_.Add(n_2, $"Hello {n_2}");

//actualTable_.Delete(n_0); //example1 
actualTable_.Delete(n_1); //example2 

//var searchTest = actualTable_.FindIndex(n_1) != -1; //example1
var searchTest = actualTable_.FindIndex(n_2) != -1; //example2 

var addTest = actualTable_.Add(n_2, $"Hello {n_2}");
string aString = "0001";
System.Console.WriteLine(aString.GetHashCode());
aString = "1000";
System.Console.WriteLine(aString.GetHashCode());
var aNum = 10;
System.Console.WriteLine(aNum.GetHashCode());
var actualTable = new HashTable<int, string>(10);
int n = 10;
var h = (string k) => (Char.ToUpper(k[0]) - 'A') % n;
var p = (string k, int i) => (h(k) + i) % n; //1, 2, 3, 4
var p_2 = (string k, int i) => (h(k) + i * i) % n; // 1, 4, 9, 16, 25
var p_2_1 = (string k, int i) => (h(k) + 0.5*(i*i + i)) % n; //1, 3, 6, 10 

var keyString = "Alex";
var idx = h(keyString);
int i_ = 0;
idx = p(keyString, ++i_);
idx = p(keyString, ++i_);
idx = p(keyString, ++i_);

i_ = 0;
var idx2 = p_2(keyString, ++i_);
idx2 = p_2(keyString, ++i_);
idx2= p_2(keyString, ++i_);

i_ = 0;
var idx2_1 = p_2_1(keyString, ++i_);
idx2_1 = p_2_1(keyString, ++i_);
idx2_1= p_2_1(keyString, ++i_);


for(int i = 0; i < n/2; ++i){
    actualTable.Add(i, $"Hello {i}");
}

actualTable.Add(10, $"Hello {10}");
actualTable.Add(1, $"Hello 1");
//Wrap around (if as begin) Collisions
// for(int i = 0; i < n/2; ++i){
//     actualTable.Add(i*n + i, $"Hello {i}");
// }

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

res = actualTable.Add(10, $"Hello {10}");
res = actualTable.Delete(10);
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
var table1 = new HashTable<string, Person>(data);

for(int i = 0; i < keys.Length * 0.7; ++i) {
    table1.Add(keys[i], people[i]);
}

//---ADD---

var table = new HashTable<string, Person>(people.Length);

int keyIdx = 0;
foreach(var person in people) {
    table.Add(keys[keyIdx++], person);
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


