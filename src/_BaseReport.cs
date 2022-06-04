namespace Exemplar.Web.Areas.PropertyInspection.Models.ReportViewModels
{
    using System.Collections.Generic;
    using Exemplar.Web.Areas.PropertyInspection.Models.ReportViewModels.SectionViewModels;

    public class BaseReport
    {
        public string AdditionalSummary { get; set; }

        public string OutBuildingDamage { get; set; }

        public string OutBuildingNotes { get; set; }

        public List<CollateralItem> CollateralItems { get; set; } = new List<CollateralItem>();

        public DamageOverviewSection DamageOverviewSection { get; set; }

        public List<InteriorItem> InteriorItems { get; set; } = new List<InteriorItem>();

        public List<NonStormDamageItem> NonStormDamageItems { get; set; } = new List<NonStormDamageItem>();

        public ReportHeaderSection ReportHeaderSection { get; set; }

        public List<RoofComponent> RoofComponents { get; set; }

        public RoofObservationSection RoofObservationSection { get; set; }

        public RoofSummarySection RoofSummarySection { get; set; }

        public StormInfoSection StormInfoSection { get; set; }
    }
}
