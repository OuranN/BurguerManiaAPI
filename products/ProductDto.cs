namespace BurguerManiaAPI.products;

public record ProductDto(int Id, string Name, string Path_image, decimal Price, string Description, string FullDescription, int CategoryId);
