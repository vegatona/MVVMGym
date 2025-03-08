using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Mockup
{
    public partial class RoutineSundayPage : ContentPage
    {
        public RoutineSundayPage()
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
                SelectedMuscleLabel.Text = $"Has seleccionado: {selectedMuscle}";
                MusclePicker.IsVisible = false;
                ShowExercises(selectedMuscle);
            }
        }

        private void ShowExercises(string selectedMuscle)
        {
            if (ExercisesListView.ItemsSource != null)
            {
                ExercisesListView.ItemsSource = null;
            }
            List<Exercise> exercises = new List<Exercise>();

            switch (selectedMuscle)
            {
                case "Pecho":
                    exercises.Add(new Exercise { Name = "Press inclinado con mancuernas o barra (4x8-10 reps)", Image = "press_inclinado.png" });
                    exercises.Add(new Exercise { Name = "Press de banca plano con barra (4x8 reps)", Image = "press_plano.png" });
                    exercises.Add(new Exercise { Name = "Press declinado con mancuernas o barra (3x10-12 reps)", Image = "press_declinado.png" });
                    exercises.Add(new Exercise { Name = "Aperturas en banco inclinado (3x12-15 reps)", Image = "apertura.png" });
                    break;

                case "Biceps":
                    exercises.Add(new Exercise { Name = "Curl con barra recta (ligero) (3x12-15 reps)", Image = "curl_barra_recta.png" });
                    exercises.Add(new Exercise { Name = "Curl concentrado (3x12 reps por brazo)", Image = "curl_concen.png" });
                    exercises.Add(new Exercise { Name = "Curl en predicador (banco Scott) (3x10-12 reps)", Image = "curl_barra_z.png" });
                    exercises.Add(new Exercise { Name = "Curl martillo con mancuerna (3x12-15 reps)", Image = "curl_martillo.png" });
                    break;

                case "Triceps":
                    exercises.Add(new Exercise { Name = "Fondos en banco (bench dips) (3x10-12 reps)", Image = "fondos_banco.png" });
                    exercises.Add(new Exercise { Name = "Press francés con barra EZ (4x10-12 reps)", Image = "press_frances.png" });
                    exercises.Add(new Exercise { Name = "Jalones de tríceps en polea (agarre recto) (3x10-12 reps)", Image = "extencion_triceps.png" });
                    exercises.Add(new Exercise { Name = "Extensión de tríceps por encima de la cabeza con mancuerna (3x10-12 reps)", Image = "extension_triceps_encima_cabeza_con_mancuerna.png" });
                    break;

                case "Espalda":
                    exercises.Add(new Exercise { Name = "Jalón al pecho en polea alta (agarre estrecho) (3x12 reps)", Image = "jalon_pecho_polea_alta_agarre_estrecho.png" });
                    exercises.Add(new Exercise { Name = "Pullover (3x15 reps)", Image = "pull_over.png" });
                    exercises.Add(new Exercise { Name = "Remo con barra T (agarre cerrado) (4x10 reps)", Image = "remo_barra_t_agarre_cerrado.png" });
                    exercises.Add(new Exercise { Name = "Remo en máquina (agarre neutro) (3x10-12 reps)", Image = "remo_maquina_agarre_neutro.png" });
                    break;

                case "Pierna":
                    exercises.Add(new Exercise { Name = "Hip thrust con barra (peso moderado) (4x12-15 reps)", Image = "hip_thrust_barra.png" });
                    exercises.Add(new Exercise { Name = "Peso muerto convencional o sumo (4x6 reps)", Image = "peso_muerto.png" });
                    exercises.Add(new Exercise { Name = "Prensa de piernas (peso pesado) (4x8-10 reps)", Image = "prensa.png" });
                    exercises.Add(new Exercise { Name = "Sentadilla búlgara con mancuernas (3x10-12 reps por pierna)", Image = "sentadillas_bulgaras.png" });
                    break;
            }

            ExercisesListView.ItemsSource = exercises;
            ExercisesLayout.IsVisible = exercises.Count > 0;
        }
    }
}
