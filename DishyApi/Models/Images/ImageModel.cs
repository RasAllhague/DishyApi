namespace DishyApi.Models.Images;

public class ImageModel
{
    public int Id { get; set; }
    public string OriginalName { get; set; }
    public string InternalName { get; set; }
    public string extension { get; set; }
    public long Size { get; set; }
    public int CreateUserId { get; set; }
    public DateTime CreateDate { get; set; }
}
