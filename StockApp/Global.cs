using Microsoft.Win32;
using StockApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //Feed dataset
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(datatable);

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
                dataSet.WriteXml(filename);
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

        public static bool compare1 = false;
        public static bool compare2 = false;

        //Application name
        public static string AppName = "Stock Application";

        //CompareTwo Table
        public static bool CompareTwoTable = false;

    }
}