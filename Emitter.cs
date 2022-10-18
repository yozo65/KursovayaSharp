﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KursovayaSharp
{
    public class Emitter
    {
        public int ParticlesCount = 500;
        public float GravitationX = 0;
        public float GravitationY = 1;
        public int MousePositionX;
        public int MousePositionY;

        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 360; // разброс частиц относительно Direction
        public int SpeedMin = 1; // начальная минимальная скорость движения частицы
        public int SpeedMax = 10; // начальная максимальная скорость движения частицы
        public int RadiusMin = 2; // минимальный радиус частицы
        public int RadiusMax = 10; // максимальный радиус частицы
        public int LifeMin = 60; // минимальное время жизни частицы
        public int LifeMax = 120; // максимальное время жизни частицы

        public int ParticlesPerTick = 5;

        //Интересно, а видно, что я код вставляю?
        public virtual void ResetParticle(ParticleColorful particle)
        {

            particle.FromColor = Color.White;
            //particle.ColorFrom = Color.White;

            particle.X = X;
            particle.Y = Y;
            particle.Life = ParticleColorful.rand.Next(LifeMin, LifeMax);
            particle.ColorFrom = Color.White;

            var direction = Direction
                + (double)ParticleColorful.rand.Next(Spreading)
                - Spreading / 2;

            var speed = Particle.rand.Next(SpeedMin, SpeedMax);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle.Radius = ParticleColorful.rand.Next(RadiusMin, RadiusMax);
        }
        public virtual ParticleColorful CreateParticle()
        {
            var particle = new ParticleColorful();
            particle.FromColor = Color.White;


            return particle;
        }
        private void picDisplay_Click(object sender, EventArgs e)
        {

        }
        // добавил функцию обновления состояния системы


        public void UpdateState()
        {

            int particlesToCreate = ParticlesPerTick;

            foreach (var particle in particles)
            {

                // уменьшаю здоровье
                // если здоровье кончилось
                if (particle.Life < 0)
                {
                    if (particlesToCreate > 0)
                    {

                        /* у нас как сброс частицы равносилен созданию частицы */
                        particlesToCreate -= 1; // поэтому уменьшаем счётчик созданных частиц на 1
                        ResetParticle(particle);
                    }
                }
                else
                {

                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;
                    particle.Life -= 1;


                    foreach (var point in impactPoints)
                    {
                        if (point == impactPoints[0]) point.ImpactParticle(particle, Color.Red);
                        if (point == impactPoints[1]) point.ImpactParticle(particle, Color.Blue);
                        if (point == impactPoints[2]) point.ImpactParticle(particle, Color.Green);
                        if (point == impactPoints[3]) point.ImpactParticle(particle, Color.Purple);
                        if (point == impactPoints[4]) point.ImpactParticle(particle, Color.Yellow);
                        if (point == impactPoints[5]) point.ImpactParticle(particle, Color.HotPink);
                        if (point == impactPoints[6]) point.ImpactParticle(particle, Color.Orange);
                        if (impactPoints.Count > 7) { if (point == impactPoints[7]) point.ImpactParticle(particle, Color.Red); }
                    }

                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;

                }
            }

            while (particlesToCreate >= 1)
            {
                particlesToCreate -= 1;
                var particle = CreateParticle();

                ResetParticle(particle);
                particles.Add(particle);
            }

        }
        public int CheckColor;
        // функция рендеринга
        public void Render(Graphics g)
        {
            // утащили сюда отрисовку частиц
            foreach (var particle in particles)
            {

                particle.Draw(g);


            }
            foreach (var point in impactPoints)
            {
                if (point == impactPoints[0]) point.Render(g, Color.Red);
                if (point == impactPoints[1]) point.Render(g, Color.Blue);
                if (point == impactPoints[2]) point.Render(g, Color.Green);
                if (point == impactPoints[3]) point.Render(g, Color.Purple);
                if (point == impactPoints[4]) point.Render(g, Color.Yellow);
                if (point == impactPoints[5]) point.Render(g, Color.HotPink);
                if (point == impactPoints[6]) point.Render(g, Color.Orange);
                if (impactPoints.Count > 7) if (point == impactPoints[7]) point.Render(g, Color.Red);



            }
        }
    }

    public class TopEmitter : Emitter
    {
        public int Width; // длина экрана

        public override void ResetParticle(ParticleColorful particle)
        {
            base.ResetParticle(particle); // вызываем базовый сброс частицы, там жизнь переопределяется и все такое

            // а теперь тут уже подкручиваем параметры движения
            particle.X = Particle.rand.Next(Width); // позиция X -- произвольная точка от 0 до Width
            particle.Y = 0;  // ноль -- это верх экрана 

            particle.SpeedY = 1; // падаем вниз по умолчанию
            particle.SpeedX = Particle.rand.Next(-2, 2); // разброс влево и вправо у частиц 
        }
    }
}
