using System;
using System.Collections.Generic;

namespace SubtitleSynchronization;

public struct SynchronizationData
{
    public int Units { get; set; }
    public int Modified { get; set; }

    public override string ToString()
    {
        return $"Synchronization success rate: {Math.Round(this.Modified * 100.0 / this.Units, 2)}%\nUnits found: {this.Units}\nSuccessful mods: {this.Modified}";
    }
}
