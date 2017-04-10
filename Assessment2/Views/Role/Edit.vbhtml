@ModelType Assessment2.ViewModel.Role.EditRoleViewModel
@Code
    ViewData("Title") = "Edit"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="CenteredDivOuter">
    <div class="CenteredDivInner Panel">
        <div>
            <table class="pure-table VerticalTableHeader">
                <thead>
                    <tr>
                        <th><span>Name:</span></th>
                        @For Each Permission In Model.AllPermissions
                            @<th><span>@Permission.ToString:</span></th>
                        Next
                        <th><span>Delete:</span></th>
                    </tr>
                </thead>
                @For Each Role In Model.AllRoles
                    @<tr>
                        <td>@Role.Item1.Name</td>
                        @For Each Permission In Model.AllPermissions
                            @Code
                            Dim Permission2 = Permission
                            Dim NameAttributeValue = "Role_" & Role.Item1.Id.ToString & "_Permission"
                            End Code
                            @<td>
                                <input
                                    name="@NameAttributeValue"
                                    value="@DirectCast(Permission, Integer)"
                                    type="checkbox"
                                    onchange="document.Bris.UI.EditRoles.RolePermissionsChanged(this, @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Role.Item1.Id), @DirectCast(Permission, Integer));"
                                    @If Role.item2.Any(Function(i) i.Permission = Permission2) Then
                                        @:checked=""
                                    End If
                                />
                            </td>
                        Next
                        <td>
                            @If Not Assessment2.Models.DefaultRoles.AllDefaultRoles.Contains(Role.Item1.id) Then
                                @<button
                                    class="pure-button pure-button-secondary"
                                    onclick="document.Bris.UI.EditRoles.DeleteRole(this, @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Role.Item1.Id));"
                                >Delete</button>
                            End If
                        </td>
                    </tr>
               Next
            </table><br/>
            <span id="EditRoles_RolePermissionError" class="ErrorMessage"></span>
        </div>
    </div>
</div>
<br />
<div class="CenteredDivOuter">
    <div class="CenteredDivInner Panel">
        <div>
            <table class="pure-table VerticalTableHeader">
                <thead>
                    <tr>
                        <th><span>Name:</span></th>
                        @For Each Role In Model.AllRoles
                            If Role.Item1.Id <> Assessment2.Models.DefaultRoles.Guest Then
                                @<th><span>@Role.Item1.Name</span></th>
                            End If
                        Next
                        <th><span>Delete:</span></th>
                    </tr>
                </thead>
                @For Each iUser In Model.AllUsers
                    @<tr>
                        <td>@iUser.Name</td>
                        @For Each Role In Model.AllRoles
                        If Role.Item1.Id <> Assessment2.Models.DefaultRoles.Guest Then
                            @<td>
                                <input
                                    @Code
                                        @("name=""User_" & iUser.Id.ToString & "_Role""")
                                    End Code
                                    value="@Role.Item1.Id"
                                    Type="radio"
                                    onchange="document.Bris.UI.EditRoles.UserRoleChanged(this, @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(iUser.Id), @Assessment2.Escaping.GuidAsJavascrptFunctionParameterInAttribute(Role.Item1.Id));"
                                    @If Role.Item1.Id = iUser.Role.Id Then
                                        @:checked=""
                                    End If
                                />
                            </td>
                        End If
                        Next
                        <td>
                            <button class="pure-button pure-button-secondary" onclick="alert('Not implemented yet');">Delete</button><br/>
                        </td>
                    </tr>
                Next
            </table><br/>
            <span id="EditRoles_UserRoleError" class="ErrorMessage"></span>
        </div>
    </div>
</div>