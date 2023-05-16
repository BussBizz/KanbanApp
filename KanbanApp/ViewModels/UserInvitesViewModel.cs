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
    [QueryProperty(nameof(InviteList), "invites")]
    [QueryProperty(nameof(MembershipList), "memberships")]
    public partial class UserInvitesViewModel : ObservableObject
    {
        private readonly MemberService _memberService;
        private readonly InviteService _inviteService;
        [ObservableProperty] private ObservableCollection<Invite> _inviteList;
        [ObservableProperty] private ObservableCollection<Member> _membershipList;
        [ObservableProperty] private string _inviteCode;

        public UserInvitesViewModel(MemberService memberService, InviteService inviteService)
        {
            _memberService = memberService;
            _inviteService = inviteService;
            InviteList = new ObservableCollection<Invite>();
            MembershipList = new ObservableCollection<Member>();
        }

        [RelayCommand]
        public async Task AcceptInvite(Invite invite)
        {
            var newMembership = await _memberService.MembershipFromInvite(invite);
            InviteList.Remove(invite);
            await _inviteService.DeleteInvite(invite);
            MembershipList.Add(newMembership);
        }

        [RelayCommand]
        public async Task TryCode()
        {
            var invite = await _inviteService.GetInviteByCode(InviteCode);
            if (invite != null)
            {
                var userId = await SecureStorage.GetAsync("userId");
                invite.UserId = int.Parse(userId);
                await AcceptInvite(invite);
            }
            else
            {
                await Shell.Current.DisplayAlert("Fejl", $"Ingen invitation med koden: {InviteCode}", "Ok");
                InviteCode = string.Empty;
            }
        }

        partial void OnInviteCodeChanged(string value)
        {
            InviteCode = value.ToUpper();
        }
    }
}
