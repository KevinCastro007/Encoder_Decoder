using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace Model {

	public abstract class Encoder_Decoder {

		// Class attributes.
		protected string input_phrase;
		protected string output_phrase;
		protected bool is_processed;
		protected char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        private static string pattern;
        private static Regex rgx;

		// Class constructor.
		public Encoder_Decoder(string input_phrase) {
			this.input_phrase = input_phrase.ToLower().Trim();
		}

		// Class destructor.
		~Encoder_Decoder() {}

		// Attributes setters.
		public void SetInput_Phrase(string input_phrase) { this.input_phrase = input_phrase; }
		public void SetOutput_Phrase(string output_phrase) { this.output_phrase = output_phrase; }

		// Attributes getters.
		public string GetInput_Phrase() { return this.input_phrase; }
		public string GetOutput_Phrase() { return this.output_phrase; }

		// Exports the input and output phrases (encoded or decoded). (Only if it has been processed.)
		public void Export(string algorithm, string file_name) {

			if (is_processed) {
		        using (StreamWriter sw = new StreamWriter(file_name + ".txt")) {
		        	sw.WriteLine("Algorithm used: " + algorithm);
		        	sw.Write("Input Phrase: "); sw.WriteLine(this.input_phrase); 
		        	sw.Write("Output Phrase: "); sw.WriteLine(this.output_phrase);
		        }
				using (XmlWriter xw = XmlWriter.Create(file_name + ".xml")) {
				    xw.WriteStartDocument();
				    xw.WriteStartElement("Algorithm used: " + algorithm);
					xw.WriteElementString("Input Phrase: ", this.input_phrase);
					xw.WriteElementString("Output Phrase: ", this.output_phrase);
				    xw.WriteEndDocument();
				}
				Console.WriteLine("Successful exportation.");
			} else { 
				Console.WriteLine("The input phrase must be processed. Encode or decoded it first!"); 
			}

		}

		// Encodes input phrase and stores it in output phrase.
		public abstract void Encode();

		// Decodes input phrase and stores it in the output phrase.
		public abstract void Decode();

		// Validates a string according to a stablished pattern (Any character that's not is the range a to z or isn't a space.).
		public static bool ValidateInput_Phrase(string phrase) {
         	phrase = phrase.ToLower();
         	pattern = "[^a-z ]";			
         	rgx = new Regex(pattern);
         	return !rgx.IsMatch(phrase);
		}

	}

	// Vigenere substitution algorithm.
	public class Vigenere_Substitution : Encoder_Decoder {

		private string value;
	
		public Vigenere_Substitution(string input_phrase, string value) : base(input_phrase) { this.value = value; }		

		public void Export(string file_name) {
			base.Export("Vigenere Substitution. - : " + this.value, file_name);
		}
		
		public void SetValue(string value) { this.value = value; }
		public string GetValue() { return this.value; }
		
        // Encoding procedure.
		public override void Encode() { 

			this.is_processed = true;
			this.output_phrase = "";
			int i = 0;
			foreach (char letter in this.input_phrase) {  // Letter by letter.
				if (letter != ' ') {                      // If it's different to space:
					i = i >= this.value.Length ? 0 : i;   // Check the index i.  
                                                          // Get the position: Index of the letter in alphabet plus the digit.
					int position = (Array.IndexOf(this.alphabet, letter) + Convert.ToInt16(this.value[i].ToString())) % 26;
					this.output_phrase += this.alphabet[position]; // Add the specific letter to the output, according to the position.
					i++;
				} else {
					i = 0;                                 // Reset index i.
					this.output_phrase += ' ';             // Add space to the output.
				}
			}
			
		}
	
        // Decoding procedure.
		public override void Decode() { 
		
			this.is_processed = true;
			this.output_phrase = "";
			int i = 0;
			foreach (char letter in this.input_phrase) {   // Letter by letter.
				if (letter != ' ') {                    
					i = i >= this.value.Length ? 0 : i;    // Check the index i. 
					int position = Array.IndexOf(this.alphabet, letter) - Convert.ToInt16(this.value[i].ToString());
					position = position < 0 ? position += 26 : position;
					this.output_phrase += this.alphabet[position];
					i++;
				} else {
					i = 0;
					this.output_phrase += ' ';
				}
			}

		}
		
	}	

	public class Phone_Code : Encoder_Decoder {
		
		private Hashtable phone_numbers;		

		public Phone_Code(string input_phrase) : base(input_phrase) { }	

		// Sets the confirguration according to the algorithm.
		public void SetPhone_Numbers() {
			
			this.phone_numbers = new Hashtable();
			int key = 2;
			for (int i = 0; i < 26; i++) {
				if (key == 7) {
					this.phone_numbers.Add(key, "" + this.alphabet[i] + this.alphabet[i + 1] + this.alphabet[i + 2] + this.alphabet[i + 3]);
					i += 3;
                	key++;
				}
			   	else if (key == 9) {
               		this.phone_numbers.Add(key, "" + this.alphabet[i] + this.alphabet[i + 1] + this.alphabet[i + 2] + this.alphabet[i + 3]);             		
	                break;
    	        } else {
    	        	this.phone_numbers.Add(key, "" + this.alphabet[i] + this.alphabet[i + 1] + this.alphabet[i + 2]);   	        	
                	i += 2;
                	key++;
                }
            }
			
		}
			
		public void Export(string file_name) { base.Export("Phone Code.", file_name); }

		public override void Encode() {

			this.is_processed = true;
			this.output_phrase = "";												// Replaces each space with a *.
			foreach (char letter in this.input_phrase) {							// Evaluates each letter of the string.
				if (letter != ' ') {						
					foreach (DictionaryEntry entry in this.phone_numbers) {			// For each entry in the hast table.
						if (entry.Value.ToString().Contains(letter.ToString())) {	// Get the key and set the value.	
							this.output_phrase += entry.Key.ToString() + (entry.Value.ToString().IndexOf(letter) + 1).ToString() + " ";
							break;
						}
					}						
				} else {
					this.output_phrase += "* ";				        
           		}
			}
		}	
		
		public override void Decode() {

          	this.is_processed = true;
          	this.output_phrase = "";
          	string substring;
          	for (int i = 0; i < this.input_phrase.Length; i++) { 
              	if (this.input_phrase[i] != ' ' && this.input_phrase[i] != '*') {
                    substring = "";
                    while (this.input_phrase[i] != '*') {
                    	substring = this.input_phrase[i] != ' ' ? substring += this.input_phrase[i] : substring;
                        i++;
                        if (i == this.input_phrase.Length) {
                        	break;
                        }
                    }
                    i--;
                    for (int j = 0; j < substring.Length; j++) {
                        if (j >= substring.Length) { 
                        	break; 
                        }
                        this.output_phrase += (this.phone_numbers[Convert.ToInt32(substring[j].ToString())].ToString())[Convert.ToInt32(substring[j + 1].ToString()) - 1];
                        j++;
                    }
             	} else {
             		this.output_phrase = this.input_phrase[i] == '*' ? this.output_phrase += ' ' : this.output_phrase;
            	}		
			}
		}
	}
	
	public class Binary_Code : Encoder_Decoder {
		
		private Hashtable binary_equivalents;

		public Binary_Code(string input_phrase) : base(input_phrase) { }	

		// Decimal to binary (5 bits).
		public string Decimal_To_Binary(int n) {
			int[] bin = new int[5];
			int i = 4;
			while (n != 0) {
				bin[i] = n % 2;
				n /= 2;
				i--;
			}
			string str_bin = "";
			foreach (int bit in bin) { 
				str_bin += bit; 
			}
			return str_bin;
		}

		// Set the hash table.
		public void SetBinary_Equivalents() {
			
			this.binary_equivalents = new Hashtable();
			for (int i = 0; i < 26; i++) {					// Key: Alphabet letter - Value: Binary representation.
				this.binary_equivalents.Add(alphabet[i].ToString(), this.Decimal_To_Binary(i));
			}		
			
		}
		
		public void Export(string file_name) { 
			base.Export("Binary Code.", file_name); 
		}

		public override void Encode() {
			
			this.is_processed = true;
			this.output_phrase = "";
			foreach (char letter in this.input_phrase) {	// For each letter in the string looks the value in the hash.s
				this.output_phrase = letter != ' ' ? this.output_phrase += this.binary_equivalents[letter.ToString()] + " " : this.output_phrase += "* ";
         	}
			
		}
		
		public override void Decode() {
			
         	this.is_processed = true;
         	this.output_phrase = "";
			string substring;
			for (int i = 0; i < this.input_phrase.Length; i++) {
				if (this.input_phrase[i] != ' ' && this.input_phrase[i] != '*') {					
					substring = "";
					while (this.input_phrase[i] != ' ') {		// Until there is not a space: create the substring.
						substring += this.input_phrase[i];		
						i++;
						if (i >= this.input_phrase.Length) {	// Ends if it's the end of the phrase.
							break;
						}
					}
					i--;										// Look for the Key related to the Value (substring).	
					foreach (DictionaryEntry entry in this.binary_equivalents) {							
						if (entry.Value.ToString() == substring) {			
							this.output_phrase += entry.Key;		
							break;
						}
					}	
				} else {	
					this.output_phrase = this.input_phrase[i] == '*' ? this.output_phrase += ' ' : this.output_phrase;
				}
			}
			
		}

	}		

	public class Transposition : Encoder_Decoder {

		public Transposition(string input_phrase) : base(input_phrase) { }		

		public void Export(string file_name) {
			base.Export("Transposition.", file_name);
		}
		
		private void Encode_Decode() {
			
			this.is_processed = true;
         	this.output_phrase = "";
			string substring = "";
			for (int i = 0; i < this.input_phrase.Length; i++) {
				if (this.input_phrase[i] != ' ') {
					substring = "";
					while (this.input_phrase[i] != ' ') {			// Until there is not a space: create the substring.
						substring += this.input_phrase[i];
						i++;
						if (i == this.input_phrase.Length) {		// Ends if it's the end of the phrase.
							break;
						}
					}
					i--;
					char[] arr = substring.ToCharArray();
					Array.Reverse(arr);
					this.output_phrase += new string(arr);		// Add the reversed substring version.
				} else { 
					this.output_phrase += ' '; 
				}
			}		
		
		}

		// Look for words around the phrase, reverse them and the concat them to the output phrase.
		public override void Encode() { this.Encode_Decode(); }
	
		public override void Decode() { this.Encode_Decode(); }

	}
	
	public class Keyword : Encoder_Decoder {

		private string keyword;
	
		public Keyword(string input_phrase, string keyword) : base(input_phrase) { this.keyword = keyword; }		
		
		public void SetKeyword(string keyword) { this.keyword = keyword; }  

		public void Export(string file_name) {
			base.Export("Special Word. - : " + this.word, file_name);
		}		
		
		public override void Encode() { 

			this.is_processed = true;
			this.output_phrase = "";
			int i = 0;
			foreach (char letter in this.input_phrase) {
				if (letter != ' ') {
					i = i >= this.keyword.Length ? 0 : i;
					int position = Array.IndexOf(this.alphabet, letter) + Array.IndexOf(this.alphabet, this.keyword[i]) + 2;
					position = position > 26 ? position % 26 : position;
					position = position != 0 ? position - 1 : position;
					this.output_phrase += this.alphabet[position];					
					i++;
				} else {
					i = 0;
					this.output_phrase += ' ';
				}
			}
			
		}
	
		public override void Decode() { 
		
			this.is_processed = true;
			this.output_phrase = "";
			int i = 0;
			foreach (char letter in this.input_phrase) {
				if (letter != ' ') {
					i = i >= this.keyword.Length ? 0 : i;
					int value = (Array.IndexOf(this.alphabet, letter) + 1) - (Array.IndexOf(this.alphabet, this.keyword[i]) + 1);
					position = position < 0 ? position += 26 : position;
					position = position == 0 ? 26 : position;
					this.output_phrase += this.alphabet[position - 1];
					i++;
				} else {
					i = 0;
					this.output_phrase += ' ';
				}
			}

		}
		
	}

}

namespace Console.

//                                                                                                                                                                    ccccccccccccccccccccccccccccc

namespace Console {

	public class Console_View {

		private static int algoritm;
		private static string input_phrase;
        private static int procedure; 
        private static string export;
        private static int value;
        private static string keyword;
        private static string file_name;
		
		public Console_View() {}

		public void Run() {
     	
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
         	switch (algoritm) {
         		case 1:
					Console.WriteLine("\nVigenere Substitution.");
					Console.Write("Type the value: ");
         			value = Convert.ToInt32(Console.ReadLine());
         			if (value > 10 && value < 99) {
	         			Console.WriteLine("1 - Encode.");
	         			Console.WriteLine("2 - Decode.");
	         			Console.Write("Procedure #: ");
	         			procedure = Convert.ToInt32(Console.ReadLine());
	         			Vigenere_Substitution vs = new Vigenere_Substitution(input_phrase, value.ToString());
	         			switch (procedure) {
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
	         			switch (export[0]) {
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
         			else {
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
         			switch (procedure) {
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
         			switch (export[0]) {
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
         			switch (procedure) {
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
         			switch (export[0]) {
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
         			switch (procedure) {
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
         			switch (export[0]) {
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
         			switch (procedure) {
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
         			switch (export[0]) {
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