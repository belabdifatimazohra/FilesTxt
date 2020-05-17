/*
 * Date de creation: 15/04/2020;
 * Derniere modification: 15/05/2020
 * Auteur: BELABDI Fatima Zohra;
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Windows.Forms;


namespace FilesTxt
{
    class Program
    {
        static List<string> list = new List<string>();

        static void ReadFile(string SourcePath, string DistPath)
        {
            StreamReader reader = new StreamReader(SourcePath); // Fichier source
            StreamWriter writedata = new StreamWriter(DistPath); // Fichier Résultat //(path,true) in order to not delete the countent


           
            String Mat = ""; // Matricule
            Double SumImpact = 0;// Calcul de somme Impact
            Double Impact = 0; // Récuperer la valeur de l'Impact de la ligne en cours
            using (reader)
            {
                string line; // used to Read line
                // Parcourir le fichier
                while ((line = reader.ReadLine()) != null)
                {
                    String[] words = line.Split('\t');
                    // Verifier le mat precedant avec le mat en cours si différent faire un saut de ligne et calculer la somme
                    if (Mat.Equals(words[0])== false)
                    {
                        // Saut de ligne pour séparer les employes et calcul de l'impact 
                        writedata.WriteLine(" \t  \t  \t  \t  \t  \t  \t"   + Convert.ToString(SumImpact));
                        Console.WriteLine();
                        list.Add(words[0]);// Rajouter à la liste des matricules
                        Impact = 0; 
                        SumImpact = 0;
                    }
                    // Sinon copier coller du fichier source et fichier resultat
                    for (int i = 0; i < words.Length; i++)
                    {   
                        writedata.Write(words[i] + "\t");
                    }
                    // Calculer la somme de l'impacte
                    Impact = Convert.ToDouble(words[7]);
                    SumImpact = SumImpact + Impact;
                    Mat = words[0];
                    writedata.WriteLine();

                }
                // Ecrire dans le fichier resultat la dernière somme calculee
                writedata.WriteLine(" \t  \t  \t  \t  \t  \t  \t" + Convert.ToString(SumImpact));
            }


            reader.Close();
            writedata.Close();
        }


        static OpenFileDialog openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select a text file",
            Filter = "Text files (*.txt)|*.txt",
            Title = "Open text file"
        };


        
        [STAThread]
        static void Main(string[] args)
        {


         if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                 string SourcePath = openFileDialog1.FileName;

                 string DistPath = Path.ChangeExtension(SourcePath, null)+"_Result.txt";// Creation du fichier resultat

                    ReadFile(SourcePath, DistPath);

                }
                catch (SecurityException ex)
            {
                MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}" );
            }
        }
         
            DialogResult result = MessageBox.Show(" Traitement terminer ", "", MessageBoxButtons.OK);
            Console.ReadLine();

        }
    }

  
}
