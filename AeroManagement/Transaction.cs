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
    class Transaction
    {
        public Transaction()
        {

        }
        
        private string trasnSrcToString(short src)
        {
            string source = null;
            switch(src)
            {
                case 0:
                    source = "SCP Diagnostic ";
                    break;
                case 1:
                    source = "SCP Com ";
                    break;
                case 2:
                    source = "SCP Local MP ";
                    break;
                case 3:
                    source = "SIO Diagnostic ";
                    break;
                case 4:
                    source = "SIO ";
                    break;
                case 5:
                    source = "SIO Tamper ";
                    break;
                case 6:
                    source = "SIO Power ";
                    break;
                case 7:
                    source = "MP ";
                    break;
                case 8:
                    source = "CP ";
                    break;
                case 9:
                    source = "ACR ";
                    break;
                case 10:
                    source = "ACR Reader Tamper ";
                    break;
                case 11:
                    source = "ACR Door ";
                    break;
                case 13:
                    source = "ACR REX";
                    break;
                case 14:
                    source = "ACR REX2 ";
                    break;
                case 15:
                    source = "Timezone ";
                    break;
                case 16:
                    source = "Proc ";
                    break;
                case 17:
                    source = "Trg ";
                    break;
                case 18:
                    source = "Trg Var ";
                    break;
                case 19:
                    source = "MP Group ";
                    break;
                case 20:
                    source = "Access Area ";
                    break;
                case 21:
                    source = "ACR Alt ";
                    break;
                case 23:
                    source = "SIO Emergency ";
                    break;
                case 24:
                    source = "Login ";
                    break;
                default:
                    source = "Unknown Src " + src.ToString();
                    break;
            }

            return source;
        }
        //////
        // Method: ProcessCmdStatus
        // PreCondition: message.ReplyType = enSCPReplyType.enSCPReplyTransaction
        //////
        public void ProcessCmndStatus(SCPReplyMessage message)
        {
            // string commStatus = null;
            string cmdStatus = null;
            switch (message.cmnd_sts.status)
            {
                case 0:
                    cmdStatus = "Tag " + message.cmnd_sts.sequence_number + " Failed, SCP Offline";
                    break;
                case 1:
                    cmdStatus = "Tag " + message.cmnd_sts.sequence_number + " Success";
                    break;
                case 2:
                    cmdStatus = "Tag " + message.cmnd_sts.sequence_number + " NAK";
                    break;
                default:
                    break;
            }
            //commStatus = "CommStatus, Channel " + message.comm.nChannelId + " Error: " + message.comm.error_code + " Primary Status: " + currentStatus;
            //Global.scpEventQueue.Enqueue(cmdStatus);
            return;
        }
        //////
        // Method: ProcessTransactionLogEvent
        // PreCondition: message.ReplyType = enSCPReplyType.enSCPReplyTransaction
        //////
        public void ProccessCommstatus(SCPReplyMessage message)
        {
          //  string currentStatus = null;
            switch (message.comm.current_primary_comm)
            {
                case 0:
                  //  currentStatus = "Channel Detached";
                    break;
                case 1:
               //     currentStatus = "Comm Not Attempted";
                    break;
                case 2:
                  //  currentStatus = "Controller Offline";
                    if (Global.scp_fw_update)
                    {
                        Global.scp_fw_update = false;
                        Global.scpEventQueue.Enqueue("Firmware Upgrade Success");
                    }
                    Global.scp_online = false;
                    break;
                case 3:
                  //  currentStatus = "Controller Online";
                    Global.scp_online = true;
                    break;
                default:
               //     currentStatus = "Unknown";
                    break;
            }
            //Global.scpEventQueue.Enqueue(currentStatus);
            return;
        }
         public void ProccessIDReport(SCPReplyMessage message)
        {
            string idReportStatus = null;
            DateTime tempTime = new DateTime(1970, 1, 1).AddSeconds(message.id.e_sec);
            string displayTime = tempTime.ToString("dddd, dd MMMM yyyy HH: mm:ss");
            string macAddr = BitConverter.ToString(message.id.mac_addr);
            string displayMacAddr = macAddr.Substring(15, 2) + ":" + macAddr.Substring(12, 2) + ":" + macAddr.Substring(9, 2) + ":" + macAddr.Substring(6, 2) + ":" + macAddr.Substring(3, 2) + ":" + macAddr.Substring(0, 2);
            string firmware = message.id.sft_rev_major + "." + message.id.sft_rev_minor + "." + message.id.cumulative_bld_cnt;
            idReportStatus = "Controller MAC: " + displayMacAddr + ", FW: " + firmware + ", Card Users: " + message.id.db_active;
            Global.scpEventQueue.Enqueue(idReportStatus);
            return;
        }

        public void ProccessSCPReplySrSio(SCPReplyMessage message)
        {
            string sioStatus = null;
            sioStatus = "SIO " + message.sts_sio.number + " " + deviceNumToModel(message.sts_sio.model) + " FW: " + message.sts_sio.revision;
            Global.scpEventQueue.Enqueue(sioStatus);
            return;
        }


        public void ProcessTransactionLogEvent(SCPReplyMessage message)
        {
            // message.ReplyType is a transaction log event (known and part of the pre-condition)
            // There are many reply types. Reply type 07 (enSCPReplyTransaction) is a transaction log event.
            // Check HIDAero_Scp_in.h for SCP_REPLYTYPE and find the associated structure variable “tran”. 
            // Next, look at message.tran.tran_type which contains the transaction type
            // SCPReplyTransaction tran; // enSCPReplyTransaction
            switch (message.tran.tran_type)
            {
                // case (short)tranType.xxxxx
                // case (short)tranType.xxxxx
                // case (short)tranType.xxxxx
                // ...
                case (short)tranType.tranTypeSioComm: ProcessTransactionSioComm(message); break;
                case (short)tranType.tranTypeCardBin: ProcessTransactionUnknowCardFormat(message); break;
                //  case (short)tranType.tranTypeCardBin: ProcessTransactionCardBin(message); break; Old code from before
                case (short)tranType.tranTypeCardFull: ProcessTransactionUnknownCard(message); break;
                case (short)tranType.tranTypeCardID: ProcessTransactionKnownCard(message); break;
                case (short)tranType.tranTypeCoS: ProcessTransactionMP(message); break;
                case (short)tranType.tranTypeREX: ProcessTransactionRex(message); break;
                case (short)tranType.tranTypeCoSDoor: ProcessTransactionCosDoor(message); break;
                case (short)tranType.tranTypeAcr: ProcessTransactionAcr(message); break;
                // There are many transaction types. Transaction type Trans Type 3:x (tranTypeCardBin) is
                // used when the controller sends binary card data after a card is read.
                case (short)tranType.tranTypeI64CardFull: ProcessTransaction64Full(message); break;
                case (short)tranType.tranTypeI64CardID: ProcessTransaction64KnownCard(message); break;
                case (short)tranType.tranTypeAsci: ProcessTransactionAsci(message); break;

                default: break;
            }

        }
        private string deviceNumToModel(short nProdID)
        {
            switch (nProdID)
            {
                case 190:
                    return "V100";
                case 191:
                    return "V200";
                case 192:
                    return "V300";
                case 193:
                    return "X100";
                case 194:
                    return "X200";
                case 195:
                    return "X300";
                case 196:
                    return "X1100";
                default:
                    return "Unknown Device " + nProdID.ToString();
            }
        }
        private void ProcessTransactionMP(SCPReplyMessage message)
        {
            switch (message.tran.tran_code)
            {
                case 3:
                    Global.scpEventQueue.Enqueue(trasnSrcToString(message.tran.source_type) + message.tran.source_number + ":  Normal");
                    break;
                case 4:
                case 5:
                    Global.scpEventQueue.Enqueue(trasnSrcToString(message.tran.source_type) + message.tran.source_number + ":  Alarm!");
                    break;
                default: break;
            }
        }
        private void ProcessTransactionSioComm(SCPReplyMessage message)
        {
            switch (message.tran.s_comm.comm_sts)
            {
                case 2:
                    Global.scpEventQueue.Enqueue(trasnSrcToString(message.tran.source_type) + message.tran.source_number + ": " + deviceNumToModel(message.tran.s_comm.nProductId) + " Offline");
                    break;
                case 3:
                    if(message.tran.source_number == 0)
                    {
                        Global.scpEventQueue.Enqueue(trasnSrcToString(message.tran.source_type) + message.tran.source_number + ": " + deviceNumToModel(message.tran.s_comm.nProductId) + " Online");
                    }
                    else if (message.tran.source_number > 0 && message.tran.source_number < 4)
                    {
                        if (Global.sio_encrypt[(message.tran.source_number - 1)])
                        {
                            Global.scpEventQueue.Enqueue(trasnSrcToString(message.tran.source_type) + message.tran.source_number + ": " + deviceNumToModel(message.tran.s_comm.nProductId) + " Online, Communication Encrypted");
                        }
                        else
                        {
                            Global.scpEventQueue.Enqueue(trasnSrcToString(message.tran.source_type) + message.tran.source_number + ": " + deviceNumToModel(message.tran.s_comm.nProductId) + " Online, Communication Unencrypted");
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private void ProcessTransactionUnknowCardFormat(SCPReplyMessage message)
        {
            switch (message.tran.tran_code)
            {
                case 1:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Unknown " + message.tran.c_bin.bit_count + " bits format, Card:" + Utilities.ConvertHexStringToByteArray(message.tran.c_bin.bit_array, message.tran.c_bin.bit_count));
                    break;
                default: break;
            }
        }
        private void ProcessTransactionCosDoor(SCPReplyMessage message)
        {
            switch (message.tran.tran_code)
            {
                case 3:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Door Forced Open Recovered.");
                    break;
                case 4:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": DOOR FORCED OPEN!");
                    break;
                default: break;
            }
        }
        private void ProcessTransactionAcr(SCPReplyMessage message)
        {
            switch (message.tran.tran_code)
            {
                case 1:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": ACR Disabled");
                    break;
                case 2:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": ACR Unlocked");
                    break;
                case 3:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": ACR Locked");
                    break;
                case 4:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": ACR Facility Code Only");
                    break;
                case 5:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": ACR Card Only");
                    break;
                case 6:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": ACR PIN Only");
                    break;
                case 7:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": ACR Card and PIN");
                    break;
                case 8:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": ACR Card or PIN");
                    break;
                default: break;
            }
        }

        private void ProcessTransactionRex(SCPReplyMessage message)
        {
            switch (message.tran.tran_code)
            {
                case 2:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": REX Button, Door Not Open");
                    break;
                case 3:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number  + ": REX Button, Door Opened");
                    break;
                case 5:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number  + ": Software REX, Door Not Open");
                    break;
                case 6:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Software REX, Door Opened");
                    break;
                default: break;
            }
        }

        private void ProcessTransactionKnownCard(SCPReplyMessage message)
        {

            switch (message.tran.tran_code)
            {
                case 1:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Deactiviated card for " + message.tran.c_id.cardholder_id);
                    break;
                case 2:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Before Activation for " + message.tran.c_id.cardholder_id);
                    break;
                case 3:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Expired card for " + message.tran.c_id.cardholder_id);
                    break;
                case 4:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Out of Schedlue for " + message.tran.c_id.cardholder_id);
                    break;
                case 5:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Incorrect PIN for " + message.tran.c_id.cardholder_id);
                    break;
                case 6:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Anti-Passback violation for " + message.tran.c_id.cardholder_id);
                    break;
                case 10:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Duress! Door Open for " + message.tran.c_id.cardholder_id);
                    break;
                case 11:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Duress! Door Not Open for " + message.tran.c_id.cardholder_id);
                    break;
                case 12:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Grant, Door Not Open for " + message.tran.c_id.cardholder_id);
                    break;
                case 13:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Grant, Door Open for " + message.tran.c_id.cardholder_id);
                    break;
                case 14:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, No Access Assigned for " + message.tran.c_id.cardholder_id);
                    break;
                default: break;
            }
        }

        private void ProcessTransactionUnknownCard(SCPReplyMessage message)
        {
            switch (message.tran.tran_code)
            {
                case 1:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, ACR is in lockdown mode!");
                    break;
                case 3:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Unregistered Facility Code: " + message.tran.c_full.facility_code + ", Card: " + message.tran.c_full.cardholder_id);
                    break;
                case 5:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Unknow Card: " + message.tran.c_full.cardholder_id);
                    break;
                default: break;
            }
        }

        private void ProcessTransaction64Full(SCPReplyMessage message)
        {
            switch (message.tran.tran_code)
            {
                case 1:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, ACR is in lockdown mode!");
                    break;
                case 3:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Unregistered Facility Code: " + message.tran.c_fulli64.facility_code + ", Card: " + message.tran.c_fulli64.cardholder_id);
                    break;
                case 5:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Unknow Card: " + message.tran.c_fulli64.cardholder_id);
                    break;
                default: break;
            }
        }

        private void ProcessTransaction64KnownCard(SCPReplyMessage message)
        {

            switch (message.tran.tran_code)
            {
                case 1:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Deactiviated card for " + message.tran.c_idi64.cardholder_id);
                    break;
                case 2:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Before Activation for " + message.tran.c_idi64.cardholder_id);
                    break;
                case 3:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Expired card for " + message.tran.c_idi64.cardholder_id);
                    break;
                case 4:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Out of Schedlue for " + message.tran.c_idi64.cardholder_id);
                    break;
                case 5:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Incorrect PIN for " + message.tran.c_idi64.cardholder_id);
                    break;
                case 6:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, Anti-Passback violation for " + message.tran.c_id.cardholder_id);
                    break;
                case 10:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Duress! Door Open for " + message.tran.c_idi64.cardholder_id);
                    break;
                case 11:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Duress! Door Not Open for " + message.tran.c_idi64.cardholder_id);
                    break;
                case 12:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Grant, Door Not Open for " + message.tran.c_idi64.cardholder_id);
                    break;
                case 13:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Grant, Door Open for " + message.tran.c_idi64.cardholder_id);
                    break;
                case 14:
                    Global.scpEventQueue.Enqueue("ACR " + message.tran.source_number + ": Access Denied, No Access Assigned for " + message.tran.c_idi64.cardholder_id);
                    break;
                default: break;
            }
        }

        private void ProcessTransactionAsci(SCPReplyMessage message)
        {
            string diag = new string(message.tran.t_diag.bfr);
             if (diag.Contains("SIO COMM-1 AES256 Rmaci"))
            {
                Global.sio_encrypt[0] = true;
            }
            else if (diag.Contains("SIO COMM-2 AES256 Rmaci"))
            {
                Global.sio_encrypt[1] = true;
            }
            else if (diag.Contains("SIO COMM-3 AES256 Rmaci"))
            {
                Global.sio_encrypt[2] = true;
            }
        }

        //////
        // Method: ProcessTransactionCardBin
        //   Use this transaction to get binary card data after a card is read. Evaluate attributes such as:
        //     message.tran.source_type = which object in the system reported the transaction, example, access control reader 0x09 (reference chapter 11)
        //     message.tran.tran_code = the transaction code tied to this transaction type
        //
        // PreCondition: 
        //   message.ReplyType is a log event
        //   message.tran.tran_type = tranType.tranTypeCardBin
        // PostCondition: 
        //   Transaction data is output to the UI
        //////
        private void ProcessTransactionCardBin(SCPReplyMessage message)
        {
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "********** Below is logging from the sample application **********\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "*********************");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "**Transaction Found**");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "*********************");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "TypeCardBin 3:" + message.tran.source_type);
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "Transaction Serial Number: " + message.tran.ser_num);
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "Transaction Time         : " + message.tran.time);
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "Transaction Code         : " + message.tran.tran_code);
            
            // transaction codes correlate to the transaction type; or TypeCardBin (this method)...
            switch (message.tran.tran_code)
            {
                case 1: SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "Transaction Code Lookup  : Access Denied, Invalid Card Format"); break;
                default: break;
            }

            // tranTypeCardBin is the enumeration listed for trans type 3:x. Naviate the API document to find the associated reply structure TypeCardBin
            // Fields returned by the transaction include:
            //
            //   bit_count: number of valid data bits
            //   bit_array[100]: bit array
            //
            // In order to access the fields returned, you must find the object within message.tran to reference. Find the object name by:
            //
            //   1. search C header files for SCP_TRANS_TYPE > union structure TypeCardBin > object to reference is c_bin or
            //   2. scan assembly object browser members for HID.Aero.ScpdNet.Wrapper > SCPReplyMessage.SCPReplyTransaction, scan objects one by one
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "Transaction Code         : " + message.tran.tran_code);
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "Bit Count                : " + message.tran.c_bin.bit_count);
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "Bit Array                : " + Utilities.ConvertHexStringToByteArray(message.tran.c_bin.bit_array));

            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "********** Above is logging from the sample application **********\n");
            SCPDLL.scpDebug((int)enSCPDebugLevel.enSCPDebugToFile, "\n");
            
        }
    }
}
