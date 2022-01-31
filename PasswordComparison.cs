using System;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace QuickFixes
{
    class Program
    {
        static void Main()
        {
            Demo("abc", "xyz", false);
            Demo("", "", true);
            Demo("ABC", "ABC", true);
            Demo("Andy", "Andrews", false);
            Demo("", " ", false);
            Demo(" ", "  ", false);
            Demo("\uD802", "\uD8ff", false);
            Demo("Jimmy!", "jimmy!", false);
            Demo("\u007F", " ", false);
            Demo("\u007F", "", false);
            Demo("/mytext", "⁄mytext", false);
            Demo("\u00C0", "A", false);
            Demo("\u00C1", "A", false);
            Demo("\u00C2", "A", false);
            Demo("\u00C3", "A", false);
            Demo("\u00C4", "A", false);
            Demo("\u00C5", "A", false);
            Demo("\u00C6", "A", false);
            Demo("Security", "Securit￥", false);
        }

        static void Demo(string s1, string s2, bool expected)
        {
            bool same = TimeConstantComparison(s1, s2);
            string passfail = (same == expected) ? "Pass!" : "Fail!";
            Console.WriteLine($"{passfail} [{s1}] == [{s2}]?");
        }
        
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        static bool TimeConstantComparison(string s1, string s2)
        {
            int accum = s1.Length ^ s2.Length;
            int mn = Math.Min(s1.Length, s2.Length);

            for (int i = 0; i < mn; i++)
                accum |= (s1[i] ^ s2[i]);

            return accum == 0 ? true : false;
        }
    }
}
