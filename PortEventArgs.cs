using System;

namespace ObservableExtensions
{
    public class PortEventArgs : EventArgs
    {
        public PortEventArgs(string data)
        {
            Data = data;
        }

        public string Data {get;private set;}
        
    }
}