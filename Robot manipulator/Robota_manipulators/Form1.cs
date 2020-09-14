using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// to work with the OpenGL library
using Tao.OpenGl;
// to work with the library FreeGLUT
using Tao.FreeGlut;
// for working with the control element SimpleOpenGLControl
using Tao.Platform.Windows;


namespace Robota_manipulators
{

    public partial class Form1 : Form
    {
        //the lengths and widths of each part of the robot arm
        float Bazes_platums = 1.0f;
        float Bazes_radiuss = 1.0f;
        float Augshejas_rokas_platums = 3.0f;
        float Augshejas_rokas_garums = 0.6f;
        float Apakshejas_rokas_platums = 3.0f;
        float Apakshejas_rokas_garums = 0.6f;
        float Augshejo_pirkstu_platums = 0.4f;
        float Augshejo_pirkstu_garums = 0.1f;
        float Apakshejo_pirkstu_platums = 0.4f;
        float Apakshejo_pirkstu_garums = 0.1f;
        //lighting
        static float[] LightAmbient = { 0.1f, 0.1f, 0.05f, 1.0f };
        static float[] LightEmission = { 1.0f, 1.0f, 0.8f, 1.0f };
        static float[] LightDiffuse = { 1.0f, 1.0f, 0.8f, 1.0f };
        static float[] LightSpecular = { 1.0f, 1.0f, 1.0f, 1.0f };
        static float[] LightDirection= {1.0f, 1.0f, 1.0f};
        float [] point = new float[3];
        //the angle of rotation of each part
        static float[] theta = { -165.0f, -55.0f, -55.0f, 15.0f, -65.0f, -35.0f, 70.0f }; //the angle of rotation of the parts of robot arm
        static int axis = 0; //indicates which part to rotate (which array theta index to take)

        public Form1()
        {
            InitializeComponent();
            SK.InitializeContexts();
        }

        void baze()
        {
             Gl.glPushMatrix(); //save the matrix

             Gl.glRotatef(-90.0f, 1.0f, 0.0f, 0.0f); //rotate the cylinder so that it is on the base and not on a round wall

            Glu.gluCylinder(Glu.gluNewQuadric(), Bazes_radiuss, Bazes_radiuss, Bazes_platums, 25, 25);
            //Base as a cylinder, the last two parameters 25 and 25 are used to make the cylinder round
            Gl.glPopMatrix(); //loading matrix
       }

