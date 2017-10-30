
//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;
//using System.Timers;
//using WebSocketSharp;

//namespace RR.WebsocketService_V1
//{
//    public class WebSocketLoggerService
//    {
//        Timer tm = null;

//        public WebSocketLoggerService()
//        {
//            //this.tm.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
//            //this.tm.AutoReset = true;
//            //this.tm = new Timer(60000);
//            //this.tm.Start();

//            Console.WriteLine("WebSocketLoggerService start...");
//            Debug.WriteLine("WebSocketLoggerService start...");
//            Task.Factory.StartNew(() =>
//            {
//                using (var ws = new WebSocket("ws://localhost/log"))
//                {
//                    ws.OnMessage += (sender, e) =>
//                        Debug.WriteLine("Laputa says: " + e.Data);

//                    //ws.OnOpen += onOpen;

//                    ws.Connect();

//                    ws.Send("WebSocket STOP");

//                }
//            });
//            Console.WriteLine("WebSocketLoggerService is working...");
//            Debug.WriteLine("WebSocketLoggerService is working...");
//        }

//        private void onOpen(object sender, EventArgs e)
//        {
//            Console.WriteLine("WebSocketLoggerService is onOpen...");
//            Debug.WriteLine("WebSocketLoggerService is onOpen...");
//        }
//    }
//}

