using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.ViewModels.Admin.Advertisement;
using DoctorType.Domain.ViewModels.UserPanel.Advertisement;
using DoctorType.Domain.ViewModels.UserPanel.Project;
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

        //Filter Advertisement From User Panel 
        Task<List<Advertisemenet>?> FilterAdvertisementFromUserPanel(ulong userId);

        //Edit Advertisement From User Panel 
        Task<EditAdvertisementUserPanelViewModel?> FillEditAdvertisementUserPanelViewModel(ulong advertisementId, ulong userId);

        //Edit Advertisement From User Panel 
        Task<bool> EditAdvertisementFromUserPanel(EditAdvertisementUserPanelViewModel model, ulong userId);

        //Delete Advertisement From User Panel 
        Task<bool> DeleteAdvertisementFromUserPanel(ulong advertisementId, ulong userId);

        //Show Advertisement Detail User Panel Side 
        Task<ShowAdvertisementDetailUserPanelSideViewModel?> FillShowAdvertisementDetailUserSideViewModel(ulong advertisementId, ulong userId);

        #endregion

        #region Admin Side 

        //List Of Advertisement Admin Side
        Task<List<Advertisemenet>?> ListOfAdvertisementAdminSide();

        //Show Advertisement Detail Admin Side 
        Task<ShowAdvertisementDetailAdminSideViewModel?> FillShowAdvertisementDetailAdminSideViewModel(ulong advertisementId);

        //Delete Advertisement From Admin Side  
        Task<bool> DeleteAdvertisementFromAdminPanel(ulong advertisementId);

        //List OF Projects For Working On 
        Task<List<Advertisemenet>?> ListOfProjectForWorkingOnInUserPanel(ulong userId);

        #endregion
    }
}
