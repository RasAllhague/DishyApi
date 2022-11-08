namespace DishyApi.Models.Foodplans;

public class FoodplanDishModel
{
    public int Id { get; set; }
    public int FoodplanId { get; set; }
    public int DishId { get; set; }
    public DateTime PlannedDate { get; set; }
    public float AmountMultiplier { get; set; }
    public int CreateUserId { get; set; }
    public DateTime CreateDate { get; set; }
}
