using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weixin.Web
{
    public partial class Jumppage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("https://taoquan.taobao.com/coupon/unify_apply.htm?sellerId=817719264&activityId=06a1d6ddc2864c8baf42643fad073f05");
        }
    }
}