﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model;
using SystemStock.Business.Model.Category;
using SystemStock.Business.Validation.Category;
using SystemStock.RelationalData;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryCreateValidator _categoryCreateValidator;
        private readonly IMapper _mapper;
        private readonly HttpContext _context;

        public CategoryService
            (
                ICategoryRepository categoryRepository,
                CategoryCreateValidator categoryCreateValidator,
                IMapper mapper,
                HttpContext context
            ) 
        {
            _categoryRepository = categoryRepository;
            _categoryCreateValidator = categoryCreateValidator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<BaseResponse<CategoryModel>> Create(CategoryModel model)
        {
            try
            {
                var result = new BaseResponse<CategoryModel>();
                result.Message = result.Validate(await _categoryCreateValidator.ValidateAsync(model));
                if (result.Message.Count() > 0)
                {
                    return result;
                }
                var user = _context.Session.GetString("sessionUser");
                var entity = new CategoryEntity()
                {
                    Name = model.Name,
                    Active = true,
                };

                entity = (await _categoryRepository.GetDbSetCategory().AddAsync(entity)).Entity;
                await _categoryRepository.SaveChanges();

                result.Data = _mapper.Map<CategoryModel>(entity);
                return result;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
