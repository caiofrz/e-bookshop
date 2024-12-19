using System.Net;
using AutoMapper;
using backend_api.Application.DTOs;
using backend_api.Application.Filters;
using backend_api.Application.Helpers;
using backend_api.Domain.Interfaces.Services;
using backend_api.Domain.Models;
using backend_api.Domain.Models.Queries;
using backend_api.UI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.UI.Controllers;

[ApiController]
[Route("api/books")]
[ValidateModel]
[Consumes("application/json")]
[Produces("application/json")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BooksController(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retorna uma lista dos livros cadastrados.
    /// </summary>
    /// <response code="200">Sucesso ao listar os registros ou mensagem indicando que nenhum registro foi encontrado para esta pesquisa.</response>
    /// <response code="500">Erro inesperado.</response>
    [ProducesResponseType(typeof(ApiResponse<BooksResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BooksQueryParams queryParams)
    {
        var books = await _bookService.GetAllAsync(queryParams);
        var booksList = books?.Select(_mapper.Map<BookDto>);
        var booksDto = new BooksResponseDto(booksList, queryParams.Page, queryParams.PageSize);

        var response = ApiResponseHelper.CriarRespostaSucesso((int)HttpStatusCode.OK,
                                                              "Livros recuperados com sucesso",
                                                              booksDto);
        return Ok(response);
    }

    /// <summary>
    /// Recupera um livro pelo id.
    /// </summary>
    /// <response code="200">Sucesso ao recuperar o registro.</response>
    /// <response code="404">Não encontrado.</response>
    /// <response code="500">Erro inesperado.</response>
    [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        var bookDto = _mapper.Map<BookDto>(book);
        var response = ApiResponseHelper.CriarRespostaSucesso((int)HttpStatusCode.OK,
                                                              "Livro recuperado com sucesso",
                                                              bookDto);
        return Ok(response);
    }

    /// <summary>
    /// Cadastra um novo livro.
    /// </summary>
    /// <response code="201">Sucesso ao cadastrar o livro.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="409">Dados inválidos.</response>
    /// <response code="500">Erro inesperado.</response>
    [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookCreateDto dto)
    {
        var bookModel = _mapper.Map<Book>(dto);
        var newBook = await _bookService.CreateAsync(bookModel);
        var response = ApiResponseHelper.CriarRespostaSucesso((int)HttpStatusCode.Created,
                                                      "Livro cadastrado com sucesso",
                                                      _mapper.Map<BookDto>(newBook));

        return Created(nameof(Create), response);
    }

    /// <summary>
    /// Atualiza os dados de um livro.
    /// </summary>
    /// <response code="201">Sucesso ao atualizar  o livro.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Livro não encontrado.</response>
    /// <response code="409">Dados inválidos.</response>
    /// <response code="500">Erro inesperado.</response>
    [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookCreateDto dto)
    {
        var book = _mapper.Map<Book>(dto);
        var updatedBook = await _bookService.UpdateAsync(id, book);

        var response = ApiResponseHelper.CriarRespostaSucesso((int)HttpStatusCode.Created,
                                                      "Livro editado com sucesso",
                                                      _mapper.Map<BookDto>(updatedBook));
        return Created(nameof(Update), response);
    }

    /// <summary>
    /// Deleta um livro.
    /// </summary>
    /// <response code="204">Sucesso ao deletar o livro.</response>
    /// <response code="404">Livro não encontrado.</response>
    /// <response code="500">Erro inesperado.</response>
    [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.DeleteAsync(id);
        return NoContent();
    }
}