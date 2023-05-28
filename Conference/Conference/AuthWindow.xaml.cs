using Conference.Models;
using Conference.Infrastructure;
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
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Conference
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        bool verify = true;
        int verifyCheck = 0;

        public AuthWindow()
        {
            InitializeComponent();

            captchaTextBlock.Visibility = Visibility.Collapsed;
            captchaTextBox.Visibility = Visibility.Collapsed;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (ConferenceContext db = new ConferenceContext())
            {
                if (captchaTextBlock.Visibility == Visibility.Visible)
                {
                    if (captchaTextBlock.Text == captchaTextBox.Text)
                    {
                        verify = true;
                    }
                }

                Participant participant = db.Participants.Where(u => u.Id == Convert.ToInt32(IdTextBox.Text) && u.Password == PasswordBox.Password).Include(u => u.RoleNavigation).FirstOrDefault();

                

                if (participant != null && verify)
                {
                    new ParticipantWindow(participant).Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неуспешная авторизация");
                    verifyCheck += 1;

                    // captcha view
                    
                    captchaTextBlock.Text = CaptchaBuilder.Refresh();
                    verify = false;

                    if (verifyCheck > 3)
                    {
                        disableButton();
                        captchaTextBox.Visibility = Visibility.Visible;
                        captchaTextBlock.Visibility = Visibility.Visible;
                        captchaTextBlock.Text = CaptchaBuilder.Refresh();
                    }
                }
            }
        }

        async void disableButton()
        {
            LoginButton.IsEnabled = false;
            await Task.Delay(TimeSpan.FromSeconds(10));
            LoginButton.IsEnabled = true;
        }
    }
}
