namespace SRE.Program.WebAPI.BusinessLogics.Logistics.Entities;

public enum tracking_status_enum
{
    Created,

    PackageReceived,

    InTransit,

    OutforDelivery,

    DeliveryAttempted,

    Delivered,

    ReturnedtoSender,

    Exception
}
