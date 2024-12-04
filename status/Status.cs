namespace BurguerManiaAPI.status;


public class Status{

  public int Id{get; init;}
  public string Name {get; private set;}

  public Status(string name){
    Name = name;
  }

  public void setName(string name){
    Name=name;
  }

}