// File: Program.cs
// License: 
// Please see readme.md

namespace ImBatch
{
    using System;
    using System.IO;

    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                System.Console.WriteLine("imbatch.exe src commands.xml dst");
                return;
            }

            var source = args[0];
            var batchsource = args[1];
            var target = args[2];

            try
            {
                var batcher = new ImageBatcher();
                batcher.LoadFromFile(batchsource);
                var folder = Path.GetDirectoryName(source);
                var filter = Path.GetFileName(source);
                long num = 0;
                foreach (var file in Directory.EnumerateFiles(folder, filter))
                {
                    try
                    {
                        Console.Write("Processing {0}... ", file);
                        batcher.Apply(num, folder, target, Path.GetFileName(file));
                        Console.WriteLine(" done.");
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("Error: {0}", e.Message);
                    }
                    ++num;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Fatal error: {0}", e.Message);
            }
        }
    }
}
