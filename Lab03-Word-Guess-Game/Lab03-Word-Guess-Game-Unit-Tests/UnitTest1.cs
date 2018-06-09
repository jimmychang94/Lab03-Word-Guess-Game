using System;
using Xunit;
using Lab03_Word_Guess_Game;

namespace Lab03_Word_Guess_Game_Unit_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CreateAFileTest()
        {
            string path = "../../../unitTest.txt";
            string[] startingWords = new string[] { "cat", "dog", "tea", "sun", "code", "pull", "loop", "exit", "catch", "mouse", "hello", "world", "class", "object", "string", "update", "boolean", "fellows", "request", "supercalifragilisticexpialidocious" };
            Program.CreateFile(path, startingWords);
        }

        [Fact]
        public void ReadAFileTest()
        {
            string path = "../../../unitTest.txt";
            string[] startingWords = new string[] { "cat", "dog", "tea", "sun", "code", "pull", "loop", "exit", "catch", "mouse", "hello", "world", "class", "object", "string", "update", "boolean", "fellows", "request", "supercalifragilisticexpialidocious" };
            string[] readWords = Program.ReadAFile(path);
            Assert.Equal(startingWords, readWords);
        }
        [Fact]
        public void AddAWordTest()
        {
            string path = "../../../unitTest.txt";
            string word = "word";
            Program.AddAWord(path, word);
            string[] readWords = Program.ReadAFile(path);
            Assert.Equal(word, readWords[readWords.Length - 1]);
        }

        [Fact]
        public void DeleteAFileTest()
        {
            string path = "../../../unitTest.txt";
            Program.DeleteFile(path);
        }
    }
}
