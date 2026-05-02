namespace FactoryManagment.Domain.Enums;

public enum DowntimeReason
{
    Breakdown,          // Unexpected equipment failure
    PlannedMaintenance, // Scheduled servicing or inspection
    MaterialShortage,   // Production stopped due to insufficient materials
    Changeover,         // Switching production to a different product type
    Other               // Any reason not covered above
}
