@ModelType Assessment2.Models.User
@Code
    ViewData("Title") = "Settings"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<form class="pure-form pure-form-aligned" onsubmit="return false;">
    <div class="pure-control-group">
        <label for="EditUserSettings_UserName">User name:</label>
        <input
            id="EditUserSettings_UserName" name="EditUserSettings_UserName"
            type="text"
            oninput="document.Bris.UI.EditUserSettings.FormDataChanged(@Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Model.Id));"
            value="@Model.Name"
        />
    </div>
    <span id="EditUserSettings_UserName_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-control-group">
        <label for="EditUserSettings_Email">E-Mail address:</label>
        <input
            id="EditUserSettings_Email" name="EditUserSettings_Email"
            type="text"
            oninput="document.Bris.UI.EditUserSettings.FormDataChanged(@Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Model.Id));"
            value="@Model.Email"
        />
    </div>
    <span id="EditUserSettings_Email_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-control-group">
        <label for="EditUserSettings_Password">Password:<br/>(optional)</label>
        <input
            id="EditUserSettings_Password" name="EditUserSettings_Password"
            type="text"
            oninput="document.Bris.UI.EditUserSettings.FormDataChanged(@Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Model.Id));"
        />
    </div>
    <span id="EditUserSettings_Password_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-control-group">
        <label for="EditUserSettings_ConfirmPassword">Retype password:</label>
        <input
            id="EditUserSettings_ConfirmPassword" name="EditUserSettings_ConfirmPassword"
            type="password"
            oninput="document.Bris.UI.EditUserSettings.FormDataChanged(@Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Model.Id));"
        />
    </div>
    <span id="EditUserSettings_ConfirmPassword_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-controls">
        <button
            id="EditUserSettings_SubmitButton"
            class="pure-button pure-button-primary"
            disabled
            onclick="document.Bris.UI.EditUserSettings.BeginSave(this, @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Model.Id));"
        >
            Save
        </button><br/>
        <span id="EditUserSettings_Error" class="ErrorMessage"></span>
    </div>
</form>