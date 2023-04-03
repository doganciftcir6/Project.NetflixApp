using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.CategoryDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;
        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper, IValidator<CreateCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryValidator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _categoryRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _categoryRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
        {
            var entity = await _categoryRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetCategoryDto>>(entity);
            return mappingDto;
        }

        public async Task<GetCategoryDto> GetByIdAsync(int id)
        {
            var entity = await _categoryRepository.GetByFilterAsync(x => x.Id == id);
            if (entity != null)
            {
                var mappingDto = _mapper.Map<GetCategoryDto>(entity);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateCategoryDto> InsertAsync(CreateCategoryDto createCategoryDto)
        {
            var validationResponse = _createCategoryValidator.Validate(createCategoryDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Category>(createCategoryDto);
                await _categoryRepository.InsertAsync(mappingEntity);
                return createCategoryDto;
            }
            return null;
        }

        public async Task<UpdateCategoryDto> UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            var validationResponse = _updateCategoryValidator.Validate(updateCategoryDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Category>(updateCategoryDto);
                await _categoryRepository.UpdateAsync(mappingEntity);
                return updateCategoryDto;
            }
            return null;
        }
    }
}
