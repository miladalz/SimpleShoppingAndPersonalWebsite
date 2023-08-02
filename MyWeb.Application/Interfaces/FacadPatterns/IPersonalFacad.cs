using MyWeb.Application.Services.Personals.Commands;
using MyWeb.Application.Services.Personals.Queries;

namespace MyWeb.Application.Interfaces.FacadPatterns
{
    public interface IPersonalFacad
    {
        IGetAboutService GetAboutService { get; }
        IGetClientService GetClientService { get; }
        IGetContactService GetContactService { get; }
        IGetHomeService GetHomeService { get; }
        IGetPriceService GetPriceService { get; }
        IGetProjectService GetProjectService { get; }
        IGetServiceService GetServiceService { get; }
        IGetSocialMediaService GetSocialMediaService { get; }
        IGetSupportService GetSupportService { get; }
        IGetFeatureService GetFeatureService { get; }
        IGetCounterService GetCounterService { get; }
        IGetTeamService GetTeamService { get; }
        IAddOrEditMessageService AddOrEditMessageService { get; }
        IAddOrEditSiteNewsMemberService AddOrEditSiteNewsMemberService { get; }
        IAddOrEditAboutService AddOrEditAboutService { get; }
        IAddOrEditClientService AddOrEditClientService { get; }
        IAddOrEditHomeService AddOrEditHomeService { get; }
        IAddOrEditSocialMediaService AddOrEditSocialMediaService { get; }
        IAddOrEditContactService AddOrEditContactService{ get; }
        IAddOrEditCounterService AddOrEditCounterService { get; }
        IAddOrEditFeatureService AddOrEditFeatureService { get; }
        IAddOrEditPriceService AddOrEditPriceService { get; }
        IAddOrEditProjectService AddOrEditProjectService { get; }
        IAddOrEditServiceService AddOrEditServiceService { get; }
        IAddOrEditSupportService AddOrEditSupportService { get; }
        IAddOrEditTeamService AddOrEditTeamService { get; }
    }
}
