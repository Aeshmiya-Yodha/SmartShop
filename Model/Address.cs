public class Address
{
    public int AddressId {get; set; }

    public required string  AddressLane {get; set;}

    public required string  City {get; set;}

    public required string  State {get; set; }

    public required string Country {get; set;}

    public required string PostalCode {get; set;}

    public  int  UserId {get; set;}

    public required User User {get; set; }
}