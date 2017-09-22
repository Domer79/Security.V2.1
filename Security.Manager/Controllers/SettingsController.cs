using System.Web.Mvc;
using Security.Model;
using System.Net;

namespace Security.Manager.Controllers
{
    public class SettingsController : BaseSecurityController
    {        
        public ActionResult GetSystemSettings()
        {
            var settings = CoreSecurity.Settings.GetSystemSettings();
            return JsonByNewtonsoft(settings, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSetting(Setting setting)
        {
            CoreSecurity.Settings.SetValue<string>(setting.Name, setting.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}