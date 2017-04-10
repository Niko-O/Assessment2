@Code
    ViewData("Title") = "BRIS - Register account"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<form class="pure-form pure-form-aligned" onsubmit="return false;">
    <div class="pure-control-group">
        <label for="Register_UserName">User name:</label>
        <input
            id="Register_UserName" name="Register_UserName"
            type="text"
            oninput="document.Bris.UI.Register.FormDataChanged();"
        />
    </div>
    <span id="Register_UserName_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-control-group">
        <label for="Register_Email">E-Mail address:</label>
        <input
            id="Register_Email" name="Register_Email"
            type="text"
            oninput="document.Bris.UI.Register.FormDataChanged();"
        />
    </div>
    <span id="Register_Email_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-control-group">
        <label for="Register_Password">Password:</label>
        <input
            id="Register_Password" name="Register_Password"
            type="text"
            oninput="document.Bris.UI.Register.FormDataChanged();"
        />
    </div>
    <span id="Register_Password_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-control-group">
        <label for="Register_ConfirmPassword">Retype password:</label>
        <input
            id="Register_ConfirmPassword" name="Register_ConfirmPassword"
            type="password"
            oninput="document.Bris.UI.Register.FormDataChanged();"
        />
    </div>
    <span id="Register_ConfirmPassword_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-controls">
        <button id="Register_SubmitButton" class="pure-button pure-button-primary" disabled onclick="document.Bris.UI.Register.BeginRegister(this);">Register</button><br/>
        <span id="Register_Error" class="ErrorMessage"></span><br/>
        <span id="Register_DebugEmailContent" class="DebugMessage"></span>
    </div>
</form>