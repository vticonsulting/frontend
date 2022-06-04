namespace Exemplar.Web.Areas.PropertyInspection.Models.ReportViewModels.SectionViewModels
{
    public class InteriorItem
    {
        public InteriorItem(string location, string size, string description)
        {
            Location = location;
            Size = size;
            Description = description;
        }

        public string Location { get; set; }

        public string Size { get; set; }

        public string Description { get; set; }
    }
}
