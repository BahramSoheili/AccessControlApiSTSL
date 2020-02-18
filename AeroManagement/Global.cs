using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HID.Aero.ScpdNet.Wrapper;
using System.Threading;
using System.Collections.Concurrent;

namespace AeroManagement
{
    public static class Global
    {
        //Queue for Event
        public static ConcurrentQueue<string> scpEventQueue;
        public static ScpdWrite _scpdWrite;
        public static ScpdRead _scpdRead;
        public static PanelController _controller;

        // Thread we will use to monitor all responses from the driver, and continually run the driver
        public static Thread _threadRead;
        public static Boolean scp_online;
        public static Boolean scp_fw_update;
        public static Boolean tz_update;

        //Reset Card User Flag
        public static Boolean[] reset_cfg;

        //Start or stop program
        public static Boolean program_run;

        //Transcations variables
        public static Boolean[] sio_encrypt;

        //Program configuration
        public static AeroDemoConfig demo_cfg;       
    }
   
    public static class Constants
    {
        public const short UC_COUNT = 6;
        public const string USER_CARD_DB = "cardUserDB.json";
        public const string DEMO_CONFIG = "aerodemo_config.json";
        public const short DEMO_SCP_ID = 1234;
        public const string DEMO_SCP_IP_DEFAULT = "192.168.45.5";
        public const short PIN_SIZE = 4;
        public const short SIO_COUNT = 3;
        public const short RDR_COUNT = 4;
        public const short RDR_APB_COUNT = 8;
        public const short MP_COUNT = 25;
        public const short CP_COUNT = 16;
        public const short ACR_COUNT = 64;
        public const short SIO_ADDRESS_X100 = 0;
        public const short SIO_ADDRESS_X200 = 1;
        public const short SIO_ADDRESS_X300 = 2;
        public const short RDR_ADDRESS_X1100L = 0;
        public const short RDR_ADDRESS_X1100R = 1;
        public const short RDR_ADDRESS_X100L = 2;
        public const short RDR_ADDRESS_X100R = 3;
        public const short RDR_ADDRESS_X1100L_PAIR = 4;
        public const short RDR_ADDRESS_X1100R_PAIR = 5;
        public const short RDR_ADDRESS_X100L_PAIR = 6;
        public const short RDR_ADDRESS_X100R_PAIR = 7;
        public const short SIO_RS485_PORT = 1;
        public const short CF_COUNT = 8;
        public const short SIO_MODE_AERO = 0;
        public const short SIO_MODE_VERTX = 1;
        public const string DEFAULT_TZ_NAME = "24x7";
        public const string DEFAULT_TZ_STARTTIME = "00:00:00";
        public const string DEFAULT_TZ_ENDTIME = "23:59:59";
        public const short DAYS_OF_WEEK = 7;
        public const short TZ_COUNT = 4;
        public const short DG_COUNT = 7;
        public const short AREA_ENABLE = 2;
        public const short AREA_ENTRY = 10;
        public const short AREA_EXIT = 11;
        public const short AREA_OCC = 5;
        public const short AREA_APB_MODE = 2;
        public const short RDR_MASTER= 1;
        public const short RDR_SLAVE = 2;
        public const short RDR_ADDR_LEFT_PAIR = 2;
        public const short RDR_ADDR_RIGHT_PAIR = 3;
        public const short LED_MODE = 1;
        public const int MAX_CARD_USER = 100;
        public enum V1100_ACCESSDB_CFG
        {
            nCards = 10000,
            nalvl = 32,
            nPinDigits = 68,
            bApbLocation = 1,
            bActDate = 2,
            bDeactDate = 2,
            bUseLimit = 1,
            nHostResponseTimeout = 5,
            nEscortTimeout = 15,
            nMultiCardTimeout = 15,
            nAssetTimeout = 10
        }
        public enum V1100_DEFAULT_CFG
        {
            rs485portNum = 4,
            transaction = 10000,
            nsio = 32,
            nmp = 615,
            ncp = 388,
            nacr = 64,
            nalvl = 255,
            ntrg = 100,
            nproc = 100,
            gmt_offset = -28800,
            ntz = 255,
            nHol = 100,
            ntranlimit = 100000,
            opermode = 1
        }
        public enum V100_HARD_SPEC
        {
            V100_MODEL = 190,
            V100_INPUT = 7,
            V100_OUTPUT = 4,
            V100_READER = 2
        }

