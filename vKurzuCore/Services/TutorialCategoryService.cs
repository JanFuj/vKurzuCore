using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using vKurzuCore.Helpers;
using vKurzuCore.Models;
using vKurzuCore.Repositories;
using vKurzuCore.Services.Comunication;
using vKurzuCore.Services.Contracts;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.Services
{
    public class TutorialCategoryService : ITutorialCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _loggedUserId;
        private readonly ClaimsPrincipal _loggedUser;
        private readonly IMapper _mapper;


        public TutorialCategoryService(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _loggedUserId = userManager.GetUserId(contextAccessor.HttpContext.User);
            _loggedUser = contextAccessor.HttpContext.User;
            _mapper = mapper;
        }

        public async Task<TutorialCategoryResponse> ApproveAsync(int id, bool approve)
        {
            try
            {
                var categoryToApprove = await FindByIdAsync(id);
                if (categoryToApprove == null) return null;

                categoryToApprove.Approved = approve;
                await _unitOfWork.SaveAsync();
                return new TutorialCategoryResponse(categoryToApprove);
            }
            catch (Exception ex)
            {
                return new TutorialCategoryResponse($"An error occurred when saving the category: {ex.Message}", "");
            }
        }

        public async Task<TutorialCategoryResponse> CreateAsync(TutorialCategory category)
        {
            try
            {
                category.OwnerId = _loggedUserId;
                _unitOfWork.TutorialCategories.Add(category);
                await _unitOfWork.SaveAsync();

                return new TutorialCategoryResponse(category);
            }
            catch (DbUpdateException sqlEx) when (sqlEx.InnerException.HResult == (-2146232060))
            {
                return new TutorialCategoryResponse($"Zadane url již existuje", "UrlTitle");
            }
            catch (Exception ex)
            {
                return new TutorialCategoryResponse($"An error occurred when saving the category: {ex.Message}", "");
            }
        }

        public async Task<TutorialCategoryResponse> DeleteAsync(int id)
        {
            try
            {
                var categoryToDelete = await FindByIdAsync(id);
                if (categoryToDelete == null)
                    return null;
                _unitOfWork.TutorialCategories.Remove(categoryToDelete);
                await _unitOfWork.SaveAsync();
                return new TutorialCategoryResponse(categoryToDelete);
            }
            catch (Exception ex)
            {
                return new TutorialCategoryResponse($"An error occurred when deleting the category: {ex.Message}", "");
            }
        }

        public async Task<TutorialCategory> FindByIdAsync(int id)
        {
            var category = await _unitOfWork.TutorialCategories.FindByIdAsync(id);
            if (_loggedUser.IsInRole(Constants.Roles.Lector))
            {
                if (category?.OwnerId != _loggedUserId)
                    category = null;
            }
            return category;
        }

        public async Task<IEnumerable<TutorialCategory>> GetAllAsync()
        {
            return await _unitOfWork.TutorialCategories.GetAllAsync();
        }

        public async Task<IEnumerable<TutorialCategory>> GetAllPublished()
        {
            return await _unitOfWork.TutorialCategories.GetPublishedTutorialCategories();
        }

        public async Task<TutorialCategoryResponse> UpdateAsync(TutorialCategoryDto categoryDto)
        {
            try
            {
                var categoryToUpdate = await FindByIdAsync(categoryDto.Id);
                if (categoryToUpdate == null)
                    return null;
                _mapper.Map(categoryDto, categoryToUpdate);
                await _unitOfWork.SaveAsync();
                return new TutorialCategoryResponse(categoryToUpdate);
            }
            catch (DbUpdateException sqlEx) when (sqlEx.InnerException.HResult == (-2146232060))
            {
                return new TutorialCategoryResponse($"Zadane url již existuje", "UrlTitle");
            }
            catch (Exception ex)
            {
                return new TutorialCategoryResponse($"An error occurred when updating the category: {ex.Message}", "");
            }
        }
    }
}
