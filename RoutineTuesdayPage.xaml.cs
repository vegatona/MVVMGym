using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

// Si hay otra clase Exercise en otro namespace, usa un alias para evitar conflicto
using ExerciseModel = Mockup.Exercise;

namespace Mockup
{
    public partial class RoutineTuesdayPage : ContentPage
    {
        public RoutineTuesdayPage()
        {
            InitializeComponent();
        }

        private void OnShowPickerTapped(object sender, EventArgs e)
        {
            MusclePicker.IsVisible = true;
        }

        private void OnMuscleSelected(object sender, EventArgs e)
        {
            if (MusclePicker.SelectedItem != null)
            {
                string selectedMuscle = MusclePicker.SelectedItem.ToString();
                Console.WriteLine($"M�sculo seleccionado: {selectedMuscle}");
                SelectedMuscleLabel.Text = $"Has seleccionado: {selectedMuscle}";
                MusclePicker.IsVisible = false;
                ShowExercises(selectedMuscle);
            }
        }

        private void OnExerciseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            ExerciseModel selectedExercise = e.SelectedItem as ExerciseModel;
            if (selectedExercise != null)
            {
                DisplayAlert("Ejercicio seleccionado", selectedExercise.Name, "OK");
            }

            ExercisesListView.SelectedItem = null;
        }

        private void ShowExercises(string selectedMuscle)
        {
            if (ExercisesListView.ItemsSource != null)
            {
                ExercisesListView.ItemsSource = null;
            }
            List<ExerciseModel> exercises = new List<ExerciseModel>();

            switch (selectedMuscle)
            {
                case "Pecho":
                    exercises.Add(new ExerciseModel { Name = "Press inclinado con mancuernas o barra (4x8-10 reps)", Image = "press_inclinado.png" });
                    exercises.Add(new ExerciseModel { Name = "Press de banca plano con barra (4x8 reps)", Image = "press_plano.png" });
                    exercises.Add(new ExerciseModel { Name = "Press declinado con mancuernas o barra (3x10-12 reps)", Image = "press_declinado.png" });
                    exercises.Add(new ExerciseModel { Name = "Aperturas en banco inclinado (3x12-15 reps)", Image = "apertura.png" });
                    break;

                case "Biceps":
                    exercises.Add(new ExerciseModel { Name = "Curl con barra recta (ligero) (3x12-15 reps)", Image = "curl_barra_recta.png" });
                    exercises.Add(new ExerciseModel { Name = "Curl concentrado (3x12 reps por brazo)", Image = "curl_concen.png" });
                    exercises.Add(new ExerciseModel { Name = "Curl en predicador (banco Scott) (3x10-12 reps)", Image = "curl_barra_z.png" });
                    exercises.Add(new ExerciseModel { Name = "Curl martillo con mancuerna (3x12-15 reps)", Image = "curl_martillo.png" });
                    break;

                case "Triceps":
                    exercises.Add(new ExerciseModel { Name = "Fondos en banco (bench dips) (3x10-12 reps)", Image = "fondos_banco.png" });
                    exercises.Add(new ExerciseModel { Name = "Press franc�s con barra EZ (4x10-12 reps)", Image = "press_frances.png" });
                    exercises.Add(new ExerciseModel { Name = "Jalones de tr�ceps en polea (agarre recto) (3x10-12 reps)", Image = "extencion_triceps.png" });
                    exercises.Add(new ExerciseModel { Name = "Extensi�n de tr�ceps por encima de la cabeza con mancuerna (3x10-12 reps)", Image = "extension_triceps_encima_cabeza_con_mancuerna.png" });
                    break;

                case "Espalda":
                    exercises.Add(new ExerciseModel { Name = "Jal�n al pecho en polea alta (agarre estrecho) (3x12 reps)", Image = "jalon_pecho_polea_alta_agarre_estrecho.png" });
                    exercises.Add(new ExerciseModel { Name = "Pullovers (3x15 reps)", Image = "pull_over.png" });
                    exercises.Add(new ExerciseModel { Name = "Remo con barra T (agarre cerrado) (4x10 reps)", Image = "remo_barra_t_agarre_cerrado.png" });
                    exercises.Add(new ExerciseModel { Name = "Remo en m�quina (agarre neutro) (3x10-12 reps)", Image = "remo_maquina_agarre_neutro.png" });
                    break;

                case "Pierna":
                    exercises.Add(new ExerciseModel { Name = "Hip thrust con barra (peso moderado) (4x12-15 reps)", Image = "hip_thrust_barra.png" });
                    exercises.Add(new ExerciseModel { Name = "Peso muerto convencional o sumo (4x6 reps)", Image = "peso_muerto.png" });
                    exercises.Add(new ExerciseModel { Name = "Prensa de piernas (peso pesado) (4x8-10 reps)", Image = "prensa.png" });
                    exercises.Add(new ExerciseModel { Name = "Sentadilla b�lgara con mancuernas (3x10-12 reps por pierna)", Image = "sentadillas_bulgaras.png" });
                    break;
            }

            ExercisesListView.ItemsSource = exercises;
            ExercisesLayout.IsVisible = exercises.Count > 0;
        }
    }
}
