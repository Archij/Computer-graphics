using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace uzdevums1
{
    public partial class Form1 : Form
    {
        int x1, y1, x2, y2, x3, y3, x4, y4, xc, yc, radius, enx, eny; //Start and end coordinates, center coordinates, radius
        int check = 0;
        int rasterCheck = 1;
        Graphics graphicsObj;
        List<String> colors = new List<String> {"Red", "Green", "Blue", "Black", "Violet", "Cyan", 
        "Gold", "Gray", "Indigo", "Magenta", "Orange", "Yellow"};
        List<Int32> izmeri = new List<Int32> {1,2,3,4,5,6,7,8,9,10};
        Pen zimulis = new Pen(Color.Red);
        int indeks = 0;
        int izmers = 1;
        Label label7, label8, label9, label10, label11;
        TextBox textBox5, textBox6, textBox7, textBox8, textBox9;
        
        

        public Form1()
        {
            InitializeComponent();
            BindingSource b = new BindingSource();
            b.DataSource = colors;
            comboBox1.DataSource = b.DataSource;
            BindingSource b1 = new BindingSource();
            b1.DataSource = izmeri;
            comboBox2.DataSource = b1.DataSource;

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs ev)
        {

                if (rasterCheck == 1) //Drawing a line segment using the Brezenham algorithm
                 {
                    if (check == 0)
                    {
                        x1 = ev.X;
                        y1 = ev.Y;
                        check = 1;
                    }
                    else if (check == 1)
                    {
                        x2 = ev.X;
                        y2 = ev.Y;
                        check = 0;
                    textBox1.Text = x1.ToString();
                    textBox2.Text = y1.ToString();
                    textBox3.Text = x2.ToString();
                    textBox4.Text = y2.ToString();
                    check = 0;

                    graphicsObj = pictureBox1.CreateGraphics();


                    int x, dx;
                    int y, dy;
                    int d;
                    int xerr = 0, yerr = 0;
                    int incX = 0, incY = 0;

                    //Coordinate difference
                    dx = x2 - x1;
                    dy = y2 - y1;

                    if (dx > 0)
                    {
                        incX = 1;
                    }
                    else if (dx == 0)
                    {
                        incX = 0;
                    }
                    else if (dx < 0)
                    {
                        incX = -1;
                    }

                    if (dy > 0)
                    {
                        incY = 1;
                    }
                    else if (dy == 0)
                    {
                        incY = 0;
                    }
                    else
                    {
                        incY = -1;
                    }

                    dx = Math.Abs(dx);
                    dy = Math.Abs(dy);

                    if (dx > dy)
                    {
                        d = dx;
                    }
                    else
                    {
                        d = dy;
                    }

                    x = x1;
                    y = y1;

                    for (int i = 0; i <= d; i++)
                    {
                        //Detect errors
                        xerr = xerr + dx;
                        yerr = yerr + dy;
                        if (xerr > d)
                        {
                            xerr = xerr - d;
                            x = x + incX;
                        }
                        if (yerr > d)
                        {
                            yerr = yerr - d;
                            y = y + incY;
                        }

                        graphicsObj.DrawRectangle(zimulis, x, y, izmers, izmers);

                    }
                    }

                }

                else if (rasterCheck == 2) //Drawing a circle using the Brezenham algorithm
                {
                    if (check == 0)
                    {
                        x1 = ev.X;
                        y1 = ev.Y;
                        check = 1;
                    }
                    else if (check == 1)
                    {
                        x2 = ev.X;
                        y2 = ev.Y;
                        check = 0;
                    radius = (int)Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                    xc = x1;
                    yc = y1;
                    textBox1.Text = xc.ToString();
                    textBox2.Text = yc.ToString();
                    textBox3.Text = radius.ToString();
                    check = 0;

                    int x, y, dxt;
                    long r2, dst, t, s, e, ca, cd, indx;
                    r2 = (long)radius * (long)radius;
                    dst = 4 * r2;
                    dxt = (int)((double)radius / Math.Sqrt(2));
                    t = 0;
                    s = -4 * r2 * (long)radius;
                    e = (-s / 2) - 3 * r2;
                    ca = -6 * r2;
                    cd = -10 * r2;
                    x = 0;
                    y = radius;

                    graphicsObj = pictureBox1.CreateGraphics();
                    graphicsObj.DrawRectangle(zimulis, xc, yc + radius, izmers, izmers);
                    graphicsObj.DrawRectangle(zimulis, xc, yc - radius, izmers, izmers);
                    graphicsObj.DrawRectangle(zimulis, xc + radius, yc, izmers, izmers);
                    graphicsObj.DrawRectangle(zimulis, xc - radius, yc, izmers, izmers);

                    for (indx = 1; indx <= dxt; indx++)
                    {
                        x++;
                        if (e >= 0)
                        {
                            e = e + t + ca;
                        }
                        else
                        {
                            y--;
                            e = e + t - s + cd;
                            s = s + dst;
                        }
                        t = t - dst;

                        graphicsObj.DrawRectangle(zimulis, xc + x, yc + y, izmers, izmers);
                        graphicsObj.DrawRectangle(zimulis, xc + y, yc + x, izmers, izmers);
                        graphicsObj.DrawRectangle(zimulis, xc + y, yc - x, izmers, izmers);
                        graphicsObj.DrawRectangle(zimulis, xc + x, yc - y, izmers, izmers);
                        graphicsObj.DrawRectangle(zimulis, xc - x, yc - y, izmers, izmers);
                        graphicsObj.DrawRectangle(zimulis, xc - y, yc - x, izmers, izmers);
                        graphicsObj.DrawRectangle(zimulis, xc - y, yc + x, izmers, izmers);
                        graphicsObj.DrawRectangle(zimulis, xc - x, yc + y, izmers, izmers);

                    }

                    }

                }

                else if (rasterCheck == 3) //Drawing an ellipse using the Brezenham algorithm
                {
                    if (check == 0)
                    {
                        xc = ev.X;
                        yc = ev.Y;
                        check = 1;
                    }
                    else if (check == 1)
                    {
                        enx = ev.X;
                        check = 2;
                    }
                    else if (check == 2)
                    {
                        eny = ev.Y;
                        check = 0;
                        textBox1.Text = xc.ToString();
                        textBox2.Text = yc.ToString();
                        textBox3.Text = enx.ToString();
                        textBox4.Text = eny.ToString();
                        check = 0;

                        int x, y, a, b;
                        long a2, b2, dds, ddt, t, s, e, ca, cd, indx;
                        float dxt;

                        a = Math.Abs(enx - xc);
                        b = Math.Abs(eny - yc);
                        a2 = (long)a * (long)a;
                        b2 = (long)b * (long)b;
                        dds = 4 * a2;
                        ddt = 4 * b2;
                        dxt = (float)(a2 / Math.Sqrt(a2 + b2));
                        t = 0;
                        s = -4 * a2 * b;
                        e = (-s / 2) - 2 * b2 - a2;
                        ca = -6 * b2;
                        cd = ca - 4 * a2;
                        x = xc;
                        y = yc + b;

                        graphicsObj = pictureBox1.CreateGraphics();
                        graphicsObj.DrawRectangle(zimulis, x, y, izmers, izmers);
                        graphicsObj.DrawRectangle(zimulis, x, 2 * yc - y, izmers, izmers);
                        graphicsObj.DrawRectangle(zimulis, 2 * xc - x, 2 * yc - y, izmers, izmers);
                        graphicsObj.DrawRectangle(zimulis, 2 * xc - x, y, izmers, izmers);

                        for (indx = 1; indx <= dxt; indx++)
                        {
                            x++;
                            if (e >= 0)
                            {
                                e = e + t + ca;
                            }
                            else
                            {
                                y--;
                                e = e + t - s + cd;
                                s = s + dds;
                            }
                            t = t - ddt;

                            graphicsObj.DrawRectangle(zimulis, x, y, izmers, izmers);
                            graphicsObj.DrawRectangle(zimulis, x, 2 * yc - y, izmers, izmers);
                            graphicsObj.DrawRectangle(zimulis, 2 * xc - x, 2 * yc - y, izmers, izmers);
                            graphicsObj.DrawRectangle(zimulis, 2 * xc - x, y, izmers, izmers);
                        }

                        dxt = Math.Abs(y - yc);
                        e = e - t / 2 + s / 2 + b2 + a2;
                        ca = -6 * a2;
                        cd = ca - 4 * b2;
                        for (indx = 1; indx <= dxt; indx++)
                        {
                            y--;
                            if (e <= 0)
                            {
                                e = e - s + ca;
                            }
                            else
                            {
                                x++;
                                e = e - s + t + cd;
                                t = t - ddt;
                            }
                            s = s + dds;


                            graphicsObj.DrawRectangle(zimulis, x, y, izmers, izmers);
                            graphicsObj.DrawRectangle(zimulis, x, 2 * yc - y, izmers, izmers);
                            graphicsObj.DrawRectangle(zimulis, 2 * xc - x, 2 * yc - y, izmers, izmers);
                            graphicsObj.DrawRectangle(zimulis, 2 * xc - x, y, izmers, izmers);

                        }

                    }

                }

                else //Drawing of the Bezier curve
                {
                    if (check == 0)
                    {
                        x1 = ev.X;
                        y1 = ev.Y;
                        check = 1;
                    }
                    else if (check == 1)
                    {
                        x2 = ev.X;
                        y2 = ev.Y;
                        check = 2;
                    }
                    else if (check == 2)
                    {
                        x3 = ev.X;
                        y3 = ev.Y;
                        check = 3;
                    }
                    else if (check == 3)
                    {
                        x4 = ev.X;
                        y4 = ev.Y;
                        check = 0;

                    if (x1 >= 0 && y1 >= 0 && x2 >= 0 && y2 >= 0 && x3 >= 0 && y3 >= 0 && x4 >= 0 && y4 >= 0)
                    {
                        textBox1.Text = x1.ToString();
                        textBox2.Text = y1.ToString();
                        textBox3.Text = x2.ToString();
                        textBox4.Text = y2.ToString();
                        textBox5.Text = x3.ToString();
                        textBox6.Text = y3.ToString();
                        textBox7.Text = x4.ToString();
                        textBox8.Text = y4.ToString();
                        double solis;
                        int cx, cy, bx, by, ax, ay, x, y;
                        try
                        {
                            solis = Convert.ToDouble(textBox9.Text);

                            if (solis > 0 && solis <= 1)
                            {
                                graphicsObj = pictureBox1.CreateGraphics();

                                if (indeks == 0)
                                {
                                    graphicsObj.FillEllipse(Brushes.Black, x1, y1, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x2, y2, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x3, y3, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x4, y4, 10, 10);

                                }
                                else
                                {
                                    graphicsObj.FillEllipse(Brushes.Red, x1, y1, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x2, y2, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x3, y3, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x4, y4, 10, 10);
                                }

                                for (double t = 0; t <= 1; t = t + solis)
                                {
                                    cx = 3 * (x2 - x1);
                                    cy = 3 * (y2 - y1);

                                    bx = 3 * (x3 - x2) - cx;
                                    by = 3 * (y3 - y2) - cy;

                                    ax = x4 - x1 - cx - bx;
                                    ay = y4 - y1 - cy - by;

                                    x = (int)((ax * Math.Pow(t, 3)) + (bx * Math.Pow(t, 2)) + (cx * t) + x1); //obtains the Bezier curve x coordinate
                                    y = (int)((ay * Math.Pow(t, 3)) + (by * Math.Pow(t, 2)) + (cy * t) + y1); //obtains the Bezier curve y coordinate

                                    graphicsObj.DrawRectangle(zimulis, x, y, izmers, izmers);
                                }

                                if (indeks == 0)
                                {
                                    graphicsObj.FillEllipse(Brushes.Black, x2, y2, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x2, y2, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x3, y3, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x4, y4, 10, 10);

                                }
                                else
                                {
                                    graphicsObj.FillEllipse(Brushes.Red, x1, y1, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x2, y2, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x3, y3, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x4, y4, 10, 10);
                                }

                            }
                            else
                            {
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                DialogResult result;

                                result = MessageBox.Show("Please enter a time step in the interval 0<t<=1!", "Wrong time step!", buttons);

                                if (result == System.Windows.Forms.DialogResult.Yes)
                                {

                                    this.Close();

                                }
                            }
                        }
                        catch (FormatException)
                        {
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                DialogResult result;

                                result = MessageBox.Show("Please enter a time step in the interval 0<t<=1!", "No time step entered!", buttons);

                                if (result == System.Windows.Forms.DialogResult.Yes)
                                {

                                    this.Close();

                                }
                        }
                      }

                   }

                }
            }

        private void button1_Click(object sender, EventArgs e) //if you choose to draw a line segment
        {
            rasterCheck = 1;

            for (int ix = this.Controls.Count - 1; ix >= 0; ix--)
            {

                if (Controls[ix].Name == "label7" || Controls[ix].Name == "label8" || Controls[ix].Name == "label9" ||
                    Controls[ix].Name == "label10" || Controls[ix].Name == "label11" || Controls[ix].Name == "textBox5" ||
                    Controls[ix].Name == "textBox6" || Controls[ix].Name == "textBox7" || Controls[ix].Name == "textBox8" ||
                    Controls[ix].Name == "textBox9")
                {
                    Controls[ix].Dispose(); //Discard unnecessary components from the form
                }

            }

            label1.Text = "x1";
            label1.Location = new Point(184, 28);
            label2.Text = "y1";
            label2.Location = new Point(305, 29);
            label3.Text = "x2";
            label3.Location = new Point(427, 28);
            label4.Text = "y2";
            label4.Location = new Point(546, 29);
            label4.Show();
            textBox1.Text = "";
            textBox1.Location = new Point(208, 25);
            textBox2.Text = "";
            textBox2.Location = new Point(329, 25);
            textBox3.Text = "";
            textBox3.Location = new Point(451, 25);
            textBox4.Text = "";
            textBox4.Location = new Point(570, 25);
            textBox4.Show();
            button5.Location = new Point(653, 22);
            button6.Location = new Point(797, 21);
        }

        private void button2_Click(object sender, EventArgs e) //if you choose to draw a circle
        {
            rasterCheck = 2;

            for (int ix = this.Controls.Count - 1; ix >= 0; ix--)
            {

                if (Controls[ix].Name == "label7" || Controls[ix].Name == "label8" || Controls[ix].Name == "label9" ||
                    Controls[ix].Name == "label10" || Controls[ix].Name == "label11" || Controls[ix].Name == "textBox5" ||
                    Controls[ix].Name == "textBox6" || Controls[ix].Name == "textBox7" || Controls[ix].Name == "textBox8" ||
                    Controls[ix].Name == "textBox9")
                {
                    Controls[ix].Dispose(); //Discard unnecessary components from the form
                }

            }

            label1.Text = "xc";
            label1.Location = new Point(184, 28);
            label2.Text = "yc";
            label2.Location = new Point(305, 29);
            label3.Text = "radius";
            label3.Location = new Point(427, 28);
            label4.Hide();
            textBox1.Text = "";
            textBox1.Location = new Point(208, 25);
            textBox2.Text = "";
            textBox2.Location = new Point(329, 25);
            textBox3.Text = "";
            textBox3.Location = new Point(471, 25);
            textBox4.Hide();
            button5.Location = new Point(653, 22);
            button6.Location = new Point(797, 21);
            
           
        }

        private void button3_Click(object sender, EventArgs e) //if you choose to draw an ellipse
        {
            rasterCheck = 3;

            for (int ix = this.Controls.Count - 1; ix >= 0; ix--)
            {

                if (Controls[ix].Name == "label7" || Controls[ix].Name == "label8" || Controls[ix].Name == "label9" ||
                    Controls[ix].Name == "label10" || Controls[ix].Name == "label11" || Controls[ix].Name == "textBox5" ||
                    Controls[ix].Name == "textBox6" || Controls[ix].Name == "textBox7" || Controls[ix].Name == "textBox8" ||
                    Controls[ix].Name == "textBox9")
                    {
                          Controls[ix].Dispose(); //Discard unnecessary components from the form
                    }

            }


            label1.Text = "xc";
            label1.Location = new Point(184, 28);
            label2.Text = "yc";
            label2.Location = new Point(305, 29);
            label3.Text = "enx";
            label3.Location = new Point(427, 28);
            label4.Text = "eny";
            label4.Location = new Point(546, 29);
            label4.Show();
            textBox1.Text = "";
            textBox1.Location = new Point(208, 25);
            textBox2.Text = "";
            textBox2.Location = new Point(329, 25);
            textBox3.Text = "";
            textBox3.Location = new Point(451, 25);
            textBox4.Text = "";
            textBox4.Location = new Point(570, 25);
            textBox4.Show();
            button5.Location = new Point(653, 22);
            button6.Location = new Point(797, 21);
            
        }

        private void button4_Click(object sender, EventArgs e) //if you choose to draw a Bezier curve
        {
            rasterCheck = 4;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            label1.Text = "x1";
            label1.Location = new Point(181, 9);
            label2.Text = "y1";
            label2.Location = new Point(181, 35);
            label3.Text = "x2";
            label3.Location = new Point(286, 9);
            label4.Text = "y2";
            label4.Location = new Point(286, 35);
            label4.Show();
            label7 = new Label();
            label7.Location = new Point(399, 9);
            label7.Text = "x3";
            label7.Show();
            label7.Size = new Size(18, 13);
            label7.Name = "label7";
            Controls.Add(label7);
            label8 = new Label();
            label8.Location = new Point(399, 33);
            label8.Text = "y3";
            label8.Show();
            label8.Size = new Size(18, 13);
            label8.Name = "label8";
            Controls.Add(label8);
            label9 = new Label();
            label9.Location = new Point(511, 10);
            label9.Text = "x4";
            label9.Show();
            label9.Size = new Size(18, 20);
            label9.Name = "label9";
            Controls.Add(label9);
            label10 = new Label();
            label10.Location = new Point(511, 34);
            label10.Text = "y4";
            label10.Show();
            label10.Size = new Size(18, 13);
            label10.Name = "label10";
            Controls.Add(label10);
            label11 = new Label();
            label11.Location = new Point(620, 6);
            label11.Text = "Time step";
            label11.Show();
            label11.Size = new Size(100, 15);
            label11.Name = "label11";
            Controls.Add(label11);
            textBox1.Text = "";
            textBox1.Location = new Point(201, 6);
            textBox2.Text = "";
            textBox2.Location = new Point(201, 30);
            textBox3.Text = "";
            textBox3.Location = new Point(310, 6);
            textBox4.Text = "";
            textBox4.Location = new Point(310, 30);
            textBox4.Show();
            textBox5 = new TextBox();
            textBox5.Location = new Point(423,6);
            textBox5.Show();
            textBox5.Size = new Size(60, 20);
            textBox5.Name = "textBox5";
            Controls.Add(textBox5);
            textBox6 = new TextBox();
            textBox6.Location = new Point(423, 30);
            textBox6.Show();
            textBox6.Size = new Size(60, 20);
            textBox6.Name = "textBox6";
            Controls.Add(textBox6);
            textBox7 = new TextBox();
            textBox7.Location = new Point(535, 6);
            textBox7.Show();
            textBox7.Size = new Size(60, 20);
            textBox7.Name = "textBox7";
            Controls.Add(textBox7);
            textBox8 = new TextBox();
            textBox8.Location = new Point(535, 30);
            textBox8.Show();
            textBox8.Size = new Size(60, 20);
            textBox8.Name = "textBox8";
            Controls.Add(textBox8);
            textBox9 = new TextBox();
            textBox9.Location = new Point(618, 30);
            textBox9.Size = new Size(60, 20);
            textBox9.Show();
            textBox9.Name = "textBox9";
            textBox9.Text = "0.0001";
            Controls.Add(textBox9);
            button5.Location = new Point(682,25);
            button6.Location = new Point(797, 24);
        }

        private void button5_Click(object sender, EventArgs ev) //if you press the "Draw" button
        {
            try
            {

                if (rasterCheck == 1) //if a line segment is selected
                {
                    x1 = Convert.ToInt32(textBox1.Text);
                    y1 = Convert.ToInt32(textBox2.Text);
                    x2 = Convert.ToInt32(textBox3.Text);
                    y2 = Convert.ToInt32(textBox4.Text);

                         if (x1 >= 0 && y1 >= 0 && x2 >= 0 && y2 >= 0)
                         {

                            graphicsObj = pictureBox1.CreateGraphics();


                            int x, dx;
                            int y, dy;
                            int d;
                            int xerr = 0, yerr = 0;
                            int incX = 0, incY = 0;

                            //Coordinate difference
                            dx = x2 - x1;
                            dy = y2 - y1;

                            if (dx > 0)
                             {
                                incX = 1;
                            }
                            else if (dx == 0)
                            {
                                 incX = 0;
                            }
                            else if (dx < 0)
                            {
                                 incX = -1;
                             }

                            if (dy > 0)
                            {
                                incY = 1;
                            }
                            else if (dy == 0)
                            {
                                 incY = 0;
                             }
                            else
                            {
                                incY = -1;
                            }

                             dx = Math.Abs(dx);
                             dy = Math.Abs(dy);

                            if (dx > dy)
                            {
                                d = dx;
                            }
                            else
                            {
                                d = dy;
                            }

                            x = x1;
                            y = y1;

                            for (int i = 0; i <= d; i++)
                            {
                                //Detect errors
                                xerr = xerr + dx;
                                yerr = yerr + dy;
                                if (xerr > d)
                                {
                                 xerr = xerr - d;
                                 x = x + incX;
                                }
                                if (yerr > d)
                                {
                                    yerr = yerr - d;
                                    y = y + incY;
                                }

                                graphicsObj.DrawRectangle(zimulis, x, y, izmers, izmers);

                            }

                        }

                        else
                        {
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                            DialogResult result;

                            result = MessageBox.Show("Please enter non-negative coordinates!", "At least one of the coordinates is negative!", buttons);

                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {

                                this.Close();

                            }
                        }
                    }

                    else if (rasterCheck == 2) //if a circle is selected
                    {
                        
                        xc = Convert.ToInt32(textBox1.Text);
                        yc = Convert.ToInt32(textBox2.Text);
                        radius = Convert.ToInt32(textBox3.Text);
                        if (xc >= 0 && yc >= 0 && radius >= 0) //if all entered parameters for the circle are non-negative
                        {

                            int x, y, dxt;
                            long r2, dst, t, s, e, ca, cd, indx;
                            r2 = (long)radius * (long)radius;
                            dst = 4 * r2;
                            dxt = (int)((double)radius / Math.Sqrt(2));
                            t = 0;
                            s = -4 * r2 * (long)radius;
                            e = (-s / 2) - 3 * r2;
                            ca = -6 * r2;
                            cd = -10 * r2;
                            x = 0;
                            y = radius;

                           graphicsObj = pictureBox1.CreateGraphics();
                           graphicsObj.DrawRectangle(zimulis, xc, yc + radius, izmers, izmers);
                           graphicsObj.DrawRectangle(zimulis, xc, yc - radius, izmers, izmers);
                           graphicsObj.DrawRectangle(zimulis, xc + radius, yc, izmers, izmers);
                           graphicsObj.DrawRectangle(zimulis, xc - radius, yc, izmers, izmers);

                            for (indx = 1; indx <= dxt; indx++)
                            {
                                x++;
                                if (e >= 0)
                                {
                                    e = e + t + ca;
                                }
                                else
                                {
                                    y--;
                                    e = e + t - s + cd;
                                    s = s + dst;
                                }
                                t = t - dst;

                                graphicsObj.DrawRectangle(zimulis, xc + x, yc + y, izmers, izmers);
                                graphicsObj.DrawRectangle(zimulis, xc + y, yc + x, izmers, izmers);
                                graphicsObj.DrawRectangle(zimulis, xc + y, yc - x, izmers, izmers);
                                graphicsObj.DrawRectangle(zimulis, xc + x, yc - y, izmers, izmers);
                                graphicsObj.DrawRectangle(zimulis, xc - x, yc - y, izmers, izmers);
                                graphicsObj.DrawRectangle(zimulis, xc - y, yc - x, izmers, izmers);
                                graphicsObj.DrawRectangle(zimulis, xc - y, yc + x, izmers, izmers);
                                graphicsObj.DrawRectangle(zimulis, xc - x, yc + y, izmers, izmers);
                                
                            }

                        }

                        else
                        {
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                            DialogResult result;

                            result = MessageBox.Show("Please enter non-negative coordinates!", "At least one of the coordinates is negative!", buttons);

                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {

                                 this.Close();

                            }
                         }

                    }

                    else if (rasterCheck == 3) //if an ellipse is selected
                    {

                        xc = Convert.ToInt32(textBox1.Text);
                        yc = Convert.ToInt32(textBox2.Text);
                        enx = Convert.ToInt32(textBox3.Text);
                        eny = Convert.ToInt32(textBox4.Text);


                        if (xc >= 0 && yc >= 0 && enx >= 0 && eny >=0)
                        {
                            int x, y, a, b;
                            long a2, b2, dds, ddt, t, s, e, ca, cd, indx;
                            float dxt;

                            a = Math.Abs(enx - xc);
                            b = Math.Abs(eny - yc);
                            a2 = (long)a * (long)a;
                            b2 = (long)b * (long)b;
                            dds = 4 * a2;
                            ddt = 4 * b2;
                            dxt = (float)(a2 / Math.Sqrt(a2 + b2));
                            t = 0;
                            s = -4 * a2 * b;
                            e = (-s / 2) - 2 * b2 - a2;
                            ca = -6 * b2;
                            cd = ca - 4 * a2;
                            x = xc;
                            y = yc + b;

                            graphicsObj = pictureBox1.CreateGraphics();
                            graphicsObj.DrawRectangle(zimulis, x, y, izmers, izmers);
                            graphicsObj.DrawRectangle(zimulis, x, 2*yc - y, izmers, izmers);
                            graphicsObj.DrawRectangle(zimulis, 2*xc - x, 2*yc - y, izmers, izmers);
                            graphicsObj.DrawRectangle(zimulis, 2*xc - x, y, izmers, izmers);

                            for (indx = 1; indx <= dxt; indx++)
                            {
                                x++;
                                if (e >= 0)
                                {
                                    e = e + t + ca;
                                }
                                else
                                {
                                    y--;
                                    e = e + t - s + cd;
                                    s = s + dds;
                                }
                                t = t - ddt;

                                graphicsObj.DrawRectangle(zimulis, x, y, izmers, izmers);
                                graphicsObj.DrawRectangle(zimulis, x, 2 * yc - y, izmers, izmers);
                                graphicsObj.DrawRectangle(zimulis, 2 * xc - x, 2 * yc - y, izmers, izmers);
                                graphicsObj.DrawRectangle(zimulis, 2 * xc - x, y, izmers, izmers);
                            }

                                dxt = Math.Abs(y - yc);
                                e = e - t / 2 + s / 2 + b2 + a2;
                                ca = -6 * a2;
                                cd = ca - 4 * b2;
                                for (indx = 1; indx <= dxt; indx++)
                                {
                                    y--;
                                    if (e <= 0)
                                    {
                                        e = e - s + ca;
                                    }
                                    else
                                    {
                                        x++;
                                        e = e - s + t + cd;
                                        t = t - ddt;
                                    }
                                    s = s + dds;


                                    graphicsObj.DrawRectangle(zimulis, x, y, izmers, izmers);
                                    graphicsObj.DrawRectangle(zimulis, x, 2 * yc - y, izmers, izmers);
                                    graphicsObj.DrawRectangle(zimulis, 2 * xc - x, 2 * yc - y, izmers, izmers);
                                    graphicsObj.DrawRectangle(zimulis, 2 * xc - x, y, izmers, izmers);
                                  
                                }
 
                            }
           

                        else
                        {
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                            DialogResult result;

                            result = MessageBox.Show("Please enter non-negative coordinates!", "At least one of the coordinates is negative!", buttons);

                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {

                                 this.Close();

                            }
                         }
                    }

                    else //if the Bezier curve is selected
                    {

                        x1 = Convert.ToInt32(textBox1.Text);
                        y1 = Convert.ToInt32(textBox2.Text);
                        x2 = Convert.ToInt32(textBox3.Text);
                        y2 = Convert.ToInt32(textBox4.Text);
                        x3 = Convert.ToInt32(textBox5.Text);
                        y3 = Convert.ToInt32(textBox6.Text);
                        x4 = Convert.ToInt32(textBox7.Text);
                        y4 = Convert.ToInt32(textBox8.Text);

                        if (x1 >= 0 && y1 >= 0 && x2 >=0 && y2>=0 && x3>=0 && y3>=0 && x4>=0 && y4>=0)
                        {
                            double solis;
                            int cx, cy, bx, by, ax, ay, x, y;
                            solis = Convert.ToDouble(textBox9.Text);

                            if (solis > 0 && solis <= 1)
                            {
                                graphicsObj = pictureBox1.CreateGraphics();
                                if (indeks == 0)
                                {
                                    graphicsObj.FillEllipse(Brushes.Black, x1, y1, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x2, y2, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x3, y3, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x4, y4, 10, 10);

                                }
                                else
                                {
                                    graphicsObj.FillEllipse(Brushes.Red, x1, y1, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x2, y2, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x3, y3, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x4, y4, 10, 10);
                                }

                                for (double t = 0; t <= 1; t = t + solis)
                                {
                                    cx = 3 * (x2 - x1);
                                    cy = 3 * (y2 - y1);

                                    bx = 3 * (x3 - x2) - cx;
                                    by = 3 * (y3 - y2) - cy;

                                    ax = x4 - x1 - cx - bx;
                                    ay = y4 - y1 - cy - by;

                                    x = (int)((ax * Math.Pow(t, 3)) + (bx * Math.Pow(t, 2)) + (cx * t) + x1);
                                    y = (int)((ay * Math.Pow(t, 3)) + (by * Math.Pow(t, 2)) + (cy * t) + y1);

                                    graphicsObj.DrawRectangle(zimulis, x, y, izmers, izmers);
                                }

                                if (indeks == 0)
                                {
                                    graphicsObj.FillEllipse(Brushes.Black, x2, y2, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x2, y2, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x3, y3, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Black, x4, y4, 10, 10);

                                }
                                else
                                {
                                    graphicsObj.FillEllipse(Brushes.Red, x1, y1, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x2, y2, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x3, y3, 10, 10);
                                    graphicsObj.FillEllipse(Brushes.Red, x4, y4, 10, 10);
                                }
                                
                            }
                            else
                            {
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                DialogResult result;

                                result = MessageBox.Show("Please enter a time step in the interval 0<t<=1!", "Wrong time step!", buttons);

                                if (result == System.Windows.Forms.DialogResult.Yes)
                                {

                                    this.Close();

                                }
                            }
                        }

                        else
                        {
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                            DialogResult result;

                            result = MessageBox.Show("Please enter non-negative coordinates!", "At least one of the coordinates is negative!", buttons);

                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {

                                 this.Close();

                            }
                         }
                    }

            }
            catch (FormatException)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show("Please enter all coordinates!", "At least one of the point coordinates is not entered!", buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    this.Close();

                }

            }

        }

        private void button6_Click(object sender, EventArgs e) //cleaning button
        {
            if (graphicsObj != null)
            {
                graphicsObj.Clear(Color.White);
            }
        }



        private void comboBox2_SelectedValueChanged(object sender, EventArgs e) //if the drawing size has been changed
        {
            izmers = Convert.ToInt32(comboBox2.SelectedValue);
        }

        private void comboBox1_SelectedValueChanged_1(object sender, EventArgs e) //if the color is changed
        {

            indeks = comboBox1.SelectedIndex;

            if (indeks == 0)
            {
                zimulis = new Pen(Color.Red);
            }
            else if (indeks == 1)
            {
                zimulis = new Pen(Color.Green);
            }
            else if (indeks == 2)
            {
                zimulis = new Pen(Color.Blue);
            }
            else if (indeks == 3)
            {
                zimulis = new Pen(Color.Black);
            }
            else if (indeks == 4)
            {
                zimulis = new Pen(Color.Violet);
            }
            else if (indeks == 5)
            {
                zimulis = new Pen(Color.Cyan);
            }
            else if (indeks == 6)
            {
                zimulis = new Pen(Color.Gold);
            }
            else if (indeks == 7)
            {
                zimulis = new Pen(Color.Gray);
            }
            else if (indeks == 8)
            {
                zimulis = new Pen(Color.Indigo);
            }
            else if (indeks == 9)
            {
                zimulis = new Pen(Color.Magenta);
            }
            else if (indeks == 10)
            {
                zimulis = new Pen(Color.Orange);
            }
            else
            {
                zimulis = new Pen(Color.Yellow);
            }
        }

    }
}
