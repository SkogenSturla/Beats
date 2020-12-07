using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Beats
{
    class forFort
    {
        double bpm = 100;

        public double oldBPM
        {
            get
            {
                return bpm;
            }

            set
            {
               bpm = value;
            }
        }
    }
    
        class clsRandomAccess
        {
            // ------------------ Constants --------------------
            const int NAMESIZES = 3;
            const int TID = 3;

            const int RECORDSIZE = NAMESIZES + 4;          // First name

            // ----------------------- Instance variables ------------------
            private string bpm;       // Demographics
            private string counter;

            private string errorMessage;
            private string fileName;

            private FileStream myFile;
            private BinaryReader br;
            private BinaryWriter bw;

            // ----------------------- Constructor --------------------
            public clsRandomAccess()
            {
                // initialise instance variables
                myFile = null;
                errorMessage = "??";
                fileName = "Friends.txt";               // Default file name
            }
            public clsRandomAccess(string fn)
                : this()    // Call no-arg constructor first
            {
                fileName = fn;
            }
            #region Property Methods
            // -------------- Property Methods --------------------

            public string BPM
            {
                get
                {
                    return bpm;
                }
                set
                {
                bpm = value;                   // Do we have a string?                  
                }
            }



            public string Counter
            {
                get
                {
                    return counter;
                }
                set
                {
                counter = value;                  // Do we have a string?
                }
            }


            public string FileName
            {
                get
                {
                    return fileName;
                }

                set
                {
                    if (value.Length > 0)
                        fileName = value;
                }
            }

            public FileStream MyFile
            {
                get
                {
                    return myFile;
                }

                set
                {
                    myFile = value;
                }
            }

            public BinaryReader BinReader
            {
                get
                {
                    return br;
                }

                set
                {
                    br = value;
                }
            }

            public BinaryWriter BinWriter
            {
                get
                {
                    return bw;
                }

                set
                {
                    bw = value;
                }
            }

            public String ErrorText
            {
                get
                {
                    return errorMessage;
                }
            }
            #endregion

            // -------------- General Methods --------------------
            /****
             * This creates a random access file.
             * 
             * Parameter list:
             *    fn       a string that holds the file name to use
             * 
             * Return value:
             *    int      0 if error, 1 otherwise 
             ****/
            public int Create(string fn)
            {
                try
                {
                    myFile = new FileStream(fn, FileMode.OpenOrCreate);
                    bw = new BinaryWriter(myFile);
                    fileName = fn;
                }
                catch
                {
                    return 0;
                }
                return 1;
            }

            /****
             * This opens a file for reading
             * 
             * Parameter list:
             *    fn        the file name
             * 
             * Return value:
             *    int      0 if error, 1 otherwise 
             ****/

            public int Open(string fn)
            {
                if (bw == null)
                {
                    return Create(fn);
                }
                else
                {
                    myFile = new FileStream(fn, FileMode.OpenOrCreate);
                }

                return 1;
            }


            /****
             * This closes the currently-open file.
             * 
             * Parameter list:
             *    n/a
             * 
             * Return value:
             *    void      
             ****/

            public void Close()
            {
                if (myFile != null)
                    myFile.Close();
                if (bw != null)
                    bw.Close();
                if (br != null)
                    br.Close();
            }

            /**
             * This writes one record to the currently-open file
             * 
             * Parameter list:
             *    num           an integer that holds the record number
             * 
             * Return value:
             *    int      0 if error, 1 otherwise 
             * 
             * CAUTION: this method assumes that the properties contain the
             * record to be written.
             */

            public int WriteOneRecord(long num)
            {
                int errorFlag = 1;

                try
                {
                    if (myFile != null && bw != null)
                    {   // Position the file pointer
                        myFile.Seek(num * RECORDSIZE, SeekOrigin.Begin);
                        bw = new BinaryWriter(myFile);

                        bw.Write(BPM);        // Write the data
                        bw.Write(Counter);
                        bw.Close();
                    }
                }
                catch (IOException ex)
                {
                    errorMessage = ex.Message;    // In case they want to view it.
                    errorFlag = 0;
                }
                return errorFlag;
            }

            /**
            * This reads one record and returns it as a string
            * 
            * Parameter list:
            *   num            an integer that holds the record number
            * 
            * Return value
            *   int            0 if error, 1 otherwise 
            */

            public int ReadOneRecord(long num)
            {
                try
                {
                    if (myFile != null)
                        myFile.Close();

                    myFile = new FileStream(fileName, FileMode.Open);
                    br = new BinaryReader(myFile);

                    if (myFile != null && br != null)
                    {
                        myFile.Seek(num * RECORDSIZE, SeekOrigin.Begin);    // Position the file pointer
                        BPM = br.ReadString();
                        Counter = br.ReadString();
                        br.Close();

                    }
                }
                catch (IOException ex)
                {
                    errorMessage = ex.Message;
                    return 0;
                }

                return 1;
            }
            /**
            * Purpose: This determines the number of records currently in the file
            * 
            * Parameter list:
            *   void
            * 
            * Return value
            *   long          the number of bytes in the file
            */
            public long getRecordCount()
            {
                long records = 0;
                long remainder;

                try
                {
                    if (myFile != null)
                    {
                        records = myFile.Seek(0, SeekOrigin.End);    // Position the file pointer
                        Close();
                    }
                }
                catch (IOException ex)
                {
                    //MessageBox.Show("Error: " + ex.Message);
                    return -1;
                }
                remainder = records % RECORDSIZE;     // See if there is a partial record
                records = records / RECORDSIZE;       // Calculate the records
                if (remainder > 0)                    // If there was a partial record...
                    records++;                          // ...up the counter to account for it.

                return records;
            }

        }
    }


