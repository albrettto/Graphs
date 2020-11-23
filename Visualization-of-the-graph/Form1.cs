using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visualization_of_the_graph
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
        }

        struct Circle
        {
            public int x; 
            public int y;
            public int rad;
        }

        Circle [] koortoch = new Circle[50]; //массив точек
        int n = 0; //счетчик текущей вершины
        int versh = -1; //понадобиться нам для обработки различных ситуаций при нажатии мыши

        private void Canvas_Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right) // Если нажата правая кнопка мыши
            {
                Pen pen_default = new Pen(Color.Blue, 2);
                koortoch[n].rad = 15;
                koortoch[n].x = e.X - koortoch[n].rad;
                koortoch[n].y = e.Y - koortoch[n].rad;
                Canvas_Panel.CreateGraphics().DrawEllipse(pen_default, koortoch[n].x, koortoch[n].y, 
                    koortoch[n].rad * 2, koortoch[n].rad * 2);
                String drawString = (n+1).ToString();
                Font drawFont = new Font("Arial", 14);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                PointF drawPoint = new PointF(koortoch[n].x + koortoch[n].rad/2, koortoch[n].y + koortoch[n].rad/2);
                Canvas_Panel.CreateGraphics().DrawString(drawString, drawFont, drawBrush, drawPoint);
                n++;
            }
            if (e.Button == MouseButtons.Left) // Если нажата левая кнопка мыши
            {
                Pen pen_selected = new Pen(Color.Red, 2);
                if (versh == -1)//если пока не запомнили ни одну из вершин
                {
                    for (int i = 0; i < n; i++) //проверяем попал ли щелчок мыши на какую-то из вершин
                        if (e.X - koortoch[i].rad <= koortoch[i].x + koortoch[i].rad &&
                            e.X - koortoch[i].rad >= koortoch[i].x - koortoch[i].rad &&
                            e.Y - koortoch[i].rad <= koortoch[i].y + koortoch[i].rad &&
                            e.Y - koortoch[i].rad >= koortoch[i].y - koortoch[i].rad)
                        {
                            versh = i;//если попали, то запоминаем вершину по которой щелкнули
                            Canvas_Panel.CreateGraphics().DrawEllipse(pen_selected, koortoch[i].x, koortoch[i].y,
                    koortoch[i].rad * 2, koortoch[i].rad * 2);
                            break;
                        }
                }
                else //если по одной из вершин уже раннее щелкнули(запомнили), то
                {
                    int toversh = -1;
                    for (int i = 0; i < n; i++) //проверяем попал ли щелчок мыши на какую-то из вершин
                        if (e.X - koortoch[i].rad <= koortoch[i].x + koortoch[i].rad &&
                            e.X - koortoch[i].rad >= koortoch[i].x - koortoch[i].rad &&
                            e.Y - koortoch[i].rad <= koortoch[i].y + koortoch[i].rad &&
                            e.Y - koortoch[i].rad >= koortoch[i].y - koortoch[i].rad)
                        {
                            toversh = i;//если попали, то запоминаем вершину по которой щелкнули
                            Canvas_Panel.CreateGraphics().DrawEllipse(pen_selected, koortoch[i].x, koortoch[i].y,
                    koortoch[i].rad * 2, koortoch[i].rad * 2);
                            break;
                        }
                    if ((toversh != -1) && (versh != toversh))//если щелкнули сначала по одной, а потом по другой вершине
                    {
                        Point p1 = new Point(koortoch[versh].x + koortoch[versh].rad , koortoch[versh].y + koortoch[versh].rad );
                        Point p2 = new Point(koortoch[toversh].x + koortoch[toversh].rad , koortoch[toversh].y + koortoch[toversh].rad );
                        Canvas_Panel.CreateGraphics().DrawLine(pen_selected, p1, p2);
                        toversh = -1;
                        versh = -1;
                    }
                }
            }
            Result_TextBox.Text = versh.ToString();
        }
        


        private void Clear_Button_Click(object sender, EventArgs e)
        {
            Canvas_Panel.Refresh();
            n = 0;
        }

        private void Create_Button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= n; ++i)
            {
                Matrica_DataGridView.Columns.Add(i.ToString(), i.ToString());
                Matrica_DataGridView.Rows.Add();
            }
            for (int i = 1; i <= n; ++i)
            {
                Matrica_DataGridView[0, i].Value = i;
                Matrica_DataGridView[i, 0].Value = i;
            }
        }
    }
}
