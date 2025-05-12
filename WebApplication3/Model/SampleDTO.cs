namespace test1template.Models;

public class SampleDTO
{
    public string sampleLine { get; set; }
    public DateTime sampleDate { get; set; }
    public int sampleInt { get; set; }
    public decimal sampleFee { get; set; }
    public OlegDTO oleg { get; set; }
    public List<OlegDTO> olegs { get; set; }
}

public class OlegDTO
{
    public int age { get; set; }
}
