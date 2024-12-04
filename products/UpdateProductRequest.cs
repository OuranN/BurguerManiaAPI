namespace BurguerManiaAPI.products;

public record UpdateProductRequest(string Name, string PathImage, decimal Price, string BaseDescription, string FullDescription, int CategoryId);
