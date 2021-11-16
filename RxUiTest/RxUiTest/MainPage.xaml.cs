using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;

namespace RxUiTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Observable
                .Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1), RxApp.TaskpoolScheduler)
                .SelectMany(GetResultAsync)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(val => Label.Text = val);
        }

        static async Task<string> GetResultAsync(long arg)
        {
            await Task.Delay(1000).ConfigureAwait(false);
            var res = $"Timer:{arg}, on {System.Threading.Thread.CurrentThread.ManagedThreadId}.";
            return res;
        }
    }
}
