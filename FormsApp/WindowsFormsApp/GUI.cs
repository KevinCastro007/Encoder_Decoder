using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp
{
    public partial class GUI : Form
    {
        private int? algorithm_number;
        private string pattern;
        private Regex rgx;

        public GUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private bool Validate_Phrase_Encode(string phrase)
        {
            if (phrase == "")
            {
                return false;
            }
            else
            {
                phrase = phrase.ToLower();
                pattern = "[^a-z ]";
                rgx = new Regex(pattern);
                return !rgx.IsMatch(phrase);
            }
        }

        private bool Validate_Phrase_Decode(string phrase, int algorithm)
        {
            if (phrase == "")
            {
                return false;
            }
            else
            {
                phrase = phrase.ToLower();
                switch (algorithm)
                {
                    case 1:
                    case 4:
                    case 5:                        
                        pattern = "[^a-z ]";
                        break;
                    case 2:
                        pattern = "[^0-1 *]";
                        break;
                    case 3:
                        pattern = "[^1-9 *]";
                        break;
                }
                rgx = new Regex(pattern);
                return !rgx.IsMatch(phrase);
            }
        }

        private bool ValidateVigenereValue(string value)
        {
            if (value == "")
            {
                return false;
            }
            else 
            {
                value = value.ToLower();
                pattern = "[^0-9]";
                rgx = new Regex(pattern);
                if (rgx.IsMatch(value))
                {
                    return false;
                }
                else
                {
                    return Convert.ToInt32(value) >= 10 && Convert.ToInt32(value) <= 99;
                }
            }
        }

        private bool ValidateKeyword(string keyword)
        {
            if (keyword == "")
            {
                return false;
            }
            else
            {
                keyword = keyword.ToLower();
                pattern = "[^a-z]";
                rgx = new Regex(pattern);
                return !rgx.IsMatch(keyword);
            }
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            if (this.algorithm_number == null)
            {
                MessageBox.Show("Select the encoding/decoding algorithm.");
            }
            else
            {
                if (Validate_Phrase_Encode(this.richTextPhrase.Text.Trim()))
                {
                    if (this.algorithm_number == 1)
                    {
                        string value = Microsoft.VisualBasic.Interaction.InputBox("Type the Value to proceed with the algorithm.", "Get Value", "", -1, -1);
                        if (ValidateVigenereValue(value.Trim()))
                        {
                            Vigenere_Substitution vs = new Vigenere_Substitution(this.richTextPhrase.Text.Trim(), value);
                            vs.Encode();
                            this.richTextPhrase.Text = vs.GetOutput_Phrase();
                        }
                        else
                        {
                            MessageBox.Show("WRONG!");
                        }
                    }
                    else if (this.algorithm_number == 5)
                    {
                        string keyword = Microsoft.VisualBasic.Interaction.InputBox("Type the Keyword to proceed with the algorithm.", "Get Keyword", "", -1, -1);
                        if (ValidateKeyword(keyword.Trim()))
                        {
                            Keyword kw = new Keyword(this.richTextPhrase.Text, keyword);
                            kw.Encode();
                            this.richTextPhrase.Text = kw.GetOutput_Phrase();
                        }
                        else
                        {
                            MessageBox.Show("WRONG!");
                        }
                    }
                    else
                    {
                        switch (this.algorithm_number)
                        {
                            case 2:
                                Binary_Code bc = new Binary_Code(this.richTextPhrase.Text);
                                bc.Encode();
                                this.richTextPhrase.Text = bc.GetOutput_Phrase();
                                break;
                            case 3:
                                Phone_Code pc = new Phone_Code(this.richTextPhrase.Text);
                                pc.Encode();
                                this.richTextPhrase.Text = pc.GetOutput_Phrase();
                                break;
                            case 4:
                                Transposition tr = new Transposition(this.richTextPhrase.Text);
                                tr.Encode();
                                this.richTextPhrase.Text = tr.GetOutput_Phrase();
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("INPUT ERROR");
                }
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {               
            if (this.algorithm_number == null)
            {
                MessageBox.Show("Select the encoding/decoding algorithm.");
            }
            else 
            {
                if (Validate_Phrase_Decode(this.richTextPhrase.Text, (int)this.algorithm_number))
                {
                    if (this.algorithm_number == 1)
                    {
                        string value = Microsoft.VisualBasic.Interaction.InputBox("Type the Value to proceed with the algorithm.", "Get Value", "", -1, -1);
                        if (ValidateVigenereValue(value.Trim()))
                        {
                            Vigenere_Substitution vs = new Vigenere_Substitution(this.richTextPhrase.Text.Trim(), value);
                            vs.Decode();
                            this.richTextPhrase.Text = vs.GetOutput_Phrase();
                        }
                        else
                        {
                            MessageBox.Show("WRONG!");
                        }
                    }
                    else if (this.algorithm_number == 5)
                    {
                        string keyword = Microsoft.VisualBasic.Interaction.InputBox("Type the Keyword to proceed with the algorithm.", "Get Keyword", "", -1, -1);
                        if (ValidateKeyword(keyword.Trim()))
                        {
                            Keyword kw = new Keyword(this.richTextPhrase.Text, keyword);
                            kw.Decode();
                            this.richTextPhrase.Text = kw.GetOutput_Phrase();
                        }
                        else
                        {
                            MessageBox.Show("WRONG!");
                        }
                    }
                    else
                    {
                        switch (this.algorithm_number)
                        {
                            case 2:
                                Binary_Code bc = new Binary_Code(this.richTextPhrase.Text);
                                bc.Decode();
                                this.richTextPhrase.Text = bc.GetOutput_Phrase();
                                break;
                            case 3:
                                Phone_Code pc = new Phone_Code(this.richTextPhrase.Text);
                                pc.Decode();
                                this.richTextPhrase.Text = pc.GetOutput_Phrase();
                                break;
                            case 4:
                                Transposition tr = new Transposition(this.richTextPhrase.Text);
                                tr.Decode();
                                this.richTextPhrase.Text = tr.GetOutput_Phrase();
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("INPUT ERROR");
                }   
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export!");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.algorithm_number = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.algorithm_number = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.algorithm_number = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            this.algorithm_number = 4;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            this.algorithm_number = 5;
        }
    }
}
