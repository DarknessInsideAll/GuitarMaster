using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace GuitarMaster
{
    struct Notes
    {
        public string _note;
        public Brush _color;

        public Notes(string note, Brush color)
        {
            _note = note;
            _color = color;

        }
    }

    struct Fret
    {
        public Notes _verticalNote;
        public int _numOfFret;
        public Notes _answerNote;


        public Fret(Notes verticalNote, int numOfFret, Notes answerNote)
        {
            _verticalNote = verticalNote;
            _numOfFret = numOfFret;
            _answerNote = answerNote;
        }
    }




    public partial class MainWindow : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;
       

        List<string> unselectedStrings;
        List<Fret> frets = new List<Fret>();
        Fret currentFret;

        Random rnd = new Random();

        Notes C;
        Notes D;
        Notes E;
        Notes F;
        Notes G;
        Notes A;
        Notes B;



        public MainWindow()
        {
            InitializeComponent();

            

            C = new Notes("C", btnNoteC.Background);
            D = new Notes("D", btnNoteD.Background);
            E = new Notes("E", btnNoteE.Background);
            F = new Notes("F", btnNoteF.Background);
            G = new Notes("G", btnNoteG.Background);
            A = new Notes("A", btnNoteA.Background);
            B = new Notes("B", btnNoteB.Background);





        }

        public void Timer()
        {


            int min = int.Parse(TimerTextMenu.Text.Split(':')[0]);
            int sec = int.Parse(TimerTextMenu.Text.Split(':')[1]);
            _time = TimeSpan.FromMinutes(min) + TimeSpan.FromSeconds(sec);
            TimerText.Text = _time.ToString(@"mm\:ss");

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerText.Text = _time.ToString(@"mm\:ss");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    MainContentWindow.Visibility = Visibility.Collapsed;
                    menuWindow.Visibility = Visibility.Visible;
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }



        public void AddingFrets()
        {
            frets.Add(new Fret(E, 1, F));
            frets.Add(new Fret(B, 1, C));
            frets.Add(new Fret(E, 1, F));
            frets.Add(new Fret(G, 2, A));
            frets.Add(new Fret(D, 2, E));
            frets.Add(new Fret(A, 2, B));
            frets.Add(new Fret(E, 3, G));
            frets.Add(new Fret(B, 3, D));
            frets.Add(new Fret(D, 3, F));
            frets.Add(new Fret(A, 3, C));
            frets.Add(new Fret(G, 4, B));
            frets.Add(new Fret(E, 5, A));
            frets.Add(new Fret(B, 5, E));
            frets.Add(new Fret(G, 5, C));
            frets.Add(new Fret(D, 5, G));
            frets.Add(new Fret(A, 5, D));
            frets.Add(new Fret(B, 6, F));
            frets.Add(new Fret(E, 7, B));
            frets.Add(new Fret(G, 7, D));
            frets.Add(new Fret(D, 7, A));
            frets.Add(new Fret(A, 7, E));
            frets.Add(new Fret(E, 8, C));
            frets.Add(new Fret(B, 8, G));
            frets.Add(new Fret(A, 8, F));
            frets.Add(new Fret(G, 9, E));
            frets.Add(new Fret(D, 9, B));
            frets.Add(new Fret(E, 10, D));
            frets.Add(new Fret(B, 10, A));
            frets.Add(new Fret(G, 10, F));
            frets.Add(new Fret(D, 10, C));
            frets.Add(new Fret(A, 10, G));



            unselectedStrings = new List<string>();

            if (checkboxNoteE.IsChecked == false)
                unselectedStrings.Add((string)checkboxNoteE.Content);
            if (checkboxNoteB.IsChecked == false)
                unselectedStrings.Add((string)checkboxNoteB.Content);
            if (checkboxNoteG.IsChecked == false)
                unselectedStrings.Add((string)checkboxNoteG.Content);
            if (checkboxNoteD.IsChecked == false)
                unselectedStrings.Add((string)checkboxNoteD.Content);
            if (checkboxNoteA.IsChecked == false)
                unselectedStrings.Add((string)checkboxNoteA.Content);




            for (int i = 0; i < frets.Count; i++)
            {

                for (int j = 0; j < unselectedStrings.Count; j++)
                {
                    if (frets[i]._verticalNote._note == unselectedStrings[j])
                    {
                        frets.Remove(frets[i]);
                        i -= 1;
                        break;

                    }

                }
            }

            if (unselectedStrings.Count == 5)
            {
                frets.Clear();
            }


        }

        public async void AnswerAnim(bool answer)
        {
            var bc = new ColorConverter();
            Answer.Background = new SolidColorBrush(Colors.White);

            CircleEase easing = new CircleEase();
            easing.EasingMode = EasingMode.EaseInOut;



            ColorAnimation animAnswer = new ColorAnimation();
            animAnswer.To = Colors.Red;
            if (answer == true)
                animAnswer.To = (Color)bc.ConvertFrom("#3bd955");
            animAnswer.Duration = TimeSpan.FromSeconds(1.2);
            animAnswer.EasingFunction = easing;
            animAnswer.AutoReverse = true;
            Answer.Background.BeginAnimation(SolidColorBrush.ColorProperty, animAnswer);



            ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
            thicknessAnimation.From = new Thickness(5);
            thicknessAnimation.To = new Thickness(2);
            thicknessAnimation.Duration = TimeSpan.FromSeconds(1.2);
            thicknessAnimation.EasingFunction = easing;
            thicknessAnimation.AutoReverse = true;
            Answer.BeginAnimation(Border.BorderThicknessProperty, thicknessAnimation);



            DoubleAnimation textOpacity = new DoubleAnimation();
            textOpacity.To = 0;
            textOpacity.Duration = TimeSpan.FromSeconds(0.5);
            textAnswer.BeginAnimation(TextBox.OpacityProperty, textOpacity);
            await Task.Delay((int)(0.5 * 1000));
            textAnswer.Content = "✘";
            if (answer == true)
                textAnswer.Content = "✔";
            textOpacity.To = 1;
            textAnswer.BeginAnimation(TextBox.OpacityProperty, textOpacity);
            await Task.Delay((int)(1.2 * 1000));
            textAnswer.Content = "?";

        }

        private void updateNote()
        {
            currentFret = frets[rnd.Next(0, frets.Count)];

            BorderOfNote.BorderBrush = currentFret._verticalNote._color;
            NoteInBorder.Content = currentFret._verticalNote._note;
            LineOfFret.Content = currentFret._numOfFret;
            frets.Remove(currentFret);
            if (frets.Count == 0)
            {
                AddingFrets();
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(((Button)sender).Content) == currentFret._answerNote._note)
            {
                _timer.Stop();
                Timer();
                AnswerAnim(true);
                updateNote();
            }
            else
                AnswerAnim(false);
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            menuWindow.Visibility = Visibility.Hidden;
            AddingFrets();
            if (unselectedStrings.Count != 5)
                updateNote();
            Timer();

            MainContentWindow.Visibility = Visibility.Visible;


        }

        private void goToMenuButton(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            MainContentWindow.Visibility = Visibility.Collapsed;
            menuWindow.Visibility = Visibility.Visible;
        }
    }
}
