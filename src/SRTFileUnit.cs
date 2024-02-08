using System;
using System.Collections.Generic;
using System.Text;



namespace SRTFileEditor
{
    // This class represents just one unit of the subtitle file - an element. A unit comprises an index number, a time frame
    // and a caption, and they are stored in that order
    internal class SRTFileUnit
    {
        // Retrieves the innder dictionary which stores the items. It can't be modified through this property
        public SortedDictionary<int, string> Items { get; private set; }

        // Constructs an SRTFileUnit object with empty
        public SRTFileUnit()
        {
            Items = new SortedDictionary<int, string>();
        }
        //
        // Set the value of items by their indices. The order of the items are: 
        //
        // 0 -> index of the unit
        // 1 -> start time frmae of the unit
        // 2 -> end time frame of the unit
        // 3 -> caption
        //
        // Note that items 1 and 2 are just two parts of one thing - the time frame. Also, in cases where
        // the unit's caption is divided into multiple lines the index from 3 and beyond (usually 3 and 4)
        // would represent parts of the caption
        public void SetItemByIndex(int index,string value)
        {
            Items.Add(index, value);
        }
        //
        // 
        // get the value of items by their indices. The order of the items are: 
        //
        // 0 -> index of the unit
        // 1 -> start time frmae of the unit
        // 2 -> end time frame of the unit
        // 3 -> caption
        //
        // Note that items 1 and 2 are just two parts of one thing - the time frame. Also, in cases where
        // the unit's caption is divided into multiple lines the index from 3 and beyond (usually 3 and 4)
        // would represent parts of the caption
        public string GetItemByIndex(int index)
        {
            return Items[index];
        }


        public void SetStartTimeInMilliseconds(int milliseconds)
        {
            Items[1] = (milliseconds > 0) ? SRTTimeFormatter.TimeStringFromMilliseconds(milliseconds) : SRTTimeFormatter.Zero;
        }

        
        public void SetEndTimeInMilliseconds(int milliseconds)
        {
            Items[2] = (milliseconds > 0) ? SRTTimeFormatter.TimeStringFromMilliseconds(milliseconds) : SRTTimeFormatter.Zero;
        }


        // get the time as milliseconds for easy modification
        public int StartTimeAsMilliseconds()
        {
            return SRTTimeFormatter.TimeAsMilliseconds(Items[1]);
        }


        // get the time as milliseconds for easy modification
        public int EndTimeAsMilliseconds()
        {
            return SRTTimeFormatter.TimeAsMilliseconds(Items[2]);
        }


        // Clears the items 
        public void SetItemsToDefault()
        {
            Items.Clear();
        }


        // Returns the time frame for this unit
        public string GetTime()
        {
            return Items[1] + " --> " + Items[2];
        }
    }
}
