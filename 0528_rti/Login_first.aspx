<%@ Page Language="C#" AutoEventWireup="false" masterpagefile="~/container.master" CodeFile="Login_first.aspx.cs" Inherits="Login_first" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="boostrap/css/bootstrap.min.css" rel="stylesheet" />
<style>

    body {
     font-family: 微軟正黑體, "Microsoft JhengHei", Arial, sans-serif;
    }
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
<h4 class="title">會員註冊 </h4>
    <div class="form-horizontal">
          <div class="form-group ">
            <label class="col-sm-2 control-label "><span class="iconstyle">*</span>帳號：</label>
            <div class="col-sm-5  ">
                <input type="text" class="form-control" placeholder="請輸入帳號" id="account" />
            </div>
             <%-- <div style="margin-top:5px">
      <span style="color:red;margin-top:10px">帳號英文數字總共應至少要６個</span>
                  </div>--%>
        </div>
         <div class="form-group">

            <label class="col-sm-2 control-label"><span class="iconstyle">*</span>密碼：</label>
            <div class="col-sm-5">
                <input type="password" class="form-control"  id="password" />
              </div>
                <%-- <div style="margin-top:5px">
                     <span style="color:red;margin-top:10px">密碼必需英文數字混合應至少要６個</span>
                 </div>--%>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label"><span class="iconstyle">*</span>電子郵件：</label>
            <div class="col-sm-6">
                <input type="text" class="form-control" placeholder="請輸入信箱" id="email" />
            </div>
        </div>
 
         <div class="form-group ">
               <label class="col-sm-2 control-label"><span class="iconstyle">*</span>國別：</label>
            <div class="form-inline col-sm-4">
                <select id="country"  class="form-control col-sm-4">
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
                    <label class="checkbox-inline control-label" style="margin-left:10px" ><input type="checkbox" class="radioBtnClass"  value="華語Mandarin">華語Mandarin</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="閩南語Minnan">閩南語Minnan</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="客家語Hakka">客家語Hakka</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="粵語Cantonese">粵語Cantonese</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="English">英語 English</label>
                    <label class="checkbox-inline"><input type="checkbox" class="radioBtnClass" value="德語German">德語German</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="法語French">法語French</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="俄語Russian">俄語Russian</label>
                    <label class="checkbox-inline"><input type="checkbox" class="radioBtnClass" value="西班牙語Spanish">西班牙語Spanish</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="日語Japanese">日語Japanese</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="越南語Vietnamese">越南語Vietnamese</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="泰語Thai">泰語Thai</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="印尼語Indonesian">印尼語Indonesian</label> 
                    <label class="checkbox-inline" ><input type="checkbox" class="radioBtnClass" value="其他other">其他other</label>
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
              var account = $("#account").val();
              var password = $("#password").val();
              var email = $("#email").val();
              var country = $("#country").val();
             
              //語言
              var langu = [];
              $("input[type='checkbox'].radioBtnClass").each(function () {
                  if ($(this).prop("checked")) {
                      langu.push($(this).val());
                  }
              });
             

              if (account == "")
              {
                  alert("請輸入帳號");
                  return false;
              }
              if (password == "")
              {
                  alert("請輸入密碼");
                  return false;
              }
              //var chkpwd = validPassword(password);
              //if (chkpwd == false)
              //{
              //    alert("密碼必須要英數混和至少六個，請檢查");
              //    return false;
              //}
              if (email == "")
              {
                  alert("請輸入信箱");
                  return false;
              }
              var chkemail = validEmail(email);
              if (chkemail == false)
              {
                  alert("電子郵件格式有誤，請檢查");
                  return false;
              }
              if (country == "")
              {
                  alert("請選擇國家");
                  return false;
              }
              if (langu == "")
              {
                  alert("請選擇語言");
                  return false;
              }
          }
      
         // 驗證帳號
          function validAccount(account) {
              //var accountRule = /{5,14}$/g;
              return accountRule.test(account);
          }
          //驗證密碼
          function validPassword(password)
          {
              var passwordRule = /^[A-Za-z][0-9A-Za-z_]{5,14}$/g;
              return passwordRule.test(password);
          }
          //驗證email
          function validEmail(email) {
              var emailRule = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$/g;
              return emailRule.test(email);
          }

    
    </script>
</asp:Content>