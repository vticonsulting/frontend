namespace Exemplar.Web.Areas.PropertyInspection.Models.ReportViewModels.SectionViewModels
{
    using System.Collections.Generic;

    public class RoofObservationSection
    {
        public string BrittlenessTestConducted { get; set; }

        public string BrittlenessTestFrontPass { get; set; }

        public string BrittlenessTestBackPass { get; set; }

        public string BrittlenessTestLeft { get; set; }

        public string BrittlenessTestLeftPass { get; set; }

        public string BrittlenessTestRightPass { get; set; }

        public string DownspoutDamage { get; set; }

        public string DripEdge { get; set; }

        public string ElevationDamage { get; set; }

        public string FasciaDamage { get; set; }

        public string FeltDescription { get; set; }

        public string GutterDamage { get; set; }

        public string HailDamage { get; set; }

        public string HailDamageFront { get; set; }

        public string HailDamageLeft { get; set; }

        public string HailDamageBack { get; set; }

        public string HailDamageRight { get; set; }

        public string HailTestSize { get; set; }

        public string IceWaterShield { get; set; }

        public string PitchFront { get; set; }

        public string PitchBack { get; set; }

        public string PitchLeft { get; set; }

        public string PitchRight { get; set; }

        public string PriorRepairs { get; set; }

        public string PriorRepairsFront { get; set; }

        public string PriorRepairsBack { get; set; }

        public string PriorRepairsLeft { get; set; }

        public string PriorRepairsRight { get; set; }

        public List<RoofAccessories> RoofAccessories { get; set; }

        public ReportType ReportType { get; set; }

        public string RoofMaterial { get; set; }

        public string RoofType { get; set; }

        public string ScreenDamage { get; set; }

        public string SidingDamage { get; set; }

        public string Stories { get; set; }

        public string WindDamage { get; set; }

        public string WindDamageRight { get; set; }

        public string WindDamageBack { get; set; }

        public string WindDamageLeft { get; set; }

        public string WindDamageFront { get; set; }

        public string WindowDamage { get; set; }

        public string WindowScreenDamage { get; set; }
    }
}
