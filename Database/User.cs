using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(255)] // Adjust the maximum length as needed
    public string Username { get; set; }

    [Required]
    [MaxLength(255)] // Adjust the maximum length as needed
    public string Password { get; set; }
}
