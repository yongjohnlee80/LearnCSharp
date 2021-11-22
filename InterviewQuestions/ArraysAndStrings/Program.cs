using System;
using System.Collections.Generic;
using System.Text;

namespace ArraysAndStrings
{
    class Program 
    {
        /// <summary>
        /// Interview Problem #1
        /// This method returns if the argument contains all unique characters.
        /// </summary>
        /// <param name="str">String argument to test for its uniqueness.</param>
        /// <returns>boolean value</returns>
        // O(n)
        public static bool IsUnique(string str) {
            var hashTable = new Dictionary<char, bool>();
            foreach(char i in str.ToCharArray()) {
                if (hashTable.ContainsKey(i)) {
                    return false;
                } else {
                    hashTable.Add(i, true);
                }
            }
            return true;
        }

        /// <summary>
        /// Interview Problem #2
        /// Given 2 strings, this method checks if they are permutated variation of each other.
        /// </summary>
        /// <param name="a">first string</param>
        /// <param name="b">second string</param>
        /// <returns>returns true if they are permatated variation of the strings.</returns>
        public static bool IsPermutation(string a, string b) {
            return Sort(a).Equals(Sort(b));

            // Sort helper function.
            // Convert string in to char array then sort it
            // Convert the sorted array back as string.
            string Sort(string str) {
                char[] array = str.ToCharArray();
                Array.Sort(array);
                return new string(array);
            }
        }

        /// <summary>
        /// Interview Problem #3
        /// Any whitespace within the sring is replaced with '%20'
        /// The input contains extra spaces to accomodate replacement of ' ' with "%20"
        /// </summary>
        /// <param name="str">Input string argument with extra pace in the end to accomodate "%20" instead of a whitespace.</param>
        /// <param name="trueLen">The true length is the string length without extra spaces.</param>
        /// <returns>The resulting string with whitespace replaced with "%20".</returns>
        public static string URLify(string str, int trueLen) {
            char[] temp = str.ToCharArray();
            char[] temp2 = new char[str.Length];
            try {
                for(int i=0, j =0; i<trueLen; i++, j++) {
                    if(temp[i]==' ') {
                        temp2[j] = '%';
                        temp2[j+1] = '2';
                        temp2[j+2] = '0';
                        j+=2;
                    } else {
                        temp2[j] = temp[i];
                    }
                }
            } catch(IndexOutOfRangeException e) {
                Console.WriteLine($"the string doesn't contain enough trailing spaces to accomodate replacement: {e.Message}.");
            }
            return new string(temp2);
        }

        /// <summary>
        /// Generate a List of strings containing the string permutations.
        /// eg) ABC = ABC, ACB, BAC, BCA, CBA, CAB
        /// This method uses recursive DFS algorithm to generate all string permutations.
        /// </summary>
        /// <param name="str">String argument to permute</param>
        /// <returns>List of strings</returns>
        public static List<string> GetStringPermutation(string str) {
            var result = new List<string>();
            Permute(str, 0, str.Length-1);
            return result;
            
            // Permute helper recursive-function.
            void Permute(string str, int l, int r) {
                if(l == r) {
                    result.Add(str);
                    return;
                } else {
                    for(int i=l; i<=r; i++) {
                        // swap for variations.
                        str = Swap(str, l, i);
                        // next depth.
                        Permute(str, l+1, r);
                        // back to orginal.
                        str = Swap(str, l, i);
                    }
                } 
            }

            // Swap helper function.
            // Swaps characters in a string at indices i, j
            string Swap(string str, int i, int j) {
                if(i == j) return str;

                char[] tempStr=str.ToCharArray();
                char temp;
                temp = tempStr[i];
                tempStr[i] = tempStr[j];
                tempStr[j] = temp;
                return new string(tempStr);
            }
        }

