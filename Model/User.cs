
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;

public class User
{
    public int UserId {get; set; }
    public required string  FirstName {get; set;}
    public required string LastName {get; set ; }
    public required string ContactNumber {get; set;} // authentication will be handled by the Contact number or Password
    public string ? Email{get; set; }
    public required string PasswordHash {get; set; }
    public required ICollection<Address> Addresses {get; set; }
}