using MyWeb.Domain.Entities.Commons;

namespace MyWeb.Domain.Entities.Personal
{
    public class About : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Title2 { get; set; }
        public string Description2 { get; set; }
        public string TaskTitle { get; set; }
        public string Image { get; set; }
        public string HeaderImage  { get; set; }
        public ICollection<AboutTask> TaskItems { get; set; }
    }
    public class AboutTask : BaseEntity
    {
        public virtual About About { get; set; }
        public string Title { get; set; }
    }

    public class Clinet : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public string Comment { get; set; }
        public string TitleImage { get; set; }
    }

    public class Contact : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<ContactItem> ContactItems { get; set; }
    }
    public class ContactItem : BaseEntity
    {
        public virtual Contact Contact { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }

    public class Counter : BaseEntity
    {
        public string Icon { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }

    public class Feature : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<FeatureItem> FeatureItems { get; set; }
    }
    public class FeatureItem : BaseEntity
    {
        public virtual Feature Feature { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }

    public class Home : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
    }

    public class Price : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<PriceItem> PriceItems { get; set; }
    }
    public class PriceItem : BaseEntity
    {
        public virtual Price Price { get; set; }
        public long Amount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public bool Featured { get; set; }
    }

    public class Project : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<ProjectItem> ProjectItems { get; set; }
    }
    public class ProjectItem : BaseEntity
    {
        public virtual Project Project { get; set; }
        public string ProjectType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
    }

    public class Service : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<ServiceItem> ServiceItem { get; set; }
    }
    public class ServiceItem : BaseEntity
    {
        public virtual Service Service { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Support : BaseEntity
    {
        public string Location { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string LinkTitle { get; set; }
    }

    public class SocialMedia : BaseEntity
    {
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
    }

    public class Team : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<TeamMember> TeamMemberItems { get; set; }
    }
    public class TeamMember : BaseEntity
    {
        public virtual Team Team { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Jobtitle { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string GooglePlus { get; set; }
        public string Twitter { get; set; }
    }

    public class Message : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }        
    }
    public class SiteNewsMember : BaseEntity
    {
        public string Email { get; set; }
    }
}
