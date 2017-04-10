@ModelType Assessment2.ViewModel.Setup.ShowTableDataViewModel
@Code
    ViewData("Title") = "ShowTableData"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Role</h2>
<table class="pure-table">
    <tr>
        <td>Id</td>
        <td>Name</td>
    </tr>
    @For Each i In Model.Role
        @<tr>
            <td>@i.Id</td>
            <td>@i.Name</td>
        </tr>
    Next
</table>

<h2>RolePermission</h2>
<table class="pure-table">
    <tr>
        <td>Id</td>
        <td>FK_Role</td>
        <td>Permission</td>
    </tr>
    @For Each i In Model.RolePermission
        @<tr>
            <td>@i.Id</td>
            <td>@i.FK_Role</td>
            <td>@i.Permission</td>
        </tr>
    Next
</table>

<h2>Manufacturer</h2>
<table class="pure-table">
    <tr>
        <td>Id</td>
        <td>Name</td>
    </tr>
    @For Each i In Model.Manufacturer
        @<tr>
            <td>@i.Id</td>
            <td>@i.Name</td>
        </tr>
    Next
</table>

<h2>User</h2>
<table class="pure-table">
    <tr>
        <td>Id</td>
        <td>Name</td>
        <td>Email</td>
        <td>PasswordHash</td>
        <td>FK_Role</td>
    </tr>
    @For Each i In Model.User
        @<tr>
            <td>@i.Id</td>
            <td>@i.Name</td>
            <td>@i.Email</td>
            <td>@i.PasswordHash</td>
            <td>@i.FK_Role</td>
        </tr>
    Next
</table>

<h2>RegistrationProcess</h2>
<table class="pure-table">
    <tr>
        <td>Id</td>
        <td>FK_User</td>
        <td>RandomValue</td>
        <td>UniqueValue</td>
        <td>ExpirationDateTime</td>
    </tr>
    @For Each i In Model.RegistrationProcess
        @<tr>
            <td>@i.Id</td>
            <td>@i.FK_User</td>
            <td>@i.RandomValue</td>
            <td>@i.UniqueValue</td>
            <td>
                @i.ExpirationDateTime.ToShortDateString
                @i.ExpirationDateTime.ToLongTimeString
            </td>
        </tr>
    Next
</table>

<h2>Banana</h2>
<table class="pure-table">
    <tr>
        <td>Id</td>
        <td>Name</td>
    </tr>
    @For Each i In Model.Banana
        @<tr>
            <td>@i.Id</td>
            <td>@i.Name</td>
            <td>@i.FK_Manufacturer</td>
            <td>@i.Radiation</td>
            <td>
                @If i.FK_CreatedBy.HasValue Then
                    @i.FK_CreatedBy.Value.ToString
                Else
                    @:NULL
                End If
            </td>
            <td>
                @If i.Barcode is Nothing Then
                    @:NULL
                Else
                    @i.Barcode
                End If
            </td>
        </tr>
    Next
</table>
