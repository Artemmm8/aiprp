using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace bondarchuk_zhukovskyLab2_server_
{
    public partial class Form1 : Form
    {
        private int port = 8005; // порт для приема входящих запросов
        private string ip = "127.0.0.1";
        private Account account;
        private Services.FileIOService fileIOService;
        private readonly string PATH = $"{Environment.CurrentDirectory}\\currentBalance.json";

        public Form1()
        {
            InitializeComponent();
            fileIOService = new Services.FileIOService(this.PATH);
            account = new Account(fileIOService.LoadData());
            this.textBox2.Text = account.Sum.ToString();
        }

        private void DoWork()
        {
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                this.Text = "Сервер запущен. Ожидание подключений...";
                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    string message = string.Empty;
                    if (!decimal.TryParse(builder.ToString(), out decimal value))
                    {
                        message = "некорректная операция";
                    }
                    else
                    {
                        switch (builder[0])
                        {
                            case '-':
                                decimal minus = decimal.Parse(builder.ToString().Substring(1));
                                if (account.Sum - minus >= 0)
                                {
                                    account.Sum -= minus;
                                    message = $"со счёта списано {builder.ToString().Substring(1)} ден. ед. ";
                                    this.listBox1.Items.Add(DateTime.Now.ToShortTimeString() + ": " + builder.ToString() + " (успешно)");
                                }
                                else
                                {
                                    message = "на счёте недостаточно средств ";
                                    this.listBox1.Items.Add(DateTime.Now.ToShortTimeString() + ": " + builder.ToString() + " (отклонено)");
                                }

                                break;
                            case '+':
                                account.Sum += decimal.Parse(builder.ToString().Substring(1));
                                message = $"на счёт зачислено {builder.ToString().Substring(1)} ден. ед. ";
                                this.listBox1.Items.Add(DateTime.Now.ToShortTimeString() + ": " + builder.ToString() + " (успешно)");
                                break;
                        }
                    }

                    // отправляем ответ
                    message += $"(баланс счёта {account.Sum} ден. ед.)";
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    this.textBox2.Text = account.Sum.ToString();
                    fileIOService.SaveData(account.Sum);

                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void DoWorkAsync()
        {
            await Task.Run(() => this.DoWork());
        }

        private void Start_Click(object sender, EventArgs e)
        {
            this.DoWorkAsync();
        }

        private void StartClient()
        {
            Process.Start(@"F:\programs\AIPRP\bondarchuk-zhukovskyLab2(client)\bin\Debug\bondarchuk-zhukovskyLab2(client).exe");
        }

        private void AddClient_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(this.StartClient);
            Thread thread2 = new Thread(this.StartClient);

            thread1.Start();
            thread2.Start();
        }
    }
}
