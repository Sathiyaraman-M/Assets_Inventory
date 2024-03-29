﻿using System.Text.Json;
using Fluid.BgService.Models;
using Fluid.Shared.Models;
using static System.Threading.Tasks.Task;

namespace Fluid.BgService.Services;

public class BackgroundLoggerService : BackgroundService
{
    private readonly WritableOptions<BackgroundLogTime> _options;
    private readonly SystemConfigurationService _systemConfigurationService;

    public BackgroundLoggerService(WritableOptions<BackgroundLogTime> options, SystemConfigurationService systemConfigurationService)
    {
        _options = options;
        _systemConfigurationService = systemConfigurationService;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (DateTime.Now < _options.Value.NextLogDateTime)
                await Delay(_options.Value.NextLogDateTime - DateTime.Now);            
            _systemConfigurationService.SystemConfiguration.Motherboards = SystemConfigurationService.GetMotherboardsDetails().ToList();
            _systemConfigurationService.SystemConfiguration.PhysicalMemories = SystemConfigurationService.GetPhysicalMemoryInfos().ToList();
            _systemConfigurationService.SystemConfiguration.HardDisks = SystemConfigurationService.GetHardDisksInfo().ToList();  
            await _systemConfigurationService.LogSystemConfiguration();
        }
    }
}