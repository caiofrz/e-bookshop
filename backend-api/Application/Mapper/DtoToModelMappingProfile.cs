using AutoMapper;
using backend_api.Application.DTOs;
using backend_api.Domain.Models;

namespace backend_api.Application.Mapper;

public class DtoToModelMappingProfile : Profile
{
    public DtoToModelMappingProfile()
    { 
        #region book
        CreateMap<BookCreateDto, Book>().ReverseMap();
        CreateMap<BookDto, Book>().ReverseMap();
        #endregion

        #region sale
        CreateMap<RegisterSaleDto, Sale>().ReverseMap();
        CreateMap<RegisterSaleItemDto, SaleItem>().ReverseMap();
        CreateMap<SaleDto, Sale>().ReverseMap();
        #endregion
    }
}