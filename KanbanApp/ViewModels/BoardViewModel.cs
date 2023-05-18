﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        [ObservableProperty] private Board _currentBoard;
        [ObservableProperty] private ObservableCollection<Category> _categories;
        [ObservableProperty] private bool _isRefreshing;
        public BoardViewModel(CategoryService categoryService, MemberService memberService)
        {
            _categoryService = categoryService;
            _memberService = memberService;
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
        public async Task GoToTaskPage(KanbanTask kanbanTask)
        {
            var userId = await SecureStorage.GetAsync("userId");
            var member = CurrentBoard.Members.FirstOrDefault(m => m.UserId == int.Parse((userId)));
            var param = new Dictionary<string, object> { { "currentTask", kanbanTask }, { "currentMember", member } };
            await Shell.Current.GoToAsync(nameof(TaskPage), param);
        }
        [RelayCommand]
        public async Task GoToCreateCategory()
        {
            var param = new Dictionary<string, object> { { "board", CurrentBoard } };
            await Shell.Current.GoToAsync(nameof(CreateCategoryPage), param);
        }
        async partial void OnCurrentBoardChanged(Board value)
        {
            value.Members = await _memberService.GetMembershipsByBoard(value.Id);
            var categories = await _categoryService.GetCategoriesByBoard(value.Id);
            foreach (var category in categories) { Categories.Add(category); }
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
