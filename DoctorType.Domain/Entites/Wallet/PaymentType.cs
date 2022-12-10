using System.ComponentModel.DataAnnotations;

namespace DoctorType.Domain.Entities.Wallet;

public enum PaymentType
{
    [Display(Name = "شارژ حساب")]
    ChargeWallet = 0,

    [Display(Name = "خرید")]
    Buy = 1,

    [Display(Name = "HomeVisit")]
    HomeVisit = 2,

    [Display(Name = "HomeNurse")]
    HomeNurse = 3,

    [Display(Name = "DeathCertificate")]
    DeathCertificate = 4,

    [Display(Name = "HomeLabortory")]
    HomeLaboratory = 5,

    [Display(Name = "HomePatientTransport")]
    HomePatientTransport = 6,

    [Display(Name = "HomePharmacy")]
    HomePharmacy = 7,

    [Display(Name = "خرید اشتراک")]
    Reservation = 8,

    [Display(Name = "OnlineVisit")]
    OnlineVisit = 8,
}