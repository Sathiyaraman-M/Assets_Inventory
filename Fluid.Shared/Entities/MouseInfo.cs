﻿namespace Fluid.Shared.Entities;

public class MouseInfo : IHardwareComponentInfo, IEquatable<MouseInfo>
{
    public bool Equals(MouseInfo other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return OemSerialNo == other.OemSerialNo &&
               Manufacturer == other.Manufacturer &&
               Model == other.Model &&
               IsWireless == other.IsWireless &&
               MachineId == other.MachineId;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((MouseInfo)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(OemSerialNo, Manufacturer, Model, IsWireless, Description, MachineId);
    }

    public static bool operator ==(MouseInfo left, MouseInfo right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(MouseInfo left, MouseInfo right)
    {
        return !Equals(left, right);
    }

    [Key]
    public string OemSerialNo { get; set; }

    public string Manufacturer { get; set; }

    public string Model { get; set; }

    public bool IsWireless { get; set; }

    public string Description { get; set; }
    public HardwareChangeMode HardwareChangeMode { get; set; }

    public UseStatus UseStatus { get; set; }

    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }
}
