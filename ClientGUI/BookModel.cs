using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientGUI 
{
    class BookModel: INotifyPropertyChanged
    {
        private string name; // название книги
        private string autor; // автор книги
        private string genre; // жанр книги
        private int year; // год выпуска
        private double price; // цена книги
        private bool isAvalible; // есть ли книга в продаже
        private int id;
        public string Name 
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name");}
        }
        public string Autor 
        {
            get { return autor; }
            set { autor = value; OnPropertyChanged("Autor");}

        }
        public string Genre 
        { 
            get { return genre; }
            set { genre = value; OnPropertyChanged("Genre");}
        }
        public int Id
        { 
            get { return id; }
            set { id = value; OnPropertyChanged("Id");}
        } // айдишник для базы данных
        public int Year 
        { 
            get { return year; }
            set { year = value; OnPropertyChanged("Year"); }
        }
        public double Price 
        {
            get { return price; }
            set { price = value; OnPropertyChanged("Price"); }
        }

        public bool IsAvaliable 
        { 
            get { return isAvalible; }
            set {  isAvalible = value; OnPropertyChanged("IsAvaliable"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
