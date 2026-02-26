using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DVLD.Global
{
    static public class clsutil
    {

        static private string GeneratGuid()
        {

            Guid guid1 = Guid.NewGuid();
            return guid1.ToString();


        }


        static private string ReplaceFileNameWithGuid(string sourcefile)
        {
            string filename = sourcefile;
            FileInfo fi = new FileInfo(filename);
            string extn = fi.Extension;

            return GeneratGuid() + extn;
        }


        static private bool CreateFolderIfDoesNotExist(string DestiationFolder)
        {
            if (Directory.Exists(DestiationFolder) == false)
            {
                try
                {

                    Directory.CreateDirectory(DestiationFolder);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }


            }
            return true;
        }

        static public bool CopyImageToProjectImageFolder(ref string sourcefile)
        {

            string DestiationFolder = @"C:\DVLD-People-Images\";
            if (CreateFolderIfDoesNotExist(DestiationFolder) == false)
            {

                return false;
            }
            string destinationFile = DestiationFolder + ReplaceFileNameWithGuid(sourcefile);

            try
            {
                File.Copy(sourcefile, destinationFile);


            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            sourcefile = destinationFile;
            return true;



        }


    }
}
