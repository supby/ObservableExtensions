using System;
using System.Reactive.Linq;

namespace ObservableExtensions
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = new PortEmulator("data/data.txt");
            var portObservable = 
                Observable.FromEventPattern<PortEventArgs>(
                    h => port.OnDataArrived += h, h => port.OnDataArrived -= h)
                .Select(x => x.EventArgs.Data);

            //portObservable.Scan<string>((s,s1) => s + s1).Subscribe(x => Console.WriteLine(x));
            portObservable.Split(";").Subscribe(x => Console.WriteLine(x));
            //portObservable.Aggregate<string>((s,s1) => s + s1).Subscribe(x => Console.WriteLine(x));

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();

        }
    }
}
