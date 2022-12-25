using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace Supermarket.Models;

public class FilterViewModel
{
    public FilterViewModel(List<ProductCategory> companies, int category, string name)
    {
        // устанавливаем начальный элемент, который позволит выбрать всех
        companies.Insert(0, new ProductCategory { Name = "Всі", Id = 0 });
        ProductCategories = new SelectList(companies, "Id", "Name", category);
        SelectedCategory = category;
        SelectedName = name;
    }
    public SelectList ProductCategories { get; }
    public int SelectedCategory { get; }
    public string SelectedName { get; }
}