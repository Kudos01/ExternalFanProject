using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;
using LattePanda.Firmata;
namespace Get_CPU_Temp
{
    class Program
    {
        static Arduino arduino = new Arduino();
        public class UpdateVisitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }
            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
            }
            public void VisitSensor(ISensor sensor) { }
            public void VisitParameter(IParameter parameter) { }
        }
        static int GetCpuTemp(Computer computer, UpdateVisitor updateVisitor)
        {
            int Cputemp = 0;
            //Computer computer = new Computer();
            //computer.Open();
            //computer.CPUEnabled = true;
            computer.Accept(updateVisitor);
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                if (computer.Hardware[i].HardwareType == HardwareType.CPU)
                {
                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                        {
                            Cputemp += (int)computer.Hardware[i].Sensors[j].Value;
                        }
                    }
                }
            }
            
            return Cputemp;
        }
        static void Main(string[] args)
        {
            bool stateChange = false;
            byte state = Arduino.LOW;

            //Set Arduino Pin to output and also turn it off when starting the application
            arduino.pinMode(0, Arduino.OUTPUT);
            arduino.digitalWrite(13, state);

            // Create the computer object used to query temperature.
            Computer computer = new Computer();

            // Init
            computer.Open();
            computer.CPUEnabled = true;

            while (true)
            {
                UpdateVisitor updateVisitor = new UpdateVisitor();
                int temp = GetCpuTemp(computer, updateVisitor) / 8;
                Console.WriteLine(temp);
                if (temp >= 70 && state == Arduino.LOW)
                    stateChange = true;
                else if (temp < 60 && state == Arduino.HIGH)
                    stateChange = true;

                if (stateChange)
                    {
                    Console.WriteLine("Sending stuff");
                    state = state==Arduino.LOW ? Arduino.HIGH : Arduino.LOW;
                    arduino.digitalWrite(13, state);//
                    stateChange = false;
                }
            }
        }
    }
}