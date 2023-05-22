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
        private readonly CategoryService _categoryService;
        private readonly MemberService _memberService;
        private readonly TasksService _tasksService;
        [ObservableProperty] private Board _currentBoard;
        [ObservableProperty] private ObservableCollection<Category> _categories;
        [ObservableProperty] private Member _currentMember;
        public BoardViewModel(CategoryService categoryService, MemberService memberService, TasksService tasksService)
        {
            _categoryService = categoryService;
            _memberService = memberService;
            _tasksService = tasksService;
            Categories = new ObservableCollection<Category>();
        }
       
        [RelayCommand]
        public async Task GoToCreateTask(Category category)
        {
            var param = new Dictionary<string, object> { { "category", category }, { "currentMember", CurrentMember } };
            await Shell.Current.GoToAsync(nameof(CreateTaskPage), param);
        }

        [RelayCommand]
        public async Task GoToTaskPage(KanbanTask kanbanTask)
        {
            var param = new Dictionary<string, object> { { "currentTask", kanbanTask }, { "currentMember", CurrentMember }, { "board", CurrentBoard } };
            await Shell.Current.GoToAsync(nameof(TaskPage), param);
        }

        [RelayCommand]
        public async Task DeleteTask(KanbanTask kanbanTask)
        {
            await _tasksService.DeleteTask(kanbanTask);
            CurrentBoard.Categories.FirstOrDefault(c => c.Id == kanbanTask.CategoryId).KanbanTasks.Remove(kanbanTask);
        }

        [RelayCommand]
        public async Task GoToCreateCategory()
        {
            var param = new Dictionary<string, object> { { "board", CurrentBoard } };
            await Shell.Current.GoToAsync(nameof(CreateCategoryPage), param);
        }

        [RelayCommand]
        public async Task GoToAdminPage()
        {
            var param = new Dictionary<string, object> { { "board", CurrentBoard } };
            await Shell.Current.GoToAsync(nameof(AdminPage), param);
        }

        async partial void OnCurrentBoardChanged(Board value)
        {
            var members = await _memberService.GetMembershipsByBoard(value.Id);
            value.Members = members.ToList();
            var userId = await SecureStorage.GetAsync("userId");
            CurrentMember = value.Members.FirstOrDefault(m => m.UserId == int.Parse(userId));
            var categories = await _categoryService.GetCategoriesByBoard(value.Id);
            foreach (var category in categories) { Categories.Add(category); }
            value.Categories = Categories.ToList();
        }

        [RelayCommand]
        public async Task InviteButton()
        {
            var param = new Dictionary<string, object> { { "board", CurrentBoard } };
            await Shell.Current.GoToAsync(nameof(InvitePage), param);
        }
    }
}
