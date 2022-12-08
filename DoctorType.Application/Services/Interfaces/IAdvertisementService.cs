using DoctorType.Domain.ViewModels.UserPanel.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Interfaces
{
    public interface IAdvertisementService 
    {
        #region User Panel Side 

        //Fill Create Advertisement View Model
        Task<CreateAdvertisementUserPanelSideViewModel> FillCreateAdvertisementUserPanelSideViewModel(ulong userId);

        //Create Advertisement From User Panel 
        Task<bool> CreateAdvertisementFromUserPanel(CreateAdvertisementUserPanelSideViewModel model);

        #endregion

        #region Admin Side 



        #endregion
    }
}
