namespace CDR_API.Entities
{
    public class CDR_Table
    {
        public int Id { get; set; }
        public string? caller_id { get; set; }
        public string? recipient { get; set; }
        public string? call_date { get; set; }
        public string? end_time { get; set; }
        public int? duration { get; set; }
        public double? cost { get; set; }
        public string? reference { get; set; }
        public string? currency { get; set; }
    }
}
