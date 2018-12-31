using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Enigma
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool keyPressed = false;
        Ellipse lastKey = new Ellipse();
        Ellipse[] allCircles;
        
        public MainPage()
        {
            this.InitializeComponent();
            Windows.UI.Core.CoreWindow.GetForCurrentThread().KeyDown += Keyboard_KeyDown;
            Windows.UI.Core.CoreWindow.GetForCurrentThread().KeyUp += Keyboard_KeyUp;

            Ellipse[] circles = { circle_a, circle_b, circle_c, circle_d, circle_e, circle_f, circle_g, circle_h, circle_i, circle_j, circle_k, circle_l, circle_m, circle_n, circle_o, circle_p, circle_q, circle_r, circle_s, circle_t, circle_u, circle_v, circle_w, circle_x, circle_y, circle_z };
            allCircles = circles;
        }

        private void incRotors()
        {
            int r1 = Int32.Parse(txt_rotor1_value.Text);
            int r2 = Int32.Parse(txt_rotor2_value.Text);
            int r3 = Int32.Parse(txt_rotor3_value.Text);

            r1 = r1 + 1;
            if (r1 > 26) {
                r1 = 1;
                r2 = r2 + 1;
                if(r2>26)
                {
                    r2 = 1;
                    r3 = r3 + 1;
                    if(r3>26)
                    {
                        r3 = 1;
                    }
                }
            }
            txt_rotor1_value.Text = r1.ToString();
            txt_rotor2_value.Text = r2.ToString();
            txt_rotor3_value.Text = r3.ToString();
        }

        private void ProcessKey(string c)
        {
            char[] orig = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            char[] e_i = "EKMFLGDQVZNTOWYHXUSPAIBRCJ".ToCharArray();
            char[] e_ii = "AJDKSIRUXBLHWTMCQGZNPYFVOE".ToCharArray();
            char[] e_iii = "BDFHJLCPRTXVZNYEIWGAKMUSQO".ToCharArray();
            char[] e_iv = "ESOVPZJAYQUIRHXLNFTGKDCMWB".ToCharArray();
            char[] e_v = "VZBRGITYUPSDNHLXAWMJQOFECK".ToCharArray();
            char[] e_vi = "JPGVOUMFYQBENHZRDKASXLICTW".ToCharArray();
            char[] e_vii = "NZJHGRCXMYSWBOUFAIVLPEKQDT".ToCharArray();
            char[] e_viii = "FKQHTLXOCBJSPDZRAMEWNIUYGV".ToCharArray();

            char[][] enigma = { e_i, e_ii, e_iii, e_iv, e_v, e_vi, e_vii, e_viii };

            // ok get what the index of that letter we just typed... a-> 0, b->1, etc
            int origIndex = Array.IndexOf(orig, char.Parse(c));

            // now identify what rotor codes 1,2,3 are using
            char[] r1 = enigma[Int32.Parse(txt_rotor1_number.Text)-1];
            char[] r2 = enigma[Int32.Parse(txt_rotor2_number.Text)-1];
            char[] r3 = enigma[Int32.Parse(txt_rotor3_number.Text)-1];

            // now find what the user pressed key spits our of r1 based on index
            char r1Val = r1[(((origIndex+Int32.Parse(txt_rotor1_value.Text))%26))];
            int r1Index = Array.IndexOf(r1, r1Val);
            char r2Val = r2[((Int32.Parse(txt_rotor2_value.Text) + r1Index) % 26)];
            int r2Index = Array.IndexOf(r2, r2Val);
            char r3Val = r3[((Int32.Parse(txt_rotor3_value.Text) + r2Index) % 26)];
            int r3Index = Array.IndexOf(r3, r3Val);

            // now go thru the reflector

            // now go back thru the rotors

            //txt_rotor3_number.Text = r2Val.ToString();

            allCircles[origIndex].Fill = new SolidColorBrush(Windows.UI.Colors.Beige);
            lastKey = allCircles[origIndex];
        }

        private void Keyboard_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            if (!keyPressed)
            {
                keyPressed = true;

                switch (e.VirtualKey)
                {
                    case VirtualKey.Q: ProcessKey( "Q"); break;
                    case VirtualKey.W: ProcessKey( "W"); break;
                    case VirtualKey.E: ProcessKey( "E"); break;
                    case VirtualKey.R: ProcessKey( "R"); break;
                    case VirtualKey.T: ProcessKey( "T"); break;
                    case VirtualKey.Y: ProcessKey( "Y"); break;
                    case VirtualKey.U: ProcessKey( "U"); break;
                    case VirtualKey.I: ProcessKey( "I"); break;
                    case VirtualKey.O: ProcessKey( "O"); break;
                    case VirtualKey.P: ProcessKey( "P"); break;
                    case VirtualKey.A: ProcessKey( "A"); break;
                    case VirtualKey.S: ProcessKey( "S"); break;
                    case VirtualKey.D: ProcessKey( "D"); break;
                    case VirtualKey.F: ProcessKey( "F"); break;
                    case VirtualKey.G: ProcessKey( "G"); break;
                    case VirtualKey.H: ProcessKey( "H"); break;
                    case VirtualKey.J: ProcessKey( "J"); break;
                    case VirtualKey.K: ProcessKey( "K"); break;
                    case VirtualKey.L: ProcessKey( "L"); break;
                    case VirtualKey.Z: ProcessKey( "Z"); break;
                    case VirtualKey.X: ProcessKey( "X"); break;
                    case VirtualKey.C: ProcessKey( "C"); break;
                    case VirtualKey.V: ProcessKey( "V"); break;
                    case VirtualKey.B: ProcessKey( "B"); break;
                    case VirtualKey.N: ProcessKey( "N"); break;
                    case VirtualKey.M: ProcessKey( "M"); break;
                }
            }
        }

        private void Keyboard_KeyUp(CoreWindow sender, KeyEventArgs e)
        {
            keyPressed = false;
            lastKey.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
            incRotors();
        }
    }
}
