using System;
using SubtitleSynchronization;


public static class Program
{
    public static void Main()
    {
        var path = "sub.txt";
        SRTSynchronizer sync = new SRTSynchronizer();
        SynchronizationData data = sync.SyncByMilliseconds(path, 1000);
        Console.WriteLine(data);
    }
}
