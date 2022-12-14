namespace DishyApi.Models.Dishes;

public class DishUserRightModel
{
    public int Id { get; set; }
    public int DishId { get; set; }
    public int UserId { get; set; }
    public int RightId { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }
    public bool Deactivated { get; set; }
    public int CreateUserId { get; set; }
    public DateTime CreateDate { get; set; }
    public int? ModifyUserId { get; set; }
    public DateTime? ModifyDate { get; set; }
}
