using DoctorType.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoctorType.Web.Areas.UserPanel.Controllers
{
    public class TariffController : UserPanelBaseController
    {
        #region ctor

        private readonly ITariffService _tariffService;

        public TariffController(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }

        #endregion

        #region List Of Tarrifs 

        public async Task<IActionResult> ListOfTarrifs()
        {
            #region Fill Model 

            var model = await _tariffService.ShowListOfTarrifsInUserPanel() ;
            if (model == null) return NotFound();

            #endregion

            return View(model);
        }

        #endregion
    }
}
