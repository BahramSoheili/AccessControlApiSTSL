using AeroApi.Helpers;
using AeroManagement;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace AeroApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class AeroController: Microsoft.AspNetCore.Mvc.Controller
    {
        private const string IP = "192.168.45.5";
        //private LiveEvents liveEvents;

        public AeroController()
        {
          
        }
        [HttpGet("PanelStatus")]
        public string PanelStatus()
        {
            try
            {
                int a;
                int b;
                int c;
                int d;
                int version = Global._scpdWrite.DriverVersion();
                a = version >> 24;
                b = (version >> 16) & 255;
                c = (version >> 8) & 255;
                d = version & 255;
                string report = (Helper.GetTimeStamp() + ": Driver Vesion: " + a + "." + b + "." + c + "." + d);
                Global._controller.Send_CC_IDRequest(Constants.DEMO_SCP_ID);
                return report;
            }
            catch(System.Exception ex)
            {
                return "OffLine";
            }
           
        }
        [HttpGet]
        public IActionResult Connect()
        {
            //Program related
            Global.scp_online = false;
            Global.scp_fw_update = false;
            Global.tz_update = false;
            Global.program_run = true;
            Global.reset_cfg = new bool[Constants.UC_COUNT];
            for (int i = 0; i < Constants.UC_COUNT; i++)
            { Global.reset_cfg[i] = false; }
            //transaction related
            Global.sio_encrypt = new bool[Constants.SIO_COUNT];
            //SCP Init
            Global._scpdWrite = new ScpdWrite();
            Global._scpdRead = new ScpdRead();
            Global._controller = new PanelController(Global._scpdWrite);
            Global._scpdWrite.TurnOnDebug(); // turn on the debug for the driver log file
            Global._threadRead = new Thread(Global._scpdRead.GetScpdMessagesUntilShutdown);
            Global._controller.Send_CC_Channel();
            Global._controller.Send_CC_NewSCP("195.168.45.5");
            Global._controller.Send_CC_AttachSCP();
            Global._threadRead.Start();
            return Ok();
        }

        [HttpGet("Live")]
        public IActionResult Live(string ip)
        {
            Global._controller.Send_CC_SYS();
            Global._controller.Send_CC_Channel();
            Global._controller.Send_CC_NewSCP(ip);
            Global._controller.Send_CC_AttachSCP();
            Global._threadRead.Start();
            return Ok();
        }
    }
}