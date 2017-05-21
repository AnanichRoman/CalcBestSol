using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graph = System.Windows.Forms.DataVisualization.Charting;


namespace MRO_L1
{
    public partial class Form1 : Form
    {

        double[,] arrval = new double[5, 101]; //abcde : 1..100
        double[] K2 = new double[101];
        double[] K1 = new double[101];
        int K1min = 0;
        int K2min = 0;
        double[] Kn2 = new double[101];
        double[] Kn1 = new double[101];
        int Kn1min = 0;
        int Kn2min = 0;
        double[] Ka2 = new double[101];
        double[] Ka1 = new double[101];
        int Ka1min = 0;
        int Ka2min = 0;
        double[] Km = new double[101];
        int Kmmin = 0;
        double[] KMosh = new double[101];
        int Kmoshmin = 0;
        double[] KGK = new double[101];
        double[] KinGK = new double[101];
        double[] Kname = new double[101];
        int Knamemin = 0;
        double k1max = 0;
        double k1min = 9000;
        double k2max = 0;
        double k2min = 9000;
        double[,,] byn= new double[101,101,2];
        double[] KB = new double[101];
        double[] progr = new double[101];
        int N;
        int[] KnO = new int[4] { 0, 0, 0, 0 };
        double[] KnO2 = new double[101];
        double[] KnO1 = new double[101];

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* MSScriptControl.ScriptControl sc = new MSScriptControl.ScriptControl();
             sc.Language = "VBScript";
             string expression = textBox1.Text;
             int x = 15;
             object result = sc.Eval(expression);
             MessageBox.Show(result.ToString());*/



            MessageBox.Show(GK_min.ToString());
            MessageBox.Show(GK_max.ToString());


            N=Convert.ToInt32(t_N.Text);

            for (int i = 0; i < 100; i++)
            {
                KMosh[i] = 0;
                KGK[i] = 0;
                KB[i] = 0;
                Kname[i] = 1;
            }

            double[] min, max;

            min = new double[5];
            max = new double[5];

            min[0] = Convert.ToDouble(a1.Text);
            min[1] = Convert.ToDouble(b1.Text);
            min[2] = Convert.ToDouble(c1.Text);
            min[3] = Convert.ToDouble(d1.Text);
            min[4] = Convert.ToDouble(e1.Text);

            max[0] = Convert.ToDouble(a2.Text);
            max[1] = Convert.ToDouble(b2.Text);
            max[2] = Convert.ToDouble(c2.Text);
            max[3] = Convert.ToDouble(d2.Text);
            max[4] = Convert.ToDouble(e2.Text);


            progr[0] = (Convert.ToDouble(a2.Text) - Convert.ToDouble(a1.Text)) / N;
            progr[1] = (Convert.ToDouble(b2.Text) - Convert.ToDouble(b1.Text)) / N;
            progr[2] = (Convert.ToDouble(c2.Text) - Convert.ToDouble(c1.Text)) / N;
            progr[3] = (Convert.ToDouble(d2.Text) - Convert.ToDouble(d1.Text)) / N;
            progr[4] = (Convert.ToDouble(e2.Text) - Convert.ToDouble(e1.Text)) / N;

            arrval[0, 0] = Convert.ToDouble(a1.Text);
            arrval[1, 0] = Convert.ToDouble(b1.Text);
            arrval[2, 0] = Convert.ToDouble(c1.Text);
            arrval[3, 0] = Convert.ToDouble(d1.Text);
            arrval[4, 0] = Convert.ToDouble(e1.Text);
            

            for (int i = 0; i < 4; i++) //abcde
            {
                double tmp=arrval[i, 0];
                for (int j = 1; j < N;j++ )
                {
                    
                    arrval[i, j] = tmp;
                    tmp += progr[i];
                    
                }
            }

