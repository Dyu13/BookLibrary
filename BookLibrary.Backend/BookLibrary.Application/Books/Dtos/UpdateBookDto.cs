namespace BookLibrary.Application.Books.Dtos;

public class UpdateBookDto
{
    public int BookId { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required byte[] Image { get; set; }

    // TODO: maybe let update the Authors as well
}
