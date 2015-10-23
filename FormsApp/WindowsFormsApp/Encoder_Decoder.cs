using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace WindowsFormsApp
{
    public abstract class Encoder_Decoder
    {

        // Class attributes.
        protected string input_phrase;
        protected string output_phrase;
        protected bool is_processed;
        protected char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        // Class constructor.
        public Encoder_Decoder(string input_phrase)
        {
            this.input_phrase = input_phrase.ToLower().Trim();
        }

        // Class destructor.
        ~Encoder_Decoder() { }

        // Attributes setters.
        public void SetInput_Phrase(string input_phrase) { this.input_phrase = input_phrase; }
        public void SetOutput_Phrase(string output_phrase) { this.output_phrase = output_phrase; }

        // Attributes getters.
        public string GetInput_Phrase() { return this.input_phrase; }
        public string GetOutput_Phrase() { return this.output_phrase; }

        // Exports the input and output phrases (encoded or decoded). (Only if it has been processed.)
        public void Export(string algorithm, string file_name)
        {

            if (is_processed)
            {
                using (StreamWriter sw = new StreamWriter(file_name + ".txt"))
                {
                    sw.WriteLine("Algorithm used: " + algorithm);
                    sw.Write("Input Phrase: "); sw.WriteLine(this.input_phrase);
                    sw.Write("Output Phrase: "); sw.WriteLine(this.output_phrase);
                }
                using (XmlWriter xw = XmlWriter.Create(file_name + ".xml"))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement("Algorithm used: " + algorithm);
                    xw.WriteElementString("Input Phrase: ", this.input_phrase);
                    xw.WriteElementString("Output Phrase: ", this.output_phrase);
                    xw.WriteEndDocument();
                }
                Console.WriteLine("Successful exportation.");
            }
            else
            {
                Console.WriteLine("The input phrase must be processed. Encode or decoded it first!");
            }

        }

        // Encodes input phrase and stores it in output phrase.
        public abstract void Encode();

        // Decodes input phrase and stores it in the output phrase.
        public abstract void Decode();
    }
}
