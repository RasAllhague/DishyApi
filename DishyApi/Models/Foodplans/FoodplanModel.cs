namespace DishyApi.Models.Foodplans;

public class FoodplanModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? ImageId { get; set; }
    public bool NotifyUsers { get; set; }
    public bool Deactivated { get; set; }
    public int OwningUserId { get; set; }
    public int CreateUserId { get; set; }
    public DateTime CreateDate { get; set; }
    public int? ModifyUserId { get; set; }
    public DateTime? ModifyDate { get; set; }
}
