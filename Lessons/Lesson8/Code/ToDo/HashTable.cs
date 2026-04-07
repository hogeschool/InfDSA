using System.Collections.ObjectModel;

namespace ToDo;

public class HashTable<K, V> : IHashTable<K, V>
{
    Entry<K, V>[]? buckets { get; set;}

    public int Count { get; private set; }
    public int Size => buckets != null ? buckets.Length : -1;

    public ReadOnlyCollection<Entry<K, V>> data => buckets == null? null : buckets.AsReadOnly();

    public HashTable() { buckets = null; }

    public HashTable(Entry<K, V>[]? input) { importData(input); }
    public HashTable(ICollection<Entry<K, V>>? input) { importData(input);}

    public HashTable(ICollection<Entry<K, V>>? input, int count) { 
        importData(input);
        this.Count = count;
    }

    public HashTable(int capacity)
    {
        buckets = new Entry<K, V>[capacity];
    }

    public int getIndex(K key)
    {
        int hashCode = Math.Abs(key.GetHashCode());
        int index = hashCode % buckets.Length;
        return index;
    }

    // public bool Add_(K key, V value) {
    //     var res = insert(new Entry<K, V>(key, value), buckets, Count);
    //     Count = res.Item2;
    //     return res.Item1;
    // }

    public bool Add(K key, V value) {
        if(buckets == null || buckets.Length == 0 || key == null) return false;
        int index = getIndex(key);
        if (buckets[index] == null) //empty slot
        {
            buckets[index] = new Entry<K, V>(key, value);
            Count++;
            return true;
        }
        if (buckets[index].Key.Equals(key)) // no duplications
        {
            return false;
        }
        
        int potentialIndex = (index + 1) % buckets.Length; //linear probing
        while(potentialIndex != index)
        {
            if (buckets[potentialIndex] == null) //empty slot
            {
                buckets[potentialIndex] = new Entry<K, V>(key, value);
                Count++;
                return true;    
            }

            if (buckets[potentialIndex].Key.Equals(key)) // no duplications
            {
                return false;
            }
            potentialIndex = (potentialIndex + 1) % buckets.Length;
        }

        return false;
    }

    public V? Find(K key)
    {
        if(buckets == null || buckets.Length == 0 || key == null) return default;
        int index = getIndex(key);
        if (buckets[index] == null) //empty slot
        {
            return default;
        }
        if (buckets[index].Key.Equals(key)) //found
        {
            return buckets[index].Value;
        }
        
        int potentialIndex = (index + 1) % buckets.Length; //linear probing
        while(potentialIndex != index)
        {
            if (buckets[potentialIndex] == null) //empty slot
            {
                return default;    
            }

            if (buckets[potentialIndex].Key.Equals(key)) //found
            {
                return buckets[potentialIndex].Value;
            }
            potentialIndex = (potentialIndex + 1) % buckets.Length;
        }

        return default;
    }

    //This method returns -1 if key has not been found, the index in buckets otherwise.
    public int FindIndex(K key)
    {
        if(buckets == null || buckets.Length == 0 || key == null) return -1;
        int index = getIndex(key);
        if (buckets[index] == null) //empty slot
        {
            return -1;
        }
        if (buckets[index].Key.Equals(key)) 
        {
            return index;
        }
        
        int potentialIndex = (index + 1) % buckets.Length; //linear probing
        while(potentialIndex != index)
        {
            if (buckets[potentialIndex] == null) //empty slot
            {
                return -1;    
            }

            if (buckets[potentialIndex].Key.Equals(key)) // no duplications
            {
                return potentialIndex;
            }
            potentialIndex = (potentialIndex + 1) % buckets.Length;
        }

        return -1;
    }
    
    public bool Delete(K key) => DeleteLazy(key);//DeleteReHash(key)//DeleteEager(key)

