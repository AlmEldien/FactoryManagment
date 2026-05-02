using FactoryManagment.Domain.Enums;

namespace FactoryManagment.Domain.Entities;

public class MachineDowntime
{
    public Guid           Id        { get; private set; }
    public DowntimeReason Reason    { get; private set; }
    public DateTime       StartedAt { get; private set; }
    public DateTime?      EndedAt   { get; private set; }

    // Navigation Properties:
    // ======================
    // 1) One Downtimes "Has" One Machine (one-to-many with Machine)
    public Guid    MachineId { get; private set; }    // Foreign Key
    public Machine Machine   { get; private set; } = null!;


    /// <summary>
    /// Calculates the actual downtime minutes that overlap with the given period.
    /// Handles cases where downtime started before or extends beyond the period.
    /// </summary>
    /// <param name="periodStart">The start of the reporting period.</param>
    /// <param name="periodEnd">The end of the reporting period.</param>
    /// <returns>Overlapping downtime in minutes, or 0 if no overlap.</returns>
    public double GetOverlapMinutes(DateTime periodStart, DateTime periodEnd)
    {
        var actualEnd = EndedAt ?? DateTime.UtcNow;

        var overlapStart = StartedAt > periodStart ? StartedAt : periodStart;

        var overlapEnd = actualEnd < periodEnd ? actualEnd : periodEnd;

        if (overlapEnd <= overlapStart)
            return 0;

        return (overlapEnd - overlapStart).TotalMinutes;
    }
}
