﻿using Fluid.Client.Pages.Dialogs;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using MudBlazor;

namespace Fluid.Client.Pages.Tabs;
public partial class Motherboard
{
    private List<MotherboardInfo> _motherboards;
    private string _searchString;
    private MudTable<MotherboardInfo> _motherboardTable;
    private int _totalItems;

    private async Task<TableData<MotherboardInfo>> OnServerReloadAsync(TableState tableState)
    {
        if (!string.IsNullOrWhiteSpace(_searchString))
        {
            tableState.Page = 0;
        }
        await LoadDataAsync(tableState.Page, tableState.PageSize, tableState);
        return new TableData<MotherboardInfo> { TotalItems = _totalItems, Items = _motherboards };
    }

    private async Task LoadDataAsync(int page, int pageSize, TableState tableState)
    {
        string[] orderings = null;
        if (!string.IsNullOrEmpty(tableState.SortLabel))
        {
            orderings = tableState.SortDirection != SortDirection.None ? new[] { $"{tableState.SortLabel} {tableState.SortDirection}" } : new[] { $"{tableState.SortLabel}" };
        }
        var response = await MasterHttpClient.GetAllAsync(new PagedRequest
        {
            PageNumber = page + 1,
            PageSize = pageSize,
            SearchString = _searchString,
            OrderBy = orderings
        });
        if (response.Succeeded)
        {
            _totalItems = response.TotalCount;
            _motherboards = response.Data;
        }
        else
        {
            foreach (var message in response.Messages)
            {
                snackbar.Add(message, Severity.Error);
            }
        }
    }

    private async void InvokeModal(string oemSerialNo)
    {
        var parameters = new DialogParameters();
        if (!string.IsNullOrEmpty(oemSerialNo))
        {
            var item = _motherboards.FirstOrDefault(c => c.OemSerialNo == oemSerialNo);
            if (item != null)
            {
                parameters.Add(nameof(MotherboardDialog.Model), new MotherboardInfo()
                {
                    OemSerialNo = item.OemSerialNo,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    PurchaseDate = item.PurchaseDate,
                    Description = item.Description,
                    Price = item.Price
                });
                parameters.Add(nameof(MotherboardDialog.IsEditMode), true);
            }
        }
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<MotherboardDialog>(string.IsNullOrEmpty(oemSerialNo) ? "Add" : "Update", parameters, options);
        if (!(await dialog.Result).Cancelled)
        {
            OnSearch("");
        }
    }

    private async Task Delete(string id)
    {
        if ((await dialogService.ShowMessageBox("Confirm Delete?", "Are you sure want to delete this Motherboard? This action cannot be undone", yesText: "Delete", cancelText: "Cancel")) == true)
        {
            var response = await MasterHttpClient.DeleteAsync(id);
            OnSearch("");
            foreach (var message in response.Messages)
            {
                if (response.Succeeded)
                {
                    snackbar.Add(message, Severity.Success);
                }
                else
                {
                    snackbar.Add(message, Severity.Error);
                }
            }
        }
    }

    private void OnSearch(string value)
    {
        _searchString = value;
        _motherboardTable.ReloadServerData();
    }
}
