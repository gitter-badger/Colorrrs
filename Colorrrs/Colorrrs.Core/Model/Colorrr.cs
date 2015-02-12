using System.ComponentModel;
using System.Runtime.CompilerServices;
using Colorrrs.Core.Annotations;

namespace Colorrrs.Core.Model
{
    public class Colorrr : INotifyPropertyChanged
    {
        public float Brightness
        {
            get
            {
                return (Red * 299 + Green * 587 + Blue * 114) / 1000f;
            }
        }

        private byte _red;
        public byte Red
        {
            get { return _red; }
            set
            {
                if (_red != value)
                {
                    _red = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Brightness");
                }
            }
        }

        private byte _green;

        public byte Green
        {
            get
            {
                return _green;
            }
            set
            {
                if (_green != value)
                {
                    _green = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Brightness");
                }
            }
        }

        private byte _blue;

        public byte Blue
        {
            get
            {
                return _blue;
            }
            set
            {
                if (_blue != value)
                {
                    _blue = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Brightness");
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
