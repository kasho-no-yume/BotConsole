// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Text;
using BotConsole.Delegate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BotConsole.TouhouPD.Wife;
using BotConsole.TouhouPD;
using BotConsole.TouhouPD.Equipment;
using static System.Net.Mime.MediaTypeNames;
using BotConsole;

Console.WriteLine("Hello,the bot server console started-up!");

HttpListener httpListener = new HttpListener();
httpListener.Prefixes.Add("http://127.0.0.1:5701/receive/");
httpListener.Start();
var thread = new Thread(Commandline);
thread.Start();

while (true)
{
    HttpListenerContext context = httpListener.GetContext();
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;
    string content = "";
    switch (request.HttpMethod)
    {
        case "POST":
            {
                Stream stream = context.Request.InputStream;
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                content = reader.ReadToEnd();
                var json = JsonConvert.DeserializeObject<JObject>(content);
                switch(json["post_type"].ToString())
                {                 
                    case "message": 
                        if (MsgDelegate.receiveMsg != null) 
                            MsgDelegate.receiveMsg(json); 
                        break;
                    case "notice":
                        if(NoticeDelegate.receiveNotice!=null)
                            NoticeDelegate.receiveNotice(json);
                        break;
                }
            }
            break;
        case "GET":
            {
                var data = request.QueryString;
                Console.WriteLine(data);
            }
            break;
    }
    response.Close();
    
}
void Commandline()
{
    while(true)
    {
        string cmd=Console.ReadLine();
        switch(cmd)
        {
            case "draw":Console.WriteLine(RandomWife.RandomId());
                break;
            case "collect":Console.WriteLine(EquipFactory.RandomQuality(Equip.Quality.R));
                break;           
        }
    }
}
static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
{
    Exception ex = e.Exception;
    JObject json = new JObject();
    json.Add("message_type", "private");
    json.Add("user_id", "1935515130");
    json.Add("mseeage", ex.Message);
    new Sender().Send("send_msg",json);
}
static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
{
    Exception ex = e.ExceptionObject as Exception;
    JObject json = new JObject();
    json.Add("message_type", "private");
    json.Add("user_id", "1935515130");
    json.Add("mseeage", ex.Message);
    new Sender().Send("send_msg", json);
}