        public enum V200_HARD_SPEC
        {
            V200_MODEL = 191,
            V200_INPUT = 19,
            V200_OUTPUT = 2,
            V200_READER = 0
        }

        public enum V300_HARD_SPEC
        {
            V300_MODEL = 192,
            V300_INPUT = 3,
            V300_OUTPUT = 12,
            V300_READER = 0
        }

        public enum X100_HARD_SPEC
        {
            X100_MODEL = 193,
            X100_INPUT = 7,
            X100_OUTPUT = 4,
            X100_READER = 4
        }

        public enum X200_HARD_SPEC
        {
            X200_MODEL = 194,
            X200_INPUT = 19,
            X200_OUTPUT = 2,
            X200_READER = 0
        }

        public enum X300_HARD_SPEC
        {
            X300_MODEL = 195,
            X300_INPUT = 3,
            X300_OUTPUT = 12,
            X300_READER = 0
        }

        public enum X1100_HARD_SPEC
        {
            X1100_MODEL = 196,
            X1100_INPUT = 7,
            X1100_OUTPUT = 4,
            X1100_READER = 4
        }
        public enum AV_COLOR
        {
            OFF,
            RED,
            GREEN,
            AMBER,
            BLUE
        }

        public enum ACR_MODE
        {
            ACR_DISABLE = 1,
            ACR_UNLOCK,
            ACR_LOCK,
            ACR_FACONLY,
            ACR_CARDONLY,
            ACR_PINONLY,
            ACR_CARD_AND_PIN,
            ACR_CARD_OR_PIN,
            ACR_ACCES_DENIED = 11,
            ACR_ACCESS_GRANT
        }
        public enum MP_ADDRESS
        {
            X1100_TAM,
            X1100_ACFAIL,
            X1100_DCFAIL,
            X100_TAM,
            X100_ACFAIL,
            X100_DCFAIL,
            X200IN_1,
            X200IN_2,
            X200IN_3,
            X200IN_4,
            X200IN_5,
            X200IN_6,
            X200IN_7,
            X200IN_8,
            X200IN_9,
            X200IN_10,
            X200IN_11,
            X200IN_12,
            X200IN_13,
            X200IN_14,
            X200IN_15,
            X200IN_16,
            X200_TAM,
            X200_ACFAIL,
            X200_DCFAIL
        }
        public enum CP_ADDRESS
        {
            X1100_OUT2,
            X1100_OUT4,
            X100_OUT2,
            X100_OUT4,
            X300_RELAY1,
            X300_RELAY2,
            X300_RELAY3,
            X300_RELAY4,
            X300_RELAY5,
            X300_RELAY6,
            X300_RELAY7,
            X300_RELAY8,
            X300_RELAY9,
            X300_RELAY10,
            X300_RELAY11,
            X300_RELAY12
        }
        public enum USER_CONTROL_PAGE
        {
            UC_READER,
            UC_MP,
            UC_CP,
            UC_USER,
            UC_CF,
            UC_TZ
        }

        public enum READER_MODE
        {
            DISABLE,
            WEIGAND,
            OSDP
        }

        public enum CF_TYPE
        {
            DISABLE,
            H10301,
            MIFARE_CSN,
            C1K35,
            C1K48,
            DESFIRE_CSN,
            FELICA_CSN
        }

        public enum DAY_OF_WEEK
        {
            MON,
            TUE,
            WED,
            THU,
            FRI,
            SAT,
            SUN
        }
        public enum LED_COLOR
        {
            OFF,
            RED,
            GREEN,
            AMBER,
            BLUE
        }

    }

