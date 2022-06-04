namespace Exemplar.Web.Areas.PropertyInspection.Models.ReportViewModels.SectionViewModels
{
    public class RoofComponent
    {
        public RoofComponent(string componentName,  string componentPresent,  string componentQty, string componentQtyDamaged,  string componentMaterial, string componentPainted)
        {
            ComponentPresent = componentPresent == "Yes" ? true : false;
            ComponentName = componentName;
            ComponentMaterial = componentMaterial;
            ComponentPainted = componentPainted;
            ComponentQty = componentQty;
            ComponentQtyDamaged = componentQtyDamaged;
        }

        public bool ComponentPresent { get; set; }

        public string ComponentName { get; set; }

        public string ComponentMaterial { get; set; }

        public string ComponentPainted { get; set; }

        public string ComponentQty { get; set; }

        public string ComponentQtyDamaged { get; set; }
    }
}
