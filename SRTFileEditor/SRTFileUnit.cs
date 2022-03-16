using System;
using System.Collections.Generic;
using System.Text;



namespace SRTFileEditor
{
    // This class represents just one unit of the subtitle file - an element. A unit comprises an index number, a time frame
    // and a caption, and they are stored in that order
    internal class SRTFileUnit
    {
        // we will use a dictionary to associate an index with an item in the element, such as the time or caption
        private SortedDictionary<int, string> _items;



        
        // Retrieves the innder dictionary which stores the items. It can't be modified through this property
        public SortedDictionary<int,string> Items
        {
            get
            {
                return _items;
            }
        }





        // Constructs an SRTFileUnit object with empty
        public SRTFileUnit()
        {
            _items = new SortedDictionary<int, string>();
        }
        //
        // 
        //     Set the value of items by their indices. The order of the items are: 
        //
        //     0 -> index of the unit
        //     1 -> start time frmae of the unit
        //     2 -> end time frame of the unit
        //     3 -> caption
        //
        //     Note that items 1 and 2 are just two parts of one thing - the time frame. Also, in cases where
        //     the unit's caption is divided into multiple lines the index from 3 and beyond (usually 3 and 4)
        //     would represent parts of the caption
        public void SetItemByIndex(int index,string value)
        {
            _items.Add(index, value);
        }
        //
        // 
        //     get the value of items by their indices. The order of the items are: 
        //
        //     0 -> index of the unit
        //     1 -> start time frmae of the unit
        //     2 -> end time frame of the unit
        //     3 -> caption
        //
        //     Note that items 1 and 2 are just two parts of one thing - the time frame. Also, in cases where
        //     the unit's caption is divided into multiple lines the index from 3 and beyond (usually 3 and 4)
        //     would represent parts of the caption
        public string GetItemByIndex(int index)
        {
            return _items[index];
        }



        public void SetStartTimeInMilliseconds(int milliseconds)
        {
            _items[1] = (milliseconds > 0) ? SRTTimeFormatter.TimeStringFromMilliseconds(milliseconds) : SRTTimeFormatter.Zero;
        }

        

        public void SetEndTimeInMilliseconds(int milliseconds)
        {
            _items[2] = (milliseconds > 0) ? SRTTimeFormatter.TimeStringFromMilliseconds(milliseconds) : SRTTimeFormatter.Zero;
        }



        // get the time as milliseconds for easy modification
        public int StartTimeAsMilliseconds()
        {
            return SRTTimeFormatter.TimeAsMilliseconds(_items[1]);
        }


        // get the time as milliseconds for easy modification
        public int EndTimeAsMilliseconds()
        {
            return SRTTimeFormatter.TimeAsMilliseconds(_items[2]);
        }




        // Clears the items 
        public void SetItemsToDefault()
        {
            Items.Clear();
        }



        
        // Returns the time frame for this unit
        public string GetTime()
        {
            return _items[1] + " --> " + _items[2];
        }
    }
}
