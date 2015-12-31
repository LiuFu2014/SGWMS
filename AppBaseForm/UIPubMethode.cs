using System;
using System.Collections.Generic;
using System.Text;

namespace UI
{
    public static class UIPubMethode 
    {
        public static bool InputMessage(string sPromtText, string sTitle, string sDefault, InputMsgType inType, out string sValue)
        {
            bool bOK = false;
            sValue = "";
            frmInputMessage frmX = new frmInputMessage();
            frmX.InputValueType = inType;
            frmX.DefaultValue = sDefault;
            frmX.PromptText = sPromtText;
            frmX.TitleText = sTitle;
            frmX.ShowDialog();
            bOK = frmX.ResultIsOK;
            if (bOK)
            {
                sValue = frmX.ResultValue;
            }
            frmX.Dispose();
            return bOK;
        }
    }
}
