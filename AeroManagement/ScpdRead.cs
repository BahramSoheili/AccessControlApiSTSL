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
     public class ScpdRead
    {
        private bool shutdownFlag; 
        
        public ScpdRead()
        {
            shutdownFlag = false;
        }

        // The main program will signal for thread shutdown using this method
        public void SetShutdownFlag() 
        {
            SCPDLL.scpDebugSet((int)enSCPDebugLevel.enSCPDebugToFile);
            
            Thread.Sleep(100);
            shutdownFlag = true;
        }

        // Entry point for the thread which will keep the thread alive until shutdown is signaled
        public void GetScpdMessagesUntilShutdown()
        {
            while (shutdownFlag == false)
            {
                GetScpdMessage();
                Thread.Sleep(20);
            }
        }

        private void GetScpdMessage()
        {
            // The reply message class is populated with replies from the driver
            SCPReplyMessage message = new SCPReplyMessage();

            // This method runs the driver and must be continuously called.
            // Without continuously calling this method, 
            // connections will fall offline and transactions
            // will not make their way into the host software.
            if (message.GetMessage())
            {            
                ProcessMessage(message);
            }
        }

        private void ProcessMessage(SCPReplyMessage message)
        {
            Transaction transaction = new Transaction();
            WebConfig webConfig = new WebConfig();

            // message.SCPId contains the SCPID that sent the message
            // message.ReplyType contains the reply type - test the reply type to determine how to process. 
            // See HIDAero_scp_in.h > Enumeration enSCPReplyType for enumerations to test against
            //  All would be processed in a complete application. This sample implements a few examples (not commented out below)
            switch (message.ReplyType)
            {
                // case (int)enSCPReplyType.enSCPReplyUnknown:                              // 00 - not recognized: passed on as is
                // case (int)enSCPReplyType.enSCPReplyACK: 		                            // 01 - ACK
                case (int)enSCPReplyType.enSCPReplyCommStatus:                            // 02 - comm_status
                    transaction.ProccessCommstatus(message);
                    break;
                // case (int)enSCPReplyType.enSCPReplyNAK                                   // 03 - NAK
                case (int)enSCPReplyType.enSCPReplyIDReport:	                            // 04 - ID report
                    transaction.ProccessIDReport(message);
                    break;
                // case (int)enSCPReplyType.enSCPReplyUTAGReport                            // 05 - UTAG report
                // case (int)enSCPReplyType.enSCPReplyTranStatus                            // 06 - transaction log status

                case (int)enSCPReplyType.enSCPReplyTransaction:                             // 07 - transaction log event > example implemented
                    transaction.ProcessTransactionLogEvent(message);
                    break;

                // case (int)enSCPReplyType.enSCPReplySrMsp1Drvr                        // 08 - status: MSP1 (SIO comm) driver
                case (int)enSCPReplyType.enSCPReplySrSio: 	                            // 09 - status: SIO
                    transaction.ProccessSCPReplySrSio(message);
                    break;
                // case (int)enSCPReplyType.enSCPReplySrMp		                            // 10 - status: Monitor Point
                // case (int)enSCPReplyType.enSCPReplySrCp		                            // 11 - status: Control Point
                // case (int)enSCPReplyType.enSCPReplySrAcr		                            // 12 - status: Access Control Reader
                // case (int)enSCPReplyType.enSCPReplySrTz		                            // 13 - status: Timezone
                // case (int)enSCPReplyType.enSCPReplySrTv		                            // 14 - status: Trigger Variable
                case (int)enSCPReplyType.enSCPReplyCmndStatus:                            // 15 - Direct command delivery status
                    transaction.ProcessCmndStatus(message);
                    break;
                // case (int)enSCPReplyType.enSCPReplySrMpg,			                    // 16 - status: Monitor Point Group
                // case (int)enSCPReplyType.enSCPReplySrArea,			                    // 17 - status: Access Area
                // case (int)enSCPReplyType.enSCPReplyBioAddResult,		                    // 19 - Bio Add/Modify/Delete Result
                // case (int)enSCPReplyType.enSCPReplyStrStatus,		                    // 20 - SCP Structure Status report
                // case (int)enSCPReplyType.enSCPReplyCmndStatusExt,                        // 23 - Extended Direct command delivery status
                // case (int)enSCPReplyType.enSCPReplyLoginInfo,		                    // 24 - Login info
                // case (int)enSCPReplyType.enSCPReplyPkgInfo,			                    // 25 - Installed package information
                // case (int)enSCPReplyType.enSCPReplyWebConfigNotes,		                // 26 - Web Config Notes
                // case (int)enSCPReplyType.enSCPReplyWebConfigNetwork:                     // 27 - Web Config Network > example implemented

                case (int)enSCPReplyType.enSCPReplyWebConfigHostCommPrim:	                // 28 - Web Config Host Comm
                    webConfig.ProcessWebConfigMessage(message);
                    break;
                // case (int)enSCPReplyType.enSCPReplyWebConfigHostCommAlt		            // 29 - Web Config Host Comm
                // case (int)enSCPReplyType.enSCPReplyWebConfigSessionTmr		            // 30 - Web Config Session Timer
                // case (int)enSCPReplyType.enSCPReplyWebConfigWebConn			            // 31 - Web Config Web Connection
                // case (int)enSCPReplyType.enSCPReplyWebConfigAutoSave		                // 32 - Web Config Auto Save
                // case (int)enSCPReplyType.enSCPReplyWebConfigNetDiag			            // 33 - Web Config Network Diagnostic
                // case (int)enSCPReplyType.enSCPReplyWebConfigTimeServer		            // 34 - Web Config Time Server
                // case (int)enSCPReplyType.enSCPReplyWebConfigCentralStation	            // 35 - Web Config Central station
                // case (int)enSCPReplyType.enSCPReplyWebConfigCardDBSize		            // 36 - Web Config Card DB Size
                // case (int)enSCPReplyType.enSCPReplyWebConfigDiagnostics		            // 37 - Web Config Diagnostics
                // case (int)enSCPReplyType.enSCPReplyCertInfo					            // 38 - Web Config Cert Info
                // case (int)enSCPReplyType.enSCPReplyElevRelayInfo				            // 39 - Elevator Relay Status Info
                // case (int)enSCPReplyType.enSCPReplySrFileInfo				            // 40 - File Information
                // case (int)enSCPReplyType.enSCPReplyOsdpPassthrough = 50	                // 50 - OSDP passthrough
                // case (int)enSCPReplyType.enSCPReplySioRelayCounts		                // 51 - SIO relay counts

                // configuration read commands
                // case (int)enSCPReplyType.enSCPReplyAdbs=enCcScpAdbSpec,
                // case (int)enSCPReplyType.enSCPReplyAccException=enCcAccException,
                // case (int)enSCPReplyType.enSCPReplyMP=enCcMP,
                // case (int)enSCPReplyType.enSCPReplyCP=enCcCP,
                // case (int)enSCPReplyType.enSCPReplyACR=enCcACR,
                // case (int)enSCPReplyType.enSCPReplyTimezone=enCcScpTimezone,
                // case (int)enSCPReplyType.enSCPReplyHoliday=enCcScpHoliday,
                // case (int)enSCPReplyType.enSCPReplyAlvl=enCcAlvl,
                // case (int)enSCPReplyType.enSCPReplyTrgr=enCcTrgr,
                // case (int)enSCPReplyType.enSCPReplyTrgr128=enCcTrgr128,
                // case (int)enSCPReplyType.enSCPReplyCard=enCcAdbCardI64DTic32,
                // case (int)enSCPReplyType.enSCPReplyCard255FF=enCcAdbCardI64DTic32A255FF,
                // case (int)enSCPReplyType.enSCPReplyMemRead=enCcMemRead

                default: break;
            }
        }

        
    }
}
