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
    /// Логика взаимодействия для AddParticipantWindow.xaml
    /// </summary>
    public partial class AddParticipantWindow : Window
    {
        public AddParticipantWindow()
        {
            InitializeComponent();

            using (ConferenceContext db = new ConferenceContext())
            {
                GenderComboBox.ItemsSource = db.Participants.Select(p => p.Gender).Distinct().ToList();
                RoleComboBox.ItemsSource = db.Participants.Select(p => p.RoleNavigation).Distinct().ToList();
                DirectionComboBox.ItemsSource = db.Participants.Select(p => p.Direction).Distinct().ToList();

                if (AttachToEventCheckBox.IsChecked == true)
                {
                    DirectionStackPanel.Visibility = Visibility.Visible;
                    DirectionComboBox.ItemsSource = db.Participants.Select(p => p.)
                }

            }
        }

        private void AddImageButton(object sender, RoutedEventArgs e)
        {

        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
