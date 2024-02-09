using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Domain;

public class AuthorEntity
{
    [Key]
    public int AuthorId { get; set; }

    [Required]
    public required string Name { get; set; }

    public List<BookEntity>? Books { get; set; }
}
