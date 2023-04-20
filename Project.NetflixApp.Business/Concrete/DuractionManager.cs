using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Business.Helpers.Constans;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.DuractionDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class DuractionManager : IDuractionService
    {
        private readonly IDuractionRepository _duractionRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDuractionDto> _createDuractionValidator;
        private readonly IValidator<UpdateDuractionDto> _updateDuractionValidator;

        public DuractionManager(IDuractionRepository duractionRepository, IMapper mapper, IValidator<CreateDuractionDto> createDuractionValidator, IValidator<UpdateDuractionDto> updateDuractionValidator)
        {
            _duractionRepository = duractionRepository;
            _mapper = mapper;
            _createDuractionValidator = createDuractionValidator;
            _updateDuractionValidator = updateDuractionValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _duractionRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _duractionRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, DuractionMessages.Deleted);
            }
            return new Response(ResponseType.NotFound, DuractionMessages.NotDeleted);
        }

        public async Task<IDataResponse<IEnumerable<GetDuractionDto>>> GetAllAsync()
        {
            var entityData = await _duractionRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetDuractionDto>>(entityData);
            return new DataResponse<IEnumerable<GetDuractionDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetDuractionDto>> GetByIdAsync(int id)
        {
            var entityData = await _duractionRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetDuractionDto>(entityData);
                return new DataResponse<GetDuractionDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetDuractionDto>(ResponseType.NotFound, $"{DuractionMessages.NotFound}" + $"{id}");
        }

        public async Task<IResponse> InsertAsync(CreateDuractionDto createDuractionDto)
        {
            var validationResponse = _createDuractionValidator.Validate(createDuractionDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Duraction>(createDuractionDto);
                await _duractionRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, DuractionMessages.Created);
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateDuractionDto updateDuractionDto)
        {
            var oldData = await _duractionRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateDuractionDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateDuractionValidator.Validate(updateDuractionDto);
                if (validationResponse.IsValid)
                {
                    var mappingEntity = _mapper.Map<Duraction>(updateDuractionDto);
                    await _duractionRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, DuractionMessages.Updated);
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, $"{DuractionMessages.NotUpdated}" + $"{oldData.Id}");
        }
    }
}
