using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace uzdevums2
{
    public partial class Form1 : Form
    {
        public List<Point> vertexArray = new List<Point>();
        public List<Point> rezerve = new List<Point>();
        public List<String> colors = new List<String> {"Red", "Green", "Blue", "Black", "Violet", "Cyan", 
        "Gold", "Gray", "Indigo", "Magenta", "Orange", "Yellow"};
        public SolidBrush zimulis = new SolidBrush(Color.Red);
        public Pen konturuZimulis = new Pen(Color.Black);
        public Pen zimulis1 = new Pen(Color.Red);
        public int indeks = 0;
        public int izmers = 1;
        public int virsotnes = 0;
        public Graphics graphicsObj;
        Bitmap bm;
        public int index = 0;


        public Form1()
        {
            InitializeComponent();
            bm = new Bitmap(pictureBox1.Size.Width,pictureBox1.Size.Height);
            
            pictureBox1.Image = bm;
            
            graphicsObj = Graphics.FromImage(bm);
            BindingSource b = new BindingSource();
            b.DataSource = colors;
            comboBox1.DataSource = b.DataSource;
            BindingSource b1 = new BindingSource();
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            vertexArray.Add(new Point(e.X, e.Y));
            
                if (indeks == 3)
                {
                    graphicsObj.FillRectangle(Brushes.Red, vertexArray[virsotnes].X, vertexArray[virsotnes].Y, izmers, izmers);

                }
                else
                {
                    graphicsObj.FillRectangle(Brushes.Black, vertexArray[virsotnes].X, vertexArray[virsotnes].Y, izmers, izmers);
                }
                virsotnes++;
                pictureBox1.Image = bm;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
               String virsotnuKoordinatas = textBox1.Text;
               textBox1.Clear();
               List<Char> x1 = new List<char>();
               List<Char> y1 = new List<char>();
               String x = " ";
               String y = " ";
               int vaiJaunaKoordinata = 0;
               int punkts = 0;

               for (int i = 0; i < virsotnuKoordinatas.Length; i++)
               {
                   if (Char.IsNumber(virsotnuKoordinatas, i) == true)
                   {
                       if (vaiJaunaKoordinata == 0)
                       {
                           x1.Add(virsotnuKoordinatas[i]);

                           if (i <= virsotnuKoordinatas.Length - 2)
                           {
                               if (Char.IsNumber(virsotnuKoordinatas, i + 1) == false)
                               {
                                   vaiJaunaKoordinata = 1;
                               }
                           }
                       }

                       else if (vaiJaunaKoordinata == 1)
                       {
                           y1.Add(virsotnuKoordinatas[i]);

                           if (i <= virsotnuKoordinatas.Length - 2)
                           {
                               if (Char.IsNumber(virsotnuKoordinatas, i + 1) == false)
                               {
                                   punkts++;
                                   vaiJaunaKoordinata = 0;
                                   x = string.Join("", x1.ToArray());
                                   y = string.Join("", y1.ToArray());
                                   x1.Clear();
                                   y1.Clear();

                                   if (punkts > index)
                                   {
                                       vertexArray.Add(new Point(Convert.ToInt32(x), Convert.ToInt32(y)));


                                       if (indeks == 3)
                                       {
                                           graphicsObj.FillRectangle(Brushes.Red, vertexArray[virsotnes].X, vertexArray[virsotnes].Y, izmers, izmers);
                                       }
                                       else
                                       {
                                           graphicsObj.FillRectangle(Brushes.Black, vertexArray[virsotnes].X, vertexArray[virsotnes].Y, izmers, izmers);

                                       }

                                       virsotnes++;
                                       pictureBox1.Image = bm;
                                   }
                               }


                           }
                           else
                           {
                               punkts++;
                              x = string.Join("", x1.ToArray());
                              y = string.Join("", y1.ToArray());
                              x1.Clear();
                              y1.Clear();
                              if (punkts>index)
                              {
                              vertexArray.Add(new Point(Convert.ToInt32(x), Convert.ToInt32(y)));
                             
                             
                                  if (indeks == 3)
                                  {
                                      graphicsObj.FillRectangle(Brushes.Red, vertexArray[virsotnes].X, vertexArray[virsotnes].Y, izmers, izmers);
                                  }
                                  else
                                  {
                                      graphicsObj.FillRectangle(Brushes.Black, vertexArray[virsotnes].X, vertexArray[virsotnes].Y, izmers, izmers);
                                  }
                                  virsotnes++;
                                  pictureBox1.Image = bm;
                                }
                              
                              }
                         }

                   }
               }

               index = 0;
               for (int i = 0; i < vertexArray.Count; i++)
               {
                   textBox1.AppendText("X: " + vertexArray[i].X + "  Y: " + vertexArray[i].Y + "\n");
                   index++;
               }

               

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (vertexArray.Count > 1)
            {
                if (indeks == 3)
                {
                    konturuZimulis = new Pen(Color.Red);
                    graphicsObj.DrawPolygon(konturuZimulis, vertexArray.ToArray());
                }
                else
                {
                    konturuZimulis = new Pen(Color.Black);
                    graphicsObj.DrawPolygon(konturuZimulis, vertexArray.ToArray());
                }
               pictureBox1.Image = bm;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (graphicsObj != null)
            {
                graphicsObj.Clear(Color.White);
            }

            vertexArray.Clear();
            textBox1.Text = " ";
            virsotnes = 0;
            pictureBox1.Image = bm;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                graphicsObj.FillPolygon(zimulis, vertexArray.ToArray());
                pictureBox1.Image = bm;
            }
            catch (ArgumentException)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show("Please enter a vertex!", "No vertices specified or entered!", buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    this.Close();

                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                double[,] Edges = new double[vertexArray.Count, 9]; //2D array, rows correspond to the number of vertices, columns:
                 // 1 - vertex in a row, 2 - x coordinate of the current vertex, 3 - y coordinate of the current vertex, 4 - x coordinate
                 // of the next vertex, 5 - y coordinate of the next vertex, 6 - 1 / m, where m is the ratio of current and next points
                 // 7 - minimum y between the two vertices, 8 - x corresponding to the minimum y, 9 - maximum y
                double m;
                List<int> horizontalesX = new List<int>();

                for (int i = 0; i < vertexArray.Count; i++)
                {

                    if (i < vertexArray.Count - 1)
                    {
                        Edges[i, 0] = i;
                        Edges[i, 1] = vertexArray[i].X;
                        Edges[i, 2] = vertexArray[i].Y;
                        Edges[i, 3] = vertexArray[i + 1].X;
                        Edges[i, 4] = vertexArray[i + 1].Y;
                        m = (Convert.ToDouble(vertexArray[i + 1].Y) - Convert.ToDouble(vertexArray[i].Y)) /
                            (Convert.ToDouble(vertexArray[i + 1].X) - Convert.ToDouble(vertexArray[i].X));
                        Edges[i, 5] = 1.0 / m;
                        if (vertexArray[i].Y < vertexArray[i + 1].Y)
                        {
                            Edges[i, 6] = vertexArray[i].Y;
                            Edges[i, 7] = vertexArray[i].X;
                        }
                        else
                        {
                            Edges[i, 6] = vertexArray[i + 1].Y;
                            Edges[i, 7] = vertexArray[i + 1].X;
                        }
                        if (vertexArray[i].Y > vertexArray[i + 1].Y)
                        {
                            Edges[i, 8] = vertexArray[i].Y;
                        }
                        else
                        {
                            Edges[i, 8] = vertexArray[i + 1].Y;
                        }



                    }
                    else
                    {
                        Edges[i, 0] = i;
                        Edges[i, 1] = vertexArray[i].X;
                        Edges[i, 2] = vertexArray[i].Y;
                        Edges[i, 3] = vertexArray[0].X;
                        Edges[i, 4] = vertexArray[0].Y;
                        m = (Convert.ToDouble(vertexArray[0].Y) - Convert.ToDouble(vertexArray[i].Y)) /
                            (Convert.ToDouble(vertexArray[0].X) - Convert.ToDouble(vertexArray[i].X));
                        Edges[i, 5] = 1.0 / m;
                        if (vertexArray[i].Y < vertexArray[0].Y)
                        {
                            Edges[i, 6] = vertexArray[i].Y;
                            Edges[i, 7] = vertexArray[i].X;
                        }
                        else
                        {
                            Edges[i, 6] = vertexArray[0].Y;
                            Edges[i, 7] = vertexArray[0].X;
                        }
                        if (vertexArray[i].Y > vertexArray[0].Y)
                        {
                            Edges[i, 8] = vertexArray[i].Y;
                        }
                        else
                        {
                            Edges[i, 8] = vertexArray[0].Y;
                        }
                    }
                }



                double yp, yc, yn, xp, xc, xn, xprimc;

                for (int i = 0; i < vertexArray.Count; i++)
                {
                    if (i == 0) //at i = 0 we calculate the previous, current and next y
                    {
                        yp = Convert.ToDouble(Edges[vertexArray.Count - 1, 2]);
                        yc = Convert.ToDouble(Edges[i, 2]);
                        yn = Convert.ToDouble(Edges[i + 1, 2]);
                        xp = Convert.ToDouble(Edges[vertexArray.Count - 1, 1]);
                        xc = Convert.ToDouble(Edges[i, 1]);
                        xn = Convert.ToDouble(Edges[i + 1, 1]);
                    }
                    else if (i == vertexArray.Count - 1) //if i is the last iteration
                    //calculate the previous, current and next y
                    {
                        yp = Convert.ToDouble(Edges[i - 1, 2]);
                        yc = Convert.ToDouble(Edges[i, 2]);
                        yn = Convert.ToDouble(Edges[0, 2]);
                        xp = Convert.ToDouble(Edges[i - 1, 1]);
                        xc = Convert.ToDouble(Edges[i, 1]);
                        xn = Convert.ToDouble(Edges[0, 1]);


                    }
                    else //calculate the previous, current and next y in other cases
                    {
                        yp = Convert.ToDouble(Edges[i - 1, 2]);
                        yc = Convert.ToDouble(Edges[i, 2]);
                        yn = Convert.ToDouble(Edges[i + 1, 2]);
                        xp = Convert.ToDouble(Edges[i - 1, 1]);
                        xc = Convert.ToDouble(Edges[i, 1]);
                        xn = Convert.ToDouble(Edges[i + 1, 1]);

                    }

                    if (yp < yc && yc < yn)
                    {
                        m = (yp - yc) / (xp - xc);
                        xprimc = xp + (1.0 / m) * (yc - 1 - yp);

                        if (i == 0)
                        {
                            //we update the coordinates of the vertices in the list of edges
                            Edges[vertexArray.Count - 1, 3] = Math.Round(xprimc, 0);
                            Edges[vertexArray.Count - 1, 4] = yc - 1;
                        }
                        else
                        {
                            //we update the coordinates of the vertices in the list of edges
                            Edges[i - 1, 3] = Convert.ToInt32(Math.Round(xprimc, 0));
                            Edges[i - 1, 4] = yc - 1;
                        }

                    }

                    else if (yp > yc && yc > yn)
                    {
                        m = (yn - yc) / (xn - xc);
                        xprimc = xn + (1.0 / m) * (yc - 1 - yn);

                        //we update the coordinates of the vertices in the list of edges
                        Edges[i, 1] = Convert.ToInt32(Math.Round(xprimc, 0));
                        Edges[i, 2] = yc - 1;

                    }

                }



                for (int i = 0; i < vertexArray.Count; i++) //we update all other changed fields in the edge list
                {

                    m = (Edges[i, 4] - Edges[i, 2]) / (Edges[i, 3] - Edges[i, 1]);
                    Edges[i, 5] = 1 / m;
                    if (Edges[i, 2] < Edges[i, 4])
                    {
                        Edges[i, 6] = Edges[i, 2];
                        Edges[i, 7] = Edges[i, 1];
                    }
                    else
                    {
                        Edges[i, 6] = Edges[i, 4];
                        Edges[i, 7] = Edges[i, 3];
                    }
                    if (Edges[i, 2] > Edges[i, 4])
                    {
                        Edges[i, 8] = Edges[i, 2];
                    }
                    else
                    {
                        Edges[i, 8] = Edges[i, 4];
                    }

                }

                double x = 0.0;
                List<Point> intersections = new List<Point>();
                for (int i = 0; i < vertexArray.Count; i++) //we are looking for horizontal line slits with the edge of the landfill
                {
                    for (int j = Convert.ToInt32(Edges[i, 6]); j <= Convert.ToInt32(Edges[i, 8]); j++) //for each edge of the landfill we look at all possible slits
                    {
                        if (j == Convert.ToInt32(Edges[i, 6])) //if there is a minimum y of this edge
                        {
                            x = Edges[i, 7];
                            intersections.Add(new Point(Convert.ToInt32(Edges[i, 7]), j)); //take the x corresponding to this minimum y
                        }

                        else if (j == Convert.ToInt32(Edges[i, 8])) //if there is a maximum y of this edge
                        {
                            if (Edges[i, 2] > Edges[i, 4])
                            {
                                x = 0;
                                intersections.Add(new Point(Convert.ToInt32(Edges[i, 1]), j)); //take the x corresponding to this maximum y
                            }
                            else
                            {
                                x = 0;
                                intersections.Add(new Point(Convert.ToInt32(Edges[i, 3]), j)); //take the x corresponding to this maximum y
                            }
                        }

                        else
                        {
                            x = x + Edges[i, 5];
                            intersections.Add(new Point(Convert.ToInt32(Math.Round(x, 0)), Convert.ToInt32(j)));
                        }
                    }
                }

                int ymin = intersections[0].Y;
                int ymax = intersections[0].Y;

                for (int i = 0; i < intersections.Count; i++)
                {

                    if (intersections[i].Y < ymin) //determine which of all y is the smallest
                    {
                        ymin = intersections[i].Y;
                    }
                    if (intersections[i].Y > ymax) //determine which of all y is the largest
                    {
                        ymax = intersections[i].Y;
                    }

                }

                for (int y = ymin; y <= ymax; y++)
                {
                    for (int j = 0; j < intersections.Count; j++)
                    {

                        if (intersections[j].Y == y)
                        {
                            horizontalesX.Add(intersections[j].X);
                        }
                    }
                    horizontalesX.Sort();

                    for (int j = 0; j <= horizontalesX.Count - 2; j = j + 2)
                    {
                        graphicsObj.DrawLine(zimulis1, new Point(horizontalesX[j], y), new Point(horizontalesX[j + 1], y));
                    }
                    pictureBox1.Image = bm;
                    horizontalesX.Clear();
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show("Please enter a vertex!", "No vertices specified or entered!", buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    this.Close();

                }
            }

            catch (IndexOutOfRangeException)
            {
                
            }


        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            indeks = comboBox1.SelectedIndex;

            if (indeks == 0)
            {
                zimulis = new SolidBrush(Color.Red);
                zimulis1 = new Pen(Color.Red);
            }
            else if (indeks == 1)
            {
                zimulis = new SolidBrush(Color.Green);
                zimulis1 = new Pen(Color.Green);
            }
            else if (indeks == 2)
            {
                zimulis = new SolidBrush(Color.Blue);
                zimulis1 = new Pen(Color.Blue);
            }
            else if (indeks == 3)
            {
                zimulis = new SolidBrush(Color.Black);
                zimulis1 = new Pen(Color.Black);
            }
            else if (indeks == 4)
            {
                zimulis = new SolidBrush(Color.Violet);
                zimulis1 = new Pen(Color.Violet);
            }
            else if (indeks == 5)
            {
                zimulis = new SolidBrush(Color.Cyan);
                zimulis1 = new Pen(Color.Cyan);
            }
            else if (indeks == 6)
            {
                zimulis = new SolidBrush(Color.Gold);
                zimulis1 = new Pen(Color.Gold);
            }
            else if (indeks == 7)
            {
                zimulis = new SolidBrush(Color.Gray);
                zimulis1 = new Pen(Color.Gray);
            }
            else if (indeks == 8)
            {
                zimulis = new SolidBrush(Color.Indigo);
                zimulis1 = new Pen(Color.Indigo);
            }
            else if (indeks == 9)
            {
                zimulis = new SolidBrush(Color.Magenta);
                zimulis1 = new Pen(Color.Magenta);
            }
            else if (indeks == 10)
            {
                zimulis = new SolidBrush(Color.Orange);
                zimulis1 = new Pen(Color.Orange);
            }
            else
            {
                zimulis = new SolidBrush(Color.Yellow);
                zimulis1 = new Pen(Color.Yellow);
            }
        }

    }
}
