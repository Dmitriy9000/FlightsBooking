using System;
using System.Web.Mvc;
using NLog;
using PhaseTicket.DeviceCommanderRef;

namespace PhaseTicket.Controllers
{
    public class DevicesController : FeaturedController
    {
        private CommandServiceClient GetClient()
        {
            return new CommandServiceClient();
        }

        #region NV11

        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        public ActionResult EnableMoneyReceiving()
        {
            try
            {
                using (var client = new CommandServiceClient())
                {
#if DEBUG
                    return OK();
#endif
                    client.EnableMoneyReceiving();
                    return OK();
                }
            }
            catch (Exception ex)
            {
                _log.FatalException("Error: {0}", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult DisableMoneyReceiving()
        {
            try
            {
                using (var client = new CommandServiceClient())
                {
#if DEBUG
                    return OK();
#endif
                    client.DisableMoneyReceiving();
                    return OK();
                }
            }
            catch (Exception ex)
            {
                _log.FatalException("Error: {0}", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult RecyclerStoredCount()
        {
            try
            {
                using (var client = new CommandServiceClient())
                {
#if DEBUG
                    return OK("0");
#endif
                    var count = client.RecyclerStoredCount();
                    return OK(count.ToString());
                }
            }
            catch (Exception ex)
            {
                _log.FatalException("Error: {0}", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult GetMoneyReceived()
        {
            try
            {
                using (var client = new CommandServiceClient())
                {
#if DEBUG
                    return OK("0");
#endif
                    var count = client.GetSessionMoneyReceived();
                    return OK(count.ToString());
                }
            }
            catch (Exception ex)
            {
                _log.FatalException("Error: {0}", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult ReturnPushedMoney()
        {
            try
            {
                using (var client = new CommandServiceClient())
                {
#if DEBUG
                    return OK();
#endif
                    client.ReturnSessionMoney();
                    return OK();
                }
            }
            catch (Exception ex)
            {
                _log.FatalException("Error: {0}", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult DropNotesCount()
        {
            try
            {
                using (var client = new CommandServiceClient())
                {
#if DEBUG
                    return OK();
#endif
                    client.FlushPaymentSession();
                    return OK();
                }
            }
            catch (Exception ex)
            {
                _log.FatalException("Error: {0}", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult ClearRecycler()
        {
            try
            {
                using (var client = new CommandServiceClient())
                {
#if DEBUG
                    return OK();
#endif
                    client.ClearRecycler();
                    return OK();
                }
            }
            catch (Exception ex)
            {
                _log.FatalException("Error: {0}", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult Change(int count)
        {
            try
            {
                using (var client = new CommandServiceClient())
                {
#if DEBUG
                    return OK();
#endif
                    client.Change(count);
                    return OK();
                }
            }
            catch (Exception ex)
            {
                _log.FatalException("Error: {0}", ex);
                return Error(ex.Message);
            }
        }

        #endregion

        #region Printer

        public ActionResult PrintUrl(string url)
        {
            try
            {
                using (var client = new CommandServiceClient())
                {
#if DEBUG
                    return OK();
#endif
                    client.PrintUrl(url);
                    _log.Info("Url printed: {0}", url);
                    return OK();
                }
            }
            catch (Exception ex)
            {
                _log.FatalException("Error: {0}", ex);
                return Error(ex.Message);
            }
        }

        #endregion

        #region Custom VKP-80II

        public ActionResult PrintCheck(decimal amount, decimal comission)
        {
            try
            {
                using (var client = new CommandServiceClient())
                {
#if DEBUG
                    return OK();
#endif
                    client.PrintCheck(amount, comission);
                    _log.Info("Check printed. Amount: {0}, Comission: {1}", amount, comission);
                    return OK();
                }
            }
            catch (Exception ex)
            {
                _log.FatalException("Error: {0}", ex);
                return Error(ex.Message);
            }
        }

        #endregion
    }
}
