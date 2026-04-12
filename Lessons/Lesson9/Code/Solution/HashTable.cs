using System.Collections.ObjectModel;

namespace Solution;

public class HashTable<K, V> : IHashTable<K, V>
{
    Entry<K, V>[]? buckets { get; set;}

    public ReadOnlyCollection<Entry<K, V>> data => buckets == null? null : buckets.AsReadOnly();

    public HashTable() { buckets = null;}

    public HashTable(Entry<K, V>[]? input) { importData(input);}
    public HashTable(ICollection<Entry<K, V>>? input) { importData(input);}

    public HashTable(int capacity)
    {
        buckets = new Entry<K, V>[capacity];
    }
    
    int count;
    int numDeletions;

    Func<int, int> probingFuncLinear(int index)
    {    
        return (int i) => (index + i) % buckets.Length;
    }

    Func<int, int> probingFuncQuadratic(int index)
    {    
        return (int i) => (index + i * i) % buckets.Length;
    }
    
    public int Capacity {get => buckets != null ? buckets.Length : -1;}
    public int Count {get => count;}

    public int getIndex(K key)
    {
        int hashCode = Math.Abs(key.GetHashCode());
        int index = hashCode % buckets.Length;
        return index;
    }

    public bool Add(K key, V value)
    {
        if(buckets == null || buckets.Length == 0) return false;
        //if(Count >= 0.5 * Capacity) rehash();
        
        if(Find != null && Find(key) != null) return false; //no duplications may occur
        
        int index = getIndex(key);
        if (buckets[index] == null || buckets[index].State == Status.deleted) { // the bucket is empty, we can insert
            if(buckets[index] == null)
              count++;
            else{
              count++;
              numDeletions--;
            }
            buckets[index] = new Entry<K, V>(key, value);
            return true;
        }
        // we have to do probing to find an empty bucket
        {
            var potentialIndex = (index + 1) % buckets.Length;

            while (potentialIndex != index)
            {
                if(buckets[potentialIndex] == null || 
                   buckets[potentialIndex].State == Status.deleted)
                {
                    buckets[potentialIndex] = new Entry<K, V>(key, value);
                    count++;
                    if(buckets[potentialIndex].State == Status.deleted)
                       numDeletions--;
                    return true;
                }
                potentialIndex = (potentialIndex + 1) % buckets.Length; //probingFuncLinear(potentialIndex)(1);
            }

            return false;         
        }
    }

    public bool Add_ProbingFunc(K key, V value)
    {
        if(buckets == null || buckets.Length == 0) return false;
        //if(Count >= 0.5 * Capacity) rehash();
        
        if(Find != null && Find(key) != null) return false; //no duplications may occur
        
        int index = getIndex(key);
        if (buckets[index] == null || buckets[index].State == Status.deleted) { // the bucket is empty, we can insert
            if(buckets[index] == null)
              count++;
            else{
              count++;
              numDeletions--;
            }
            buckets[index] = new Entry<K, V>(key, value);
            return true;
        }
        // we have to do probing to find an empty bucket
        int i = 1; //First attempt

        while (i < Capacity)   //potentialIndex != index
        {
            var potentialIndex = probingFuncLinear(index)(i);//(index + i) % buckets.Length; //linear probing

            if(buckets[potentialIndex] == null || 
                buckets[potentialIndex].State == Status.deleted)
            {
                buckets[potentialIndex] = new Entry<K, V>(key, value);
                count++;
                if(buckets[potentialIndex].State == Status.deleted)
                    numDeletions--;
                return true;
            }

            i++;
        }

        return false;         
    }

    public bool Add__(K key, V value)
    {
        if(buckets == null || buckets.Length == 0) return false;
        //if(Count >= 0.5 * Capacity) rehash();
        
        if(Find != null && Find(key) != null) return false; //no duplications may occur
        
        int index = getIndex(key);
        if (buckets[index] == null || buckets[index].State == Status.deleted) { // the bucket is empty, we can insert
            if(buckets[index] == null)
              count++;
            else{
              count++;
              numDeletions--;
            }
            buckets[index] = new Entry<K, V>(key, value);
            return true;
        }

        //the bucket contains the key, we cannot insert
        // else if (buckets[index].Key.Equals(key))
        //     return false;

        //else // we have to do probing to find an empty bucket
        {
            var potentialIndex = (index + 1) % buckets.Length;

            while (buckets[potentialIndex] != null && potentialIndex != index)
            {
                if(buckets[potentialIndex].State == Status.deleted)
                {
                    buckets[potentialIndex] = new Entry<K, V>(key, value);
                    count++;
                    numDeletions--;
                    return true;
                }
                potentialIndex = (potentialIndex + 1) % buckets.Length;
            }

            if(buckets[potentialIndex] == null)
            {
                buckets[potentialIndex] = new Entry<K, V>(key, value);
                count++;
                return true;
            }  

            return false;         
        }
    }

