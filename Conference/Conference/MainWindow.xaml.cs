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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Conference
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (ConferenceContext db = new ConferenceContext())
            {
                EventsListView.ItemsSource = db.Events.ToList();

                List<string> sortList = new List<string>() { "Сначала ближайшие", "Сначала поздние" };
                EventSortComboBox.ItemsSource = sortList.ToList();
                List<string> filtertList = db.Events.Select(u => u.Direction).Distinct().ToList();
                filtertList.Insert(0, "Все направления");
                EventFilterComboBox.ItemsSource = filtertList.ToList();
            }         


        }

        private void EventFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ConferenceContext db = new ConferenceContext())
            {
                if (db.Events.Select(u => u.Direction).Distinct().ToList().Contains(EventFilterComboBox.SelectedValue))
                {
                    EventsListView.ItemsSource = db.Events.Where(u => u.Direction == EventFilterComboBox.SelectedValue).ToList();
                }
                else
                {
                    EventsListView.ItemsSource = db.Events.ToList();
                }
            }
        }

        private void EventSortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ConferenceContext db = new ConferenceContext())
            {
                if (EventSortComboBox.SelectedValue == "Сначала поздние")
                {
                    EventsListView.ItemsSource = db.Events.OrderByDescending(u => u.Date).ToList();
                }

                if (EventSortComboBox.SelectedValue == "Сначала ближайшие")
                {
                    EventsListView.ItemsSource = db.Events.OrderBy(u => u.Date).ToList();
                }
            }
        }
    }
}
