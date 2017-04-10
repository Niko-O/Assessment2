@Code
    Dim LoginData = Assessment2.Security.Security.GetLoginData()
End Code
<!DOCTYPE html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/Content/js")
</head>
<body>
    <div id="Header">
        <div class="pure-menu SiteMap">
            <ul class="Panel pure-menu-list">
                <li class="pure-menu-item">
                    @Html.ActionLink("Main Page", "Index", "Index", Nothing, New With {.class = "pure-menu-link"})
                </li>
                @If LoginData.IsLoggedIn Then
                    @<li class="pure-menu-item">
                        @Html.ActionLink("My profile", "Show", "User", New With {.UserId = LoginData.User.id}, New With {.class = "pure-menu-link"})
                    </li>
                    @<li class="pure-menu-item">
                        @Html.ActionLink("My bananas", "ByUser", "Banana", New With {.UserId = LoginData.User.id}, New With {.class = "pure-menu-link"})
                    </li>
                End If
                @If LoginData.HasPermission(Assessment2.Models.Permission.ViewBanana) Then
                    @<li class="pure-menu-item">
                        @Html.ActionLink("Browse bananas", "List", "Banana", Nothing, New With {.class = "pure-menu-link"})
                    </li>
                End If
                @If LoginData.HasPermission(Assessment2.Models.Permission.ViewManufacturer) Then
                    @<li class="pure-menu-item">
                        @Html.ActionLink("Browse manufacturers", "List", "Manufacturer", Nothing, New With {.class = "pure-menu-link"})
                    </li>
                End If
                @If LoginData.HasPermission(Assessment2.Models.Permission.EditRoles) Then
                    @<li class="pure-menu-item">
                        @Html.ActionLink("Edit Users and Roles", "Edit", "Role", Nothing, New With {.class = "pure-menu-link"})
                    </li>
                End If
            </ul>
        </div>
        <div>
            @If LoginData.IsLoggedIn Then
                @<div class="UserArea Panel">
                    Hi, @Html.Encode(LoginData.User.Name).<br />
                    <button
                        id="LogoutButton"
                        class="pure-button pure-button-secondary"
                        onclick="document.Bris.UI.UserArea.BeginLogout(this, 
                            @If ViewBag.RedirectToMainPageInsteadOfReloadingOnLogout Then
                                @:true
                            Else
                                @:false
                            End If
                        );"
                    >
                        Logout</button>
                </div>
            Else
                @<div class="UserArea Panel pure-form pure-form-aligned">
                    <form action="#" onsubmit="return false;">
                    <div class="pure-control-group">
                        <label for="Login_UserName">
                            Benutzername:
                        </label>
                        <input id="Login_UserName" name="Login_UserName" type="text" oninput="document.Bris.UI.UserArea.LoginFormDataChanged();" />
                    </div>
                    <span id="Login_UserName_ValidationMessage" class="ErrorMessage"></span>
                    <div class="pure-control-group">
                        <label for="Login_Password">
                            Passwort:
                        </label>
                        <input id="Login_Password" name="Password" type="password" />
                    </div>
                    <div class="pure-controls">
                        <button
                            id="Login_SubmitButton"
                            class="pure-button pure-button-primary"
                            onclick="document.Bris.UI.UserArea.BeginLogin(this,
                                @If ViewBag.RedirectToMainPageInsteadOfReloadingOnLogin Then
                                    @:true
                                Else
                                    @:false
                                End If
                            );"
                            disabled="disabled">
                            Login
                        </button>
                        @Html.ActionLink("Register", "Register", "User", Nothing, New With {.class = "pure-button pure-button-secondary"})
                    </div>
                    <span id="Login_Error" class="ErrorMessage"></span>
                    </form>
                </div>
            End If
        </div>
    </div>
    <div id="MainContent">
        <div class="CenteredDivOuter">
            <div class="CenteredDivInner Panel">
                @RenderBody()
            </div>
        </div>
    </div>
    <div id="Footer">
        Room for Impressum and stuff
    </div>
</body>
</html>
