﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Nagru___Manga_Organizer
{
    /* Implementation of custom serialization method
       Author: sdktsg, (Jan 9, 2008)                         */
    public static class FileSerializer
    {
        //serialize passed object
        public static void Serialize(string sFilepath, object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Object cannot be null");

            Stream stream = null;
            try
            {
                stream = File.Open(sFilepath, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, obj);
            }
            finally { if (stream != null) stream.Close(); }
        }

        //deserialize passed file
        public static T Deserialize<T>(string sFilepath)
        {
            T obj = default(T);
            Stream stream = null;

            try
            {
                stream = File.Open(sFilepath, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                obj = (T)bFormatter.Deserialize(stream);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show(
                    "The database was invalid.", "Manga Organizer", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally { if (stream != null) stream.Close(); }

            return obj;
        }
    }
}