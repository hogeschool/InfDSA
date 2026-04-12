

public enum Status {filled, deleted};
public class Entry<K, V>
{
    public K Key { get; set; }
    public V Value { get; set; }
    public Status State {get; set;}

    public Entry(K key, V value)
    {
        Key = key;
        Value = value;
        State = Status.filled;
    }

    public Entry(Entry<K, V> entry)
    {
        Key = entry.Key;
        Value = entry.Value;
        State = entry.State;
    }
}