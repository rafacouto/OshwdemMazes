#!/bin/bash

echo 'Press <enter> to generate new maze and input "end" to exit.'

t=0
while read r ; do 
    [ "$r" == "end" ] && break 
    clear 
    echo -e "text 20,30\"\n$(echo '' | ../precompiled/MazeGenerator.exe \
	    | tail -n +4)\n\"" | tee maze.txt | tail -n +2 | head -n -1
    t=$((t+1)) 
    echo $t
done 
    
convert -size 690x565 xc:white -font "FreeMono" -pointsize 16 -fill black -draw @maze.txt maze.png 
convert maze.png maze_div.png -compose dissolve -define compose:args=20 -composite print.png

eog print.png 2> /dev/null &


