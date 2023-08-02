using MyWeb.Application.Services.Personals.Queries;

namespace EndPoint.Site.Models.ViewModels.Personal
{
    public class PersonalViewModel
    {
        public List<AboutList> Abouts { get; set; }
        public List<ClientList> Clients { get; set; }
        public List<ContactList> Contacts { get; set; }
        public List<HomeList> Homes { get; set; }
        public List<PriceList> Prices { get; set; }
        public List<ProjectList> Projects { get; set; }
        public List<ServiceList> Services { get; set; }
        public List<SocialMediaList> SocialMedias { get; set; }
        public SupportList SupportTop { get; set; }
        public SupportList SupportMiddle { get; set; }
        public SupportList SupportBottom { get; set; }
        public SupportList SupportFooter { get; set; }
        public List<FeatureList> Features { get; set; }
        public List<CounterList> Counters { get; set; }
        public List<TeamList> Teams { get; set; }
    }
}
