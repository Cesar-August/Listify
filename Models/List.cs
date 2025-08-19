using System.ComponentModel.DataAnnotations;
public class List
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public int UserId { get; set; }

    [Required]
    public User User { get; set; } = null!;

    public ICollection<Card> Cards { get; set; } = new List<Card>();
}