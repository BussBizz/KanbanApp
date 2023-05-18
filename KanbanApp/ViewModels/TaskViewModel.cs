using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Services;
using System.Collections.ObjectModel;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(CurrentTask), "currentTask")]
    [QueryProperty(nameof(Member), "currentMember")]
    public partial class TaskViewModel : ObservableObject
    {
        [ObservableProperty] private KanbanTask _currentTask;
        [ObservableProperty] private Member _member;
        [ObservableProperty] private Comment _newComment;
        [ObservableProperty] private string _creator;
        [ObservableProperty] private string _assigned;
        [ObservableProperty] private string _description;
        [ObservableProperty] private string _deadline;
        [ObservableProperty] private ObservableCollection<Comment> _comments;
        private readonly CommentsService _commentsService;
        public TaskViewModel(CommentsService commentsService)
        {
            _newComment = new Comment();
            _comments = new ObservableCollection<Comment>();
            _commentsService = commentsService;

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
        async partial void OnCurrentTaskChanged(KanbanTask value)
        {
            Creator = $"Oprettet af: {value.Creator.User.Name}";
            Assigned = $"Ansvarlig: {value.Assigned?.User.Name}";
            Description = $"Beskrivelse:\n{value.Description}";
            Deadline = $"Deadline: {value.Deadline}";
            var result = await _commentsService.GetCommentsByTask(value.Id);
            foreach (var comment in result)
            {
                Comments.Add(comment);
            }

        }

    }
}
