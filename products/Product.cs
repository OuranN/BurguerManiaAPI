using BurguerManiaAPI.categories;

namespace BurguerManiaAPI.products;

public class Product
{
    public int Id { get; init; } // Chave primária
    public string Name { get; private set; }
    public string PathImage { get; private set; }
    public decimal Price { get; private set; }
    public string BaseDescription { get; private set; }
    public string FullDescription { get; private set; }
    public int CategoryId { get; set; } // Chave estrangeira

    public Categories? Category { get; set; } // Relacionamento com Categories

    public Product(string name, string pathImage, decimal price, string baseDescription, string fullDescription, int categoryId)
    {
        Name = name;
        PathImage = pathImage;
        Price = price;
        BaseDescription = baseDescription;
        FullDescription = fullDescription;
        CategoryId = categoryId;
    }
    public void SetName(string name) => Name = name;
    public void SetPathImage(string pathImage) => PathImage = pathImage;
    public void SetPrice(decimal price) => Price = price;
    public void SetBaseDescription(string baseDescription) => BaseDescription = baseDescription;
    public void SetFullDescription(string fullDescription) => FullDescription = fullDescription;
    public void SetCategoryId(int categoryId) => CategoryId = categoryId;
}
