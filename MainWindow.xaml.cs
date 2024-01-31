using System.IO;
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

namespace My_Learn_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string NotesFileName = "notes.txt";
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closed += Window_Closed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MaxHeight = this.Height;
            this.MaxWidth = this.Width;
            LoadNotes();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveNotes();
        }

        private void SaveNotes()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(NotesFileName))
                {
                    foreach (UIElement element in MainGrid.Children)
                    {
                        if (element is TextBox textBox)
                        {
                            writer.WriteLine($"{textBox.Name}:{textBox.Text}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving notes: {ex.Message}");
            }
        }

        private void LoadNotes()
        {
            try
            {
                if (File.Exists(NotesFileName))
                {
                    using (StreamReader reader = new StreamReader(NotesFileName))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (!string.IsNullOrEmpty(line))
                            {
                                string[] parts = line.Split(':');
                                if (parts.Length == 2)
                                {
                                    if (FindName(parts[0]) is TextBox textBox)
                                    {
                                        textBox.Text = parts[1];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notes: {ex.Message}");
            }
        }
    }
}