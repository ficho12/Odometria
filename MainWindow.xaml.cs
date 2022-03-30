using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Odometria
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double radio= 31.25;
        double separacionRuedas=20.00;
        double resolucionEncoder=510.3;
        double encoderDer;
        double encoderIzq;
        double x=400;
        double y=225;
        double theta=0;
        ObservableCollection<double> coordBot;
        ObservableCollection<double> coordSig;
        ObservableCollection<Coordenada> tablaOdometria;
        Polyline recorrido;
        Point punto;
        bool inicio = true;

        public MainWindow()
        {
            InitializeComponent();

            coordBot = new ObservableCollection<double>();
            coordSig = new ObservableCollection<double>();
            tablaOdometria = new ObservableCollection<Coordenada>();

            coordSig.Add(x);
            coordSig.Add(y);
            coordSig.Add(theta);

            recorrido = new Polyline();
            punto = new Point();
            punto.X = x;
            punto.Y = y;
            recorrido.Points.Add(punto);
            recorrido.Stroke = Brushes.Blue;
            recorrido.StrokeThickness = 5;

            Canvas.Children.Add(recorrido);

        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if(inicio)
            {
                Info info = new Info(tablaOdometria);
                info.Show();
                info.Owner = this;
                inicio = false;
            }
            
            switch (e.Key)
            {
                case Key.I:
                    avanzar();
                    break;

                case Key.K:
                    retroceder();
                    break;

                case Key.J:
                    girar_izq();
                    break;

                case Key.L:
                    girar_dch();
                    break;

                case Key.U:
                    mov_dch();
                    break;

                case Key.O:
                    mov_izq();
                    break;
            }
        }

        private void avanzar()
        {
            encoderDer = 10;
            encoderIzq = 10;

            odometria(coordSig[0], coordSig[1], coordSig[2], encoderDer, encoderIzq);

            punto.X = coordSig[0];
            punto.Y = coordSig[1];
            
            recorrido.Points.Add(punto);

            coordBot = coordSig;
        }

        private void retroceder()
        {
            encoderDer = -10;
            encoderIzq = -10;

            odometria(coordSig[0], coordSig[1], coordSig[2], encoderDer, encoderIzq);

            punto.X = coordSig[0];
            punto.Y = coordSig[1];
            recorrido.Points.Add(punto);

            coordBot = coordSig;
        }

        private void girar_izq()
        {
            encoderDer = 10;
            encoderIzq = -10;

            odometria(coordSig[0], coordSig[1], coordSig[2], encoderDer, encoderIzq);

            punto.X = coordSig[0];
            punto.Y = coordSig[1];
            recorrido.Points.Add(punto);

            coordBot = coordSig;
        }

        private void girar_dch()
        {
            encoderDer = -10;
            encoderIzq = 10;

            odometria(coordSig[0], coordSig[1], coordSig[2], encoderDer, encoderIzq);

            punto.X = coordSig[0];
            punto.Y = coordSig[1];
            recorrido.Points.Add(punto);

            coordBot = coordSig;
        }

        private void mov_izq()
        {
            encoderDer = 3;
            encoderIzq = 10;

            odometria(coordSig[0], coordSig[1], coordSig[2], encoderDer, encoderIzq);

            punto.X = coordSig[0];
            punto.Y = coordSig[1];
            recorrido.Points.Add(punto);

            coordBot = coordSig;
        }

        private void mov_dch()
        {
            encoderDer = 10;
            encoderIzq = 3;

            odometria(coordSig[0], coordSig[1], coordSig[2], encoderDer, encoderIzq);

            punto.X = coordSig[0];
            punto.Y = coordSig[1];
            recorrido.Points.Add(punto);

            coordBot = coordSig;
        }

        private void odometria(double x, double y, double theta, double encoderDer, double encoderIzq)
        {
            double factorConversion;
            double ruedaDer;
            double ruedaIzq;
            double avance;
            double giro;

            factorConversion = (2*(double)Math.PI*radio) / (1*resolucionEncoder);

            ruedaDer = factorConversion * encoderDer;
            ruedaIzq = factorConversion * encoderIzq;

            avance = (ruedaDer + ruedaIzq) / 2;
            giro = -((ruedaDer - ruedaIzq) / separacionRuedas);

            coordSig[2] = theta + giro;
            coordSig[0] = x + avance * Math.Cos(coordSig[2]);
            coordSig[1] = y + avance * Math.Sin(coordSig[2]);

            Coordenada coordTemp = new Coordenada(coordSig[0], coordSig[1], coordSig[2]);
            tablaOdometria.Add(coordTemp);
        }
    }
}