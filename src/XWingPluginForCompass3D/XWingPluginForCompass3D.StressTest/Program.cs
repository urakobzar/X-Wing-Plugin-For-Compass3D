using XWingPluginForCompass3D.Wrapper;
using XWingPluginForCompass3D.Model;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.IO;

namespace XWingPluginForCompass3D.StressTest
{
    /// <summary>
    /// Класс для нагрузочного тестирования.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Основной метод класса для запуска нагрузочного тестирования.
        /// </summary>
        private static void Main()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var xWingBuilder = new XWingBuilder();
            var xWing = new XWing();
            xWing.SetParameters(300, 300, 50, 
                80, 150, 50,
                10);
            var streamWriter = new StreamWriter($"StressTest.txt", true);
            var modelCounter = 0;
            var computerInfo = new ComputerInfo();
            ulong usedMemory = 0;
            while (usedMemory*0.96 <= computerInfo.TotalPhysicalMemory)
            {
                xWingBuilder.BuildDetail(xWing);
                usedMemory = (computerInfo.TotalPhysicalMemory - computerInfo.AvailablePhysicalMemory);
                streamWriter.WriteLine(
                    $"{++modelCounter}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");
                streamWriter.Flush();
            }
            stopWatch.Stop();
            streamWriter.WriteLine("END");
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
}