using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        private static int algoritm;
        private static string input_phrase;
        private static int procedure;
        private static string export;
        private static int value;
        private static string keyword;
        private static string file_name;

        static void Main(string[] args)
        {
            Console.WriteLine("Available algorithms for encoding/decoding:");
            Console.WriteLine("1 - Vigenere Substitution.");
            Console.WriteLine("2 - Transposition Letter by Letter.");
            Console.WriteLine("3 - Phone Code.");
            Console.WriteLine("4 - Binary Code.");
            Console.WriteLine("5 - Keyword.");
            Console.Write("\nType the algoritm #: ");
            algoritm = Convert.ToInt32(Console.ReadLine());
            Console.Write("Type the phrase: ");
            input_phrase = Console.ReadLine();
            switch (algoritm)
            {
                case 1:
                    Console.WriteLine("\nVigenere Substitution.");
                    Console.Write("Type the value: ");
                    value = Convert.ToInt32(Console.ReadLine());
                    if (value > 10 && value < 99)
                    {
                        Console.WriteLine("1 - Encode.");
                        Console.WriteLine("2 - Decode.");
                        Console.Write("Procedure #: ");
                        procedure = Convert.ToInt32(Console.ReadLine());
                        Vigenere_Substitution vs = new Vigenere_Substitution(input_phrase, value.ToString());
                        switch (procedure)
                        {
                            case 1:
                                vs.Encode();
                                Console.WriteLine("\n-- Successful encoding procedure --");
                                Console.WriteLine("Original phrase: " + vs.GetInput_Phrase());
                                Console.WriteLine("Encoded phrase: " + vs.GetOutput_Phrase());
                                break;
                            case 2:
                                vs.Decode();
                                Console.WriteLine("\n-- Successful encoding procedure --");
                                Console.WriteLine("Original phrase: " + vs.GetInput_Phrase());
                                Console.WriteLine("Decoded phrase: " + vs.GetOutput_Phrase());
                                break;
                            default:
                                Console.WriteLine("\nNo option.");
                                break;
                        }
                        Console.Write("\nExport results? (Y/N) ");
                        export = Console.ReadLine();
                        switch (export[0])
                        {
                            case 'Y':
                                Console.Write("Type a name for the output file: ");
                                file_name = Console.ReadLine();
                                vs.Export(file_name);
                                break;
                            case 'N':
                                break;
                            default:
                                Console.WriteLine("No option.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid value.");
                    }
                    break;
                case 2:
                    Console.WriteLine("\nTransposition Letter by Letter.");
                    Console.WriteLine("1 - Encode phrase.");
                    Console.WriteLine("2 - Decode phrase.");
                    Console.Write("Procedure #: ");
                    procedure = Convert.ToInt32(Console.ReadLine());
                    Transposition t = new Transposition(input_phrase);
                    switch (procedure)
                    {
                        case 1:
                            t.Encode();
                            Console.WriteLine("\n-- Successful encoding procedure --");
                            Console.WriteLine("Original phrase: " + t.GetInput_Phrase());
                            Console.WriteLine("Encoded phrase: " + t.GetOutput_Phrase());
                            break;
                        case 2:
                            t.Decode();
                            Console.WriteLine("\n-- Successful encoding procedure --");
                            Console.WriteLine("Original phrase: " + t.GetInput_Phrase());
                            Console.WriteLine("Decoded phrase: " + t.GetOutput_Phrase());
                            break;
                        default:
                            Console.WriteLine("\nNo option.");
                            break;
                    }
                    Console.Write("\nExport results? (Y/N) ");
                    export = Console.ReadLine();
                    switch (export[0])
                    {
                        case 'Y':
                            Console.Write("Type a name for the output file: ");
                            file_name = Console.ReadLine();
                            t.Export(file_name);
                            break;
                        case 'N':
                            break;
                        default:
                            Console.WriteLine("No option.");
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine("\nPhone Code.");
                    Console.WriteLine("1 - Encode phrase.");
                    Console.WriteLine("2 - Decode phrase.");
                    Console.Write("Procedure #: ");
                    procedure = Convert.ToInt32(Console.ReadLine());
                    Phone_Code pc = new Phone_Code(input_phrase);
                    pc.SetPhone_Numbers();
                    switch (procedure)
                    {
                        case 1:
                            pc.Encode();
                            Console.WriteLine("\n-- Successful encoding procedure --");
                            Console.WriteLine("Original phrase: " + pc.GetInput_Phrase());
                            Console.WriteLine("Encoded phrase: " + pc.GetOutput_Phrase());
                            break;
                        case 2:
                            pc.Decode();
                            Console.WriteLine("\n-- Successful encoding procedure --");
                            Console.WriteLine("Original phrase: " + pc.GetInput_Phrase());
                            Console.WriteLine("Decoded phrase: " + pc.GetOutput_Phrase());
                            break;
                        default:
                            Console.WriteLine("\nNo option.");
                            break;
                    }
                    Console.Write("\nExport results? (Y/N) ");
                    export = Console.ReadLine();
                    switch (export[0])
                    {
                        case 'Y':
                            Console.Write("Type a name for the output file: ");
                            file_name = Console.ReadLine();
                            pc.Export(file_name);
                            break;
                        case 'N':
                            break;
                        default:
                            Console.WriteLine("No option.");
                            break;
                    }
                    break;
                case 4:
                    Console.WriteLine("\nBinary Code.");
                    Console.WriteLine("1 - Encode phrase.");
                    Console.WriteLine("2 - Decode phrase.");
                    Console.Write("Procedure #: ");
                    procedure = Convert.ToInt32(Console.ReadLine());
                    Binary_Code bc = new Binary_Code(input_phrase);
                    bc.SetBinary_Equivalents();
                    switch (procedure)
                    {
                        case 1:
                            bc.Encode();
                            Console.WriteLine("\n-- Successful encoding procedure --");
                            Console.WriteLine("Original phrase: " + bc.GetInput_Phrase());
                            Console.WriteLine("Encoded phrase: " + bc.GetOutput_Phrase());
                            break;
                        case 2:
                            bc.Decode();
                            Console.WriteLine("\n-- Successful encoding procedure --");
                            Console.WriteLine("Original phrase: " + bc.GetInput_Phrase());
                            Console.WriteLine("Decoded phrase: " + bc.GetOutput_Phrase());
                            break;
                        default:
                            Console.WriteLine("\nNo option.");
                            break;
                    }
                    Console.Write("\nExport results? (Y/N) ");
                    export = Console.ReadLine();
                    switch (export[0])
                    {
                        case 'Y':
                            Console.Write("Type a name for the output file: ");
                            file_name = Console.ReadLine();
                            bc.Export(file_name);
                            break;
                        case 'N':
                            break;
                        default:
                            Console.WriteLine("No option.");
                            break;
                    }
                    break;
                case 5:
                    Console.WriteLine("\nKeyword.");
                    Console.Write("Type the keyword: ");
                    keyword = Console.ReadLine();
                    Console.WriteLine("1 - Encode phrase.");
                    Console.WriteLine("2 - Decode phrase.");
                    Console.Write("Procedure #: ");
                    procedure = Convert.ToInt32(Console.ReadLine());
                    Keyword kw = new Keyword(input_phrase, keyword);
                    switch (procedure)
                    {
                        case 1:
                            kw.Encode();
                            Console.WriteLine("\n-- Successful encoding procedure --");
                            Console.WriteLine("Original phrase: " + kw.GetInput_Phrase());
                            Console.WriteLine("Encoded phrase: " + kw.GetOutput_Phrase());
                            break;
                        case 2:
                            kw.Decode();
                            Console.WriteLine("\n-- Successful encoding procedure --");
                            Console.WriteLine("Original phrase: " + kw.GetInput_Phrase());
                            Console.WriteLine("Decoded phrase: " + kw.GetOutput_Phrase());
                            break;
                        default:
                            Console.WriteLine("\nNo option.");
                            break;
                    }
                    Console.Write("\nExport results? (Y/N) ");
                    export = Console.ReadLine().ToLower();
                    switch (export[0])
                    {
                        case 'y':
                            Console.Write("Type a name for the output file: ");
                            file_name = Console.ReadLine();
                            kw.Export(file_name);
                            break;
                        case 'n':
                            break;
                        default:
                            Console.WriteLine("No option.");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("\nNo option.");
                    break;
            }
        }
    }
}
