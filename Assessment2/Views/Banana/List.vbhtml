@ModelType Assessment2.ViewModel.Banana.BananasListViewModel
@Code
    ViewData("Title") = "List"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<table class="pure-table">
    @For Each Tuple In Model.Bananas
        @<tr>
            <td>@Tuple.Banana.Name</td>
            <td>
                @If Model.ShowManufacturerLink Then
                    @Html.ActionLink(Tuple.Banana.Manufacturer.Name, "Show", "Manufacturer", New With {.ManufacturerId = Tuple.Manufacturer.Id}, Nothing)
                Else
                    @Tuple.Banana.Manufacturer.Name
                End If
            </td>
            <td>@Assessment2.UnitConversion.NanoSievertsToString(Tuple.Banana.Radiation)</td>
        </tr>
        @<tr>
            <td>
                @If Model.ShowConsumeBananaLink Then
                    @<button
                        class="pure-button pure-button-secondary"
                        onclick="document.Bris.UI.BrowseBananas.BeginAddBananaConsumption(this, @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Tuple.Banana.Id))"
                    >I ate this</button>
                End If
            </td>
            <td>
                @If Model.ShowEditBananaLink Then
                    @Html.ActionLink("Edit", "Edit", "Banana", New With {.BananaId = Tuple.Banana.Id}, New With {.class = "pure-button pure-button-secondary"})
                End If
            </td>
            <td>
                @If Model.ShowDeleteBananaLink Then
                    @<button
                        class="pure-button pure-button-secondary"
                        onclick="document.Bris.UI.BrowseBananas.BeginDeleteBanana(this, @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Tuple.Banana.Id))"
                    >Delete</button>
                End If
            </td>
        </tr>
        @<tr>
            <td colspan="3">
                <hr/>
            </td>
        </tr>
    Next
</table><br/>
@If Model.ShowAddBananaLink Then
    @Html.ActionLink("Add Banana", "Add", "Banana", Nothing, New With {.class = "pure-button pure-button-secondary"})    
End If