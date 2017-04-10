@ModelType Assessment2.ViewModel.User.UserProfileViewModel
@Code
    ViewData("Title") = "Show"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code


<h1>
    @Model.User.Name
</h1>
<br/>
@If Model.ShowBananasLink Then
    @Html.ActionLink(Model.NumberOfBananas & " bananas eaten.", "ByUser", "Banana", New With {.UserId = Model.User.Id}, Nothing)
Else
    @Model.NumberOfBananas
    @: bananas eaten.
End If
<br />

Total radiation:<br/>
@Assessment2.UnitConversion.NanoSievertsToString(Model.TotalRadiation)<br />
@If Model.ShowUserSettingsLink Then
    @Html.ActionLink("Settings", "Settings", "User", New With {.UserId = Model.User.Id}, New With {.class = "pure-button pure-button-secondary"})
End If
