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
    class Reader
    {
        ScpdWrite _scpdWrite;

        public Reader(ScpdWrite scpdWrite)
        {
            _scpdWrite = scpdWrite;
        }

        //////
        // Method: Send_CC_ACR_OSDP_PASSTHROUGH
        // 
        // Send an OSDP passthrough message to an OSDP reader
        //
        //////
        public bool Send_CC_ACR_OSDP_PASSTHROUGH(short scpNumber, short acrNumber, string dataLen, string data)
        {
            bool success = false;

            CC_ACR_OSDP_PASSTHROUGH osdpPt = new CC_ACR_OSDP_PASSTHROUGH
            {
                scp_number = scpNumber,
                acr_number = acrNumber,
                sequence_num = 1,
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

    }
}
