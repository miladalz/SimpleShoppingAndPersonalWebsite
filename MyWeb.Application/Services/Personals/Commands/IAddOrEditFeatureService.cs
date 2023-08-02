using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditFeatureService
    {
        ResultDto ExecuteAdd(FeatureDto featureDto);
        ResultDto ExecuteEdit(FeatureDto featureDto);
        ResultDto ExecuteDelete(long id);
    }
    public class AddOrEditFeatureService : IAddOrEditFeatureService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditFeatureService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(FeatureDto featureDto)
        {
            var feature = new Feature()
            {
                Title = featureDto.Title,
                Description = featureDto.Description
            };
            _context.Features.Add(feature);

            List<FeatureItem> featureItems = new();
            foreach (var item in featureDto.FeatureItems)
            {
                featureItems.Add(new FeatureItem
                {
                    Description = item.Description,
                    Title = item.Title,
                    Feature = feature,
                    Icon = item.Icon
                });
            }
            _context.FeatureItems.AddRange(featureItems);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }

        public ResultDto ExecuteEdit(FeatureDto featureDto)
        {
            var feature = _context.Features
                .Include(p => p.FeatureItems)
                .FirstOrDefault(p => p.Id == featureDto.Id);
            if (feature != null)
            {
                feature.Title = featureDto.Title;
                feature.Description = featureDto.Description;
                feature.UpdateTime = DateTime.Now;

                foreach (var item in featureDto.FeatureItems)
                {
                    var existItem = feature.FeatureItems.FirstOrDefault(a => a.Id == item.Id);
                    if (existItem != null)
                    {
                        existItem.Description = item.Description;
                        existItem.Title = item.Title;
                        existItem.Feature = feature;
                        existItem.Icon = item.Icon;
                        existItem.UpdateTime = DateTime.Now;
                    }
                }
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "با موفقیت ذخیره شد",
                };
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "رکوردی یافت نشد.",
                };
            }
        }
        public ResultDto ExecuteDelete(long id)
        {
            var feature = _context.Features
                .Include(p => p.FeatureItems)
                .FirstOrDefault(p => p.Id == id);
            if (feature == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            feature.RemoveTime = DateTime.Now;
            feature.IsRemoved = true;
            foreach (var item in feature.FeatureItems)
            {
                feature.RemoveTime = DateTime.Now;
                feature.IsRemoved = true;
            }
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }
    public class FeatureDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<FeatureItemDto> FeatureItems { get; set; }
    }
    public class FeatureItemDto
    {
        public long Id { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
