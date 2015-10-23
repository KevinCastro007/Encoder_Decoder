using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WindowsFormsApp
{
    public class Binary_Code : Encoder_Decoder
    {
        private Hashtable binary_equivalents;

        public Binary_Code(string input_phrase) : base(input_phrase) 
        {
            this.binary_equivalents = new Hashtable();
            for (int i = 0; i < 26; i++)
            {					// Key: Alphabet letter - Value: Binary representation.
                this.binary_equivalents.Add(alphabet[i].ToString(), this.Decimal_To_Binary(i));
            }        
        }

        // Decimal to binary (5 bits).
        public string Decimal_To_Binary(int n)
        {
            int[] bin = new int[5];
            int i = 4;
            while (n != 0)
            {
                bin[i] = n % 2;
                n /= 2;
                i--;
            }
            string str_bin = "";
            foreach (int bit in bin)
            {
                str_bin += bit;
            }
            return str_bin;
        }

        // Set the hash table.
        public void SetBinary_Equivalents()
        {
            this.binary_equivalents = new Hashtable();
            for (int i = 0; i < 26; i++)
            {					// Key: Alphabet letter - Value: Binary representation.
                this.binary_equivalents.Add(alphabet[i].ToString(), this.Decimal_To_Binary(i));
            }

        }

        public void Export(string file_name)
        {
            base.Export("Binary Code.", file_name);
        }

        public override void Encode()
        {
            this.is_processed = true;
            this.output_phrase = "";
            foreach (char letter in this.input_phrase)
            {	// For each letter in the string looks the value in the hash.s
                this.output_phrase = letter != ' ' ? this.output_phrase += this.binary_equivalents[letter.ToString()] + " " : this.output_phrase += "* ";
            }

        }

        public override void Decode()
        {
            this.is_processed = true;
            this.output_phrase = "";
            string substring;
            for (int i = 0; i < this.input_phrase.Length; i++)
            {
                if (this.input_phrase[i] != ' ' && this.input_phrase[i] != '*')
                {
                    substring = "";
                    while (this.input_phrase[i] != ' ')
                    {		// Until there is not a space: create the substring.
                        substring += this.input_phrase[i];
                        i++;
                        if (i >= this.input_phrase.Length)
                        {	// Ends if it's the end of the phrase.
                            break;
                        }
                    }
                    i--;										// Look for the Key related to the Value (substring).	
                    foreach (DictionaryEntry entry in this.binary_equivalents)
                    {
                        if (entry.Value.ToString() == substring)
                        {
                            this.output_phrase += entry.Key;
                            break;
                        }
                    }
                }
                else
                {
                    this.output_phrase = this.input_phrase[i] == '*' ? this.output_phrase += ' ' : this.output_phrase;
                }
            }
        }
    }
}
