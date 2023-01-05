# hexapod_move_absolute_C_sharp_console
This C# program moves absolutely the single axis of the hexapod. 

The C# program asks if you want to move single axis of the hexapod abolutely. If yes, it asks you for axis you want to move like X/Y/Z/U/V/W, and also asks you for absolute movement in mm. Finally, it moves single axis of the hexapod. Then, the console application asks you if you want to read current position of the hexapod. If answer is yes (Y OR y), it sends command POS? to hexapod and current hexapod position is read and printed into the console appliaction. If answer is different from yes (different from Y or y), the program escapes while loop for position reading and asks you if you want to move hexapod again. 

The C# program asks for IP address of hexapod, for example 147.213.112.19, and port of hexapod, for example 50000. It creates session with hexapod using TCPClient in the line 25. In the line 26, the NetworkStream is established. At the end, the network stream is flushed and closed. The TCP session with hexapod is also closed.  
