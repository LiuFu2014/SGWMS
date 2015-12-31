using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime;
using System.IO;
using System.Windows.Forms;
using System.Resources;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;


namespace FileFun
{
    /// <summary> 
    /// 参数传递方式枚举 ,ByValue 表示值传递 ,ByRef 表示址传递 
    /// </summary> 
    public enum ModePass
    {

        ByValue = 0x0001,

        ByRef = 0x0002

    }

    /// <summary>
    /// 对INI文件进行操作的类
    /// </summary>
    public class MyIniFile
    {
        private bool _IsChanged = false;
        public string FileName; //INI文件名
        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        //类的构造函数，传递INI文件名
        public MyIniFile(string AFileName)
        {
            _IsChanged = false;
            // 判断文件是否存在
            FileInfo fileInfo = new FileInfo(AFileName);
            //Todo:搞清枚举的用法
            if ((!fileInfo.Exists))
            { //|| (FileAttributes.Directory in fileInfo.Attributes))
                //文件不存在，建立文件
                System.IO.StreamWriter sw = new System.IO.StreamWriter(AFileName, false, System.Text.Encoding.Default);
                try
                {
                    sw.Write("");
                    sw.Close();
                }

                catch
                {
                    throw (new ApplicationException("Ini文件不存在"));
                }
            }
            //必须是完全路径，不能是相对路径
            FileName = fileInfo.FullName;
        }
        //写INI文件
        public void StrWrite(string Section, string Ident, string Value)
        {
            _IsChanged = true;
            if (!WritePrivateProfileString(Section, Ident, Value, FileName))
            {

                throw (new ApplicationException("写Ini文件出错"));
            }
        }
        //读取INI文件指定
        public string StrRead(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }

        //读整数
        public int IntRead(string Section, string Ident, int Default)
        {
            string intStr = StrRead(Section, Ident, Convert.ToString(Default));
            try
            {
                return Convert.ToInt32(intStr);

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show("无法将 \"" + intStr + "\"" + " 转化成 Integer 类型  ; " + ex.Message);
                return Default;
            }
        }

        //写整数
        public void IntWrite(string Section, string Ident, int Value)
        {
            _IsChanged = true;
            StrWrite(Section, Ident, Value.ToString());
        }

        //读布尔
        public bool BoolRead(string Section, string Ident, bool Default)
        {
            try
            {
                return Convert.ToBoolean(StrRead(Section, Ident, Convert.ToString(Default)));
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
                return Default;
            }
        }

        //写Bool
        public void BoolWrite(string Section, string Ident, bool Value)
        {
            _IsChanged = true;
            StrWrite(Section, Ident, Convert.ToString(Value));
        }

