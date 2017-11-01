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
<div style="margin:20px;margin-left:40px">
<h4 class="title">會員資料</h4>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-2 control-label"><span class="iconstyle">*</span>姓名：</label>
            <div class="col-sm-4">
                <input type="text" class="form-control" placeholder="姓名" id="nam" />              
            </div>
        </div>
         <div class="form-group">
              <label class="col-sm-2 control-label"><span class="iconstyle">*</span>性別：</label>
            <div class="form-inline col-sm-4">
                <select  class="form-control col-sm-12" id="sex">
                    <option value="">請選擇</option>
                    <option value="1">男</option>
                    <option value="2">女</option>
              </select>
                </div>
     </div>
         <div class="form-group">
              <label class="col-sm-2 control-label">教育程度：</label>
            <div class="form-inline col-sm-4">
                <select  class="form-control col-sm-12" id="edu">
                    <option value="">請選擇</option>
                    <option value="1">大學</option>
                    <option value="2">高中</option>
                </select>
            </div>
        </div>
        
           <div class="form-group">
              <label class="col-sm-2 control-label">職業：</label>
            <div class="form-inline col-sm-4">
                <select  class="form-control col-sm-12" id="job">
                    <option value="">請選擇</option>
                    <option value="1">軍人</option>
                    <option value="2">工程</option>
                </select>
            </div>
        </div>

         <div class="form-group ">
              <label class="col-sm-2 control-label"><span class="iconstyle">*</span>生日：</label>
            <div class="col-sm-3">
               <input type="date" class="datetimepicker form-control" id="birth" >
            </div>     
        </div>
         <div class="form-group ">
            <label class="col-sm-2 control-label ">通訊軟體：</label>
            <div class="col-sm-5  ">
                <input type="text" class="form-control" placeholder="通訊軟體" id="line" name="cc" />
            </div>
        </div>
         <div class="form-group ">
            <label class="col-sm-2 control-label "><span class="iconstyle">*</span>手機：</label>
            <div class="col-sm-5  ">
                <input type="text" class="form-control" placeholder="手機" id="phone"  onkeypress='if (event.keyCode!=46 && (event.keyCode < 48 || event.keyCode > 57)) event.returnValue=false' />
            </div>
        </div>
          <div class="form-group ">
            <label class="col-sm-2 control-label ">地址：</label>
            <div class="col-sm-5  ">
                <input type="text" class="form-control" placeholder="地址" id="addr"  /> 
            </div>
        </div>

    </div>
        <div class="form-group" >
            <div class="col-sm-offset-2 col-sm-5" style="text-align:right">
                <button type="button" style="width:70px;margin-bottom:10px" class="btn btn-primary "  onclick="onSearch();">確認</button>
            </div>
        </div>
    </div>
     <script language="javascript" type="text/javascript">
      $(document).ready(function () {
          $('.datetimepicker').datetimepicker({
              viewMode: 'years',
              format: 'MM/YYYY'
          });
      });
      function onSearch() {
          var name = $("#nam").val();
          var sex = $("#sex").val();
          var edu = $("#edu").val();
          var job = $("#job").val();
          var birth = $("#birth").val();
          var phone = $("#phone").val();
          var addr = $("#addr").val();

        
          if (name == "") {
              alert("請輸入姓名");
              return false;
          } else if (sex == "") {
              alert("請選擇性別");
              return false;
          } else if (edu == "") {
              alert("請選擇教育程度");
              return false;
          } else if (job == "") {
              alert("請選擇職業");
              return false;
          } else if (birth == "") {
              alert("請擇生日");
              return false;
              if (birth.length != 10) {
                  alert("生日格式有誤請檢查");
                  return false;
              }
          } else if (phone == "")
          {
              alert("請輸入手機");
              return false;
             
          } else if (phone.length != "10")
          {
              alert("手機號碼格式有誤請檢查");
              return false;
          }
          else if (addr == "")
          {
              alert("請輸入地址");
              return false;
          }
          alert("成功!!");
      }
 
    </script>
</asp:Content>