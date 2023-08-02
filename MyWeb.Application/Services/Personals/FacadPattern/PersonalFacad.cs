using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;
using MyWeb.Application.Services.Personals.Queries;

namespace MyWeb.Application.Services.Personals.FacadPattern
{
    public class PersonalFacad : IPersonalFacad
    {
        #region ctor
        private readonly IDataBaseContext _context;
        public PersonalFacad(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region GetServices
        private IGetAboutService _getAboutService;
        public IGetAboutService GetAboutService
        {
            get
            {
                return _getAboutService ??= new GetAboutService(_context);
            }
        }

        private IGetClientService _getClientService;
        public IGetClientService GetClientService
        {
            get
            {
                return _getClientService ??= new GetClientService(_context);
            }
        }

        private IGetContactService _getContactService;
        public IGetContactService GetContactService
        {
            get
            {
                return _getContactService ??= new GetContactService(_context);
            }
        }

        private IGetHomeService _getHomeService;
        public IGetHomeService GetHomeService
        {
            get
            {
                return _getHomeService ??= new GetHomeService(_context);
            }
        }

        private IGetPriceService _getPriceService;
        public IGetPriceService GetPriceService
        {
            get
            {
                return _getPriceService ??= new GetPriceService(_context);
            }
        }

        private IGetProjectService _getProjectService;
        public IGetProjectService GetProjectService
        {
            get
            {
                return _getProjectService ??= new GetProjectService(_context);
            }
        }

        private IGetServiceService _getServiceService;
        public IGetServiceService GetServiceService
        {
            get
            {
                return _getServiceService ??= new GetServiceService(_context);
            }
        }

        private IGetSocialMediaService _getSocialMediaService;
        public IGetSocialMediaService GetSocialMediaService
        {
            get
            {
                return _getSocialMediaService ??= new GetSocialMediaService(_context);
            }
        }

        private IGetSupportService _getSupportService;
        public IGetSupportService GetSupportService
        {
            get
            {
                return _getSupportService ??= new GetSupportService(_context);
            }
        }

        private IGetFeatureService _getFeatureService;
        public IGetFeatureService GetFeatureService
        {
            get
            {
                return _getFeatureService ??= new GetFeatureService(_context);
            }
        }

        private IGetCounterService _getCounterService;
        public IGetCounterService GetCounterService
        {
            get
            {
                return _getCounterService ??= new GetCounterService(_context);
            }
        }

        private IGetTeamService _getTeamService;
        public IGetTeamService GetTeamService
        {
            get
            {
                return _getTeamService ??= new GetTeamService(_context);
            }
        }
        #endregion

        #region AddServices
        private IAddOrEditMessageService _addOrEditMessageService;
        public IAddOrEditMessageService AddOrEditMessageService
        {
            get
            {
                return _addOrEditMessageService ??= new AddOrEditMessageService(_context);
            }
        }

        private IAddOrEditSiteNewsMemberService _addOrEditSiteNewsMemberService;
        public IAddOrEditSiteNewsMemberService AddOrEditSiteNewsMemberService
        {
            get
            {
                return _addOrEditSiteNewsMemberService ??= new AddOrEditSiteNewsMemberService(_context);
            }
        }
       
        private IAddOrEditAboutService _addOrEditAboutService;
        public IAddOrEditAboutService AddOrEditAboutService
        {
            get
            {
                return _addOrEditAboutService ??= new AddOrEditAboutService(_context);
            }
        }

        private IAddOrEditClientService _addOrEditClientService;
        public IAddOrEditClientService AddOrEditClientService
        {
            get
            {
                return _addOrEditClientService ??= new AddOrEditClientService(_context);
            }
        }

        private IAddOrEditHomeService _addOrEditHomeService;
        public IAddOrEditHomeService AddOrEditHomeService
        {
            get
            {
                return _addOrEditHomeService ??= new AddOrEditHomeService(_context);
            }
        }

        private IAddOrEditSocialMediaService _addOrEditSocialMediaService;
        public IAddOrEditSocialMediaService AddOrEditSocialMediaService
        {
            get
            {
                return _addOrEditSocialMediaService ??= new AddOrEditSocialMediaService(_context);
            }
        }

        private IAddOrEditContactService _addOrEditContactService;
        public IAddOrEditContactService AddOrEditContactService
        {
            get
            {
                return _addOrEditContactService ??= new AddOrEditContactService(_context);
            }
        }

        private IAddOrEditCounterService _addOrEditCounterService;
        public IAddOrEditCounterService AddOrEditCounterService
        {
            get
            {
                return _addOrEditCounterService ??= new AddOrEditCounterService(_context);
            }
        }

        private IAddOrEditFeatureService _addOrEditFeatureService;
        public IAddOrEditFeatureService AddOrEditFeatureService
        {
            get
            {
                return _addOrEditFeatureService ??= new AddOrEditFeatureService(_context);
            }
        }

        private IAddOrEditPriceService _addOrEditPriceService;
        public IAddOrEditPriceService AddOrEditPriceService
        {
            get
            {
                return _addOrEditPriceService ??= new AddOrEditPriceService(_context);
            }
        }

        private IAddOrEditProjectService _addOrEditProjectService;
        public IAddOrEditProjectService AddOrEditProjectService
        {
            get
            {
                return _addOrEditProjectService ??= new AddOrEditProjectService(_context);
            }
        }

        private IAddOrEditServiceService _addOrEditServiceService;
        public IAddOrEditServiceService AddOrEditServiceService
        {
            get
            {
                return _addOrEditServiceService ??= new AddOrEditServiceService(_context);
            }
        }

        private IAddOrEditSupportService _addOrEditSupportService;
        public IAddOrEditSupportService AddOrEditSupportService
        {
            get
            {
                return _addOrEditSupportService ??= new AddOrEditSupportService(_context);
            }
        }

        private IAddOrEditTeamService _addOrEditTeamService;
        public IAddOrEditTeamService AddOrEditTeamService
        {
            get
            {
                return _addOrEditTeamService ??= new AddOrEditTeamService(_context);
            }
        }
        #endregion
    }
}
