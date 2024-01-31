using AutoMapper;
using E_Commerce.Services.CouponAPI.Data;
using E_Commerce.Services.CouponAPI.Models;
using E_Commerce.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private ResponseDto  _responseDto;
        private IMapper _mapper;
        public CouponController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
            this._responseDto = new ResponseDto();
        }

        public IMapper Mapper { get; }

        //Get All Coupon Codes Available in Db
        [HttpGet]
        public ResponseDto GetAllCoupons()
        {
            try
            {
                IEnumerable<Coupon> couponList = _applicationDbContext.Coupons.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(couponList);
               
            }
            catch (Exception ex) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        //Get Coupon Codes from Db which matches the Id;
        [HttpGet]
        [Route("GetCouponByCode/{id:int}")]
        public ResponseDto GetCouponById(int id)
        {
            try
            { 
                //Conversion Coupon Domain Model to Coupon Dto using AutoMapper
                Coupon couponObj = _applicationDbContext.Coupons.First(u=>u.CouponId == id);
                //Manually mapping the properties of one object (couponObj) to another object (CouponDto). 
                _responseDto.Result = _mapper.Map<CouponDto>(couponObj);
               
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        //Get Coupon Codes from Db which matches the CouponCode;
        [HttpGet]
        [Route("GetCouponByCode/{couponCode}")]
        public ResponseDto GetCouponByCode(string couponCode)
        {
            try
            {
                //Conversion Coupon Domain Model to Coupon Dto using AutoMapper
                Coupon couponObj = _applicationDbContext.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == couponCode.ToLower());
                if(couponObj == null)
                {
                    _responseDto.IsSuccess = false;
                }
                //Manually mapping the properties of one object (couponObj) to another object (CouponDto). 
                _responseDto.Result = _mapper.Map<CouponDto>(couponObj);

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        //Create New Coupon
        [HttpPost]
        public ResponseDto PostCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                //converting couponDto To Coupon Domain Model
                Coupon couponObj = _mapper.Map<Coupon>(couponDto);
                _applicationDbContext.Coupons.Add(couponObj);
                _applicationDbContext.SaveChanges();

                //return Coupon Dto --> Converting CouponObj (DomainModel) into CouponDto (DTOModel)
                _responseDto.Result = _mapper.Map<CouponDto>(couponObj);
            }
            catch (Exception ex) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        //Edit Existing Coupon
        [HttpPut]
        public ResponseDto UpdateCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                //converting couponDto To Coupon Domain Model
                Coupon couponObj = _mapper.Map<Coupon>(couponDto);
                _applicationDbContext.Coupons.Update(couponObj);
                _applicationDbContext.SaveChanges();

                //return Coupon Dto --> Converting CouponObj (DomainModel) into CouponDto (DTOModel)
                _responseDto.Result = _mapper.Map<CouponDto>(couponObj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        //Delete A Particular Coupon By Id
        [HttpDelete]
        [Route("DeleteCouponById/{id:int}")]
        public ResponseDto DeleteCoupon(int id)
        {
            try
            {
                //converting couponDto To Coupon Domain Model
                Coupon couponObj = _applicationDbContext.Coupons.First(u=>u.CouponId == id);
                _applicationDbContext.Coupons.Remove(couponObj);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
