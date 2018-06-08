using System;
using System.IO;
using System.Text;

namespace Lab03_Word_Guess_Game
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../storedWords.txt";
            Console.WriteLine("Hello World!");
            CreateFile(path);
        }

        /// <summary>
        /// This method creates a new file if there is none. It then writes some words into the file.
        /// It requires a path to indicate where it should look for/make a file.
        /// </summary>
        /// <param name="path"></param>
        static void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter fileNew = new StreamWriter(path))
                {
                    fileNew.WriteLine("cat");
                    fileNew.WriteLine("dog");
                    fileNew.WriteLine("mouse");
                    fileNew.WriteLine("tea");
                    fileNew.WriteLine("sun");
                    fileNew.WriteLine("hello");
                    fileNew.WriteLine("world");
                    fileNew.WriteLine("code fellows");
                    fileNew.WriteLine("pull request");
                }
            }
        }
    }
}
