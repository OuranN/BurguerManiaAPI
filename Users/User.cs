namespace BurguerManiaAPI.Users;


public class User{

  public int Id{get; init;}
  public string Name {get; private set;}
  public string Email {get; private set;}
  public string Password {get; private set;}

  public User(string name, string email, string password){
    Name = name;
    Email = email;
    Password = password;
  }

  public void setName(string name){
    Name=name;
  }

  public void setEmail(string email){
    Email=email;
  }

    public void setPassword(string password){
    Password=password;
  }
}