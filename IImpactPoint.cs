using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KursovayaSharp
{
    public abstract class IImpactPoint
    {
        public float X;
        public float Y;

        // абстрактный метод с помощью которого будем изменять состояние частиц
        public abstract void ImpactParticle(ParticleColorful particle, Color cl);


        public virtual void Render(Graphics g, Color cl1)
        {

            g.FillEllipse(
                    new SolidBrush(cl1),
                    X - 1,
                    Y - 1,
                    50,
                    50
                );
        }
    }
    public class GravityPoint : IImpactPoint
    {
        public int Power = 0; // сила притяжения
        public int counter = 0;
        public int check = 0;

        // а сюда скопировали с минимальными правками то, что было в UpdateState
        public override void ImpactParticle(ParticleColorful particle, Color cl)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;


            double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до центра частицы
            if (r + particle.Radius < Power / 2) // если частица оказалась внутри окружности
            {

                counter++;
                particle.FromColor = cl;
            }



        }

        public override void Render(Graphics g, Color cl1)
        {
            // буду рисовать окружность с диаметром равным Power
            g.DrawEllipse(
                   new Pen(cl1, 5),
                   X - Power / 2,
                   Y - Power / 2,
                   Power,
                   Power
               );
            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // обязательно выносим текст и шрифт в переменные
            var text = $"{counter}";
            var font = new Font("Verdana", 10);

            // вызываем MeasureString, чтобы померить размеры текста
            var size = g.MeasureString(text, font);

            // рисуем подложку под текст
            g.FillRectangle(
                new SolidBrush(Color.Red),
                X - size.Width / 2, // так как я выравнивал текст по центру то подложка должна быть центрирована относительно X,Y
                Y - size.Height / 2,
                size.Width,
                size.Height
            );

            // ну и текст рисую уже на базе переменных
            g.DrawString(
                text,
                font,
                new SolidBrush(Color.White),
                X,
                Y,
                stringFormat
            );

        }
    }
}
