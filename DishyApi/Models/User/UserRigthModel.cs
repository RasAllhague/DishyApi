namespace DishyApi.Models.User;

public class UserRigthModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RightId { get; set; }
    public DateTime CreateDate { get; set; }
    public int CreateUserId { get; set; }
}
