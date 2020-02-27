using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FormApp1.App
{
    public class Logger
    {
        internal readonly string _filepath;
        internal readonly Encoding _enc;
        
        public Logger(string logfilePath, Encoding enc)
        {
            string dt = DateTime.Now.ToString("yyyyMMdd");
            _filepath = logfilePath + @"\log_" + dt + ".log";
            _enc = enc;
        }
        
        public void Error(string text) 
        {
            StackTrace st = new StackTrace(1, true);
            string dt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            string fileName = Path.GetFileName(st.GetFrame(0).GetFileName());
            string methodName = st.GetFrame(0).GetMethod().ToString();
            StringBuilder context = new StringBuilder();

            context.AppendFormat("{0} ", dt);
            context.AppendFormat("[Error] ");
            context.AppendFormat("{0} ", fileName);
            context.AppendFormat("Method:{0} ", methodName);
            context.AppendFormat("{0}", text);
            try
            {
                using(StreamWriter sw = new StreamWriter(_filepath, true, _enc))
                {
                    sw.WriteLine(context.ToString());
                }
            }
            catch (Exception ex)
            {                
                Console.WriteLine(context.ToString() + " " + ex.Message);
            }

        }
        public void Info(string text)
        {
            StackTrace st = new StackTrace(1, true);
            string dt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            string fileName = Path.GetFileName(st.GetFrame(0).GetFileName());
            string methodName = st.GetFrame(0).GetMethod().ToString();
            StringBuilder context = new StringBuilder();

            context.AppendFormat("{0} ", dt);
            context.AppendFormat("[Info ] ");
            context.AppendFormat("{0} ", fileName);
            context.AppendFormat("Method:{0} ", methodName);
            context.AppendFormat("{0}", text);
            try
            {
                using (StreamWriter sw = new StreamWriter(_filepath, true, _enc))
                {
                    sw.WriteLine(context.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(context.ToString() + " " + ex.Message);
            }
        }
        public void Debug(string text)
        {
            StackTrace st = new StackTrace(1, true);
            string dt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            string fileName = Path.GetFileName(st.GetFrame(0).GetFileName());
            string methodName = st.GetFrame(0).GetMethod().ToString();
            StringBuilder context = new StringBuilder();

            context.AppendFormat("{0} ", dt);
            context.AppendFormat("[Debug] ");
            context.AppendFormat("{0} ", fileName);
            context.AppendFormat("Method:{0} ", methodName);
            context.AppendFormat("{0}", text);
            try
            {
                using (StreamWriter sw = new StreamWriter(_filepath, true, _enc))
                {
                    sw.WriteLine(context.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(context.ToString() + " " + ex.Message);
            }
        }

    }
}
