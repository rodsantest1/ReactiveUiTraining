using ClassLibrary1.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel,
                    vm => vm.SpinnerVisibility,
                    v => v.Spinner.Visibility)
                .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.FireButtonCommand,
                    v => v.BtnSave)
                .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                    vm => vm.Time,
                    v => v.Timer.Text)
                .DisposeWith(disposables);

                this.ViewModel.ButtonInteraction.RegisterHandler(interaction =>
                {
                    Observable.Start(() => { })
                    //Observable.Timer(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(4))
                    .Delay(TimeSpan.FromSeconds(3))
                    .Subscribe(_ =>
                    {
                        MessageBox.Show($"Message 1 {interaction.Input.Name}", "Title 1", MessageBoxButton.OK);
                    });

                    interaction.SetOutput(interaction.Input.Name);
                });
            });
        }
    }
}
