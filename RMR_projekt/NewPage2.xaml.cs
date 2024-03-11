
using Microsoft.Maui.Controls;
using RMR_projekt.Models;
using Syncfusion.Maui.Sliders;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.Json;

namespace RMR_projekt;

public partial class NewPage2 : ContentPage
{
    private string[] workoutPickerOptionsSlovenian = { "Statistika", "Zgodovina", "Nastavitve", "Fizioloski podatki", "--------", "Odjava" };
    private string[] workoutPickerOptionsEnglish = { "Statistics", "History", "Settings", "Physiological Data", "--------", "Logout" };

    public string WorkLabel = string.Empty;

    public string Kalorije = string.Empty;

    private ObservableCollection<ExerciseItem> exerciseItems;
    private ExerciseItem selectedExercise;
    public ExerciseItem SelectedExercise
    {
        get { return selectedExercise; }
        set
        {
            if (selectedExercise != value)
            {
                selectedExercise = value;
                OnPropertyChanged(nameof(SelectedExercise));
            }
        }
    }
    
    public NewPage2()
    {
        InitializeComponent();

       

        // Retrieve the saved preferences
        bool lightModeEnabled = Preferences.Get("LightModeEnabled", false);
        bool darkModeEnabled = Preferences.Get("DarkModeEnabled", false);

        // Set the initial background color based on preferences
        if (darkModeEnabled == true)
        {
            this.BackgroundColor = Colors.Black;
            StartLabel.TextColor = Colors.Black;
            StartFrame.Background = Colors.Gray;
            WorkoutPicker.TextColor = Colors.Black;
            timeLabel.TextColor = Colors.Black;
            TotalCalories.TextColor = Colors.Black;
            AddExerciceBtn.TextColor = Colors.Black;
            AddExerciceBtn.BackgroundColor = Colors.Gray;
            workLabel.TextColor = Colors.Black;
            restLabel.TextColor = Colors.Black;
            setLabel.TextColor = Colors.Black;
            workBtn.TextColor = Colors.Black;
            workBtn.BackgroundColor = Colors.Gray;
            restBtn.TextColor = Colors.Black;
            restBtn.BackgroundColor = Colors.Gray;
            setBtn.TextColor = Colors.Black;
            setBtn.BackgroundColor = Colors.Gray;
            // Customize sliders for dark mode
            workSlider.BackgroundColor = Colors.AliceBlue;
            restSlider.BackgroundColor = Colors.AliceBlue;
            setSlider.BackgroundColor = Colors.AliceBlue;

            // Customize frames for dark mode
            StartFrame.BackgroundColor = Colors.Gray;

            workoutListView.BackgroundColor = Colors.Black;

        }
        else
        {
            this.BackgroundColor = Colors.White;
            StartLabel.TextColor = Colors.Gray;
            StartFrame.Background = Colors.White;
            WorkoutPicker.TextColor = Colors.White;
            timeLabel.TextColor= Colors.White;
            TotalCalories.TextColor = Colors.White;
            AddExerciceBtn.TextColor = Colors.Gray;
            AddExerciceBtn.BackgroundColor = Colors.White;
            workLabel.TextColor = Colors.Gray;
            restLabel.TextColor = Colors.Gray;
            setLabel.TextColor = Colors.Gray;
            workBtn.TextColor = Colors.Gray;
            workBtn.BackgroundColor = Colors.White;
            restBtn.TextColor = Colors.Gray;
            restBtn.BackgroundColor = Colors.White;
            setBtn.TextColor = Colors.Gray;
            setBtn.BackgroundColor = Colors.White;
            // Customize sliders for light mode
            workSlider.BackgroundColor = Colors.AliceBlue;
            restSlider.BackgroundColor = Colors.AliceBlue;
            setSlider.BackgroundColor = Colors.AliceBlue;

            // Customize frames for light mode
            StartFrame.BackgroundColor = Colors.White;

            workoutListView.BackgroundColor = Colors.White;

          /*  WorkFrame.BackgroundColor = Colors.White;
            RestFrame.BackgroundColor = Colors.White;
            SetsFrame.BackgroundColor = Colors.White;*/

          


        }

        List<Workout> workouts;

        bool language = Preferences.Get("Language", false);

        if (language)
        {
            // Slovenian
            WorkoutPicker.ItemsSource = workoutPickerOptionsSlovenian;
            timeLabel.Text = "00:00";
            TotalCalories.Text = "Skupaj kalorij: ";
            Kalorije = "Skupaj kalorij: ";
            StartLabel.Text = "Začni";
            WorkoutPicker.Title = "Meni";
            AddExerciceBtn.Text = "Dodaj vadbo";
            workLabel.Text = "00:00";
            workLabel2.Text = "Delo";
            workBtn.Text = "Uporabi spremembe";
            restLabel.Text = "00:00";
            restLabel2.Text = "Po?itek";
            restBtn.Text = "Uporabi spremembe";
            setLabel.Text = "00:00";
            setLabel2.Text = "Sklopi";
            setBtn.Text = "Uporabi spremembe";

            /*  WorkLabel.Text = "Trajanje";
              RestLabel.Text = "Počitek";
              SetLabel.Text = "Seti";*/
            /*  var WorkLabel = exerciseCollectionView.FindByName<Label>("WorkLabel");
              // var workFrame = exerciseCollectionView.FindByName<Frame>("WorkFrame");
              if (workLabel != null)
              {
                  workLabel.Text = "Trajanje";
              }*/

            WorkLabel = "Trajanje";

            workouts = new List<Workout>
        {
            new Workout { Name = "Pocep", ImagePath = "squat.png" },
            new Workout { Name = "benchpress", ImagePath = "benchpress.png" },
            new Workout { Name = "Boks", ImagePath = "boxing.png" },
            new Workout { Name = "Burpeeji", ImagePath = "burpees.png" },
            new Workout { Name = "Dvig na prste", ImagePath = "calf_raises.png" },
            new Workout { Name = "Dipsi", ImagePath = "dips.png" },
            new Workout { Name = "Dvig uteži", ImagePath = "dumbell_curls.png" },
            new Workout { Name = "Dvig nog obes", ImagePath = "hanging_leg_raises.png" },
            new Workout { Name = "Skoki s kolebnico", ImagePath = "jump_rope_exercice.png" },
            new Workout { Name = "Skakalni jacki", ImagePath = "jumping_jacks.png" },
            new Workout { Name = "Dvig kettlebella", ImagePath = "kettlebell_lifts.png" },
            new Workout { Name = "Kopanje", ImagePath = "kicking.png" },
            new Workout { Name = "Poteg z roko za hrbet", ImagePath = "lat_pulldowns.png" },
            new Workout { Name = "Met medicinke na tla", ImagePath = "medicine_ball_slams.png" },
            new Workout { Name = "Deska", ImagePath = "plank.png" },
            new Workout { Name = "Pull-up", ImagePath = "pullups.png" },
            new Workout { Name = "Potisk s prsmi", ImagePath = "pushups.png" },
            new Workout { Name = "Tek", ImagePath = "running.png" },
            new Workout { Name = "Stranska deska", ImagePath = "side_plank.png" },
            new Workout { Name = "Trebušnjaki", ImagePath = "situps.png" },
            new Workout { Name = "Raztezanje", ImagePath = "streching.png" },
            new Workout { Name = "Plavanje", ImagePath = "swimming.png" },
            new Workout { Name = "Dvig tricepsa", ImagePath = "tricep_curls.png" },
            new Workout { Name = "Hoja s poskokom", ImagePath = "walking_lunges" },
            new Workout { Name = "Stena sedenje", ImagePath = "wall_sit.png" }
        };
        }
        else
        {
            // English
            WorkoutPicker.ItemsSource = workoutPickerOptionsEnglish;
            timeLabel.Text = "00:00";
            TotalCalories.Text = "Total calories:";
            Kalorije = "Total calories: ";
            StartLabel.Text = "Start";
            WorkoutPicker.Title = "Menu";
            AddExerciceBtn.Text = "Add Exercise";
            workLabel.Text = "00:00";
            workLabel2.Text = "Work";
            workBtn.Text = "Apply Changes";
            restLabel.Text = "00:00";
            restLabel2.Text = "Rest";
            restBtn.Text = "Apply Changes";
            setLabel.Text = "00:00";
            setLabel2.Text = "Sets";
            setBtn.Text = "Apply Changes";

            var WorkLabel = exerciseCollectionView.FindByName<Label>("WorkLabel");
            // var workFrame = exerciseCollectionView.FindByName<Frame>("WorkFrame");
            if (workLabel != null)
            {
                workLabel.Text = "Work";
            }

            workouts = new List<Workout>
        {
            new Workout { Name = "Squat", ImagePath = "squat.png" },
            new Workout { Name = "benchpress", ImagePath = "benchpress.png" },
            new Workout { Name = "boxing", ImagePath = "boxing.png" },
            new Workout { Name = "burpees", ImagePath = "burpees.png" },
            new Workout { Name = "calf raises", ImagePath = "calf_raises.png" },
            new Workout { Name = "dips", ImagePath = "dips.png" },
            new Workout { Name = "dumbell curls", ImagePath = "dumbell_curls.png" },
            new Workout { Name = "hanging_leg_raises", ImagePath = "hanging_leg_raises.png" },
            new Workout { Name = "jump rope", ImagePath = "jump_rope_exercice.png" },
            new Workout { Name = "jumping jacks", ImagePath = "jumping_jacks.png" },
            new Workout { Name = "kettlebell lifts", ImagePath = "kettlebell_lifts.png" },
            new Workout { Name = "kicking", ImagePath = "kicking.png" },
            new Workout { Name = "lat pulldowns", ImagePath = "lat_pulldowns.png" },
            new Workout { Name = "medicine ball slams", ImagePath = "medicine_ball_slams.png" },
            new Workout { Name = "plank", ImagePath = "plank.png" },
            new Workout { Name = "pullups", ImagePath = "pullups.png" },
            new Workout { Name = "pushups", ImagePath = "pushups.png" },
            new Workout { Name = "running", ImagePath = "running.png" },
            new Workout { Name = "side +plank", ImagePath = "side_plank.png" },
            new Workout { Name = "situps", ImagePath = "situps.png" },
            new Workout { Name = "streching", ImagePath = "streching.png" },
            new Workout { Name = "swimming", ImagePath = "swimming.png" },
            new Workout { Name = "tricep curls", ImagePath = "tricep_curls.png" },
            new Workout { Name = "walking lunges", ImagePath = "walking_lunges" },
            new Workout { Name = "wall sit", ImagePath = "wall_sit.png" },

        };
        }

        exerciseItems = new ObservableCollection<ExerciseItem>();
        exerciseItems.CollectionChanged += ExerciseItems_CollectionChanged;
        exerciseCollectionView.ItemsSource = exerciseItems;



        workoutListView.ItemsSource = workouts;
        //workoutListView.IsVisible = false;
    }
    private void OnAddExerciseClicked(object sender, EventArgs e)
    {
        workoutListView.IsVisible = true;
        CalculateTotalCalories();
        //workoutListView.IsVisible = !workoutListView.IsVisible;
    }
    private void OnRestFrameTapped(object sender, EventArgs e)
    {
        var selectedFrame = sender as Frame;
        selectedExercise = selectedFrame?.BindingContext as ExerciseItem;
        restSlider.IsVisible = true;
        restBtn.IsVisible = true;
        restLabel.IsVisible = true;
        restLabel2.IsVisible = true;
        TimeSpan timeSpan = selectedExercise.RestDuration;
        string formattedTime = $"{timeSpan:mm\\:ss}";
        restLabel.Text = formattedTime;
        restSlider.Value = selectedExercise.RestDuration.TotalSeconds;
    }
    private void OnSetsFrameTapped(object sender, EventArgs e)
    {
        var selectedFrame = sender as Frame;
        selectedExercise = selectedFrame?.BindingContext as ExerciseItem;
        setSlider.IsVisible = true;
        setBtn.IsVisible = true;
        setLabel.IsVisible = true;
        setLabel2.IsVisible = true;
        int sets = selectedExercise.Sets;
        setLabel.Text = sets.ToString();
        setSlider.Value = sets;
    }
    private void OnWorkFrameTapped(object sender, EventArgs e)
    {
        var selectedFrame = sender as Frame;
        selectedExercise = selectedFrame?.BindingContext as ExerciseItem;
        workSlider.IsVisible = true;
        workBtn.IsVisible = true;
        workLabel.IsVisible = true;
        workLabel2.IsVisible = true;
        TimeSpan timeSpan = selectedExercise.WorkDuration;
        string formattedTime = $"{timeSpan:mm\\:ss}";
        workLabel.Text = formattedTime;
        workSlider.Value = selectedExercise.WorkDuration.TotalSeconds;
    }

