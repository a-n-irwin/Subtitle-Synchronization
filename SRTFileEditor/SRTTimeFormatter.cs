using System.Text;



namespace SRTFileEditor
{
    internal static class SRTTimeFormatter
    {
        // returns the time as zero
        public static string Zero
        {
            get => "00:00:00,000";
        }



        // takes the time in the SRT time string format and returns a milliseconds representation of it
        public static int TimeAsMilliseconds(string time)
        {
            int milliseconds = 0;

            // the standard format for srt element time is 00:00:00,000 representing hour:minutes:seconds,milliseconds

            // there are 3,600,000 milliseconds in an hour
            milliseconds += int.Parse(time.Substring(0, 2)) * 3600000;

            // there are 60,000 milliseconds in a minute
            milliseconds += int.Parse(time.Substring(3, 2)) * 60000;

            // there are 1000 milliseconds in a second
            milliseconds += int.Parse(time.Substring(6, 2)) * 1000;

            // get the remaining milliseconds
            milliseconds += int.Parse(time.Substring(9));

            return milliseconds;
        }



        // takes the time in milliseconds and converts it to an SRT time string format
        public static string TimeStringFromMilliseconds(int milliseconds)
        {
            // format is 00:00:00,000
            // string builder will be a buffer for 16 characters by default
            StringBuilder @string = new StringBuilder();
            int timeUnit;

            // append the number of hours: an hour is 3,600,000 milliseconds
            timeUnit = milliseconds / 3600000;

            // handle appending zero
            if (timeUnit < 10) @string.Append("0");

            @string.Append(timeUnit);
            @string.Append(":");

            // get the remnant amount of time when the hours are removed
            milliseconds = milliseconds % 3600000;




            // append the number of minutes: a minute is 60,000 milliseconds
            timeUnit = milliseconds / 60000;

            // handle appending zero
            if (timeUnit < 10) @string.Append("0");

            @string.Append(timeUnit);
            @string.Append(":");

            // get the remnant amount of time when the minutes are removed
            milliseconds = milliseconds % 60000;




            // append the number of seconds: a second is 1,000 milliseconds
            timeUnit = milliseconds / 1000;

            // handle appending zero
            if (timeUnit < 10) @string.Append("0");

            @string.Append(timeUnit);
            @string.Append(",");

            // get the remnant amount of time when the seconds are removed
            milliseconds = milliseconds % 1000;




            // append the number of milliseconds
            // handle appending zero
            if (milliseconds < 10) @string.Append("00");
            else if (milliseconds < 100) @string.Append("0");

            @string.Append(milliseconds);


            return @string.ToString();
        }


        
        // Returns two string objects containing the start and end time of the unit
        public static string[] SplitTime(string time)
        {
            string[] array = new string[2];

            int delimitIndex = time.IndexOf(" --> ");

            // the delimiter is " --> " because that is what separates the start and end time in a standard srt file
            array[0] = time.Substring(0, delimitIndex);
            array[1] = time.Substring(delimitIndex + 5);

            return array;
        }
    }
}
