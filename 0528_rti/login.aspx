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

        <div class="form-group" >
         <label class="col-sm-2 control-label"><span class="iconstyle">*</span>驗證碼：</label>
        <div class="input-group" style="margin-left:20px">
            <input id="cod" type="password" class="form-control col-sm-4"  name="password" >
        </div>
  </div>  

   <div class="form-group" >
         <label class="col-sm-2 control-label"></label>
        <div class="input-group" style="margin-left:20px">    
               
         <label style="width:195px;height:35px;background-color:#886600;margin-top:5px">
                <span style="font-size:22px;padding-left:50px;padding-top:5px">124565</span>  
          </label>               
             <button type="button" style="width:80px;text-align:left;margin-left:5px; " class="btn btn-success " onclick="onSearch();">重新選取</button>            
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