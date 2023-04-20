using System;
using System.IO;

namespace MyApp {
    internal class Program {
        static void Main(string[] args) {
            string? line, s;
            int n, pos, maxlen = 0;
            Dictionary<string, int> dict = new Dictionary<string, int>();
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
                            if (maxlen < s.Length) maxlen = s.Length;
                            if (!dict.ContainsKey(s)) {
                                dict.Add(s, 0);
                            }   
                            dict[s] += 1;
                            pos = i + 1;
                        }
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
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
                maxlen += 2;
                string tabs;
                int tabs_cnt, tmp_len;
                foreach (var el in sortedDict) {
                    tabs_cnt = 0;
                    tabs = "";
                    tmp_len = maxlen - el.Key.Length;
                    while (tabs_cnt < tmp_len) {
                        tabs_cnt++;
                        tabs += ' ';
                    }
                    sw.WriteLine("{0}{1}{2}", el.Key, tabs, el.Value);
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
