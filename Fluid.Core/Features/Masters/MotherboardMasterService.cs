﻿using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class MotherboardMasterService : IMotherboardMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public MotherboardMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<MotherboardInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            var specification = new MotherboardInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<MotherboardInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<MotherboardInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<MotherboardInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<MotherboardInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var motherboardInfo = await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(oemSerialNo);
            return motherboardInfo is not null ? await Result<MotherboardInfo>.SuccessAsync(motherboardInfo) : throw new Exception("Motherboard not found");
        }
        catch (Exception e)
        {
            return await Result<MotherboardInfo>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(MotherboardInfo model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"Motherboard with OEM Serial Number {model.OemSerialNo} already exists");
            await _unitOfWork.GetRepository<MotherboardInfo>().AddAsync(model);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(model.OemSerialNo, "Added Motherboard successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(MotherboardInfo model)
    {
        try
        {
            var oldMotherboardInfo = await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldMotherboardInfo is null) throw new Exception("Motherboard not found");
            await _unitOfWork.GetRepository<MotherboardInfo>().UpdateAsync(model, model.OemSerialNo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(model.OemSerialNo, "Updated Motherboard successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> DeleteAsync(string oemSerialNo)
    {
        try
        {
            var motherboardInfo = await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(oemSerialNo);
            if (motherboardInfo is null) throw new Exception("Motherboard not found");
            await _unitOfWork.GetRepository<MotherboardInfo>().DeleteAsync(motherboardInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(oemSerialNo);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}
