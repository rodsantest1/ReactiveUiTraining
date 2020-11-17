using ClassLibrary1.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive.Linq;

namespace ClassLibrary1.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        //Reactive properties
        [Reactive] public string SpinnerVisibility { get; set; } = "hidden";

        //Button interactions - initializing and cannot change unless through observable
        private readonly Interaction<MainModel, string> _buttonInteraction;

        public Interaction<MainModel, string> ButtonInteraction => this._buttonInteraction;

        //Reactive commands - initialize and set
        public ReactiveCommand<MainModel, string> FireButtonCommand { get; set; }

        public MainModel mainModel { get; set; }

        public string Time => this._time.Value;
        private readonly ObservableAsPropertyHelper<string> _time;
        //public string Time { [ObservableAsProperty] get; }

        public MainViewModel()
        {
            _buttonInteraction = new Interaction<MainModel, string>();

            FireButtonCommand = ReactiveCommand.CreateFromObservable<MainModel, string>(this.FireButtonClickHandler);

            InitObservables();

            mainModel = new MainModel();

            //this.WhenAnyValue(x => x.FireButtonCommand.IsExecuting)
            //    .Select(_ => TimeSpan.FromSeconds(1))
            //    .Select(_ => DateTime.Now.ToString("HH:mm:ss"))
            //    .ObserveOn(RxApp.MainThreadScheduler)
            //    .ToProperty(this, x => x.Time);

            this._time = Observable
                .Interval(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .Select(_ => DateTime.Now.ToString("HH:mm:ss"))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, x => x.Time);
        }

        private IObservable<string> FireButtonClickHandler(MainModel arg)
        {
            mainModel.Name = "Content was saved.";

            return Observable.StartAsync(async () =>
            {
                //await System.Threading.Tasks.Task.Delay(3000); //Simulate work

                return await this._buttonInteraction.Handle(mainModel);
            }).Delay(TimeSpan.FromSeconds(5));
        }

        private void InitObservables()
        {
            //Creates an observable that monitors the status of the button
            //and then updates the spinner based on the status of the button
            this.WhenAnyObservable(x => x.FireButtonCommand.IsExecuting)
                .Select(x => x == true ? this.SpinnerVisibility = "visible" : this.SpinnerVisibility = "hidden")
                //.ObserveOn(RxApp.MainThreadScheduler)
                .Delay(TimeSpan.FromSeconds(3))
                .Subscribe();
        }
    }
}
