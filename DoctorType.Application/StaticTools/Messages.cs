using SixLabors.ImageSharp.ColorSpaces;

namespace DoctorType.Application.StaticTools;

public static class Messages
{
    //This is for sample
    public static string GetMessageForSetConsultationDate(string date, string time, string phone)
    {
        return
            $"باسلام  {Environment.NewLine}در خواست مشاوره شما برای روز {date + " - ساعت  " + time}.{Environment.NewLine} تنظیم شد. لطفا در تاریخ و زمان مقرر با شماره {phone} تماس بگیرید.{Environment.NewLine} {PathTools.SiteFarsiName}";
    }
}