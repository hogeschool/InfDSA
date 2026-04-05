public interface IHashTable<K, V>{
    bool Add(K key, V value);
    V? Find(K key);
    int FindIndex(K key);
    bool Delete(K key);
}

