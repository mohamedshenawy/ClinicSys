namespace Domain.DTO
{
    public class TabulatorDTO
    {
        public IEnumerable<dynamic> Data { get; set; }
        public IEnumerable<dynamic> tableData { get; set; }
        public int Last_page { get; set; }
    }
}
