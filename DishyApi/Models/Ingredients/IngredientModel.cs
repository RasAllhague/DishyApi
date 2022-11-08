namespace DishyApi.Models.Ingredients;

public class IngredientModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public int? ImageId { get; set; }
    public int CreateUserId { get; set; }
    public DateTime CreateDate { get; set; }
    public int? ModifyUserId { get; set; }
    public DateTime? ModifyDate { get; set; }
}
