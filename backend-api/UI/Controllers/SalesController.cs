using System.Net;
using AutoMapper;
using backend_api.Application.DTOs;
using backend_api.Application.Helpers;
using backend_api.Domain.Interfaces.Services;
using backend_api.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.UI.Controllers;

[ApiController]
[Route("api/sales")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public SalesController(ISaleService saleService, IMapper mapper)
    {
        _saleService = saleService;
        _mapper = mapper;
    }

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