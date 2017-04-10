@ModelType Assessment2.ViewModel.Manufacturer.ManufacturersListViewModel
@Code
    ViewData("Title") = "List"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@For Each ManufacturerInfo In Model.Manufacturers
    @<div class="Panel BorderedPanel">
        @ManufacturerInfo.Manufacturer.Name<br />
        @If Model.ShowEditManufacturerLink Then
            @Html.ActionLink("Edit", "Edit", "Manufacturer", New With {.ManufacturerId = ManufacturerInfo.Manufacturer.Id}, New With {.class = "pure-button pure-button-secondary"})
        End If
        @If ManufacturerInfo.ShowDeleteManufacturerLink Then
            @<button
                class="pure-button pure-button-secondary"
                onclick="document.Bris.UI.BrowseBananaManufacturers.BeginDeleteManufacturer(this, @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(ManufacturerInfo.Manufacturer.Id))"
            >Delete</button>
        End If
    </div>
Next