namespace BookLibrary.Application.Books.Dtos;

public class CreateBookDto
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public required byte[] Image { get; set; }

    public required List<int> Authors { get; set; }
}
