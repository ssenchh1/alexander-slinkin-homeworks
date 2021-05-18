using System;
using System.Collections;
using System.IO;

namespace Practice17may
{
    public class OpenCommand : Command
    {
        public override void Execute(string argument, ref string currentpath)
        {
            try
            {
                Console.WriteLine();

                var path = currentpath + argument;

                var file = new FileInfo(path);
                var extension = file.Extension;

                if (extension == ".txt" || extension == ".log" || extension == ".json")
                {
                    using (var sr = file.OpenText())
                    {
                        var text = sr.ReadToEnd();

                        foreach (var line in text.Split("\r\n"))
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
                else
                {
                    using (var fs = file.OpenRead())
                    {
                        var buffer = new byte[1024];
                        var bytesRead = 0;

                        var bytes = fs.Read(buffer, bytesRead, buffer.Length);
                        bytesRead += bytes;

                        var bits = new BitArray(buffer);

                        foreach (var bit in bits)
                        {
                            var value = Convert.ToInt32((bool) bit);
                            Console.Write(value);
                        }
                    }
                }

                Console.WriteLine();
            }
            catch (FileNotFoundException exc)
            {
                Console.WriteLine("Данного файла не существует." + exc.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine();
            }
        }
    }
}