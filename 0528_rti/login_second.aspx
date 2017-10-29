<%@ Page Language="C#" AutoEventWireup="false" masterpagefile="~/container.master" CodeFile="Login_first.aspx.cs" Inherits="Login_first" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin:20px;margin-left:40px">
<h4>歡迎登入 </h4>
    <div class="form-horizontal">
          <div class="form-group">
            <label class="col-sm-2 control-label">姓名：</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" placeholder="姓名" id="naper" name="naper" />
            </div>
        </div>
         <div class="form-group">
            <label class="col-sm-2 control-label">性別：</label>
            <div class="col-sm-10">
                <input type="password" class="form-control" placeholder="性別" id="naper" name="naper" />
            </div>
        </div>


        <div class="form-group">
            <label class="col-sm-2 control-label">教育程度：</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" placeholder="教育程度" id="naper" name="naper" />
            </div>
        </div>
  
         <div class="form-group ">
               <label class="col-sm-2 control-label">職業：</label>
            <div class="form-inline col-md-3 col-sm-4">
                <select id="Kdper" name="Kdper" class="form-control col-sm-4">
                    <option value="">1</option>
                    <option value="1">2</option>
                    <option value="2">3.</option>
                </select>
            </div>
      </div>
         <div class="form-group ">
              <label class="col-sm-2 control-label">生日：</label>
            <div class="col-sm-3">
               <input type="text" value="2012-05-15 21:05" id="datetimepicker" >
            </div>
          
        </div>

        
        <div class="form-group">
            <label class="col-sm-2 control-label">通訊軟體：</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" placeholder="通訊軟體" id="naper" name="naper" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-sm-2 control-label">手機：</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" placeholder="手機" id="naper" name="naper" />
            </div>
        </div>

        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <button type="button" class="btn btn-success" onclick="onSearch();">確認</button>
            </div>
        </div>
    </div>
     <script language="javascript" type="text/javascript">
      $(document).ready(function () {
          $('#datetimepicker').datetimepicker();

      });
    </script>
</asp:Content>