using System;
using System.Device.Gpio;
using System.Threading;
using System.IO.Ports;
using System.Threading.Tasks;
// using System.Device.Spi;
// using System.Device.Spi.Drivers;

namespace gpiotest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var pin1 = 17;
            var pin2 = 18;
            bool flag = true;

            try
            {
                var serport = new SerialPort()
                {
                    PortName = "/dev/serial0",
                    BaudRate = 9600,
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One
                };

                serport.Open();

                using (var controller = new GpioController())
                {
                    controller.OpenPin(pin1, PinMode.Output);
                    Console.WriteLine($"GPIO pin configured for output: {pin1}");
                    controller.OpenPin(pin2, PinMode.Output);
                    Console.WriteLine($"GPIO pin configured for output: {pin2}");

                    Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                    {
                        controller.Dispose();
                    };

                    while (true)
                    {
                        if (flag)
                        {
                            controller.Write(pin1, PinValue.Low);
                            controller.Write(pin2, PinValue.High);
                        }
                        else
                        {
                            controller.Write(pin1, PinValue.High);
                            controller.Write(pin2, PinValue.Low);
                        }

                        flag = !flag;

                        await Task.Delay(1000);
                        

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

        }
    }
}

