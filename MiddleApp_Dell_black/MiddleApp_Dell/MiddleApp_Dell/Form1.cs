using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MiddleApp_Dell
{
    public partial class Form1 : Form
    {
        private InkOverlay InkOverlay { get; set; }
       
        
        int pagenum;
        int i = 1;
        int createNum = 1;
        Form2 objform;



        public Form1()
        {
           
            InitializeComponent();

            showOnMonitor(1);

            objform = new Form2();
            // objform.Show();

            checkBox1.Checked = false;




            InkOverlay = new InkOverlay
            {
                Handle = pictureBox1.Handle,
                
                Enabled = true
            };

         

            SetDrawingAttributes();
            ToEditMode();
          

            using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image\\1.gif", FileMode.Open))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                InkOverlay.Ink.Load(bytes);
                
                
            }

            pictureBox1.Refresh();

            label2.Text = "Page : 1";

            pagenum = 1;


            //label3.Text = ""+path.Length;
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
                Color = Color.White,
                FitToCurve = true,
                Height = 25,
                Width = 25,
                IgnorePressure = false,
                PenTip = PenTip.Ball,
            };
            InkOverlay.DefaultDrawingAttributes = attriburtes;
            InkOverlay.EraserMode = InkOverlayEraserMode.StrokeErase;
         
        }


        private void ToEditMode()
        {
            InkOverlay.EditingMode = InkOverlayEditingMode.Ink;
           // InkOverlay.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void ToEraseMode()
        {
            InkOverlay.EditingMode = InkOverlayEditingMode.Delete;
            //InkOverlay.Cursor = System.Windows.Forms.Cursors.Cross;
        }

        private void createNewPage(int createNum)
        {
             string[] path = System.IO.Directory.GetFiles(@"C:\\MiddleApp_Dell\\image");
             int newPage;
             newPage = createNum;
             pagenum = createNum;
             label2.Text = "Page : " + pagenum;
             System.IO.File.Copy(@"C:\\MiddleApp_Dell\\image_back\\1.gif", @"C:\\MiddleApp_Dell\\image\\" + newPage + ".gif", true);
             System.IO.File.Copy(@"C:\\MiddleApp_Dell\\image_back\\1.gif", @"C:\\MiddleApp_Dell\\image_back\\" + newPage + ".gif", true);
             InkOverlay.Enabled = false;
             InkOverlay.Ink = new Ink();
             InkOverlay.Enabled = true;
             using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image\\" + newPage + ".gif", FileMode.Open))
              {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    InkOverlay.Ink.Load(bytes);
              }
              pictureBox1.Refresh();

              objform.showPage(pagenum);
              i = 0;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InkOverlay.Ink.DeleteStrokes(InkOverlay.Ink.Strokes);
            pictureBox1.Refresh();
            objform.clear();

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {


           /* FileStream gifFile;
            byte[] fortifiedGif = null;
            gifFile = File.OpenWrite(@"C:\\MiddleApp_Dell\\image\\" + pagenum + ".png");
            fortifiedGif = InkOverlay.Ink.Save(PersistenceFormat.Gif);
            gifFile.Write(fortifiedGif, 0, fortifiedGif.Length);
            gifFile.Close();*/

            using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image\\" + pagenum + ".gif", FileMode.Create))
            {
                var bytes = InkOverlay.Ink.Save(PersistenceFormat.Gif);
                stream.Write(bytes, 0, bytes.Length);
            }
            objform.UpdatePictureBox(pagenum);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            string[] path = System.IO.Directory.GetFiles(@"C:\\MiddleApp_Dell\\image");

            i++;
            if (i > path.Length - 1)
            {
                i = 1;
            }
            pagenum = i;

            InkOverlay.Enabled = false;
            InkOverlay.Ink = new Ink();
            InkOverlay.Enabled = true;
            using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image\\" + pagenum + ".gif", FileMode.Open))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                InkOverlay.Ink.Load(bytes);
            }
            pictureBox1.Refresh();

            objform.showPage(pagenum);

            label2.Text = "Page : " + i;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] path = System.IO.Directory.GetFiles(@"C:\\MiddleApp_Dell\\image");

            i--;
            if (i < 1)
            {
                i = path.Length - 1;
            }
            pagenum = i;

            InkOverlay.Enabled = false;
            InkOverlay.Ink = new Ink();
            InkOverlay.Enabled = true;
            using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image\\" + pagenum + ".gif", FileMode.Open))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                InkOverlay.Ink.Load(bytes);
            }
            pictureBox1.Refresh();

            objform.showPage(pagenum);

            label2.Text = "Page : " + i;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createNum++;
            createNewPage(createNum);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void setUI ()
        {
            button5.FlatStyle = FlatStyle.Flat;//樣式
            button5.ForeColor = Color.Transparent;//前景
            button5.BackColor = Color.Transparent;//去背景
            button5.FlatAppearance.BorderSize = 0;//去邊線
            button5.FlatAppearance.MouseOverBackColor = Color.Transparent;//鼠標經過
            button5.FlatAppearance.MouseDownBackColor = Color.Transparent;//鼠標按下
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
             this.TopMost = true;
             this.FormBorderStyle = FormBorderStyle.None;
             this.WindowState = FormWindowState.Maximized;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            directoryCopy(@"C:\\MiddleApp_Dell\\image_back", @"C:\\MiddleApp_Dell\\image");

            Application.ExitThread();
            Restart();

        }


        public void directoryCopy(string sourceDirectory, string targetDirectory)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirectory);
               
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)    
                    {
                        if (!Directory.Exists(targetDirectory + "\\" + i.Name))
                        {
                           
                            Directory.CreateDirectory(targetDirectory + "\\" + i.Name);
                        }
                       
                        directoryCopy(i.FullName, targetDirectory + "\\" + i.Name);
                    }
                    else
                    {
                       
                        File.Copy(i.FullName, targetDirectory + "\\" + i.Name, true);
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private void Restart()
        {
            System.Threading.Thread thtmp = new System.Threading.Thread(new
            System.Threading.ParameterizedThreadStart(run));
            object appName = Application.ExecutablePath;
            System.Threading.Thread.Sleep(1000);
            thtmp.Start(appName);
        }

        private void run(Object obj)
        {
            System.Diagnostics.Process ps = new System.Diagnostics.Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                objform.Visible = true;
            }
            else
            {
                objform.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            createNum++;
            if (createNum <= 20)
            {
                createNewPage(createNum);
            }
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string[] path = System.IO.Directory.GetFiles(@"C:\\MiddleApp_Dell\\image");

            i++;
            if (i > path.Length - 1)
            {
                i = 1;
            }
            pagenum = i;

            InkOverlay.Enabled = false;
            InkOverlay.Ink = new Ink();
            InkOverlay.Enabled = true;
            using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image\\" + pagenum + ".gif", FileMode.Open))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                InkOverlay.Ink.Load(bytes);
            }
            pictureBox1.Refresh();

            objform.showPage(pagenum);

            label2.Text = "Page : " + i;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string[] path = System.IO.Directory.GetFiles(@"C:\\MiddleApp_Dell\\image");

            i--;
            if (i < 1)
            {
                i = path.Length - 1;
            }
            pagenum = i;

            InkOverlay.Enabled = false;
            InkOverlay.Ink = new Ink();
            InkOverlay.Enabled = true;
            using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image\\" + pagenum + ".gif", FileMode.Open))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                InkOverlay.Ink.Load(bytes);
            }
            pictureBox1.Refresh();

            objform.showPage(pagenum);

            label2.Text = "Page : " + i;
        }

        private void button10_Click(object sender, EventArgs e)
        {
           
            InkOverlay.Enabled = false;
            InkOverlay.Ink = new Ink();
            InkOverlay.Enabled = true;
            using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image_back\\1.gif", FileMode.Open))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                InkOverlay.Ink.Load(bytes);
            }
            pictureBox1.Refresh();

            System.IO.File.Copy(@"C:\\MiddleApp_Dell\\image_back\\1.gif", @"C:\\MiddleApp_Dell\\image\\" + pagenum + ".gif", true);
           
            objform.clear();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            InkOverlay.Enabled = false;
            InkOverlay.Ink = new Ink();
            InkOverlay.Enabled = true;
            using (var stream = new FileStream(@"C:\\MiddleApp_Dell\\image_back\\1.gif", FileMode.Open))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                InkOverlay.Ink.Load(bytes);
            }
            pictureBox1.Refresh();

            System.IO.File.Copy(@"C:\\MiddleApp_Dell\\image_back\\1.gif", @"C:\\MiddleApp_Dell\\image\\" + pagenum + ".gif", true);

            objform.clear();

        }
    }


}
