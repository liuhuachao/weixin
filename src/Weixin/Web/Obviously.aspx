<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Obviously.aspx.cs" Inherits="Weixin.Web.Obviously" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>林青霞也来给我点赞了</title>
    <meta name="viewport" content="initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no" />
    <link href="http://wxstatic.luxlead.com/Css/style.css" rel="stylesheet" />
    <script src="http://wxstatic.luxlead.com/JS/zepto.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jweixin-1.0.0.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jquery.min.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jweixin-1.0.0.js"></script>
</head>

<body style="" >
    <header>
        <img id="bg" src="http://wxstatic.luxlead.com/Images/TB2uJmcfVXXXXXCXpXXXXXXXXXX_!!25323709.jpg" />
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
                    <img class="po-avt data-avt" runat="server" src="http://wxstatic.luxlead.com/Images/TB2BXmkfVXXXXbUXXXXXXXXXXXX_!!25323709.png" />
              </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name"><label runat="server" id="lable1">某某</label></p>
                        <p class="post">关于前任，你想说的一句话是______________</p>
                        <p class="time">刚刚</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" width="3" height="12">林青霞，朱茵，汪涵，洛姐...</div>
                      <div class="cmt-list">
                        <p><span>谢娜：</span>哎……这么年轻就走了，天妒英才~ ~</p>
                        <p><span>宁静：</span>你爱他，他爱她，他瞎你也瞎</p>
                      </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2CVSqfVXXXXazXXXXXXXXXXXX_!!25323709.png" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">张含韵</p>
                        <p class="post">小时候不爱吃饭，导致现在个矮； 现在爱吃饭了，导致又胖又矮。。。@<label runat="server">某某</label><img runat="server" src="http://wxstatic.luxlead.com/Images/TB2BXmkfVXXXXbUXXXXXXXXXXXX_!!25323709.png" /></p>
                        <p class="time">9分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />何炅，谢娜，娜扎，欧阳娜娜，蔡少芬...</div>
                      <div class="cmt-list">
                        <p><span><label runat="server">某某</label>：</span>张小花，你几个意思啊，我只是瘦得不明显好吧！！</p>
                        <p><span>何炅：</span>目测说的不是我！</p>
                        <p><span class="data-name">汪涵</span>回复<span>何炅：</span>所以你一直不爱吃饭的意思？</p>
                        <p><span><label runat="server" id="Label2">某某</label></span>回复<span class="data-name">汪涵</span><span>：</span>瞎说什么实话！</p>
                      </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2Ai0.fVXXXXaQXpXXXXXXXXXX_!!25323709.jpg" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">蔡少芬</p>
                      <div class="post">
                        <p><b class="data-name">我老公很帅的！我也好美的！嘘，低调，低调……</b></p>
                      <img class="list-img" src="http://wxstatic.luxlead.com/Images/TB2KT9afVXXXXaCXpXXXXXXXXXX_!!25323709.jpg" /><img class="data-avt list-img" src="http://wxstatic.luxlead.com/Images/TB2rx80fVXXXXcdXpXXXXXXXXXX_!!25323709.jpg" /></div>
                        <p class="time">20分钟前</p>
                        <img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                  </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />朱茵，宁静，欧阳娜娜，赵丽颖…</div>
                      <div class="cmt-list">
                        <p><span><label runat="server" id="Label3">某某</label>：</span>要不要"喇么"夸张！炫夫狂魔！</p>
                        <p><span>蔡少芬</span>回复<span><label runat="server" id="Label4">某某</label></span>：臣妾做不到啊！</p>
                      </div>
                    </div>
                </div>
            </li>
                        
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2qhaqfVXXXXaJXXXXXXXXXXXX_!!25323709.png" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">林青霞</p>
                        <div class="post">
                          <p><b class="data-name">偏爱红装，听说洛诗琳也很多的哦</b>！</p>
                           <p><img class="list-img" src="http://wxstatic.luxlead.com/Images/TB2S.isfVXXXXaoXXXXXXXXXXXX_!!25323709.png" /><img class="list-img" src="http://wxstatic.luxlead.com/Images/TB2ZZS3fVXXXXXYXXXXXXXXXXXX_!!25323709.jpg" /><img class="data-avt list-img" src="http://wxstatic.luxlead.com/Images/%E6%B4%9B%E5%A7%9023.png" /></p>
                        </div>
                        <p class="time">40分钟前</p>
                        <img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" /><b class="data-name">洛姐</b>，杨钰莹…</div>
                      <div class="cmt-list">
                        <p><span class="data-name">杨钰莹</span><span>：</span>姐姐就像画中人，霎时间飘下来了！</p>
                        <p><span><label runat="server" id="Label7">某某</label>：</span>美得不要不要的！</p>
                      </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2_ieifVXXXXbVXXXXXXXXXXXX_!!25323709.png" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">赵丽颖</p>
                        <p class="post">分分钟感觉被复制粘贴了！<img class="data-avt" src="http://wxstatic.luxlead.com/Images/TB2fu9ufVXXXXauXXXXXXXXXXXX_!!25323709.jpg" /></p>
                        <p class="time">45分钟前</p>
                        <img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" /><b>何炅</b>，张含韵，谢娜，欧阳娜娜...</div>
                      <div class="cmt-list">
                        <p><span>张含韵：</span>哇哈哈哈！包子，这是你弟弟小笼包吧！</p>
                        <p><span><label runat="server" id="Label8">某某</label></span ><span>：</span>确定不是你小时候的照片！？？！</p>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="po-avt-wrap">
                  <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2RMx_fVXXXXbeXpXXXXXXXXXX_!!25323709.jpg" />
                </div>
                <div class="po-cmt">
                  <div class="po-hd">
                    <p class="po-name">何炅</p>
                      <p class="post">你叫啊，叫破喉咙也没人救你……@汪涵<img src="http://wxstatic.luxlead.com/Images/TB2owuwfVXXXXXKXXXXXXXXXXXX_!!25323709.png" /></p>
                        <p class="time">50分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                  </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                      <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" /><span class="data-name">谢娜</span>，汪涵，林青霞，赵丽颖，欧阳娜娜…</div>
                      <div class="cmt-list">
                        <p><span>汪涵：</span>破喉咙！破喉咙！破喉咙！重要的事情要说三遍！</p>
                       <p><span>何炅</span >回复<span>汪涵：</span>然并卵！！！</p>
                        <p><span class="data-name">谢娜</span><span>：</span>说好的做彼此的天使呢？</p>
                        <p><span><label runat="server" id="Label9">某某</label>：</span>你们城里人真会玩！可吓死宝宝了……</p>
                      </div>
                    </div>
              </div>
            </li>
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2s39pfVXXXXa8XXXXXXXXXXXX_!!25323709.jpg" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">谢娜</p>
                        <p class="post">听说穿上洛诗琳的衣服能变得跟青霞姐一样美哈！！<img src="http://wxstatic.luxlead.com/Images/TB2axl.fVXXXXaSXpXXXXXXXXXX_!!25323709.jpg" /></p>
                        <p class="time">55分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="cmt-list">
                        <p><span><label runat="server" id="Label10">某某</label>：</span>表示头像亮了！</p>
                        <p><span>谢娜</span>回复<span><label runat="server" id="Label11">某某</label>：</span>这不是重点好么！看大图，大图，图！</p>
                      </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="po-avt-wrap">
                    <img class="po-avt" src="http://wxstatic.luxlead.com/Images/a5.jpg" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                        <p class="ads">推广<img src="http://wxstatic.luxlead.com/Images/ads.png" /></p>
                        <p class="po-name">洛姐</p>
                      <div class="post">连青霞姐姐都这么喜欢洛家的衣服，真是太高兴了！加洛姐微信关注更多洛家信息哈！@<label runat="server" id="Label5">某某</label>
                            <p><a class="ad-link" href="http://wq.jd.com/mshop/gethomepage?venderid=11468">查看详情 <img src="http://wxstatic.luxlead.com/Images/link.png" /></a></p>
                            <img src="http://wxstatic.luxlead.com/Images/%E6%B4%9B%E5%A7%9023.png" /></div>
                        <p class="time">60分钟前</p>
                        <img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" /><label runat="server" id="Label6">某某</label>，林青霞，谢娜，宁静..</div>
                      <div class="cmt-list">
                        <p><span>杨钰莹：</span>穿上洛家的衣服，优雅，知性</p>
                        <p><span>谢娜</span><span> ：</span>从此路人转粉，做个优雅软妹子，哇咔咔！</p>
                      </div>
                    </div>
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