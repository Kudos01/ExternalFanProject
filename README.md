# ExternalFanProject

This is a project to help keep your laptop cool for the summer. It consists of a program that connects to an Arduino board and sensds a signal to turn on or off a connected fan depending on the temperature of your CPU. 

It is in early stages and has drawbacks discussed on my [website post][https://littlebigtech.net/], but I decided to share it anyway.

This project is composed of 2 parts:
- The Windows service
- The Arduino code

# How to run the code

First, you need to open the solution in Microsoft's Visual Studio and compile it. 
Once compiled, to register this service in windows, open an administrator PowerShell prompt and run the following:

`sc.exe CREATE "MyServiceName" binPath= "C:\path\to\project.exe"`

Replace "MyServiceName" with the name you want for the service and "C:\path\to\service.exe" with the path to your executable that you just compiled. 
Usually it is found under the "\bin\Debug\" folder in Visual studio code project folder.
