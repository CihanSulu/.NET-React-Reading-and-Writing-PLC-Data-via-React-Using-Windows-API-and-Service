namespace OMGNEW_API.Dto.Error
{
    public class GetErrorResponse
    {
        public double genel_ariza { get; set; }
        public double mekanik_ariza { get; set; }
        public double elektrik_ariza { get; set; }
        public double isletme_ariza { get; set; }
    }
}
