using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telhai.CS.FinalProject;


namespace Telhai.CS.APIServer.Models
{
    public class HttpExamRepository
    {
        private List<Exam> examList;
        HttpClient clientApi;

        static private HttpExamRepository _instance = null;

        private HttpExamRepository() 
        {
            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7070");
        }
        public static HttpExamRepository Instance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new HttpExamRepository();
                }
                return _instance;
            }
        }
        public async Task<List<Exam>> GetAllExamsAsync()
        {
            HttpResponseMessage response = await clientApi.GetAsync("api/exams");
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string? dataString = await response.Content.ReadAsStringAsync();
                var exams = JsonSerializer.Deserialize<List<Exam>>(dataString);
                if (exams != null)
                {
                    return exams;
                }
            }
            return new List<Exam>();
        }
        public async Task AddExamAsync(Exam exam)
        {
            try
            {

                //var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonStudentString = JsonSerializer.Serialize<Exam>(exam);

                var content = new StringContent(jsonStudentString, Encoding.UTF8, "application/json");
                var response = await clientApi.PostAsync("api/students", content);
                if (response != null)
                {
                    //  var jsonString = await response.Content.ReadAsStringAsync();
                    // return JsonConvert.DeserializeObject<object>(jsonString);
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateStudent(Exam exam)
        {


        }

        public async Task RemoveExam(string id)
        {
            HttpResponseMessage response = await clientApi.DeleteAsync("api/exams/" + id);
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                //..
            }
        }

        internal Task AddQuestionAsync(Question q)
        {
            throw new NotImplementedException();
        }
    }
}
