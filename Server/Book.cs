namespace Server
{
    internal class Book
    {
        //private string name; // название книги
        //private string autor; // автор книги
        //private string genre; // жанр книги
        //private int year; // год выпуска
        //private double price; // цена книги
        //private bool isAvalible; // есть ли книга в продаже
        public string? Name { get; set; }
        public string? Autor { get; set; }
        public string? Genre { get; set; }
        public int Id { get; set; } // айдишник для базы данных
        public int Year { get; set; }
        public double Price { get; set; }

        public bool IsAvaliable { get; set; }

        //public Book(string line)
        //{
        //    string[] str = line.Split(',');
        //    try
        //    {
        //        Name = str[0];
        //        Autor = str[1];
        //        Genre = str[2];
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Неправильно введены либо название, либо автор, либо жанр");
        //    }
        //    if (!Int32.TryParse(str[3], out year) && year < 0)
        //    {
        //        throw new Exception("Неправильно введен год");
        //    }
        //    if (!Double.TryParse(str[4], out price) && price < 0)
        //    {
        //        throw new Exception("Введена неправильно цена");
        //    }
        //    var condition = str[5] == "TRUE";
        //    isAvalible = condition ? true : false;
        //}
    }
}
