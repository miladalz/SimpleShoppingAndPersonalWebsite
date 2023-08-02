using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Products.Queries.GetProductForSite
{
    public interface IGetProductForSiteService
    {
        ResultDto<ResultProductForSiteDto> Execute(Ordering ordering,string SearchKey, int Page,int pageSize, long? CatId );
    }
    public class ResultProductForSiteDto
    {
        public List<ProductForSiteDto> Products { get; set; }
        public int TotalRow { get; set; }
    }
    public class ProductForSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageSrc { get; set; }
        public int Star { get; set; }
        public int Price { get; set; }
    }
    public enum Ordering
    {

        NotOrder=0,
        /// <summary>
        /// پربازدیدترین
        /// </summary>
        MostVisited=1,
        /// <summary>
        /// پرفروشترین
        /// </summary>
        Bestselling=2,
        /// <summary>
        /// محبوبترین
        /// </summary>
        MostPopular=3,
        /// <summary>
        /// جدیدترین
        /// </summary>
        theNewest=4,
        /// <summary>
        /// ارزانترین
        /// </summary>
        Cheapest=5,
        /// <summary>
        /// گرانترین
        /// </summary>
        theMostExpensive=6
    }

}
