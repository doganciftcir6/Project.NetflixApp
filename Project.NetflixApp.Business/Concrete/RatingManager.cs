using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.RatingDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class RatingManager : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateRatingDto> _createRatingDtoValidator;
        private readonly IValidator<UpdateRatingDto> _updateRatingDtoValidator;

        public RatingManager(IRatingRepository ratingRepository, IMapper mapper, IValidator<CreateRatingDto> createRatingDtoValidator, IValidator<UpdateRatingDto> updateRatingDtoValidator)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
            _createRatingDtoValidator = createRatingDtoValidator;
            _updateRatingDtoValidator = updateRatingDtoValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _ratingRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _ratingRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, "The rating was successfully deleted");
            }
            return new Response(ResponseType.NotFound, "The rating parameter could not be deleted because the rating could not be found.");
        }

        public async Task<IDataResponse<IEnumerable<GetRatingDto>>> GetAllAsync()
        {
            var entityData = await _ratingRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetRatingDto>>(entityData);
            return new DataResponse<IEnumerable<GetRatingDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetRatingDto>> GetByIdAsync(int id)
        {
            var entityData = await _ratingRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetRatingDto>(entityData);
                return new DataResponse<GetRatingDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetRatingDto>(ResponseType.NotFound, $"The related rating could not be found. Rating Id:");
        }

        public async Task<IResponse> InsertAsync(CreateRatingDto createRatingDto)
        {
            var validationRules = _createRatingDtoValidator.Validate(createRatingDto);
            if (validationRules.IsValid)
            {
                var mappingEntity = _mapper.Map<Rating>(createRatingDto);
                await _ratingRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, "The rating adding process has been successfully completed.");
            }
            return new Response(ResponseType.ValidationError, validationRules.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateRatingDto updateRatingDto)
        {
            var oldData = await _ratingRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateRatingDto.Id);
            if (oldData != null)
            {
                var validationRules = _updateRatingDtoValidator.Validate(updateRatingDto);
                if (validationRules.IsValid)
                {
                    var mappingEntity = _mapper.Map<Rating>(updateRatingDto);
                    await _ratingRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, "The rating updating process has been successfully completed.");
                }
                return new Response(ResponseType.ValidationError, validationRules.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, "The related rating could not be found. So the update process could not be completed. Rating Id:");
        }
    }
}
