using OutWeb.Authorize;
using OutWeb.Enums;
using OutWeb.Repositories;
using System.Web.Mvc;


namespace WebUser.Controller
{
    #region 基底控制器
    [ErrorHandler]
    public abstract class WebUserController : System.Web.Mvc.Controller
    {
        protected WebUserController()
        {
            ViewBag.NowHeadMenu = "";
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            ViewBag.IsFirstPage = false; //是否為首頁，請在首頁的Action此值設為True
            PublicMethodRepository.CurrentMode = SiteMode.Back;
            PublicMethodRepository.ListPageSize = (int)PageSizeConfig.SIZE10;

        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ((Request.Browser.Type == "IE" || Request.Browser.Type == "MSIE") && Request.Browser.MajorVersion < 10)
            {
                Response.Redirect("~/Content/noIE/noIE.html");
            }
        }

    }
    #endregion
}