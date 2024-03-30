using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laboratorio03
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

        private void btnDataRead_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1504-11\\SQLEXPRESS;Initial Catalog=tecsupdb;User Id=userTecsup;Password=123456";
            List<Student> students = new List<Student>();
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int IdStudent = reader.GetInt32("StudendID");
                    string FirstName = reader.GetString("FirstName");
                    string LastName = reader.GetString("LastName");

                    students.Add(new Student { Id = IdStudent, FirstName = FirstName, LastName = LastName});

                }

                connection.Close();


                dgStudents.ItemsSource = students;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDataTable_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1504-11\\SQLEXPRESS;Initial Catalog=tecsupdb;User Id=userTecsup;Password=123456";

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);


                DataTable dataTable = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dataTable);

                connection.Close();


                dgStudents.ItemsSource = dataTable.DefaultView;
        }
         catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscarNombre_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1504-11\\SQLEXPRESS;Initial Catalog=tecsupdb;User Id=userTecsup;Password=123456";

            string name = txtboxNombre.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Students WHERE FirstName = @Name", connection);
                command.Parameters.AddWithValue("@Name", name);

                DataTable dataTable = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dataTable);

                dgStudents.ItemsSource = dataTable.DefaultView;
            }
        }

    }
}