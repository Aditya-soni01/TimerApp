using TimerApp.ViewModels;

namespace TimerApp.Views;

public partial class TimerView : ContentPage
{
	TimerViewModel _viewModel;

    [Obsolete]
    public TimerView()
	{
		InitializeComponent();
		BindingContext = _viewModel = new TimerViewModel();
	}

    private void OnDatePickerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
		if(e.PropertyName == nameof(DatePicker.Date)) 
		{
			var picker = (DatePicker)sender;
			if(BindingContext is TimerViewModel viewModel) 
			{
				viewModel.SelectedDates = picker.Date;
			}
		}
    }

    private void OnTimePickerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimePicker.Time))
        {
            var picker = (TimePicker)sender;
            if (BindingContext is TimerViewModel viewModel)
            {
                viewModel.SelectedTime = picker.Time;
            }
        }
    }
}