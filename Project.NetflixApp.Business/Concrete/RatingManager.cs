using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
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

        public async Task DeleteAsync(int id)
        {
            var data = await _ratingRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _ratingRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetRatingDto>> GetAllAsync()
        {
            var entityData = await _ratingRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetRatingDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetRatingDto> GetByIdAsync(int id)
        {
            var entityData = await _ratingRepository.GetByFilterAsync(x => x.Id == id);
            if(entityData != null)
            {
                var mappingDto =_mapper.Map<GetRatingDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateRatingDto> InsertAsync(CreateRatingDto createRatingDto)
        {
            var validationRules = _createRatingDtoValidator.Validate(createRatingDto);
            if (validationRules.IsValid)
            {
                var mappingEntity = _mapper.Map<Rating>(createRatingDto);
                await _ratingRepository.InsertAsync(mappingEntity);
                return createRatingDto;
            }
            return null;
        }

        public async Task<UpdateRatingDto> UpdateAsync(UpdateRatingDto updateRatingDto)
        {
            var validationRules = _updateRatingDtoValidator.Validate(updateRatingDto);
            if (validationRules.IsValid)
            {
                var mappingEntity = _mapper.Map<Rating>(updateRatingDto);
                await _ratingRepository.UpdateAsync(mappingEntity);
                return updateRatingDto;
            }
            return null;
        }
    }
}
