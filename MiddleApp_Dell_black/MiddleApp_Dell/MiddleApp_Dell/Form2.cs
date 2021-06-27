using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Ink;

namespace MiddleApp_Dell
{
    public partial class Form2 : Form
    {
         public InkOverlay InkOverlay1 { get; set; }

        Point LeftDown;

        public Form2()
        {
            InitializeComponent();

            LeftDown = new Point();



            InkOverlay1 = new InkOverlay
            {
                Handle = pictureBox1.Handle,
                Enabled = true
            };


            //SetDrawingAttributes();
            //ToEditMode();

           // UpdatePictureBox();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            LeftDown.X = e.X;
            LeftDown.Y = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = MousePosition;
                p.Offset(-LeftDown.X, -LeftDown.Y);
                Location = p;
            }
        }



        private void ToEditMode()
        {
            InkOverlay1.EditingMode = InkOverlayEditingMode.Ink;
            // InkOverlay.Cursor = System.Windows.Forms.Cursors.Hand;
        }
        public void Form2_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CreateMyBorderlessWindow();
        }

        public void CreateMyBorderlessWindow()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            // Remove the control box so the form will only display client area.
            this.ControlBox = false;
        }

        void showOnMonitor(int showOnMonitor)
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            //get all the screen width and heights 

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Left = sc[0].Bounds.Width;
            this.Top = 0;
            this.StartPosition = FormStartPosition.Manual;
            this.Show();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void SetDrawingAttributes()
        {
            var attriburtes = new DrawingAttributes()
            {
                Color = Color.Black,
                FitToCurve = true,
                Height = 44,
                Width = 44,
                IgnorePressure = false,
                PenTip = PenTip.Ball,
            };
            InkOverlay1.DefaultDrawingAttributes = attriburtes;
            InkOverlay1.EraserMode = InkOverlayEraserMode.StrokeErase;

        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        public void clear()
        {
            InkOverlay1.Ink.DeleteStrokes(InkOverlay1.Ink.Strokes);
            pictureBox1.Refresh();
        }

        public void  showPage(int getPageNum) 
        {
            InkOverlay1.Enabled = false;
            InkOverlay1.Ink = new Ink();
            InkOverlay1.Enabled = true;
            using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image\\" + getPageNum + ".gif", FileMode.Open))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                
                InkOverlay1.Ink.Load(bytes);
                InkOverlay1.DefaultDrawingAttributes.Color = Color.Black;
            }
            pictureBox1.Refresh();
        }
   
        public void UpdatePictureBox(int getPageName)
        {

            InkOverlay1.Enabled = false;
            InkOverlay1.Ink = new Ink();
            InkOverlay1.Enabled = true;
            using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image\\" + getPageName + ".gif", FileMode.Open))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                InkOverlay1.Ink.Load(bytes);
            }
            
            pictureBox1.Refresh();

        }
    }
}
