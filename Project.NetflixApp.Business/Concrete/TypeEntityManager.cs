using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Business.Helpers.Constans;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.TypeEntityDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class TypeEntityManager : ITypeEntityService
    {
        private readonly ITypeEntityRepository _typeEntityRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTypeEntityDto> _createTypeEntityDtoValidator;
        private readonly IValidator<UpdateTypeEntityDto> _updateTypeEntityDtoValidator;

        public TypeEntityManager(ITypeEntityRepository typeEntityRepository, IMapper mapper, IValidator<CreateTypeEntityDto> createTypeEntityDtoValidator, IValidator<UpdateTypeEntityDto> updateTypeEntityDtoValidator)
        {
            _typeEntityRepository = typeEntityRepository;
            _mapper = mapper;
            _createTypeEntityDtoValidator = createTypeEntityDtoValidator;
            _updateTypeEntityDtoValidator = updateTypeEntityDtoValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _typeEntityRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _typeEntityRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, TypeEntityMessages.Deleted);
            }
            return new Response(ResponseType.NotFound, TypeEntityMessages.NotDeleted);
        }

        public async Task<IDataResponse<IEnumerable<GetTypeEntityDto>>> GetAllAsync()
        {
            var entityData = await _typeEntityRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetTypeEntityDto>>(entityData);
            return new DataResponse<IEnumerable<GetTypeEntityDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetTypeEntityDto>> GetByIdAsync(int id)
        {
            var entityData = await _typeEntityRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetTypeEntityDto>(entityData);
                return new DataResponse<GetTypeEntityDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetTypeEntityDto>(ResponseType.NotFound, $"{TypeEntityMessages.NotFound}" + $"{id}");
        }

        public async Task<IResponse> InsertAsync(CreateTypeEntityDto createTypeEntityDto)
        {
            var validationResponse = _createTypeEntityDtoValidator.Validate(createTypeEntityDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<TypeEntity>(createTypeEntityDto);
                await _typeEntityRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, TypeEntityMessages.Created);
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateTypeEntityDto updateTypeEntityDto)
        {
            var oldData = await _typeEntityRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateTypeEntityDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateTypeEntityDtoValidator.Validate(updateTypeEntityDto);
                if (validationResponse.IsValid)
                {
                    var mappingEntity = _mapper.Map<TypeEntity>(updateTypeEntityDto);
                    await _typeEntityRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, TypeEntityMessages.Updated);
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, $"{TypeEntityMessages.NotUpdated}" + $"{oldData.Id}");
        }
    }
}
