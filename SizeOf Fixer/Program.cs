using dnlib.DotNet;
using dnlib.DotNet.Writer;
using System;
using System.IO;

namespace SizeOf_Fixer
{
    internal class Program
    {
        public static string Asmpath;
        public static ModuleDefMD AsmethodMdOriginal;
        public static int SizeOFAmount;
        public static int MathsAmount;

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Drag'n drop file.");
                Console.ReadKey();
                return;
            }
            Console.Title = "SizeOf Fixer - iYaReM";

            try
            {
                AsmethodMdOriginal = ModuleDefMD.Load(args[0]);
                Asmpath = args[0];


                string directoryName = Path.GetDirectoryName(args[0]);

                if (!directoryName.EndsWith("\\"))
                {
                    directoryName += "\\";
                }


                SizeOf.SizeOfFixer(AsmethodMdOriginal);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] " + SizeOFAmount + " SizeOf's Replaced");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[+] Write y to ennable Math fix.");
                if (Console.ReadLine().ToString().ToLower() == "y")
                {
                    MathsEquations.MathsFixer(AsmethodMdOriginal);
                    MathsEquations.MathsFixer(AsmethodMdOriginal);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[+] " + MathsAmount + " Maths Equations Solved");
                }
                string filename = string.Format("{0}{1}-SizeOfsFixed{2}", directoryName, Path.GetFileNameWithoutExtension(args[0]), Path.GetExtension(args[0]));

                if (!AsmethodMdOriginal.Is32BitRequired)
                {
                    ModuleWriterOptions moduleWriterOptions = new ModuleWriterOptions(AsmethodMdOriginal);
                    moduleWriterOptions.MetaDataLogger = DummyLogger.NoThrowInstance;
                    moduleWriterOptions.MetaDataOptions.Flags = MetaDataFlags.PreserveAll;
                    AsmethodMdOriginal.Write(filename, moduleWriterOptions);
                }
                else
                {
                    NativeModuleWriterOptions moduleWriterOptions = new NativeModuleWriterOptions(AsmethodMdOriginal);
                    moduleWriterOptions.MetaDataLogger = DummyLogger.NoThrowInstance;
                    moduleWriterOptions.MetaDataOptions.Flags = MetaDataFlags.PreserveAll;
                    AsmethodMdOriginal.NativeWrite(filename, moduleWriterOptions);
                }
               

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("");
                Console.WriteLine("Done! Saving Assembly...");
            }
            catch { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid file."); }
            Console.ReadKey();
        }
    }
}
