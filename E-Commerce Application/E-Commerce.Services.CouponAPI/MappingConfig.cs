using AutoMapper;
using E_Commerce.Services.CouponAPI.Models;
using E_Commerce.Services.CouponAPI.Models.DTO;

namespace E_Commerce.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps() 
        {
            var mappongConfig = new MapperConfiguration(config =>
            {
                //Mapping CouponDto to Coupon
                config.CreateMap<CouponDto, Coupon>();
                //Mapping Coupon to Coupon Dto
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappongConfig;
        }
    }
}
