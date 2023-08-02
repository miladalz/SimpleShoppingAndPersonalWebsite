using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetFeatureService
    {
        ResultDto<List<FeatureList>> Execute();
        ResultDto<FeaturePage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        FeatureList ExecuteForEdit(long id);
    }

    public class GetFeatureService : IGetFeatureService
    {
        private readonly IDataBaseContext _context;
        public GetFeatureService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<FeatureList>> Execute()
        {
            var features = _context.Features
                .Include(p => p.FeatureItems)
                .ToList()
                .Select(p => new FeatureList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    FeatureItems = p.FeatureItems.ToList()
                    .Select(c => new Feature
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Icon = c.Icon,
                        Title = c.Title
                    }).ToList()
                }).ToList();
            return new ResultDto<List<FeatureList>>()
            {
                Data = features,
                IsSuccess = true,
            };
        }
        public ResultDto<FeaturePage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Features
                .Include(p => p.FeatureItems)
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new FeatureList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    FeatureItems = p.FeatureItems.ToList()
                    .Select(c => new Feature
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Icon = c.Icon,
                        Title = c.Title
                    }).ToList()
                }).ToList();
            return new ResultDto<FeaturePage>()
            {
                Data = new FeaturePage()
                {
                    Features = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }

        public FeatureList ExecuteForEdit(long id)
        {
            var features = _context.Features
                .Include(p => p.FeatureItems)
                .Where(p => p.Id == id)
                .Select(p => new FeatureList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    FeatureItems = p.FeatureItems.ToList()
                    .Select(c => new Feature
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Icon = c.Icon,
                        Title = c.Title
                    }).ToList()
                }).FirstOrDefault();
            return features;
        }
    }
    public class FeaturePage : ResultPage
    {
        public List<FeatureList> Features { get; set; }
    }
    public class FeatureList
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Feature> FeatureItems { get; set; }
    }
    public class Feature
    {
        public long Id { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
