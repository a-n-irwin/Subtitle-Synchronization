# Version 1.0.0: SRTFileEditor #



I wrote this software to experiment with SRT subtitle files. I normally ran into minor issues with subtitles,  
where they appear earlier or later than the words being said. This software provides a simple solution.

What this software does is to adjusts the start- and end-time of each line (or caption) of the subtitle file  
by a certain amount of milliseconds specified. Negative milliseconds will adjust it to the left(backwards) and  
positive will move it towards the right(forwards)<br><br>


### How the software works: ###  
The user inputs (or drags) the path to the subtitle file into the console, and presses enter so it can be located.  
Then the user enters the amount of adjustment in milliseconds (+ or -).<br><br>

### Weaknesses: ###
To understand the weakness of this software you have to imagine the subtitle file as being a straight line with  
points at intervals, matched against the words being said in the video. This software simply adjusts that line to  
properly match the words of the video but the amount of adjustment forward or backwards is as per your  
judgement. So, there are two major weaknesses:  
**1.** The user has to determine the magnitude and direction of adjustment  
**2.** The software is built under the assumption that the subtitle:  
  &emsp;   **a.** Follows the SRT file standard  
  &emsp;   **b.** The time interval each caption takes to be displayed(that is, the time it spends on screen before it goes off)  
  &emsp;&emsp; is appropriate.
