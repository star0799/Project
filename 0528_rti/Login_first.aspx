<%@ Page Language="C#" AutoEventWireup="false" masterpagefile="~/container.master" CodeFile="Login_first.aspx.cs" Inherits="Login_first" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                <input type="password" class="form-control" placeholder="請輸密碼" id="naper" name="naper" />
                <input type="password" class="form-control" placeholder="請輸密碼" id="naper" name="naper" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label">電子郵件：</label>
            <div class="col-sm-6">
                <input type="text" class="form-control" placeholder="請輸入信箱" id="naper" name="naper" />
            </div>
        </div>
 
         <div class="form-group ">
               <label class="col-sm-2 control-label">國別：</label>
            <div class="form-inline col-sm-4">
                <select id="Kdper" name="Kdper" class="form-control col-sm-4">
                    <option value="">1</option>
                    <option value="1">2</option>
                    <option value="2">3.</option>
                </select>
            </div>
      </div>
           <div class="form-group ">
               <label class="col-sm-2 control-label">語言：</label>
                <div class="form-inline col-sm-4">
                    <label class="checkbox-inline" ><input type="checkbox" value="">中文</label>
                    <label class="checkbox-inline" style="margin-left:10px"><input type="checkbox" value="">英文</label>
                    <label class="checkbox-inline" style="margin-left:10px"><input type="checkbox" value="">日文</label>
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