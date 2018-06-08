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
            try
            {
                string word = ReadAFile(path);

            }
            catch (Exception)
            {
                Console.WriteLine("Sorry, something went wrong with reading the file.");
            }
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
                    fileNew.WriteLine("tea");
                    fileNew.WriteLine("sun");
                    fileNew.WriteLine("mouse");
                    fileNew.WriteLine("hello");
                    fileNew.WriteLine("world");
                    fileNew.WriteLine("code fellows");
                    fileNew.WriteLine("pull request");
                    fileNew.WriteLine("supercalifragilisticexpialidocious");
                }
            }
        }

        static string ReadAFile(string path)
        {
            try
            {
                string[] allWords = File.ReadAllLines(path);
                int randNum = RandomNumberGenerator(allWords.Length);
                string chosenWord = allWords[randNum];
                return chosenWord;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static int RandomNumberGenerator(int i)
        {
            Random number = new Random();
            int randNum = number.Next(0, i);
            return randNum;
        }
    }
}
