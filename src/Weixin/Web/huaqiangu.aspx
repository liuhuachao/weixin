<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="huaqiangu.aspx.cs" Inherits="Weixin.Web.huaqiangu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>尊上和小骨来到现代，他们是这样的</title>
    <meta name="viewport" charset="utf-8" content="initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no" />
    <link href="http://wxstatic.luxlead.com/Css/style.css" rel="stylesheet" />
    <script src="http://wxstatic.luxlead.com/JS/zepto.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jquery.min.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jweixin-1.0.0.js"></script>
 </head>

<body style="" >
    <header>
        <img id="bg" src="http://wxstatic.luxlead.com/Images/s10.jpg" />
        <p id="user-name" class="data-name">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </p>
        <img id="avt" runat="server" class="data-avt" src="http://wxstatic.luxlead.com/Images/0" />
    </header>

    <div id="main">
        <div id="list">
        <ul>
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt data-avt" src="http://wxstatic.luxlead.com/Images/a0.jpg" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                        <p class="po-name">东方彧卿</p>
                        <p class="post">感觉自己萌萌哒，@小骨头</p>
                        <p class="time">刚刚</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />东方彧卿，想变蝴蝶的糖宝，洛姐...</div>
                        <div class="cmt-list">
                        <p><span>一只孤独的上仙：</span>能把你头像换了吗~</p>
                        <p><span>东方彧卿：</span>呵呵。。。</p>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/s8.jpg" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                        <p class="po-name">一只孤独的上仙</p>
                        <p class="post">墨冰，也是我。终于说出了真想<img src="http://wxstatic.luxlead.com/Images/s11.jpg"  /></p>
                        <p class="time">29分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png"  />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png"  />小骨头，夏紫薰，想变蝴蝶的糖宝，六界第一美人，洛姐...</div>
                        <div class="cmt-list">
                        <p><span>小骨头：</span>师傅你还不如不说，不过我原谅你~</p>
                        <p><span>夏紫薰：</span>好一个师徒情深。</p>
                        <p><span class="data-name">洛姐</span><span>：</span>好一个师徒情深。</p>
                        <p><span>一只孤独的上仙</span>回复<span class="data-name">洛姐</span><span>：</span>你早应该猜到了吧！</p>
                        </div>
                    </div>
                </div>
            </li>
            
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/s3.jpg" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">东方彧卿</p>
                      <div class="post">
                        <p><b class="data-name">今天和小骨在一起真开心，你要照顾好自己哦！</b></p>
                        <img class="list-img" src="http://wxstatic.luxlead.com/Images/s1.jpg" /><img class="data-avt list-img" src="http://wxstatic.luxlead.com/Images/s2.jpg" /></div>
                        <p class="time">45分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png">
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />小骨头，六界第一美人，洛姐</div>
                        <div class="cmt-list">
                        <p><span class="data-name">一只孤独的上仙</span><span>：</span>能把你的头像换了吗！</p>
                        <p><span>六界第一美人</span>：其实你们在一起也挺好的！</p>
                        </div>
                    </div>
                </div>
            </li>
              <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/a5.jpg">
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                        <p class="ads">推广<img src="http://wxstatic.luxlead.com/Images/ads.png"  /></p>
                        <p class="po-name">洛姐</p>
                      <div class="post">连小骨头都这么喜欢洛姐家的衣服，真是太高兴了！~
                            <p><a class="ad-link" href="http://wq.jd.com/mshop/gethomepage?venderid=11468">查看详情 <img src="http://wxstatic.luxlead.com/Images/link.png" /></a></p>
                            <img src="http://wxstatic.luxlead.com/Images/洛姐V5.png" /></div>
                        <p class="time">50分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />小骨头，想变蝴蝶的糖宝，夏紫薰，...</div>
                        <div class="cmt-list">
                        <p><span>六界第一美人：</span>洛家的衣服我穿上，是不是就更美了，呵呵！</p>
                        <p><span>洛姐</span>回复<span>六界第一美人 ：</span>姐姐是最漂亮的/偷笑</p>
                        </div>
                    </div>
                </div>
            </li>          
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/s9.jpg">
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">小骨头</p>
                        <div class="post">
                          <p><b class="data-name">尊上，这件衣服漂亮吗？嘿嘿，我在洛诗琳买的哦</b>！</p>
                          <p><img class="list-img" src="http://wxstatic.luxlead.com/Images/c2.jpg" /><img class="list-img" src="http://wxstatic.luxlead.com/Images/c0.jpg" /><img class="data-avt list-img" src="http://wxstatic.luxlead.com/Images/洛姐V5.png" /></p></div>
                          <p class="time">54分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" /><b class="data-name">洛姐</b></div>
                        <div class="cmt-list">
                        <p><span class="data-name">一只孤独的上仙</span><span>：</span>好美！</p>
                        <p><span>六界第一美人</span><span>：</span>骨头，穿上洛诗琳的衣服，比姐姐更美了！</p>
                      </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/s4.jpg">
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">单春秋</p>
                        <p class="post">大家不要误会，我们只是好朋友！@六界第一美人 ：你出来解释一下<img class="data-avt" src="http://wxstatic.luxlead.com/Images/s5.jpg" /></p>
                        <p class="time">57分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" /><b>小骨头</b>，东方彧卿，朗哥哥，洛姐...</div>
                        <div class="cmt-list">
                        <p><span>六界第一美人：</span>我能解释吗？</p>
                        <p><span>单春秋</span >回复<span class="data-name">六界第一美人</span><span>：</span>算了，反正我们只是朋友，对只是朋友！</p>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/s8.jpg" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">一只孤独的上仙</p>
                        <p class="post">小骨辰时到藏书阁来一趟。<img src="http://wxstatic.luxlead.com/Images/s6.jpg" /></p>
                        <p class="time">1个小时前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                      <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" /><span class="data-name">幽若姑娘</span>，夏紫薰，想变蝴蝶的糖宝，轻水</div>
                        <div class="cmt-list">
                        <p><span>小骨头：</span>叔叔，我们不约</p>
                       <p><span>六界第一美人</span>回复<span class="data-name">小骨头</span><span>：</span>好孩子！！</p>
                         <p><span>东方彧卿</span>回复<span class="data-name">小骨头</span><span>：</span>好孩子！！</p>
                        <p><span>儒尊</span><span>：</span>白子画，卒。</p>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/s8.jpg" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">一只孤独的上仙</p>
                        <p class="post">爆个照，我帅吗？<img src="http://wxstatic.luxlead.com/Images/s7.jpg" /></p>
                        <p class="time">55分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="cmt-list">
                        <p><span class="data-name">小骨头</span><span>：</span>尊上是最帅的！</p>
                        <p><span>一只孤独的上仙</span>回复<span class="data-name">小骨头</span><span>：</span>么么哒！</p>
                        </div>
                    </div>
                </div>
            </li>
            <li>
            <div class="po-avt-wrap">
              <img class="po-avt" src="http://wxstatic.luxlead.com/Images/洛姐V5头像.jpg" />
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="po-name">洛姐</p>
                <p class="post"> 关注洛诗琳更多资讯，与设计师零距离互动。还有更多礼品及优惠券领取哟~长按识别下方二维码关注洛诗琳设计师微信↓<img src="http://wxstatic.luxlead.com/Images/洛姐V5.png" /></p>
              </div>
              <div class="r"></div>
            </div>
          </li>
        </ul>    
    </div>
        <div id="share">
            <a>发送给朋友</a><a>分享到朋友圈</a>
            <p>（此朋友圈纯属虚构）</p>
        </div>
        <div id="guide" class="hide"></div>
    </div>
    <script>
        function fx() {
            $("#share a").click(function () {
                $("#guide").show();
            });
            $("#guide").click(function () {
                $(this).hide();
            });
        }
        $(document).ready(
            fx()
           )
    </script>
    </body></html>  