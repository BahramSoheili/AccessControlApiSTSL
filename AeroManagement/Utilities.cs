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
using System.Globalization;

namespace AeroManagement
{
    public static class Utilities
    {
       public static bool ConvertHexStringToByteArray(String pHexString, byte[] pByteArray, int maxSize)
        {
            int iLen = pHexString.Length;
            bool rv = false;
            byte byteVal = 0;

            if (iLen > 0)
            {
                if (pHexString.Length % 2 == 0)
                {
                    for (int index = 0; index < (iLen / 2); index++)
                    {
                        string byteValue = pHexString.Substring(index * 2, 2);
                        byteVal = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                        pByteArray[index] = byteVal;
                    }

                    rv = true;
                }
            }

            return rv;
        }

       public static string ConvertHexStringToByteArray(byte[] pByteArray)
        {
            return BitConverter.ToString(pByteArray);
        }

       public static string ConvertHexStringToByteArray(byte[] pByteArray, int bitlength)
        {
            int size = 8;
            if (bitlength % 8 == 0)
            {
               size = (bitlength / 8);
            }
            else
            {
               size = (bitlength / 8) + 1;
            }
            byte[] result = new byte[size];
            Array.Copy(pByteArray, 0, result, 0, size);
            return BitConverter.ToString(result);
        }


    }
}
