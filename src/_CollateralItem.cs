namespace Exemplar.Web.Areas.PropertyInspection.Models.ReportViewModels.SectionViewModels
{
    public class CollateralItem
    {
        public CollateralItem(string item, string damaged, string damageAmt, string size, string material, string additionalNotes)
        {
            Item = item;
            Damaged = damaged;
            DamageAmt = damageAmt;
            Size = size;
            Material = material;
            AdditionalNotes = additionalNotes;
        }

        public string Item { get; set; }

        public string Damaged { get; set; }

        public string DamageAmt { get; set; }

        public string Size { get; set; }

        public string Material { get; set; }

        public string AdditionalNotes { get; set; }
    }
}