        //从Ini文件中，将指定的Section名称中的所有Ident添加到列表中
        public void SectionRead(string Section, StringCollection Idents)
        {
            Byte[] Buffer = new Byte[16384];
            //Idents.Clear();

            int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0),
             FileName);
            //对Section进行解析
            GetStringsFromBuffer(Buffer, bufLen, Idents);
        }

        public void GetStringsFromBuffer(Byte[] Buffer, int bufLen, StringCollection Strings)
        {
            Strings.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        Strings.Add(s);
                        start = i + 1;
                    }
                }
            }
        }
        

        /// <summary>
        /// 从Ini文件中，读取所有的Sections的名称
        /// </summary>
        /// <param name="SectionList"></param>
        public void SectionsRead(StringCollection SectionList)
        {
            //Note:必须得用Bytes来实现，StringBuilder只能取到第一个Section
            byte[] Buffer = new byte[65535];
            int bufLen = 0;
            bufLen = GetPrivateProfileString(null, null, null, Buffer,
             Buffer.GetUpperBound(0), FileName);
            GetStringsFromBuffer(Buffer, bufLen, SectionList);
        }
        
        /// <summary>
        /// 读取指定的Section的所有Value到列表中
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Values"></param>
        public void SectionValuesRead(string Section, NameValueCollection Values)
        {
            StringCollection KeyList = new StringCollection();
            SectionRead(Section, KeyList);
            Values.Clear();
            foreach (string key in KeyList)
            {
                Values.Add(key, StrRead(Section, key, ""));

            }
        }
        /**/
        ////读取指定的Section的所有Value到列表中，
        //public void ReadSectionValues(string Section, NameValueCollection Values,char splitString)
        //{　 string sectionValue;
        //　　string[] sectionValueSplit;
        //　　StringCollection KeyList = new StringCollection();
        //　　ReadSection(Section, KeyList);
        //　　Values.Clear();
        //　　foreach (string key in KeyList)
        //　　{
        //　　　　sectionValue=ReadString(Section, key, "");
        //　　　　sectionValueSplit=sectionValue.Split(splitString);
        //　　　　Values.Add(key, sectionValueSplit[0].ToString(),sectionValueSplit[1].ToString());

        //　　}
        //}
        
        /// <summary>
        /// 清除某个Section
        /// </summary>
        /// <param name="Section"></param>
        public void EraseSection(string Section)
        {
            //
            if (!WritePrivateProfileString(Section, null, null, FileName))
            {

                throw (new ApplicationException("无法清除Ini文件中的Section"));
            }
        }
        //删除某个Section下的键
        public void DeleteKey(string Section, string Ident)
        {
            WritePrivateProfileString(Section, Ident, null, FileName);
        }
        //Note:对于Win9X，来说需要实现UpdateFile方法将缓冲中的数据写入文件
        //在Win NT, 2000和XP上，都是直接写文件，没有缓冲，所以，无须实现UpdateFile
        //执行完对Ini文件的修改之后，应该调用本方法更新缓冲区。
        public void UpdateFile()
        {
            WritePrivateProfileString(null, null, null, FileName);
        }

        //检查某个Section下的某个键值是否存在
        public bool ValueExists(string Section, string Ident)
        {
            //
            StringCollection Idents = new StringCollection();
            SectionRead(Section, Idents);
            return Idents.IndexOf(Ident) > -1;
        }

        //确保资源的释放
        ~MyIniFile()
        {
            UpdateFile();
        }
    }
    ///// <summary>
    ///// 操作注册表的类
    ///// </summary>
    //public class MyRegister
    //{
    //    public RegistryKey RootKey ;
        
    //    public MyRegister ()
    //    {
    //        RootKey = Registry.CurrentUser;
    //    }
    //    public void RegWrite(string KeyPath,string Key,object Value)
    //    {
    //        RegistryKey OptKey;
    //        try
    //        {
    //            OptKey = RootKey.CreateSubKey(KeyPath);
    //            OptKey.SetValue(Key, Value);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //        }
 
    //    }
    //    public object RegRead(string KeyPath, string Key, object Default)
    //    {
    //        RegistryKey OptKey;
    //        try
    //        {
    //            OptKey = RootKey.CreateSubKey(KeyPath);
    //            return (OptKey.GetValue(Key, Default));
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //        }

    //    }
    //}
    public class MyCallUnSafetyDll
    {

        ///<summary>
        /// API LoadLibrary
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int LoadLibrary(String filename);
     
        ///<summary>
        /// API GetProcAddress
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int GetProcAddress(int handle, String funcname);
     
        ///<summary>
        /// API FreeLibrary
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int FreeLibrary(int handle);
     
        ///<summary>
        ///通过非托管函数名转换为对应的委托, by ZQM
        ///</summary>
        ///<param name="dllModule">通过LoadLibrary获得的DLL句柄</param>
        ///<param name="functionName">非托管函数名</param>
        ///<param name="t">对应的委托类型</param>
        ///<returns>委托实例，可强制转换为适当的委托类型</returns>
        public static Delegate GetFunctionAddress(int dllModule, string functionName, Type t)
        {
           int address = GetProcAddress(dllModule, functionName);
           if (address == 0)
               return null;
           else
               return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
        }
     
        ///<summary>
        ///将表示函数地址的IntPtr实例转换成对应的委托, by Zqm
        ///</summary>
        public static Delegate GetDelegateFromIntPtr(IntPtr address, Type t)
        {
           if (address == IntPtr.Zero)
               return null;
           else
               return Marshal.GetDelegateForFunctionPointer(address, t);
        }
     
        ///<summary>
        ///将表示函数地址的int转换成对应的委托，by ZQM
        ///</summary>
        public static Delegate GetDelegateFromIntPtr(int address, Type t)
        {
           if (address == 0)
               return null;
           else
               return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
        }

        
        /// <summary>
        /// 设定调用的函数
        /// </summary>
        /// <param name="objArr_Param">参数对象数组</param>
        /// <param name="TypeArray_ParameterType">参数对象的类型数组</param>
        /// <param name="ModePassArray_Parameter">参数数组的传递方式数组</param>
        /// <param name="Type_Return">函数返回的类型</param>
        /// <returns></returns>
        public static object Invoke(IntPtr hModule , IntPtr hFun,string funName, object[] objArr_Param, Type[] TypeArr_ParamType, ModePass[] ModePassArr_Param, Type Type_Return)
        {
            // 下面 3 个 if 是进行安全检查 , 若不能通过 , 则抛出异常 
            IntPtr hFun0 = hFun ;
            if (hModule == IntPtr.Zero)
                throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadLibrary 操作 !"));
            if (hFun0 == IntPtr.Zero)
            {
                hFun0 =(IntPtr)  GetProcAddress((int)hModule, funName);
                throw (new Exception(" 函数指针为空 , 请确保已进行 GetProcess 操作 !"));
            }
            if (objArr_Param.Length != ModePassArr_Param.Length)
                throw (new Exception(" 参数个数及其传递方式的个数不匹配 ."));

            // 下面是创建 MyAssemblyName 对象并设置其 Name 属性 
            AssemblyName MyAssemblyName = new AssemblyName();
            MyAssemblyName.Name = "InvokeFun";
            // 生成单模块配件 
            AssemblyBuilder MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(MyAssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InvokeDll");
            // 定义要调用的方法 , 方法名为“ MyFun ”，返回类型是“ Type_Return ”参数类型是“ TypeArray_ParameterType ” 
            MethodBuilder MyMethodBuilder = MyModuleBuilder.DefineGlobalMethod(funName, MethodAttributes.Public | MethodAttributes.Static, Type_Return, TypeArr_ParamType);
            // 获取一个 ILGenerator ，用于发送所需的 IL 
            ILGenerator IL = MyMethodBuilder.GetILGenerator();
            int i;
            for (i = 0; i < objArr_Param.Length; i++)
            {// 用循环将参数依次压入堆栈 
                switch (ModePassArr_Param[i])
                {
                    case ModePass.ByValue:
                        IL.Emit(OpCodes.Ldarg, i);
                        break;
                    case ModePass.ByRef:
                        IL.Emit(OpCodes.Ldarga, i);
                        break;
                    default:
                        throw (new Exception(" 第 " + (i + 1).ToString() + " 个参数没有给定正确的传递方式 ."));
                }
            }
            if (IntPtr.Size == 4)
            {// 判断处理器类型 
                IL.Emit(OpCodes.Ldc_I4, hFun0.ToInt32());
            }
            else if (IntPtr.Size == 8)
            {
                IL.Emit(OpCodes.Ldc_I8, hFun0.ToInt64());
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
            IL.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, Type_Return, TypeArr_ParamType);
            IL.Emit(OpCodes.Ret); // 返回值 
            MyModuleBuilder.CreateGlobalFunctions();
            // 取得方法信息 
            MethodInfo MyMethodInfo = MyModuleBuilder.GetMethod(funName);
            return MyMethodInfo.Invoke(null, objArr_Param);// 调用方法，并返回其值 
        } 


        public static object DoCallMyDll(string dllFile, string funName, object[] param,Type[] Typesparam,ModePass[] ArrModePass,Type Type_Return,out bool bIsOK)
        {
            IntPtr hModule = IntPtr.Zero;
            IntPtr hFunc = IntPtr.Zero;
            bIsOK = false;
            hModule = new IntPtr (LoadLibrary(dllFile));

            if (hModule == IntPtr.Zero)

                throw (new Exception(" 函数库模块的句柄为空!"));            
            hFunc = new IntPtr(GetProcAddress(hModule.ToInt32(), funName));

            if (hFunc == IntPtr.Zero)

                throw (new Exception(" 函数指针 hFunc 为空 !"));

            object objReturn = null;
            try
            {
                objReturn = Invoke(hModule, hFunc, funName, param, Typesparam, ArrModePass, Type_Return);
                bIsOK = true;
            }
            catch (Exception err)
            {
                bIsOK = false;
            }
            return objReturn;

        }

    }
    public class MyCallSafetyDll
    {
        public static object DoCallMyDll(string dllFile, string classname, string FunName, object[] param,out bool bIsOK)
        {
            object objResult = null;
            bool  bOK = false;
            Assembly asm = Assembly.LoadFrom(dllFile);//动态加载dll的路径,这里需要物理路径，默认从应用程序根目录开始

            //加载dll后,需要使用dll中某类.
            Type t = asm.GetType(classname,true,true);//classname:类名字

            //实例化类型
            object o = Activator.CreateInstance(t);

            //得到要调用的某类型的方法
            MethodInfo method = t.GetMethod(FunName);//FunName:方法名字

            //对方法进行调用
            try
            {
                objResult = method.Invoke(o, param);//param为方法参数object数组
                bOK = true;
            }
            catch(Exception e)
            {
                bOK = false;
            }
            bIsOK = bOK;
            return (objResult);
        }

    }
    public class MyExeFile
    {
        public static void CallExe(string sFile,System.Diagnostics.ProcessWindowStyle winStyle ,  string[] args)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = System.IO.Path.GetFileName(sFile) ;
            startInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(sFile);

            //参数用空格分开 
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (i == 0) startInfo.Arguments = args[i];
                    else startInfo.Arguments = startInfo.Arguments + " " + args[i];
                }
                startInfo.UseShellExecute = false;
            }
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = winStyle;
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo = startInfo;
                p.Start();
                //p.WaitForExit();
                //System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception err)
            {
                string sErr = err.Message;
            }
        }
        public static bool CheckExeIsRunning(string ExeName)
        {
            int ProceedingCount = 0;
            bool IsExists = false;
            System.Diagnostics.Process[] Processes;
            Processes = System.Diagnostics.Process.GetProcessesByName(ExeName);

            foreach (System.Diagnostics.Process IsProcedding in Processes)
            {
                if (IsProcedding.ProcessName.ToLower() == ExeName.ToLower())
                {
                    ProceedingCount += 1;
                }
            }
            /*
            if (ProceedingCount > 1)
            {
                DialogResult result;
                //result = MessageBox.Show("服务已经打开", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                foreach (System.Diagnostics.Process myProcess in Processes)
                {                   
                    myProcess.Kill();
                }

            }
             * */
            IsExists = ProceedingCount > 1;
            return (IsExists);
        }
    }

    public class MyLogFile
    {
        public static MyLogWriter myWriter = null;
        public static void SaveLogFile(string sFile, string sText,int nDoSaveTimer)
        {            
            string slogFile = sFile.Trim();
            if (slogFile.Trim() == "")
            {
                slogFile = "Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            }
            if (myWriter == null)
            {
                myWriter = new MyLogWriter();
                int nTime = nDoSaveTimer;
                if (nTime == 0)
                {
                    nTime = 2;
                }
                myWriter.FileName = slogFile;
                myWriter.TimeWriter = nTime;
                myWriter.StrArr.Add(sText);
                myWriter.SetDoWriter();
            }
            else
            {
                myWriter.StrArr.Add(sText);
            }
        }

        public static void DoDisposeLogFile()
        {
            if (myWriter != null)
            {
                myWriter.DoDisposeLogWriter();
            }
            myWriter = null;
        }
    }
    public class MyLogWriter
    {
        public MyLogWriter()
        {
            if (strArr == null)
            {
                strArr = new StringCollection();
            }
        }

        ~MyLogWriter()
        {
            DoDisposeLogWriter();

        }

        #region 私有变量
            private System.Threading.Timer tmrSave = null;
            private bool bIsSaving = false;
        #endregion

        #region 属性
        private StringCollection strArr = null;
            public StringCollection StrArr
            {
                get { return strArr; }
                set { strArr = value; }
            }

            private int timeWriter = 2;
            public int TimeWriter
            {
                get { return timeWriter; }
                set { timeWriter = value; }
            }

            private string fileName = "";
            public string FileName
            {
                get { return fileName.Trim(); }
                set { fileName = value.Trim(); }
            }
        #endregion

        #region 私有方法
        private void WriteLogToFile(object state)
        {
            if (bIsSaving) return;
            if (strArr == null) return;
            if (strArr.Count == 0) return;
            bIsSaving = true;
            string slogFile = fileName.Trim();
            if (slogFile.Trim() == "")
            {
                slogFile = "Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            }
            FileStream fs = null;
            #region
            if (!File.Exists(slogFile))
            {
                try
                {
                    fs = File.Create(slogFile);
                }
                catch (Exception err)
                {                                     
                    MessageBox.Show(err.Message);
                    bIsSaving = false;
                    return;
                }
            }
            else
            {
                try
                {
                    fs = File.Open(slogFile, FileMode.Append);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    bIsSaving = false;
                    return;
                }
            }
            #endregion
            if (fs == null) return;
            #region
            try
            {
                foreach (string sText in strArr)
                {
                    string sData = sText;
                    sData += "\r\n";
                    byte[] bText = null;
                    bText = System.Text.UTF8Encoding.Unicode.GetBytes(sData);
                    if (bText.Length > 0)
                    {
                        fs.Write(bText, 0, bText.Length);
                    }                    
                    if (bText != null)
                    {
                        Array.Clear(bText, 0, bText.Length);
                    }
                    bText = null;
                }
                fs.Flush();
                fs.Close();
                fs.Dispose();
                fs = null;
            }
            catch (Exception err)
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
                MessageBox.Show(err.Message);
                bIsSaving = false;
                return;
            }
            #endregion
        }
        #endregion

        #region 公有方法

        public void SetDoWriter()
        {
            if (tmrSave != null)
            {
                tmrSave.Dispose();
                tmrSave = null;
            }
            if (tmrSave == null)
            {
                int nTime = timeWriter;
                if (nTime == 0)
                {
                    nTime = 2;
                }
                tmrSave = new System.Threading.Timer(new System.Threading.TimerCallback(WriteLogToFile), null, 2 * 1000, nTime * 1000);
            }
        }

        public void DoDisposeLogWriter()
        {
            #region
            if (tmrSave != null)
            {
                tmrSave.Dispose();
                tmrSave = null;
            }
            #endregion

            #region
            if (strArr != null)
            {
                strArr.Clear();
                strArr = null;
            }
            #endregion
        }

        public static  void SaveLogToFile(string sFile ,string sText)
        {
            string slogFile = sFile.Trim();
            if (slogFile.Trim() == "")
            {
                slogFile = "Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            }
            FileStream fs = null;
            #region
            if (!File.Exists(slogFile))
            {
                try
                {
                    fs = File.Create(slogFile);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                    return;
                }
            }
            else
            {
                try
                {
                    fs = File.Open(slogFile, FileMode.Append);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                    return;
                }
            }
            #endregion
            if (fs == null) return;
            #region
            try
            {
                string sData = sText;
                sData += "\r\n";
                byte[] bText = null;
                bText = System.Text.UTF8Encoding.Unicode.GetBytes(sData);
                if (bText.Length > 0)
                {
                    fs.Write(bText, 0, bText.Length);
                }
                fs.Flush();
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
                if (bText != null)
                {
                    Array.Clear(bText, 0, bText.Length);
                }
                bText = null;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
                return;
            }
            #endregion
        }
        #endregion
    }
}
