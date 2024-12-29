namespace Web_API.Dto.GetFilter
{
    public class GetFilterRequest
    {
        public int page { get; set; }
        public bool last50 { get; set; }
        public string? date1 { get; set; }
        public string? date2 { get; set; }
        public int? number1 { get; set; } //last add
        public int? number2 { get; set; }

    }
}
