﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Nagru___Manga_Organizer
{
    public partial class Browse : Form
    {
        public string sPath { get; set; }
        List<string> lFiles = new List<string>(25);
        int iWidth = Screen.PrimaryScreen.Bounds.Width / 2;
        Image imgR = null, imgL = null;
        bool bWideL, bWideR;
        int iPage = -1;

        /* Initialize form */
        public Browse()
        { InitializeComponent(); }

        /* Set form to fullscreen and grab files */
        private void Browse_Load(object sender, EventArgs e)
        {
            Cursor.Hide();

            //set fullscreen
            TopMost = true;
            Location = new Point(0, 0);
            Bounds = Screen.PrimaryScreen.Bounds;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            //set picBx positions
            picBx_Left.Location = new Point(0, 0);
            picBx_Right.Location = new Point(iWidth, 0);
            picBx_Right.Width = iWidth;

            //get files
            lFiles = ExtDirectory.GetFiles(sPath,
                SearchOption.TopDirectoryOnly);
            GoLeft();
        }

        /* Navigate files or close form */
        private void Browse_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Up:
                    GoRight();
                    break;
                case Keys.Left:
                case Keys.Down:
                    GoLeft();
                    break;
                case Keys.PrintScreen:
                case Keys.MediaNextTrack:
                case Keys.MediaPreviousTrack:
                case Keys.MediaPlayPause:
                case Keys.MediaStop:
                case Keys.VolumeUp:
                case Keys.VolumeDown:
                case Keys.VolumeMute:
                    break;
                default: Close();
                    break;
            }
        }

        /* Traverse images leftwards */
        void GoLeft()
        {
            Reset();

            if (++iPage >= lFiles.Count) iPage = 0;
            imgR = Image.FromStream(new FileStream(lFiles[iPage],
                FileMode.Open, FileAccess.Read));
            if (++iPage >= lFiles.Count) iPage = 0;
            imgL = Image.FromStream(new FileStream(lFiles[iPage],
                FileMode.Open, FileAccess.Read));

            bWideR = (imgR.Height < imgR.Width);
            bWideL = (imgL.Height < imgL.Width);
            picBx_Right.Image = imgR;

            if (!bWideL && !bWideR)
                picBx_Left.Image = imgL;
            else if (bWideL && bWideR || bWideR)
            {
                picBx_Left.Image = imgR;
                picBx_Left.Width =
                    Screen.PrimaryScreen.Bounds.Width;
                iPage--;
            }
            else iPage--;
        }

        /* Traverse images rightward */
        void GoRight()
        {
            if (iPage != 0 && picBx_Left.Width == iWidth) iPage--;
            Reset();

            if (--iPage < 0) iPage = lFiles.Count - 1;
            imgL = Image.FromStream(new FileStream(lFiles[iPage],
                FileMode.Open, FileAccess.Read));
            if (iPage - 1 < 0) iPage = lFiles.Count;
            imgR = Image.FromStream(new FileStream(lFiles[iPage - 1],
                FileMode.Open, FileAccess.Read));

            bWideR = (imgR.Height < imgR.Width);
            bWideL = (imgL.Height < imgL.Width);
            picBx_Left.Image = imgL;

            if (!bWideL && !bWideR)
                picBx_Right.Image = imgR;
            else if (bWideL)
                picBx_Left.Width =
                    Screen.PrimaryScreen.Bounds.Width;
            else iPage++;
        }

        /* Returns compared proportions of image */
        bool IsLandscape(string path)
        {
            using (Bitmap b = new Bitmap(path))
            { return b.Width > b.Height; }
        }

        /* Clear picBxs before populating them again */
        void Reset()
        {
            if (picBx_Left.Image != null)
                picBx_Left.Image.Dispose();
            if (picBx_Right.Image != null)
                picBx_Right.Image.Dispose();
            picBx_Left.Width = iWidth;
            picBx_Left.Image = null;
            picBx_Right.Image = null;
        }

        /* Alternative closing method */
        private void UserClick(object sender, MouseEventArgs e)
        { Close(); }

        /* Re-enable cursor when finished browsing */
        private void Browse_FormClosing(object sender, FormClosingEventArgs e)
        {
            TopMost = false;
            Cursor.Show();
        }
    }
}
