using System.Net;
using AutoMapper;
using backend_api.Application.DTOs;
using backend_api.Application.Filters;
using backend_api.Application.Helpers;
using backend_api.Domain.Interfaces.Services;
using backend_api.Domain.Models;
using backend_api.UI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.UI.Controllers;

[ApiController]
[Route("api/sales")]
[ValidateModel]
[Consumes("application/json")]
[Produces("application/json")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public SalesController(ISaleService saleService, IMapper mapper)
    {
        _saleService = saleService;
        _mapper = mapper;
    }

    /// <summary>
    /// Registra uma nova venda.
    /// </summary>
    /// <response code="201">Sucesso ao registrar a venda.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Item não encontrado.</response>
    /// <response code="422">Estoque insuficiente.</response>
    /// <response code="500">Erro inesperado.</response>
    [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegisterSaleDto saleDto)
    {
        var sale = _mapper.Map<Sale>(saleDto);
        var newSale = await _saleService.RegisterSaleAsync(sale);

        var response = ApiResponseHelper.CriarRespostaSucesso((int)HttpStatusCode.Created,
                                                              "Venda registrada com sucesso.",
                                                              _mapper.Map<SaleDto>(newSale));
        return Created(nameof(Create), response);
    }
}