using Microsoft.Maui.Dispatching;
using RMR_projekt.Models;
using System.Collections.ObjectModel;
using System.Xml;
using Microsoft.Maui.Controls;
using System.Runtime.ConstrainedExecution;
using Syncfusion.Maui.Gauges;
using System.Globalization;
using Microsoft.Maui.Media;
using System.Text.Json;


namespace RMR_projekt;

public partial class TimerPage : ContentPage
{
    private int currentExerciseIndex = 0;
    private int currentRound = 0;
    private bool isPaused = false;
    private ObservableCollection<ExerciseItem> exerciseItems;
    private List<ExerciseItem> exerciseList;
    private TimeSpan timeRemaining;
    private TimeSpan totalExerciseTime;
    private TimeSpan totalOveralTime;
    private int countdownSeconds = 3;
    private double caloriesBurned = 0;
    private List<Locale> localeList;
    private bool isSoundOn = true;
    private FizioloskiPodatki fizioloskiPodatki = new FizioloskiPodatki("Male", 190, 80);
    public TimerPage(ObservableCollection<ExerciseItem> items)
    {
        InitializeComponent();

        bool Language = Preferences.Get("Language", false);

        if (Language == true)
        {
            topLabel.Text = "Delo";
            ExerciceLabel.Text = "Aktivnost";
            SetsLabel.Text = "Seti";
            soundButton.Text = "Zvok";
            pauseButton.Text = "Pavza";
            skipButton.Text = "Skip";

        }
        else
        {
            topLabel.Text = "Work";
            ExerciceLabel.Text = "Exercice";
            SetsLabel.Text = "Sets";
            soundButton.Text = "Sound";
            pauseButton.Text = "Pause";
            skipButton.Text = "Skip";
        }

        // Retrieve FizioloskiPodatki from Preferences
        string fizioloskiPodatkiJson = Preferences.Get("FizioloskiPodatki", "");
        if (!string.IsNullOrEmpty(fizioloskiPodatkiJson))
        {
            fizioloskiPodatki = JsonSerializer.Deserialize<FizioloskiPodatki>(fizioloskiPodatkiJson);
        }

        exerciseItems = new ObservableCollection<ExerciseItem>();
        exerciseList = new List<ExerciseItem>();
        exerciseItems = items;
        totalExerciseTime = TimeSpan.Zero;
        foreach (ExerciseItem exercise in exerciseItems)
        {
            totalExerciseTime += (exercise.WorkDuration + exercise.RestDuration) * exercise.Sets;
            exerciseList.Add(new ExerciseItem(exercise.Name, exercise.WorkDuration * exercise.Sets, exercise.RestDuration * exercise.Sets, exercise.Sets));
        }
        totalOveralTime = totalExerciseTime;
        caloriesBurned = CalculateTotalCalories();
        LoadLocalesAsync();
        SetUpUI();
        this.Appearing += (sender, e) =>
        {
            // Start the countdown timer when the page appears
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (countdownSeconds > 0)
                {
                    // Continue the countdown
                    countdownLabel.Text = countdownSeconds.ToString();
                    SpeakText(countdownSeconds.ToString());
                    countdownSeconds--;
                    countDownLayout.IsVisible = true;
                    return true;
                }
                else
                {
                    // Start the main timer after the countdown
                    countDownLayout.IsVisible = false;
                    StartTimer();
                    return false; // Stop the countdown timer
                }
            });

        };
        //this.Appearing += (sender, e) => StartTimer();
    }
    private void StartTimer()
    {
        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (!isPaused)
            {
                if (currentExerciseIndex < exerciseItems.Count)
                {
                    ExerciseItem currentExercise = exerciseItems[currentExerciseIndex];

                    if (timeRemaining.TotalSeconds > 0)
                    {
                        // Continue the current phase (Work or Rest)
                        timeRemaining = timeRemaining.Subtract(TimeSpan.FromSeconds(1));
                        totalExerciseTime = totalExerciseTime.Subtract(TimeSpan.FromSeconds(1));
                        if (timeRemaining.TotalSeconds <= 3 && timeRemaining.TotalSeconds > 0 && isSoundOn)
                        {
                            SpeakText(timeRemaining.TotalSeconds.ToString());
                        }
                        UpdateUI();
                        return true;
                    }
                    else
                    {
                        // Switch to the next phase or exercise
                        currentRound++;
                        if (currentRound < currentExercise.Sets * 2 + 1)  // mnozi z 2 zaradi workDuration in restDuration
                        {
                            // Move to the next phase (Work or Rest)
                            timeRemaining = (currentRound % 2 == 0) ? currentExercise.RestDuration : currentExercise.WorkDuration;
                            radialAxis.Maximum = timeRemaining.TotalSeconds;
                            pointer.Value = timeRemaining.TotalSeconds;
                            if (currentRound % 2 == 0)
                            {
                                topLabel.Text = "Rest";
                                grid.BackgroundColor = Colors.OrangeRed;
                                frame1.BackgroundColor = Colors.OrangeRed;
                                frame2.BackgroundColor = Colors.OrangeRed;
                            }
                            else
                            {
                                topLabel.Text = "Work";
                                grid.BackgroundColor = Colors.GreenYellow;
                                frame1.BackgroundColor = Colors.GreenYellow;
                                frame2.BackgroundColor = Colors.GreenYellow;
                            }
                            UpdateUI();
                            return true;
                        }
                        else
                        {
                            // Nadaljuj na naslednjo vajo
                            currentRound = 0;
                            currentExerciseIndex++;

                            // Preveri èe obstaja naslednja vaja
                            if (currentExerciseIndex < exerciseItems.Count)
                            {
                                // Start timer za work duration naslednje vaje
                                timeRemaining = exerciseItems[currentExerciseIndex].WorkDuration;
                                radialAxis.Maximum = timeRemaining.TotalSeconds;
                                pointer.Value = timeRemaining.TotalSeconds;
                                topLabel.Text = "Work";
                                grid.BackgroundColor = Colors.GreenYellow;
                                frame1.BackgroundColor = Colors.GreenYellow;
                                frame2.BackgroundColor = Colors.GreenYellow;
                                currentRound++;
                                UpdateUI();
                                return true;
                            }
                            else
                            {
                                // All exercises and sets completed
                                Navigation.PushAsync(new KonecPage(totalOveralTime, caloriesBurned, exerciseList));
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    // All exercises and sets completed
                    Navigation.PushAsync(new KonecPage(totalOveralTime, caloriesBurned, exerciseList));
                    return false;
                }
            }
            else
            {
                return true;
            }
        });
    }

    private void UpdateUI()
    {
        exerciseCounterLabel.Text = $"{currentExerciseIndex + 1}/{exerciseItems.Count}";
        setsCounterLabel.Text = $"{(int)Math.Ceiling(currentRound / 2.0)}/{exerciseItems[currentExerciseIndex].Sets}";
        timer.Text = timeRemaining.ToString("mm':'ss");
        totalTimeLabel.Text = totalExerciseTime.ToString("mm':'ss");
        nameLabel.Text = exerciseItems[currentExerciseIndex].Name;
        if (pointer.Value > 0)
        {
            pointer.Value -= 1;
        }
    }
    private void UpdateSkipUI()
    {
        exerciseCounterLabel.Text = $"{currentExerciseIndex + 1}/{exerciseItems.Count}";
        setsCounterLabel.Text = $"{(int)Math.Ceiling(currentRound / 2.0)}/{exerciseItems[currentExerciseIndex].Sets}";
        timer.Text = timeRemaining.ToString("mm':'ss");
        totalTimeLabel.Text = totalExerciseTime.ToString("mm':'ss");
        nameLabel.Text = exerciseItems[currentExerciseIndex].Name;
    }
    private void OnPauseButtonClicked(object sender, EventArgs e)
    {
        isPaused = !isPaused;
        pauseButton.Text = isPaused ? "Continue" : "Pause";
    }
    private void OnSkipButtonClicked(object sender, EventArgs e)
    {
        if (currentRound % 2 != 0)
        {
            caloriesBurned -= exerciseItems[currentExerciseIndex].CalculateCalories(fizioloskiPodatki, timeRemaining);
            exerciseList[currentExerciseIndex].WorkDuration = exerciseList[currentExerciseIndex].WorkDuration.Subtract(timeRemaining);
        }
        else
        {
            exerciseList[currentExerciseIndex].RestDuration = exerciseList[currentExerciseIndex].RestDuration.Subtract(timeRemaining);
        }
        currentRound++;
        totalOveralTime = totalOveralTime.Subtract(timeRemaining);
        totalExerciseTime = totalExerciseTime.Subtract(timeRemaining);
        if (currentExerciseIndex < exerciseItems.Count)
        {
            ExerciseItem currentExercise = exerciseItems[currentExerciseIndex];

            if (currentRound < currentExercise.Sets * 2 + 1)  // mnozi z 2 zaradi workDuration in restDuration 
            {
                // Move to the next phase (Work or Rest)
                timeRemaining = (currentRound % 2 == 0) ? currentExercise.RestDuration : currentExercise.WorkDuration;
                radialAxis.Maximum = timeRemaining.TotalSeconds;
                pointer.Value = timeRemaining.TotalSeconds;
                if (currentRound % 2 == 0)
                {
                    topLabel.Text = "Rest";
                    grid.BackgroundColor = Colors.OrangeRed;
                    frame1.BackgroundColor = Colors.OrangeRed;
                    frame2.BackgroundColor = Colors.OrangeRed;
                }
                else
                {
                    topLabel.Text = "Work";
                    grid.BackgroundColor = Colors.GreenYellow;
                    frame1.BackgroundColor = Colors.GreenYellow;
                    frame2.BackgroundColor = Colors.GreenYellow;
                }
                UpdateUI();
                //UpdateSkipUI();
            }
            else
            {
                // Nadaljuj na naslednjo vajo
                currentRound = 0;
                currentExerciseIndex++;

                // Preveri èe obstaja naslednja vaja
                if (currentExerciseIndex < exerciseItems.Count)
                {
                    // Start timer za work duration naslednje vaje
                    timeRemaining = exerciseItems[currentExerciseIndex].WorkDuration;
                    radialAxis.Maximum = timeRemaining.TotalSeconds;
                    pointer.Value = timeRemaining.TotalSeconds;
                    topLabel.Text = "Work";
                    grid.BackgroundColor = Colors.GreenYellow;
                    frame1.BackgroundColor = Colors.GreenYellow;
                    frame2.BackgroundColor = Colors.GreenYellow;
                    currentRound++;
                    UpdateUI();
                    //UpdateSkipUI();
                }
            }
        }
    }
    private void SetUpUI()
    {
        TimeSpan setUpTime = exerciseItems[currentExerciseIndex].WorkDuration;
        radialAxis.Maximum = setUpTime.TotalSeconds;
        pointer.Value = setUpTime.TotalSeconds;
        topLabel.Text = "Work";
        grid.BackgroundColor = Colors.GreenYellow;
        frame1.BackgroundColor = Colors.GreenYellow;
        frame2.BackgroundColor = Colors.GreenYellow;
        exerciseCounterLabel.Text = $"{currentExerciseIndex + 1}/{exerciseItems.Count}";
        setsCounterLabel.Text = $"{(int)Math.Ceiling(currentRound / 2.0)}/{exerciseItems[currentExerciseIndex].Sets}";
        timer.Text = setUpTime.ToString("mm':'ss");
        totalTimeLabel.Text = totalExerciseTime.ToString("mm':'ss");
        nameLabel.Text = exerciseItems[currentExerciseIndex].Name;
    }
    private double CalculateTotalCalories()
    {
        double totalCalories = exerciseItems.Sum(item => item.CalculateCalories(fizioloskiPodatki));
        //TotalCalories.Text = $"Total Calories: {totalCalories:F2}";
        return totalCalories;
    }
    private async void SpeakText(string text)
    {
        //IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();
        //List<Locale> localeList = locales.ToList();
        if (localeList == null)
        {
            // Load locales if not already loaded
            LoadLocalesAsync();
            return;
        }
        var settings = new SpeechOptions()
        {
            Volume = 1.0f,
            Pitch = 1.0f,
            Locale = localeList[64]
        };

        await TextToSpeech.Default.SpeakAsync(text, settings);
    }
    private async void LoadLocalesAsync()
    {
        IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();
        localeList = locales.ToList();
    }
    private void OnSoundButtonClicked(object sender, EventArgs e)
    {
        isSoundOn = !isSoundOn;
        UpdateSoundButtonUI();
    }

    private void UpdateSoundButtonUI()
    {
        soundButton.Text = isSoundOn ? "On" : "Off";
    }
}