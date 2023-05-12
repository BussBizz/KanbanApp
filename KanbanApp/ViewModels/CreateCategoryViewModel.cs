using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Services;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(CurrentBoard), "board")]
    public partial class CreateCategoryViewModel : ObservableObject
    {
        [ObservableProperty] private Board _currentBoard;
        [ObservableProperty] private Category _newCategory;
        private CategoryService _categoryService = new CategoryService();

        public CreateCategoryViewModel()
        {
            NewCategory = new Category();
        }
        [RelayCommand]
        async Task CreateCategory()
        {
            try
            {
                NewCategory.BoardId = CurrentBoard.Id;
                var newCategory = await _categoryService.PostCategory(NewCategory);
                CurrentBoard.Categories ??= new List<Category>();
                CurrentBoard.Categories.Add(newCategory);
                var param = new Dictionary<string, object> { { "board", CurrentBoard } };
                await Shell.Current.GoToAsync("..", param);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Fejl!", e.Message, "Ok");
            }
        }
    }
}
