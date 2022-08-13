﻿using Fluid.Core.Features.Masters;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/machines")]
[ApiController]
public class MachineMasterController : ControllerBase
{
    private readonly IMachineMasterService _machineMasterService;

    public MachineMasterController(IMachineMasterService machineMasterService)
    {
        _machineMasterService = machineMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHardDisks(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _machineMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{assetTag}")]
    public async Task<IActionResult> GetByIdAsync(string assetTag)
    {
        return Ok(await _machineMasterService.GetByIdAsync(assetTag));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(MachineMasterModel model)
    {
        return Ok(await _machineMasterService.AddAsync(model));
    }

    [HttpPut("{assetTag}")]
    public async Task<IActionResult> EditAsync(MachineMasterModel model)
    {
        return Ok(await _machineMasterService.EditAsync(model));
    }

    [HttpDelete("{assetTag}")]
    public async Task<IActionResult> DeleteAsync(string assetTag)
    {
        return Ok(await _machineMasterService.DeleteAsync(assetTag));
    }
}