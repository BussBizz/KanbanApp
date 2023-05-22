using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(CurrentBoard), "board")]
    public partial class AdminViewModel : ObservableObject
    {
        private readonly MemberService _memberService;
        [ObservableProperty] private Board _currentBoard;
        [ObservableProperty] private ObservableCollection<Member> _members;

        public AdminViewModel(MemberService memberService)
        {
            _memberService = memberService;
            Members = new ObservableCollection<Member>();
        }

        partial void OnCurrentBoardChanged(Board value)
        {
            foreach (var member in CurrentBoard.Members)
            { 
                Members.Add(member);
            }
        }

        [RelayCommand]
        public async Task SaveChanges()
        {
            foreach (var member in Members)
                await _memberService.UpdateMember(member);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task RemoveMember(Member member)
        {
            await _memberService.DeleteMember(member);
            CurrentBoard.Members.Remove(member);
            Members.Remove(member);
        }
    }
}
