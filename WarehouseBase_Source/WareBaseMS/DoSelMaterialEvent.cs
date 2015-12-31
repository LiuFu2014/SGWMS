namespace WareBaseMS
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void DoSelMaterialEvent(string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther, string sRemark, string sABC, double fSafeQtyDn, double fSafeQtyUp, double fQtyBox, double fWeight, string sTypeId1, string sType1, string sTypeId2, string sType2, string sUnit, int nKeepDay, string sCSId, string sSupplier, int _nMatClass, bool bIsSelectOK);
}

