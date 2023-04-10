using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
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

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _categoryRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _categoryRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, "The category was successfully deleted");
            }
            return new Response(ResponseType.NotFound, "The category parameter could not be deleted because the category could not be found.");
        }

        public async Task<IDataResponse<IEnumerable<GetCategoryDto>>> GetAllAsync()
        {
            var entity = await _categoryRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetCategoryDto>>(entity);
            return new DataResponse<IEnumerable<GetCategoryDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetCategoryDto>> GetByIdAsync(int id)
        {
            var entity = await _categoryRepository.GetByFilterAsync(x => x.Id == id);
            if (entity != null)
            {
                var mappingDto = _mapper.Map<GetCategoryDto>(entity);
                return new DataResponse<GetCategoryDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetCategoryDto>(ResponseType.NotFound, $"The related category could not be found. Category Id:");
        }

        public async Task<IResponse> InsertAsync(CreateCategoryDto createCategoryDto)
        {
            var validationResponse = _createCategoryValidator.Validate(createCategoryDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Category>(createCategoryDto);
                await _categoryRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, "The category adding process has been successfully completed.");
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            //bu projede update metotunu repository içerisinde tracking özelliğini kullanan bir şekilde yaptım dolayısıyla tracking özelliğini UpdateAsync yapacağı için 2 kere tracking yapmak hataya sebebiyet verir. Bu nedenle AsNoTracking özelliği olan bir kayıt çeken metotu kullanarak notfound durumunu değerlendireceğim.
            var oldData = await _categoryRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateCategoryDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateCategoryValidator.Validate(updateCategoryDto);
                if (validationResponse.IsValid)
                {
                    var mappingEntity = _mapper.Map<Category>(updateCategoryDto);
                    await _categoryRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, "The category updating process has been successfully completed.");
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, "The related category could not be found. So the update process could not be completed. Category Id:");
        }
    }
}

