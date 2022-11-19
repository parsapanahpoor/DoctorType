
using System;

namespace DoctorType.Domain.ViewModels.Common.Paging
{
    public class Pager
    {
        public static BasePaging Build(int pageId, int allEntitiesCount, int take, int howManyShowPageAfterAndBefore)
        {
            var pageCount = Convert.ToInt32(Math.Ceiling(allEntitiesCount / (double)take));
            return new BasePaging
            {
                PageId = pageId,
                AllEntitiesCount = allEntitiesCount,
                HowManyShowPageAfterAndBefore = howManyShowPageAfterAndBefore,
                PageCount = pageCount,
                TakeEntity = take,
                SkipEntity = (pageId - 1) * take,
                StartPage = pageId - howManyShowPageAfterAndBefore <= 0 ? 1 : pageId - howManyShowPageAfterAndBefore,
                EndPage = pageId + howManyShowPageAfterAndBefore > pageCount ? pageCount : pageId + howManyShowPageAfterAndBefore
            };
        }

        public static object Build(object pageId, int v, object takeEntity, object howManyShowPageAfterAndBefore)
        {
            throw new NotImplementedException();
        }
    }
}
