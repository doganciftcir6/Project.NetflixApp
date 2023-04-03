using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
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

        public async Task DeleteAsync(int id)
        {
            var data = await _duractionRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _duractionRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetDuractionDto>> GetAllAsync()
        {
            var entityData = await _duractionRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetDuractionDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetDuractionDto> GetByIdAsync(int id)
        {
            var entityData = await _duractionRepository.GetByFilterAsync(x => x.Id == id);
            if(entityData != null)
            {
                var mappingDto = _mapper.Map<GetDuractionDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateDuractionDto> InsertAsync(CreateDuractionDto createDuractionDto)
        {
            var validationResponse = _createDuractionValidator.Validate(createDuractionDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Duraction>(createDuractionDto);
                await _duractionRepository.InsertAsync(mappingEntity);
                return createDuractionDto;
            }
            return null;
        }

        public async Task<UpdateDuractionDto> UpdateAsync(UpdateDuractionDto updateDuractionDto)
        {
            var validationResponse = _updateDuractionValidator.Validate(updateDuractionDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Duraction>(updateDuractionDto);
                await _duractionRepository.UpdateAsync(mappingEntity);
                return updateDuractionDto;
            }
            return null;
        }
    }
}
