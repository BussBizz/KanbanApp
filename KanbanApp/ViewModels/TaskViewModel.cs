using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Services;
using System.Collections.ObjectModel;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(CurrentTask), "currentTask")]
    [QueryProperty(nameof(Member), "currentMember")]
    [QueryProperty(nameof(CurrentBoard), "board")]
    public partial class TaskViewModel : ObservableObject
    {
        private readonly CommentsService _commentsService;
        private readonly TasksService _tasksService;

        [ObservableProperty] private KanbanTask _currentTask;
        [ObservableProperty] private Member _member;
        [ObservableProperty] private Comment _newComment;
        [ObservableProperty] private ObservableCollection<Comment> _comments;
        [ObservableProperty] private Board _currentBoard;
        
        public TaskViewModel(CommentsService commentsService, TasksService tasksService)
        {
            _newComment = new Comment();
            Comments = new ObservableCollection<Comment>();
            _commentsService = commentsService;
            _tasksService = tasksService;
        }

        [RelayCommand]
        async Task CreateComment()
        {
            if (NewComment.Content == null)
            {
                await Shell.Current.DisplayAlert("Fejl!", "Din kommentar må ikke være tom", "Ok");
                return;
            }
            try
            {
                NewComment.MemberId = Member.UserId;
                NewComment.KanbanTaskId = CurrentTask.Id;
                var newComment = await _commentsService.PostComment(NewComment);
                newComment.Member = Member;
                Comments.Add(newComment);

            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Fejl!", e.Message, "Ok");
            }
        }

        [RelayCommand]
        public async Task CompleteTask()
        {
            await _tasksService.CompleteTask(CurrentTask, Member);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task AssignTask()
        {
            var usernames = new List<string>();
            foreach (var member in CurrentBoard.Members)
            {
                usernames.Add(member.User.Name);
            }
            var username = await Shell.Current.DisplayActionSheet("Vælg ansvarlig", "Annuller", null, usernames.ToArray());
            var chosenMember = CurrentBoard.Members.FirstOrDefault(m => m.User.Name == username);

            await _tasksService.AssignTask(CurrentTask, chosenMember);

            CurrentTask.Assigned = chosenMember;
            CurrentTask.AssignedId = chosenMember.Id;
        }

        async partial void OnCurrentTaskChanged(KanbanTask value)
        {
            var result = await _commentsService.GetCommentsByTask(value.Id);
            foreach (var comment in result)
            {
                Comments.Add(comment);
            }
        }
    }
}
