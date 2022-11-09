namespace DishyApi.Models.Categories;

public enum CategoryType
{
    Dish = 1,
    Ingredient = 2,
    Foodplan = 3,
}

public class CategoryTypeModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }

    public CategoryTypeModel()
    {
        Id = 0;
        Name = "Empty";
        CreateDate = DateTime.Now;
    }
}
