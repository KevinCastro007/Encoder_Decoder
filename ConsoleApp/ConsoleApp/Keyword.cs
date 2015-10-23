using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Keyword : Encoder_Decoder
    {
        private string keyword;

        public Keyword(string input_phrase, string keyword) : base(input_phrase) { this.keyword = keyword; }

        public void SetKeyword(string keyword) { this.keyword = keyword; }

        public void Export(string file_name)
        {
            base.Export("Special Word. - : " + this.keyword, file_name);
        }

        public override void Encode()
        {
            this.is_processed = true;
            this.output_phrase = "";
            int i = 0;
            foreach (char letter in this.input_phrase)
            {
                if (letter != ' ')
                {
                    i = i >= this.keyword.Length ? 0 : i;
                    int position = Array.IndexOf(this.alphabet, letter) + Array.IndexOf(this.alphabet, this.keyword[i]) + 2;
                    position = position > 26 ? position % 26 : position;
                    position = position != 0 ? position - 1 : position;
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

        public override void Decode()
        {
            this.is_processed = true;
            this.output_phrase = "";
            int i = 0;
            foreach (char letter in this.input_phrase)
            {
                if (letter != ' ')
                {
                    i = i >= this.keyword.Length ? 0 : i;
                    int position = (Array.IndexOf(this.alphabet, letter) + 1) - (Array.IndexOf(this.alphabet, this.keyword[i]) + 1);
                    position = position < 0 ? position += 26 : position;
                    position = position == 0 ? 26 : position;
                    this.output_phrase += this.alphabet[position - 1];
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
