using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebTicTacToe.Services;

namespace WebTicTacToe.Pages;

public class IndexModel : PageModel
{
    private readonly GameService _svc;

    public IndexModel(GameService svc) => _svc = svc;

    // PROMPT: Ask Cursor to expose state to the page (board/status/scoreboard)
    // and handle handlers: OnGet(), OnPostNewGame(), OnPostMove(int row,int col), OnPostRematch().
}
