<%@ Page Language="C#" AutoEventWireup="false" masterpagefile="~/container.master" CodeFile="Login_first.aspx.cs" Inherits="Login_first" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="boostrap/css/bootstrap.min.css" rel="stylesheet" />
<style>
    
.title {
    margin: 20px 0 30px 0;
    font-family: 微軟正黑體, "Microsoft JhengHei", Arial, sans-serif;
    font-size: 1.6em;
    letter-spacing:0.5;
    color: #444;
    font-weight: bold;
}
.title::before{
    content: '';
    vertical-align: text-bottom;
    display: inline-block;
    height: 30px;
    width: 5px;
    margin-right: 10px;
    background-color: #009D4F;
    border-radius: 5px;
}
    .iconstyle {
     color:red;
    
    }
</style>

<div style="margin:20px;margin-left:40px;" >
<h4 class="title">歡迎登入 </h4>
    <div class="form-horizontal">
          <div class="form-group ">
            <label class="col-sm-2 control-label "><span class="iconstyle">*</span>帳號：</label>
            <div class="col-sm-5  ">
                <input type="text" class="form-control" placeholder="請輸入帳號" id="naper" name="naper" />
            </div>
        </div>
         <div class="form-group">

            <label class="col-sm-2 control-label"><span class="iconstyle">*</span>密碼：</label>
            <div class="col-sm-5">
                <input type="password" class="form-control" placeholder="請輸入信箱" id="naper" name="naper" />

        </div>
             </div>
        <div class="form-group">
            <label class="col-sm-2 control-label"><span class="iconstyle">*</span>電子郵件：</label>
            <div class="col-sm-6">
                <input type="text" class="form-control" placeholder="請輸入信箱" id="naper" name="naper" />
            </div>
        </div>
 
         <div class="form-group ">
               <label class="col-sm-2 control-label"><span class="iconstyle">*</span>國別：</label>
            <div class="form-inline col-sm-4">
                <select id="Kdper" name="Kdper" class="form-control col-sm-4">
                    <option value="">請選擇</option>
                    <option value="1">安道爾</option>
                    <option value="2">阿聯</option>
                    <option value="3">阿富汗</option>
                    <option value="4">安地卡及巴布達</option>
                    <option value="5">安圭拉</option>
                    <option value="6">安哥拉</option>
                    <option value="7">南極洲</option>
                    <option value="8">阿根廷</option>
                    <option value="9">美屬薩摩亞</option>
                    <option value="10">奧地利</option>
                    <option value="11">澳大利亞</option>
                    <option value="12">阿魯巴</option>
                    <option value="13">亞塞拜然</option>
                    <option value="14">波士尼亞與赫塞哥維納</option>
                    <option value="15">巴貝多</option>
                    <option value="16">孟加拉國</option>
                    <option value="17">比利時</option>
                    <option value="18">布吉納法索</option>
                    <option value="13">保加利亞</option>
                    <option value="14">巴林</option>
                    <option value="15">蒲隆地</option>
                    <option value="16">貝南</option>
                    <option value="17">聖巴泰勒米</option>
                    <option value="18">百慕達</option>
                </select>
            </div>
      </div>
           <div class="form-group ">
               <label class="col-sm-2 control-label"><span class="iconstyle">*</span>語言：</label>
                   <div class="form-inline col-sm-10">
                    <label class="checkbox-inline" style="margin-left:10px" ><input type="checkbox" value="">華語Mandarin</label>
                    <label class="checkbox-inline" ><input type="checkbox" value="">閩南語Minnan</label>
                    <label class="checkbox-inline" ><input type="checkbox" value="">客家語Hakka</label>
                    <label class="checkbox-inline" ><input type="checkbox" value="">粵語Cantonese</label>
                    <label class="checkbox-inline" ><input type="checkbox" value="">英語 English</label>
                    <label class="checkbox-inline"><input type="checkbox" value="">德語 German</label>
                    <label class="checkbox-inline" ><input type="checkbox" value="">法語French</label>
                    <label class="checkbox-inline" ><input type="checkbox" value="">俄語Russian</label>
                    <label class="checkbox-inline"><input type="checkbox" value="">西班牙語Spanish</label>
                    <label class="checkbox-inline" ><input type="checkbox" value="">日語Japanese</label>
                    <label class="checkbox-inline" ><input type="checkbox" value="">越南語Vietnamese</label>
                    <label class="checkbox-inline" ><input type="checkbox" value="">泰語Thai</label>
                    <label class="checkbox-inline" ><input type="checkbox" value="">印尼語Indonesian</label> 
                    <label class="checkbox-inline" ><input type="checkbox" value="">其他other</label>
                </div>
            </div>    
        <div class="form-group" >
            <div class="col-sm-offset-2 col-sm-6" style="text-align:right">
                <button type="button" style="width:70px;" class="btn btn-primary " onclick="onSearch();">確定</button>
            </div>
        </div>
     </div>     
  </div>     
      <script language="javascript" type="text/javascript">
          function onSearch()
          {
              alert("a");
          }
    </script>
</asp:Content>