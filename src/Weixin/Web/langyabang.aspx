<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="langyabang.aspx.cs" Inherits="Weixin.Web.langyabang" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>刚和梅长苏吃饭聊天，他说他喜欢我，你们信吗？</title>   
    <meta name="viewport" content="initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no" />     
    <link href="http://wxstatic.luxlead.com/Css/style.css" rel="stylesheet" />
    <script src="http://wxstatic.luxlead.com/JS/zepto.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jweixin-1.0.0.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jquery.min.js"></script>    
</head>

<body style="" >
    <header>
        <img id="bg" src="http://wxstatic.luxlead.com/Images/TB2iUOufVXXXXXyXXXXXXXXXXXX_!!817719264.jpg" />
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
                    <img class="po-avt data-avt" src="http://wxstatic.luxlead.com/Images/TB2q8mpfVXXXXXuXXXXXXXXXXXX_!!817719264.jpg" />
              </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">梅长苏</p>
                        <p class="post">大家猜猜，如果我回到现代，最喜欢谁？</p>
                        <p class="time">刚刚</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />霓凰郡主，洛姐...</div>
                        <div class="cmt-list">
                        <p><span>飞流：</span><label runat="server" id="lable0"></label></p>
                        <p><span>言豫津：</span>当然是<label runat="server" id="Label2"></label></p>
                         <p><span>梅长苏：</span>为什么是<label runat="server" id="Label3"></label>？</p>
                          <p><span>言豫津</span >回复<span class="data-name">梅长苏</span><span>：</span><label runat="server" id="Label4"></label>唱歌最好听</p>
                           <p><span>飞流</span >回复<span class="data-name">梅长苏</span><span>：</span><label runat="server" id="Label5"></label>姐姐最漂亮</p>
                        </div>
                    </div>
                </div>
            </li>
          <li>
            <div class="po-avt-wrap">
              <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2rvedfVXXXXbCXXXXXXXXXXXX_!!817719264.jpg" />
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="po-name">胡歌</p>
                <p class="post">谁敢冒充我说实话我就打谁哦！<img src="http://wxstatic.luxlead.com/Images/TB2qOtWfVXXXXb5XXXXXXXXXXXX_!!817719264.jpg" width="116" height="128" /></p>
                <p class="time">29分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
                <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />太皇太后，誉王，靖王，洛姐...</div>
                <div class="cmt-list">
                  <p><span>飞流：</span>给个眼神就好，我来帮你打~                  </p>
                  <p><span class="data-name">洛姐</span><span>：</span>好一个兄弟情深。</p>
                  <p><span>胡歌</span>回复<span class="data-name">洛姐</span><span>：</span>好吧，不打你！</p>
                </div>
              </div>
            </div>
          </li>
          <li>
            <div class="po-avt-wrap">
              <img class="po-avt" runat="server" src="http://wxstatic.luxlead.com/Images/TB27_uofVXXXXXGXXXXXXXXXXXX_!!817719264.jpg" />
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="po-name"><label runat="server" id="lable1"></label></p>
                <div class="post">
                  <p><b>一路走来真的很不容易，</b>能获得中国好声音冠军，真的很谢谢大家的支持！</p>
                  <img class="list-img" src="http://wxstatic.luxlead.com/Images/TB2gUxKfVXXXXaGXpXXXXXXXXXX_!!817719264.jpg" /></div>
                <p class="time">50分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
                <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />胡歌，汪峰，<label runat="server"></label>...</div>
                <div class="cmt-list">
                  <p><span class="data-name">胡歌</span><span>：</span>你的实力大家都知道！</p>                 
                   <p><span><label runat="server" id="Label6"></label></span>回复<span>胡歌 ：</span>有你真好</p>
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
                <div class="post">有请《双11代言人》冠军→<label runat="server" id="Label7"></label>，扫描二维码↓可参与第二季《双11代言人》
                  <p><a class="ad-link" href="http://wq.jd.com/mshop/gethomepage?venderid=11468">查看详情 <img src="http://wxstatic.luxlead.com/Images/link.png" /></a></p>
                <div style="width:100%"><div style="float:left;"><img src="http://wxstatic.luxlead.com/Images/%E6%B4%9B%E5%A7%9017.png" width="100" height="100" /></div><div style="float:left"><img runat="server" id="ewm" src="http://wxstatic.luxlead.com/Images/c6.jpg" width="100" height="100" /></div></div>
                <p class="time">20小时前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
                <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />胡歌，...</div>
                <div class="cmt-list">
                  <p><span><label runat="server" id="Label8"></label>：</span>谢谢洛姐的支持，我会继续努力的！</p>
                  <p><span>洛姐</span>回复<span><label runat="server" id="Label9"></label> ：</span><label runat="server" id="Label10"></label>是最漂亮的/偷笑</p>
                </div>
              </div>
            </div>
            </div>
          </li>            
        
          <li>
            <div class="po-avt-wrap">
              <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2rvedfVXXXXbCXXXXXXXXXXXX_!!817719264.jpg" />
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="po-name">胡歌</p>
                <p class="post">有时候拉黑一个人不是讨厌她，而是怕自己忍不住联系她。<img src="http://wxstatic.luxlead.com/Images/TB2eetQfVXXXXXxXpXXXXXXXXXX_!!817719264.jpeg" width="150" height="85" class="data-avt" /></p>
                <p class="time">3天前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
                <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />洛姐...</div>
                <div class="cmt-list">
                  <p><span>刘涛：</span>你真的拉黑了？？</p>
                  <p><span>胡歌</span >回复<span class="data-name">刘涛</span><span>：</span><label runat="server" id="Label14"></label>忙到没空理我了，我很想拉黑她，但我真做不到！</p>
                </div>
              </div>
            </div>
          </li>
          <li>
            <div class="po-avt-wrap">
                  <div class="po-avt-wrap"> <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2cwamfVXXXXX8XXXXXXXXXXXX_!!817719264.jpg" /> </div>
                  <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">那英</p>
                      <p class="post"><label runat="server" id="Label15"></label>，你还来不来？哈林请喝奶茶，就等你了</p>
                      <p class="time">3天前</p>
                      <img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" /> </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                      <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />中国好声音</div>
                      <div class="cmt-list">
                        <p><span><label runat="server" id="Label16"></label>：</span>来啦来啦，马上就到了。</p>
                        <p><span>胡歌</span >回复<span class="data-name"><label runat="server" id="Label17"></label></span><span>：</span>慢一点，注意看车。</p>
                        <p><span><label runat="server" id="Label18"></label></span >回复<span class="data-name">胡歌</span><span>：</span>好</p>            
                      </div>
                    </div>
                  </div>
            </div>
          </li>
          <li>
            <div class="po-avt-wrap">
              <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2q8mpfVXXXXXuXXXXXXXXXXXX_!!817719264.jpg" />
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="po-name">梅长苏</p>
                <p class="post">今年的雪下得比往年早了，幸好提前预备了冬装。<img src="http://wxstatic.luxlead.com/Images/TB2_blZfVXXXXbxXXXXXXXXXXXX_!!817719264.jpg" width="193" height="118"></p>
                <p class="time">4天前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
                <div class="cmt-list">
                  <p><span class="data-name">飞流</span><span>：</span>这套真好看！</p>
                  <p><span>梅长苏</span>回复<span class="data-name">飞流</span><span>：</span>么么哒！</p>
                </div>
              </div>
            </div>
          </li>
           <li>
            <div class="po-avt-wrap">
              <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2rzmHfVXXXXafXpXXXXXXXXXX_!!817719264.jpg" />
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="po-name">霓凰郡主</p>
                <div class="post">
                <p><b>我的毛领大衣固然好看，但都是斗篷型的，下面会漏风。</b>好想试试现代的毛领大衣。<div style="float:left"><img src="http://wxstatic.luxlead.com/Images/TB2DrGEfVXXXXbeXpXXXXXXXXXX_!!817719264.jpg" width="100" height="93"></div><div style="float:left"><img src="http://wxstatic.luxlead.com/Images/TB2UmS3fVXXXXXLXXXXXXXXXXXX_!!817719264.png" / width="100" height="93"></div>
                <div style="float:left"><img src="http://wxstatic.luxlead.com/Images/TB2vtiHfVXXXXafXpXXXXXXXXXX_!!817719264.jpg" width="100" height="93"></div>
                <div style="float:left"><img src="http://wxstatic.luxlead.com/Images/%E6%B4%9B%E5%A7%9017.png" width="100" height="93" /></div></p>
                </div>
                <p class="time">2天前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
                <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" /><b>梅长苏</b>，太皇太后</div>
                <div class="cmt-list">
                  <p><span class="data-name">梅长苏</span><span>：</span>带上我，呜呜...</p>
                  <p><span class="data-name">飞流</span><span>：</span>我也要，在哪买？</p>
                    <p><span>洛姐</span>回复<span class="data-name">飞流</span><span>：</span>扫码上面二维码就可以买啦！</p>
                    <p><span>霓凰郡主</span>回复<span class="data-name">洛姐</span><span>：</span>收你广告费哦！我先扫扫看...</p>                  
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
    
   