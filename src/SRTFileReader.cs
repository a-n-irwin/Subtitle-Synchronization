using System.IO;



namespace SRTFileEditor
{
    internal class SRTFileReader
    {
        // the subtitle file would be read in units 
        private SRTFileUnit _unit;
        private string _path;

        public string Path
        {
            set
            {
                // in cases where the file is dragged into the command line, quotes enclose the path
                if (value[0] == value[value.Length - 1] && value[0] == '"')
                    value = value.Substring(1, value.Length - 2);

                // guarantee we can find the file before proceeding
                if (!File.Exists(value)) throw new FileNotFoundException();

                _path = value;
            }
            get => _path;
        }

        // open the file for reading
        FileStream     _file;
        StreamReader  _reader;

        FileStream    _outFile;
        StreamWriter  _writer;



        public SRTFileReader()
        {
            _path = string.Empty;
            _unit = new SRTFileUnit();
        }
        
        // Constructs an SRTFileReader object with a file path
        public SRTFileReader(string path)
        {
            // in cases where the file is dragged into the command line, quotes enclose the path
            if (path[0] == path[path.Length - 1] && path[0] == '"')
                path = path.Substring(1, path.Length - 2);

            // guarantee we can find the file before proceeding
            if (!File.Exists(path)) throw new FileNotFoundException();

            _path = path;
            _unit = new SRTFileUnit();
        }
       
        // Adjusts the start and end time of the SRT file by the amount of milliseconds.A positive value adjusts
        // the start and end time to the right, and a negative value adjusts it to the left
        public void AdjustTime(int milliseconds)
        {
            _file = new FileStream(_path, FileMode.Open, FileAccess.Read);
            _reader = new StreamReader(_file);

            _outFile = new FileStream(_path + ".output.srt", FileMode.Create, FileAccess.Write);
            _writer = new StreamWriter(_outFile);

            // to store each line read from the srt file 
            string line;
            int counter = 0;

            // while not at the end of the file
            while ((line = _reader.ReadLine()) != null)
            {
                // keep adding items to the unit until we come across an empty line
                if (line != string.Empty)
                {
                    // there are times when srt files end in space characters, thus the above condition will be true
                    // the below line thus will stop further execution from recording this invalid unit
                    if (counter == 0 && (line[0] == '\t' || line[0] == ' '))
                        break;


                    if (counter != 1) _unit.SetItemByIndex(counter++, line);
                    // remember that we want to split the time up even though it is just one line from the srt file
                    // this is done to ease operation, that is why we are giving index 1 special treatment here
                    else
                    {
                        // split the line into a start and end
                        string[] time = SRTTimeFormatter.SplitTime(line);

                        // add the times as separate unit items
                        _unit.SetItemByIndex(counter++, time[0]);
                        _unit.SetItemByIndex(counter++, time[1]);
                    }
                }
                // when we come across an empty line, we want to perform our operation and reset the unit and the counter
                else
                {
                    // At the end of SRT files, there are usually multiple empty lines. The below check is meant to prevent the writing of an empty unit. Since we reset the counter
                    // to 0 after writing a unit, then if the counter is still 0 on the next write operation then we haven't read any new units. the continue statement is used instead
                    // of return or break to give the while loop more chances to check for more units (in case there are more than one lines between units)
                    if (counter == 0) continue; 

                    // reduce the start and end time by the amount of milliseconds
                    _unit.SetStartTimeInMilliseconds(_unit.StartTimeAsMilliseconds() + milliseconds);
                    _unit.SetEndTimeInMilliseconds(_unit.EndTimeAsMilliseconds() + milliseconds);


                    // write this update to the output file
                    for (int i = 0; i < _unit.Items.Count; ++i)
                    {
                        if (i != 1) _writer.WriteLine(_unit.GetItemByIndex(i));
                        // the index 1 of the actual subtitle srt file contains the full time, that is why we give it special
                        // treatment as we have divided our time into a start and an end while storing subtitle units
                        else
                        {
                            // split the line into a start and end
                            string[] time = SRTTimeFormatter.SplitTime(_unit.GetTime());

                            // write the index 1 and index 2. the loop will do the other increment to 3
                            _writer.WriteLine(_unit.GetItemByIndex(i++) + " --> " + _unit.GetItemByIndex(i));
                        }
                    }
                    // write the empty line separator
                    _writer.WriteLine();

                    // reset the counter and the unit so we can read a new unit
                    counter = 0;
                    _unit.SetItemsToDefault();
                }
            }
            // close the streams
            _reader.Close();
            _writer.Close();
        }
    }
}
