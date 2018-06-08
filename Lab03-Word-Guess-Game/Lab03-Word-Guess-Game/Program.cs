using System;
using System.IO;
using System.Text;

namespace Lab03_Word_Guess_Game
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string path = "../../../storedWords.txt";
            CreateFile(path);
            bool keepPlaying = true;
            while (keepPlaying)
            {
                keepPlaying = Menu(path);
            }
            // Game(path);
        }

        /// <summary>
        /// This method contains the menu and allows the user to navigate to other aspects of the application.
        /// While this method doesn't directly use the input of path; methods called within require it.
        /// The return allows for the menu to loop if placed in a while loop (and assigned to the condition).
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static bool Menu(string path)
        {
            Options();
            string userInput = Console.ReadLine();
            Int32.TryParse(userInput, out int userChoice);
            if (userInput == "cats are awesome!")
            {
                CatStop();
                return true;
            }
            if (userInput == "Josie")
            {
                Josie();
                return true;
            }
            switch (userChoice)
            {
                case 1:
                    Game(path);
                    break;
                case 2:
                    Admin(path);
                    break;
                case 3:
                    DeleteFile(path);
                    return false;
                default:
                    Console.WriteLine("Please try again.");
                    return true;
            }
            return true;
        }

        /// <summary>
        /// This is just a list of options that you can choose when you start the application.
        /// </summary>
        static void Options()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Enter Admin Mode");
            Console.WriteLine("3. Exit");
        }

        /// <summary>
        /// This uses the path to read the file and then chooses a random word from the file and starts the game.
        /// </summary>
        /// <param name="path"></param>
        static void Game(string path)
        {
            string[] allWords = ReadAFile(path);
            string chosenWord = GetWord(allWords);
            string[] segmentedWord = new string[chosenWord.Length];

            for (int i = 0; i < chosenWord.Length; i++)
            {
                segmentedWord[i] = " _ ";
            }

            GameMechanic(chosenWord, segmentedWord);

            Console.WriteLine("You solved it!");
            Console.WriteLine($"The word was: {chosenWord}");
            if (chosenWord.ToLower() == "supercalifragilisticexpialidocious")
            {
                Console.WriteLine("Good job figuring this one out!");
            }
            Console.WriteLine("Thanks for playing my word game!");
        }

        /// <summary>
        /// This method contains the structure of the game. It requires the chosen word and the array of " _ " that represent the word.
        /// </summary>
        /// <param name="chosenWord"></param>
        /// <param name="segmentedWord"></param>
        static void GameMechanic(string chosenWord, string[] segmentedWord)
        {
            bool solved = false;
            string guessedLetters = "";
            while (!solved)
            {
                HiddenWord(segmentedWord);
                Console.WriteLine("Guess a letter!");
                string userLetter = UserGuess(guessedLetters);

                segmentedWord = Comparison(segmentedWord, chosenWord, userLetter);
                string guessedWord = "";
                foreach (string word in segmentedWord)
                {
                    guessedWord += word;
                }
                if (chosenWord.ToLower() == guessedWord)
                {
                    solved = true;
                }
                guessedLetters += userLetter;
                Console.WriteLine($"You have currently guessed: {guessedLetters}");
            }
        }

        /// <summary>
        /// This method is used to make sure that the user gives a letter and also that it isn't already written. It then returns the user's response.
        /// </summary>
        /// <param name="guessedLetters"></param>
        /// <returns>The letter guessed by the user.</returns>
        static string UserGuess(string guessedLetters)
        {
            try
            {
                string userLetter = Console.ReadLine().Trim().ToLower();
                if (userLetter.Length != 1)
                {
                    // I threw a new FormatException() because I needed to separate the two types of errors that could occur.
                    throw new FormatException();
                }
                if (guessedLetters.Contains(userLetter))
                {
                    throw new Exception();
                }
                return userLetter;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please write a single letter");
            }
            catch (Exception)
            {
                Console.WriteLine("You have already guessed that letter.");
            }
            return "";
        }

        /// <summary>
        /// This method prints onto the console the " _ " that represent the word. To do that it requires that array to be put in.
        /// </summary>
        /// <param name="segmentedWord"></param>
        static void HiddenWord (string[] segmentedWord)
        {
            foreach (string letter in segmentedWord)
            {
                Console.Write(letter);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// This is the main logic behind the game, it checks for if the letter that the user chose is in the chosen word.
        /// If so, it will replace the " _ " in the array wherever the letter is in the chosen word.
        /// It then returns the array so that people can continue guessing.
        /// </summary>
        /// <param name="segmentedWord"></param>
        /// <param name="chosenWord"></param>
        /// <param name="userLetter"></param>
        /// <returns></returns>
        static string[] Comparison(string[] segmentedWord, string chosenWord, string userLetter)
        {
            if (chosenWord.ToLower().Contains(userLetter))
            {
                for (int i = 0; i < chosenWord.Length; i++)
                {
                    if (chosenWord[i].ToString().ToLower() == userLetter)
                    {
                        segmentedWord[i] = userLetter;
                    }
                }
            }
            return segmentedWord;
        }

        static void Admin(string path)
        {

        }

        static void AdminOptions()
        {
            Console.WriteLine("What would you like to do?");
        }

        /// <summary>
        /// This method displays all the words in a file.
        /// It requires a path in order to find the file it is meant to read.
        /// The try/catch block is to catch for when there is no file there for it to read.
        /// </summary>
        /// <param name="path"></param>
        static void ViewAllWords(string path)
        {
            try
            {
                string[] allWords = ReadAFile(path);
                foreach (string word in allWords)
                {
                    Console.WriteLine(word);
                }
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
                    fileNew.WriteLine("code");
                    fileNew.WriteLine("pull");
                    fileNew.WriteLine("loop");
                    fileNew.WriteLine("exit");
                    fileNew.WriteLine("catch");
                    fileNew.WriteLine("mouse");
                    fileNew.WriteLine("hello");
                    fileNew.WriteLine("world");
                    fileNew.WriteLine("class");
                    fileNew.WriteLine("object");
                    fileNew.WriteLine("string");
                    fileNew.WriteLine("update");
                    fileNew.WriteLine("boolean");
                    fileNew.WriteLine("fellows");
                    fileNew.WriteLine("request");
                    fileNew.WriteLine("supercalifragilisticexpialidocious");
                }
            }
        }

        /// <summary>
        /// This method deletes a file at a designated location based off of the input path.
        /// </summary>
        /// <param name="path"></param>
        static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        /// <summary>
        /// This method just reads all the words in a file and returns it if a file exists and throws an exception if it doesn't.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static string[] ReadAFile(string path)
        {
            try
            {
                string[] allWords = File.ReadAllLines(path);
                return allWords;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// This method takes in an array of all the words and then uses a random number generator to choose one of the words in the array and returns that string.
        /// </summary>
        /// <param name="allWords"></param>
        /// <returns></returns>
        static string GetWord(string[] allWords)
        {
            int randNum = RandomNumberGenerator(allWords);
            string chosenWord = allWords[randNum];
            return chosenWord;
        }

        /// <summary>
        /// This method is a number generator specifically for an array of strings.
        /// It uses the length of the array as an exclusive max for generating a random number.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        static int RandomNumberGenerator(string[] array)
        {
            Random number = new Random();
            int randNum = number.Next(0, array.Length);
            return randNum;
        }

        /// <summary>
        /// Shh...
        /// </summary>
        static void CatStop()
        {
            Console.WriteLine("Welcome the the cat stop!");
            Console.WriteLine("Indeed cats are awesome!");
            Console.WriteLine("Have two fish this time!");
            Console.WriteLine(@"
                                        _J""-.           _J""-.
                                       / o )) \ ,'`;    / o )) \ ,'`;
                                       \   ))  ;   |    \   ))  ;   |
                                        `v-.-'' \._;     `v-.-'' \._;
                                                            ");
            Console.WriteLine("I guess I'll give a hint for finding the next shop....");
            Console.WriteLine("An object is an instance of a class");
            Console.ReadLine();
            Console.WriteLine("Github");
            Console.WriteLine("Thanks for visiting!");
        }

        /// <summary>
        /// Busy, busy, busy
        /// </summary>
        static void Josie()
        {
            Console.WriteLine(@"
                                    /\   /\
                                   /  \_/  \
                                  (  o . o  ) .
                                    ( v v )__/
                                                            ");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("....");
            Console.WriteLine("I'm very busy, please go back to the game.");
            Console.WriteLine(@"
                                    /\   /\    Z
                                   /  \_/  \ z
                                  (  - . -  ) .
                                    ( v v )__/
                                                            ");
        }
    }
}
