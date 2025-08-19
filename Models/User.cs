using System.ComponentModel.DataAnnotations;
public class User
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Passwordhash { get; set; } = string.Empty;

    public ICollection<List> Lists { get; set; } = new List<List>();
    public string Password { get; internal set; }
}