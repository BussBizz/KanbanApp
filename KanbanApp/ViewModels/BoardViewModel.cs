using CommunityToolkit.Mvvm.ComponentModel;
using KanbanApp.Models;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(CurrentBoard), "board")]
    public partial class BoardViewModel : ObservableObject
    {
        [ObservableProperty]
        private Board _currentBoard;
    }
}
