using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    // Vigenere substitution algorithm.
    public class Vigenere_Substitution : Encoder_Decoder
    {
        private string value;

        public Vigenere_Substitution(string input_phrase, string value) : base(input_phrase) { this.value = value; }

        public void Export(string file_name)
        {
            base.Export("Vigenere Substitution. - : " + this.value, file_name);
        }

        public void SetValue(string value) { this.value = value; }
        public string GetValue() { return this.value; }

        // Encoding procedure.
        public override void Encode()
        {
            this.is_processed = true;
            this.output_phrase = "";
            int i = 0;
            foreach (char letter in this.input_phrase)
            {  // Letter by letter.
                if (letter != ' ')
                {                      // If it's different to space:
                    i = i >= this.value.Length ? 0 : i;   // Check the index i.  
                    // Get the position: Index of the letter in alphabet plus the digit.
                    int position = (Array.IndexOf(this.alphabet, letter) + Convert.ToInt16(this.value[i].ToString())) % 26;
                    this.output_phrase += this.alphabet[position]; // Add the specific letter to the output, according to the position.
                    i++;
                }
                else
                {
                    i = 0;                                 // Reset index i.
                    this.output_phrase += ' ';             // Add space to the output.
                }
            }
        }

        // Decoding procedure.
        public override void Decode()
        {
            this.is_processed = true;
            this.output_phrase = "";
            int i = 0;
            foreach (char letter in this.input_phrase)
            {   // Letter by letter.
                if (letter != ' ')
                {
                    i = i >= this.value.Length ? 0 : i;    // Check the index i. 
                    int position = Array.IndexOf(this.alphabet, letter) - Convert.ToInt16(this.value[i].ToString());
                    position = position < 0 ? position += 26 : position;
                    this.output_phrase += this.alphabet[position];
                    i++;
                }
                else
                {
                    i = 0;
                    this.output_phrase += ' ';
                }
            }
        }
    }
}
