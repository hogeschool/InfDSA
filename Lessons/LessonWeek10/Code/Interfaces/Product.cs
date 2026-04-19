
public class Product : IComparable<Product>
{
    public int Price { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }

    public Product(int price, string name, string brand)
    {
        Price = price;
        Name = name;
        Brand = brand;
    }

    public Product(Product p)
    {   
        if (!ReferenceEquals(p, null)) {
            Price = p.Price;
            Name = p.Name;
            Brand = p.Brand;
        }
    }

    public static bool operator==(Product p1, Product p2){
        if(ReferenceEquals(p1, null) && ReferenceEquals(p2, null) || ReferenceEquals(p1, p2))
            return true;
        if(!ReferenceEquals(p1, null))
            return p1.Equals(p2);
        return p2.Equals(p1);
    }

    public static bool operator!=(Product p1, Product p2) => !(p1 == p2);

    public override bool Equals(object? obj)
    {
        if(obj == null)
            return false;
        if(obj is Product p) 
            return ReferenceEquals(this, p) || p.Price == Price && p.Name == Name && p.Brand == Brand;
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public int CompareTo(Product? other)
    {
        if(other == null)
            return 1;
        return Price.CompareTo(other.Price);
    }
}