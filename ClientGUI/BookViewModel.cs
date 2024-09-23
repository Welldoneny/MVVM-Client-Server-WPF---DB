using MVVM;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Windows;
using System.IO;
using System.Windows.Shapes;
using System.Windows.Interop;

namespace ClientGUI
{
    class BookViewModel : INotifyPropertyChanged
    {
        private BookModel selectedBook;
        bool isLoaded = false;
        private static UdpClient udpClient = new UdpClient(8002);
        public static ObservableCollection<BookModel> Books { get; set; }

        private RelayCommand loadFromDBCommand;
        public RelayCommand LoadFromDBCommand
        {
            get
            {
                return loadFromDBCommand ?? (loadFromDBCommand = new RelayCommand(obj =>
                {
                    if (!isLoaded)
                    {
                        Books.Clear();
                        SendMessage("print -1");
                        ReceiveMessage();
                        isLoaded = true;
                    }
                }
                 ));
            }
        }

        private RelayCommand saveToDBCommand;
        public RelayCommand SaveToDBCommand
        {
            get
            {
                return saveToDBCommand ?? (saveToDBCommand = new RelayCommand(obj =>
                {
                    string data = "save ";
                    foreach (var book in Books)
                    {
                        data += book.Name;
                        data += ",";
                        data += book.Autor;
                        data += ",";
                        data += book.Genre;
                        data += ",";
                        data += book.Year.ToString();
                        data += ",";
                        data += book.Price.ToString();
                        data += ",";
                        data += book.IsAvaliable.ToString();
                        data += "\n";
                    }
                    string PATH = @".\logSW.txt";
                    using (var sw = new StreamWriter(PATH))
                    {
                        sw.WriteLine(data);
                    }
                    SendMessage(data);
                    ReceiveMessage();
                }
                 ));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(obj =>
                {
                    isLoaded = false;
                    Books.Remove(SelectedBook);
                }
                 ));
            }
        }
        
        public BookModel SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }
       
        public BookViewModel() 
        {
            Books = new ObservableCollection<BookModel>();
        }

        private static void ReceiveMessage()
        {
            IPEndPoint remoteIp = (IPEndPoint)udpClient.Client.LocalEndPoint;
            try
            {
                byte[] data = udpClient.Receive(ref remoteIp); 
                string message = Encoding.Unicode.GetString(data);
                if (message == "Данные успешно сохранены")
                {
                    MessageBox.Show(message);
                    return;
                }
                string[] lines = message.Split('\n');
                int number = 1;
                foreach (string line in lines)
                {
                    string[] words = line.Split(',');
                    Books.Add(new BookModel
                    {
                        Name = words[0],
                        Autor = words[1],
                        Genre = words[2],
                        Year = Int32.Parse(words[3]),
                        Price = double.Parse(words[4]),
                        IsAvaliable = bool.Parse(words[5]),
                        Id = number++
                    });
                }
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SendMessage(string msg)
        {
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(msg); 
                udpClient.Send(data, data.Length, "127.0.0.1", 8001);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
