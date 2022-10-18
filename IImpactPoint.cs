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

}
