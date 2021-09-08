using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FRLegends_Paint_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string OriginalCode;
        int OutlineWidth;
        string OutlineNewColor;
        
        public MainWindow()
        {
            IsThisStolen();
            InitializeComponent();
            FillHelp();
            UpdateStatus();
        }
        
        //
        private void IsThisStolen()
        {
            WebRequest request = WebRequest.Create("https://raw.githubusercontent.com/13DM/SitePictures/master/cOdEsPlEaSe.txt");
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
                
            }

            if (html.Contains("Stealing isn't cool."))
            {
                return;
            }
            else
            {
                MessageBox.Show("Value: " + "\"" + html + "\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }
        //

        private void Tb_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            OriginalCode = tb_Input.Text;
            System.Diagnostics.Debug.Write(OriginalCode);
        }

        private void B_MakeOutline_Click(object sender, RoutedEventArgs e)
        {
            string OL1 = "";
            string OL2 = "";
            string OL3 = "";
            string OL4 = "";

            if (OutlineNewColor.Length != 8)
            {
                return;
            }

            if (OutlineWidth < 1)
            {
                return;
            }

            using (StringReader reader = new StringReader(OriginalCode))
            {
                int count = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string logocode = line.Substring(0, 4);
                    string xaxis = line.Substring(4, 4);
                    string yaxis = line.Substring(8, 4);
                    string xsize = line.Substring(12, 4);
                    string ysize = line.Substring(16, 4);
                    string rotation = line.Substring(20, 4);
                    string color = line.Substring(24, 8);
                    string layertype = line.Substring(32, 2);
                    string mirror = line.Substring(34, 2);

                    int dec_xaxis = int.Parse(xaxis, System.Globalization.NumberStyles.HexNumber);
                    int dec_yaxis = int.Parse(yaxis, System.Globalization.NumberStyles.HexNumber);

                    int xposi = dec_xaxis + OutlineWidth;
                    int yposi = dec_yaxis + OutlineWidth;
                    int xnegative = dec_xaxis - OutlineWidth;
                    int ynegative = dec_yaxis - OutlineWidth;

                    if (xposi >= 65535)
                    {
                        xposi -= 65535;
                    }

                    if (yposi >= 65535)
                    {
                        yposi -= 65535;
                    }

                    if (xnegative >= 65535)
                    {
                        xnegative -= 65535;
                    }

                    if (ynegative >= 65535)
                    {
                        ynegative -= 65535;
                    }

                    string hex_xposi = xposi.ToString("x").ToUpperInvariant();
                    while (hex_xposi.Length < 4)
                    {
                        string fix = "0" + hex_xposi;
                        hex_xposi = fix;
                    }

                    string hex_yposi = yposi.ToString("x").ToUpperInvariant();
                    while (hex_yposi.Length < 4)
                    {
                        string fix = "0" + hex_yposi;
                        hex_yposi = fix;
                    }

                    string hex_xnegative = xnegative.ToString("x").ToUpperInvariant();
                    while (hex_xnegative.Length < 4)
                    {
                        string fix = "0" + hex_xnegative;
                        hex_xnegative = fix;
                    }

                    string hex_ynegative = ynegative.ToString("x").ToUpperInvariant();
                    while (hex_ynegative.Length < 4)
                    {
                        string fix = "0" + hex_ynegative;
                        hex_ynegative = fix;
                    }

                    if (hex_xposi.Length > 4 ||
                       hex_xnegative.Length > 4 ||
                       hex_yposi.Length > 4 ||
                       hex_ynegative.Length > 4)
                    {
                        if (hex_xposi.Length > 4)
                        {
                            try
                            {
                                hex_xposi = hex_xposi.Substring(4, 4);
                                if (hex_xposi.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "xposi value: " + hex_xposi, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "xposi value: " + hex_xposi, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        if (hex_xnegative.Length > 4)
                        {
                            try
                            {
                                hex_xnegative = hex_xnegative.Substring(4, 4);
                                if (hex_xnegative.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "xnegative value: " + xnegative, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "xnegative value: " + xnegative, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                        }
                        if (hex_yposi.Length > 4)
                        {
                            try
                            {
                                hex_yposi = hex_yposi.Substring(4, 4);
                                if (hex_yposi.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "ypositive value: " + hex_yposi, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "ypositive value: " + hex_yposi, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        if (hex_ynegative.Length > 4)
                        {
                            try
                            {
                                hex_ynegative = hex_ynegative.Substring(4, 4);
                                if (hex_ynegative.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "ynegative value: " + hex_ynegative, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "ynegative value: " + hex_ynegative, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }

                    OL1 += (logocode + hex_xposi + hex_yposi + xsize + ysize + rotation + OutlineNewColor + layertype + mirror + Environment.NewLine);
                    OL2 += (logocode + hex_xposi + hex_ynegative + xsize + ysize + rotation + OutlineNewColor + layertype + mirror + Environment.NewLine);
                    OL3 += (logocode + hex_xnegative + hex_yposi + xsize + ysize + rotation + OutlineNewColor + layertype + mirror + Environment.NewLine);
                    OL4 += (logocode + hex_xnegative + hex_ynegative + xsize + ysize + rotation + OutlineNewColor + layertype + mirror + Environment.NewLine);
                    count += 1;
                }
            }

            tb_Output.Text = OL1 + OL2 + OL3 + OL4 + OriginalCode;

            
            
        }

        private void Tb_OutlineSpan_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                OutlineWidth = int.Parse(tb_OutlineSpan.Text);
            }
            catch
            {
                OutlineWidth = 0;
                tb_OutlineSpan.Text = "";
            }
            System.Diagnostics.Debug.Write(OutlineWidth);
        }

        private void Tb_OutlineNewColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            OutlineNewColor = tb_OutlineNewColor.Text.ToUpperInvariant();
            tb_OutlineNewColor.Text = OutlineNewColor;
            string s = OutlineNewColor;

            if (s.Contains("G") || s.Contains("H") || s.Contains("I") || s.Contains("J") || s.Contains("K") ||
                s.Contains("L") || s.Contains("M") || s.Contains("N") || s.Contains("O") || s.Contains("P") ||
                s.Contains("Q") || s.Contains("R") || s.Contains("S") || s.Contains("T") || s.Contains("U") ||
                s.Contains("V") || s.Contains("W") || s.Contains("X") || s.Contains("Y") || s.Contains("Z"))
            {
                OutlineNewColor = OutlineNewColor.Substring(0, (OutlineNewColor.Length - 1));
                tb_OutlineNewColor.Text = OutlineNewColor;
                tb_OutlineNewColor.CaretIndex = OutlineNewColor.Length;
            }

            tb_OutlineNewColor.CaretIndex = OutlineNewColor.Length;
            if (OutlineNewColor.Length > 8)
            {
                OutlineNewColor = OutlineNewColor.Substring(0, 8);
                tb_OutlineNewColor.Text = OutlineNewColor;
                tb_OutlineNewColor.CaretIndex = OutlineNewColor.Length;
            }
        }

        private void B_ApplyEdits_Click(object sender, RoutedEventArgs e)
        {

            using (StringReader reader = new StringReader(OriginalCode))
            {
                int count = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string logocode = line.Substring(0, 4);
                    string xaxis = line.Substring(4, 4);
                    string yaxis = line.Substring(8, 4);
                    string xsize = line.Substring(12, 4);
                    string ysize = line.Substring(16, 4);
                    string rotation = line.Substring(20, 4);
                    string color = line.Substring(24, 8);
                    string layertype = line.Substring(32, 2);
                    string mirror = line.Substring(34, 2);

                    int NewXaxis;
                    int NewYaxis;
                    int NewXsize;
                    int NewYsize;
                    string NewColor;
                    int NewMirror;

                    int dec_xaxis = int.Parse(xaxis, System.Globalization.NumberStyles.HexNumber);
                    int dec_yaxis = int.Parse(yaxis, System.Globalization.NumberStyles.HexNumber);
                    int dec_xsize = int.Parse(xsize, System.Globalization.NumberStyles.HexNumber);
                    int dec_ysize = int.Parse(ysize, System.Globalization.NumberStyles.HexNumber);

                    if (tb_xaxis.Text != "")
                    {
                        NewXaxis = dec_xaxis + int.Parse(tb_xaxis.Text);
                    }
                    else
                    {
                        NewXaxis = dec_xaxis;
                    }
                    
                    if(tb_yaxis.Text != "")
                    {
                        NewYaxis = dec_yaxis + int.Parse(tb_yaxis.Text);
                    }
                    else
                    {
                        NewYaxis = dec_yaxis;
                    }

                    if(tb_xsize.Text != "")
                    {
                        NewXsize = dec_xsize + int.Parse(tb_xsize.Text);
                    }
                    else
                    {
                        NewXsize = dec_xsize;
                    }

                    if(tb_ysize.Text != "")
                    {
                        NewYsize = dec_ysize + int.Parse(tb_ysize.Text);
                    }
                    else
                    {
                        NewYsize = dec_ysize;
                    }

                    if(tb_color.Text != "")
                    {
                        NewColor = tb_color.Text;
                    }
                    else
                    {
                        NewColor = color;
                    }

                    if(tb_mirror.Text != "")
                    {
                        NewMirror = int.Parse(tb_mirror.Text);
                    }
                    else
                    {
                        NewMirror = int.Parse(mirror);
                    }

                    if (NewXaxis >= 65535)
                    {
                        NewXaxis -= 65535;
                    }

                    if (NewYaxis >= 65535)
                    {
                        NewYaxis -= 65535;
                    }

                    if (NewXsize >= 65535)
                    {
                        NewXsize -= 65535;
                    }

                    if (NewYsize >= 65535)
                    {
                        NewYsize -= 65535;
                    }
                    
                    string hex_newxaxis = NewXaxis.ToString("x").ToUpperInvariant();
                    while (hex_newxaxis.Length < 4)
                    {
                        string fix = "0" + hex_newxaxis;
                        hex_newxaxis = fix;
                    }

                    string hex_newyaxis = NewYaxis.ToString("x").ToUpperInvariant();
                    while (hex_newyaxis.Length < 4)
                    {
                        string fix = "0" + hex_newyaxis;
                        hex_newyaxis = fix;
                    }

                    string hex_newxsize = NewXsize.ToString("x").ToUpperInvariant();
                    while (hex_newxsize.Length < 4)
                    {
                        string fix = "0" + hex_newxsize;
                        hex_newxsize = fix;
                    }

                    string hex_newysize = NewYsize.ToString("x").ToUpperInvariant();
                    while (hex_newysize.Length < 4)
                    {
                        string fix = "0" + hex_newysize;
                        hex_newysize = fix;
                    }

                    string hex_newmirror = NewMirror.ToString("x").ToUpperInvariant();
                    while (hex_newmirror.Length < 2)
                    {
                        string fix = "0" + hex_newmirror;
                        hex_newmirror = fix;
                    }

                    if (hex_newmirror.Length > 2 ||
                        hex_newxaxis.Length > 4 ||
                        hex_newxsize.Length > 4 ||
                        hex_newyaxis.Length > 4 ||
                        hex_newysize.Length > 4)
                    {
                        if (hex_newmirror.Length > 2)
                        {
                            try
                            {
                                hex_newmirror = hex_newmirror.Substring(2, 2);
                                if (hex_newmirror.Length > 2)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "mirror value: " + hex_newmirror, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "mirror value: " + hex_newmirror, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        if (hex_newxaxis.Length > 4)
                        {
                            try
                            {
                                hex_newxaxis = hex_newxaxis.Substring(4, 4);
                                if (hex_newxaxis.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "x axis value: " + hex_newxaxis, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "x axis value: " + hex_newxaxis, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        if (hex_newxsize.Length > 4)
                        {
                            try
                            {
                                hex_newxsize = hex_newxsize.Substring(4, 4);
                                if (hex_newxsize.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "x size value: " + hex_newxsize, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "x size value: " + hex_newxsize, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        if (hex_newyaxis.Length > 4)
                        {
                            try
                            {
                                hex_newyaxis = hex_newyaxis.Substring(4, 4);
                                if ( hex_newyaxis.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "y axis value: " + hex_newyaxis, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "y axis value: " + hex_newyaxis, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        if (hex_newysize.Length > 4)
                        {
                            try
                            {
                                hex_newysize = hex_newysize.Substring(4, 4);
                                if (hex_newysize.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "y size value: " + hex_newysize, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "y size value: " + hex_newysize, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }

                    string EditedCode = logocode + hex_newxaxis + hex_newyaxis + hex_newxsize + hex_newysize + rotation + NewColor + layertype + hex_newmirror + Environment.NewLine;
                    tb_Output.Text += EditedCode;
                    count += 1;
                }
            }

            string s = tb_Output.Text;
            s.Replace("\n\n", "\n");
            tb_Output.Text = s;
        }

        private void Tb_color_TextChanged(object sender, TextChangedEventArgs e)
        {
            string NewColor = tb_color.Text.ToUpperInvariant();
            tb_color.Text = NewColor;
            string s = NewColor;

            if (s.Contains("G") || s.Contains("H") || s.Contains("I") || s.Contains("J") || s.Contains("K") ||
                s.Contains("L") || s.Contains("M") || s.Contains("N") || s.Contains("O") || s.Contains("P") ||
                s.Contains("Q") || s.Contains("R") || s.Contains("S") || s.Contains("T") || s.Contains("U") ||
                s.Contains("V") || s.Contains("W") || s.Contains("X") || s.Contains("Y") || s.Contains("Z"))
            {
                NewColor = NewColor.Substring(0, (NewColor.Length - 1));
                tb_color.Text = NewColor;
                tb_color.CaretIndex = NewColor.Length;
            }

            tb_color.CaretIndex = NewColor.Length;
            if (NewColor.Length > 8)
            {
                NewColor = NewColor.Substring(0, 8);
                tb_color.Text = NewColor;
                tb_color.CaretIndex = NewColor.Length;
            }
        }

        private void B_ResetButton_Click(object sender, RoutedEventArgs e)
        {
            tb_color.Text = "";
            tb_Input.Text = "";
            tb_Output.Text = "";
            tb_mirror.Text = "";
            tb_OutlineNewColor.Text = "";
            tb_rotation.Text = "";
            tb_xaxis.Text = "";
            tb_xsize.Text = "";
            tb_yaxis.Text = "";
            tb_ysize.Text = "";
            tb_OutlineSpan.Text = "";
            tb_words.Text = "";
            tb_scale.Text = "";
            tb_scaley.Text = "";
            tb_wordssize.Text = "";
            cb_textblackoutline.IsChecked = false;
            cb_textcoloroutline.IsChecked = false;
            cb_textnooutline.IsChecked = false;
        }

        static Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        private void B_Rotate90_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("This is experimental and does not work all the time. \n" + "This is a warning, program will continue action.", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);

            int x_low = -100000;
            int y_low = -100000;
            int x_high = 100000;
            int y_high = 100000;

            using (StringReader reader = new StringReader(OriginalCode))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {

                    string base_xaxis = line.Substring(4, 4);
                    string base_yaxis = line.Substring(8, 4);

                    int dec_basexaxis = int.Parse(base_xaxis, System.Globalization.NumberStyles.HexNumber);
                    int dec_baseyaxis = int.Parse(base_yaxis, System.Globalization.NumberStyles.HexNumber);

                    if (dec_basexaxis > 2000)
                    {
                        dec_basexaxis -= 65535;
                    }

                    if (dec_baseyaxis > 2000)
                    {
                        dec_baseyaxis -= 65535;
                    }

                    if (x_low == -100000 && y_low == -100000)
                    {
                        x_low = dec_basexaxis;
                        y_low = dec_baseyaxis;
                    }
                    if (x_high == 100000 && y_high == 100000)
                    {
                        x_high = dec_basexaxis;
                        y_high = dec_baseyaxis;
                    }

                    if (x_low > 2000)
                    {
                        x_low -= 65535;
                    }

                    if (x_high > 2000)
                    {
                        x_high -= 65535;
                    }

                    if (y_low > 2000)
                    {
                        y_low -= 65535;
                    }

                    if (y_high > 2000)
                    {
                        y_high -= 65535;
                    }

                    if(dec_basexaxis < x_low)
                    {
                        x_low = dec_basexaxis;
                    }

                    if (dec_baseyaxis < y_low)
                    {
                        y_low = dec_baseyaxis;
                    }

                    if (dec_basexaxis > x_high)
                    {
                        x_high = dec_basexaxis;
                    }

                    if (dec_baseyaxis > y_high)
                    {
                        y_high = dec_baseyaxis;
                    }
                }
            }

            Point center = new Point( x_high -((x_high - x_low) / 2) , y_high - ((y_high - y_low) / 2) );

            if(tb_rotation.Text == "")
            {
                MessageBox.Show("There was an error parsing a value. \n" + "rotation value: null", "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (StringReader reader = new StringReader(OriginalCode))
            {
                int count = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string logocode = line.Substring(0, 4);
                    string xaxis = line.Substring(4, 4);
                    string yaxis = line.Substring(8, 4);
                    string xsize = line.Substring(12, 4);
                    string ysize = line.Substring(16, 4);
                    string rotation = line.Substring(20, 4);
                    string color = line.Substring(24, 8);
                    string layertype = line.Substring(32, 2);
                    string mirror = line.Substring(34, 2);

                    int NewXaxis;
                    int NewYaxis;
                    int NewRotation;

                    int dec_xaxis = int.Parse(xaxis, System.Globalization.NumberStyles.HexNumber);
                    int dec_yaxis = int.Parse(yaxis, System.Globalization.NumberStyles.HexNumber);
                    int dec_rotation = int.Parse(rotation, System.Globalization.NumberStyles.HexNumber);

                    if (dec_xaxis > 2000)
                    {
                        dec_xaxis -= 65535;
                    }

                    if (dec_yaxis > 2000)
                    {
                        dec_yaxis -= 65535;
                    }

                    if (dec_rotation > 360)
                    {
                        dec_rotation = (dec_rotation - 65535);
                    }

                    Point startPoint = new Point(dec_xaxis, dec_yaxis);
                    Point newPoint = RotatePoint(startPoint, center, int.Parse(tb_rotation.Text));

                    NewXaxis = Convert.ToInt32(newPoint.X);
                    NewYaxis = Convert.ToInt32(newPoint.Y);
                    NewRotation = dec_rotation + int.Parse(tb_rotation.Text);

                    if(NewXaxis < 0)
                    {
                        NewXaxis += 65535;
                    }

                    if (NewYaxis < 0)
                    {
                        NewYaxis += 65535;
                    }

                    if (NewXaxis >= 65535)
                    {
                        NewXaxis -= 65535;
                    }

                    if (NewYaxis >= 65535)
                    {
                        NewYaxis -= 65535;
                    }

                    if (NewRotation >= 65535)
                    {
                        NewRotation -= 65535;
                    }

                    string hex_newxaxis = NewXaxis.ToString("x").ToUpperInvariant();
                    while (hex_newxaxis.Length < 4)
                    {
                        string fix = "0" + hex_newxaxis;
                        hex_newxaxis = fix;
                    }

                    string hex_newyaxis = NewYaxis.ToString("x").ToUpperInvariant();
                    while (hex_newyaxis.Length < 4)
                    {
                        string fix = "0" + hex_newyaxis;
                        hex_newyaxis = fix;
                    }

                    string hex_newrotation = NewRotation.ToString("x").ToUpperInvariant();
                    while (hex_newrotation.Length < 4)
                    {
                        string fix = "0" + hex_newrotation;
                        hex_newrotation = fix;
                    }

                    if (hex_newrotation.Length > 4 || hex_newxaxis.Length > 4 || hex_newyaxis.Length > 4 )
                    {
                        if (hex_newrotation.Length > 4)
                        {
                            try
                            {
                                hex_newrotation = hex_newrotation.Substring(4, 4);
                                if (hex_newrotation.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "rotation value: " + hex_newrotation, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "rotation value: " + hex_newrotation, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                        }
                        if (hex_newxaxis.Length > 4)
                        {
                            try
                            {
                                hex_newxaxis = hex_newxaxis.Substring(4, 4);
                                if (hex_newxaxis.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "x axis value: " + hex_newxaxis, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "x axis value: " + hex_newxaxis, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }

                    string EditedCode = logocode + hex_newxaxis + hex_newyaxis + xsize + ysize + hex_newrotation + color + layertype + mirror + Environment.NewLine;
                    tb_Output.Text += EditedCode;
                    count += 1;
                }
            }
        }

        private void B_copytoinput_Click(object sender, RoutedEventArgs e)
        {
            string s = tb_Output.Text.TrimEnd();
            tb_Input.Text = s;
            tb_Output.Text = "";
        }

        private void Tb_rotation_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = tb_rotation.Text;
            tb_rotation.Text = s.ToUpperInvariant();

            if (s.Contains("A") || s.Contains("B") || s.Contains("C") || s.Contains("D") || s.Contains("E") || s.Contains("F") ||
                s.Contains("G") || s.Contains("H") || s.Contains("I") || s.Contains("J") || s.Contains("K") ||
                s.Contains("L") || s.Contains("M") || s.Contains("N") || s.Contains("O") || s.Contains("P") ||
                s.Contains("Q") || s.Contains("R") || s.Contains("S") || s.Contains("T") || s.Contains("U") ||
                s.Contains("V") || s.Contains("W") || s.Contains("X") || s.Contains("Y") || s.Contains("Z"))
            {
                tb_rotation.Text = s.Substring(0, (tb_rotation.Text.Length - 1));
                s = tb_rotation.Text;
                tb_rotation.CaretIndex = s.Length;
            }

            tb_rotation.CaretIndex = s.Length;
            if (s.Length > 4)
            {
                s = s.Substring(0, 4);
                tb_rotation.Text = s;
                tb_rotation.CaretIndex = s.Length;
            }

            try
            {
                if (int.Parse(s) > 360 || int.Parse(s) < -360)
                {
                    s = "";
                    tb_rotation.Text = "";
                }
            }
            catch
            {
                return;
            }
        }

        private void B_scale_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("This is experimental and does not work all the time. \n" + "This is a warning, program will continue action.", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);

            int x_low = -100000;
            int y_low = -100000;
            int x_high = 100000;
            int y_high = 100000;
            double scalemod;
            double scalemody;

            if (tb_scale.Text != "")
            {
                scalemod = Convert.ToDouble(tb_scale.Text);
            }
            else
            {
                MessageBox.Show("X - Scale value is equal to null, it will be set to scale of 1. \n" + "This is a warning.", "Null Value Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                scalemod = 1;
                //return;
            }

            if (tb_scaley.Text != "")
            {
                scalemody = Convert.ToDouble(tb_scaley.Text);
            }
            else
            {
                MessageBox.Show("Y - Scale value is equal to null, it will be set to scale of 1. \n" + "This is a warning.", "Null Value Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                scalemody = 1;
                //return;
            }

            if (scalemod > 10)
            {
                scalemod = Convert.ToDouble(tb_scale.Text) / 100;
            }


            if (scalemody > 10)
            {
                scalemody = Convert.ToDouble(tb_scaley.Text) / 100;
            }


            using (StringReader reader = new StringReader(OriginalCode))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {

                    string base_xaxis = line.Substring(4, 4);
                    string base_yaxis = line.Substring(8, 4);

                    int dec_basexaxis = int.Parse(base_xaxis, System.Globalization.NumberStyles.HexNumber);
                    int dec_baseyaxis = int.Parse(base_yaxis, System.Globalization.NumberStyles.HexNumber);

                    if (dec_basexaxis > 2000)
                    {
                        dec_basexaxis -= 65535;
                    }

                    if (dec_baseyaxis > 2000)
                    {
                        dec_baseyaxis -= 65535;
                    }

                    if (x_low == -100000 && y_low == -100000)
                    {
                        x_low = dec_basexaxis;
                        y_low = dec_baseyaxis;
                    }
                    if (x_high == 100000 && y_high == 100000)
                    {
                        x_high = dec_basexaxis;
                        y_high = dec_baseyaxis;
                    }

                    if (x_low > 2000)
                    {
                        x_low -= 65535;
                    }

                    if (x_high > 2000)
                    {
                        x_high -= 65535;
                    }

                    if (y_low > 2000)
                    {
                        y_low -= 65535;
                    }

                    if (y_high > 2000)
                    {
                        y_high -= 65535;
                    }

                    if (dec_basexaxis < x_low)
                    {
                        x_low = dec_basexaxis;
                    }

                    if (dec_baseyaxis < y_low)
                    {
                        y_low = dec_baseyaxis;
                    }

                    if (dec_basexaxis > x_high)
                    {
                        x_high = dec_basexaxis;
                    }

                    if (dec_baseyaxis > y_high)
                    {
                        y_high = dec_baseyaxis;
                    }
                }
            }

            Point center = new Point(x_high - ((x_high - x_low) / 2), y_high - ((y_high - y_low) / 2));

            using (StringReader reader = new StringReader(OriginalCode))
            {
                int count = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string logocode = line.Substring(0, 4);
                    string xaxis = line.Substring(4, 4);
                    string yaxis = line.Substring(8, 4);
                    string xsize = line.Substring(12, 4);
                    string ysize = line.Substring(16, 4);
                    string rotation = line.Substring(20, 4);
                    string color = line.Substring(24, 8);
                    string layertype = line.Substring(32, 2);
                    string mirror = line.Substring(34, 2);

                    int NewXaxis;
                    int NewYaxis;
                    int NewXsize;
                    int NewYsize;


                    int dec_xaxis = int.Parse(xaxis, System.Globalization.NumberStyles.HexNumber);
                    int dec_yaxis = int.Parse(yaxis, System.Globalization.NumberStyles.HexNumber);
                    int dec_xsize = int.Parse(xsize, System.Globalization.NumberStyles.HexNumber);
                    int dec_ysize = int.Parse(ysize, System.Globalization.NumberStyles.HexNumber);

                    if (dec_xaxis > 2000)
                    {
                        dec_xaxis -= 65535;
                    }

                    if (dec_yaxis > 2000)
                    {
                        dec_yaxis -= 65535;
                    }

                    if (dec_xsize > 2000)
                    {
                        dec_xsize -= 65535;
                    }

                    if (dec_ysize > 2000)
                    {
                        dec_ysize -= 65535;
                    }

                    if(scalemod != 1)
                    {
                        NewXsize = Convert.ToInt32(Math.Round(dec_xsize * scalemod));
                    }
                    else
                    {
                        NewXsize = dec_xsize;
                    }
                    
                    if(scalemody != 1)
                    {
                        NewYsize = Convert.ToInt32(Math.Round(dec_ysize * scalemod));
                    }
                    else
                    {
                        NewYsize = dec_ysize;
                    }

                    //System.Diagnostics.Debug.Write("x size: (" + dec_xsize + ") (" + scalemod + ") = " + NewXsize + " y size: (" + dec_ysize + ") (" + scalemod + ") = " +  NewYsize + Environment.NewLine);

                    int xscale_dif = Convert.ToInt32(Math.Round((Convert.ToInt32(center.X) - dec_xaxis) * scalemod));
                    NewXaxis = Convert.ToInt32(center.X) + (xscale_dif * -1);

                    int yscale_dif = Convert.ToInt32(Math.Round((Convert.ToInt32(center.Y) - dec_yaxis) * scalemody));
                    NewYaxis = Convert.ToInt32(center.Y) + (yscale_dif * -1);

                    if (NewXaxis < 0)
                    {
                        NewXaxis += 65535;
                    }

                    if (NewYaxis < 0)
                    {
                        NewYaxis += 65535;
                    }

                    if (NewXaxis >= 65535)
                    {
                        NewXaxis -= 65535;
                    }

                    if (NewYaxis >= 65535)
                    {
                        NewYaxis -= 65535;
                    }

                    if (NewXsize < 0)
                    {
                        NewXsize += 65535;
                    }

                    if (NewYsize < 0)
                    {
                        NewYsize += 65535;
                    }

                    if (NewXsize >= 65535)
                    {
                        NewXsize -= 65535;
                    }

                    if (NewYsize >= 65535)
                    {
                        NewYsize -= 65535;
                    }


                    string hex_newxaxis = NewXaxis.ToString("x").ToUpperInvariant();
                    while (hex_newxaxis.Length < 4)
                    {
                        string fix = "0" + hex_newxaxis;
                        hex_newxaxis = fix;
                    }

                    string hex_newyaxis = NewYaxis.ToString("x").ToUpperInvariant();
                    while (hex_newyaxis.Length < 4)
                    {
                        string fix = "0" + hex_newyaxis;
                        hex_newyaxis = fix;
                    }

                    string hex_newxsize = NewXsize.ToString("x").ToUpperInvariant();
                    while (hex_newxsize.Length < 4)
                    {
                        string fix = "0" + hex_newxsize;
                        hex_newxsize = fix;
                    }

                    string hex_newysize = NewYsize.ToString("x").ToUpperInvariant();
                    while (hex_newysize.Length < 4)
                    {
                        string fix = "0" + hex_newysize;
                        hex_newysize = fix;
                    }

                    if (hex_newxsize.Length > 4 || hex_newysize.Length > 4 || hex_newxaxis.Length > 4 || hex_newyaxis.Length > 4)
                    {
                        if (hex_newxsize.Length > 4)
                        {
                            try
                            {
                                hex_newxsize = hex_newxsize.Substring(4, 4);
                                if (hex_newxsize.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "xsize value: " + hex_newxsize, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "xsize value: " + hex_newxsize, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                        }
                        if (hex_newysize.Length > 4)
                        {
                            try
                            {
                                hex_newysize = hex_newysize.Substring(4, 4);
                                if (hex_newysize.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "ysize value: " + hex_newysize, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "ysize value: " + hex_newysize, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                        }
                        if (hex_newxaxis.Length > 4)
                        {
                            try
                            {
                                hex_newxaxis = hex_newxaxis.Substring(4, 4);
                                if (hex_newxaxis.Length > 4)
                                {
                                    MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "x axis value: " + hex_newxaxis, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("There was an error parsing a value. \n" + "line: " + count + "\n" + "x axis value: " + hex_newxaxis, "Parser Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }

                    string EditedCode = logocode + hex_newxaxis + hex_newyaxis + hex_newxsize + hex_newysize + rotation + color + layertype + mirror + Environment.NewLine;
                    tb_Output.Text += EditedCode;
                    count += 1;
                }
            }
        }

        private void B_words_Click(object sender, RoutedEventArgs e)
        {
            string s = tb_words.Text;
            Int16 size;
            Int16 xco = -1;

            try
            {
                size = Convert.ToInt16(tb_wordssize.Text);
            }
            catch
            {
                return;
            }

            for (int i = 0; i < s.Length; i++)
            {
                string line;
                string logocode = String.Empty;
                string logocode1 = String.Empty;
                string logocode2 = String.Empty;

                line = s.Substring(i, 1).ToUpperInvariant();

                System.Diagnostics.Debug.Write(line + Environment.NewLine);

                if (xco == -1)
                {
                    xco += 1;
                }

                try
                {
                    if (line.Contains("0"))
                    {
                        logocode = "03E9";
                        logocode1 = "040F";
                        logocode2 = "0434";
                    }
                    if (line.Contains("1"))
                    {
                        logocode = "03EA";
                        logocode1 = "0410";
                        logocode2 = "0435";
                    }
                    if (line.Contains("2"))
                    {
                        logocode = "03EB";
                        logocode1 = "0411";
                        logocode2 = "0436";
                    }
                    if (line.Contains("3"))
                    {
                        logocode = "03EC";
                        logocode1 = "0412";
                        logocode2 = "0437";
                    }
                    if (line.Contains("4"))
                    {
                        logocode = "03ED";
                        logocode1 = "0413";
                        logocode2 = "0438";
                    }
                    if (line.Contains("5"))
                    {
                        logocode = "03EE";
                        logocode1 = "0414";
                        logocode2 = "0439";
                    }
                    if (line.Contains("6"))
                    {
                        logocode = "03EF";
                        logocode1 = "0415";
                        logocode2 = "043A";
                    }
                    if (line.Contains("7"))
                    {
                        logocode = "03F0";
                        logocode1 = "0416";
                        logocode2 = "043B";
                    }
                    if (line.Contains("8"))
                    {
                        logocode = "03F1";
                        logocode1 = "0417";
                        logocode2 = "043C";
                    }
                    if (line.Contains("9"))
                    {
                        logocode = "03F2";
                        logocode1 = "0418";
                        logocode2 = "043D";
                    }
                    if (line.Contains("A"))
                    {
                        logocode = "03F3";
                        logocode1 = "0419";
                        logocode2 = "043E";
                    }
                    if (line.Contains("B"))
                    {
                        logocode = "03F4";
                        logocode1 = "041A";
                        logocode2 = "043F";
                    }
                    if (line.Contains("C"))
                    {
                        logocode = "03F5";
                        logocode1 = "041B";
                        logocode2 = "0440";
                    }
                    if (line.Contains("D"))
                    {
                        logocode = "03F6";
                        logocode1 = "041C";
                        logocode2 = "0441";
                    }
                    if (line.Contains("E"))
                    {
                        logocode = "03F7";
                        logocode1 = "041D";
                        logocode2 = "0442";
                    }
                    if (line.Contains("F"))
                    {
                        logocode = "03F8";
                        logocode1 = "041E";
                        logocode2 = "0443";
                    }
                    if (line.Contains("G"))
                    {
                        logocode = "03F9";
                        logocode1 = "041F";
                        logocode2 = "0444";
                    }
                    if (line.Contains("H"))
                    {
                        logocode = "03FA";
                        logocode1 = "0420";
                        logocode2 = "0445";
                    }
                    if (line.Contains("I"))
                    {
                        logocode = "03FB";
                        logocode1 = "0421";
                        logocode2 = "0446";
                    }
                    if (line.Contains("J"))
                    {
                        logocode = "03FC";
                        logocode1 = "0422";
                        logocode2 = "0447";
                    }
                    if (line.Contains("K"))
                    {
                        logocode = "03FD";
                        logocode1 = "0423";
                        logocode2 = "0448";
                    }
                    if (line.Contains("L"))
                    {
                        logocode = "03FE";
                        logocode1 = "0424";
                        logocode2 = "0449";
                    }
                    if (line.Contains("M"))
                    {
                        logocode = "03FF";
                        logocode1 = "0425";
                        logocode2 = "044A";
                    }
                    if (line.Contains("N"))
                    {
                        logocode = "0400";
                        logocode1 = "0426";
                        logocode2 = "044B";
                    }
                    if (line.Contains("O"))
                    {
                        logocode = "0401";
                        logocode1 = "0427";
                        logocode2 = "044C";
                    }
                    if (line.Contains("P"))
                    {
                        logocode = "0402";
                        logocode1 = "0428";
                        logocode2 = "044D";
                    }
                    if (line.Contains("Q"))
                    {
                        logocode = "0403";
                        logocode1 = "0429";
                        logocode2 = "044E";
                    }
                    if (line.Contains("R"))
                    {
                        logocode = "0404";
                        logocode1 = "042A";
                        logocode2 = "044F";
                    }
                    if (line.Contains("S"))
                    {
                        logocode = "0405";
                        logocode1 = "042B";
                        logocode2 = "0450";
                    }
                    if (line.Contains("T"))
                    {
                        logocode = "0406";
                        logocode1 = "042C";
                        logocode2 = "0451";
                    }
                    if (line.Contains("U"))
                    {
                        logocode = "0407";
                        logocode1 = "042D";
                        logocode2 = "0452";
                    }
                    if (line.Contains("V"))
                    {
                        logocode = "0408";
                        logocode1 = "042E";
                        logocode2 = "0453";
                    }
                    if (line.Contains("W"))
                    {
                        logocode = "0409";
                        logocode1 = "042F";
                        logocode2 = "0454";
                    }
                    if (line.Contains("X"))
                    {
                        logocode = "040A";
                        logocode1 = "0430";
                        logocode2 = "0455";
                    }
                    if (line.Contains("Y"))
                    {
                        logocode = "040B";
                        logocode1 = "0431";
                        logocode2 = "0456";
                    }
                    if (line.Contains("Z"))
                    {
                        logocode = "040C";
                        logocode1 = "045A";
                        logocode2 = "0457";
                    }
                    if (line.Contains("!"))
                    {
                        logocode = "040D";
                        logocode1 = "0432";
                        logocode2 = "0458";
                    }
                    if (line.Contains("?"))
                    {
                        logocode = "040E";
                        logocode1 = "0433";
                        logocode2 = "0459";
                    }
                    if (line.Contains(" "))
                    {
                        xco += size;
                        logocode = "none";
                        logocode1 = "none";
                        logocode2 = "none";
                    }
                    if (line.Contains("."))
                    {
                        xco += size;
                        logocode = "none";
                        logocode1 = "none";
                        logocode2 = "none";
                    }
                    if (line.Contains("-"))
                    {
                        xco += size;
                        logocode = "none";
                        logocode1 = "none";
                        logocode2 = "none";
                    }
                }
                catch
                {
                    return;
                }

                string hex_size = size.ToString("x").ToUpperInvariant();
                if (hex_size.Length < 4)
                {
                    while(hex_size.Length < 4)
                    {
                        string fix = "0" + hex_size;
                        hex_size = fix;
                    }
                }

                string hex_xco = (size * i).ToString("x").ToUpperInvariant();
                if (hex_xco.Length < 4)
                {
                    while (hex_xco.Length < 4)
                    {
                        string fix = "0" + hex_xco;
                        hex_xco = fix;
                    }
                }

                if (logocode != "none")
                {
                    if(cb_textcoloroutline.IsChecked == true)
                    {
                        string outcode = logocode1 + hex_xco + "0000" + hex_size + hex_size + "0000000000FF0001" + Environment.NewLine;

                        tb_Output.Text += outcode;
                    }

                    if (cb_textblackoutline.IsChecked == true)
                    {
                        string outcode = logocode2 + hex_xco + "0000" + hex_size + hex_size + "0000000000FF0001" + Environment.NewLine;

                        tb_Output.Text += outcode;
                    }

                    if (cb_textnooutline.IsChecked == true)
                    {
                        string outcode = logocode + hex_xco + "0000" + hex_size + hex_size + "0000000000FF0001" + Environment.NewLine;

                        tb_Output.Text += outcode;
                    }
                }
                
            }
        }

        private void B_transforms_Click(object sender, RoutedEventArgs e)
        {
            if(gb_transforms.Visibility == Visibility.Visible)
            {
                b_transforms.Background = Brushes.LightGray;
                gb_transforms.Visibility = Visibility.Hidden;
                return;
            }

            b_transforms.Background = Brushes.White;
            b_scaleandrotate.Background = Brushes.LightGray;
            b_outlineandtext.Background = Brushes.LightGray;
            b_automove.Background = Brushes.LightGray;

            gb_transforms.Visibility = Visibility.Visible;
            gb_scale.Visibility = Visibility.Hidden;
            gb_text.Visibility = Visibility.Hidden;
            gb_automove.Visibility = Visibility.Hidden;
        }

        private void B_scaleandrotate_Click(object sender, RoutedEventArgs e)
        {
            if (gb_scale.Visibility == Visibility.Visible)
            {
                b_scaleandrotate.Background = Brushes.LightGray;
                gb_scale.Visibility = Visibility.Hidden;
                return;
            }

            b_transforms.Background = Brushes.LightGray;
            b_scaleandrotate.Background = Brushes.White;
            b_outlineandtext.Background = Brushes.LightGray;
            b_automove.Background = Brushes.LightGray;

            gb_transforms.Visibility = Visibility.Hidden;
            gb_scale.Visibility = Visibility.Visible;
            gb_text.Visibility = Visibility.Hidden;
            gb_automove.Visibility = Visibility.Hidden;
        }

        private void B_outlineandtext_Click(object sender, RoutedEventArgs e)
        {
            if (gb_text.Visibility == Visibility.Visible)
            {
                b_outlineandtext.Background = Brushes.LightGray;
                gb_text.Visibility = Visibility.Hidden;
                return;
            }

            b_transforms.Background = Brushes.LightGray;
            b_scaleandrotate.Background = Brushes.LightGray;
            b_outlineandtext.Background = Brushes.White;
            b_automove.Background = Brushes.LightGray;

            gb_automove.Visibility = Visibility.Hidden;
            gb_transforms.Visibility = Visibility.Hidden;
            gb_scale.Visibility = Visibility.Hidden;
            gb_text.Visibility = Visibility.Visible;
        }

        private void B_automove_Click(object sender, RoutedEventArgs e)
        {
            if (gb_text.Visibility == Visibility.Visible)
            {
                b_automove.Background = Brushes.LightGray;
                gb_automove.Visibility = Visibility.Hidden;
                return;
            }

            b_transforms.Background = Brushes.LightGray;
            b_scaleandrotate.Background = Brushes.LightGray;
            b_outlineandtext.Background = Brushes.LightGray;
            b_automove.Background = Brushes.White;

            gb_transforms.Visibility = Visibility.Hidden;
            gb_scale.Visibility = Visibility.Hidden;
            gb_text.Visibility = Visibility.Hidden;
            gb_automove.Visibility = Visibility.Visible;
        }

        private void Combo_language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combo_language.SelectedIndex == 0)
            {
                l_inputcode.Content = "Input Code";
                l_outputcode.Content = "Output Code";
                l_transheader1.Content = "Move Layers";
                l_transheader2.Content = "Size Individual Layers";
                l_transheader3.Content = "Change Color of Layers";
                l_transheader4.Content = "Change Mirror Status of Layers";
                l_transxcoord.Content = "X-Axis amount to move.";
                l_transycoord.Content = "Y-Axis amount to move.";
                l_transxsize.Content = "X-Size without scaling.";
                l_transysize.Content = "Y-Size without scaling.";
                l_transcolor.Content = "RRGGBBAA format.";
                l_transmirror.Content = "Mirror Status.";
                l_sandrheader1.Content = "Scale the Layers";
                l_sandrheader2.Content = "Rotate the Layers";
                l_textoheader1.Content = "Outline Layers";
                l_textoheader2.Content = "Text to Layers";
                l_outlinedisc.Text = "This will outline the layers. Only use with a low amount of layers as it will raise layers by 5 times";
                l_outlinewidth.Content = "Outline Width";
                l_outlinecolor.Content = "Outline Color";
                l_textsize.Content = "Size";
                l_textcontent.Content = "Text";
                l_textblackoutline.Text = "Black Outline Color Base";
                l_textcoloroutline.Text = "Color Outline Black Base";
                b_tb_scaleandrotate.Text = "Scale and Rotate";
                b_tb_outlineandtext.Text = "Outline and Text Maker";
                b_copytoclipboard.Content = "Copy To Clipboard";
                b_ResetButton.Content = "Reset";
                b_copytoinput.Content = "Copy";
                gb_transforms.Header = "Transforms Menu";
                b_ApplyEdits.Content = "Apply";
                gb_scale.Header = "Scale and Rotate Menu";
                b_scale.Content = "Apply";
                b_Rotate90.Content = "Apply";
                gb_text.Header = "Text Creator and and Outline Menu";
                b_MakeOutline.Content = "Outline";
                cb_textnooutline.Content = "No Outline";
                b_words.Content = "Create";
                b_transforms.Content = "Transforms";
                l_copyeditedcode.Text = "Copy Output to Input for editing. (This will delete input.Save it before use)";
                l_rotatedisc.Text = "Rotate the layers in degrees. (Please keep them between - 360 and 360 degrees.)";
                b_Save.Content = "Save";
                b_Loadfile.Content = "Load";
                l_scaledisc.Text = "(Scale the logo. Use either a multiplier or a percentage, both will work.)";
                b_Help.Content = "Help";
                l_help1.Content = "Help File Information";
            }

            if (combo_language.SelectedIndex == 1)
            {
                l_inputcode.Content = "開始コード";
                l_outputcode.Content = "産出コード";
                l_transheader1.Content = "レイヤーを移動する";
                l_transheader2.Content = "アイテムの個々のサイズを変更する";
                l_transheader3.Content = "色を変える";
                l_transheader4.Content = "反対側の外観を変更する";
                l_transxcoord.Content = "変換する「x-軸」量";
                l_transycoord.Content = "変換する「y-軸」量";
                l_transxsize.Content = "x 方向にスケーリングしないサイズ";
                l_transysize.Content = "y 方向にスケーリングしないサイズ";
                l_transcolor.Content = "RRGGBBAA 形式";
                l_transmirror.Content = "反対側のステータス";
                l_sandrheader1.Content = "画像サイズを拡大縮小する";
                l_sandrheader2.Content = "画像を回転させる";
                l_textoheader1.Content = "アウトラインアイテム";
                l_textoheader2.Content = "アイテムへのテキスト";
                l_outlinedisc.Text = "これにより、レイヤーの概要が表示されます。レイヤーを元の5倍上げるので、レイヤーの量が少ない場合にのみ使用します";
                l_outlinewidth.Content = "アウトライン幅";
                l_outlinecolor.Content = "アウトラインの色";
                l_textsize.Content = "サイズ";
                l_textcontent.Content = "言葉";
                l_textblackoutline.Text = "カラーベースと黒のアウトライン";
                l_textcoloroutline.Text = "黒ベースのカラーアウトライン";
                b_tb_scaleandrotate.Text = "アイテムの拡大縮小と回転";
                b_tb_outlineandtext.Text = "アウトラインおよびテキスト作成者";
                b_copytoclipboard.Content = "クリップボードにコピー";
                b_ResetButton.Content = "セットをしなおす";
                b_copytoinput.Content = "複写";
                gb_transforms.Header = "変換メニュー";
                b_ApplyEdits.Content = "アプライ";
                gb_scale.Header = "メニューのサイズ変更と回転";
                b_scale.Content = "アプライ";
                b_Rotate90.Content = "アプライ";
                gb_text.Header = "テキストクリエーターとアウトラインメニュー";
                b_MakeOutline.Content = "囲む";
                cb_textnooutline.Content = "アウトラインなし";
                b_words.Content = "作る";
                b_transforms.Content = "変ずる";
                l_copyeditedcode.Text = "編集用に出力を入力にコピーします。 （これにより入力が削除されます。使用する前に保存してください）";
                l_rotatedisc.Text = "レイヤーを度単位で回転させます。 （-360〜360度の範囲で保管してください。)";
                b_Loadfile.Content = "開ける";
                b_Save.Content = "溜める";
                l_scaledisc.Text = "（ロゴを拡大縮小します。乗数またはパーセンテージを使用します。両方とも機能します";
                b_Help.Content = "助けて";
                l_help1.Content = "援助情報";
            }

            FillHelp();
        }

        private void B_copytoclipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(tb_Output.Text);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "FR Legends file (*.frl)|*.frl";
            if (save.ShowDialog() == true)
                File.WriteAllText(save.FileName, tb_Output.Text);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog();
            load.Filter = "FR Legends file (*.frl)|*.frl";
            if (load.ShowDialog() == true)
            {
                var fileStream = load.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    tb_Input.Text = reader.ReadToEnd();
                }
            }
        }

        private void FillHelp()
        {
            if(combo_language.SelectedIndex == 0)
            {
                WebRequest request = WebRequest.Create("https://raw.githubusercontent.com/13DM/SitePictures/master/frhelp");
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader sr = new StreamReader(data))
                {
                    html = sr.ReadToEnd();

                    l_help2.Text = html;

                }
            }

            if (combo_language.SelectedIndex == 1)
            {
                WebRequest request = WebRequest.Create("https://raw.githubusercontent.com/13DM/SitePictures/master/frhelpja");
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader sr = new StreamReader(data))
                {
                    html = sr.ReadToEnd();

                    l_help2.Text = html;

                }
            }
        }


        private void UpdateStatus()
        {
            WebRequest request = WebRequest.Create("https://raw.githubusercontent.com/13DM/SitePictures/master/frupdate");
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();

                if(!html.Contains("Up to date"))
                {
                    MessageBox.Show("There is an update available! Go to www.frlegendshub.com to get it!", "Update Available", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                l_update.Text = "Status: " + html;
            }
        }

        private void B_Help_Click(object sender, RoutedEventArgs e)
        {
            gb_help.Visibility = Visibility.Visible;
        }

        private void B_closehelp_Click(object sender, RoutedEventArgs e)
        {
            gb_help.Visibility = Visibility.Hidden;
        }

        private void B_automovestart_Click(object sender, RoutedEventArgs e)
        {
            string tempcode = String.Empty;
        }

        
    }
}
