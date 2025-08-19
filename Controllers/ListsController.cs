using Microsoft.AspNetCore.Mvc;

namespace TodoListApp.Controllers
{
    public class ListsController : Controller
    {
        [HttpPost]
        public IActionResult RenameCard(int cardId, string newTitle)
        {
            // TODO: Buscar o card no banco de dados pelo cardId e alterar o título
            // Exemplo:
            // var card = _context.Cards.FirstOrDefault(c => c.Id == cardId);
            // if (card != null) { card.Title = newTitle; _context.SaveChanges(); }

            return Ok(new { success = true });
        }

        [HttpPost]
        public IActionResult DeleteCard(int cardId)
        {
            // TODO: Buscar o card no banco de dados pelo cardId e removê-lo
            // Exemplo:
            // var card = _context.Cards.FirstOrDefault(c => c.Id == cardId);
            // if (card != null) { _context.Cards.Remove(card); _context.SaveChanges(); }

            return Ok(new { success = true });
        }

        [HttpPost]
        public IActionResult MoveCard(int cardId, int targetListId)
        {
            // TODO: Buscar o card e mudar o campo de ListaId (ou ListId) dele
            // Exemplo:
            // var card = _context.Cards.FirstOrDefault(c => c.Id == cardId);
            // if (card != null) { card.ListId = targetListId; _context.SaveChanges(); }

            return Ok(new { success = true });
        }
    }
}
