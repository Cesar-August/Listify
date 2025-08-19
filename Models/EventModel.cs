using System.ComponentModel.DataAnnotations;
namespace TodoListApp.Models
{ }
public class EventModel
{
    public DateTime Date { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;
}
