using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Business.Helpers.Constans;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
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

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _countryRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _countryRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, CountryMessages.Deleted);
            }
            return new Response(ResponseType.NotFound, CountryMessages.NotDeleted);
        }

        public async Task<IDataResponse<IEnumerable<GetCountryDto>>> GetAllAsync()
        {
            var entityData = await _countryRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetCountryDto>>(entityData);
            return new DataResponse<IEnumerable<GetCountryDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetCountryDto>> GetByIdAsync(int id)
        {
            var entityData = await _countryRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetCountryDto>(entityData);
                return new DataResponse<GetCountryDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetCountryDto>(ResponseType.NotFound, $"{CountryMessages.NotFound}" + $"{id}");
        }

        public async Task<IResponse> InsertAsync(CreateCountryDto createCountryDto)
        {
            var validationResponse = _createCountryDtoValidator.Validate(createCountryDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Country>(createCountryDto);
                await _countryRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, CountryMessages.Created);
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateCountryDto updateCountryDto)
        {
            var oldData = await _countryRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateCountryDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateCountryDtoValidator.Validate(updateCountryDto);
                if (validationResponse.IsValid)
                {

                    var mappingEntity = _mapper.Map<Country>(updateCountryDto);
                    await _countryRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, CountryMessages.Updated);
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, $"{CountryMessages.NotUpdated}" + $"{oldData.Id}");
        }
    }
}
