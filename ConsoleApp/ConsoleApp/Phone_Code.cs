using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp
{
    public class Phone_Code : Encoder_Decoder
    {
        private Hashtable phone_numbers;

        public Phone_Code(string input_phrase) : base(input_phrase) {
            this.phone_numbers = new Hashtable();
            int key = 2;
            for (int i = 0; i < 26; i++)
            {
                if (key == 7)
                {
                    this.phone_numbers.Add(key, "" + this.alphabet[i] + this.alphabet[i + 1] + this.alphabet[i + 2] + this.alphabet[i + 3]);
                    i += 3;
                    key++;
                }
                else if (key == 9)
                {
                    this.phone_numbers.Add(key, "" + this.alphabet[i] + this.alphabet[i + 1] + this.alphabet[i + 2] + this.alphabet[i + 3]);
                    break;
                }
                else
                {
                    this.phone_numbers.Add(key, "" + this.alphabet[i] + this.alphabet[i + 1] + this.alphabet[i + 2]);
                    i += 2;
                    key++;
                }
            }        
        }

        public void Export(string file_name) { base.Export("Phone Code.", file_name); }

        public override void Encode()
        {
            this.is_processed = true;
            this.output_phrase = "";												// Replaces each space with a *.
            foreach (char letter in this.input_phrase)
            {							// Evaluates each letter of the string.
                if (letter != ' ')
                {
                    foreach (DictionaryEntry entry in this.phone_numbers)
                    {			// For each entry in the hast table.
                        if (entry.Value.ToString().Contains(letter.ToString()))
                        {	// Get the key and set the value.	
                            this.output_phrase += entry.Key.ToString() + (entry.Value.ToString().IndexOf(letter) + 1).ToString() + " ";
                            break;
                        }
                    }
                }
                else
                {
                    this.output_phrase += "* ";
                }
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
                    while (this.input_phrase[i] != '*')
                    {
                        substring = this.input_phrase[i] != ' ' ? substring += this.input_phrase[i] : substring;
                        i++;
                        if (i == this.input_phrase.Length)
                        {
                            break;
                        }
                    }
                    i--;
                    for (int j = 0; j < substring.Length; j++)
                    {
                        if (j >= substring.Length)
                        {
                            break;
                        }
                        this.output_phrase += (this.phone_numbers[Convert.ToInt32(substring[j].ToString())].ToString())[Convert.ToInt32(substring[j + 1].ToString()) - 1];
                        j++;
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