        /// <summary>
        /// Interview Problem #4
        /// This method check if the argument is a permutation of a palindrome.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>returns list of all permutations that are palindromes.</returns>
        public static List<string> CollectPalindrome(string str) {
            str = str.ToLower();
            var result = new List<string>();
            foreach(string i in GetStringPermutation(str)) {
                if(IsPalindrome(i)) {
                    result.Add(i);
                }
            }
            return result;

            // Check is str is palindrome.
            bool IsPalindrome(string str) {
                str = str.Replace(" ", "");
                char[] temp = str.ToCharArray();
                Array.Reverse(temp);
                return str.Equals(new string(temp));
            }
        }

        /// <summary>
        /// Interview Problem #4 second implementation.
        /// Reduced time and space complexity.
        /// This method checks if the argument is a permutation of a palindrome.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns true if palindrome permutation is found.</returns>
        public static bool CheckPalindrome(string str) {
            str = str.ToLower();
            str = str.Replace(" ", "");
            var temp = str.ToCharArray();

            // Build a hashtable for each character with its frequency.
            var table = new Dictionary<char, int>();
            foreach(char i in temp) {
                if(table.ContainsKey(i)) {
                    table[i]++;
                } else {
                    table.Add(i, 1);
                }
            }
            if(str.Length%2==0) {
                // even number of characters in the string.
                foreach(var i in table.Keys) {
                    if(table[i] != 2) return false;
                }
            } else {
                // odd number of characters in the string.
                int singleCount = 0;
                foreach(var i in table.Keys) {
                    if(table[i]==1) {
                        // allow only one unique character in the middle.
                        if(++singleCount > 1) return false;
                    } else if(table[i] != 2) {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Interview Problem #5
        /// Compare two strings whether they are one edit away from each other.
        /// Possible edit includes replace(), insert(), or remove() for a single character.
        /// </summary>
        /// <param name="a">first string</param>
        /// <param name="b">second string</param>
        /// <returns>returns true if they are one edit away from each other.</returns>
        public static bool IsOneAway(string a, string b) {
            if (Math.Abs(a.Length-b.Length)<=1) {
                if(a.Length==b.Length) {
                    int count = 0;
                    for(int i=0;i<a.Length;i++) {
                        if(a.ElementAt(i)!=b.ElementAt(i)) {
                            if(++count > 1) return false;
                        }
                    }
                    return true;
                } else if(a.Length<b.Length) {
                    return OneRemoveAway(b,a);
                } else {
                    return OneRemoveAway(a,b);
                }
            } else {
                return false;
            }

            // Helper method to check whether removing one character from string a
            // will make it the same as string b.
            // a is the longer string.
            bool OneRemoveAway(string a, string b) {
                for(int i=0;i<b.Length;i++) {
                    if(a.ElementAt(i)!=b.ElementAt(i)) {
                        a = a.Remove(i,1);
                        if(a==b) return true;
                        else return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Interview Problem #6
        /// Basic Compression Method using letter frequencies.
        /// return compressed string if it is shorter than the original string,
        /// otherwise return the original string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>compressed string if it is shorter otherwise return original string.</returns>
        public static string CompressString(string str) {
            char[] temp = str.ToCharArray();
            StringBuilder result = new StringBuilder();
            int count = 1;
            char prev = temp[0];
            for(int i=1;i<str.Length;i++) {
                if(temp[i]==prev) {
                    count++;
                } else {
                    result.Append(prev+count.ToString());
                    count = 1;
                    prev = temp[i];
                }
            }
            result.Append(prev+count.ToString());
            return (result.Length <= str.Length)? result.ToString(): str;
        }

        /// <summary>
        /// Interview Problem #7
        /// This method rotates the NxN matrix by 90 degrees.
        /// </summary>
        /// <param name="mat"></param>
        public static void RotateMaxtrix(int[,] mat) {
            // check the matrix if it is sqaure.
            if(mat.GetLength(0) != mat.GetLength(1)) return;
            int N = mat.GetLength(0);
            for(int layer = 0; layer < N/2; layer++) {
                int first = layer;
                int last = N - 1 - layer;
                for(int i = first; i < last; i++) {
                    int temp = mat[first,i];
                    mat[first,i]=mat[i,last];
                    mat[i,last]=mat[last,N-1-i];
                    mat[last,N-1-i]=mat[N-1-i,first];
                    mat[N-1-i,first]=temp;
                }
            }
        }

        /// <summary>
        /// Interview Problem #8
        /// ZeroMatrix method locates all zeros in the matrix contents, and if found,
        /// sets all elements in its columns and rows to zero.
        /// </summary>
        /// <param name="mat"></param>
        public static void ZeroMatrix(int[,] mat) {
            int N = mat.GetLength(0);
            int M = mat.GetLength(1);
            var row = new List<int>();
            var col = new List<int>();
            for(int i=0; i<N; i++) {
                for(int j=0; j<N; j++) {
                    if(mat[i,j]==0) {
                        if(!row.Contains(j)) row.Add(j);
                        if(!col.Contains(i)) col.Add(i);
                    }
                }
            }
            foreach(int i in col) {
                for(int j=0;j<M;j++) {
                    mat[i,j]=0;
                }
            }
            foreach(int j in row) {
                for(int i=0;i<N;i++) {
                    mat[i,j]=0;
                }
            }
        }

        // Printing NxM Matrix on console.
        public static void PrintMatrix(int[,] mat) {
            int N = mat.GetLength(0);
            int M = mat.GetLength(1);
            Console.WriteLine($"Printing a {N}x{M} Matrix: ");
            for(int i=0;i<N;i++) {
                for(int j=0;j<M;j++) {
                    Console.Write("{0,2:N0} ",mat[i,j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Interview Problem #9
        /// StringRotation method checks whether string b is a rotation of string b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool StringRotation(string a, string b) {
            string temp = b+b;
            return temp.Contains(a);
        }
        public static void Main(string[] args) {
            // Problem #1
            Console.WriteLine($"Problem 1: {IsUnique("hello")}");

            // Problem #2
            Console.WriteLine($"Problem 2: {IsPermutation("ABDFC", "DFCAB")}");

            // Problem #3
            Console.WriteLine($"Problem 3: {URLify("Mr John Smith    ", 13)}");

            // Problem #4
            List<string> problem4 = CollectPalindrome("Tact Coa");
            if(problem4.Count > 0) {
                Console.Write("Problem 4: True (Permutations: ");
                int count = 1;
                foreach(string i in problem4) {
                    Console.Write($"\"{i}\", ");
                    if (++count > 4) break;
                }
                Console.Write(" etc.)");
            } else {
                Console.Write("Problem 4 : False");
            }
            Console.WriteLine($"\nProblem 4B: {CheckPalindrome("Tact Coa")}");

            // Problem #5
            Console.WriteLine($"Problem 5: {IsOneAway("pale", "ple")}");
            Console.WriteLine($"Problem 5: {IsOneAway("pales", "pale")}");
            Console.WriteLine($"Problem 5: {IsOneAway("pale", "bale")}");
            Console.WriteLine($"Problem 5: {IsOneAway("pale", "bake")}");

            // Problem #6
            Console.WriteLine($"Problem 6: {CompressString("aabcccccaaa")}");

            // Problem #7
            int[,] mat = {  { 1, 2, 3, 4, 5}, 
                            { 6, 7, 8, 9,10}, 
                            {11, 0,13,14,15}, 
                            {16,17,18, 0,20},
                            {21,22,23,24,25} };
            Console.WriteLine("Problem 7:");
            RotateMaxtrix(mat);
            PrintMatrix(mat);

            // Problem #8
            Console.WriteLine("Problem 8:");
            ZeroMatrix(mat);
            PrintMatrix(mat);

            // Problem #9
            string a = "waterbottle", b = "erbottlewat";
            Console.WriteLine($"Problem 9: \"{b}\" is a rotation of \"{a}\" - {StringRotation(a, b)}");
        }
    }
}