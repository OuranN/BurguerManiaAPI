namespace BurguerManiaAPI.categories;

public class Categories
{
    public int Id { get; init; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string PathImage { get; private set; } 

    public Categories(string name, string description, string pathImage)
    {
        Name = name;
        Description = description;
        PathImage = pathImage;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }

    public void SetPathImage(string pathImage)
    {
        PathImage = pathImage;
    }

}
