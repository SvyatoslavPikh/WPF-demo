using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfDemo.Library;

namespace WpfDemo.ViewModels
{
    public class MouseTrackingViewModel : ViewModel
    {
        public ObservableCollection<MouseTrackingPointViewModel> TrackingPoints { get; set; }

        private object _lockObject = new object();
        private bool _isTrackingEnabled;
        private bool _isCanvasVisible;
        private string _buttonCaption;
        
        public ActionCommand ShowReviewCommand { get; set; }

        public bool IsCanvasVisible
        {
            get { return _isCanvasVisible; }
            set
            {
                _isCanvasVisible = value; 
                OnPropertyChanged();
                OnPropertyChanged("IsCanvasNotVisible");
            }
        }

        public bool IsCanvasNotVisible
        {
            get { return !_isCanvasVisible; }
        }

        public string ButtonCaption
        {
            get { return _buttonCaption; }
            set
            {
                _buttonCaption = value;
                OnPropertyChanged();
            }
        }

        public MouseTrackingViewModel()
        {
            TrackingPoints = new ObservableCollection<MouseTrackingPointViewModel>();
            _isTrackingEnabled = true;
            ButtonCaption = "Show Review";

            ShowReviewCommand = new ActionCommand(OnShowReview, x => true);
        }

        private void OnShowReview(object obj)
        {
            _isTrackingEnabled = !_isTrackingEnabled;
            if (_isTrackingEnabled)
            {
                TrackingPoints.Clear();
            }
            ButtonCaption = _isTrackingEnabled ? "Show Review" : "Reset";
            IsCanvasVisible = !IsCanvasVisible;
            
            
        }

        public void AddTrackingPoint(MouseTrackingPointViewModel trackingPoint)
        {
            if (_isTrackingEnabled)
            {
                var previous = TrackingPoints.LastOrDefault();
                if (previous != null)
                {
                    var radius = 10 * (int) (Math.Abs(previous.TimeTicks - trackingPoint.TimeTicks)/TimeSpan.TicksPerSecond);
                    if (radius < 1)
                    {
                        radius = 1;
                    }
                    if (radius > 100)
                    {
                        radius = 100;
                    }
                    trackingPoint.Diameter = 2 * radius;
                    trackingPoint.Radius = radius;
                    trackingPoint.X = trackingPoint.X - radius;
                    trackingPoint.Y = trackingPoint.Y - radius;
                }
                
                TrackingPoints.Add(trackingPoint);
            }
        }
        
    }
    public class MouseTrackingPointViewModel
    {
        public double X { get; set; }

        public double Y { get; set; }

        public long TimeTicks { get; set; }

        public int Radius { get; set; }

        public int Diameter { get; set; }
    }
}

