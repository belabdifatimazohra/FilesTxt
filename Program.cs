/*
 * Date de creation: 15/04/2020;
 * Derniere modification: 20/05/2020
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
                    if (Mat.Equals(words[0]) == false)
                    {
                        // Saut de ligne pour séparer les employes et calcul de l'impact 
                        writedata.WriteLine(" \t  \t  \t  \t  \t  \t  \t" + Convert.ToString(SumImpact));
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


        static void GetActiveListe(string SourcePath, string DistPath)
        {
            StreamReader reader = new StreamReader(SourcePath); // Fichier source
            StreamWriter writedata = new StreamWriter(DistPath); // Fichier Résultat //(path,true) in order to not delete the countent
            
            using (reader)
            {
                string line; // used to Read line
                // Parcourir le fichier
                while ((line = reader.ReadLine()) != null)
                {
                    String[] words = line.Split('\t');
                    // Verifier si le mat existe dans la liste 
                    if (list.Contains(words[0]))
                    {
                        // copier coller du fichier source et fichier resultat
                        for (int i = 0; i < words.Length; i++)
                        {
                            writedata.Write(words[i] + "\t");
                        }
                        writedata.WriteLine();

                    }
                    

                }
            }
            
            reader.Close();
            writedata.Close();
        }

        // Pour le fichier de calcul des sommes
        static OpenFileDialog openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select a text file",
            Filter = "Text files (*.txt)|*.txt",
            Title = "Open text file"
        };

        // Pou la lecture de fichier complet (Active + Autres)
        static OpenFileDialog FileDialogAll = new OpenFileDialog()
        {
            FileName = "Selectioner Fichier Global",
            Filter = "Text files (*.txt)|*.txt",
            Title = "Fichier Global"
        };

        static OpenFileDialog FileDialogActive = new OpenFileDialog()
        {
            FileName = "Selectionner la liste à extraire du fichier",
            Filter = "Text files (*.txt)|*.txt",
            Title = "Liste Matricule"
        };

        [STAThread]
        static void Main(string[] args)
        {

            /* Creation fichier resultat de calcul
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
            }*/

            if (FileDialogAll.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Recuperer le fichier a traiter
                    string SourcePath = FileDialogAll.FileName;

                    string DistPath = Path.ChangeExtension(SourcePath, null) + "_Result.txt";// Creation du fichier resultat

                    // Recuperer le fiche de la liste à rechercher

                    if (FileDialogActive.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string SourcePathA = openFileDialog1.FileName;
                           // Lecture du fichier et récuperation des Mat dans ue liste

                            StreamReader reader = new StreamReader(SourcePath); // Fichier source
                            string msgList = "";

                            using (reader)
                            {
                                string line; // used to Read line

                                // Parcourir le fichier
                                while ((line = reader.ReadLine()) != null)
                                {
                                    String[] words = line.Split('\t');
                                    list.Add(words[0]);
                                    msgList = msgList + " " + words[0];


                                }
                            }
                            reader.Close();
                           // DialogResult listResult = MessageBox.Show(msgList, "", MessageBoxButtons.OK);


                        }
                        catch (SecurityException ex)
                        {
                            MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                            $"Details:\n\n{ex.StackTrace}");
                        }
                    }

                    // Appel la fonction
                    GetActiveListe(SourcePath, DistPath);

                }


                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }



                DialogResult result = MessageBox.Show(" Traitement terminer ", "", MessageBoxButtons.OK);
                Console.ReadLine();

            }
        }


    }
}
