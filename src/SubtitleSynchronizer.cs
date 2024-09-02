using System;

namespace SubtitleSynchronization;

public abstract class SubtitleSynchronizer
{
    // This will be the same for all child classes, so there is no
    // need for child classes to have their own implementation. The
    // method itself, when invoked by child classes, will call the inherited 
    // SyncBySeconds() which will in turn call the child's implementation of
    // SyncByMilliseconds()
    public virtual SynchronizationData SyncBySeconds(string path, double seconds)
    {
        return SyncByMilliseconds(path, (int)(1000 * seconds));
    }

    public virtual SynchronizationData SyncByMilliseconds(string path, int milliseconds)
    {
        // Only thrown when a child fails to provide an implementation of this method
        throw new NotImplementedException(
            $"{this.GetType()} does not implement SubtitleSynchronizer.SyncByMilliseconds(string, int)"
        );
    }

    public abstract bool ParseTimeStringToMilliseconds(string time, out int start, out int end);

    // This returns a string instead of a bool because it will always parse correctly
    // since an integer input really can't contain anything else asides an integr
    public abstract string ParseMillisecondsToTimeString(int start, int end);
}