    private void OnWorkSliderClicked(object sender, EventArgs e)
    {
        workSlider.IsVisible = false;
        workBtn.IsVisible = false;
        workLabel.IsVisible = false;
        workLabel2.IsVisible = false;

        if (SelectedExercise != null)
        {
            selectedExercise.WorkDuration = TimeSpan.FromSeconds(workSlider.Value);
        }
        UpdateLabelValue();
        CalculateTotalCalories();
    }
    private void OnWorkSliderValueChanged(object sender, SliderValueChangedEventArgs e)
    {
        double sliderValue = e.NewValue;
        TimeSpan timeSpan = TimeSpan.FromSeconds(sliderValue);

        // Format the TimeSpan as "mm\:ss" and update the label
        string formattedTime = $"{timeSpan:mm\\:ss}";
        workLabel.Text = formattedTime;
        CalculateTotalCalories();
    }
    private void OnRestSliderClicked(object sender, EventArgs e)
    {
        restSlider.IsVisible = false;
        restBtn.IsVisible = false;
        restLabel.IsVisible = false;
        restLabel2.IsVisible = false;

        if (SelectedExercise != null)
        {
            selectedExercise.RestDuration = TimeSpan.FromSeconds(restSlider.Value);
        }
        UpdateLabelValue();
    }
    private void OnRestSliderValueChanged(object sender, SliderValueChangedEventArgs e)
    {
        double sliderValue = e.NewValue;
        TimeSpan timeSpan = TimeSpan.FromSeconds(sliderValue);

        // Format the TimeSpan as "mm\:ss" and update the label
        string formattedTime = $"{timeSpan:mm\\:ss}";
        restLabel.Text = formattedTime;
    }
    private void OnSetSliderClicked(object sender, EventArgs e)
    {
        setSlider.IsVisible = false;
        setBtn.IsVisible = false;
        setLabel.IsVisible = false;
        setLabel2.IsVisible = false;

        if (SelectedExercise != null)
        {
            selectedExercise.Sets = (int)setSlider.Value;
        }
        UpdateLabelValue();
        CalculateTotalCalories();

    }
    private void OnSetSliderValueChanged(object sender, SliderValueChangedEventArgs e)
    {
        double sliderValue = e.NewValue;
        int intSlider = (int)sliderValue;
        setLabel.Text = intSlider.ToString();
        CalculateTotalCalories();
    }
    private void OnWorkoutTapped(object sender, EventArgs e)
    {
        // var tappedViewCell = (ViewCell)sender;
        var tappedViewCell = (Frame)sender;
        var tappedWorkout = (Workout)tappedViewCell.BindingContext;
        var newExercise = new ExerciseItem
        {
            Name = tappedWorkout.Name,
            WorkDuration = TimeSpan.FromSeconds(60),
            RestDuration = TimeSpan.FromSeconds(30),
            Sets = 1
        };

        exerciseItems.Add(newExercise);
        workoutListView.IsVisible = false;
        CalculateTotalCalories();
    }
    private void OnDeleteWorkoutTapped(object sender, EventArgs e)
    {
        var selectedLabel = sender as Label;
        selectedExercise = selectedLabel?.BindingContext as ExerciseItem;
        exerciseItems.Remove(selectedExercise);
        CalculateTotalCalories();
    }
    private void ExerciseItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateLabelValue();
        CalculateTotalCalories();
    }

    private void UpdateLabelValue()
    {
        TimeSpan totalDuration = CalculateTotalTime();
        timeLabel.Text = $"{totalDuration:mm\\:ss}";
    }

    private TimeSpan CalculateTotalTime()
    {
        TimeSpan totalDuration = TimeSpan.Zero;

        foreach (ExerciseItem exerciseItem in exerciseItems)
        {
            totalDuration += (exerciseItem.WorkDuration + exerciseItem.RestDuration) * exerciseItem.Sets;
        }

        return totalDuration;

    }

    private void CalculateTotalCalories()
    {
        FizioloskiPodatki fizioloskiPodatki = GetFizioloskiPodatki();
        double totalCalories = exerciseItems.Sum(item => item.CalculateCalories(fizioloskiPodatki));
        TotalCalories.Text = $"{Kalorije}{totalCalories:F2}";
    }

    private FizioloskiPodatki GetFizioloskiPodatki()
    {
        string serializedData = Preferences.Get("FizioloskiPodatki", string.Empty);

        if (!string.IsNullOrEmpty(serializedData))
        {
            FizioloskiPodatki instance = JsonSerializer.Deserialize<FizioloskiPodatki>(serializedData);

            // Set the singleton instance with the deserialized data
            FizioloskiPodatki.Instance.Spol = instance.Spol;
            FizioloskiPodatki.Instance.visina_cm = instance.visina_cm;
            FizioloskiPodatki.Instance.teza_kg = instance.teza_kg;

            return FizioloskiPodatki.Instance;
        }

        // Return a default value or handle the case where data is not available
        return FizioloskiPodatki.Instance;
    }
    private async void OnStartFrameTapped(object sender, EventArgs e)
    {
        if (exerciseItems != null && exerciseItems.Count > 0)
        {
            await Navigation.PushAsync(new TimerPage(exerciseItems));
        }
    }

    private async void Fizioloski_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FizioloskiP());
    }

    public Dictionary<string, Page> pageDictionary = new Dictionary<string, Page>
    {
            { "Statistika", new StatistikePage() },
            { "Statistics", new StatistikePage() },
            { "Zgodovina", new ZgodovinaPage() },
            { "History", new ZgodovinaPage() },
            { "Fizioloski podatki", new FizioloskiP() },
            { "Physiological Data", new FizioloskiP() },
            { "Nastavitve", new SettingsPage() },
            { "Settings", new SettingsPage() },
            { "Odjava", new MainPage() },
            { "Logout", new MainPage() }
    };


    private async void WorkoutPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedPage = WorkoutPicker.SelectedItem as string;

        if (selectedPage != null && pageDictionary.ContainsKey(selectedPage))
        {
            await Navigation.PushAsync(pageDictionary[selectedPage]);
        }

        // Reset the selected index to avoid firing the event multiple times
        WorkoutPicker.SelectedIndex = -1;
    }
}

