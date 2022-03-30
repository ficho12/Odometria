using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odometria
{
    [Serializable]
    public class Coordenada
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Theta { get; set; }

        public Coordenada(double X, double Y, double Theta)
        {
            this.X = X;
            this.Y = Y;
            this.Theta = Theta;
        }
    }
}
