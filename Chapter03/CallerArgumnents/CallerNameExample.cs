using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CallerArgumnents
{
    public class Gift : INotifyPropertyChanged
    {
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyRaised();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised([CallerMemberName] string propertyname="")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
