﻿using System;
using System.Windows.Forms;
using System.Threading;

namespace Nagru___Manga_Organizer
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /* Checks for already running instance
               Author: K. Scott Allen (August 20, 2004) */
            using (Mutex mutex = new Mutex(false, @"Global\" + Application.ProductName))
            {
                if (!mutex.WaitOne(0, false))
                    return;

                GC.Collect();
                Application.Run(new Main(args));
            }
        }
    }
}
