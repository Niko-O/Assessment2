@ModelType Assessment2.ViewModel.Manufacturer.ShowManufacturerViewModel
@Code
    ViewData("Title") = "Show"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
<div class="Panel BorderedPanel">
    @Model.Manufacturer.Name<br />
    @Model.Bananas.Count bananas in total.<br />
    @If Model.ShowEditManufacturerLink Then
        @Html.ActionLink("Edit", "Edit", "Manufacturer", New With {.ManufacturerId = Model.Manufacturer.Id}, New With {.class = "pure-button pure-button-secondary"})
    End If
    @If Model.ShowDeleteManufacturerLink Then
        @<button
            class="pure-button pure-button-secondary"
            onclick="document.Bris.UI.BrowseBananaManufacturers.BeginDeleteManufacturer(this, @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Model.Manufacturer.Id))"
        >Delete</button>
    End If
    <br />
    @For Each Banana In Model.Bananas
        @<p>@Banana.Name</p>
    Next
</div>
