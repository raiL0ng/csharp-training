
using System;
using System.IO;

namespace MyApp {
    internal class Program {
        static void Main(string[] args) {
            string? line, s;
            int n, pos;
            SortedDictionary<string, int> dict = new SortedDictionary<string, int>();
            try {
                StreamReader sr = new StreamReader("test.txt");
                line = sr.ReadLine();
                while (!String.IsNullOrEmpty(line)) {
                    n = line.Length;
                    pos = 0;
                    for (int i = 0; i < n; i++) {
                        if (!Char.IsLetter(line[i])) {
                            if (i - pos <= 0) {
                                pos++;
                                continue;
                            }
                            s = line.Substring(pos, i - pos).ToLower();
                            if (!dict.ContainsKey(s)) {
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
                    // Console.WriteLine(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                // foreach (var el in dict) {
                //     Console.WriteLine("{0}\t{1}", el.Key, el.Value);
                // }

            }
            catch(Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally {
                Console.WriteLine("\nВсе слова были успешно посчитаны\n");
            }
            var sortedDict = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            try {
                
                StreamWriter sw = new StreamWriter("words-freq.txt");
                foreach (var el in sortedDict) {
                    sw.WriteLine("{0}\t\t{1}", el.Key, el.Value);
                }
                sw.Close();
            }
            catch(Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally {
                Console.WriteLine("Все слова записаны в файл \"words-freq.txt\" в порядке убывания");
            }

        }
    }
}
