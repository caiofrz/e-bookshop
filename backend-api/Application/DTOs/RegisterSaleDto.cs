using System.ComponentModel.DataAnnotations;

namespace backend_api.Application.DTOs;

public class RegisterSaleDto
{
    [Required]
    public List<RegisterSaleItemDto> Items { get; set; }
}

public class RegisterSaleItemDto
{
    [Required(ErrorMessage = "O id do livro é obrigatorio")]
    public int BookId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade mínima é 1")]
    public int Quantity { get; set; }
}