            //TRASH
            NCalc.Expression expression1 = new NCalc.Expression(F1.Text);
            NCalc.Expression expression2 = new NCalc.Expression(F2.Text);
            //newtrash
            for (int f = 0; f < N; f++)
            {
               

                //1st
                if (F1.Text.IndexOf("a") != -1)
                {

                    expression1.Parameters["a"] = arrval[0,f];
                }
                if (F1.Text.IndexOf("b") != -1)
                {

                    expression1.Parameters["b"] = arrval[1, f];
                }
                if (F1.Text.IndexOf("c") != -1)
                {

                    expression1.Parameters["c"] = arrval[2, f];
                }
                if (F1.Text.IndexOf("d") != -1)
                {

                    expression1.Parameters["d"] = arrval[3, f];
                }
                if (F1.Text.IndexOf("e") != -1)
                {

                    expression1.Parameters["e"] = arrval[4, f];
                }
              
                //2nd
                if (F2.Text.IndexOf("a") != -1)
                {

                    expression2.Parameters["a"] = arrval[0, f];
                }
                if (F2.Text.IndexOf("b") != -1)
                {

                    expression2.Parameters["b"] = arrval[1, f];
                }
                if (F2.Text.IndexOf("c") != -1)
                {

                    expression2.Parameters["c"] = arrval[2, f];
                }
                if (F2.Text.IndexOf("d") != -1)
                {

                    expression2.Parameters["d"] = arrval[3, f];
                }
                if (F2.Text.IndexOf("e") != -1)
                {

                    expression2.Parameters["e"] = arrval[4, f];
                }

                double value1;

                double.TryParse(expression1.Evaluate().ToString(), out value1);

                double value2;

                double.TryParse(expression2.Evaluate().ToString(), out value2);

                K1[f] = value1;
                K2[f] = value2;
                
                //KN
                
                for (int i = 0; i < N; i++)
                {
                    if (k1max < K1[i]) k1max = K1[i];
                    if (k1min > K1[i]) k1min = K1[i];
                    if (k2max < K2[i]) k2max = K2[i];
                    if (k2min > K2[i]) k2min = K2[i];
                }

                
                for (int i = 0; i < N; i++)
                {
                    Kn1[i] = (K1[i] - k1min) / (k1max - k1min);
                    Kn2[i] = (K2[i] - k2min) / (k2max - k2min);
                }

                //OBOBSH
                if((c_K1.Checked==true)&&(c_Ob.Checked==false))
                {
                    KnO[0]=1;
                    for (int i = 0; i < N; i++)
                    {
                        KnO1[i] = 1 - Kn1[i];
                    }

                }

                if ((c_K1.Checked == false) && (c_Ob.Checked == true))
                {
                    KnO[1] = 1;
                    for (int i = 0; i < N; i++)
                    {
                        KnO1[i] = 1 - Kn1[i];
                    }

                }

                if ((c_K2.Checked == false) && (c_Ob.Checked == true))
                {
                    KnO[2] = 1;
                    for (int i = 0; i < N; i++)
                    {
                        KnO2[i] = 1 - Kn2[i];
                    }

                }
              
                if ((c_K2.Checked == false) && (c_Ob.Checked == true))
                {
                    KnO[3] = 1;
                    for (int i = 0; i < N; i++)
                    {
                        KnO2[i] = 1 - Kn2[i];
                    }

                }
                // adekt
                for (int i = 0; i < N; i++)
                {
                    Ka1[i] = Kn1[i] * Convert.ToDouble(numericUpDown1.Value);
                    Ka2[i] = Kn2[i] * Convert.ToDouble(numericUpDown2.Value);
                }

                // mult
                for (int i = 0; i < N; i++)
                {
                    Km[i] = Kn1[i] * Convert.ToDouble(numericUpDown1.Value) * Kn2[i] * Convert.ToDouble(numericUpDown2.Value);
                }
              

                // GK

                double giver = 0;

                if (c_GK.Checked == true) // CH - ON
                {
                    double mn = Convert.ToDouble(GK_min.Text);
                    double mx = Convert.ToDouble(GK_max.Text);
                    double[,] storage = new double[N,2];
                    int j=0;
                    double minval = 9000000000;
                    int pos=0;

                    for (int i = 0; i < N; i++)
                    {
                        if (Kn2[i] > mn)
                        {
                            if (Kn2[i] < mx)
                            {
                                storage[j, 0] = i;
                                storage[j, 1] = Kn2[i];
                                j++;
                            }
                        }
                    }

                    for (int i = 0; i < j; i++)
                    {
                        if (storage[i, 1] < minval)
                        {
                            minval = storage[i, 1];
                            pos = Convert.ToInt32(storage[i,0]);
                        }
                    }
                    KGK[pos] = K1[pos];


                }
                if (c_GK.Checked == false) //ch - offf
                {
                    double mn = Convert.ToDouble(GK_min.Text);
                    double mx = Convert.ToDouble(GK_max.Text);
                    double[,] storage = new double[N, 2];
                    int j = 0;
                    double minval = 9000000000;
                    int pos = 0;

                    for (int i = 0; i < N; i++)
                    {
                        if (Kn1[i] > mn)
                        {
                            if (Kn1[i] < mx)
                            {
                                storage[j, 0] = i;
                                storage[j, 1] = Kn1[i];
                                j++;
                            }
                        }
                    }

                    for (int i = 0; i < j; i++)
                    {
                        if (storage[i, 1] < minval)
                        {
                            minval = storage[i, 1];
                            pos = Convert.ToInt32(storage[i, 0]);
                        }
                    }
                    KGK[pos] = K2[pos];
                }


               
                // KB


                if ((c_K1.Checked == true) && (c_K2.Checked == false))//T F
                {
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (i == j) byn[i, j, 0] = 666;
                            else
                            {
                                if ((K1[i] > K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 2;
                                    byn[i, j, 1] = 0;
                              
                                }
                                if ((K1[i] < K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 1;
                                    
                                }
                                if ((K1[i] > K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 1;
                                   
                                }
                                if ((K1[i] < K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 0;
                                    byn[i, j, 1] = 2;
                                
                                }
                                if ((K1[i] == K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 0;

                                }
                                if ((K1[i] == K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 1;
                                }
                                if ((K1[i] > K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 0.5;
                                }
                                if ((K1[i] < K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 0;
                                    byn[i, j, 1] = 0.5;
                              
                                }
                                if ((K1[i] == K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 0.5;
                                }
                            }
                        }
                    }
                }


                if ((c_K1.Checked == false) && (c_K2.Checked == false))//dw dw
                {
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (i == j) byn[i, j, 0] = 666;
                            else
                            {
                                if ((K1[i] > K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 1;

                                }
                                if ((K1[i] < K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 2;
                                    byn[i, j, 1] = 0;
                                   
                                }
                                if ((K1[i] > K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 0;
                                    byn[i, j, 1] = 2;
                                   
                                }
                                if ((K1[i] < K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 1;

                                }
                                if ((K1[i] == K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 0;

                                }
                                if ((K1[i] == K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 1;
                                }
                                if ((K1[i] > K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 0;
                                    byn[i, j, 1] = 0.5;
                                 
                                }
                                if ((K1[i] < K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 0.5;
                                }
                                if ((K1[i] == K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 0.5;
                                }
                            }
                        }
                    }
                }

                if ((c_K1.Checked == false) && (c_K2.Checked == true)) //dw up
                {
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (i == j) byn[i, j, 0] = 666;
                            else
                            {
                                if ((K1[i] > K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 0;
                                    byn[i, j, 1] = 2;
                                   
                                    
                                }
                                if ((K1[i] < K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 1;

                                }
                                if ((K1[i] > K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 1;

                                }
                                if ((K1[i] < K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 2;
                                    byn[i, j, 1] = 0;
                                    
                                }
                                if ((K1[i] == K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 1;

                                }
                                if ((K1[i] == K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 0;
                                }
                                if ((K1[i] > K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 0;
                                    byn[i, j, 1] = 0.5;
                                   
                                }
                                if ((K1[i] < K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 0.5;
                                }
                                if ((K1[i] == K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 0.5;
                                }
                            }
                        }
                    }
                }



              

              

                if ((c_K1.Checked == true) && (c_K2.Checked == true))//up up < <
                {
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (i == j) byn[i, j, 0] = 666;
                            else
                            {
                                if ((K1[i] < K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 1;
                                   
                                }
                                if ((K1[i] < K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 0;
                                    byn[i, j, 1] = 2;
                                   // Kname[i] = 0;

                                }
                                if ((K1[i] > K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 2;
                                    byn[i, j, 1] = 0;
                                   // Kname[i] = 1;
                                }
                                if ((K1[i] < K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 1;
                                    
                                }
                                if ((K1[i] == K1[j]) && (K2[i] > K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 1;

                                }
                                if ((K1[i] == K1[j]) && (K2[i] < K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 0;
                                }
                                if ((K1[i] > K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 1;
                                    byn[i, j, 1] = 0.5;
                                }
                                if ((K1[i] < K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 0;
                                    byn[i, j, 1] = 0.5;
                                    //Kname[i] = 0;
                                }
                                if ((K1[i] == K1[j]) && (K2[i] == K2[j]))
                                {
                                    byn[i, j, 0] = 0.5;
                                    byn[i, j, 1] = 0.5;
                                }
                            }
                        }
                    }
                }

                ///////////////////////////////////////////////////////____HERE_______\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                
                    for (int i = 0; i <N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (byn[i, j, 0] == 0)
                            {
                                Kname[i] = 0;
                                
                                break;
                            }
                        }
                    }


                    int countz = 0;
                    for (int i = 0; i < N; i++)
                    {
                        if (Kname[i] == 1) countz++;
                    }
                    Knamemin = countz;
                    // mosh

                    double gkmin = 90000000000;

                    if ((c_Mo1.Checked == true) && (c_Mo2.Checked == false))//up dw
                    {
                        for (int i = 0; i < N; i++)
                        {
                            double T1 = K1[i];
                            double T2 = K2[i];
                            double count = 0;
                            if (Kname[i] == 1)
                            {
                                for (int j = 0; j < N; j++)
                                {
                                    if (i != j)
                                    {
                                        if ((T1 > K1[j]) && (T2 < K2[j])) count++;
                                    }
                                }
                                KMosh[i] = count - Knamemin;
                                if (KMosh[i] < 0) KMosh[i] = 0;
                            }
                        }
                    }

                    if ((c_Mo1.Checked == true) && (c_Mo2.Checked == true))//up up
                    {
                        for (int i = 0; i < N; i++)
                        {
                            double T1 = K1[i];
                            double T2 = K2[i];
                            double count = 0;
                            if (Kname[i] == 1)
                            {
                                for (int j = 0; j < N; j++)
                                {
                                    if (i != j)
                                    {
                                        if ((T1 > K1[j]) && (T2 > K2[j])) count++;
                                    }
                                }
                                KMosh[i] = count - Knamemin;
                                if (KMosh[i] < 0) KMosh[i] = 0;
                            }
                        }
                    }

                    if ((c_Mo1.Checked == false) && (c_Mo2.Checked == true))//dw up
                    {
                        for (int i = 0; i < N; i++)
                        {
                            double T1 = K1[i];
                            double T2 = K2[i];
                            double count = 0;
                            if (Kname[i] == 1)
                            {
                                for (int j = 0; j < N; j++)
                                {
                                    if (i != j)
                                    {
                                        if ((T1 < K1[j]) && (T2 > K2[j])) count++;
                                    }
                                }
                                KMosh[i] = count - Knamemin;
                                if (KMosh[i] < 0) KMosh[i] = 0;
                            }
                        }
                    }

                    if ((c_Mo1.Checked == false) && (c_Mo2.Checked == false))//dw dw
                    {
                        for (int i = 0; i < N; i++)
                        {
                            double T1 = K1[i];
                            double T2 = K2[i];
                            double count = 0;
                            if (Kname[i] == 1)
                            {
                                for (int j = 0; j < N; j++)
                                {
                                    if (i != j)
                                    {
                                        if ((T1 < K1[j]) && (T2 < K2[j])) count++;
                                    }
                                }
                                KMosh[i] = count - Knamemin;
                                if (KMosh[i] < 0) KMosh[i] = 0;
                            }
                        }
                    }
                //kname check

                ///////////////////////////////////////////////////////____HERE_______\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                //KB full
                    for (int i = 0; i < N; i++)
                    {
                        double tmp = 0;
                        for (int j = 0; j < N; j++)
                        {
                            if (byn[i, j, 0] != 666) tmp += byn[i, j, 0];
                        }
                        KB[i] = tmp;
                    }



                //// MINVALS1
                double minnimal =9999999999999;
                for (int i = 0; i < N; i++)
                {
                    if (Kn1[i] < minnimal) { Kn1min = i; minnimal = Kn1[i]; }
                }
                //2
                minnimal = 9999999999999;
                for (int i = 0; i < N; i++)
                {
                    if (Kn2[i] < minnimal) { Kn2min = i; minnimal = Kn2[i]; }
                }
                //3
                minnimal = 9999999999999;
                for (int i = 0; i < N; i++)
                {
                    if (Ka1[i] < minnimal) { Ka1min = i; minnimal = Ka1[i]; }
                }
                //4
                minnimal = 9999999999999;
                for (int i = 0; i < N; i++)
                {
                    if (Ka2[i] < minnimal) { Ka2min = i; minnimal = Ka1[2]; }
                }
                //5
                minnimal = 0;
                for (int i = 0; i < N; i++)
                {
                    if (KMosh[i] > minnimal) { Kmoshmin = i; minnimal = KMosh[i]; }
                }

                //6
                minnimal = 99999999;
                for (int i = 0; i < N; i++)
                {
                    if (Km[i] < minnimal) { Kmmin = i; minnimal = Km[i]; }
                }

                //7
                minnimal = 99999999;
                for (int i = 0; i < N; i++)
                {
                    if (K1[i] < minnimal) { K1min = i; minnimal = K1[i]; }
                }

                //8
                minnimal = 99999999;
                for (int i = 0; i < N; i++)
                {
                    if (K2[i] < minnimal) { K2min = i; minnimal = K2[i]; }
                }

                //end
                
            }


            Ktable();
            Kchart();
            Kbyte();

        }

        public void Ktable()
        {
            Form form2 = new Form();
            form2.Size = new System.Drawing.Size(1100, 600);
            form2.Show();

            DataGridView table1 = new DataGridView();
            table1.Size = new System.Drawing.Size(1000, 500);
            table1.ScrollBars = ScrollBars.Both;
            table1.RowCount = N;
            table1.ColumnCount = 14;
            table1.Columns[0].HeaderText = "№";
            table1.Columns[1].HeaderText = "K1";
            table1.Columns[2].HeaderText = "K2";
            table1.Columns[3].HeaderText = "Name";
            table1.Columns[4].HeaderText = "K1N";
            table1.Columns[5].HeaderText = "K2N";
            table1.Columns[6].HeaderText = "K1A";
            table1.Columns[7].HeaderText = "K2A";
            table1.Columns[8].HeaderText = "mult";
            table1.Columns[9].HeaderText = "KM";
            table1.Columns[10].HeaderText = "GK";
            table1.Columns[11].HeaderText = "BynC";

            for (int i = 1; i < N; i++)
            {
                table1[0, i].Value = i ;
                table1[1, i].Value = K1[i];
                table1[2, i].Value = K2[i];

                table1[4, i].Value = Kn1[i];
                table1[5, i].Value = Kn2[i];
                table1[6, i].Value = Ka1[i];
                table1[7, i].Value = Ka2[i];
                table1[8, i].Value = Km[i];
                table1[9, i].Value = KMosh[i];
                table1[10, i].Value = KGK[i];
                table1[11, i].Value = KB[i];
               
                if (Kname[i] == 1) table1[3, i].Value = "K";
                else table1[3, i].Value = "C";
            }
            


            if (KnO[0] == 1)
            {
                table1.Columns[12].HeaderText = "K1No";
                for (int i = 0; i < N; i++)
                {
                    
                    table1[12, i].Value = KnO1[i];
                }
               
            }

            if (KnO[1] == 1)
            {
                table1.Columns[12].HeaderText = "K1No";
                for (int i = 0; i < N; i++)
                {

                    table1[12, i].Value = KnO1[i];
                }
              
            }

            if (KnO[2] == 1)
            {
                table1.Columns[13].HeaderText = "K2No";
                for (int i = 0; i < N; i++)
                {

                    table1[13, i].Value = KnO2[i];
                }
               
            }

            if (KnO[3] == 1)
            {
                table1.Columns[13].HeaderText = "K2No";
                for (int i = 0; i < N; i++)
                {

                    table1[13, i].Value = KnO2[i];
                }
              
            }

            table1[1, K1min].Style.BackColor = Color.Red;
            table1[2, K2min].Style.BackColor = Color.Red;
            table1[4, Kn1min].Style.BackColor = Color.Red;
            table1[5, Kn2min].Style.BackColor = Color.Red;
            table1[6, Ka1min].Style.BackColor = Color.Red;
            table1[7, Ka2min].Style.BackColor = Color.Red;
            table1[8, Kmmin].Style.BackColor = Color.Red;
            table1[9, Kmoshmin].Style.BackColor = Color.Red;
            
            form2.Controls.Add(table1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form2 = new Form();
            form2.Size = new System.Drawing.Size(1100, 600);
            form2.Show();

            DataGridView table1 = new DataGridView();
            table1.Size = new System.Drawing.Size(1000, 500);
            table1.ScrollBars = ScrollBars.Both;
            table1.RowCount = N;
            table1.ColumnCount = 12;
            table1.Columns[0].HeaderText = "№";
            table1.Columns[1].HeaderText = "K1";
            table1.Columns[2].HeaderText = "K2";
            table1.Columns[3].HeaderText = "Name";
            table1.Columns[4].HeaderText = "K1N";
            table1.Columns[5].HeaderText = "K2N";
            table1.Columns[6].HeaderText = "K1A";
            table1.Columns[7].HeaderText = "K2A";
            table1.Columns[8].HeaderText = "mult";
            table1.Columns[9].HeaderText = "KM";
            table1.Columns[10].HeaderText = "GK";
            table1.Columns[11].HeaderText = "BynC";
            
            for (int i = 0; i < N; i++)
            {
                table1[0, i].Value = i+1;
                table1[1, i].Value = K1[i];
                table1[2, i].Value = K2[i];

                table1[4, i].Value = Kn1[i];
                table1[5, i].Value = Kn2[i];
                table1[6, i].Value = Ka1[i];
                table1[7, i].Value = Ka2[i];
                table1[8, i].Value = Km[i];
                table1[9, i].Value = KMosh[i];
                table1[10, i].Value = KGK[i];
                table1[11, i].Value = KB[i];

                if (Kname[i] == 1) table1[3, i].Value = "K";
                if (Kname[i] == 0) table1[3, i].Value = "C";
            }

                table1[1, K1min].Style.BackColor = Color.Red;
                table1[2, K2min].Style.BackColor = Color.Red;
                table1[4, Kn1min].Style.BackColor = Color.Red;
                table1[5, Kn2min].Style.BackColor = Color.Red;
                table1[6, Ka1min].Style.BackColor = Color.Red;
                table1[7, Ka2min].Style.BackColor = Color.Red;
                table1[8, Kmmin].Style.BackColor = Color.Red;
                table1[8, Kmoshmin].Style.BackColor = Color.Red;
 
                
                form2.Controls.Add(table1);
        }

        public void Kchart()
        {
            Form form3 = new Form();
            form3.Size = new System.Drawing.Size(800, 800);
            form3.Show();



            Graph.Chart chart;

            chart = new Graph.Chart();
            chart.Location = new System.Drawing.Point(10, 10);
            chart.Size = new System.Drawing.Size(1000, 700);


            chart.ChartAreas.Add("draw");

            chart.ChartAreas["draw"].AxisX.Minimum = k1min;
            chart.ChartAreas["draw"].AxisX.Maximum = k1max;
            chart.ChartAreas["draw"].AxisX.Interval = (k1max - k1min) / 50;
            chart.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.White;
            chart.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Dash;


            chart.ChartAreas["draw"].AxisY.Minimum = k2min;
            chart.ChartAreas["draw"].AxisY.Maximum = k2max;
            chart.ChartAreas["draw"].AxisY.Interval = (k2max - k2min) / 50;
            chart.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.White;
            chart.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Dash;

            chart.ChartAreas["draw"].BackColor = Color.Black;


            chart.Series.Add("points");
            // Set the type to line      
            chart.Series["points"].ChartType = Graph.SeriesChartType.Point;
            chart.Series["points"].BorderWidth = 3;
            chart.Series["points"].Color = Color.Red;

            chart.Series["points"].LegendText = "x=K1;y=K2";
            // Create a new legend called "MyLegend".
            chart.Legends.Add("MyLegend");
            chart.Legends["MyLegend"].BorderColor = Color.Cyan; // I like tomato juice!

            for (int i = 0; i < N; i++)
            {
                chart.Series["points"].Points.AddXY(K1[i], K2[i]);
            }
            form3.Controls.Add(chart);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form3 = new Form();
            form3.Size = new System.Drawing.Size(800, 800);
            form3.Show();



            Graph.Chart chart;
            
            chart = new Graph.Chart();
            chart.Location = new System.Drawing.Point(10, 10);
            chart.Size = new System.Drawing.Size(1000, 700);
            

            chart.ChartAreas.Add("draw");

            chart.ChartAreas["draw"].AxisX.Minimum = k1min;
            chart.ChartAreas["draw"].AxisX.Maximum = k1max;
            chart.ChartAreas["draw"].AxisX.Interval = (k1max-k1min)/50;
            chart.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.White;
            chart.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Dash;


            chart.ChartAreas["draw"].AxisY.Minimum = k2min;
            chart.ChartAreas["draw"].AxisY.Maximum = k2max;
            chart.ChartAreas["draw"].AxisY.Interval = (k2max - k2min) / 50;
            chart.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.White;
            chart.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Dash;

            chart.ChartAreas["draw"].BackColor = Color.Black;


            chart.Series.Add("points");
            // Set the type to line      
            chart.Series["points"].ChartType = Graph.SeriesChartType.Point;
            chart.Series["points"].BorderWidth = 3; 
            chart.Series["points"].Color = Color.Red;

            chart.Series["points"].LegendText = "x=K1;y=K2";
            // Create a new legend called "MyLegend".
            chart.Legends.Add("MyLegend");
            chart.Legends["MyLegend"].BorderColor = Color.Cyan; // I like tomato juice!

            for (int i = 0; i<N; i++)
            {
                chart.Series["points"].Points.AddXY(K1[i],K2[i]);
            }
            form3.Controls.Add(chart);

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Value = 1 - numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = 1 - numericUpDown2.Value;
        }

        public void Kbyte()
        {
            Form form4 = new Form();
            form4.Size = new System.Drawing.Size(800, 620);
            form4.Show();

            DataGridView table3 = new DataGridView();
            table3.Size = new System.Drawing.Size(770, 570);
            table3.ScrollBars = ScrollBars.Both;
            table3.RowCount = N;
            table3.ColumnCount = N;

            for (int i = 0; i < N; i++)
            {
                table3.Columns[i].HeaderText = i.ToString();

            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (byn[i, j, 0] != 666)
                        table3[i, j].Value = byn[i, j, 0].ToString() + ";" + byn[i, j, 1].ToString();
                    else table3[i, j].Value = "---";
                }

            }

            form4.Controls.Add(table3);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form4 = new Form();
            form4.Size = new System.Drawing.Size(800, 620);
            form4.Show();

            DataGridView table3 = new DataGridView();
            table3.Size = new System.Drawing.Size(770, 570);
            table3.ScrollBars = ScrollBars.Both;
            table3.RowCount = N;
            table3.ColumnCount = N;
            
            for (int i = 0; i < N; i++)
            {
                table3.Columns[i].HeaderText = i.ToString();
               
            }
            
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (byn[i, j, 0] != 666)
                        table3[i, j].Value = byn[i, j, 0].ToString() + ";" + byn[i, j, 1].ToString();
                    else table3[i, j].Value = "---";
                }

            }

                form4.Controls.Add(table3);
        }
        
    }
}
