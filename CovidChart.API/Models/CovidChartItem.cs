namespace CovidChart.API.Models;

public class CovidChartItem
{
    public CovidChartItem()
    {
        Counts = new List<int>();
    }

    public string CovidDate { get; set; }
    public List<int> Counts { get; set; }
}