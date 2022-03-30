using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para Info.xaml
    /// </summary>
    /// 

    public partial class Info : Window
    {
        ObservableCollection<Coordenada> listaCord;

        public Info(ObservableCollection<Coordenada> l)
        {
            InitializeComponent();
            listaCord = new ObservableCollection<Coordenada>();
            listaCord = l;
            listCord.ItemsSource = listaCord;
        }
    }

}