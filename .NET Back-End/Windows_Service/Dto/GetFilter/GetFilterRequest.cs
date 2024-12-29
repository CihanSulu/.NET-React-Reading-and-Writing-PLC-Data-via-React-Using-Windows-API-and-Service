namespace OMGNEW_API.Dto.GetFilter
{
    public class GetFilterRequest
    {
        public int page { get; set; }
        public bool last50 { get; set; }
        public string? date1 { get; set; }
        public string? date2 { get; set; }

    }
}
