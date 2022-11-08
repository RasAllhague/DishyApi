namespace DishyApi.Models.Dishes;

public class DishCategoryModel
{
    public int Id { get; set; }
    public int DishId { get; set; }
    public int CategoryId { get; set; }
    public int CreateUserId { get; set; }
    public DateTime CreateDate { get; set; }
}