    public bool DeleteLazy(K key) //Naive/Lazy/wrong
    {    
        if(buckets == null || buckets.Length == 0 || key == null) return false;
        int index = getIndex(key);
        if (buckets[index] == null) //empty slot
        {
            return false;
        }
        if (buckets[index].Key.Equals(key)) //found
        {
            buckets[index] = null;
            Count--;
            return true;
        }
        
        int potentialIndex = (index + 1) % buckets.Length; //linear probing
        while(potentialIndex != index)
        {
            if (buckets[potentialIndex] == null) //empty slot
            {
                return false;    
            }

            if (buckets[potentialIndex].Key.Equals(key)) //found
            {
                buckets[potentialIndex] = null;
                Count--;
                return true;
            }
            potentialIndex = (potentialIndex + 1) % buckets.Length;
        }

        return false;
    }

    public bool DeleteReHash(K key) //Using ReHash
    {    
        if(buckets == null || buckets.Length == 0 || key == null) return false;
        int index = getIndex(key);
        if (buckets[index] == null) //empty slot
        {
            return false;
        }
        if (buckets[index].Key.Equals(key)) //found
        {
            buckets[index] = null;
            Count--;
            rehash();
            return true;
        }
        
        int potentialIndex = (index + 1) % buckets.Length; //linear probing
        while(potentialIndex != index)
        {
            if (buckets[potentialIndex] == null) //empty slot
            {
                return false;    
            }

            if (buckets[potentialIndex].Key.Equals(key)) //found
            {
                buckets[potentialIndex] = null;
                Count--;
                // if(Count/buckets.Length > 0.5){ rehash(3*Size);}
                // else{rehash();}
                rehash(); //reinsert all values in the hashtable
                return true;
            }
            potentialIndex = (potentialIndex + 1) % buckets.Length;
        }
        
        return false;
    }
    
    public bool DeleteEager(K key) //Eager deletion
    {    
        if(buckets == null || buckets.Length == 0 || key == null) return false;
        int index = getIndex(key);
        if (buckets[index] == null) //empty slot
        {
            return false;
        }
        if (buckets[index].Key.Equals(key)) //found
        {
            buckets[index] = null;
            Count--;
            localRehash(index);
            // var nextIndex = (index + 1) % buckets.Length;
            // while(buckets[nextIndex] != null)
            // {
            //     var tmpEntry = new Entry<K,V>(buckets[nextIndex].Key, buckets[nextIndex].Value);
            //     //deletion
            //     buckets[nextIndex] = null;
            //     Count--;
            //     //
            //     Add(tmpEntry.Key, tmpEntry.Value); //reinsertion
            //     nextIndex = (nextIndex + 1) % buckets.Length;
            // }
            return true;
        }
        
        int potentialIndex = (index + 1) % buckets.Length; //linear probing
        while(potentialIndex != index)
        {
            if (buckets[potentialIndex] == null) //empty slot
            {
                return false;    
            }

            if (buckets[potentialIndex].Key.Equals(key)) //found
            {
                buckets[potentialIndex] = null;
                Count--;
                //rehash(); //O(n)
                //looking for the next empty slot, re-insert all entries in the cluster
                localRehash(potentialIndex);
                // var nextIndex = (potentialIndex + 1) % buckets.Length;
                // while(buckets[nextIndex] != null)
                // {
                //     var tmpEntry = new Entry<K,V>(buckets[nextIndex].Key, buckets[nextIndex].Value);
                //     //deletion
                //     buckets[nextIndex] = null;
                //     Count--;
                //     //
                //     Add(tmpEntry.Key, tmpEntry.Value); //reinsertion
                //     nextIndex = (nextIndex + 1) % buckets.Length;
                // }
                return true;
            }
            potentialIndex = (potentialIndex + 1) % buckets.Length;
        }
        
        return false;
    }

