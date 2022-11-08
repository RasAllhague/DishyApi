namespace DishyApi.Models.User;

public class UserModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
}
