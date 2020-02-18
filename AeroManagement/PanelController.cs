/*************************************************************************************
* Copyright © 2019 HID Global Corporation.  All rights reserved.
* This software is protected by copyright law and international treaties, as well as any nondisclosure agreements that you or the organization you represent have signed.  
* Any unauthorized reproduction, distribution or use of the software is prohibited.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HID.Aero.ScpdNet.Wrapper;
using System.Threading;

namespace AeroManagement
{
    public class PanelController
    {
        ScpdWrite _scpdWrite;

        public PanelController(ScpdWrite scpdWrite)
        {
            _scpdWrite = scpdWrite;
        }

        //////
        // Method: Send_CC_WEB_CONFIG_READ
        // 
        // Sample 900 command that will get web config information from the SCP
        // There are various 900 read commands that will read the different webpages
        //
        // Sample command 900. 
        //   Find thie command by searching the API manual for "900" or by looking up "900" in the HID Aero Product Line Overview \
        //   "API Quick Reference". You will find the associated class "CC_WEB_CONFIG_READ" in the API manual.
        //   You will find the following parameters in the API manual.
        //
        //  Command 900: Reads Configuration
        //    Field         Name            Comments
        //    1             900             Read Configuration
        //    2             scp_number      SCP (intelligent controller) number - unique reference per driver
        //    3             read_type       Read type   
        //                                  1 = Home notes
        //                                  2 = Network settings
        //                                  3 = Host communication primary settings
        //                                  ...
        //////
        public bool Send_CC_WEB_CONFIG_READ(short scpNumber, Int16 iReadType)
        {
            bool success = false;

            CC_WEB_CONFIG_READ webConfig = new CC_WEB_CONFIG_READ
            {
                scp_number = scpNumber,
                read_type = iReadType
            };

            _scpdWrite.WriteCommand(900, webConfig);

            return success;
        }

        //////
        // Method: Send_CC_RESET
        // 
        // Reset the SCP database. This will remove all hardware and database settings, for example, anything set by 1107 and 1105 commands, as well
        // as subsequent commands to setup hardware, card IDs, PINs, etc.
        //
        //////
        public bool Send_CC_SYS()
        {
            bool success = false;

            CC_SYS sys = new CC_SYS();
            sys.nPorts = 512;
            sys.nScps = 512;
            sys.bDirectMode = 1;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcSystem, sys);

            return success;
        }
        public bool Send_CC_Channel()
        {
            bool success = false;

            CC_CHANNEL channel = new CC_CHANNEL();
            channel.nChannelId = 250;
            channel.cType = 4;
            channel.timer1 = 2800;
            channel.timer2 = 5000;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcCreateChannel, channel);

            return success;
        }

        public bool Send_CC_AttachSCP()
        {
            bool success = false;

            CC_ATTACHSCP attach = new CC_ATTACHSCP();
            attach.nChannelId = 250;
            attach.nSCPId = Constants.DEMO_SCP_ID;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcAttachScp, attach);

            return success;
        }

        public bool Send_CC_NewSCP(string ip)
        {
            bool success = false;
            string pwd = "password";

            if(ip.Length > 15)
            {
                return false;
            }

            CC_NEWSCP_LN scp = new CC_NEWSCP_LN();
            scp.nSCPId = Constants.DEMO_SCP_ID;
            scp.nCommAccess = 4;
            scp.e_max = 3;
            scp.poll_delay = 5000;
            scp.offline_time = 15000;
            for(int i=0; i < pwd.Length; i++)
            {
                scp.cPswdString[i] = pwd[i];
            }
            for (int j = 0; j < ip.Length; j++)
            {
                scp.cCommString[j] = ip[j];
            }
            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcCreateScpLn, scp);

            return success;
        }

        public bool Send_CC_IDRequest(short scpNumber)
        {
            bool success = false;

            CC_IDREQUEST request = new CC_IDREQUEST
            {
                scp_number = scpNumber
            };

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcIDRequest, request);

            return success;
        }

        public bool Send_CC_CC_SIOSRQ(short scpNumber)
        {
            bool success = false;

            CC_SIOSRQ request = new CC_SIOSRQ();
            request.scp_number = scpNumber;
            request.first = 1;
            request.count = 3;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcSioSrq, request);

            return success;
        }
        public bool Send_CC_RESET(short scpNumber)
        {
            bool success = false;

            CC_RESET reset = new CC_RESET
            {
                scp_number = scpNumber
            };

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcReset, reset);

            return success;
        }

        //////
        // Method: Send_CC_FIRMWARE
        // 
        // Reset the SCP database. This will remove all hardware and database settings, for example, anything set by 1107 and 1105 commands, as well
        // as subsequent commands to setup hardware, card IDs, PINs, etc.
        //
        //////
        public bool Send_CC_FIRMWARE(short scpNumber, string sFileName)
        {
            bool success = false;

            CC_FIRMWARE firmware = new CC_FIRMWARE();
            firmware.scp_number = scpNumber;

            // Set Field #3, path and file name for the binary firmware file
            // The .NET wrapper's marshalling which requires the underlying C data structure be maintained
            // This is why we cannot use methods like sFileName.ToCharArray() in the class constructor
            sFileName.CopyTo(0, firmware.file_name, 0, sFileName.Length);

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcFirmwareDown, firmware);

            return success;
        }

        public bool Send_CC_RS485BUS(short msp, short port, bool vertx, bool not_X1100)
        {
            bool success = false;

            CC_MSP1 rs485bus = new CC_MSP1();
            rs485bus.scp_number = Constants.DEMO_SCP_ID;
            rs485bus.msp1_number = msp;
            rs485bus.port_number = port;
            if (not_X1100) { 
                rs485bus.baud_rate = 38400;
            }
            rs485bus.reply_time = 90;

            if (vertx)
            {
                rs485bus.nProtocol = 15;
            }

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcMsp1, rs485bus);

            return success;
        }

        public bool Send_CC_SETSIO(short sio_address, int type)
        {
            bool success = false;

            CC_SIO siocfg = new CC_SIO();
            siocfg.scp_number = Constants.DEMO_SCP_ID;
            siocfg.sio_number = sio_address;
            siocfg.address = sio_address;
            siocfg.ser_num_high = -1;
            siocfg.enable = 1;
            siocfg.e_max = 3;
            switch (type)
            {
                case (int)Constants.V100_HARD_SPEC.V100_MODEL:
                    siocfg.model = (int)Constants.V100_HARD_SPEC.V100_MODEL;
                    siocfg.nInputs = (int)Constants.V100_HARD_SPEC.V100_INPUT;
                    siocfg.nOutputs = (int)Constants.V100_HARD_SPEC.V100_OUTPUT;
                    siocfg.nReaders = (int)Constants.V100_HARD_SPEC.V100_READER;
                    siocfg.port = (short)Constants.SIO_RS485_PORT;
                    break;
                case (int)Constants.V200_HARD_SPEC.V200_MODEL:
                    siocfg.model = (int)Constants.V200_HARD_SPEC.V200_MODEL;
                    siocfg.nInputs = (int)Constants.V200_HARD_SPEC.V200_INPUT;
                    siocfg.nOutputs = (int)Constants.V200_HARD_SPEC.V200_OUTPUT;
                    siocfg.nReaders = (int)Constants.V200_HARD_SPEC.V200_READER;
                    siocfg.port = (short)Constants.SIO_RS485_PORT;
                    break;
                case (int)Constants.V300_HARD_SPEC.V300_MODEL:
                    siocfg.model = (int)Constants.V300_HARD_SPEC.V300_MODEL;
                    siocfg.nInputs = (int)Constants.V300_HARD_SPEC.V300_INPUT;
                    siocfg.nOutputs = (int)Constants.V300_HARD_SPEC.V300_OUTPUT;
                    siocfg.nReaders = (int)Constants.V300_HARD_SPEC.V300_READER;
                    siocfg.port = (short)Constants.SIO_RS485_PORT;
                    break;
                case (int)Constants.X100_HARD_SPEC.X100_MODEL:
                    siocfg.model = (int)Constants.X100_HARD_SPEC.X100_MODEL;
                    siocfg.nInputs = (int)Constants.X100_HARD_SPEC.X100_INPUT;
                    siocfg.nOutputs = (int)Constants.X100_HARD_SPEC.X100_OUTPUT;
                    siocfg.nReaders = (int)Constants.X100_HARD_SPEC.X100_READER;
                    siocfg.port = (short)Constants.SIO_RS485_PORT;
                    break;
                case (int)Constants.X200_HARD_SPEC.X200_MODEL:
                    siocfg.model = (int)Constants.X200_HARD_SPEC.X200_MODEL;
                    siocfg.nInputs = (int)Constants.X200_HARD_SPEC.X200_INPUT;
                    siocfg.nOutputs = (int)Constants.X200_HARD_SPEC.X200_OUTPUT;
                    siocfg.nReaders = (int)Constants.X200_HARD_SPEC.X200_READER;
                    siocfg.port = (short)Constants.SIO_RS485_PORT;
                    break;
                case (int)Constants.X300_HARD_SPEC.X300_MODEL:
                    siocfg.model = (int)Constants.X300_HARD_SPEC.X300_MODEL;
                    siocfg.nInputs = (int)Constants.X300_HARD_SPEC.X300_INPUT;
                    siocfg.nOutputs = (int)Constants.X300_HARD_SPEC.X300_OUTPUT;
                    siocfg.nReaders = (int)Constants.X300_HARD_SPEC.X300_READER;
                    siocfg.port = (short)Constants.SIO_RS485_PORT;
                    break;
                case (int)Constants.X1100_HARD_SPEC.X1100_MODEL:
                    siocfg.model = (int)Constants.X1100_HARD_SPEC.X1100_MODEL;
                    siocfg.nInputs = (int)Constants.X1100_HARD_SPEC.X1100_INPUT;
                    siocfg.nOutputs = (int)Constants.X1100_HARD_SPEC.X1100_OUTPUT;
                    siocfg.nReaders = (int)Constants.X1100_HARD_SPEC.X1100_READER;
                    siocfg.port = 0;
                    break;
                default:
                    return false;
            }

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcSio, siocfg);

            return success;
        }

        public bool Send_CC_SETINPUT(short address, int type)
        {
            bool success = false;

            CC_IP inputcfg = new CC_IP();
            inputcfg.scp_number = Constants.DEMO_SCP_ID;
            inputcfg.sio_number = address;
            inputcfg.debounce = 4;
            inputcfg.icvt_num = 1;
            switch (type)
            {
                case (int)Constants.V100_HARD_SPEC.V100_MODEL:
                case (int)Constants.X100_HARD_SPEC.X100_MODEL:
                case (int)Constants.X1100_HARD_SPEC.X1100_MODEL:
                    for(short i = 0; i < (int)Constants.V100_HARD_SPEC.V100_INPUT; i++)
                    {
                        inputcfg.input = i;
                        if(i == 0 || i== 2)
                        {
                            //Set normal closed
                            inputcfg.icvt_num = 0;
                        }
                        else
                        {
                            //Set normally open
                            inputcfg.icvt_num = 1;
                        }
                        success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcInput, inputcfg);
                    }
                    break;
                case (int)Constants.V200_HARD_SPEC.V200_MODEL:
                case (int)Constants.X200_HARD_SPEC.X200_MODEL:
                    for (short i = 0; i < (int)Constants.V200_HARD_SPEC.V200_INPUT; i++)
                    {
                        inputcfg.input = i;
                        success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcInput, inputcfg);
                    }
                    break;
                case (int)Constants.V300_HARD_SPEC.V300_MODEL:
                case (int)Constants.X300_HARD_SPEC.X300_MODEL:
                    for (short i = 0; i < (int)Constants.V300_HARD_SPEC.V300_INPUT; i++)
                    {
                        inputcfg.input = i;
                        success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcInput, inputcfg);
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }

        public bool Send_CC_SETOUTPUT(short address, int type)
        {
            bool success = false;

            CC_OP outcfg = new CC_OP();
            outcfg.scp_number = Constants.DEMO_SCP_ID;
            outcfg.sio_number = address;

            switch (type)
            {
                case (int)Constants.V100_HARD_SPEC.V100_MODEL:
                case (int)Constants.X100_HARD_SPEC.X100_MODEL:
                case (int)Constants.X1100_HARD_SPEC.X1100_MODEL:
                    for (short i = 0; i < (int)Constants.V100_HARD_SPEC.V100_OUTPUT; i++)
                    {
                        outcfg.output = i;
                        success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcOutput, outcfg);
                    }
                    break;
                case (int)Constants.V200_HARD_SPEC.V200_MODEL:
                case (int)Constants.X200_HARD_SPEC.X200_MODEL:
                    for (short i = 0; i < (int)Constants.V200_HARD_SPEC.V200_OUTPUT; i++)
                    {
                        outcfg.output = i;
                        success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcOutput, outcfg);
                    }
                    break;
                case (int)Constants.V300_HARD_SPEC.V300_MODEL:
                case (int)Constants.X300_HARD_SPEC.X300_MODEL:
                    for (short i = 0; i < (int)Constants.V300_HARD_SPEC.V300_OUTPUT; i++)
                    {
                        outcfg.output = i;
                        success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcOutput, outcfg);
                    }
                    break;
                default:
                    return false;
            }

            return success;
        }

        public bool Send_CC_SETMP(short sio, short input, short mp)
        {
            bool success = false;
            CC_MP mpcfg = new CC_MP();
            mpcfg.scp_number = Constants.DEMO_SCP_ID;
            mpcfg.sio_number = sio;
            mpcfg.mp_number = mp;
            mpcfg.ip_number = input;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcMP, mpcfg);
            return success;
        }
        public bool Send_CC_TRCP(short cp, short on_off)
        {
            bool success = false;
            CC_CPCTL cpcfg = new CC_CPCTL();
            cpcfg.scp_number = Constants.DEMO_SCP_ID;
            cpcfg.cp_number = cp;
            cpcfg.command = on_off;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcCpCtl, cpcfg);
            return success;
        }
            public bool Send_CC_SETCP(short sio, short output, short cp)
        {
            bool success = false;
            CC_CP cpcfg = new CC_CP();
            cpcfg.scp_number = Constants.DEMO_SCP_ID;
            cpcfg.sio_number = sio;
            cpcfg.op_number = output;
            cpcfg.cp_number = cp;
            cpcfg.dflt_pulse = 1;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcCP, cpcfg);
            return success;
        }

        public bool Send_CC_SETRDR(short sio, short rdr, bool osdp, bool keypad)
        {
            bool success = false;
            CC_RDR rdrcfg = new CC_RDR();
            rdrcfg.scp_number = Constants.DEMO_SCP_ID;
            rdrcfg.sio_number = sio;
            rdrcfg.dt_fmt = 1;
            rdrcfg.reader = rdr;
            if (keypad)
            {
                rdrcfg.keypad_mode = 2;
            }

            if (osdp)
            {
                rdrcfg.led_drive_mode = 7;
                rdrcfg.osdp_flags = 1;
            }
            else
            {
                rdrcfg.led_drive_mode = 1;
            }

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcReader, rdrcfg);
            return success;
        }

        public bool Send_CC_SETRDR(short sio, short rdr, bool osdp, bool keypad, bool osdp_apb)
        {
            bool success = false;
            CC_RDR rdrcfg = new CC_RDR();
            rdrcfg.scp_number = Constants.DEMO_SCP_ID;
            rdrcfg.sio_number = sio;
            rdrcfg.dt_fmt = 1;
            rdrcfg.reader = rdr;
            if (keypad)
            {
                rdrcfg.keypad_mode = 2;
            }

            if (osdp)
            {
                rdrcfg.led_drive_mode = 7;
                rdrcfg.osdp_flags = 33;
            }
            else
            {
                rdrcfg.led_drive_mode = 1;
            }

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcReader, rdrcfg);
            return success;
        }

        public bool Send_CC_TempUnlock(short acr_num)
        {
            bool success = false;
            CC_UNLOCK acr = new CC_UNLOCK();
            acr.scp_number = Constants.DEMO_SCP_ID;
            acr.acr_number = acr_num;
            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcUnlock, acr);
            return success;
        }

        public bool Send_CC_ChangeACRMode(short acr_num, short mode)
        {
            bool success = false;
            CC_ACRMODE acr = new CC_ACRMODE();
            acr.scp_number = Constants.DEMO_SCP_ID;
            acr.acr_number = acr_num;
            acr.acr_mode = mode;
            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcAcrMode, acr);
            return success;
        }

        public bool Send_CC_DelCard(long card)
        {
            bool success = false;
            CC_CARDDELETEI64 delcard = new CC_CARDDELETEI64();
            delcard.scp_number = Constants.DEMO_SCP_ID;
            delcard.cardholder_id = card;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcCardDeleteI64, delcard);
            return success;
        }

            public bool Send_CC_AddCard(long card, string pin, short al)
        {
            bool success = false;
            CC_ADBC_I64DTIC32 addcard_cmd = new CC_ADBC_I64DTIC32();
                
            addcard_cmd.lastModified = 0;
            addcard_cmd.scp_number = Constants.DEMO_SCP_ID;
            addcard_cmd.flags = 3;
            addcard_cmd.use_count = 0;
            addcard_cmd.card_number = card;
            addcard_cmd.issue_code = 0;
            if (!String.IsNullOrEmpty(pin))
            {
                for (short i = 0; i < Constants.PIN_SIZE; i++)
                {
                    addcard_cmd.pin[i] = pin[i];
                }
            }
            addcard_cmd.apb_loc = 0;
            addcard_cmd.tmp_days = 0;
            addcard_cmd.tmp_date = 0;
            addcard_cmd.vac_days = 0;
            addcard_cmd.vac_date = 0;
            addcard_cmd.asset_group = 0;
            addcard_cmd.act_time = 1131447600;
            addcard_cmd.dact_time = 1879471233;
            addcard_cmd.alvl[0] = al;

            /*
        public short[] alvl_prec;
        public short[] user_level;
        */
            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcAdbCardI64DTic32, addcard_cmd);

            return success;
        }

        public bool Send_CC_SCP_DAYLIGHT()
        {
            bool success = false;
            TimeZone curTimeZone = TimeZone.CurrentTimeZone;
            System.Globalization.DaylightTime dl = curTimeZone.GetDaylightChanges(DateTime.Now.Year);
            CC_SCP_DAYLIGHT timecfg = new CC_SCP_DAYLIGHT();

            timecfg.nScpID = Constants.DEMO_SCP_ID;
            timecfg.nSYear = (short)dl.Start.Year;
            timecfg.nSMonth = (short)dl.Start.Month;
            timecfg.nSDay = (short)dl.Start.Day;
            timecfg.nSHh = (short)dl.Start.Hour;
            timecfg.nSMm = (short)dl.Start.Minute;
            timecfg.nSSs = (short)dl.Start.Second;
            timecfg.nEYear = (short)dl.End.Year;
            timecfg.nEMonth = (short)dl.End.Month;
            timecfg.nEDay = (short)dl.End.Day;
            timecfg.nEHh = (short)dl.End.Hour;
            timecfg.nEMm = (short)dl.End.Minute;
            timecfg.nESs = (short)dl.End.Second;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcScpDaylight, timecfg);
            return success;
        }

        public bool Send_CC_SETTIME()
        {
            bool success = false;
            CC_TIME timecfg = new CC_TIME();
            timecfg.scp_number = Constants.DEMO_SCP_ID;
            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcTime, timecfg);
            return success;
        }

        public bool Send_CC_SETTZ_ALWAYS(short tz_num)
        {
            bool success = false;
            CC_SCP_TZEX_ACT tzcfg = new CC_SCP_TZEX_ACT();
            tzcfg.nScpID = Constants.DEMO_SCP_ID;
            tzcfg.number = tz_num;
            tzcfg.mode = 1;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcScpTimezoneExAct, tzcfg);
            return success;
        }

        public bool Send_CC_SETTZ(short tz_num, short start, short end, short dayMask)
        {
            bool success = false;
            CC_SCP_TZEX_ACT tzcfg = new CC_SCP_TZEX_ACT();
            tzcfg.nScpID = Constants.DEMO_SCP_ID;
            tzcfg.number = tz_num;
            tzcfg.mode = 2;
            tzcfg.intervals = 1;
            tzcfg.i[0].i_days = dayMask;
            tzcfg.i[0].i_start = start;
            tzcfg.i[0].i_end = end;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcScpTimezoneExAct, tzcfg);
            return success;
        }

        public bool Send_CC_SETAL(short alvl, short tz)
        {
            bool success = false;
            CC_ALVL_EX alcfg = new CC_ALVL_EX();
            alcfg.scp_number = Constants.DEMO_SCP_ID;
            alcfg.alvl_number = alvl;
            for (int i = 0; i < Constants.RDR_APB_COUNT; i++) {
                alcfg.tz[i] = tz;
             }

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcAlvlEx, alcfg);
            return success;
        }

        public bool Send_CC_SETAL(short alvl, short[] tz)
        {
            bool success = false;
            CC_ALVL_EX alcfg = new CC_ALVL_EX();
            alcfg.scp_number = Constants.DEMO_SCP_ID;
            alcfg.alvl_number = alvl;
            for (int i = 0; i < Constants.ACR_COUNT; i++)
            {
                alcfg.tz[i] = tz[i];
            }

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcAlvlEx, alcfg);
            return success;
        }
        public bool Send_CC_SETAREA(short area, short occupancy)
        {
            bool success = false;
            CC_AREA_SPC areacfg = new CC_AREA_SPC();
            areacfg.scp_number = Constants.DEMO_SCP_ID;
            areacfg.area_number = area;
            areacfg.access_control = Constants.AREA_ENABLE;
            areacfg.occ_max = occupancy;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcAreaSpc, areacfg);
            return success;
        }

        public bool Send_CC_SETACR(short acr_num, short acr_mode, short r_sio, short r_num, short strike_sio, short strike_num, short door_sio, short door_num, short rex_sio, short rex_num)
        {
            bool success = false;
            CC_ACR acrcfg = new CC_ACR();
            acrcfg.scp_number = Constants.DEMO_SCP_ID;
            acrcfg.acr_number = acr_num;
            acrcfg.pair_acr_number = -1;
            acrcfg.rdr_sio = r_sio;
            acrcfg.rdr_number = r_num;
            acrcfg.strk_sio = strike_sio;
            acrcfg.strk_number = strike_num;
            acrcfg.strike_t_min = 1;
            acrcfg.strike_t_max = 5;
            acrcfg.strike_mode = 1;
            acrcfg.door_sio = door_sio;
            acrcfg.door_number = door_num;
            acrcfg.dc_held = 10;
            acrcfg.rex0_sio = rex_sio;
            acrcfg.rex0_number = rex_num;
            acrcfg.rex1_sio = -1;
            acrcfg.altrdr_sio = -1;
            acrcfg.cd_format = 255;
            acrcfg.actl_flags = 32;
            acrcfg.offline_mode = 1;
            acrcfg.default_mode = acr_mode;
            if (acr_mode == (short)Constants.ACR_MODE.ACR_CARD_AND_PIN)
            {
                //Force card to read first
                acrcfg.spare = 2;
            }
            acrcfg.default_led_mode = 1;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcACR, acrcfg);
            return success;
        }
        public bool Send_CC_SETACR(short acr_num, short acr_mode, short r_sio, short r_num, short strike_sio, short strike_num, short door_sio, short door_num, short rex_sio, short rex_num, short abp_master, short pair_acr, short entry_loc, short exit_loc)
        {
            bool success = false;
            CC_ACR acrcfg = new CC_ACR();
            acrcfg.scp_number = Constants.DEMO_SCP_ID;
            acrcfg.acr_number = acr_num;
            acrcfg.pair_acr_number = -1;
            acrcfg.access_cfg = abp_master;
            acrcfg.pair_acr_number = pair_acr; 
            acrcfg.rdr_sio = r_sio;
            acrcfg.rdr_number = r_num;
            acrcfg.strk_sio = strike_sio;
            acrcfg.strk_number = strike_num;
            acrcfg.strike_t_min = 1;
            acrcfg.strike_t_max = 5;
            acrcfg.strike_mode = 1;
            acrcfg.door_sio = door_sio;
            acrcfg.door_number = door_num;
            acrcfg.dc_held = 10;
            acrcfg.rex0_sio = rex_sio;
            acrcfg.rex0_number = rex_num;
            acrcfg.rex1_sio = -1;
            acrcfg.altrdr_sio = -1;
            acrcfg.cd_format = 255;
            acrcfg.actl_flags = 32;
            acrcfg.offline_mode = 1;
            acrcfg.default_mode = acr_mode;
            if (acr_mode == (short)Constants.ACR_MODE.ACR_CARD_AND_PIN)
            {
                //Force card to read first
                acrcfg.spare = 2;
            }
            acrcfg.default_led_mode = 1;
            acrcfg.apb_mode = Constants.AREA_APB_MODE;
            acrcfg.apb_in = entry_loc;
            acrcfg.apb_to = exit_loc;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcACR, acrcfg);
            return success;
        }

        public bool Send_CC_SETCF(short format_num, short bit, short fac_code)
        {
            bool success = false;
            CC_SCP_CFMT cfcfg = new CC_SCP_CFMT();
            cfcfg.nScpID = Constants.DEMO_SCP_ID;
            cfcfg.number = format_num;
            switch (bit)
            {
                case 26:
                    cfcfg.facility = fac_code;
                    cfcfg.function_id = 1;
                    cfcfg.arg.sensor.bits = 26;
                    cfcfg.arg.sensor.pe_ln = 13;
                    cfcfg.arg.sensor.po_ln = 13;
                    cfcfg.arg.sensor.po_loc = 13;
                    cfcfg.arg.sensor.fc_ln = 8;
                    cfcfg.arg.sensor.fc_loc = 1;
                    cfcfg.arg.sensor.ch_ln = 16;
                    cfcfg.arg.sensor.ch_loc = 9;
                    break;
                case 32:
                    cfcfg.facility = -1;
                    cfcfg.function_id = 1;
                    cfcfg.arg.sensor.flags = 1;
                    cfcfg.arg.sensor.bits = 32;
                    cfcfg.arg.sensor.ch_ln = 32;
                    break;
                case 35:
                    cfcfg.facility = fac_code;
                    cfcfg.function_id = 1;
                    cfcfg.arg.sensor.flags = 4;
                    cfcfg.arg.sensor.bits = 35;
                    cfcfg.arg.sensor.fc_ln = 12;
                    cfcfg.arg.sensor.fc_loc = 2;
                    cfcfg.arg.sensor.ch_ln = 20;
                    cfcfg.arg.sensor.ch_loc = 14;
                    break;
                case 48:
                    cfcfg.facility = fac_code;
                    cfcfg.function_id = 1;
                    cfcfg.arg.sensor.flags = 4;
                    cfcfg.arg.sensor.bits = 48;
                    cfcfg.arg.sensor.fc_ln = 22;
                    cfcfg.arg.sensor.fc_loc = 2;
                    cfcfg.arg.sensor.ch_ln = 23;
                    cfcfg.arg.sensor.ch_loc = 24;
                    break;
                case 56:
                    cfcfg.facility = -1;
                    cfcfg.function_id = 1;
                    cfcfg.arg.sensor.flags = 1;
                    cfcfg.arg.sensor.bits = 56;
                    cfcfg.arg.sensor.ch_ln = 56;
                    break;
                case 64:
                    cfcfg.facility = -1;
                    cfcfg.function_id = 1;
                    cfcfg.arg.sensor.flags = 1;
                    cfcfg.arg.sensor.bits = 64;
                    cfcfg.arg.sensor.ch_ln = 64;
                    break;
                default:
                    return false;
            }
            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcScpCfmt, cfcfg);
            return success;
        }
        public bool Send_CC_SETAV(short acr_state)
        {
            bool success = false;
            CC_RLEDSPC avcfg = new CC_RLEDSPC();
            avcfg.scp_number = Constants.DEMO_SCP_ID;
            avcfg.led_mode = 1;
            avcfg.rled_id = acr_state;
            switch (acr_state)
            {
                case (short)Constants.ACR_MODE.ACR_CARDONLY:
                    avcfg.on_color = (short)Constants.AV_COLOR.RED;
                    avcfg.off_color = (short)Constants.AV_COLOR.RED;
                    avcfg.repeat_count = 1;
                    break;
                case (short)Constants.ACR_MODE.ACR_CARD_AND_PIN:
                    avcfg.on_color = (short)Constants.AV_COLOR.RED;
                    avcfg.off_color = (short)Constants.AV_COLOR.RED;
                    avcfg.repeat_count = 1;
                    break;
                case (short)Constants.ACR_MODE.ACR_ACCES_DENIED:
                    avcfg.on_color = (short)Constants.AV_COLOR.RED;
                    avcfg.on_time = 5;
                    avcfg.off_time = 5;
                    avcfg.repeat_count = 2;
                    avcfg.beep_count = 2;
                    break;
                case (short)Constants.ACR_MODE.ACR_ACCESS_GRANT:
                    avcfg.on_color = (short)Constants.AV_COLOR.GREEN;
                    avcfg.off_color = (short)Constants.AV_COLOR.RED;
                    avcfg.on_time = 50;
                    avcfg.off_time = 0;
                    avcfg.repeat_count = 1;
                    avcfg.beep_count = 1;
                    break;
                default:
                    return false;
            }

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcRledSpc, avcfg);
            return success;
        }

        public bool Send_CC_SETSCP()
        {
            TimeZone curTimeZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOffset = curTimeZone.GetUtcOffset(DateTime.Now);
            bool success = false;
            CC_SCP_SCP scpcfg = new CC_SCP_SCP();
            scpcfg.number = Constants.DEMO_SCP_ID;
            scpcfg.nMsp1Port = (short)Constants.V1100_DEFAULT_CFG.rs485portNum;
            scpcfg.nTransactions = (int)Constants.V1100_DEFAULT_CFG.transaction;
            scpcfg.nTranLimit = (int)Constants.V1100_DEFAULT_CFG.ntranlimit;
            scpcfg.nSio = (short)Constants.V1100_DEFAULT_CFG.nsio;
            scpcfg.nMp = (short)Constants.V1100_DEFAULT_CFG.nmp;
            scpcfg.nCp = (short)Constants.V1100_DEFAULT_CFG.ncp;
            scpcfg.nAcr = (short)Constants.V1100_DEFAULT_CFG.nacr;
            scpcfg.nAlvl = (short)Constants.V1100_DEFAULT_CFG.nalvl;
            scpcfg.nTrgr = (short)Constants.V1100_DEFAULT_CFG.ntrg;
            scpcfg.nProc = (short)Constants.V1100_DEFAULT_CFG.nproc;
            scpcfg.gmt_offset = (currentOffset.Hours * 3600 + currentOffset.Minutes + currentOffset.Seconds) * -1;
            scpcfg.nTz = (short)Constants.V1100_DEFAULT_CFG.ntz;
            scpcfg.nHol = (short)Constants.V1100_DEFAULT_CFG.nHol;
            scpcfg.nOperModes = (short)Constants.V1100_DEFAULT_CFG.opermode;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcScpScp, scpcfg);
            return success;
        }
        public bool Send_CC_SETAccessDB()
        {
            bool success = false;
            CC_SCP_ADBS dbcfg = new CC_SCP_ADBS();
            dbcfg.nScpID = Constants.DEMO_SCP_ID;
            dbcfg.nCards = (int)Constants.V1100_ACCESSDB_CFG.nCards;
            dbcfg.nAlvl = (short)Constants.V1100_ACCESSDB_CFG.nalvl;
            dbcfg.nPinDigits = (short)Constants.V1100_ACCESSDB_CFG.nPinDigits;
            dbcfg.bApbLocation = (short)Constants.V1100_ACCESSDB_CFG.bApbLocation;
            dbcfg.bActDate = (short)Constants.V1100_ACCESSDB_CFG.bActDate;
            dbcfg.bDeactDate = (short)Constants.V1100_ACCESSDB_CFG.bDeactDate;
            dbcfg.bUseLimit = (short)Constants.V1100_ACCESSDB_CFG.bUseLimit;
            dbcfg.nHostResponseTimeout = (short)Constants.V1100_ACCESSDB_CFG.nHostResponseTimeout;
            dbcfg.nEscortTimeout = (short)Constants.V1100_ACCESSDB_CFG.nEscortTimeout;
            dbcfg.nMultiCardTimeout = (short)Constants.V1100_ACCESSDB_CFG.nMultiCardTimeout;
            dbcfg.nAssetTimeout = (short)Constants.V1100_ACCESSDB_CFG.nAssetTimeout;

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcScpAdbSpec, dbcfg);
            return success;
        }

        public bool Send_CC_ACR_OSDP_PASSTHROUGH(short scpNumber, short acrNumber, int seq, string dataLen, string data)
        {
            bool success = false;

            CC_ACR_OSDP_PASSTHROUGH osdpPt = new CC_ACR_OSDP_PASSTHROUGH
            {
                scp_number = scpNumber,
                acr_number = acrNumber,
                sequence_num = seq,
                reader_role = 0,
                msg_type = 0, // osdp_mfg
                data_len = Convert.ToInt16(dataLen)
            };

            if (data.Length > 0)
            {
                Utilities.ConvertHexStringToByteArray(data, osdpPt.data, 1024);
                success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcAcrOsdpPassthrough, osdpPt);
            }

            return success;
        }

        public bool Send_CC_ACR_LED(short scpNumber, short door_mode, short on_c, short off_c)
        {
            bool success = false;

            CC_RLEDSPC ledCfg = new CC_RLEDSPC
            {
                scp_number = scpNumber,
                led_mode = Constants.LED_MODE,
                rled_id = door_mode,
                on_color = on_c,
                off_color = off_c
            };

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcRledSpc, ledCfg);

            return success;
        }

        public bool Send_CC_ACR_LED_MODE(short scpNumber, short acr_num)
        {
            bool success = false;

            CC_ACRLEDMODE ledCfg = new CC_ACRLEDMODE
            {
                scp_number = scpNumber,
                acr_number = acr_num,
                led_mode = Constants.LED_MODE
            };

            success = _scpdWrite.WriteCommand((short)enCfgCmnd.enCcAcrLedMode, ledCfg);

            return success;
        }
    }
}
