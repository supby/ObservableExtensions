using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace ObservableExtensions 
{
    public static class ObservableExtensions 
    {
        public static IObservable<string> Split(
            this IObservable<string> source, string separator) 
        {
            var buf = new List<string>();
            return Observable.Create<string> (o =>
                source.Subscribe (x => {
                    var splitted = x.Split(separator);

                    if(splitted.Length == 1)
                    {
                        buf.Add(x);
                    }
                    else
                    {
                        buf.Add(splitted[0]);
                        o.OnNext(string.Join(string.Empty, buf));
                        buf.Clear();
                        buf.Add(splitted[splitted.Length - 1]);

                        for (int i = 1; i < splitted.Length - 1; i++)
                        {
                            o.OnNext(splitted[i]);
                        }
                    }   
                },
                o.OnError,
                o.OnCompleted));
        }
    }
}