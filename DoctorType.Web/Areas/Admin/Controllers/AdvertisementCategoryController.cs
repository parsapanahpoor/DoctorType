using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.ViewModels.Advertisement.AdvertisementCategory;
using DoctorType.Web.HttpManager;
using Microsoft.AspNetCore.Mvc;

namespace DoctorType.Web.Areas.Admin.Controllers
{
    public class AdvertisementCategoryController : AdminBaseController
    {
        #region Ctor

        private readonly IAdvertisementCategoryService _adsCategory;

        public AdvertisementCategoryController(IAdvertisementCategoryService adsCategory)
        {
            _adsCategory = adsCategory;
        }

        #endregion

        #region List Of Main Catgeories

        public async Task<IActionResult> Index()
        {
            return View(await _adsCategory.FilterAdsCategory());
        }

        #endregion

        #region List Of Sub Categories

        public async Task<IActionResult> FilterSubCategories(ulong ParentId)
        {
            return View(await _adsCategory.FilterChildAdsCategory(ParentId));
        }

        #endregion

        #region Create Category

        public async Task<IActionResult> CreateCategory(ulong? ParentId)
        {
            if (ParentId != null)
            {
                var category = await _adsCategory.GetAdvertisementCategoryById(ParentId.Value);
                if (category == null) return NotFound();
                ViewBag.ParentId = ParentId;
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(AdvertisementCategoryViewModel Categeory)
        {
            if (Categeory.ParentId != null)
            {
                var getCategory = _adsCategory.GetAdvertisementCategoryById(Categeory.ParentId.Value);
                if (getCategory.Result == null) return NotFound();
            }
            if (ModelState.IsValid)
            {
                var result = await _adsCategory.AddAdsCategory(Categeory);

                switch (result)
                {
                    case CreateAdsCategoryResult.Faild:
                        TempData[ErrorMessage] = "ثبت با خطا مواجه شده است  ";
                        break;
                    case CreateAdsCategoryResult.CategoryNotFound:
                        return NotFound();
                    case CreateAdsCategoryResult.Success:
                        TempData[SuccessMessage] = "ثبت  با موفقیت انجام شد";
                        return RedirectToAction(nameof(Index));
                }

            }
            return View(Categeory);
        }
        #endregion

        #region Edit Category

        public async Task<IActionResult> Edit(ulong id)
        {
            var category = await _adsCategory.GetAdvertisementCategoryById(id);
            if (category == null) return NotFound();
            var model = await _adsCategory.SetAdsCAtegoryViewModel(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdvertisementCategoryViewModel cat)
        {
            if (cat.ParentId != null)
            {
                var getCategoryByParentId = await _adsCategory.GetAdvertisementCategoryById(cat.ParentId.Value);
                if (getCategoryByParentId == null) return NotFound();
            }

            if (cat.CatgeoryId != null)
            {
                var getCategoryById = await _adsCategory.GetAdvertisementCategoryById(cat.CatgeoryId.Value);
                if (getCategoryById == null) return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _adsCategory.EditAdsCategory(cat);

                switch (result)
                {

                    case CreateAdsCategoryResult.Faild:
                        TempData[ErrorMessage] = "ویرایش با خطا رویرو شده است  ";
                        break;
                    case CreateAdsCategoryResult.Success:
                        TempData[SuccessMessage] = "ویرایش  با موفقیت انجام شد";
                        if (cat.ParentId != null)
                        {
                            //return Redirect("/Admin/AdvertisementCategory/Index?ParentId=" + cat.ParentId);
                            return RedirectToAction("FilterSubCategories", "AdvertisementCategory", new { ParentId = cat.ParentId });
                        }
                        return RedirectToAction(nameof(Index));
                }

            }

            return View(cat);
        }

        #endregion

        #region Delete Location

        public async Task<IActionResult> DeleteAdsCategory(ulong id)
        {
            await _adsCategory.RemoveAdsCategory(id);


            return JsonResponseStatus.Success();
        }

        #endregion

        #region Recovery Location

        public async Task<IActionResult> RecoveryAdsCategory(ulong id)
        {
            await _adsCategory.RecoveryAdsCategory(id);

            return JsonResponseStatus.Success();
        }

        #endregion
    }
}