    public bool Add___(K key, V value)
    {
        if(buckets == null || buckets.Length == 0) return false;
        //if(Count >= 0.5 * Capacity) rehash();
        
        if(Find != null && Find(key) != null) return false; //no duplications may occur
        
        int index = getIndex(key);
        if (buckets[index] == null || buckets[index].State == Status.deleted) { // the bucket is empty, we can insert
            if(buckets[index] == null)
              count++;
            else{
              count++;
              numDeletions--;
            }
            buckets[index] = new Entry<K, V>(key, value);
            return true;
        }

        //the bucket contains the key, we cannot insert
        // else if (buckets[index].Key.Equals(key))
        //     return false;

        //else // we have to do probing to find an empty bucket
        {
            var potentialIndex = (index + 1) % buckets.Length;
            while (buckets[potentialIndex] != null &&  
                   buckets[potentialIndex].State != Status.deleted) // the bucket in position potentialIndex is not empty
            {
                if (potentialIndex == index) //|| buckets[potentialIndex].Key.Equals(key)) 
                      return false;
                potentialIndex = (potentialIndex + 1) % buckets.Length;
            }
            if(buckets[potentialIndex] == null)
              count++;
            else{ 
              count++;
              numDeletions--;
            }
            buckets[potentialIndex] = new Entry<K, V>(key, value);
            return true;        
        }
    }

    public V? Find(K key)
    {
        int index = getIndex(key);

        if (buckets[index] == null) return default;

        if (//buckets[index] != null && 
            buckets[index].Key.Equals(key) &&
            buckets[index].State != Status.deleted) // the hashed bucket contains the key we are looking for
        {
            return buckets[index].Value;
        }
        //else // the key we are looking for could be in another position: use linear probing to find it
        {
            var potentialIndex = (index + 1) % buckets.Length; //(index + 1) >= buckets.Length ? 0 : (index + 1);
            while (potentialIndex != index)
            {
                if (buckets[potentialIndex] == null) return default;
                if (buckets[potentialIndex].Key.Equals(key) && 
                    buckets[potentialIndex].State != Status.deleted)
                {
                    return buckets[potentialIndex].Value;
                }

                potentialIndex = (potentialIndex + 1) % buckets.Length;
            }
        }
        return default;
    }

    public bool Delete(K key)
    {
        //if(numDeletions > Capacity / 8 || Count >= Capacity / 2) rehash();

        int index = getIndex(key);
        if (buckets[index] == null) return false;

        if (//buckets[index] != null && 
            buckets[index].Key.Equals(key) &&
            buckets[index].State != Status.deleted
            ) //the hashed bucket is not empty and it contains the key that we want to delete
        {
            //buckets[index] = null;
   
            buckets[index].State = Status.deleted;
            count--;
            numDeletions++;
            return true;
 
        }

        //else //the key we want to delete could be in another position: use linear probing to find it.
        {
            var potentialIndex = (index + 1) % buckets.Length; //(index + 1) >= buckets.Length ? 0 : (index + 1);
            while (potentialIndex != index)
            {
                if (buckets[potentialIndex] == null) return false;
                if (buckets[potentialIndex].Key.Equals(key) &&
                    buckets[potentialIndex].State != Status.deleted)
                {
                    //buckets[potentialIndex] = null; //wrong deletion
                    buckets[potentialIndex].State = Status.deleted; //correct deletion
                    count--;
                    numDeletions++;
                    return true;
                    
                }
                potentialIndex = (potentialIndex + 1) % buckets.Length;
            }

            return false;
        }
    }

    void rehash(){
        if(buckets == null || buckets.Length == 0) return;
        var oldBuckets = buckets;
        var newBuckets = Capacity > 0 ? new Entry<K, V>[Capacity * 2] : new Entry<K, V>[10];
        buckets = newBuckets;
        count = 0;
        numDeletions = 0;

        for(int i = 0; i < oldBuckets.Length; ++i){
            if(oldBuckets[i] != null && oldBuckets[i].State == Status.filled)
               Add(oldBuckets[i].Key, oldBuckets[i].Value);
        }

    }

    private void importData(Entry<K, V>[]? inputData){
        if(inputData != null) {
            int totalElements = 0;
            buckets = new Entry<K, V>[inputData.Length];
            for (int i = 0; i < inputData.Length; ++i) {
                if(inputData[i] != null) {
                    buckets[i] = new Entry<K, V>(inputData[i]);
                    if(inputData[i].State != Status.deleted){
                        totalElements++; 
                    }
                }
            }
            count = totalElements;
        }
    }

    private void importData(ICollection<Entry<K, V>>? inputData){
        if(inputData != null) {
            int totalElements = 0;
            buckets = new Entry<K, V>[inputData.Count];
            for (int i = 0; i < inputData.Count; ++i) {
                if(inputData.ElementAt(i) != null) {
                    buckets[i] = new Entry<K, V>(inputData.ElementAt(i));
                    if(inputData.ElementAt(i).State != Status.deleted){
                        totalElements++; 
                    }
                }
            }
            count = totalElements;
        }
    }

}



