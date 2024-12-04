namespace BurguerManiaAPI.products;

public record AddProductRequest(string Name, string PathImage, decimal Price, string BaseDescription, string FullDescription, int CategoryId);
