namespace HttpRequester
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static Dictionary<string, int> SessionStore = new Dictionary<string, int>();
        const string NewLine = "\r\n";

        static async Task Main(string[] args)
        {

            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() => ProcessClientAsync(tcpClient));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        private static async Task ProcessClientAsync(TcpClient tcpClient)
        {
            using NetworkStream networkStream = tcpClient.GetStream();
            byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
            int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
            string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);

            var sid = Regex.Match(request, @"sid=[^\n]*\r\n").Value?.Replace("sid=", string.Empty).Trim();
            Console.WriteLine(sid);
            var newSid = Guid.NewGuid().ToString();
            var count = 0;
            if (SessionStore.ContainsKey(sid))
            {
                SessionStore[sid]++;
                count = SessionStore[sid];
            }
            else
            {
                sid = null;
                SessionStore[newSid] = 1;
                count = 1;
            }


            string responseText = "<h1>" + count + "</h1>" + "<h1>" + DateTime.UtcNow + "</h1>";
            string response = "HTTP/1.0 200 OK" + NewLine +
                              "Server: SoftUniServer/1.0" + NewLine +
                              "Content-Type: text/html" + NewLine +
                              "Set-Cookie: user=Niki; Max-Age: 3600; HttpOnly;" + NewLine +
                              (string.IsNullOrWhiteSpace(sid)
                                  ? ("Set-Cookie: sid=" + newSid + NewLine)
                                  : string.Empty) +
                              // "Location: https://google.com" + NewLine +
                              // "Content-Disposition: attachment; filename=niki.html" + NewLine +
                              "Content-Lenght: " + responseText.Length + NewLine +
                              NewLine +
                              responseText;
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            Console.WriteLine(request);
            Console.WriteLine(new string('=', 60));
        }
    


    //private static async Task ProcessClientAsync(TcpClient tcpClient)
        //{
        //    using NetworkStream networkStream = tcpClient.GetStream();

        //    byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
        //    int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
        //    string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);

        //    string responseText = @"<form action='/Account/Login' method='post'>
        //                                <input type=date name='date' />
        //                                <input type=text name='username' />
        //                                <input type=password name='password' />
        //                                <input type=submit value='Login' />
        //                                </form>
        //                                <br/>
        //                                <h1>" + DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) + @"</h1>
        //                                <hr>
        //                                <br/>
        //                                <img src='https://softuni.bg/Content/images/about-page/career-opportunities.png' 
        //                                        title='Кариерно съдействие' 
        //                                        alt='Кариерно съдействие предоставено от Софтуерен университет' 
        //                                        class='img-responsive'>";
            
        //    string response = "HTTP/1.0 200 OK" + NewLine +
        //                      "Server: SoftUniServer/1.0" + NewLine +
        //                      "Content-Type: text/html" + NewLine +
        //                      //"Location: https://google.com" + NewLine +
        //                      // "Content-Disposition: attachment; filename=niki.html" + NewLine +
        //                      $"Content-Length: {responseText.Length}" + NewLine +
        //                      NewLine +
        //                      responseText;

        //    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
        //    await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);

        //    Console.WriteLine(request);
        //    Console.WriteLine(new string('=', 60));
        //}
    }
}
