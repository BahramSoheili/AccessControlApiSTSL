/*************************************************************************************
* Copyright © 2019 HID Global Corporation.  All rights reserved.
* This software is protected by copyright law and international treaties, as well as any nondisclosure agreements that you or the organization you represent have signed.  
* Any unauthorized reproduction, distribution or use of the software is prohibited.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HID.Aero.ScpdNet.Wrapper;

namespace AeroManagement
{
    public static class UIOutput
    {
        private readonly  static Object uiOutputLock = new Object();
        public  static void OutputUI(string outputText, bool writeToDriver)
        {
            DateTime now = DateTime.Now;
            string direction = writeToDriver ? "=>" : "<=";

            lock (uiOutputLock)
            {
                Console.WriteLine("{0} | {1}:{2}:{3}:{4} {5} {6}", now.Date, now.Hour, now.Minute, now.Second, now.Millisecond, direction, outputText);
            }
        }

        public  static void FlushDebug()
        {
            // Toggle debug level to flush
            SCPDLL.scpDebugSet((int)enSCPDebugLevel.enSCPDebugOff);
            Thread.Sleep(1000);
            SCPDLL.scpDebugSet((int)enSCPDebugLevel.enSCPDebugToFile);
        }
    }
}
