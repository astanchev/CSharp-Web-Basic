using SIS.HTTP;
using SIS.HTTP.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DemoApp
{
    using System.Text;

    public static class Program
    {
        public static async Task Main()
        {
            var routeTable = new List<Route>
            {
                new Route(HttpMethodType.Get, "/", Index),
                new Route(HttpMethodType.Get, "/users/login", Login),
                new Route(HttpMethodType.Post, "/users/login", DoLogin),
                new Route(HttpMethodType.Get, "/contact", Contact),
                new Route(HttpMethodType.Get, "/favicon.ico", FavIcon),
                new Route(HttpMethodType.Get, "/headers", Headers),
                new Route(HttpMethodType.Get, "/greetings", Greetings),
                new Route(HttpMethodType.Post, "/greetings", DoGreet)
            };

            var httpServer = new HttpServer(80, routeTable);

            await httpServer.StartAsync();
        }

        private static HttpResponse DoGreet(HttpRequest request)
        {
            var data = request.Body.Split('&');
            var name = data[0].Split('=')[1];
            var town = data[1].Split('=')[1];

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h1>GREETINGS</h1>")
                .AppendLine($"<div>Hello <strong><i>{name}</i></strong> from <strong>{town}</strong>!</div><br><br><a href='/'>Go Home</a>");

            return new HtmlResponse(sb.ToString());
        }

        private static HttpResponse Greetings(HttpRequest request)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h1>GREETINGS</h1>")
                .AppendLine(
                    "<form method=\"POST\">Name<br><input type=\"text\"name=\"name\"><br>Town:<br><input type=\"text\" name=\"town\"><br><br><input type=\"submit\" value=\"Submit\"></form>");

            return new HtmlResponse(sb.ToString());
        }

        private static HttpResponse Headers(HttpRequest request)
        {
            var headers = request.Headers;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h1>HEADERS</h1>")
                .AppendLine("<table border=\"1\"><thead><tr><th>Header name</th><th>Header value</th></tr></thead><tbody>");

            foreach (var header in headers)
            {
                sb.AppendLine($"<tr><td>{header.Name}</td><td>{header.Value}</td></tr>");
            }

            sb.AppendLine("</tbody></table><br><br><a href='/'>Go Home</a>");

            return new HtmlResponse(sb.ToString());
        }
        
        private static HttpResponse FavIcon(HttpRequest request)
        {
            var byteContent = File.ReadAllBytes("wwwroot/favicon.ico");
            return new FileResponse(byteContent, "image/x-icon");
        }

        private static HttpResponse Contact(HttpRequest request)
        {
            return new HtmlResponse("<h1>contact</h1><br><br><a href='/greetings'>Go to greetings</a><br><br><a href='/'>Go Home</a>");
        }

        public static HttpResponse Index(HttpRequest request)
        {
            var username = request.SessionData.ContainsKey("Username") ? 
                                        request.SessionData["Username"] : 
                                        "Anonymous";                

            return new HtmlResponse($"<h1>Home page. Hello, {username}</h1><img src='/images/img.jpeg' /><br><a href='/users/login'>Go to login</a><br><a href='/contact'>Go to contact</a><br><a href='/headers'>Go to headers</a><br><a href='/greetings'>Go to greetings</a>");
        }
                    
        public static HttpResponse Login(HttpRequest request)
        {
            request.SessionData["Username"] = "Mitko";
            return new HtmlResponse("<h1>login page<br><br><a href='/'>Go Home</a></h1>");
        }

        public static HttpResponse DoLogin(HttpRequest request)
        {
            return new HtmlResponse("<h1>login page form</h1>");
        }
    }
}
