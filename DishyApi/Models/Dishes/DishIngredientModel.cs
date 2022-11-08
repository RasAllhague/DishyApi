namespace DishyApi.Models.Dishes;

public class DishIngredientModel
{
    public int Id { get; set; }
    public int DishId { get; set; }
    public int IngredientId { get; set; }
    public float? BaseAmount { get; set; }
    public int? MeasurementUnitId { get; set; }
    public DateTime CreateDate { get; set; }
    public int CreateUserId { get; set; }
}
