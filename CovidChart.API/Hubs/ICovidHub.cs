using CovidChart.API.Models;

namespace CovidChart.API.Hubs;

public interface ICovidHub
{
    Task ReceiveCovidList(List<CovidChartItem> covidChartData);
}
