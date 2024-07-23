using CovidChart.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace CovidChart.API.Hubs;

public class CovidHub : Hub<ICovidHub>
{
    private readonly CovidService _service;

    public CovidHub(CovidService service)
    {
        _service = service;
    }

    public async Task GetCovidList()
    {
        var covidChartData = _service.GetCovidChartList();
        await Clients.All.ReceiveCovidList(covidChartData);
    }
}