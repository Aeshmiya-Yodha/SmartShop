
public class User
{
    public int UserId {get; set; } // Key 
    public required string  FirstName {get; set;}
    public required string LastName {get; set ; }
    public required string ContactNumber {get; set;} // authentication will be handled by the Contact number or Password
    public string ? Email{get; set; }
    public required string PasswordHash {get; set; }
    public  ICollection<Address> Addresses {get; set; } = new List<Address>() ;
    public Cart Cart{get; set;} = null!; //This is a promise to the compiler that this will be filled "This will always be filled" which is not in the case of the new User
    public ICollection<Order> Orders {get; set;} = new List<Order>();
}