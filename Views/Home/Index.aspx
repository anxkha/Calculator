<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Calculator.Models.Calculator>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Online Calculator
</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
 <link href="<%: Url.Content("~/Content/Buttons.css") %>" rel="Stylesheet" type="text/css" />
 <link href="<%: Url.Content("~/Content/DisplayWindows.css") %>" rel="Stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="MainContent" runat="server">
 <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
 <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
 <script src="<%: Url.Content("~/Scripts/buttonhandlers.js") %>" type="text/javascript"></script>
   
 <% Html.EnableClientValidation(); %>

 <h2>Online Calculator</h2>

 <% using (Html.BeginForm()) { %>
  <div>
   <%: Html.ValidationMessageFor(m => m.CurrentValue) %>
   <% if (Model.DivideByZero) { %> <span class="error">Cannot divide by zero.</span> <% } %>
   <div class="buttonrow">
    <div style="float: left;">
    <% if (Model.MemoryStore != 0) { %>
     <br /><br /><br /><span class="memory">M</span>
    <% } %>
    </div>
    <%: Html.TextBoxFor(m => m.CurrentHistory, new { @readonly = "readonly", @class = "displaywindow" }) %>
    <br />
    <%: Html.TextBoxFor(m => m.CurrentValue, new { @readonly = "readonly", @class = "displaywindow" })%>
   </div>
   <div class="buttonrow">
    <div><input type="submit" id="MemoryClear" name="MemoryClear" value="MC" class="memorybutton" /></div>
    <div><input type="submit" id="MemoryRecall" name="MemoryRecall" value="MR" class="memorybutton" /></div>
    <div><input type="submit" id="MemorySave" name="MemorySave" value="MS" class="memorybutton" /></div>
    <div><input type="submit" id="MemoryAdd" name="MemoryAdd" value="M+" class="memorybutton" /></div>
    <div><input type="submit" id="MemorySubtract" name="MemorySubtract" value="M-" class="memorybutton" /></div>
   </div>
   <div class="buttonrow">
    <div><input type="button" id="RemoveEntry" name="RemoveEntry" value="&larr;" class="actionbutton" /></div>
    <div><input type="button" id="ClearEntry" name="ClearEntry" value="CE" class="actionbutton" /></div>
    <div><input type="submit" id="Clear" name="Clear" value="C" class="actionbutton" /></div>
    <div><input type="button" id="ChangeSign" name="ChangeSign" value="&plusmn;" class="actionbutton" /></div>
    <div><input type="submit" id="SquareRoot" name="SquareRoot" value="&radic;" class="actionbutton" /></div>
   </div>
   <div class="buttonrow">
    <div><input type="button" id="Seven" name="Seven" value="7" class="numberbutton" /></div>
    <div><input type="button" id="Eight" name="Eight" value="8" class="numberbutton" /></div>
    <div><input type="button" id="Nine" name="Nine" value="9" class="numberbutton" /></div>
    <div><input type="submit" id="Divide" name="Divide" value="/" class="actionbutton" /></div>
    <div><input type="submit" id="Percentage" name="Percentage" value="%" class="actionbutton" /></div>
   </div>
   <div class="buttonrow">
    <div><input type="button" id="Four" name="Four" value="4" class="numberbutton" /></div>
    <div><input type="button" id="Five" name="Five" value="5" class="numberbutton" /></div>
    <div><input type="button" id="Six" name="Six" value="6" class="numberbutton" /></div>
    <div><input type="submit" id="Multiply" name="Multiply" value="*" class="actionbutton" /></div>
    <div><input type="submit" id="Reciprocate" name="Reciprocate" value="1/x" class="actionbutton" /></div>
   </div>
   <div class="buttonrow">
    <div><input type="submit" id="Equals" name="Equals" value="=" class="equalsbutton" /></div>
    <div>
     <div><input type="button" id="One" name="One" value="1" class="numberbutton" /></div>
     <div><input type="button" id="Two" name="Two" value="2" class="numberbutton" /></div>
     <div><input type="button" id="Three" name="Three" value="3" class="numberbutton" /></div>
     <div><input type="submit" id="Minus" name="Minus" value="-" class="actionbutton" /></div>
    </div>
    <div>
     <div><input type="button" id="Zero" name="Zero" value="0" class="zerobutton" /></div>
     <div><input type="button" id="Period" name="Period" value="." class="numberbutton" /></div>
     <div><input type="submit" id="Plus" name="Plus" value="+" class="actionbutton" /></div>
    </div>
   </div>
  </div>
 <% } %>
</asp:Content>
