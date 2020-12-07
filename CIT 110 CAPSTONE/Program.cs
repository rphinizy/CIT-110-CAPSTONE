using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace CIT_110_CAPSTONE
{
    static class Program
    {
        // **************************************************
        //
        // Title: CIT110 CAPSTONE - Maze Crawler
        // Description: Winform maze game where user controls an icon to
        // get to the end of the maze.
        // Application Type: Windows Forms
        // Author: Phinizy, Robin
        // Dated Created: 11/15/2020
        // Last Modified: 12/6/2020
        //
        // **************************************************
        [STAThread]
        static void Main()
        {
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
