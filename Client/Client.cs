using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Client
{
    internal class Client
    {
        static void greeting()
        {
            //Окно приветствия
            Console.WriteLine("Вас приветствует программа учета книг в книжном магазине!\n" +
                "Для работы с программой вводите команду и аргумент через пробел\n" +
                "print [0]- Вывод строки по номеру. 0 - выведет все строки\n" +
                "delete [0] - Удаление из файла строки по номеру. 0 - удалит все файлы\n" +
                "write [Название,Автор,Жанр,Год выпуска,Цена,Доступна в библиотеке] - Запись в конец файла");
        }
        public static UdpClient udpClient;
        static void Main(string[] args)
        {
            udpClient = new UdpClient(8002);
            greeting();
            while (true)
            {
                try
                {
                    SendMessage();
                    ReceiveMessage();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        private static void ReceiveMessage()
        {
            IPEndPoint remoteIp = (IPEndPoint)udpClient.Client.LocalEndPoint;
            try
            {
                byte[] data = udpClient.Receive(ref remoteIp);
                string message = Encoding.Unicode.GetString(data);
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void SendMessage()
        {
            try
            {
                string msg = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(msg);
                udpClient.Send(data, data.Length, "127.0.0.1", 8001);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
