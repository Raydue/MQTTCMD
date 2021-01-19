using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
// 1/25今天嘗試使用第二視窗顯示內容未果
namespace MQTTCMD
{
    public class Program
    {
        static List<string> TopicsHome = new List<string>();
        public static MqttClient client; 
        static string clientid;
       
        static string tmp;



      public static void Main(string[] args)
        {
           
                try
                {
                    client = new MqttClient("192.168.0.5");
                    
                    clientid = Guid.NewGuid().ToString();
                    client.Connect(clientid);
                }
                catch (SystemException)
                {
                    Console.WriteLine("Connection Failed");
                }
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
           
            client.ConnectionClosed += close;
            do
            {
                Console.WriteLine("please write a topic you want to subscribe.");
                tmp = Console.ReadLine();
                TopicsHome.Add(tmp);
            }
            while (tmp != "end");
            
            foreach(string num in TopicsHome)
            {
                client.Subscribe(new string[] { num }, new byte[] { 0 });
            }
            
           /* if (args.Length != 0)
            {
                if (args[0] == "1" )
                {   
                    while(client.IsConnected)
                    secondexecution(carrier);
                }
            }
            else
            {
                firstexecution();
            }*/
        }
        /*private static void firstexecution()
        {
            Console.WriteLine("please write a topic you want to subscribe.");
            tmp = Console.ReadLine();
           
            TopicsHome.Add(tmp);     //從第五個字元開始讀取並輸入TopicsHome
         
            client.Subscribe(new string[] { TopicsHome[0] }, new byte[] { 0 });
            Process.Start("MQTTCMD.exe", "1");
            Console.ReadKey();
            
        }*/
       /* private static void secondexecution(string ReceiveMsg)
        {
            Console.WriteLine(ReceiveMsg);  
        }*/
        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
             string ReceiveMsg = Encoding.UTF8.GetString(e.Message);
            

            Console.WriteLine($"{DateTime.Now}\t{ReceiveMsg.Split(',').Length}" );
            //secondexecution(carrier);
           
        }
      static void close(object sender,EventArgs e)
        { 
            client.Disconnect();
        }
       
    }
}
