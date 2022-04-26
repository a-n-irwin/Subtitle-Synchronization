## SRTFileEditor ##



I wrote this software so as to solve the problem of subtitle files not following
the words being said in a video.

What this software does is that it adjusts the subtitle file by a certain amount
of milliseconds specified. Negative milliseconds will adjust it backwards and positive
will move it forwards

**How the software works:**

The user inputs (or drags) the path to the subtitle file, and presses enter so it can be
located. Then the user enters the amount of adjustment in milliseconds (+ or -).

**Weaknesses:**

To understand the weakness of this software you have to imagine the subtitle file as
being a straight line with points at intervals, matched against the words being said in
the video. This software simply adjusts that line to properly match the words of the video
but the amount of adjustment forward or backwards is as per your judgement. 
Hence there are two major weaknesses:  
**1.** The user has to determine the magnitude of adjustment  
**2.** The software is built under the assumption that the subtitle:  
  &emsp;   **a.** Follows the SRT file standard  
  &emsp;   **b.** The time interval each caption takes to be displayed(that is, the time it  
  &emsp;&emsp;&ensp;spends on screen before it goes off) is appropriate.
