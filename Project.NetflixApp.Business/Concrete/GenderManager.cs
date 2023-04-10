using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
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

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _genderRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _genderRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, "The gender was successfully deleted");
            }
            return new Response(ResponseType.NotFound, "The gender parameter could not be deleted because the gender could not be found.");
        }

        public async Task<IDataResponse<IEnumerable<GetGenderDto>>> GetAllAsync()
        {
            var entityData = await _genderRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetGenderDto>>(entityData);
            return new DataResponse<IEnumerable<GetGenderDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetGenderDto>> GetByIdAsync(int id)
        {
            var entityData = await _genderRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetGenderDto>(entityData);
                return new DataResponse<GetGenderDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetGenderDto>(ResponseType.NotFound, $"The related gender could not be found. Gender Id:");
        }

        public async Task<IResponse> InsertAsync(CreateGenderDto createGenderDto)
        {
            var validationReponse = _createGenderDtoValidator.Validate(createGenderDto);
            if (validationReponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Gender>(validationReponse);
                await _genderRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, "The gender adding process has been successfully completed.");
            }
            return new Response(ResponseType.ValidationError, validationReponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateGenderDto updateGenderDto)
        {
            var oldData = await _genderRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateGenderDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateGenderDtoValidator.Validate(updateGenderDto);
                if (validationResponse.IsValid)
                {
                    var mappingEntity = _mapper.Map<Gender>(validationResponse);
                    await _genderRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, "The gender updating process has been successfully completed.");
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, "The related gender could not be found. So the update process could not be completed. Gender Id:");
        }
    }
}
