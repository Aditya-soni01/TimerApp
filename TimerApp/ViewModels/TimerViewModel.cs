using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;



namespace TimerApp.ViewModels
{
    public class TimerViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private DateTime _selectedDates;

        public DateTime SelectedDates
        {
            get { return _selectedDates; }
            set 
            { 
                if (_selectedDates != value)
                {
                    _selectedDates = value;
                    OnPropertyChanged();
                }
            }
        }

        private TimeSpan _selectedTime;
        public TimeSpan SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                if (_selectedTime != value)
                {
                    _selectedTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _countdownDays;
        public int CountdownDays
        {
            get { return _countdownDays; }
            set
            {
                if (_countdownDays != value)
                {
                    _countdownDays = value;
                    OnPropertyChanged(nameof(CountdownDays));
                }
            }
        }

        private int _countdownHours;
        public int CountdownHours
        {
            get { return _countdownHours; }
            set
            {
                if (_countdownHours != value)
                {
                    _countdownHours = value;
                    OnPropertyChanged(nameof(CountdownHours));
                }
            }
        }
        private int _countdownMinutes;
        public int CountdownMinutes
        {
            get { return _countdownMinutes; }
            set
            {
                if (_countdownMinutes != value)
                {
                    _countdownMinutes = value;
                    OnPropertyChanged(nameof(CountdownMinutes));
                }
            }
        }
        private int _countdownSeconds;
        public int CountdownSeconds
        {
            get { return _countdownSeconds; }
            set
            {
                if (_countdownSeconds != value)
                {
                    _countdownSeconds = value;
                    OnPropertyChanged(nameof(CountdownSeconds));
                }
            }
        }

       

        public Command StartCommand { get; set; }
        public Command StopCommand { get; set; }
        private bool _isTimerRunning = false;
        private bool _stopTimer = false;

        [Obsolete]
        public TimerViewModel()
        {
            StartCommand = new Command(async () => await StartTimer());
            StopCommand = new Command(async () => await StopTimer());
        }

        private async Task StopTimer()
        {
            _stopTimer = true;
            CountdownDays = 0;
            CountdownHours = 0;
            CountdownMinutes = 0;
            CountdownSeconds = 0;
        }

        [Obsolete]
        private async Task StartTimer()
        {

            // Check if the timer is already running
            if (_isTimerRunning)
            {
                return; // Do nothing if the timer is already running
            }
            // Reset countdown properties to clear previous timer value
            CountdownDays = 0;
            CountdownHours = 0;
            CountdownMinutes = 0;
            CountdownSeconds = 0;

            _isTimerRunning = true;
            _stopTimer = false;

            // Calculate total seconds from days and selected time
            int totalSeconds = (int)(SelectedDates.Date - DateTime.Today).TotalSeconds + (int)SelectedTime.TotalSeconds;

            // Start the countdown timer
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (!_stopTimer) // Check if the timer should continue running
                {
                    if (totalSeconds >= 0)
                    {
                        CountdownDays = totalSeconds / (24 * 3600);
                        int remainingSeconds = totalSeconds % (24 * 3600);
                        CountdownHours = remainingSeconds / 3600;
                        remainingSeconds %= 3600;
                        CountdownMinutes = remainingSeconds / 60;
                        CountdownSeconds = remainingSeconds % 60;

                        //// Play sound effect for each second
                        //PlaySoundEffect("tick_sound.mp3");

                        // Decrement totalSeconds after updating properties
                        totalSeconds--;
                    }

                    if (totalSeconds < 0)
                    {
                        // Stop the timer when countdown reaches 0
                        _isTimerRunning = false; // Reset the flag
                        Device.InvokeOnMainThreadAsync(async () =>
                        {
                            await Application.Current.MainPage.DisplayAlert("Times Up!", "The timer has reached 0.", "OK");
                        });

                        return false;
                    }

                    return true; // Continue the timer
                }
                else
                {
                    _isTimerRunning = false; // Reset the flag
                    return false; // Stop the timer immediately
                }
            });
        }

        //private async Task PlaySoundEffect(string soundFileName)
        //{
        //    try
        //    {
        //        var player = new MediaPlayer();
        //        var assembly = typeof(App).GetTypeInfo().Assembly;
        //        var stream = assembly.GetManifestResourceStream(soundFileName);
        //        player.Source = MediaSource.FromStream(() => stream);
        //        await player.PlayAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that occur while playing the sound effect
        //        Console.WriteLine($"Error playing sound effect: {ex.Message}");
        //    }
        //}


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
