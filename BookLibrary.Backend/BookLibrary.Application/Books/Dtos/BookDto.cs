namespace BookLibrary.Application.Books.Dtos;

public class BookDto : UpdateBookDto
{
    public required List<int> AuthorIds { get; set; }

    public required List<string> Authors { get; set; }
}
