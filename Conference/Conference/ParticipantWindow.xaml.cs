using Conference.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Conference
{
    /// <summary>
    /// Логика взаимодействия для ParticipantWindow.xaml
    /// </summary>
    public partial class ParticipantWindow : Window
    {
        public ParticipantWindow(Participant participant)
        {
            InitializeComponent();

            using (ConferenceContext db = new ConferenceContext())
            {
                BitmapImage image = new BitmapImage(new Uri(participant.ImagePath));
                image.CacheOption = BitmapCacheOption.OnLoad;
                ImageBoxPath.Source = image;

                if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 9 && (Convert.ToInt32(DateTime.Now.ToString("HH")) < 11))
                {
                    GreetingTextBlock.Text = ($"Доброе утро, \n{participant.Surname} {participant.Name} {participant.Patronymic}!");
                }

                else if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 11 && (Convert.ToInt32(DateTime.Now.ToString("HH")) < 18))
                {
                    GreetingTextBlock.Text = ($"Добрый день, \n{participant.Surname} {participant.Name} {participant.Patronymic}!");
                }

                else
                {
                    GreetingTextBlock.Text = ($"Добрый вечер, \n{participant.Surname} {participant.Name} {participant.Patronymic}!");
                }

                if (participant.RoleId == 1)
                {
                    PartWindow.Title = "Окно жюри";
                    TitleTextBlock.Text = "Окно жюри";
                    EventsButton.Visibility = Visibility.Collapsed;
                    ParticipantsButton.Visibility = Visibility.Collapsed;
                    JuryButton.Visibility = Visibility.Collapsed;
                }

                else if (participant.RoleId == 2)
                {
                    PartWindow.Title = "Окно модератора";
                    TitleTextBlock.Text = "Окно модератора";
                    EventsButton.Visibility = Visibility.Collapsed;
                    ParticipantsButton.Visibility = Visibility.Collapsed;
                    JuryButton.Visibility = Visibility.Collapsed;
                }

                else if (participant.RoleId == 3)
                {
                    PartWindow.Title = "Окно организатора";
                    TitleTextBlock.Text = "Окно организатора";
                }

                else
                {
                    PartWindow.Title = "Окно участника";
                    TitleTextBlock.Text = "Окно участника";
                    EventsButton.Visibility = Visibility.Collapsed;
                    ParticipantsButton.Visibility = Visibility.Collapsed;
                    JuryButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void MyProfileButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void JuryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ParticipantsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EventsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void JuryModerButton_Click(object sender, RoutedEventArgs e)
        {
            new AddParticipantWindow().ShowDialog();
        }
    }
}
