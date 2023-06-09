# ExternalFanProject

This is a project to help keep your laptop cool for the summer. It consists of a program that connects to an Arduino board and sends a signal to turn on or off a connected fan depending on the temperature of your CPU. 

It is in early stages, and has drawbacks discussed later, but I decided to share it anyway.

Youtube video: https://www.youtube.com/watch?v=5sFD1-F1M_Q

This project is composed of 2 parts:
- The Windows service
- The Arduino code

# Wiring

![](schematic.png)

# How to run the code

First, you need to open the solution in Microsoft's Visual Studio and compile it. 
Once compiled, to register this service in windows, open an administrator PowerShell prompt and run the following:

`sc.exe CREATE "MyServiceName" binPath= "C:\path\to\project.exe"`

Replace "MyServiceName" with the name you want for the service and "C:\path\to\service.exe" with the path to your executable that you just compiled. 
Usually it is found under the "\bin\Debug\" folder in Visual studio code project folder.

To start the service, also in an administrator PowerShell prompt:

`sc.exe start MyServiceName`

Again, replacing `MyServiceName` with the service name you chose before.

# Arduino code

Open the `external_fan.ino` sketch in the Arduino Code folder and uplaod it to your board. It uses pin 13 as the enable pin for the fan.

# Limitations

Some important limitations to take into account is that currently:

- The temperature thresholds cannot be dynamically selected.
- In order for the service to start, the Arduino needs to be plugged in.
- The program will stop working if the Arduino is disconected, and you will have to run the start command again

As I mentioned, this is a work-in-progress project, so this list of limitations might shrink, and new features could be added, not only to make the software more robust, but also more user firendly.
