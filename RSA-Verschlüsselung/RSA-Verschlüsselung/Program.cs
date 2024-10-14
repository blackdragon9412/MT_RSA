using System.Text;

namespace RSA_Verschlüsselung
{
    internal class Program
    {

        public static long ModEXP(long Basis, long exponent, long modulus) // Methode zur Berechnung des d`s für den private key, benutzt modulare Exponentialrechnung || Satz von Euler
        {
            
            long ergebniss = 1;
            

            while (exponent > 0) { 

                if ((exponent & 1) != 0) // Schaut darauf ob der exponent gerade ist, weil es funktioniert nur mit vielfachen von 2
                {

                    ergebniss = (ergebniss * Basis) % modulus;// macht den Moduli und multipliziert mit basis mit exponent


                }

                exponent  = exponent >> 1; // versetzt bit nach rechts um 1, = exponent / 2 mit Runden
                Basis = (Basis * Basis) % modulus; // quadriert die Basis


            }

            return ergebniss; // returned das ergebniss
        }
    
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode; // Ändert die Konsolen Codierung zu Unicode, weil manche Zeichen nicht in ASCII dargestellt werden können

            string text = "Mississippi";// Input Text
            

            char[] chars = text.ToCharArray();  // Umwandlung in Char array

            int[] unicodeArray = new int[chars.Length]; // int Array wo die Buchstaben in integer umgewandelt werden


            for (int i = 0; i < chars.Length; i++) // loop zum um wandeln der chars in int
            {
                unicodeArray[i] = (int)chars[i];
            }



            Console.WriteLine();

            int p = 11;// primzahelen paar 
            int q = 13;

            int N = p * q; 

            int phi = (11 - 1) * (13 - 1); // berechnung von phi von N 

            int e = 65537; // Primzahl die Teilerfremd zu p und q ist

            int d = 113;// multiples inverse von e


            for (int i = 0; i < unicodeArray.Length; i++) // Loop, der die zahlen der Buchstaben mit dem e und dem N berchnet
            {

                unicodeArray[i] = (int)ModEXP(unicodeArray[i],e,N);
                 
            }

            StringBuilder base64Output = new StringBuilder();

            foreach (int encryptedValue in unicodeArray)
            {
                
                byte[] bytes = BitConverter.GetBytes(encryptedValue); // Wandelt integer in bytes um

                
                base64Output.Append(Convert.ToBase64String(bytes) + " "); // Wandelt in Base64 um 
            }

            Console.WriteLine("Base64 encoded encrypted text:"); 
            Console.WriteLine(base64Output.ToString()); // Gibt das Array aus

            Console.WriteLine();



        }
    }
}
