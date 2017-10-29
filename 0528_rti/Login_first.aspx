<%@ Page Language="C#" AutoEventWireup="false" masterpagefile="~/container.master" CodeFile="Login_first.aspx.cs" Inherits="Login_first" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin:20px">
<h3>歡迎登入 </h3>
<section>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-2 control-label">電子郵件：</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" placeholder="請輸入信箱" id="naper" name="naper" />
            </div>
        </div>
  



        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <button type="button" class="btn btn-success" onclick="onSearch();">查詢</button>
                <button type="button" class="btn btn-success" id="PrintBtn">列印</button>
            </div>
        </div>
    </div>

</section>
     </div>
</asp:Content>