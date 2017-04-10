@ModelType Assessment2.ViewModel.Banana.BananasByUserViewModel
@Code
    ViewData("Title") = "ByUser"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code


<h1>
	@Assessment2.Security.Security.GetLoginData.User.Name's bananas
</h1>
<br/>
@Model.TotalBananasEaten bananas eaten.<br/>
Total radiation ingested: @Assessment2.UnitConversion.NanoSievertsToString(Model.TotalRadiation)<br/>
<table class="pure-table">
	<thead>
		<tr>
			<th>Name</th>
			<th>Manufacturer</th>
			<th>Eaten</th>
			<th>Radiation</th>
			<th>Total radiation</th>
            <th colspan="2" align="center">+/-</th>
		</tr>
	</thead>
	@For Each Tuple In Model.Bananas
	    @<tr>
		    <td>
                @Html.ActionLink(Tuple.Banana.Name, "Show", "Banana", New With {.BananaId = Tuple.Banana.Id}, Nothing)
		    </td>
		    <td>
                @Html.ActionLink(Tuple.Banana.Manufacturer.Name, "Show", "Manufacturer", New With {.ManufacturerId = Tuple.Banana.FK_Manufacturer}, Nothing)
		    </td>
		    <td>
			    @Tuple.Amount
		    </td>
		    <td>
			    @Assessment2.UnitConversion.NanoSievertsToString(Tuple.Banana.Radiation)
		    </td>
		    <td>
			    @Assessment2.UnitConversion.NanoSievertsToString(Tuple.Banana.Radiation * Tuple.Amount)
		    </td>
            <td>
                <button
                    class="pure-button pure-button-secondary"
                    onclick="document.Bris.UI.BananasByUser.BeginAddRemoveBananaConsumption(this, @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Tuple.Banana.Id), 1)"
                >
                    +
                </button>
            </td>
            <td>
                <button
                    class="pure-button pure-button-secondary"
                    onclick="document.Bris.UI.BananasByUser.BeginAddRemoveBananaConsumption(this, @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Tuple.Banana.Id), -1)"
                >
                    -
                </button>
            </td>
	    </tr>
	Next
</table><br/>
<span id="BananasByUser_Error" class="ErrorMessage"></span>
