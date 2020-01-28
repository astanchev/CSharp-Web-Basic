namespace Chronometer
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            IChronometer chronometer = new Chronometer();

            while (true)
            {
                string inputLine = Console.ReadLine();
                if (inputLine == "exit")
                {
                    break;
                }

                switch (inputLine)
                {
                    case "start":
                    {
                        chronometer.Start();
                        break;
                    }
                    case "stop":
                    {
                        chronometer.Stop();
                        break;
                    }
                    case "lap":
                    {
                        Console.WriteLine(chronometer.Lap());
                        break;
                    }
                    case "time":
                    {
                        Console.WriteLine(chronometer.GetTime);
                        break;
                    }
                    case "laps":
                    {
                        Console.WriteLine("Laps: " + 
                                          (chronometer.Laps.Count == 0 ? 
                                              "no laps" : 
                                              "\r\n" + string.Join("\r\n", 
                                                  chronometer
                                                      .Laps
                                                      .Select((lap, index) => $"{index}. {lap}"))));
                        break;
                    }
                    case "reset":
                    {
                        chronometer.Reset();
                        break;
                    }
                }

            }
        }
    }
}
