using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        static int Qid = 0;
        private ObservableCollection<Exam> exams = new ObservableCollection<Exam>();
        private ObservableCollection<Question> questionsList = new ObservableCollection<Question>();
        private ObservableCollection<String> answersList = new ObservableCollection<String>();
        public TeacherWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded_1;
            examsList.ItemsSource = exams;
            this.QuestionsLB.ItemsSource = questionsList;
            AnswersListBox.ItemsSource = answersList;
        }
     
        private async void btn_AddExam_Click(object sender, RoutedEventArgs e)
        {
            id++;
            string idCounter = id.ToString();
            Exam s = new Exam ("Name_" + idCounter );
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
            Qid++;    
            Question q = new Question(Qid);
            await HttpExamRepository.Instance.AddQuestionAsync(q);
            
        }

        private async void textQuestion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                if (this.QuestionsLB.SelectedItem is Question q)
                {
                    // Http put update question
                    await HttpExamRepository.Instance.updateQuestionText(this.)
                }
                
            }
        }

        private async void AddAnswer_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.QuestionsLB.SelectedItem is Question q)
            {
                q.AnswersCount++;
                await HttpExamRepository.Instance.UpdateQuestionAnswer(); //http put update question
                Correct_answer.Items.Add(q.AnswersCount);
            }
            
                
            
        }

        private async void AnswersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.AnswersListBox.SelectedItem is String ans)
            {
                await HttpExamRepository.Instance.UpdateQuestionAnswer(ans);
            }
        }

        private async void txtExame_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                // Http put update question
                await HttpExamRepository.Instance.updateExamName(this.txtExame.Text);
            }
        }

        private async void txtID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                // Http put update question
                await HttpExamRepository.Instance.updateExamId(this.txtID.Text);
            }
        }

        private async void txtTeacher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                // Http put update question
                await HttpExamRepository.Instance.updateExamTeacherName(this.txtTeacher.Text);
            }
        }

        private void time_begining_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                // Http put update question
                
            }
        }

        private void time_duration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                // Http put update question
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                // Http put update question
            }
        }

        public /* List<Exam> */ void allExams()
        {
          //  throw new NotImplementedException();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            /*
            string ext; //will be used to get file extension
            repo.SaveStudents();
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
           "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
           "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                txtPicture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                ext = System.IO.Path.GetExtension(openFileDialog.FileName);
                File.Copy(openFileDialog.FileName, Directory.GetCurrentDirectory() + "\\Pictures\\" + this.txtId.Text + ext);

                if (this.listBoxStudents.SelectedItem is Student s)
                {
                    s.picture = Directory.GetCurrentDirectory() + "\\Pictures\\" + this.txtId.Text + ext;
                }
            }
            */
        }
        private void btnLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
           "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
           "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                this.QuestionImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void QuestionsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.QuestionsLB.SelectedItem is Question q)
            {
                this.textQuestion.Text = q.content;
                foreach (String ans in q.answers)
                { this.answersList.Add(ans); }
            }
        }

        private async void Correct_answer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.examsList.SelectedItem is Exam ex)
            {
                if(this.QuestionsLB.SelectedItem is Question q)
                {
                    await HttpExamRepository.Instance.updateExamQuestionCorrect(ex, q, this.Correct_answer);
                }
            }
        }
        private async void exame_Datepicker_ValueChanged(object sender, EventArgs e)
        {
            await HttpExamRepository.Instance.updateExamDate(this.exame_Datepicker);
        }
        private void examsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                this.txtExame.Text = ex.examName;
                this.txtID.Text = ex.id;
                this.txtTeacher.Text = ex.TeacherName;
                this.exame_Datepicker = ex.date;
            }
        }
    }
}
