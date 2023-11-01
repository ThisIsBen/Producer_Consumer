using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producer_Consumer
{
    class FileIO
    {
       

        public static bool IsFileReady(string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            FileStream inputStream = null;
            try
            {
                inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                return true ;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Close();
                }
                    
            }
        }
        public static void waitUntilFileIsReady(string filename)
        {
            while (!IsFileReady(filename))
            {
                
                //Waiting for the file to be ready
                Thread.Sleep(1000);//wait a moment for processing copy}

                
            }

        }
    }
}
