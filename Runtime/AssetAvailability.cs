using System;

public enum AssetAvailability
{
    NotAvailable,
    LocallyAvailable,
    RemotelyAvailable,
}

public static class AssetAvailabilityExtensions
{
    public static AssetAvailability GetCommon(AssetAvailability a1, AssetAvailability a2, AssetAvailability a3)
    {
        return GetCommon(GetCommon(a1, a2), a3);
    }

    public static AssetAvailability GetCommon(AssetAvailability a1, AssetAvailability a2)
    {
        switch (a1, a2)
        {
            case (AssetAvailability.NotAvailable, _):
            case (_, AssetAvailability.NotAvailable):
                return AssetAvailability.NotAvailable;

            case (AssetAvailability.RemotelyAvailable, _):
            case (_, AssetAvailability.RemotelyAvailable):
                return AssetAvailability.RemotelyAvailable;

            case (AssetAvailability.LocallyAvailable, AssetAvailability.LocallyAvailable):
                return AssetAvailability.LocallyAvailable;

            default:
                throw new ArgumentOutOfRangeException(nameof(a1), a1,
                    $"Unexpected asset availability: ({a1},{a2})");
        }
    }

    public static bool IsAvailableLocallyOrRemotely(this AssetAvailability a)
    {
        return a == AssetAvailability.LocallyAvailable || a == AssetAvailability.RemotelyAvailable;
    }
}
