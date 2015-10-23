using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    public class Transposition : Encoder_Decoder
    {
        public Transposition(string input_phrase) : base(input_phrase) { }

        public void Export(string file_name)
        {
            base.Export("Transposition.", file_name);
        }

        private void Encode_Decode()
        {
            this.is_processed = true;
            this.output_phrase = "";
            string substring = "";
            for (int i = 0; i < this.input_phrase.Length; i++)
            {
                if (this.input_phrase[i] != ' ')
                {
                    substring = "";
                    while (this.input_phrase[i] != ' ')
                    {		// Until there is not a space: create the substring.
                        substring += this.input_phrase[i];
                        i++;
                        if (i == this.input_phrase.Length)
                        {    // Ends if it's the end of the phrase.
                            break;
                        }
                    }
                    i--;
                    char[] arr = substring.ToCharArray();
                    Array.Reverse(arr);
                    this.output_phrase += new string(arr);		// Add the reversed substring version.
                }
                else
                {
                    this.output_phrase += ' ';
                }
            }
        }

        // Look for words around the phrase, reverse them and the concat them to the output phrase.
        public override void Encode() { this.Encode_Decode(); }

        public override void Decode() { this.Encode_Decode(); }
    }
}
