using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
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

        public async Task DeleteAsync(int id)
        {
            var data = await _typeEntityRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _typeEntityRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetTypeEntityDto>> GetAllAsync()
        {
            var entityData = await _typeEntityRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetTypeEntityDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetTypeEntityDto> GetByIdAsync(int id)
        {
            var entityData = await _typeEntityRepository.GetByFilterAsync(x => x.Id == id);
            if(entityData != null)
            {
                var mappingDto = _mapper.Map<GetTypeEntityDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateTypeEntityDto> InsertAsync(CreateTypeEntityDto createTypeEntityDto)
        {
            var validationResponse = _createTypeEntityDtoValidator.Validate(createTypeEntityDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<TypeEntity>(createTypeEntityDto);
                await _typeEntityRepository.InsertAsync(mappingEntity);
                return createTypeEntityDto;
            }
            return null;
        }

        public async Task<UpdateTypeEntityDto> UpdateAsync(UpdateTypeEntityDto updateTypeEntityDto)
        {
            var validationResponse = _updateTypeEntityDtoValidator.Validate(updateTypeEntityDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<TypeEntity>(updateTypeEntityDto);
                await _typeEntityRepository.UpdateAsync(mappingEntity);
                return updateTypeEntityDto;
            }
            return null;
        }
    }
}
