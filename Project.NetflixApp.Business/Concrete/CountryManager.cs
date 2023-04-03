using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.CountryDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class CountryManager : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCountryDto> _createCountryDtoValidator;
        private readonly IValidator<UpdateCountryDto> _updateCountryDtoValidator;

        public CountryManager(ICountryRepository countryRepository, IMapper mapper, IValidator<CreateCountryDto> createCountryDtoValidator, IValidator<UpdateCountryDto> updateCountryDtoValidator)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _createCountryDtoValidator = createCountryDtoValidator;
            _updateCountryDtoValidator = updateCountryDtoValidator;
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _countryRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _countryRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetCountryDto>> GetAllAsync()
        {
            var entityData = await _countryRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetCountryDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetCountryDto> GetByIdAsync(int id)
        {
            var entityData = await _countryRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetCountryDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateCountryDto> InsertAsync(CreateCountryDto createCountryDto)
        {
            var validationResponse = _createCountryDtoValidator.Validate(createCountryDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Country>(createCountryDto);
                await _countryRepository.InsertAsync(mappingEntity);
                return createCountryDto;
            }
            return null;
        }

        public async Task<UpdateCountryDto> UpdateAsync(UpdateCountryDto updateCountryDto)
        {
            var validationResponse = _updateCountryDtoValidator.Validate(updateCountryDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Country>(updateCountryDto);
                await _countryRepository.UpdateAsync(mappingEntity);
                return updateCountryDto;
            }
            return null;
        }
    }
}
