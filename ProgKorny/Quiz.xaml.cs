using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
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

namespace ProgKorny
{
    /// <summary>
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Window
    {
        string correctAnswer;
        string result = "";
        int index = 0;
        DataTable table = null;
        int prize = 0;
        int[] randomIndexes = new int[10];
        public Quiz()
        {
            InitializeComponent();

            string connectionString = "SERVER=localhost;DATABASE=prog_korny;UID=root;PASSWORD=;";

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlDataAdapter sqldata = new MySqlDataAdapter("SELECT * from quiz", conn);
            table = new DataTable();
            sqldata.Fill(table);
            for (int i = 0; i < randomIndexes.Length; i++)
            {
                Random rnd = new Random();
                var temp = rnd.Next(0, table.Rows.Count-1);
                if (!randomIndexes.Contains(temp))
                {
                    randomIndexes[i] = temp;
                }
                else
                {
                    while (randomIndexes.Contains(temp))
                        temp = rnd.Next(0, table.Rows.Count - 1);
                    randomIndexes[i] = temp;
                }

            }
            questionLabel.Content = table.Rows[randomIndexes[index]]["question"];
            button_1.Content = table.Rows[randomIndexes[index]]["ans_1"].ToString();
            button_2.Content = table.Rows[randomIndexes[index]]["ans_2"].ToString();
            button_3.Content = table.Rows[randomIndexes[index]]["ans_3"].ToString();
            button_4.Content = table.Rows[randomIndexes[index]]["ans_4"].ToString();
            correctAnswer = table.Rows[randomIndexes[index]]["correct_ans"].ToString();
        }

        private void Button_click(object sender, RoutedEventArgs e)
        {
            result = "Incorrect!" + " The correct answer is: " + correctAnswer;
            Button b = (Button)sender;
            if (b.Content.ToString() == correctAnswer)
            {
                result = "Correct!";

            }
            if (result != "Correct!")
            {
                b.Background = Brushes.Red;
                MessageBox.Show(result);
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
            Update();
            prize = IncPrize();
            textBlock_prize.Text = prize.ToString();
        }

        private void Update()
        {
            List<Button> buttons = new() { button_1, button_2, button_3, button_4 };
            if (result == "Correct!")
            {
                if (index < randomIndexes.Count()-1)
                {
                    index++;
                    questionLabel.Content = table.Rows[randomIndexes[index]]["question"];
                    Random rnd = new Random();
                    HashSet<int> answers = new();
                    while(answers.Count != 4)
                    {
                        var temp = rnd.Next(2, 6);
                        answers.Add(temp);
                    }
                    for (var i = 0; i < buttons.Count; i++)
                    {
                        buttons[i].Content = table.Rows[randomIndexes[index]][answers.ElementAt(0)].ToString();
                        answers.Remove(answers.ElementAt(0));
                        buttons[i].Visibility = Visibility.Visible;
                    }
                    correctAnswer = table.Rows[randomIndexes[index]]["correct_ans"].ToString();
                }
                else
                {
                    MessageBox.Show("Congratulations! You all the questions correctly, you won:  " + prize);
                    System.Windows.Application.Current.Shutdown();
                }
                if (index % 3 == 0 && index != 9)
                {
                    MessageBoxResult result = MessageBox.Show("Would you like to continue?", "Quiz", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            break;
                        case MessageBoxResult.No:
                            MessageBox.Show("You won: " + prize);
                            this.Close();
                            MainWindow m = new MainWindow();
                            m.Show();
                            break;
                    }
                }
            }
        }

        private int IncPrize()
        {
            return index == 1 ? prize + 50 : (int)(2 * prize);
        }

        private void fiftyFifty_Click(object sender, RoutedEventArgs e)
        {
            List<Button> buttons = new() { button_1, button_2, button_3, button_4 };
            for (var i = 0; i<buttons.Count; i++)
            {
                if(buttons[i].Content.ToString() == correctAnswer)
                {
                    buttons.RemoveAt(i);
                }
            }
            Random rnd = new Random();
            var count = 0;
            for (var i = 0; i < buttons.Count; i++)
            {
                if(count <= 1)
                {
                    var index = rnd.Next(0, buttons.Count);
                    buttons[index].Visibility = Visibility.Hidden;
                    buttons.RemoveAt(index);
                    count++;
                }
            }
            fiftyFifty.Visibility = Visibility.Hidden;
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();

            using (Stream stream = client.OpenRead("http://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&explaintext=1&titles="+correctAnswer))
            using (StreamReader reader = new StreamReader(stream))
            {
                JsonSerializer ser = new JsonSerializer();
                Result result = ser.Deserialize<Result>(new JsonTextReader(reader));
                Help h = new();
                h.Show();
                foreach (Page page in result.query.pages.Values)
                    h.wikiAnswer_textBox.Text += page.extract;
                if (h.wikiAnswer_textBox.Text == null || h.wikiAnswer_textBox.Text == "")
                {
                    h.wikiAnswer_textBox.Text = "I could not find anything!";
                }
                Button b = (Button)sender;
                b.Visibility = Visibility.Hidden;
            }
        }
    }
}
