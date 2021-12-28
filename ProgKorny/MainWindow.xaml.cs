using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProgKorny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void quizStart_btn_Click(object sender, RoutedEventArgs e)
        {
            Quiz q = new Quiz();
            q.Show();
            this.Close();
        }

        private void addQuestions_Click(object sender, RoutedEventArgs e)
        {
            const string URL = "https://opentdb.com/api.php";
            string urlParams = "?amount=10&type=multiple";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            JArray jarray = null;

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(urlParams).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                json = json.Substring(29);
                json = json.Remove(json.Length - 1, 1);
                jarray = JArray.Parse(json);
            }
            client.Dispose();
            string connectionString = "SERVER=localhost;DATABASE=prog_korny;UID=root;PASSWORD=;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            foreach(var j in jarray)
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT IGNORE INTO quiz(question,ans_1,ans_2,ans_3,ans_4,correct_ans) VALUES(?question,?ans_1,?ans_2,?ans_3,?ans_4,?correct_ans)";
                cmd.Parameters.Add("?question", MySqlDbType.VarChar).Value = j["question"].ToString().Replace("&#039","'").Replace("&quot;","\"");
                cmd.Parameters.Add("?ans_1", MySqlDbType.VarChar).Value = j["correct_answer"].ToString().Replace("&#039", "'").Replace("&quot;", "\""); 
                cmd.Parameters.Add("?ans_2", MySqlDbType.VarChar).Value = j["incorrect_answers"][0].ToString().Replace("&#039", "'").Replace("&quot;", "\"");
                cmd.Parameters.Add("?ans_3", MySqlDbType.VarChar).Value = j["incorrect_answers"][1].ToString().Replace("&#039", "'").Replace("&quot;", "\"");
                cmd.Parameters.Add("?ans_4", MySqlDbType.VarChar).Value = j["incorrect_answers"][2].ToString().Replace("&#039", "'").Replace("&quot;", "\""); 
                cmd.Parameters.Add("?correct_ans", MySqlDbType.VarChar).Value = j["correct_answer"].ToString().Replace("&#039", "'").Replace("&quot;", "\""); 
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            MessageBox.Show("Success!");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }

}
