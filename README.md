# OSHWDEM Mazes

[OpenSource Hardware Demontration][OSH01] is the most important barcamp of makers in Galicia. Since 2014, there are robot challenges and everybody is invited to participate.

One of the contest is the _Maze with robots_. This program is intended to be the maze generator according to the specific rules of the contest. It is compiled and executed on the big screen just in the beginning of the competition and the real maze is configured with the walls as shown on the big screen.

The source code is published so that you can study and generate mazes in order to audit the code and practice with maze configurations similar to the competition ones.

![Executing Maze Generator](img01.png)



## Run the Maze Generator

There is a precompiled .NET binary and it should be enought to execute the program: _MazeGenerator.exe_ Try to execute with the command:

    mono MazeGenerator.exe

If something was wrong with the command, you probably need the Mono runtime. Debian flavours provide a Mono package and it is easy to install:

    sudo apt-get install mono-runtime

See [the official _How to install Mono_][MON01] if you have another GNU/Linux distro.

Note for Windoze users: it should work from a console. However, we do NOT offer support for privative operating systems.



## Compile the Maze Generator

Source code includes a _OshwdemMazes.sln_ file. Install MonoDevelop, open it and build the code. It generates a new binary under the _bin_ folder.

To install MonoDevelop:

    sudo apt-get install monodevelop


## License

Version 3 of the GNU General Public License (GPLv3). See LICENSE.txt.



## Extra commands

Command line forever!


### Massive maze generation

Generate 10 mazes and save them in a file:

    for T in $(seq 10) ; do echo "Thanks" | mono MazeGenerator.exe >> mazes.txt ; done


### Show and save at the same time

Save the maze in a file at the moment of generate it:

    mono MazeGenerator.exe | tee -a mazes.txt





[MON01]: http://www.mono-project.com/docs/getting-started/install/linux/
[OSH01]: http://oshwdem.org/
