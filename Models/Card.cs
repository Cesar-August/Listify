using System.ComponentModel.DataAnnotations;
public class Card
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public int ListId { get; set; }

    [Required]
    public List List { get; set; } = null!;
}