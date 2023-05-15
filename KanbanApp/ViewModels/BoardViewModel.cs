using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Pages;
using KanbanApp.Services;
using System.Collections.ObjectModel;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(CurrentBoard), "board")]
    public partial class BoardViewModel : ObservableObject
    {
        [ObservableProperty] private Board _currentBoard;
        [ObservableProperty] private ObservableCollection<Category> _categories;
        [ObservableProperty] private bool _isRefreshing;
        private readonly CategoryService _categoryService;
        public BoardViewModel(CategoryService categoryService)
        {
            _categoryService = categoryService;
            Categories = new ObservableCollection<Category>();
        }
        public async void BackButton()
        {
            foreach (var page in Shell.Current.Navigation.NavigationStack)
            {
                if (page != null && (page.GetType() == typeof(CreateBoardPage)))
                {
                    Shell.Current.Navigation.RemovePage(page);
                    break;
                }
            }
            var param = new Dictionary<string, object> { { "newBoard", CurrentBoard } };

            await Shell.Current.GoToAsync("..", param);
        }
        [RelayCommand]
        public async Task GoToCreateTask(Category category)
        {
            var param = new Dictionary<string, object> { { "category", category } };
            await Shell.Current.GoToAsync(nameof(CreateTaskPage), param);
        }
        [RelayCommand]
        public async Task GoToCreateCategory()
        {
            var param = new Dictionary<string, object> { { "board", CurrentBoard } };
            await Shell.Current.GoToAsync(nameof(CreateCategoryPage), param);
        }
        async partial void OnCurrentBoardChanged(Board value)
        {
            var cat = await _categoryService.GetCategoriesByBoard(value.Id);
            foreach (var category in cat) { Categories.Add(category); }
            value.Categories = Categories;
        }

        [RelayCommand]
        public async Task InviteButton()
        {
            var param = new Dictionary<string, object> { { "board", CurrentBoard } };
            await Shell.Current.GoToAsync(nameof(InvitePage), param);
        }
    }
}
