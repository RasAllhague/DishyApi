namespace DishyApi.Models.Categories;

public class CategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int CategoryTypeId { get; set; }
    public DateTime CreateDate { get; set; }
    public int CreateUserId { get; set; }
    public DateTime? ModifyDate { get; set; }
    public int? ModifyUserId { get; set; }

    public CategoryModel()
    {
        Id = 0;
        Name = "Empty";
        Description = null;
        CategoryTypeId = 0;
        CreateDate = DateTime.Now;
        ModifyDate = DateTime.Now;
        CreateUserId = 0;
        ModifyUserId = 0;
    }
}
