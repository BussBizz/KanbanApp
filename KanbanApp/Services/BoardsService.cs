using KanbanApp.Models;
using KantineApp.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.Services
{
    public class BoardsService : BaseService
    {
        private string _apiPath = "/api/Boards";

        public async Task<Board[]> GetBoards()
        {
            var result = await GetData<Board[]>(_apiPath);
            return result;
        }

        public async Task<Board> GetBoard(int id)
        {
            var path = $"{_apiPath}/{id}";
            var result = await GetData<Board>(path);
            return result;
        }

        public async Task<Board> PostBoard(Board board)
        {
            var result = await PostData(board, _apiPath);
            return result;
        }
    }
}
