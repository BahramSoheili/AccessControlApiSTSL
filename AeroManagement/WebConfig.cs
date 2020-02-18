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

namespace AeroManagement
{
    class WebConfig
    {
        public WebConfig()
        {
        }

        //////
        // Method: ProcessWebConfigMessage
        // PreCondition: message.ReplyType = enSCPReplyType.enSCPReplyWebConfigxxxx message
        //////
        public void ProcessWebConfigMessage(SCPReplyMessage message)
        {
            // message.ReplyType is a web config network transaction (known and part of the pre-condition)
            switch (message.ReplyType)
            {
                // case (short)tranType.xxxxx
                // case (short)tranType.xxxxx
                // case (short)tranType.xxxxx
                // ...

                // There are many web config read_types. Read type 3 returns Web Config Host Comm enumeration
                case (int)enSCPReplyType.enSCPReplyWebConfigHostCommPrim:	                // 28 - Web Config Host Comm
                    ProcessWebConfigHostCommPrim(message);
                    break;

                default: break;
            }

        }

        //////
        // Method: ProcessWebConfigHostCommPrim
        //   Use this message to read back or write the web configuration for primary host communication settings.
        //
        // PreCondition: 
        //   message.ReplyType is enSCPReplyType.enSCPReplyWebConfigHostCommPrim
        // PostCondition: 
        //   Transaction data is output to the UI
        //////
        private void ProcessWebConfigHostCommPrim(SCPReplyMessage message)
        {
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "********** Below is logging from the sample application **********\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "****************************************\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "**WebConfig HostCommPrim Message Found**\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "****************************************\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "Enumeration: enSCPReplyWebConfigHostCommPrim\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "scp_number               : " + message.web_host_comm_prim.scp_number + "\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "address                  : " + message.web_host_comm_prim.address + "\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "dataSecurity             : " + message.web_host_comm_prim.dataSecurity + "\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "cType                    : " + message.web_host_comm_prim.cType + "\n");

            if (message.web_host_comm_prim.cType == 1) // if cType is IP Server, the underlying C union returned will include only the specific fields logged below
            {
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipserver.cAuthIP1        : " + message.web_host_comm_prim.ipserver.cAuthIP1 + "\n");
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipserver.cAuthIP2        : " + message.web_host_comm_prim.ipserver.cAuthIP2 + "\n");
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipserver.nPort           : " + message.web_host_comm_prim.ipserver.nPort + "\n");
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipserver.enableAuthIP    : " + message.web_host_comm_prim.ipserver.enableAuthIP + "\n");
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipserver.nNicSel         : " + message.web_host_comm_prim.ipserver.nNicSel + "\n");
            }
            else if (message.web_host_comm_prim.cType == 2) // if cType is IP client, the underlycing C untion returned will include only the specific fields logged below
            {
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipclient.cHostIP         : " + message.web_host_comm_prim.ipclient.cHostIP + "\n");
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipclient.nPort           : " + message.web_host_comm_prim.ipclient.nPort + "\n");
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipclient.rqIntvl         : " + message.web_host_comm_prim.ipclient.rqIntvl + "\n");
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipclient.connMode        : " + message.web_host_comm_prim.ipclient.connMode + "\n");
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipclient.cHostName       : " + String.Concat(message.web_host_comm_prim.ipclient.cHostName) + "\n");
                SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "ipclient.nNicSel         : " + message.web_host_comm_prim.ipclient.nNicSel + "\n");
            }

            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "********** Above is logging from the sample application **********\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "\n");
            
        }
    }
}
