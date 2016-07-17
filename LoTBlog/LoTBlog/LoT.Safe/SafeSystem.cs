using LoT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Safe
{
    /// <summary>
    /// 以后根据渗透特征进行蜜罐诈骗，现在先用来做敏感信息记录
    /// </summary>
    public static partial class SafeSystem
    {
        /// <summary>
        /// 记录访客IP
        /// </summary>
        public static void RecordVisitorIP()
        {

        }

        /// <summary>
        /// 记录安全攻击
        /// </summary>
        /// <param name="strInput">输入</param>
        /// <returns>是否安全</returns>
        public static bool RecordSafeAttack(string strInput)
        {
            bool systemSafe = false;

            SafeAttackEnum attackType = SafeAttackEnum.Other;

            //先根据输入判断安全等级
            HarmLevelEnum levelEnum = HtmlSafeHelper.GetHarmLevelAndSafeAttackType(strInput, out attackType);



            //记录攻击类型和攻击者的信息



            if (levelEnum == HarmLevelEnum.Normal)
            {
                systemSafe = true;
            }
            return systemSafe;
        }
    }
}
