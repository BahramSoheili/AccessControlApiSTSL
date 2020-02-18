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
     public class ScpdWrite
    {
        public ScpdWrite()
        {
        }

        //Get DriverVersion
        public int DriverVersion()
        {
            return SCPDLL.scpGetDllVersion();
        }

        //////
        // Method: TurnOnDebug
        // 
        // Turn on debugging to a debug file
        //
        //////
        public bool TurnOnDebug()
        {
            // Sets the debugging level to the level passed in. When debugging is enabled the following file name will be used: SCPDEBUG.txt.
            // Some example debug levels are:
            // 
            //   enSCPDebugOff: Turn off debugging
            //   enScpDebugToFile: Turn on debugging to a debug file
            //
            // See API reference for more details
            //
            SCPDLL.scpDebugSetFile((int)enSCPDebugLevel.enSCPDebugToFile, "AeroDemoDebug");
            return SCPDLL.scpDebugSet((int)enSCPDebugLevel.enSCPDebugToFile);
        }
        public int GetLastCmdTag()
        {
            return SCPDLL.scpGetTagLastPosted(Constants.DEMO_SCP_ID);
        }
        //////
        // Method: WriteCommand
        // PreCondition: SCP online
        // PostCondition: Pass/Fail response is sent from the driver
        //////
        public bool WriteCommand(string command)
        {
            // send the command
            return SCPDLL.scpConfigCommand(command);
        }

        //////
        // Method: WriteCommand
        // PreCondition: SCP online
        // PostCondition: Pass/Fail response is sent from the driver
        //////
        public bool WriteCommand(short command, IConfigCommand cfg)
        {
            SCPConfig scp = new SCPConfig();
            bool success = scp.scpCfgCmndEx((Int16)command, cfg);
            return success;
        }

        //////
        // Method: WriteDriverAndCommunicationInitialization
        // PreCondition: HIDAeroWrap64 (or HIDAeroWrap for x86) is referenced by the project
        // PostCondition: Pass/Fail response is sent from the driver
        //   If the command is meant for the driver, the pass indication from the driver indicates that the command was executed. 
        //   If the command is meant for the intelligent controller, the pass indication from the driver indicates that the command was \
        //     successfully placed into the drivers command queue for subsequent intelligent controller command download. * this method * 
        //////
        public void WriteDriverAndCommunicationInitialization(string ip)
        {
            // System Level Specification
            // Command 011 (CC_SYS): Use to allocate the basic system structures
            // Field		Name		Comments
            // 1		011		        System Level Specification Command
            // 2		nPorts		    Maximum number of communication channels, from 1-1024
            // 3		nScps		    Maxinum number of SCPs (intelligent controllers), from 1-1024
            // 4		nTimeZones	    Not used, set to 0
            // 5		nHolidays		Not used, set to 0
            // 6		bDirectMode	    Not used, set to 1
            // 7		debug_rq		Not used, set to 0
            // 8-11		nDebugArg[4]	Not used, set to 0
            SCPDLL.scpConfigCommand("11 512 512 0 0 1 0 0 0 0 0");

            // Create Channel
            // Command 012 (CC_CHANNEL): Use to create a communication channel between driver and SCP
            // Field		Name		Comments
            // 1		012		        Create Channel Command
            // 2		nChannelId	    Channel number to create
            // 3		cType		    Channel Type - 4 TCP/IP IP Server
            // 4		cPort		    TCP/IP port for IP client mode
            // 5		baud_rate		Not used, set to 0
            // 6		timer1		    SCP reply timeout
            // 7		timer2		    TCP/IP retry connect interval
            // 8		cModemId[64]	Not used, set to 0
            // 9		cRTSMode		Not used, set to 0
            SCPDLL.scpConfigCommand("012 250 4 0 0 2800 5000 \"\" 0");

            // Create SCP
            // Command 1013 (CC_NEWSCP_LN): Use to define the connection parameters for an intelligent controller
            // Field		Name			Comments
            // 1		1013			    Create SCP Command
            // 2		nSCPId			    SCP number
            // 3		address			    Not used, set to 0
            // 4		nCommAccess		    Type of communication - will attached to 012 channel
            // 5		e_max			    Retry count before SCP is offline
            // 6		poll_delay		    Minimum time before SCP is offline
            // 7		cCommString[128]	IP address of DHCP host name
            // 8		cPassword			Logon password
            // 9		offline_time		Number of milliseconds between messages before SCP goes offline 
            // 10		nAltPortEnable		Not used, set to 0
            // 11		nAltPortCommAccess	Not used, set to 0
            // 12 		nAltPortPoll_delay	Not used, set to 0
            // 13		cAltPortCommString[32]	Nto used, set to NULL
            SCPDLL.scpConfigCommand("1013 1234 0 4 3 5000 \"" + ip + "\" \"password\" 15000  0  0  0  0 \"\"");

            // Attach SCP to Channel
            // Command 0207 (CC_ATTACHSCP): Use to attach the communication of the driver to the SCP 
            // Field		Name		Comments
            // 1		0207		    Attach SCP to Channel Command
            // 2		nSCPId		    SCP number - from 1013 command
            // 3		nChannelId	    Channel ID - from 012 command
            // 4		nToAltPort	    Not used, set to 0
            SCPDLL.scpConfigCommand("0207 1234 250 0");
        }

        //////
        // Method: HardwareAndDBInitialization
        // 
        // Setup hardware and DB initialization. If commands setup in this method are failing, RESET_SCP
        //
        //////
        public void HardwareAndDBInitialization()
        {
            //////
            // SCP Device Specification CC_SCP_SCP
            // nMsp1Port(8) = 2 // # of RS-485 interface ports
            // nSio(10) = 8 // # of SIOs
            // nAcr(13) = 64 // # of ACRs
            //
            //                          1 2    3 4 5 6 7 8     9 10  11  12 13  14  15  16    17  18  19 20 21  22 23 24 25 26 27
            SCPDLL.scpConfigCommand("1107 0 1234 0 0 0 0 2 80000  8   7   4 64 128 128 128 28800 100 100 50 64   0  0  0  0 10  0");

            //////
            // Access DB Specification CC_SCP_ADBS
            // nCards(4) = 100 ; number of cardholder records to allocate
            // nAlvl(5) = 8 ; number of access levels per cardholder
            // nPinDigits(6) = 10572 = 0x294C
            // least significant lower nibble = 0xC = 12 ; number of PIN digits to store
            // least significant upper nibble = 0x4 = 4 ; number of bytes in the CardId in excess of 4 (? 4 + 4 = 8 bytes = 64 bits)
            // upper byte lower nibble = 0x0900 ; pin duress digit constant
            // upper byte upper nibble = 0x2900 ; 0x2000 pin_duress_append and 0x0900 ? 
            //                          1 2    3   4 5     6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42
            SCPDLL.scpConfigCommand("1105 0 1234 100 8 10572 0 0 2  2  0  0  1  0  0  0  0  0  0  0 16  1 16  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0");

            //////
            // Driver Configuration CC_MSP1
            // msp1_number(4) = same as "sio driver number", unique on SCP, range 0 - nMsp1Port (CC_SCP_SCP)
            // port_number(5) = physical port number on SCP, port 1 X1100 SIO RS-485, port 3 is internal SIO
            //
            // rs-485 #0 is on-board port 3
            // 
            //                         1 2    3 4 5  6     7
            SCPDLL.scpConfigCommand("108 0 1234 0 3  0     90");

            //////
            // SIO Panel Config CC_SIO
            // sio_number(4) = sio number on scp, unique on scp, range of 0 to nSio-1 from 1107
            // inputs(5)
            // outputs(6)
            // readers(7)
            // model(8) = 196 for on-board X1100
            // port(13) = msp1 port - msp1_number to attach to. From 108.
            // 
            // sio #0 is on-board => attached to rs-485 from 108 = 0
            //
            //                         1 2    3 4  5  6 7   8 9 10 11 12 13 14 15 16 17 18
            SCPDLL.scpConfigCommand("109 0 1234 0  7  4 4 196 0 0  -1  1  0  0  0  0  3  0");

            //////
            // Reader Specification CC_RDR
            // sio_number(4) = from 109 sio_number
            // reader(5) = reader number on this sio; range 0 to nReaders-1
            // 
            // osdp_flags(9) =
            //   +1 for 9600 baud
            //   +4 for 115.2k baud
            //   +128 for secure channel
            //   +16 for logging
            // For Logging CC = Panel, 33 = Reader, 55 = Error
            // 
            // reader #0/sio #0 is on-board reader #1
            // 
            //                         1 2    3 4 5 6 7 8 9
            SCPDLL.scpConfigCommand("112 0 1234 0 0 1 2 7 4");

            //////
            // Access Control Reader Configuration CC_ACR
            // acr_number(4) = unique on the SCP
            // rdr_sio(7) = the sio_number on the SCP that contains the reader (109 CC_SIO)
            // rdr_number(8) = the reader number on the SIO (112 CC_RDR)
            // strk_sio(9) = the sio number on the SCP that contains the strike relay
            // strk_number(10) = the relay number on the sio
            // 
            // acr    ->reader #, sio # 
            // acr #0 -> sio_r 0, reader 0, sio_s 0, strk 0
            // 
            // 1 2    3 4 5 6 7 8  9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26  27 28 29 30 31 32 33 34 35 36 37 38 39 40 41
            SCPDLL.scpConfigCommand("115 0 1234 0 0 0 0 0  0  0  1  5  1  3  0 10  3  1 -1  0  0  0 -1  0  0 255  0  0  0  0 32  4  8  1  4 20 12 20  0  0  0");
        }

        public void X1100AddCard(string card, string pin, string access_level)
        {
            SCPDLL.scpConfigCommand("5304 0 1234  1 " + card  +  " 0 \"" + pin +"\" 1 0 0 0 0 0 0  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0  0  0 1131447600 - 1  0  0  0   0 1 0 0 0 0 0 0 0 0 0 0 0  0 0 0 0 0 0 0 0 0 0  0 0 0 0 0 0 0 0 0 0  0 0 0 0 0 0 0 0 0 0  0 0 0 0 0 0 0 0 0 0   0 0 0 0 0 0 0 0 0 0   0 0 0 0 0 0 0 0 0 0   0");
        }

        public void X1100DeleteCard(string card)
        {
            SCPDLL.scpConfigCommand("3305 1234  " + card);
        }

        public void HardwareAndDBInitializationX1100()
        {
            //X1100. V100 
            //Monitor Point AC Fail, Battery Fail, Tamper
            //Control Point Bottom Left and Right Relay
            //ACR 0 Left RK40 OSDP
            //ACR 1 Right R10 Weigand 

            //Set SCP 
            SCPDLL.scpConfigCommand("1107 0 1234 0 0 0 0 4 10000 32 615 388 64 255 1000 1000 - 28800 0 255 100 0 100000 0 1 0 0");

            //Set AccessDB
            SCPDLL.scpConfigCommand("1105 0 1234  10000 32 4 0 1 2 2 0 0 0 1 0 0 0  5 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 15 15 10 0 0");

            //Set Time and DST
            SCPDLL.scpConfigCommand("302 1234");

            //Set format 0 no format 1 26bit facility code 1, 2 26bit fc 99
            SCPDLL.scpConfigCommand("1102 0 1234 0 - 1 0 1 1 32 0 0 0 0 0 0 32 0 0 0");
            SCPDLL.scpConfigCommand("1102 0 1234 1 1 0 1 0 26 13 0 13 13 8 1 16 9 0 0");
            SCPDLL.scpConfigCommand("1102 0 1234 2 99 0 1 0 26 13 0 13 13 8 1 16 9 0 0");

            //Set Timezone
            SCPDLL.scpConfigCommand("3103 0 1234 1 1 0 0 1 127 0 1439 \"\"");

            //Set Access Level
            SCPDLL.scpConfigCommand("2116 0 1234 1 0 1 1 1 1 1 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0");

            //Set AV Behavior, Access Grant, Idle, Access Deny
            SCPDLL.scpConfigCommand("122 0 1234 1 5 1 1 0 0 1 0 0 0");
            SCPDLL.scpConfigCommand("122 0 1234 1 7 1 1 0 0 1 0 0 0");
            SCPDLL.scpConfigCommand("122 0 1234 1 12 2 1 50 0 1 1 0 0");
            SCPDLL.scpConfigCommand("122 0 1234 1 11 1 0 5 5 2 2 0 0");

            //Set RS485 Bus, all SIO runs at 38400 for baud rate
            SCPDLL.scpConfigCommand("108 0 1234 0 3  0 90 0 0");

            //Set SIO
            SCPDLL.scpConfigCommand("109 0 1234 0  7  4 4 196 0 0 - 1  1  0  0  0  0  3  0");

            //Set Input & outputs
            SCPDLL.scpConfigCommand("110 0 1234 0 0 0 4 0");
            SCPDLL.scpConfigCommand("110 0 1234 0 1 1 4 0");
            SCPDLL.scpConfigCommand("110 0 1234 0 2 0 4 0");
            SCPDLL.scpConfigCommand("110 0 1234 0 3 1 4 0");
            SCPDLL.scpConfigCommand("110 0 1234 0 4 1 4 0");
            SCPDLL.scpConfigCommand("110 0 1234 0 5 1 4 0");
            SCPDLL.scpConfigCommand("110 0 1234 0 6 1 4 0");
            SCPDLL.scpConfigCommand("111 0 1234 0 0 0");
            SCPDLL.scpConfigCommand("111 0 1234 0 1 0");
            SCPDLL.scpConfigCommand("111 0 1234 0 2 0");
            SCPDLL.scpConfigCommand("111 0 1234 0 3 0");

            //Set Monitor points on X1100 Tamper, AC Fail, Battery Fail
            SCPDLL.scpConfigCommand("113 0 1234 0 0 4 0 0 0  0");
            SCPDLL.scpConfigCommand("113 0 1234 1 0 5 0 0 0  0");
            SCPDLL.scpConfigCommand("113 0 1234 1 0 6 0 0 0  0");

            //Set Control Points on X300
            SCPDLL.scpConfigCommand("114 0 1234 0 0 1 1");
            SCPDLL.scpConfigCommand("114 0 1234 1 0 3 1");

            //Set Readers on X1100 Left OSDP Key pad, Right R10 weigand
            SCPDLL.scpConfigCommand("112 0 1234 0 1 1 0 1");

            //Set ACR on Left and right door
            SCPDLL.scpConfigCommand("115 0 1234 1 0 - 1 0 1  0  2  1  5  1  0  2 10  0  3 - 1  0  0  0 - 1  0  0 255  0  0  0  0 32  1  5  1  0 0 0 0 0  0  0");
       }

        //////
        // Example methods from HID Aero Product Line Overview are below. These methods are not used by the application.
        // Rather, Controller and Reader classes are used to form commands and send to ScpdWrite class.
        //////


        //////
        // Method: WriteCmdAscii
        // PreCondition: HIDAeroWrap64 (or HIDAeroWrap for x86) is referenced by the project
        // PostCondition: Pass/Fail response is sent from the driver
        //   If the command is meant for the driver, the pass indication from the driver indicates that the command was executed
        //   If the command is meant for the intelligent controller, the pass indication from the driver indicates that the command was \
        //     successfully placed into the drivers command queue for subsequent intelligent controller command download. * this method *
        //////
        public bool WriteCmdAscii()
        {
            //////
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
            // Field            1    2  3
            string command = "900 1234  3";

            // Call public static method scpConfigCommand to send the ASCII command string
            bool success = HID.Aero.ScpdNet.Wrapper.SCPDLL.scpConfigCommand(command);

            return success;
        }

        //////
        // Method: WriteCmdClass
        // PreCondition: HIDAeroWrap64 (or HIDAeroWrap for x86) is referenced by the project
        // PostCondition: Pass/Fail response is sent from the driver
        //   If the command is meant for the driver, the pass indication from the driver indicates that the command was executed. 
        //   If the command is meant for the intelligent controller, the pass indication from the driver indicates that the command was \
        //     successfully placed into the drivers command queue for subsequent intelligent controller command download. * this method * 
        //////
        public bool WriteCmdClass()
        {
            //////
            // Sample command 206. 
            //   Find this command by searching the API manual for "206" or by looking up "206" in the HID Aero Product Line Overview \
            //   "API Quick Reference". You will find the associated class "CC_FIRMWARE" in the API manual.
            //   You will find the following parameters in the API manual.
            //
            //  Command 206: SCP Firmware Download
            //    Field         Name            Comments
            //    1             206             Firmware Download
            //    2             scp_number      SCP (intelligent controller) number - unique reference per driver
            //    3             file_name[200]  The path and file name information for the binary firmware file. This is a null \
            //                                    terminated character-string of no more than 200 characters.
            //////

            // Instantiate the SCPConfig class we need to send a command using a command class
            HID.Aero.ScpdNet.Wrapper.SCPConfig command = new SCPConfig();

            // Instantiate the command class for CC_FIRMWARE, command number 206
            HID.Aero.ScpdNet.Wrapper.CC_FIRMWARE firmwareCommand = new CC_FIRMWARE();

            // Set Field #2, the SCP (intelligent controller) reference number
            firmwareCommand.scp_number = 1234; 

            // Set Field #3, path and file name for the binary firmware file
            // The .NET wrapper's marshalling which requires the underlying C data structure be maintained
            // This is why we cannot use methods like sFileName.ToCharArray() in the class constructor
            string fileName = "c:\\HIDAero\\Firmware\\X1100.crc";
            fileName.CopyTo(0, firmwareCommand.file_name, 0, fileName.Length);


            // Call SCPConfig object with firmwareCommand that implements the iConfigCommand interface
            return command.scpCfgCmndEx((Int16)enCfgCmnd.enCcFirmwareDown, firmwareCommand);
        }       
    }
}


