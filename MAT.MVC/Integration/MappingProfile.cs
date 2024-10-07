using MAT.MVC.Integration.BackendApi.Models;
using MAT.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace MAT.MVC.Integration
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ResultPasajeDto, ReservaStandard>()
                .ForMember(dest => dest.PasajeId, opt => opt.MapFrom(src => GetGuid(src.PasajeId)))
                .ForMember(dest => dest.PasajeroId, opt => opt.MapFrom(src => GetGuid(src.PasajeroId)))
                .ForMember(dest => dest.ButacaId, opt => opt.MapFrom(src => GetGuid(src.ButacaId)))
                .ForMember(dest => dest.FechaReserva, opt => opt.MapFrom(src => src.FechaReserva == default ? (DateTime?)null : src.FechaReserva))
                .ForMember(dest => dest.FechaCompra, opt => opt.MapFrom(src => src.FechaCompra == default ? (DateTime?)null : src.FechaCompra))
                .ForMember(dest => dest.ViajeId, opt => opt.MapFrom(src => GetGuid(src.ViajeId)))
                .ForMember(dest => dest.FacturaId, opt => opt.MapFrom(src => GetGuid(src.FacturaId)))
                .ForMember(dest => dest.EstadoPasaje, opt => opt.MapFrom(src => src.EstadoPasaje))
                .ForMember(dest => dest.VoucherId, opt => opt.MapFrom(src => GetGuid(src.VoucherId)))
                .ForMember(dest => dest.PrecioId, opt => opt.MapFrom(src => GetGuid(src.PrecioId)))
                .ForMember(dest => dest.ButacaPiso, opt => opt.MapFrom(src => src.ButacaPiso))
                .ForMember(dest => dest.ButacaFila, opt => opt.MapFrom(src => src.ButacaFila))
                .ForMember(dest => dest.ButacaPosicion, opt => opt.MapFrom(src => src.ButacaPosicion))
                .ForMember(dest => dest.ButacaCodigoButaca, opt => opt.MapFrom(src => src.ButacaCodigoButaca))
                .ForMember(dest => dest.TransporteID, opt => opt.MapFrom(src => GetGuid(src.TransporteID)))
                .ForMember(dest => dest.TransporteNroCoche, opt => opt.MapFrom(src => src.TransporteNroCoche))
                .ForMember(dest => dest.PaqueteID, opt => opt.MapFrom(src => GetGuid(src.PaqueteID)))
                .ForMember(dest => dest.PasajeroNombre, opt => opt.MapFrom(src => src.PasajeroNombre))
                .ForMember(dest => dest.PasajeroApellido, opt => opt.MapFrom(src => src.PasajeroApellido))
                .ForMember(dest => dest.MonedaTipo, opt => opt.MapFrom(src => src.MonedaTipo))
                .ForMember(dest => dest.TransporteTipo, opt => opt.MapFrom(src => src.TransporteTipo));
        }

        private Guid GetGuid(Guid? id)
        {
            return id ?? Guid.Empty;
        }
    }
}