using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Fcs.Server
{
    public class Server
    {
        private readonly PollingHandler _pollingHandler;
        private readonly ResponseHandler _responseHandler;
        private readonly HttpListener _httpListener = new();
        private bool _isRunning = true;
        public bool IsRunning { 
            get { return _isRunning; }
            set { _isRunning = value; }
        }

        public Server(PollingHandler pollingHandler, ResponseHandler responseHandler) {
            _pollingHandler = pollingHandler;
            _responseHandler = responseHandler;
            _httpListener.Prefixes.Add("http://localhost:5000/"); // add prefix "http://localhost:5000/"
            _httpListener.Start(); // start server (Run application as Administrator!)
            Console.WriteLine("Server started.");
            Thread responseThread = new Thread(ResponseThread);
            responseThread.Start(); // start the response thread
        }

        private void ResponseThread()
        {
            while (_isRunning)
            {
                HttpListenerContext context = _httpListener.GetContext();
                ProcessRequest(context);
            }
        }
         
        private void ProcessRequest(HttpListenerContext context)
        {
            var request = context.Request;
            Uri url = request.Url!;
            if (url.AbsolutePath == "/poll")
            {
                string responseText = _pollingHandler.PollEvent();
                byte[] responseArray = Encoding.UTF8.GetBytes(responseText);
                context.Response.OutputStream.Write(responseArray, 0, responseArray.Length);
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.KeepAlive = false;
                context.Response.Close();
            }
            else if (url.AbsolutePath.StartsWith("/response"))
            {
                string text;
                using (var reader = new StreamReader(request.InputStream,
                         request.ContentEncoding))
                {
                    text = reader.ReadToEnd();
                }

                _responseHandler.Handle(text);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.KeepAlive = false;
                context.Response.Close();
            }
        } 
    }
}
