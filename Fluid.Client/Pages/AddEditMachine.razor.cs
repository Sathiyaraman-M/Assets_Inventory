﻿using System.Text.Json;
using Fluid.Client.Pages.Dialogs.MachineMasterDialogs;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fluid.Client.Pages;

public partial class AddEditMachine
{
    [Parameter] public string Id { get; set; }

    [Parameter] public string FeedLogId { get; set; }

    private SystemConfiguration _model = new();

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(Id))
        {
            var result = await MasterHttpClient.GetByIdAsync(Id);
            if (result.Succeeded)
            {
                _model = result.Data;
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    snackbar.Add(message, Severity.Error);
                }
                navigationManager.NavigateTo("/Machine-Master");
            }
        }

        if (!string.IsNullOrEmpty(FeedLogId))
        {
            var result = await FeedLogHttpClient.GetByIdAsync(FeedLogId);
            if (result.Succeeded)
            {
                _model = (SystemConfiguration)JsonSerializer.Deserialize(result.Data.JsonRaw, typeof(SystemConfiguration));
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    snackbar.Add(message, Severity.Error);
                }
                navigationManager.NavigateTo("/Machine-Master");
            }
        }
        await base.OnInitializedAsync();
    }

    private async Task InvokeMotherboardDialog(bool isNew, bool isEdit, MotherboardInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachineMotherboardDialog.IsNew), isNew },
            { nameof(AddEditMachineMotherboardDialog.IsEdit), isEdit },
            { nameof(AddEditMachineMotherboardDialog.Model), info }
        };
        var options = new DialogOptions
            { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachineMotherboardDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as MotherboardInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (_model.Motherboards.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            _model.Motherboards.Remove(_model.Motherboards.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        _model.Motherboards.Add(updatedInfo);
    }

    private async Task InvokeHardDiskDialog(bool isNew, bool isEdit, HardDiskInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachineHardDiskDialog.IsNew), isNew },
            { nameof(AddEditMachineHardDiskDialog.IsEdit), isEdit },
            { nameof(AddEditMachineHardDiskDialog.Model), info }
        };
        var options = new DialogOptions
            { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachineHardDiskDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as HardDiskInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (_model.HardDisks.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            _model.HardDisks.Remove(_model.HardDisks.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        _model.HardDisks.Add(updatedInfo);
    }

    private async Task InvokePhysicalMemoryDialog(bool isNew, bool isEdit, PhysicalMemoryInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachinePhysicalMemoryDialog.IsNew), isNew },
            { nameof(AddEditMachinePhysicalMemoryDialog.IsEdit), isEdit },
            { nameof(AddEditMachinePhysicalMemoryDialog.Model), info }
        };
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachinePhysicalMemoryDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as PhysicalMemoryInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (_model.PhysicalMemories.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            _model.PhysicalMemories.Remove(_model.PhysicalMemories.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        _model.PhysicalMemories.Add(updatedInfo);
    }

    private async Task InvokeKeyboardDialog(bool isNew, bool isEdit, KeyboardInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachineKeyboardDialog.IsNew), isNew },
            { nameof(AddEditMachineKeyboardDialog.IsEdit), isEdit },
            { nameof(AddEditMachineKeyboardDialog.Model), info }
        };
        var options = new DialogOptions
            { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachineKeyboardDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as KeyboardInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (_model.Keyboards.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            _model.Keyboards.Remove(_model.Keyboards.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        _model.Keyboards.Add(updatedInfo);
    }

    private async Task InvokeMonitorDialog(bool isNew, bool isEdit, MonitorInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachineMonitorDialog.IsNew), isNew },
            { nameof(AddEditMachineMonitorDialog.IsEdit), isEdit },
            { nameof(AddEditMachineMonitorDialog.Model), info }
        };
        var options = new DialogOptions
            { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachineMonitorDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as MonitorInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (_model.Monitors.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            _model.Monitors.Remove(_model.Monitors.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        _model.Monitors.Add(updatedInfo);
    }

    private async Task InvokeMouseDialog(bool isNew, bool isEdit, MouseInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachineMouseDialog.IsNew), isNew },
            { nameof(AddEditMachineMouseDialog.IsEdit), isEdit },
            { nameof(AddEditMachineMouseDialog.Model), info }
        };
        var options = new DialogOptions
            { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachineMouseDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as MouseInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (_model.Mouses.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            _model.Mouses.Remove(_model.Mouses.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        _model.Mouses.Add(updatedInfo);
    }

    private void DeleteMotherboardInfo(MotherboardInfo motherboardInfo)
    {
        _model.Motherboards.Remove(motherboardInfo);
    }

    private void DeletePhysicalMemoryInfo(PhysicalMemoryInfo physicalMemoryInfo)
    {
        _model.PhysicalMemories.Remove(physicalMemoryInfo);
    }

    private void DeleteHardDiskInfo(HardDiskInfo hardDisk)
    {
        _model.HardDisks.Remove(hardDisk);
    }
    
    private void DeleteKeyboardInfo(KeyboardInfo keyboardInfo)
    {
        _model.Keyboards.Remove(keyboardInfo);
    }

    private void DeleteMonitorInfo(MonitorInfo monitorInfo)
    {
        _model.Monitors.Remove(monitorInfo);
    }

    private void DeleteMouseInfo(MouseInfo mouseInfo)
    {
        _model.Mouses.Remove(mouseInfo);
    }

    private async Task SubmitAsync()
    {
        if (string.IsNullOrEmpty(Id))
        {
            var result = await MasterHttpClient.AddAsync(_model);
            if (result.Succeeded)
            {
                snackbar.Add(result.Messages[0], Severity.Success);
                navigationManager.NavigateTo("/Machine-Master");
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    snackbar.Add(message, Severity.Error);
                }
            }
        }
        else
        {
            var result = await MasterHttpClient.EditAsync(_model);
            if (result.Succeeded)
            {
                snackbar.Add(result.Messages[0], Severity.Success);
                navigationManager.NavigateTo("/Machine-Master");
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    snackbar.Add(message, Severity.Error);
                }
            }
        }
    }
}