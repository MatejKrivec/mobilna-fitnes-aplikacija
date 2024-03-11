using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMR_projekt.Models
{
    

    public class ExerciseItem : INotifyPropertyChanged
    {
      /*  public List<Workout> workoutsss = new List<Workout>
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
     };*/

        private TimeSpan workDuration;
        private TimeSpan restDuration;
        private int sets;

        public string Name { get; set; }

        public TimeSpan WorkDuration
        {
            get { return workDuration; }
            set
            {
                if (workDuration != value)
                {
                    workDuration = value;
                    OnPropertyChanged(nameof(WorkDuration));
                }
            }
        }

        public TimeSpan RestDuration
        {
            get { return restDuration; }
            set
            {
                if (restDuration != value)
                {
                    restDuration = value;
                    OnPropertyChanged(nameof(RestDuration));
                }
            }
        }

        public int Sets
        {
            get { return sets; }
            set
            {
                if (sets != value)
                {
                    sets = value;
                    OnPropertyChanged(nameof(Sets));
                }
            }
        }

        public double CalculateCalories(FizioloskiPodatki fizioloskiPodatki)
        {
            List<string> workoutNamesSlovenian = new List<string>
            {
                "Pocep", "benchpress", "Boks", "Burpeeji", "Dvig na prste", "Dipsi", "Dvig uteži",
                "Dvig nog obes", "Skoki s kolebnico", "Skakalni jacki", "Dvig kettlebella", "Kopanje",
                "Poteg z roko za hrbet", "Met medicinke na tla", "Deska", "Pull-up", "Potisk s prsmi",
                "Tek", "Stranska deska", "Trebušnjaki", "Raztezanje", "Plavanje", "Dvig tricepsa",
                "Hoja s poskokom", "Stena sedenje"
            };

            Dictionary<string, string> translationDictionary = new Dictionary<string, string>
            {
                { "Pocep", "Squat" },
                { "benchpress", "Bench Press" },
                { "Boks", "Boxing" },
                { "Burpeeji", "Burpees" },
                { "Dvig na prste", "Calf Raises" },
                { "Dipsi", "Dips" },
                { "Dvig uteži", "Dumbbell Curls" },
                { "Dvig nog obes", "Hanging Leg Raises" },
                { "Skoki s kolebnico", "Jump Rope" },
                { "Skakalni jacki", "Jumping Jacks" },
                { "Dvig kettlebella", "Kettlebell Lifts" },
                { "Kopanje", "Kicking" },
                { "Poteg z roko za hrbet", "Lat Pulldowns" },
                { "Met medicinke na tla", "Medicine Ball Slams" },
                { "Deska", "Plank" },
                { "Pull-up", "Pull-ups" },
                { "Potisk s prsmi", "Push-ups" },
                { "Tek", "Running" },
                { "Stranska deska", "Side Plank" },
                { "Trebušnjaki", "Sit-ups" },
                { "Raztezanje", "Stretching" },
                { "Plavanje", "Swimming" },
                { "Dvig tricepsa", "Tricep Curls" },
                { "Hoja s poskokom", "Walking Lunges" },
                { "Stena sedenje", "Wall Sit" }
            };

            // MET values and adjustment factors (adjust as needed)
            Dictionary<string, double> metValues = new Dictionary<string, double>
            {
                { "Squat", 6.0 },
                { "benchpress", 4.5 },
                { "boxing", 10.0 }, // Adjust as needed
                { "burpees", 8.0 },
                { "calf raises", 3.0 },
                { "dips", 3.0 },
                { "dumbbell curls", 3.0 },
                { "hanging leg raises", 3.0 },
                { "jump rope", 12.0 },
                { "jumping jacks", 8.0 },
                { "kettlebell lifts", 5.0 },
                { "kicking", 6.0 },
                { "lat pulldowns", 3.0 },
                { "medicine ball slams", 8.0 },
                { "plank", 3.0 },
                { "pullups", 3.0 },
                { "pushups", 3.8 },
                { "running", 9.8 },
                { "side plank", 3.0 },
                { "situps", 3.0 },
                { "stretching", 2.0 },
                { "swimming", 7.0 },
                { "tricep curls", 3.0 },
                { "walking lunges", 3.5 },
                { "wall sit", 2.0 },
            };

            Dictionary<string, double> adjustmentFactors = new Dictionary<string, double>
            {
                { "Squat", 1.0 },
                { "benchpress", 1.0 },
                { "boxing", 1.5 }, // Adjust as needed
                { "burpees", 1.0 },
                { "calf raises", 1.0 },
                { "dips", 1.0 },
                { "dumbbell curls", 1.0 },
                { "hanging leg raises", 1.0 },
                { "jump rope", 1.0 },
                { "jumping jacks", 1.0 },
                { "kettlebell lifts", 1.0 },
                { "kicking", 1.0 },
                { "lat pulldowns", 1.0 },
                { "medicine ball slams", 1.0 },
                { "plank", 1.0 },
                { "pullups", 1.0 },
                { "pushups", 1.0 },
                { "running", 1.0 },
                { "side plank", 1.0 },
                { "situps", 1.0 },
                { "stretching", 1.0 },
                { "swimming", 1.0 },
                { "tricep curls", 1.0 },
                { "walking lunges", 1.0 },
                { "wall sit", 1.0 },
            };

            string translatedName = workoutNamesSlovenian.Contains(Name) ? translationDictionary[Name] : Name;

            double metValue = metValues.ContainsKey(translatedName) ? metValues[translatedName] : 8.0;
            double adjustmentFactor = adjustmentFactors.ContainsKey(translatedName) ? adjustmentFactors[translatedName] : 1.0;

            // Exercise duration in hours
            double durationHours = (WorkDuration.TotalSeconds + RestDuration.TotalSeconds) * Sets / 3600.0;

            // Calculate calories burned for this exercise
            double caloriesBurned = (metValue * fizioloskiPodatki.teza_kg * fizioloskiPodatki.visina_cm * durationHours) / 200.0 * adjustmentFactor;

            // Adjust for gender (assuming higher calorie burn for males)
            if (fizioloskiPodatki.Spol.Equals("Male", StringComparison.OrdinalIgnoreCase) )
            {
                caloriesBurned *= 1.1; // Adjust as needed
            }

            return caloriesBurned;
        }

        public double CalculateCalories(FizioloskiPodatki fizioloskiPodatki, TimeSpan workDuration)
        {
            // MET values and adjustment factors (adjust as needed)
            Dictionary<string, double> metValues = new Dictionary<string, double>
            {
                { "Squat", 6.0 },
                { "benchpress", 4.5 },
                { "boxing", 10.0 }, // Adjust as needed
                { "burpees", 8.0 },
                { "calf raises", 3.0 },
                { "dips", 3.0 },
                { "dumbbell curls", 3.0 },
                { "hanging leg raises", 3.0 },
                { "jump rope", 12.0 },
                { "jumping jacks", 8.0 },
                { "kettlebell lifts", 5.0 },
                { "kicking", 6.0 },
                { "lat pulldowns", 3.0 },
                { "medicine ball slams", 8.0 },
                { "plank", 3.0 },
                { "pullups", 3.0 },
                { "pushups", 3.8 },
                { "running", 9.8 },
                { "side plank", 3.0 },
                { "situps", 3.0 },
                { "stretching", 2.0 },
                { "swimming", 7.0 },
                { "tricep curls", 3.0 },
                { "walking lunges", 3.5 },
                { "wall sit", 2.0 },
            };

            Dictionary<string, double> adjustmentFactors = new Dictionary<string, double>
            {
                { "Squat", 1.0 },
                { "benchpress", 1.0 },
                { "boxing", 1.5 }, // Adjust as needed
                { "burpees", 1.0 },
                { "calf raises", 1.0 },
                { "dips", 1.0 },
                { "dumbbell curls", 1.0 },
                { "hanging leg raises", 1.0 },
                { "jump rope", 1.0 },
                { "jumping jacks", 1.0 },
                { "kettlebell lifts", 1.0 },
                { "kicking", 1.0 },
                { "lat pulldowns", 1.0 },
                { "medicine ball slams", 1.0 },
                { "plank", 1.0 },
                { "pullups", 1.0 },
                { "pushups", 1.0 },
                { "running", 1.0 },
                { "side plank", 1.0 },
                { "situps", 1.0 },
                { "stretching", 1.0 },
                { "swimming", 1.0 },
                { "tricep curls", 1.0 },
                { "walking lunges", 1.0 },
                { "wall sit", 1.0 },
            };

            double metValue = metValues.ContainsKey(Name) ? metValues[Name] : 8.0; // Default MET value
            double adjustmentFactor = adjustmentFactors.ContainsKey(Name) ? adjustmentFactors[Name] : 1.0; // Default adjustment factor

            // Exercise duration in hours
            double durationHours = (workDuration.TotalSeconds + RestDuration.TotalSeconds) / 3600.0;

            // Calculate calories burned for this exercise
            double caloriesBurned = (metValue * fizioloskiPodatki.teza_kg * fizioloskiPodatki.visina_cm * durationHours) / 200.0 * adjustmentFactor;

            // Adjust for gender (assuming higher calorie burn for males)
            if (fizioloskiPodatki.Spol.Equals("Male", StringComparison.OrdinalIgnoreCase))
            {
                caloriesBurned *= 1.1; // Adjust as needed
            }

            return caloriesBurned;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double CalculateCaloriesPoCasu(FizioloskiPodatki fizioloskiPodatki)
        {
            // MET values and adjustment factors (adjust as needed)
            Dictionary<string, double> metValues = new Dictionary<string, double>
            {
                { "Squat", 6.0 },
                { "benchpress", 4.5 },
                { "boxing", 10.0 }, // Adjust as needed
                { "burpees", 8.0 },
                { "calf raises", 3.0 },
                { "dips", 3.0 },
                { "dumbbell curls", 3.0 },
                { "hanging leg raises", 3.0 },
                { "jump rope", 12.0 },
                { "jumping jacks", 8.0 },
                { "kettlebell lifts", 5.0 },
                { "kicking", 6.0 },
                { "lat pulldowns", 3.0 },
                { "medicine ball slams", 8.0 },
                { "plank", 3.0 },
                { "pullups", 3.0 },
                { "pushups", 3.8 },
                { "running", 9.8 },
                { "side plank", 3.0 },
                { "situps", 3.0 },
                { "stretching", 2.0 },
                { "swimming", 7.0 },
                { "tricep curls", 3.0 },
                { "walking lunges", 3.5 },
                { "wall sit", 2.0 },
            };

            Dictionary<string, double> adjustmentFactors = new Dictionary<string, double>
            {
                { "Squat", 1.0 },
                { "benchpress", 1.0 },
                { "boxing", 1.5 }, // Adjust as needed
                { "burpees", 1.0 },
                { "calf raises", 1.0 },
                { "dips", 1.0 },
                { "dumbbell curls", 1.0 },
                { "hanging leg raises", 1.0 },
                { "jump rope", 1.0 },
                { "jumping jacks", 1.0 },
                { "kettlebell lifts", 1.0 },
                { "kicking", 1.0 },
                { "lat pulldowns", 1.0 },
                { "medicine ball slams", 1.0 },
                { "plank", 1.0 },
                { "pullups", 1.0 },
                { "pushups", 1.0 },
                { "running", 1.0 },
                { "side plank", 1.0 },
                { "situps", 1.0 },
                { "stretching", 1.0 },
                { "swimming", 1.0 },
                { "tricep curls", 1.0 },
                { "walking lunges", 1.0 },
                { "wall sit", 1.0 },
            };

            double metValue = metValues.ContainsKey(Name) ? metValues[Name] : 8.0; // Default MET value
            double adjustmentFactor = adjustmentFactors.ContainsKey(Name) ? adjustmentFactors[Name] : 1.0; // Default adjustment factor

            // Exercise duration in hours
            double durationHours = (WorkDuration.TotalSeconds + RestDuration.TotalSeconds) / 3600.0;

            // Calculate calories burned for this exercise
            double caloriesBurned = (metValue * fizioloskiPodatki.teza_kg * fizioloskiPodatki.visina_cm * durationHours) / 200.0 * adjustmentFactor;

            // Adjust for gender (assuming higher calorie burn for males)
            if (fizioloskiPodatki.Spol.Equals("Male", StringComparison.OrdinalIgnoreCase))
            {
                caloriesBurned *= 1.1; // Adjust as needed
            }

            return caloriesBurned;
        }

        
        public ExerciseItem()
        {

        }
        public ExerciseItem(string name, TimeSpan workDuration, TimeSpan restDuration, int sets)
        {
            Name = name;
            WorkDuration = workDuration;
            RestDuration = restDuration;
            Sets = sets;
        }
    }
}
