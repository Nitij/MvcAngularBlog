using System.Web;
using System.Web.Optimization;

namespace MvcAngularBlog
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angularJS").Include(
                        "~/Scripts/angular.js"
                        , "~/Scripts/angular-route.js"
                        , "~/Scripts/angular-sanitize.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularScripts").Include(
                "~/Angular_Scripts/BlogApp.js"
                , "~/Angular_Scripts/Routes.js"
                , "~/Angular_Scripts/Services/ArchiveService.js"
                , "~/Angular_Scripts/Services/BlogArticleService.js"
                , "~/Angular_Scripts/Services/BlogCommentsService.js"
                , "~/Angular_Scripts/Services/BlogUserService.js"
                , "~/Angular_Scripts/Services/HelperService.js"
                , "~/Angular_Scripts/Services/TagService.js"
                , "~/Angular_Scripts/Directives/ArticleDirectives.js"
                , "~/Angular_Scripts/Controllers/ArticleController.js"
                , "~/Angular_Scripts/Controllers/UserController.js"
                , "~/Angular_Scripts/Controllers/TagsController.js"
                , "~/Angular_Scripts/Controllers/ArticleTagController.js"
                , "~/Angular_Scripts/Controllers/ArchiveController.js"
                , "~/Angular_Scripts/Controllers/ArticleArchiveController.js"
                , "~/Angular_Scripts/Controllers/BlogController.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //lightweight text editor
            bundles.Add(new ScriptBundle("~/bundles/niceEdit").Include(
                        "~/Scripts/nicEdit.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
        }
    }
}