    public class AeroDemoConfig
    {
         public string controller_ip { get; set; }
         public List<int> sio_address { get; set; }
         public List<int> reader_config { get; set; }
         public List<bool> reader_keypad { get; set; }
        public List<bool> reader_apb { get; set; }
        public List<bool> mp_config { get; set; }
         public List<bool> cp_config { get; set; }
        public List<bool> reader_seos { get; set; }
        public List<int> cardformat_config { get; set; }
        public List<int> cardformat_fc { get; set; }
        public int aero_sio { get; set; }
        public string tz1_name { get; set; }
        public string tz1_starttime_1 { get; set; }
        public string tz1_endtime_1 { get; set; }
        public List<bool> tz1_days_1 { get; set; }
        public string tz2_name { get; set; }
        public string tz2_starttime_1 { get; set; }
        public string tz2_endtime_1 { get; set; }
        public List<bool> tz2_days_1 { get; set; }
        public string tz3_name { get; set; }
        public string tz3_starttime_1 { get; set; }
        public string tz3_endtime_1 { get; set; }
        public List<bool> tz3_days_1 { get; set; }
        public string tz4_name { get; set; }
        public string tz4_starttime_1 { get; set; }
        public string tz4_endtime_1 { get; set; }
        public List<bool> tz4_days_1 { get; set; }
        private void ClearConfig()
        {
            controller_ip = Constants.DEMO_SCP_IP_DEFAULT;
            sio_address.Clear();
            for (int i = 0; i < Constants.SIO_COUNT; i++)
            {
                sio_address.Add(0);
            }
            reader_config.Clear();
            for (int i = 0; i < Constants.RDR_COUNT; i++)
            {
                reader_config.Add(0);
            }
            reader_keypad.Clear();
            for (int i = 0; i < Constants.RDR_COUNT; i++)
            {
                reader_keypad.Add(false);
            }
            mp_config.Clear();
            for (int i = 0; i < Constants.MP_COUNT; i++)
            {
                mp_config.Add(false);
            }
            cp_config.Clear();
            for (int i = 0; i < Constants.CP_COUNT; i++)
            {
                cp_config.Add(false);
            }
            reader_seos.Clear();
            for (int i = 0; i < Constants.RDR_COUNT; i++)
            {
                reader_seos.Add(false);
            }
            cardformat_config.Clear();
            for (int i = 0; i < Constants.CF_COUNT; i++)
            {
                cardformat_config.Add(0);
            }
            cardformat_fc.Clear();
            for (int i = 0; i < Constants.CF_COUNT; i++)
            {
                cardformat_fc.Add(0);
            }
            aero_sio = Constants.SIO_MODE_AERO;
            tz1_name = Constants.DEFAULT_TZ_NAME;
            tz1_starttime_1 = Constants.DEFAULT_TZ_STARTTIME;
            tz1_endtime_1 = Constants.DEFAULT_TZ_ENDTIME;
            tz1_days_1.Clear();
            for (int i = 0; i < Constants.DAYS_OF_WEEK; i++)
            {
                tz1_days_1.Add(false);
            }
            tz2_name = null;
            tz2_starttime_1 = Constants.DEFAULT_TZ_STARTTIME;
            tz2_endtime_1 = Constants.DEFAULT_TZ_ENDTIME;
            tz2_days_1.Clear();
            for (int i = 0; i < Constants.DAYS_OF_WEEK; i++)
            {
                tz2_days_1.Add(false);
            }
            tz3_name = null;
            tz3_starttime_1 = Constants.DEFAULT_TZ_STARTTIME;
            tz3_endtime_1 = Constants.DEFAULT_TZ_ENDTIME;
            tz3_days_1.Clear();
            for (int i = 0; i < Constants.DAYS_OF_WEEK; i++)
            {
                tz3_days_1.Add(false);
            }
            tz4_name = null;
            tz4_starttime_1 = Constants.DEFAULT_TZ_STARTTIME;
            tz4_endtime_1 = Constants.DEFAULT_TZ_ENDTIME;
            tz4_days_1.Clear();
            for (int i = 0; i < Constants.DAYS_OF_WEEK; i++)
            {
                tz4_days_1.Add(false);
            }
        }
    }
}
