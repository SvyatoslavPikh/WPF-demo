using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WpfDemo.Library;
using WpfDemo.ViewModels;

namespace WpfDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private Stopwatch _watch = new Stopwatch();
        private readonly MouseTrackingViewModel _vieModel;

        public MainWindow()
        {
            InitializeComponent();
            //Unfortunetly I had to do it because there was no solution from the box to pass EventArgs using ICommand binding
            _vieModel = new MouseTrackingViewModel();
            DataContext = _vieModel;
            _watch = Stopwatch.StartNew();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var container = (IInputElement)sender;
            _vieModel.AddTrackingPoint(new MouseTrackingPointViewModel()
            {
                X = e.GetPosition(container).X,
                Y = e.GetPosition(container).Y,
                TimeTicks = _watch.ElapsedTicks
            });
        }

        public void Dispose()
        {
            _watch.Stop();
        }
    }
}
