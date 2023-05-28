using Conference.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        Participant? currentParticipant;
        string? oldImage;
        string? newImage;
        string? newImagePath;

        public AddParticipantWindow()
        {
            InitializeComponent();

            using (ConferenceContext db = new ConferenceContext())
            {
                GenderComboBox.ItemsSource = db.Participants.Select(p => p.Gender).Distinct().ToList();
                List<string> RolesList = new List<string>() { "Жюри", "Модератор" };
                RoleComboBox.ItemsSource = RolesList.ToList();
                DirectionComboBox.ItemsSource = db.Events.Select(p => p.Direction).Distinct().ToList();
                EventComboBox.ItemsSource = db.Events.Select(p => p.Name).Distinct().ToList();

            }
        }

        private void AddImageButton(object sender, RoutedEventArgs e)
        {
            Stream myStream;

            if (currentParticipant != null)
            {
                oldImage = System.IO.Path.Combine(Environment.CurrentDirectory, $"images/{currentParticipant.Photo}");
            }
            else
            {
                oldImage = null;
            }

            // проверяем, есть ли изображение у товара и запоминаем путь к изображению сейчас
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                if ((myStream = dlg.OpenFile()) != null)
                {
                    dlg.DefaultExt = ".png";
                    dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                    dlg.Title = "Open Image";
                    dlg.InitialDirectory = "./";

                    // Предпросмотр изображения
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.UriSource = new Uri(dlg.FileName);

                    image.DecodePixelWidth = 200;
                    image.DecodePixelHeight = 300;
                    UserImageBoxPath.Source = image;
                    image.EndInit();

                    try
                    {
                        newImage = dlg.SafeFileName;
                        newImagePath = dlg.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                myStream.Dispose();
            }

        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(SurnameTextBox.Text))
                errors.AppendLine("Укажите фамилию");
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(PatronymicTextBox.Text))
                errors.AppendLine("Укажите отчество");
            if (string.IsNullOrWhiteSpace(GenderComboBox.Text))
                errors.AppendLine("Укажите пол");
            if (string.IsNullOrWhiteSpace(RoleComboBox.Text))
                errors.AppendLine("Укажите роль");

            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
                errors.AppendLine("Укажите email");
            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
                errors.AppendLine("Укажите номер телефона");
            if (string.IsNullOrWhiteSpace(DirectionComboBox.Text))
                errors.AppendLine("Укажите направление");
            if (string.IsNullOrWhiteSpace(UserPasswordBox.Text))
                errors.AppendLine("Установите пароль");

            //
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            using (ConferenceContext db = new ConferenceContext())
            {
                int tempRole = 0;

                if (RoleComboBox.Text == "Жюри")
                {
                    tempRole = 1;
                }
                else if(RoleComboBox.Text == "Модератор") 
                {
                    tempRole = 2; 
                }

                try
                {
                    Participant participant = new Participant()
                    {
                        Surname = SurnameTextBox.Text,
                        Name = NameTextBox.Text,
                        Patronymic = PatronymicTextBox.Text,
                        Gender = GenderComboBox.Text,
                        RoleId = tempRole,
                        Email = EmailTextBox.Text,
                        Phone = PhoneTextBox.Text,
                        Direction = DirectionComboBox.Text,
                        Event = EventComboBox.Text,
                        Password = UserPasswordBox.Text,
                        Photo = ImageBox.Text
                    };

                    db.Participants.Add(participant);


                    if (String.IsNullOrEmpty(newImage))
                    {
                        participant.Photo = "picture.png";
                        BitmapImage image = new BitmapImage(new Uri(participant.ImagePath));
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        UserImageBoxPath.Source = image;
                    }
                    else
                    {
                        string newRelativePath = $"{System.DateTime.Now.ToString("HHmmss")}_{newImage}";
                        participant.Photo = newRelativePath;

                        File.Copy(newImagePath, System.IO.Path.Combine(Environment.CurrentDirectory, $"images/{newRelativePath}"));

                        BitmapImage image = new BitmapImage(new Uri(participant.ImagePath));
                        image.CacheOption = BitmapCacheOption.OnLoad;

                        UserImageBoxPath.Source = image;
                    }


                    db.SaveChanges();

                    MessageBox.Show("Пользователь успешно добавлен!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }


            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AttachToEventCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            EventTextBlock.Visibility = Visibility.Visible;
            EventComboBox.Visibility = Visibility.Visible;
        }

        private void AttachToEventCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            EventTextBlock.Visibility = Visibility.Collapsed;
            EventComboBox.Visibility = Visibility.Collapsed;
        }
    }
}
