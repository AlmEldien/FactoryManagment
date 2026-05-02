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


    private MachineDowntime() { }
    private MachineDowntime(Guid machineId, DowntimeReason reason, DateTime startedAt)
    {
        Id        = Guid.NewGuid();
        MachineId = machineId;
        Reason    = reason;
        StartedAt = startedAt;
    }


    /// <summary>Starts a new downtime record for the specified machine.</summary>
    /// <param name="machineId">The ID of the machine that went down.</param>
    /// <param name="reason">The enum reason for the downtime.</param>
    public static MachineDowntime Begin(Guid machineId, DowntimeReason reason)
    {
        if (machineId == Guid.Empty)
            throw new ArgumentException("Machine id is required!", nameof(machineId));

        return new MachineDowntime(machineId, reason, DateTime.UtcNow);
    }

    /// <summary>
    /// Marks this downtime as ended. Throws if already ended.
    /// </summary>
    public void End()
    {
        if (EndedAt.HasValue)
            throw new InvalidOperationException("This downtime is already ended!");

        EndedAt = DateTime.UtcNow;
    }

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
