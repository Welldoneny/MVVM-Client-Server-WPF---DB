using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NLog;
namespace Server
{
    internal class Server
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        static string message = string.Empty;
        public static UdpClient udpServer;
        static List<Book> Books = new List<Book>();
        static string print(string answer, int line)
        {
            answer = "\nНазвание  Автор  Жанр  Год выпуска  Цена  Доступна в библиотеке";

           if (line == -1)
           {
                answer = string.Empty;
                foreach (var item in Books)
                {
                    answer += item.Name + "," + item.Autor + "," + item.Genre + "," + item.Year.ToString() +
                    "," + item.Price.ToString() + "," + item.IsAvaliable.ToString() + "\n";
                }
                return answer;
           }
           if (line == 0)
           {
                int number = 1;
                foreach (var item in Books)
                {
                    answer += ("\n" + number.ToString() + ": \"" + item.Name + "\" " + item.Autor + ": " + item.Genre + " " +
                    item.Year.ToString() + " " + item.Price.ToString() + " " + item.IsAvaliable.ToString());
                    number++;
                }
           }
           else
           {
                line = line - 1;
                try
                {
                    answer += ("\n" + (line+1).ToString() + ": \"" + Books[line].Name + "\" " + Books[line].Autor + ": " + Books[line].Genre +
                    " " + Books[line].Year.ToString() + " " + Books[line].Price.ToString() + " " + Books[line].IsAvaliable.ToString());
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                    answer = "Введена нещуствующая строка";
                }
           }
        return answer;
            
        }
        static string delete(int line)
        {
            if (line == 0)
            {
                Books.Clear();
                using (BookContext db = new BookContext())
                {
                    foreach (Book item in db.Books.ToList())
                    {
                        db.Remove(item);
                    }
                    db.SaveChanges();
                }
                return "Удалены все записи";
            }
            try
            {
                using (BookContext db = new BookContext())
                {
                    Book b = db.Books.ElementAt(line-1);
                    db.Books.Remove(b);
                    db.SaveChanges();
                }
                Books.RemoveAt(line - 1);
                return "Удалена запись под номером - " + line.ToString();
            }
            catch (Exception e)
            {
                return ($"Ошибка: {e.Message}");
            }

        }
        static string write(string str)
        {
            try
            {
                string[] strings = str.Split(',');
                Book b = new Book
                {
                    Name = strings[0],
                    Autor = strings[1],
                    Genre = strings[2],
                    Year = Int32.Parse(strings[3]),
                    Price = Double.Parse(strings[4]),
                    IsAvaliable = bool.Parse(strings[5])
                };
                Books.Add(b);
                using (BookContext db = new BookContext())
                {
                    db.Books.Add(b);
                    db.SaveChanges();
                }
                return "Данные успешно добавлены";
            }
            catch (Exception)
            {

                return "Неправильно введены данные";
            }
        }
        static void Main(string[] args)
        {
            udpServer = new UdpClient(8001);
            Console.WriteLine("Server is working");
            Logger.Info("Запуск сервера");

            // читаем от туда информацию
            using (BookContext db = new BookContext())
            {

                var bb = db.Books.ToList();
                foreach (Book b in bb)
                {
                    Books.Add((Book)b);
                }
            }
         
            while (true)
            {
                try
                {
                    ReceiveMessage();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                }
            }
        }
        private static void saveDB(string str)
        {
            string[] books = str.Split('\n');
            //Console.WriteLine(str);
            
            using (BookContext db = new BookContext())
            {
                Books.Clear();
                db.Books.ExecuteDelete();               
                db.SaveChanges();

                for (int i = 0; i < books.Length-1; i++)
                {
                    string book = books[i];
                    string[] strings = book.Split(",");
                    Book b = new Book
                    {
                        Name = strings[0],
                        Autor = strings[1],
                        Genre = strings[2],
                        Year = Int32.Parse(strings[3]),
                        Price = Double.Parse(strings[4]),
                        IsAvaliable = bool.Parse(strings[5])
                    };
                    Books.Add(b);
                    db.Books.Add(b);
                    db.SaveChanges();
                }
            }
        }
        private static void SendMessage(string[] command)
        {
            string answer = string.Empty;
            string mainCommand = command[0];
            int argument = -1;
            if (mainCommand == "print" || mainCommand == "delete")
            {
                try
                {
                    argument = Int32.Parse(command[1]);
                }
                catch (Exception)
                {
                    mainCommand = "#service";
                }
            }
            switch (mainCommand)
            {
                case "print":
                    answer = print(answer, argument);
                    break;
                case "delete":
                    answer = delete(argument);
                    break;
                case "write":
                    answer = write(command[1]);
                    break;
                case "save":
                    saveDB(command[1]);
                    answer = "Данные успешно сохранены";
                    break;
                case "#service":
                    answer = "Не введен аргумент, либо введен с ошибкой";
                    break;
                default:
                    answer = "Неизвестная команда";
                    break;
            }
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(answer);
                udpServer.Send(data, data.Length, "127.0.0.1", 8002);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private static void ReceiveMessage()
        {
            IPEndPoint remoteIp = (IPEndPoint)udpServer.Client.LocalEndPoint;
            try
            {
                byte[] data = udpServer.Receive(ref remoteIp);
                message = Encoding.Unicode.GetString(data);
                string[] command = message.Split(' ', 2);
                SendMessage(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.Error(ex.Message);
            }
        }
    }
}
