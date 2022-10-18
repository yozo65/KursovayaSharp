using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovayaSharp
{
    public partial class Form1 : Form
    {
        GravityPoint point1; GravityPoint point2;
        GravityPoint point3;
        GravityPoint point4;
        GravityPoint point5;
        GravityPoint point6;
        GravityPoint point7;
        TopEmitter emitter;
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            this.emitter = new TopEmitter
            {
                Width = picDisplay.Width,
                GravitationY = 0.25f
            };

            emitters.Add(this.emitter);
            point1 = new GravityPoint
            {
                X = picDisplay.Width / 2 + 100,
                Y = picDisplay.Height / 2 + 30,
            };
            point2 = new GravityPoint
            {
                X = picDisplay.Width / 2 - 100,
                Y = picDisplay.Height / 2 + 30,
            };
            point3 = new GravityPoint
            {
                X = picDisplay.Width / 2 - 200,
                Y = picDisplay.Height / 2 + 60,
            };
            point4 = new GravityPoint
            {
                X = picDisplay.Width / 2 - 300,
                Y = picDisplay.Height / 2 + 90,
            };
            point5 = new GravityPoint
            {
                X = picDisplay.Width / 2 + 200,
                Y = picDisplay.Height / 2 + 60,
            };
            point6 = new GravityPoint
            {
                X = picDisplay.Width / 2 + 300,
                Y = picDisplay.Height / 2 + 90,
            };
            point7 = new GravityPoint
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };
            emitter.impactPoints.Add(point1);
            emitter.impactPoints.Add(point2);
            emitter.impactPoints.Add(point3);
            emitter.impactPoints.Add(point4);
            emitter.impactPoints.Add(point5);
            emitter.impactPoints.Add(point6);
            emitter.impactPoints.Add(point7);
        }
        private void picDisplay_Click(object sender, EventArgs e)
        {
        }
        int counter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "Количество частиц: " + emitter.particles.Count; //Добавил вывод на экран количества частиц

            emitter.UpdateState(); // каждый тик обновляем систему

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g); // рендерим систему
            }
            picDisplay.Invalidate();
        }
    }
}
