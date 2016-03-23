using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
namespace Task3
{
    public partial class Form1 : Form
    {
        public double[] x, y;
        public double[] x1, y1;
        public double[] x2, y2; 
        public int n;
        public int m;
        public double a, b; 
        public Form1()
        {
            InitializeComponent();
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
         //   dataGridView.Columns.Add("N", "N");
            column.DataPropertyName = "N";
            column.Name = "N";
            dataGridView.Columns.Add(column);

            // Initialize and add a check box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "||Un||";
            column.Name = "||Un||";
            dataGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "||Un-F||";
            column.Name = "||Un-F||";
            dataGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "||Un-F||*100/||Un||";
            column.Name = "||Un-F||*100/||Un||";
            dataGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "||F||";
            column.Name = "||F||";
            dataGridView.Columns.Add(column);
        }


        private void ReadData()
        {
            a = double.Parse(textBox1.Text);
            b = double.Parse(textBox2.Text);
            n = int.Parse(textBox3.Text);
            m = int.Parse(textBox4.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadData();
            Solving.N = n;
            double[] alfas = Solving.GetAlphaForKurant(a, b);
            CalculatePolynomForKurant(alfas, ref this.x, ref this.y);
            DrawFunctionForKurant(this.x, this.y);
            DrawMyFunction(this.x2, this.y2);
        }

        private void DrawFunctionForKurant(double[] x, double[] y)
        {
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            for (int i = 0; i < x.Length; i++)
            {
                list.Add((x[i]), (y[i]));
            }
            LineItem myCurve = pane.AddCurve("Sinc", list, Color.Blue, SymbolType.None);

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void DrawFunction(double[] x1, double[] y1)
        {
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            for (int i = 0; i < x1.Length; i++)
            {
                list.Add((x1[i]), (y1[i]));
            }

            LineItem myCurve = pane.AddCurve("Sinc", list, Color.Red, SymbolType.None);

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void DrawMyFunction(double[] x2, double[] y2)
        {
            GraphPane pane = zedGraphControl.GraphPane;
            PointPairList list2 = new PointPairList();
            x2 = new double[201];
            y2 = new double[201];
            for(int i=0;i<200;i++)
            {
                x2[i] = a + ((b - a) / 200) * i;
                y2[i] = Solving.F(x2[i]);
                list2.Add(x2[i], y2[i]);
            }
            
            LineItem myCurve = pane.AddCurve("Sinc", list2, Color.Green, SymbolType.None);

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void CalculatePolynomForKurant(double[] alfas, ref double[] x, ref double[] y)
        {
            x = new double[n + 1];
            y = new double[n + 1];
            for (int j = 0; j < n + 1; ++j)
            {
                x[j] = a + ((b - a) / n) * j;
            }

            for (int j = 0; j < n + 1; ++j)
            {
                y[j] = Solving.PhiPolinomialForKurant(x[j],a, b);
            }
        }

        private void CalculatePolynom(double[] alfas,ref double[] x1, ref double[] y1)
        {
            x1 = new double[n+1];
            y1 = new double[n+1];
            for (int j = 0; j < n + 1; ++j)
            {
                x1[j] = a + (b - a) / n * j;
            }

            for (int j = 0; j < n + 1; ++j)
            {
                y1[j] = Solving.PhiPolinomial(x1[j], m, a, b);
            }
        }

        private void Function_button_Click(object sender, EventArgs e)
        {
            ReadData();
            double[] alfas = Solving.GetAlpha(m, a, b);
            CalculatePolynom(alfas, ref this.x1, ref this.y1);
            DrawFunction(this.x1, this.y1);
            DrawMyFunction(this.x2, this.y2);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void calculate_button_Click(object sender, EventArgs e)
        {
            int k = 0;
            int m = 10;
            int j = 0;
            for (int i = 0; i < 10; i++)
            {
                dataGridView.Rows.Add(new DataGridViewRow());
                dataGridView.Rows[j].Cells["N"].Value = n;
                dataGridView.Rows[j].Cells["||Un||"].Value = Norma.NormaUn(a, b).ToString("F12");
                dataGridView.Rows[j].Cells["||Un-F||"].Value = Norma.NormaUnF(a, b).ToString("F12");
                dataGridView.Rows[j].Cells["||Un-F||*100/||Un||"].Value = Norma.NormaUnFDivUn(a, b).ToString("F12");
                dataGridView.Rows[j].Cells["||F||"].Value = Norma.NormaF(a, b).ToString("F12");
                j++;
            }
        }
    }
}