    public bool Delete_BackwardShifting(K key) {    
    
        int initialIdx = FindIndex(key);
        if (initialIdx == -1) return false; // key not found 
        // Remove the entry at initialIdx 
        buckets[initialIdx] = null;
        var emptyIdx = initialIdx;
        Count--; 
        int nextIdx = (emptyIdx + 1) % Size; 
        while (buckets[nextIdx] != null) { 
            int hashIdx = getIndex(buckets[nextIdx].Key); 
            // ideal position of element at nextIdx 
            // Check whether hashIdx is still reachable without passing emptyIdx 
            bool inRange = (emptyIdx < hashIdx && hashIdx <= nextIdx) ||
                           (nextIdx < emptyIdx && emptyIdx < hashIdx) || 
                           (hashIdx <= nextIdx && nextIdx < emptyIdx);
            //outRange => unreachable => shift necessary
            bool outOfRange =  hashIdx  <= emptyIdx && emptyIdx < nextIdx ||
                               nextIdx <= hashIdx && hashIdx < emptyIdx ||
                               emptyIdx <= nextIdx && nextIdx < hashIdx
                            ;
            if (!inRange) { 
                // Shift element at nextIdx back into emptyIdx 
                buckets[emptyIdx] = buckets[nextIdx]; 
                buckets[nextIdx] = null; 
                // Now the new empty slot is at nextIdx 
                emptyIdx = nextIdx; } 
            nextIdx = (nextIdx + 1) % Size; 
        } 
        return true; 
    }

    void localRehash(int index)
    {
        var nextIndex = (index + 1) % buckets.Length;
        while(buckets[nextIndex] != null)
        {
            var tmpEntry = new Entry<K,V>(buckets[nextIndex].Key, buckets[nextIndex].Value);
            //deletion
            buckets[nextIndex] = null;
            Count--;
            //
            Add(tmpEntry.Key, tmpEntry.Value); //reinsertion
            nextIndex = (nextIndex + 1) % buckets.Length;
        }

    }
    
    void rehash(int newSize = -1){

        if(buckets == null || buckets.Length == 0) return;
        var oldBuckets = buckets;
        var newBuckets = newSize > Size && Size > 0 ? new Entry<K, V>[newSize] : new Entry<K, V>[10];
        buckets = newBuckets;
        Count = 0;

        for(int i = 0; i < oldBuckets.Length; ++i){
            if(oldBuckets[i] != null)
              Count = insert(new Entry<K,V>(oldBuckets[i].Key, oldBuckets[i].Value), buckets, Count).Item2;
        }

    }
    
    //static Add
    static Tuple<bool, int> insert(Entry<K,V>? el, Entry<K,V>[] buckets, int count)
    {
        if(el == null || buckets == null || buckets.Length == 0) return Tuple.Create(false, count);
        
        var key = el.Key;
        int index = Math.Abs(key.GetHashCode()) % buckets.Length;
        if(buckets[index] == null){
            count++;
            buckets[index] = el;
            return Tuple.Create(true, count);
        }
        
        else if(buckets[index].Key.Equals(key)){
          return Tuple.Create(false, count);}
  
        //With Collisions:
        else
        {
            int potentialIndex = (index + 1) % buckets.Length; //linear probing
            while(buckets[potentialIndex] != null){
                if(potentialIndex == index || buckets[potentialIndex].Key.Equals(key))
                {
                   return Tuple.Create(false, count);
                }
                potentialIndex = (potentialIndex + 1) % buckets.Length;               
            }           
            count++;
            buckets[potentialIndex] = el;
            return Tuple.Create(true, count);
        }
    }

    //DO NOT REMOVE the following methods:
    private void importData(Entry<K, V>[]? inputData){
        if(inputData != null) {
            buckets = new Entry<K, V>[inputData.Length];
            for (int i = 0; i < inputData.Length; ++i) 
                buckets[i] = inputData[i];
        }
    }

    private void importData(ICollection<Entry<K, V>>? inputData){
        if(inputData != null) {
            buckets = new Entry<K, V>[inputData.Count];
            for (int i = 0; i < inputData.Count; ++i) 
                buckets[i] = inputData.ElementAt(i);
        }
    }
}
