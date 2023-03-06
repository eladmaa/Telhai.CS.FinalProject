using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telhai.CS.APIServer.Models;

namespace Telhai.CS.FinalProject
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private const float MAX_TEST_DURATION = 3f;
        private const float MIN_TEST_DURATION = 1f;
        static int id = 0;
        private ObservableCollection<Exam> exams = new ObservableCollection<Exam>();

        public TeacherWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded_1;
            examsList.ItemsSource = exams;
        }
     
        private async void btn_AddExam_Click(object sender, RoutedEventArgs e)
        {
            id++;
            string idCounter = id.ToString();
            Exam s = new Exam (   "Name_" + idCounter );
            await HttpExamRepository.Instance.AddExamAsync(s);
            //Reload
            List<Exam> list = await HttpExamRepository.Instance.GetAllExamsAsync();
            examsList.ItemsSource = list;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            for (float i = MIN_TEST_DURATION; i <= MAX_TEST_DURATION; i += 0.5f)
            {
                time_duration.Items.Add(i.ToString());
            }
            time_duration.Items.Insert(0, "Choose exam duration");
            time_duration.SelectedIndex = 0;
        }

        private async void btn_AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                Question q = new Question();
                await HttpExamRepository.Instance.AddQuestionAsync(q);
            }
        }
    }
}
