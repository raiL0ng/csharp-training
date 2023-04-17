
using System;
using System.IO;

namespace MyApp // Note: actual namespace depends on the project name.
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            string? line, s;
            int n, pos;
            try
            {
                StreamReader sr = new StreamReader("test.txt");
                line = sr.ReadLine();
                Dictionary<string, int> dict = new Dictionary<string, int>();
                while (!String.IsNullOrEmpty(line))
                {
                    n = line.Length;
                    pos = 0;
                    for (int i = 0; i < n; i++) {
                        if (!Char.IsLetter(line[i]))
                        {
                            s = line.Substring(pos, i - pos);
                            if (!dict.ContainsKey(s))
                            {
                                dict.Add(s, 0);
                            }   
                            dict[s] += 1;
                            pos = i + 1;
                            // foreach (var el in dict)
                            // {
                            //     Console.WriteLine("Key: \"{0}\", Value: {1}", el.Key, el.Value);
                            // }
                        }
                    }
                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                foreach (var el in dict)
                {
                    Console.WriteLine("Key: \"{0}\", Value: {1}", el.Key, el.Value);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("\nExecuting finally block.");
            }
        }
    }
}
