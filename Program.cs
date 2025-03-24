﻿using System;
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
            // var lightTimeInMilliseconds = 500;
            // var dimTimeInMilliseconds = 500;
            // byte[] buffer = new byte[2];
            // UInt16 data = 0, config, sendValue;

            var serport = new SerialPort();

            // SPI
            // DAC on GertBoard on CS1
            // using '/dev/spidev0.1' (use chipSelect = 0 for spidev0.0)
            // var busId = 0;
            // var chipSelect = 1;
            // var settings = new SpiConnectionSettings(busId, chipSelect);

            // // create device to talk to DtoA (MCP4802)
            // SpiDevice spi = new UnixSpiDevice(settings);

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
                    


                    // Console.WriteLine($"Light for {lightTimeInMilliseconds}ms");
                    // controller.Write(pin, PinValue.High);
                    // Thread.Sleep(lightTimeInMilliseconds);
                    // Console.WriteLine($"Dim for {dimTimeInMilliseconds}ms");
                    // controller.Write(pin, PinValue.Low);
                    // Thread.Sleep(dimTimeInMilliseconds);
                    
                    // // data has to be sent as 16 bit value with CS low for both bytes
                    // // 
                    // config = 0x3000;    // Channel A, Gain = 1 (0011)
                    // // config = 0xB000;    // Channel B, Gain = 1 (1011)
                    // // config = 0x1000;    // Channel A, Gain = 2 (0001)
                    // // config = 0x9000;    // Channel B, Gain = 2 (1001)

                    // // data left shifted by 4 bits
                    // sendValue = (ushort)(data << 4);
                    // sendValue |= config;

                    // buffer[0] = (byte)((sendValue & 0xFF00) >> 8);
                    // buffer[1] = (byte)(sendValue & 0x00FF);
                    
                    // // send using Write(ReadOnlySpan) as WriteByte will raise CS between bytes 
                    // ReadOnlySpan<byte> span = buffer;
                    
                    // // write 16 bit value to DAC
                    // spi.Write(span);

                    // // increment data value
                    // data += 10;

                    // // reset data value
                    // if (data >= 256) data = 0;
                }
            }
        }
    }
}

