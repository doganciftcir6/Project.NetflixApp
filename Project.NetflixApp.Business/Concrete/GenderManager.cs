using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.GenderDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class GenderManager : IGenderService
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateGenderDto> _createGenderDtoValidator;
        private readonly IValidator<UpdateGenderDto> _updateGenderDtoValidator;

        public GenderManager(IGenderRepository genderRepository, IMapper mapper, IValidator<CreateGenderDto> createGenderDtoValidator, IValidator<UpdateGenderDto> updateGenderDtoValidator)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
            _createGenderDtoValidator = createGenderDtoValidator;
            _updateGenderDtoValidator = updateGenderDtoValidator;
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _genderRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _genderRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetGenderDto>> GetAllAsync()
        {
            var entityData = await _genderRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetGenderDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetGenderDto> GetByIdAsync(int id)
        {
            var entityData = await _genderRepository.GetByFilterAsync(x => x.Id == id);
            if(entityData != null)
            {
                var mappingDto = _mapper.Map<GetGenderDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateGenderDto> InsertAsync(CreateGenderDto createGenderDto)
        {
            var validationReponse = _createGenderDtoValidator.Validate(createGenderDto);
            if (validationReponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Gender>(validationReponse);
                await _genderRepository.InsertAsync(mappingEntity);
                return createGenderDto;
            }
            return null;
        }

        public async Task<UpdateGenderDto> UpdateAsync(UpdateGenderDto updateGenderDto)
        {
            var validationResponse = _updateGenderDtoValidator.Validate(updateGenderDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Gender>(validationResponse);
                await _genderRepository.UpdateAsync(mappingEntity);
                return updateGenderDto;
            }
            return null;
        }
    }
}
