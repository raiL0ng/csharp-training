using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MyApp {
    internal class Program {
        static void Main(string[] args) {
            string? text, filename;
            int maxlen = 0;
            Dictionary<string, int> dict = new Dictionary<string, int>();
            Console.WriteLine("Введите имя файла для считывания:");
            filename = Console.ReadLine();
            if (filename != null) {
                try {
                    text = File.ReadAllText(filename);
                    text = text.ToLower();
                    Regex regular_exp = new Regex("[^а-яa-z\']");
                    text = regular_exp.Replace(text, " ");
                    string[] words = text.Split( new char[] {' '}
                                               , StringSplitOptions.RemoveEmptyEntries);
                    foreach (var el in words) {
                        if (maxlen < el.Length) maxlen = el.Length;
                        if (!dict.ContainsKey(el)) dict.Add(el, 0);
                        dict[el] += 1;
                    }
                    Console.WriteLine("\nВсе слова были успешно посчитаны\n");
                    var sortedDict = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
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
                    Console.WriteLine("Ошибка: " + e.Message);
                    Environment.Exit(0);

                }
                finally {
                    Console.WriteLine("Все слова записаны в файл \"words-freq.txt\" в порядке убывания");
                }
            }
        }
    }
}
