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
    <div class="form-horizontal" ">
   <div class="form-group" >
         <label class="col-sm-2 control-label">帳號：</label>
            <div class="input-group" style="margin-left:20px">
                <%--<span class="input-group-addon "><i class="glyphicon glyphicon-user"></i></span>--%>
                <input id="username"  class="form-control" style="width:285px" name="username" placeholder="Username">
            </div>
  </div>         
    <div class="form-group" >
         <label class="col-sm-2 control-label">密碼：</label>
        <div class="input-group" style="margin-left:20px">
            <%--<span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>--%>
            <input id="password" type="password" class="form-control col-sm-2" style="width:285px" name="password" placeholder="Password">
        </div>
  </div>   
        <div class="form-group" style="text-align:right" >
            <div class="col-sm-offset-2 col-sm-5">
                <button type="button" style="width:70px;" class="btn btn-primary " onclick="onSearch();">確定</button>
            </div>
        </div>
     </div>     
  </div> 
    
        
      <script >
          function onSearch() {
              alert("a");
          }
    </script>
</asp:Content>