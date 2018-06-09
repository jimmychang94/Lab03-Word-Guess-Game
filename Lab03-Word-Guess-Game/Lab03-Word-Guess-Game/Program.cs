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
            string[] startingWords = new string[] { "cat", "dog", "tea", "sun", "code", "pull", "loop", "exit", "catch", "mouse", "hello", "world", "class", "object", "string", "update", "boolean", "fellows", "request", "supercalifragilisticexpialidocious" };
            string path = "../../../storedWords.txt";
            CreateFile(path, startingWords);
            bool keepPlaying = true;
            while (keepPlaying)
            {
                keepPlaying = Menu(path);
            }
        }

        /// <summary>
        /// This method contains the menu and allows the user to navigate to other aspects of the application.
        /// The return allows for the menu to loop if placed in a while loop (and assigned to the condition).
        /// </summary>
        /// <param name="path">This is the relative url to where the file with the words are</param>
        /// <returns>This returns whether the user wants to exit the game or not</returns>
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
                    bool adminStop = false;
                    while (!adminStop)
                    {
                        adminStop = Admin(path);
                    }
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
        /// This method reads the file and then chooses a random word from the file and starts the game.
        /// It is the foundation for the game. It also responds once you have fininshed the game.
        /// </summary>
        /// <param name="path">This is the relative url to where the words are located</param>
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
        /// <param name="chosenWord">This is the randomly chosen word</param>
        /// <param name="segmentedWord">This holds the array of " _ " representing the letters in the word</param>
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
        /// <param name="guessedLetters">This is the string of letters that the user has guessed</param>
        /// <returns>The letter guessed by the user.</returns>
        static string UserGuess(string guessedLetters)
        {
            try
            {
                // This allows for the user to have a bit more leeway in how they write the letter.
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
        /// This method prints onto the console the " _ " that represent the word.
        /// </summary>
        /// <param name="segmentedWord">The array of " _ " that represents the word</param>
        static void HiddenWord(string[] segmentedWord)
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
        /// <param name="segmentedWord">The array of " _ " that represent the word</param>
        /// <param name="chosenWord">The randomly chosen word that the user is trying to guess</param>
        /// <param name="userLetter">The letter that the user guessed</param>
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

        /// <summary>
        /// This holds the admin framework which allows the user to view all words, add a word, and delete a word.
        /// </summary>
        /// <param name="path">The relative url to where the words are located</param>
        /// <returns>whether the user wants to continue in the admin screen or not</returns>
        static bool Admin(string path)
        {
            AdminOptions();
            Int32.TryParse(Console.ReadLine(), out int adminChoice);
            switch (adminChoice)
            {
                case 1:
                    ViewAllWords(path);
                    break;
                case 2:
                    Console.WriteLine("What word would you like to add?");
                    string addedWord = Console.ReadLine().Trim();
                    AddAWord(path, addedWord);
                    Console.Clear();
                    Console.WriteLine("Your word has been added");
                    break;
                case 3:
                    Console.WriteLine("What word would you like to delete?");
                    DeleteAWord(path);
                    break;
                case 4:
                    return true;
                default:
                    Console.WriteLine("Sorry, I didn't understand.");
                    break;
            }
            return false;
        }

        /// <summary>
        /// This method just hold the options you get in the admin view.
        /// </summary>
        static void AdminOptions()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. View all words");
            Console.WriteLine("2. Add a word");
            Console.WriteLine("3. Delete a word");
            Console.WriteLine("4. Exit");
        }

        /// <summary>
        /// This method displays all the words in a file.
        /// The try/catch block is to catch for when there is no file there for it to read.
        /// </summary>
        /// <param name="path">The relative url to where the words are located</param>
        static void ViewAllWords(string path)
        {
            string[] allWords = ReadAFile(path);
            foreach (string word in allWords)
            {
                Console.WriteLine(word);
            }

        }

        /// <summary>
        /// This method first deletes a file if there is one. It then creates a new file and then writes some words into the file. 
        /// </summary>
        /// <param name="path">The relative url to where the words are located</param>
        /// <param name="array">An array of words to put into the file</param>
        public static void CreateFile(string path, string[] array)
        {
            DeleteFile(path);
            using (StreamWriter fileNew = new StreamWriter(path))
            {
                foreach (string word in array)
                {
                    fileNew.WriteLine(word);
                }
            }
        }

        /// <summary>
        /// This method just reads all the words in a file and returns it if a file exists and throws an exception if it doesn't. 
        /// </summary>
        /// <param name="path">The relative url to where the words are located</param>
        /// <returns>This returns an array of all the words in the file</returns>
        public static string[] ReadAFile(string path)
        {
            string[] allWords = File.ReadAllLines(path);
            return allWords;
        }
        
        /// <summary>
        ///  This method has the user tell us what word they want to add
        ///  It only adds it to the file if the word is not already in the file.
        /// 
        /// </summary>
        /// <param name="path">The relative url to where the words are located</param>
        /// <param name="addedWord">The word that the user wants to remove</param>
        public static void AddAWord(string path, string addedWord)
        {
            string[] allWords = ReadAFile(path);
            foreach (string word in allWords)
            {
                if (string.Equals(word, addedWord, StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }

            }
            using (StreamWriter newWord = File.AppendText(path))
            {
                newWord.WriteLine(addedWord);
            }
        }

        /// <summary>
        /// This method takes the user input of what word they want to delete.
        /// It then compares it to the other words on file and if it finds it then it removes it.
        /// </summary>
        /// <param name="path">The relative url to where the words are located</param>
        static void DeleteAWord(string path)
        {
            string[] allWords = ReadAFile(path);
            string[] deletedWordArray = new string[allWords.Length];
            string deleteWord = Console.ReadLine().Trim();
            int counter = 0;
            for (int i = 0; i < allWords.Length; i ++)
            {
                if (deleteWord != allWords[i])
                {
                    deletedWordArray[counter] = allWords[i];
                    counter++;
                }
            }

            CreateFile(path, deletedWordArray);
            Console.WriteLine($"{deleteWord} has been terminated.");
        }



        /// <summary>
        /// This method deletes a file at a designated location based off of the input path.
        /// </summary>
        /// <param name="path">The relative url to where the words are located</param>
        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }
        
        /// <summary>
        /// This method takes in an array of all the words and then uses a random number generator to choose one of the words in the array and returns that string.
        /// </summary>
        /// <param name="allWords">This is an array of all the words in the file</param>
        /// <returns>A random word from the array of words</returns>
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
        /// <param name="array">This is an array of all the words in the file</param>
        /// <returns>A random number between 0 and the length of the array</returns>
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
