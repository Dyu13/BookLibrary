using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Domain;

public class BookEntity
{
    [Key]
    public int BookId { get; set; }

    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required byte[] Image { get; set; }

    public List<AuthorEntity>? Authors { get; set; }
}