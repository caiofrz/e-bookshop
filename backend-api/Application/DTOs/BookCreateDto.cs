using System.ComponentModel.DataAnnotations;

namespace backend_api.Application.DTOs;

public class BookCreateDto
{
    [Required(ErrorMessage = "O ISBN é obrigatório.")]
    [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN deve conter entre 10 e 13 caracteres.")]
    public string ISBN { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "O título do livro deve conter entre {1} e {2} caracteres.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "O(s) autor(res) do livro deve(m) ser informado(s)")]
    public List<string> Authors { get; set; }

    [Required(ErrorMessage = "A categoria deve ser informada.")]
    public string Category { get; set; }

    [Required(ErrorMessage = "O preço do livro deve ser informado.")]
    [Range(0.01, 10000.00, ErrorMessage = "O preço deve ser um valor entre {1} e {2} reais")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "A quantidade em estoque deve ser informada.")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade adiconada ao estoque deve ser um valor inteiro positivo.")]
    public int StockQuantity { get; set; }
};