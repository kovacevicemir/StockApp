using Microsoft.Win32;
using StockApp.Models;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace StockApp
{
    public static class Global
    {
        public static Member thisUser = new Member();

        //SERIALIZE OBJECT TO XML STRING METHOD
        public static string SerializeToXml(object value)
        {
            StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
            XmlSerializer serializer = new XmlSerializer(value.GetType());
            serializer.Serialize(writer, value);
            return writer.ToString();
        }

        //WRITE TO .XML FILE
        public static void SaveToXML(string xmlString)
        {
            //Create new save dialog
            var sfd = new SaveFileDialog
            {
                Filter = "XML-File | *.xml",
            };

            if (sfd.ShowDialog() == true)
            {
                //filename is path+filename
                string filename = sfd.FileName;

                File.WriteAllText(@filename, xmlString);
                MessageBox.Show("XML created successfully !");

            }
        }

        //SAVE DATATABLE TO XML. File
        public static void SaveDataTableToXml(DataTable datatable)
        {
            //Call Serialize table to string method first
            string xml_table = SerializeTableToString(datatable);

            //Create new save dialog
            var sfd = new SaveFileDialog
            {
                Filter = "XML-File | *.xml",
            };

            if (sfd.ShowDialog() == true)
            {
                //filename is path+filename
                string filename = sfd.FileName;


                // Save to disk
                File.WriteAllText(filename, xml_table);
            }
        }

        //SERIALIZE DATA TABLE TO XML STRING
        public static string SerializeTableToString(DataTable table)
        {
            if (table == null)
            {
                return null;
            }
            else
            {
                using (var sw = new StringWriter())
                using (var tw = new XmlTextWriter(sw))
                {
                    // Must set name for serialization to succeed.
                    table.TableName = @"MyTable";

                    // --

                    tw.Formatting = Formatting.Indented;

                    tw.WriteStartDocument();
                    tw.WriteStartElement(@"data");

                    ((IXmlSerializable)table).WriteXml(tw);

                    tw.WriteEndElement();
                    tw.WriteEndDocument();

                    // --

                    tw.Flush();
                    tw.Close();
                    sw.Flush();

                    return sw.ToString();
                }
            }
        }

        //Write to custom Log.txt file method
        public static void CustomLog(string outputMessage)
        {
            string sDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyApplicationDir");
            //MessageBox.Show(sDirectory.ToString());

            if (!Directory.Exists(sDirectory))
            {
                Directory.CreateDirectory(sDirectory);
                using (FileStream stream = File.Create(Path.Combine(sDirectory, "Log.txt")))
                {
                    stream.Close();
                }
            }

            using (StreamWriter writer = new StreamWriter(sDirectory + "/Log.txt", append: true))
            {
                writer.WriteLine(outputMessage);
                writer.Close();
            }
        }

        //Used for compare button handler -> open compare window only if user select data 1 and data 2 to compare (simple switch)
        public static bool compare1 = false;
        public static bool compare2 = false;

        //Application name
        public static string AppName = "Stock Application";

        //CompareTwo Table
        public static bool CompareTwoTable = false;

        //VERIFICATION METHOD - 0-9 A-Z
        public static bool Verification(string verify)
        {
            if (verify != "")
            {
                bool verification = Regex.IsMatch(verify, @"^[a-zA-Z0-9]+$");
                if (verification == false)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        //OVERLOAD VERIFICATION METHOD CHECK DATATABLE -> Check if dataTable is empty
        public static bool Verification(DataTable verify)
        {
            if(verify != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //OVERLOAD VERIFICATION METHOD CHECK DATATABLE -> Check if dataTable have more than X amount of rows
        public static bool Verification(DataTable verify, int rowNumber)
        {
            if (verify != null)
            {
                int counter = 0;
                foreach(DataRow row in verify.Rows)
                {
                    counter++;
                }
                if(counter > rowNumber)
                {
                    return true; //Table is not null and it have more than int rowNumber rows...
                }
                else
                {
                    return false; //Table have less than int rowNumber rows, return false...
                }
            }
            else
            {
                return false; //Table is null return false...
            }
        }

    }
}