
public class Person
{
    public int Age { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }

    public Person(int age, string lastName, string firstName)
    {
        Age = age;
        LastName = lastName;
        FirstName = firstName;
    }

    public Person(Person p)
    {   
        if (!ReferenceEquals(p, null)) {
            Age = p.Age;
            LastName = p.LastName;
            FirstName = p.FirstName;
        }
    }

    public static bool operator==(Person p1, Person p2){
        if(ReferenceEquals(p1, null) && ReferenceEquals(p2, null) || ReferenceEquals(p1, p2))
            return true;
        if(!ReferenceEquals(p1, null))
            return p1.Equals(p2);
        return p2.Equals(p1);
    }

    public static bool operator!=(Person p1, Person p2) => !(p1 == p2);

    public override bool Equals(object? obj)
    {
        if(obj == null)
            return false;
        if(obj is Person p) 
            return ReferenceEquals(this, p) || p.Age == Age && p.LastName == LastName && p.FirstName == FirstName;
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}