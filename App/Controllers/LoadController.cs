using App.Models;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Text;
using Service.Customer;
using Service.Load;
using TimeUtils;

namespace App.Controllers;

public class LoadController(ILoadService loadService, ICustomerService customerService, LocalTimeWindow.Factory timeWindowFactory) : Controller
{
    private static readonly LocalDateTimePattern DateTimePattern = LocalDateTimePattern.CreateWithInvariantCulture("uuuu'-'MM'-'dd'T'HH':'mm");

    public async Task<IActionResult> Index()
    {
        var vm = (await loadService.GetAllAsync()).ToIndexViewModel(await customerService.GetAllAsync());
        if (Request.Headers.ContainsKey("X-Up-Target"))
        {
            return PartialView("_LoadTable", vm);
        }
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var customers = await customerService.GetAllAsync();
        var vm = new LoadFormViewModel
        {
            Customers = customers.ToOptions(),
            Stops = new List<StopFormViewModel>
            {
                new() { SequenceNumber = 1 }
            }
        };
        return PartialView("_LoadForm", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LoadFormViewModel form)
    {
        var dto = new LoadCreationDto(Stops: BuildStopDtos(form.Stops), CustomerId: form.CustomerId);
        await loadService.AddAsync(form.CustomerId, dto);
        return await LoadTablePartial();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var load = await loadService.GetByIdAsync(id);
        if (load == null)
        {
            return NotFound();
        }
        var customers = await customerService.GetAllAsync();
        var vm = new LoadFormViewModel
        {
            Id = load.LoadNumber,
            CustomerId = load.CustomerId,
            Customers = customers.ToOptions(),
            Stops = load.Stops
                .OrderBy(s => s.SequenceNumber)
                .Select(s => new StopFormViewModel
                {
                    SequenceNumber = s.SequenceNumber,
                    Start = s.AppointmentWindow.Start.ToString("uuuu'-'MM'-'dd'T'HH':'mm", null),
                    End = s.AppointmentWindow.End.ToString("uuuu'-'MM'-'dd'T'HH':'mm", null),
                    ZoneId = s.AppointmentWindow.ZoneId
                }).ToList()
        };
        return PartialView("_LoadForm", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(long id, LoadFormViewModel form)
    {
        var dto = new LoadDto(Stops: BuildStopDtos(form.Stops), CustomerId: form.CustomerId, LoadNumber: id);
        await loadService.UpdateAsync(form.CustomerId, dto);
        return await LoadTablePartial();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        await loadService.RemoveAsync(id);
        return await LoadTablePartial();
    }

    [HttpPost]
    public async Task<IActionResult> HatWinners()
    {
        var vm = (await loadService.GetHatWinnerLoadsAsync()).ToIndexViewModel(await customerService.GetAllAsync());
        return PartialView("_HatWinners", vm);
    }

    [HttpGet]
    public IActionResult AddStopFields(int index)
    {
        var vm = new StopFormViewModel { SequenceNumber = index + 1 };
        return PartialView("_StopFields", new StopFieldsPartialModel(index, vm));
    }

    private async Task<IActionResult> LoadTablePartial()
    {
        var vm = (await loadService.GetAllAsync()).ToIndexViewModel(await customerService.GetAllAsync());
        return PartialView("_LoadTable", vm);
    }

    private List<StopDto> BuildStopDtos(List<StopFormViewModel> stops)
    {
        return stops
            .Where(s => !string.IsNullOrWhiteSpace(s.Start) && !string.IsNullOrWhiteSpace(s.End))
            .Select(s =>
            {
                var start = DateTimePattern.Parse(s.Start).Value;
                var end = DateTimePattern.Parse(s.End).Value;
                var appointmentWindow = timeWindowFactory.Create(start, end, s.ZoneId);
                return new StopDto(s.SequenceNumber, appointmentWindow);
            }).ToList();
    }
}
