using dnlib.DotNet;
using dnlib.DotNet.Writer;
using System;
using System.IO;
using System.Reflection;

namespace SizeOf_Fixer
{
    internal class Program
    {
        public static string Asmpath;
        public static ModuleDefMD AsmethodMdOriginal;
        public static Assembly AsmmethodNew;
        public static int SizeOFAmount;
        public static int MathsAmount;

        private static void Main(string[] args)
        {
            Console.Title = "SizeOf Fixer - iYaReM";

            AsmethodMdOriginal = ModuleDefMD.Load(args[0]);
            AsmmethodNew = Assembly.LoadFrom(args[0]);
            Asmpath = args[0];


            string directoryName = Path.GetDirectoryName(args[0]);

            if (!directoryName.EndsWith("\\"))
            {
                directoryName += "\\";
            }


            SizeOf.SizeOfFixer(AsmethodMdOriginal);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[!] " + SizeOFAmount + " SizeOf's Replaced");

            MathsEquations.MathsFixer(AsmethodMdOriginal);
            MathsEquations.MathsFixer(AsmethodMdOriginal);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[+] " + MathsAmount + " Maths Equations Solved");

            string filename = string.Format("{0}{1}-SizeOfsFixed{2}", directoryName, Path.GetFileNameWithoutExtension(args[0]), Path.GetExtension(args[0]));

            ModuleWriterOptions moduleWriterOptions = new ModuleWriterOptions(AsmethodMdOriginal) { Logger = DummyLogger.NoThrowInstance };
            ModuleWriterOptions options = moduleWriterOptions;

            AsmethodMdOriginal.Write(filename, options);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("");
            Console.WriteLine("Done! Saving Assembly...");

            Console.ReadKey();
        }
    }
}
