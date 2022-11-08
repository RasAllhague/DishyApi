namespace DishyApi.Models.Ingredients;

public class IngredientCategoryModel
{
    public int Id { get; set; }
    public int IngredientId { get; set; }
    public int CategoryId { get; set; }
    public int CreateUserId { get; set; }
    public DateTime CreateDate { get; set; }
}
