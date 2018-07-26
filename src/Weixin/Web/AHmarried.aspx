<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AHmarried.aspx.cs" Inherits="Weixin.Web.AHmarried" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>男神嫁了！对象居然是她！</title>
    <meta name="viewport" content="initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no" />
    <link href="http://wxstatic.luxlead.com/Css/style.css" rel="stylesheet" />
    <script src="http://wxstatic.luxlead.com/JS/zepto.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jquery.min.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jweixin-1.0.0.js"></script>
</head>    

<body style="" >
    <header>
        <img id="bg" src="http://wxstatic.luxlead.com/Images/TB2p.zNfVXXXXckXXXXXXXXXXXX_!!817719264.jpg" />
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
                    <img class="po-avt data-avt" runat="server" src="http://wxstatic.luxlead.com/Images/TB27_uofVXXXXXGXXXXXXXXXXXX_!!817719264.jpg" />
                </div>
                <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name"><label runat="server">壹壹壹</label></p>
                      <p class="post">晓明和baby大婚，我居然没到场，真想跟你们拍大合影@黄晓明 @anglebaby<img src="http://wxstatic.luxlead.com/Images/TB2fynEfVXXXXaQXpXXXXXXXXXX_!!817719264.jpg" width="206" height="127" /></p>
                        <p class="time">刚刚</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
                    </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                        <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />范冰冰，李晨，王思聪...</div>
                        <div class="cmt-list">
                            <p><span>黄晓明：</span>没事，红包来就可以了！</p>                      
                            <p><span>马云</span ><span>：</span>我也没去，不过我不打算发红包！</p>
                            <p><span>黄晓明</span >回复<span class="data-name">马云</span><span>：</span>怎么这么抠。。。</p>         
                        </div>
                    </div>
                </div>
            </li>
          </ul>
        <p>&nbsp;</p>
        <ul>

          <li>
            <div class="po-avt-wrap">
              <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB22a6wfVXXXXcdXpXXXXXXXXXX_!!817719264.jpg">
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="po-name">anglebaby</p>
                <p class="post">谢谢小马哥送的衣服，你说哪套最好看？@<label runat="server">壹壹壹</label></p>
                <p>
                    <img  src="http://wxstatic.luxlead.com/Images/TB2Sp_BfVXXXXbdXpXXXXXXXXXX_!!817719264.jpg" width="100" height="100" />
                    <img  src="http://wxstatic.luxlead.com/Images/TB290YDfVXXXXbeXpXXXXXXXXXX_!!817719264.jpg" width="100" height="100" />
                </p>    
                <p class="time">20分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
                <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" / />赵薇，李冰冰，徐峥，杜淳...</div>
                <div class="cmt-list">
                  <p><span>黄晓明：</span>我的baby穿什么都好看！</p>
                  <p><span class="data-name"><label runat="server">壹壹壹</label></span><span>：</span>好会秀恩爱，不过真的都好看。</p>
                  <p><span>范冰冰</span><span>：</span>都好看！</p>
                </div>
              </div>
            </div>
          </li>
          <li>
            <div class="po-avt-wrap">
              <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2TzLUfVXXXXa_XXXXXXXXXXXX_!!817719264.jpg" />
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="po-name">徐峥</p>
                <div class="post">
                  <p><b>@<label runat="server">壹壹壹</label> 怎么没来参加婚礼？</b></p>
                      我想邀请你担任《港囧2》的女主角，有空给我回个信。
                   <p><img class="list-img" runat="server" src="http://wxstatic.luxlead.com/Images/TB27_uofVXXXXXGXXXXXXXXXXXX_!!817719264.jpg" /></p>
                </div>
                <p class="time">50分钟前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" / />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
                <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" / />黄晓明，anglebaby，林志玲，舒淇...</div>
                <div class="cmt-list">
                  <p><span class="data-name"><label runat="server">壹壹壹</label></span><span>：</span>最近通告好多，你这个要排到明年3月才能参加哦。</p>
                 
                   <p><span>范冰冰</span>回复<span><label runat="server">壹壹壹</label> ：</span>不要太累了，要劳逸结合。</p>
                </div>
              </div>
            </div>
          </li>
          <li>
            <div class="po-avt-wrap">
              <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB26bPTfVXXXXbvXXXXXXXXXXXX_!!817719264.jpg" />
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="ads">推广<img src="http://wxstatic.luxlead.com/Images/ads.png" /></p>
                <p class="po-name">范冰冰</p>
                <p>为@<label runat="server">壹壹壹</label>设计的衣服做好了~~，快扫描二维码看看！</p>
                <p><img src="http://wxstatic.luxlead.com/Images/meizi636.jpg" width="200" height="200" /></p>
            
                <p class="time">2小时前</p>
                <img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
                <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />赵薇，李晨，马云...</div>
                <div class="cmt-list">
                  <p><span><label runat="server">壹壹壹</label>：</span>谢谢冰冰，刚好要换季买衣服了。</p>
                  <p><span>范冰冰</span>回复<span><label runat="server">壹壹壹</label> ：</span>扫描二维码加我的设计师，喜欢哪件直接跟她说哦。</p>
                  <p><span>马云：</span>衣服挺好看。</p>
                </div>
              </div>
            </div>
          </li>            
          
          <li>
            <div class="po-avt-wrap">
              <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2mUrNfVXXXXcCXXXXXXXXXXXX_!!817719264.jpg" />
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="po-name">李晨</p>
                <p class="post">黄晓明和baby都大婚了，什么时候轮到我们？@范冰冰<img src="http://wxstatic.luxlead.com/Images/TB28MbYfVXXXXaNXXXXXXXXXXXX_!!817719264.jpg" width="161" height="104" class="data-avt" /></p>
                <p class="time">昨天</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
                <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />马云，赵薇，王思聪，李冰冰...</div>
                <div class="cmt-list">
                  <p><span>anglebaby：</span>大黑牛，你要主动点才行哦。</p>
                  <p><span>王思聪：</span>我都还没结婚，你不要太急。</p>
                  <p><span>黄晓明</span >回复<span class="data-name">王思聪</span><span>：</span>最近跟新女友过得怎样?</p>
                </div>
              </div>
            </div>
          </li>
          <li>
            <div class="po-avt-wrap">
                  <div class="po-avt-wrap"> 
                      <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB22DT2fVXXXXXnXXXXXXXXXXXX_!!817719264.jpg" / > 
                  </div>
                  <div class="po-cmt">
                    <div class="po-hd">
                      <p class="po-name">马云</p>
                      <p class="post">@<label runat="server">壹壹壹</label>，今年冬天会来得比较早，我已经给你准备好冬装了，你什么时候来拿？</p>
                      <p class="time">昨天</p>
                      <img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" /> </div>
                    <div class="r"></div>
                    <div class="cmt-wrap">
                      <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />徐峥，anglebaby，林志玲</div>
                      <div class="cmt-list">
                        <p><span><label runat="server">壹壹壹</label>：</span>小马哥，最近没空，过段时间来拿</p>
                        <p><span>马云</span >回复<span class="data-name"><label runat="server">壹壹壹</label></span><span>：</span>天气都已经转凉了，再不来拿，我会忍不住卖掉的。</p>
                        <p><span>范冰冰</span >回复<span class="data-name">马云</span><span>：</span>世界上有种东西叫“快递”</p>
                        <p><span>马云</span >回复<span>范冰冰：</span>好吧，到付！</p>
                        
                      </div>
                    </div>
                  </div>

            </div>
          </li>
          <li>
            <div class="po-avt-wrap">
              <img class="po-avt" src="http://wxstatic.luxlead.com/Images/TB2uX2EfVXXXXa5XpXXXXXXXXXX_!!817719264.jpg" />
            </div>
            <div class="po-cmt">
              <div class="po-hd">
                <p class="po-name">王思聪</p>
                <p >周末要一起去看洛诗琳时装show吗？@<label runat="server">壹壹壹</label>，快扫二维码一起去。</p>
                  <p><img src="http://wxstatic.luxlead.com/Images/meizi635.jpg" width="200" height="200" /></p>
                <p class="time">2天前</p><img class="c-icon" src="http://wxstatic.luxlead.com/Images/c.png" />
              </div>
              <div class="r"></div>
              <div class="cmt-wrap">
               <div class="like"><img src="http://wxstatic.luxlead.com/Images/l.png" />张予曦，anglebaby，林志玲，马云...</div>
                <div class="cmt-list">
                  <p><span class="data-name"><label runat="server">壹壹壹</label></span><span>：</span>要是看到喜欢的衣服，你会给我买吗？</p>
                  <p><span>王思聪</span>回复<span class="data-name"><label runat="server">壹壹壹</label></span><span>：</span>必须的！</p>
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