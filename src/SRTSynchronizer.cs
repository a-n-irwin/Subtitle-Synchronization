using System;
using System.IO;

namespace SubtitleSynchronization;

public class SRTSynchronizer : SubtitleSynchronizer
{
    // For writing text to the files
    private StreamReader _reader;
    private StreamWriter _writer;


    public SRTSynchronizer()
    {

    }

    public override SynchronizationData SyncByMilliseconds(string path, int milliseconds)
    {
        var data = new SynchronizationData();
        var line = String.Empty;
        var isEntryPoint = false;

        _reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
        _writer = new StreamWriter(new FileStream($"{path}.synched.srt", FileMode.Create, FileAccess.Write));

        while ((line = _reader.ReadLine()) != null)
        {
            // Safe to write because a valid SRT unit wouldn't start with a timeframe
            _writer.WriteLine(line);
            isEntryPoint = !isEntryPoint && int.TryParse(line, out int index);

            if (isEntryPoint)
            {
                data.Units++;
                // It is expected that the timeframe should come right after the index (entry point) line
                line = _reader.ReadLine();
                bool parsed = ParseTimeStringToMilliseconds(line, out int start, out int end);

                if (parsed)
                {
                    // Modifies the start and the end, making sure they don't go below 0 then
                    // returns them back as an SRT time string
                    string time = ParseMillisecondsToTimeString(Math.Max(start + milliseconds, 0), Math.Max(end + milliseconds, 0));
                    _writer.WriteLine(time);
                    data.Modified++;
                }
                else _writer.WriteLine(line);

                // Until we an empty line (Unit terminator)
                while ((line = _reader.ReadLine()?.Trim()) != null && line != String.Empty)
                    _writer.WriteLine(line);

                // Write the terminator
                if (line != null) _writer.WriteLine();

                isEntryPoint = false;
            }
        }

        _reader.Close();
        _writer.Close();

        return data;
    }

    public override bool ParseTimeStringToMilliseconds(string time, out int start, out int end)
    {
        start = end = int.MinValue;

        int separatorIndex = time.IndexOf("-->");
        if (separatorIndex == -1) return false;

        string startTimestamp = time.Substring(0, separatorIndex);
        string endTimestamp = time.Substring(separatorIndex + 3);

        return ParseTimestampToMilliseconds(startTimestamp, out start) && ParseTimestampToMilliseconds(endTimestamp, out end);
    }


    public override string ParseMillisecondsToTimeString(int start, int end)
    {
        return $"{ParseMillisecondsToTimestamp(start)} --> {ParseMillisecondsToTimestamp(end)}";
    }


    private bool ParseTimestampToMilliseconds(string timestamp, out int milliseconds)
    {
        milliseconds = int.MinValue;

        // Remove unnecessary whitespaces, if any, at the beginning and end
        timestamp = timestamp.Trim();

        // The expected timestamp format: hr:min:sec,millsec (i.e 00:00:00,000)
        // Anything else, no matter how trivial, will likely result in failed parsing
        try
        {
            // Get the hours, minutes, seconds, and milliseconds respectively
            milliseconds = int.Parse(timestamp.Substring(0, 2)) * 3600000;
            milliseconds += int.Parse(timestamp.Substring(3, 2)) * 60000;
            milliseconds += int.Parse(timestamp.Substring(6, 2)) * 1000;
            milliseconds += int.Parse(timestamp.Substring(9));
        }
        catch (FormatException)
        {
            return false;
        }

        return true;
    }

    public string ParseMillisecondsToTimestamp(int milliseconds)
    {
        int hours = milliseconds / 3600000;
        milliseconds %= 3600000;

        int minutes = milliseconds / 60000;
        int _milliseconds = milliseconds % 60000;
        int seconds = _milliseconds / 1000;

        _milliseconds %= 1000;

        return $"{hours:D2}:{minutes:D2}:{seconds:D2},{_milliseconds:D3}";
    }
}