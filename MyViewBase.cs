using System.Web.Mvc;

namespace PhaseTicket
{
    public class MyViewBase<T> : WebViewPage<T>
    {
       
        public MyViewBase()
        {

        }

        public bool IsDebug
        {
            get
            {
                bool isDebugMode = false;
#if DEBUG
                isDebugMode = true;
#endif
                return isDebugMode;

            }

        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}