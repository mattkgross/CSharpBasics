using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpBasics.Problems
{
    /*
     * An aside: this problem was inspired by a good buddy of mine.
     * Check out an implemntation of his in Python for more practice.
     * 
     */

    public static class MinPalindromes
    {
        /*
         * The Problem:
         * Given a string of arbitrary length calculate the minimum number
         * of non overlapping palindromes.
         * 
         * An Example:
         * 
         * The String: abccbaab
         * 
         * Explanation: abccba and baab overlap
         * You can’t split the string like that since they share a character.
         * 
         * Answer: (abccba, a, b)
         */

        /// <summary>
        /// Returns a string grouped into characters (single character palindromes, technically)
        /// and unique, non-overlapping palindromes found in the string, traversing left to right.
        /// THIS IS CASE SENSISTIVE.
        /// </summary>
        /// <returns>An array of palindromes.</returns>
        /// <param name="phrase">Phrase.</param>
        public static string[] FindPalindromes(string phrase)
        {
            /* So we're gonna go for this in O(n) time cuz why not.
             * The key is to use a stack and iterate the string only once.
             * 
             * 
             * Time: O(n)
             * Space: O(n)
             */

            List<string> result = new List<string>();

            Stack<char> runner = new Stack<char>();

            // You could also use a HaspMap to check in O(1) time.
            // But we'll serially Dequeue in sync, which is also constant.
            Queue<int> palindromeIndices = new Queue<int>();
            Queue<string> palindromes = new Queue<string>();

            int index = 0;

            // We'll iterate the list in order, pushing to a stack as we go.
            for (; index < phrase.Length; index++)
            {
                // First, we peek to see if the next value is on the stack.
                // If so, we're starting a palondrome!

                string currentDrome = string.Empty;

                // Build the palindrome as we pop off the stack and keep reading further.
                while (index < phrase.Length && runner.Count > 0 && phrase[index] == runner.Peek())
                {
                    char val = runner.Pop();
                    currentDrome = val + currentDrome + val;
                    index++;
                }

                // If a palindrome was found this round, mark it.
                if (currentDrome.Length != 0)
                {
                    // Mark the occurrence of this palindrome by using the count of the current stack.
                    palindromeIndices.Enqueue(runner.Count);

                    // Add the palindrome to our exisiting list of multi-letter ones.
                    palindromes.Enqueue(currentDrome);
                }

                // We found a dangling character (that could potentially be part of a larger palindrome)
                // So we push it onto the stack.
                if (index < phrase.Length)
                {
                    runner.Push(phrase[index]);
                }
            }

            // At this point, we have all of our multi-character palindromes in an ordered list.
            // And we have a queue of indices at which they occured in our stack, which is comprised
            // of all of the single, dangling characters.
            index = 0;

            // But it's not that easy, cuz we need to add the stack to the string in reverse order now,
            // so we pop it onto... another stack!
            Stack<char> reverseRunner = new Stack<char>();
            while (runner.Count > 0)
            {
                reverseRunner.Push(runner.Pop());
            }


            while (reverseRunner.Count > 0)
            {
                // If we're at an index that a palindrome was, in
                if (palindromeIndices.Count > 0 && index == palindromeIndices.Peek())
                {
                    palindromeIndices.Dequeue();
                    result.Add(palindromes.Dequeue());
                }
                // Otherwise, just add the next character.
                else
                {
                    result.Add(reverseRunner.Pop().ToString());
                }

                index++;
            }

            // If there's any palindromes at the end, we don't want to forget them!
            while (palindromes.Count > 0)
            {
                result.Add(palindromes.Dequeue());
            }

            return result.ToArray();
        }
    }

    [TestFixture]
    public class MinPalindromesTest
    {
        [Test]
        public void Test()
        {
            // <input, result
            Dictionary<string, string[]> testCases = new Dictionary<string, string[]>
            {
                { "abccbaab", new string[] { "abccba", "a", "b" } }, // Test #0
                { "abccbaababccbabccb", new string[] { "abccba", "a", "b", "abccba", "bccb" } },
                { "" , new string[0] }
            };

            foreach (KeyValuePair<string, string[]> test in testCases)
            {
                int index = 0;

                foreach (string palindrome in MinPalindromes.FindPalindromes(test.Key))
                {
                    Assert.AreEqual(test.Value[index], palindrome,
                                    $"Test #{index} failed. Input: {test.Key}, " +
                                    $"Expected: {test.Value[index]}, Actual: {palindrome}");
                    index++;
                }
            }
        }
    }
}
