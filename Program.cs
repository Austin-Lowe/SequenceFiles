using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SequenceFiles
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Console.WriteLine("Please add a directory:");
            string input = Console.ReadLine();

            DirectoryInfo dir = new DirectoryInfo(input);

            FileInfo[] infos = dir.GetFiles();

            //string[] filepaths = Directory.GetFiles(dir, "*.sql");

            int value = 0;
            string lead = "";

            foreach (FileInfo file in infos)
            {
                Match result = Regex.Match(file.Name, @"^.*?(?=_)");
                string nName = file.Name;

                if (String.IsNullOrEmpty(result.Value))
                {

                    if (value.ToString().Length == 1)
                    {
                        value = value + 1;
                        lead = "00" + value.ToString() + "0_";
                    }
                    else if (value.ToString().Length == 2)
                    {
                        value = value + 1;
                        lead = "0" + value.ToString() + "0_";
                    }
                    else if (value.ToString().Length == 3)
                    {
                        value = value + 10;
                        lead = "0" + value.ToString() + "_";
                    }
                }
                else
                {
                    //Need to fix regex to only pull the numbers or handle filenames with Underscores in them.
                    //Scripts folder is issue
                    value = Int32.Parse(result.Value);
                }

                nName = lead + file.Name;

                File.Move(file.FullName, file.FullName.Replace(file.Name, nName));

                Console.WriteLine(file.FullName.Replace(file.Name, nName));

            }
        }
    }
}
