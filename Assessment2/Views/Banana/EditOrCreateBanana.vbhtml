@ModelType Assessment2.ViewModel.Banana.EditOrCreateBananaViewModel
@Code
    ViewData("Title") = "BRIS - " & If (Model.Banana is Nothing, "Create Banana", "Edit Banana")
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim FormDataChangedArgument = If (Model.Banana is Nothing, "null", Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Model.Banana.Id))
End Code

<form onsubmit="return false;">
    <div class="pure-control-group">
        <label for="EditOrCreateBanana_Name">Name:</label>
        <input
            id="EditOrCreateBanana_Name" name="EditOrCreateBanana_Name"
            type="text"
            oninput="document.Bris.UI.EditOrCreateBanana.FormDataChanged(@FormDataChangedArgument);"
            @If Model.Banana IsNot Nothing Then
                @:value="@Model.Banana.Name"
            End If
        />
    </div>
    <span id="EditOrCreateBanana_Name_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-control-group">
        <label for="EditOrCreateBanana_Radiation">Radiation (nSv):</label>
        <input
            id="EditOrCreateBanana_Radiation" name="EditOrCreateBanana_Radiation"
            Type = "text"
            oninput = "document.Bris.UI.EditOrCreateBanana.FormDataChanged(@FormDataChangedArgument);"
            @If Model.Banana IsNot Nothing Then
                @:value="@Model.Banana.Radiation"
            End If
        />
    </div>
    <span id="EditOrCreateBanana_Radiation_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-control-group">
        <label for="EditOrCreateBanana_ManufacturerId">Manufacturer:</label>
        <select
            id="EditOrCreateBanana_ManufacturerId" name="EditOrCreateBanana_ManufacturerId"
            onchange = "document.Bris.UI.EditOrCreateBanana.FormDataChanged(@FormDataChangedArgument);"
        >
            @For Each Manufacturer In Model.AllManufacturers
                @<option
                    value = "@Manufacturer.Id"
                    @If Model.Banana IsNot Nothing AndAlso Manufacturer.Id = Model.Banana.Manufacturer.Id Then
                        @:selected
                    End If
                >
                    @Manufacturer.Name
                </option>
            Next
        </select>
    </div>
    <span id="EditOrCreateBanana_ManufacturerId_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-control-group">
        <label for="EditOrCreateBanana_Barcode">Barcode (optional):</label>
        <input
            id="EditOrCreateBanana_Barcode" name="EditOrCreateBanana_Barcode"
            Type = "text"
            oninput = "document.Bris.UI.EditOrCreateBanana.FormDataChanged(@FormDataChangedArgument);"
            @If Model.Banana IsNot Nothing Then
                @:value="@Model.Banana.Barcode"
            End If
        />
    </div>
    <span id="EditOrCreateBanana_Barcode_ValidationMessage" class="ErrorMessage"></span>

    <div class="pure-controls">
        <button
            id = "EditOrCreateBanana_SubmitButton"
            class="pure-button pure-button-primary"
            @If Model.Banana Is Nothing Then
                @:disabled
            End If
            onclick="document.Bris.UI.EditOrCreateBanana.BeginSave(this,
                @If Model.Banana Is Nothing Then
                    @:null
                Else
                    @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Model.Banana.Id)
                End If
            );"
        >
            Save
        </button><br/>
        <span id="EditOrCreateBanana_Error" class="ErrorMessage"></span><br/>
    </div>
</form>