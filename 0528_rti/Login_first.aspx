<%@ Page Language="C#" AutoEventWireup="false" masterpagefile="~/container.master" CodeFile="Login_first.aspx.cs" Inherits="Login_first" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin:20px;margin-left:40px">
<h4>歡迎登入 </h4>
    <div class="form-horizontal">
          <div class="form-group">
            <label class="col-sm-2 control-label">帳號：</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" placeholder="帳號" id="naper" name="naper" />
            </div>
        </div>
         <div class="form-group">
            <label class="col-sm-2 control-label">密碼：</label>
            <div class="col-sm-10">
                <input type="password" class="form-control" placeholder="請輸入信箱" id="naper" name="naper" />
            </div>
        </div>


        <div class="form-group">
            <label class="col-sm-2 control-label">電子郵件：</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" placeholder="請輸入信箱" id="naper" name="naper" />
            </div>
        </div>
  
         <div class="form-group ">
               <label class="col-sm-2 control-label">國別：</label>
            <div class="form-inline col-md-3 col-sm-4">
                <select id="Kdper" name="Kdper" class="form-control col-sm-4">
                    <option value="">1</option>
                    <option value="1">2</option>
                    <option value="2">3.</option>
                </select>
            </div>
      </div>

        
            <div class="container">
            <p>語言：</p>
            <form>
            <label class="radio-inline">
                <input type="radio" name="optradio">Option 1
            </label>
            <label class="radio-inline">
                <input type="radio" name="optradio">Option 2
            </label>
            <label class="radio-inline">
                <input type="radio" name="optradio">Option 3
            </label>
            </form>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <button type="button" class="btn btn-success" onclick="onSearch();">確認</button>
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