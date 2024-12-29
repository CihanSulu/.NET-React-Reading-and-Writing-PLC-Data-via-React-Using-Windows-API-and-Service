namespace OMGNEW_API.Dto.GetFilter
{
    public class UpdateFilterRequest
    {
        public float genel_ariza { get; set; }
        public float mekanik_ariza { get; set; }
        public float elektrik_ariza { get; set; }
        public float isletme_ariza { get; set; }
        public int dataId { get; set; }
    }
}
