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
        [Reactive] public string SpinnerVisibility { get; set; } = "Hidden";

        //Button interactions - initializing and cannot change unless through observable
        private readonly Interaction<MainModel, string> _buttonInteraction;

        public Interaction<MainModel, string> ButtonInteraction => this._buttonInteraction;

        //Reactive commands - initialize and set
        public ReactiveCommand<MainModel, string> FireButtonCommand { get; set; }

        public MainViewModel()
        {
            InitObservables();

            _buttonInteraction = new Interaction<MainModel, string>();

            FireButtonCommand = ReactiveCommand.CreateFromObservable<MainModel, string>(this.FireButtonClickHandler);
        }

        private IObservable<string> FireButtonClickHandler(MainModel arg)
        {
            arg = new MainModel();
            arg.Name = "Content was saved.";

            return Observable.StartAsync(async () =>
            {
                //await System.Threading.Tasks.Task.Delay(3000); //Simulate work

                return await this._buttonInteraction.Handle(arg);
            });
        }

        private void InitObservables()
        {
            //Creates an observable that monitors the status of the button
            //and then updates the spinner based on the status of the button
            this.WhenAnyObservable(x => x.FireButtonCommand.IsExecuting)
                .Select(x => x == true ? this.SpinnerVisibility = "Visible" : this.SpinnerVisibility = "Hidden")
                //.ObserveOn(RxApp.MainThreadScheduler)
                //.Delay(TimeSpan.FromSeconds(3))
                .Subscribe();
        }
    }
}