        void Augsheja_roka() //draw the upper hand
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, 0.5f * Augshejas_rokas_platums, 0.0f);
            Gl.glScalef(Augshejas_rokas_garums, Augshejas_rokas_platums, Augshejas_rokas_garums);
            Glut.glutSolidCube(1.0); //draw a filled parallelogram with radius 1
            Gl.glPopMatrix();
        }

        void Apaksheja_roka() //draw the lower hand
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, 0.5f * Apakshejas_rokas_platums, 0.0f);
            Gl.glScalef(Apakshejas_rokas_garums, Apakshejas_rokas_platums, Apakshejas_rokas_garums);
            Glut.glutSolidCube(1.0); //zdraw a filled parallelepiped
            Gl.glPopMatrix();
        }

        void augshejie_pirksti(float nobide) //draw the upper fingers, "nobide" indicates how much to move along the x-axis, 
            //this is necessary so that the fingers can be placed on the x-axis one after the other with spaces
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(nobide, 0.5f * Augshejo_pirkstu_platums, 0.0f);
            Gl.glScalef(Augshejo_pirkstu_garums, Augshejo_pirkstu_platums, Augshejo_pirkstu_garums);
            Glut.glutSolidCube(1.0);
            Gl.glPopMatrix();
        }

        void apakshejie_pirksti(float nobide) //Draw the lower parts of the fingertips
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(nobide, 0.5f * Apakshejo_pirkstu_platums, 0.0f);
            Gl.glScalef(Apakshejo_pirkstu_garums, Apakshejo_pirkstu_platums, Apakshejo_pirkstu_garums);
            Glut.glutSolidCube(1.0);
            Gl.glPopMatrix();

        }

        void augshejs_ikshkis(float nobide) //Draw the upper part of the thumb
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(nobide, 0.5f * Augshejo_pirkstu_platums, 0.0f);
            Gl.glScalef(Augshejo_pirkstu_garums, Augshejo_pirkstu_platums, Augshejo_pirkstu_garums);
            Glut.glutSolidCube(1.0);
            Gl.glPopMatrix();
        }

        void apakshejs_ikshkis(float nobide) //Draw the lower part of the thumb
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(nobide, 0.5f * Apakshejo_pirkstu_platums, 0.0f);
            Gl.glScalef(Augshejo_pirkstu_garums, Augshejo_pirkstu_platums, Augshejo_pirkstu_garums);
            Glut.glutSolidCube(1.0);
            Gl.glPopMatrix();
        }



        public void display() //robot hand display
        {

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT); //clean colors and depth
            Gl.glLoadIdentity(); //Loading the identity matrix
            Glu.gluLookAt(0.0f, 2.0f, 10.0f,	// eye position - point (0,2,10)
                        0.0f, 2.0f, 0.8f,			// robot arm position - point (0,2.0,0)
                        0.0f, 1.0f, 0.0f);	// camera location


            Gl.glColor3f(1.0f, 0.0f, 0.0f); //Apply color to the base - red
            Gl.glRotatef(theta[0], 0.0f, 1.0f, 0.0f); //rotate the base along the y axis
            baze();


            Gl.glTranslatef(0.0f, Bazes_platums, 0.0f);
            Gl.glRotatef(theta[1], 1.0f, 0.0f, 0.0f); //rotate the upper arm along the x axis
            Gl.glColor3f(1.0f, 1.0f, 0.0f); //Apply color to the upper hand - yellow
            Augsheja_roka();


            Gl.glTranslatef(0.0f, Augshejas_rokas_platums, 0.0f);
            Gl.glRotatef(theta[2], 1.0f, 0.0f, 0.0f); //rotate the lower arm along the x axis
            Gl.glColor3f(0.0f, 1.0f, 0.0f); //Apply color to the lower hand - green
            Apaksheja_roka();

            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, Apakshejas_rokas_platums , 0.25f);
            Gl.glRotatef(theta[3], 1.0f, 0.0f, 0.0f); //rotate the upper fingers along the x axis
            Gl.glColor3f(1.0f, 0.0f, 0.0f); //Apply color to the upper fingers - red
            augshejie_pirksti(0.28f);
            augshejie_pirksti(0.08f);
            augshejie_pirksti(-0.1f);
            augshejie_pirksti(-0.3f);

            Gl.glTranslatef(0.0f, Augshejo_pirkstu_platums, 0.0f);
            Gl.glRotatef(theta[4], 1.0f, 0.0f, 0.0f); //rotate the lower fingers along the x axis
            Gl.glColor3f(1.0f, 1.0f, 0.0f); //Apply color to the lower fingers - yellow
            apakshejie_pirksti(0.28f);
            apakshejie_pirksti(0.08f);
            apakshejie_pirksti(-0.1f);
            apakshejie_pirksti(-0.3f);

            Gl.glPopMatrix();
           

            Gl.glTranslatef(0.15f, 3.0f, -0.25f);
            Gl.glRotatef(theta[5], 1.0f, 0.0f, 0.0f); //rotate the upper thumb along the x-axis
            Gl.glColor3f(1.0f, 0.0f, 0.0f); //Apply color to the upper thumb - red
            augshejs_ikshkis(0.10f);

            Gl.glTranslatef(0.0f, 0.4f, 0.0f);
            Gl.glRotatef(theta[6], 1.0f, 0.0f, 0.0f); //rotate the lower thumb along the x axis
            Gl.glColor3f(1.0f, 1.0f, 0.0f); //Apply color to the lower part of the thumb - yellow
            apakshejs_ikshkis(0.10f);

            Gl.glFlush();
            SK.Invalidate();


        }



        private void Form1_Load(object sender, EventArgs e) //starting the program
        {
            Glut.glutInit(); //Glut initialization
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);


            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f); //clean the paints

            Gl.glViewport(0, 0, SK.Width, SK.Height); //View rectangle


            Gl.glMatrixMode(Gl.GL_PROJECTION); //project in the room
            Gl.glLoadIdentity(); //load identity matrix
            Glu.gluPerspective(60, (float)SK.Width / (float)SK.Height, 0.1, 200);
            Gl.glMatrixMode(Gl.GL_MODELVIEW); //Instead of a projection matrix, we will use a model view matrix


            Gl.glEnable(Gl.GL_DEPTH_TEST); //turn on depth testing

            int[] Textures = new int[1];
            Gl.glEnable(Gl.GL_TEXTURE_2D); //Turns on 2D textures
            Gl.glGenTextures(1, Textures); //Defines the texture name
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, Textures[0]); //Assigns a name to the texture object

            Bitmap bitmap = new Bitmap("Texture.bmp"); //load image "Texture.bmp" for texture
            System.Drawing.Imaging.BitmapData data; //new BitmapData object
            Rectangle Rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            data = bitmap.LockBits(Rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //The texture image is read-only and the pixel format is in RGB system
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0,
                            Gl.GL_RGBA, data.Width, data.Height, 0,
                            Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, data.Scan0);
            //On the robot hand will put 2D texture, MIP texturing levels - 0, number of color components - corresponds to RGBA,
            //width and height - length and width of the texture image, format - corresponds to RGBA, pixel data type - usigned,
            //Scan0 indicates the image memory area
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);

            Gl.glEnable(Gl.GL_TEXTURE_GEN_S); //allow automatic texture coordinate generation
            Gl.glEnable(Gl.GL_TEXTURE_GEN_T);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL); //Apply colored material
            display(); //display
            //put on the lights
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, LightAmbient);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, LightDiffuse);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, LightSpecular);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, LightDirection);
            Gl.glEnable(Gl.GL_LIGHT0); //turn on the lights
            Gl.glEnable(Gl.GL_LIGHTING);

            display(); //show again to turn on the light

            //Put the position lighting coordinates in the text fields
            textBox1.Text = LightDirection[0].ToString();
            textBox2.Text = LightDirection[1].ToString();
            textBox3.Text = LightDirection[2].ToString();

            Gl.glEnd();
            SK.Select(); //select (focus) this SimpleOpenGL form to be able to use the keyboard
      
        }

        private void button1_Click(object sender, EventArgs e) //By pressing the lighting button
        {
            try
            {
                //read the coordinates of the position of the light
                LightDirection[0] = (float)Convert.ToDecimal(textBox1.Text);
                LightDirection[1] = (float)Convert.ToDecimal(textBox2.Text);
                LightDirection[2] = (float)Convert.ToDecimal(textBox3.Text);


                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, LightDirection); //change the position lighting coordinates
                display();
                SK.Select(); //select (focus) this SimpleOpenGL form to be able to use the keyboard

            }
            catch (FormatException)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show("Please enter lighting coordinates as numbers!", "No numbers entered!", buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    this.Close();

                }
                SK.Select(); //select (focus) this SimpleOpenGL form to be able to use the keyboard
            }
        }


        private void SK_KeyPress(object sender, KeyPressEventArgs e) //By pressing the keys on the keyboard
        {
            //base rotation along the y axis
            if (e.KeyChar.ToString() == "q") //the base rotates counterclockwise when q is pressed
            {
                axis = 0; //change the 0 element of the array theta that corresponds to the base
                theta[axis] = theta[axis] + 5.0f;
                if (theta[axis] > 360.0) //if the ring is bypassed
                {
                    theta[axis] = 0.0f; //the value of the angle is again zero
                }
                display();
            }

            else if (e.KeyChar.ToString() == "a") //the base rotates clockwise when press a
            {
                axis = 0;
                theta[axis] = theta[axis] - 5.0f;
                if (theta[axis] < -360.0) //if the ring is bypassed from the other side
                {
                    theta[axis] = 0.0f; //the value of the angle is again zero
                }
                display();
            }
            //rotation of the upper arm along the x axis
            else if (e.KeyChar.ToString() == "w")
            {
                axis = 1;
                theta[axis] = theta[axis] + 5.0f;
                if (theta[axis] > 70.0) //the upper arm can be rotated a maximum of 70 degrees
                {
                   theta[axis] = 70.0f;
                }
                display();
            }
            else if (e.KeyChar.ToString() == "s")
            {
                axis = 1;
                theta[axis] = theta[axis] - 5.0f;
                if (theta[axis] < -70.0) //or -70 degrees if to the other side
                {
                   theta[axis] = -70.0f;
                }
                display();
            }
            //rotation of the lower arm along the x axis
            else if (e.KeyChar.ToString() == "e")
            {
                axis = 2;
                theta[axis] = theta[axis] + 5.0f;
                if (theta[axis] > 70.0)
                {
                    theta[axis] = 70.0f;
                }
                display();
            }
            else if (e.KeyChar.ToString() == "d")
            {
                axis = 2;
                theta[axis] = theta[axis] - 5.0f;
                if (theta[axis] < -70.0)
                {
                    theta[axis] = -70.0f;
                }
                display();
            }
            //rotation of the four upper fingers along the x axis
            else if (e.KeyChar.ToString() == "r")
            {
                axis = 3;
                theta[axis] = theta[axis] + 5.0f;

                if (theta[axis] > 70.0)
                {
                    theta[axis] = 70.0f;
                }
                display();

               
               
            }
            else if (e.KeyChar.ToString() == "f")
            {
                axis = 3;
                theta[axis] = theta[axis] - 5.0f;
                if (theta[axis] < -70.0)
                {
                    theta[axis] = -70.0f;
                }
                display();
            }
            //rotation of the lower parts of the four fingers along the x axis
            else if (e.KeyChar.ToString() == "t")
            {
                axis = 4;
                theta[axis] = theta[axis] + 5.0f;
                if (theta[axis] > 70.0)
                {
                    theta[axis] = 70.0f;
                }
                display();
            }
            else if (e.KeyChar.ToString() == "g")
            {
                axis = 4;
                theta[axis] = theta[axis] - 5.0f;
                if (theta[axis] < -70.0)
                {
                    theta[axis] = -70.0f;
                }
                display();
            }
            //rotation of the upper part of the thumb along the x-axis
            else if (e.KeyChar.ToString() == "y")
            {
                axis = 5;
                theta[axis] = theta[axis] + 5.0f;
                if (theta[axis] > 70.0)
                {
                    theta[axis] = 70.0f;
                }
                display();
            }
            else if (e.KeyChar.ToString() == "h")
            {
                axis = 5;
                theta[axis] = theta[axis] - 5.0f;
                if (theta[axis] < -70.0)
                {
                    theta[axis] = -70.0f;
                }
                display();
            }
            //rotation of the lower part of the thumb along the x axis
            else if (e.KeyChar.ToString() == "u")
            {
                axis = 6;
                theta[axis] = theta[axis] + 5.0f;
                if (theta[axis] > 70.0)
                {
                    theta[axis] = 70.0f;
                }
                display();
            }
            else if (e.KeyChar.ToString() == "j")
            {
                axis = 6;
                theta[axis] = theta[axis] - 5.0f;
                if (theta[axis] < -70.0)
                {
                    theta[axis] = -70.0f;
                }
                display();
            }

        }


    }



}