$(document).ready(function()
{

    document.Bris = document.Bris || {};

    document.Bris.JsonRequest = function(RequestName, PostParameters, Callback)
    {
        var Url = "/Ajax/" + RequestName;
        //noinspection JSUnusedGlobalSymbols
        $.ajax
        (
            {
                type: "POST",
                contentType: "application/json",
                url: Url,
                async: true,
                data: JSON.stringify(PostParameters),
                processData: false,
                dataType: "text",
                error: function(Request, Message, ThrownError)
                {
                    Callback(false, null);
                    if (Request.status == 200 && Message == "parsererror")
                    {
                        alert("JsonRequest.Error: " + ThrownError);
                    }
                    else
                    {
                        alert("JsonRequest.Error: " + Message + ", " + ThrownError);
                    }
                },
                success: function(Data)
                {
                    //for (var i = 0; i < 1000000000; i++);

                    var Success = false;
                    var JsonData = null;

                    // This line just helps IntelliJ. It doesn't actually do anything.
                    if (0 != 0) JsonData = {Success: false, Data: null, ErrorMessage: ""};

                    try
                    {
                        JsonData = $.parseJSON(Data);
                        Success = true;
                    }
                    catch (e)
                    {
                        Success = false;
                    }
                    if (Success)
                    {
                        if (Callback != null)
                        {
                            Callback(true, JsonData);
                        }
                    }
                    else
                    {
                        Callback(false, null);
                        console.log("JsonRequest.JsonParseError");
                        console.error(Data);
                        alert("JsonRequest.JsonParseError: " + Data);
                    }
                }
            }
        );
    };

    //region "PageInteraction"

        document.Bris.GoBack = function()
        {
            window.history.back();
        };

        document.Bris.ReloadCurrentPage = function()
        {
            location.reload(true);
        };

        document.Bris.RedirectToIndex = function()
        {
            document.Bris.RedirectTo("/Index/Index");
        };

        document.Bris.RedirectTo = function(PageUrl)
        {
            window.location.replace(PageUrl);
        };

    //endregion

    //region "Functional"

        document.Bris.Functional = document.Bris.Functional || {};

        document.Bris.Functional.ObjectKeys = function(Object)
        {
            var Result = [];
            for (var Key in Object)
            {
                if (Object.hasOwnProperty(Key))
                {
                    Result.push(Key);
                }
            }
            return Result;
        };

        document.Bris.Functional.Where = function(Items, Predicate)
        {
            var Result = [];
            for (var i = 0; i < Items.length; i++)
            {
                if (Predicate(Items[i]))
                {
                    Result.push(Items[i]);
                }
            }
            return Result;
        };

        document.Bris.Functional.SingleOrDefault = function(Items, DefaultValue, Predicate)
        {
            for (var i = 0; i < Items.length; i++)
            {
                if (Predicate(Items[i]))
                {
                    return Items[i];
                }
            }
            return DefaultValue;
        };

        document.Bris.Functional.Select = function(Items, Selector)
        {
            var Result = [];
            for (var i = 0; i < Items.length; i++)
            {
                Result.push(Selector(Items[i]));
            }
            return Result;
        };

        document.Bris.Functional.ForEach = function(Items, Action)
        {
            for (var i = 0; i < Items.length; i++)
            {
                Action(Items[i], i);
            }
        };

    //endregion

    //region "Tasks"

        document.Bris.Tasks = document.Bris.Tasks || {};

        document.Bris.Tasks.RunningTasks = [];

        document.Bris.Tasks.BeginTask = function(DisplayElement)
        {
            var NewTask = {
                DisplayElement: DisplayElement,
                OriginalText: DisplayElement.innerText
            };
            DisplayElement.innerText = "Loading...";
            DisplayElement.setAttribute("disabled", "");
            document.Bris.Tasks.RunningTasks.push(NewTask);
            return NewTask;
        };

        document.Bris.Tasks.EndTask = function(TaskReference)
        {
            var Index = -1;
            for (var i = 0; i < document.Bris.Tasks.RunningTasks.length; i++)
            {
                if (document.Bris.Tasks.RunningTasks[i] === TaskReference)
                {
                    Index = i;
                    break;
                }
            }
            if (Index === -1)
            {
                throw new Error("There is no task with this token running.");
            }
            document.Bris.Tasks.RunningTasks.splice(Index, 1);
            TaskReference.DisplayElement.innerText = TaskReference.OriginalText;
            TaskReference.DisplayElement.removeAttribute("disabled");
        };

    //endregion

    //region "Validation

        document.Bris.Validation = document.Bris.Validation || {};

        document.Bris.Validation.CurrentlyRunningValidation = null;
        document.Bris.Validation.DeferredValidationInput = null;

        document.Bris.Validation.ValidateFormData = function(FormName, FormData, IgnoreEmptyInputsForErrorMessages, Callback)
        {
            //console.log("ValidateFormDataCalled. CurrentlyRunningValidation: " + document.Bris.Validation.CurrentlyRunningValidation);
            function DelayedValidate()
            {
                //console.log("Validation triggered.");
                /** @return bool */
                function CanBeConsideredAnEmptyValue(Value)
                {
                    return Value === null ||
                           Value === "";
                }

                document.Bris.JsonRequest
                (
                    "ValidateFormData",
                    {
                        FormName: FormName,
                        FormData: JSON.stringify(FormData)
                    },
                    function(Valid, Response)
                    {
                        // Helper statement for IntelliJ Idea.
                        if (0 != 0) Response.Data = {IsValid: false, ValidationErrors: [{FieldName: "", ErrorMessage: ""}], GeneralValidationErrorMessage: ""};

                        if (Valid)
                        {
                            var Errors = {};
                            document.Bris.Functional.ForEach(document.Bris.Functional.ObjectKeys(FormData), function(Key)
                            {
                                var Error = null;
                                if (Response.Success && (!IgnoreEmptyInputsForErrorMessages || !CanBeConsideredAnEmptyValue(FormData[Key])))
                                {
                                    if (Response.Data.ValidationErrors !== null)
                                    {
                                        var ErrorsForThisKey = document.Bris.Functional.Where(Response.Data.ValidationErrors, function(i) { return i.FieldName === Key; });
                                        Error = ErrorsForThisKey.length === 0 ? null : document.Bris.Functional.Select(ErrorsForThisKey, function(i) { return i.ErrorMessage; }).join("\r\n");
                                    }
                                }
                                Errors[Key] = Error;
                            });
                            if (Response.Success)
                            {
                                Callback(Response.Data.IsValid, Errors, Response.Data.GeneralValidationErrorMessage);
                            }
                            else
                            {
                                Callback(false, Errors, null);
                                alert(Response.ErrorMessage);
                            }
                        }
                        document.Bris.Validation.CurrentlyRunningValidation = null;
                        var DeferredData = document.Bris.Validation.DeferredValidationInput;
                        document.Bris.Validation.DeferredValidationInput = null;
                        if (DeferredData === null)
                        {
                            //console.log("Validation reset. No deferred data.");
                        }
                        else
                        {
                            //console.log("Validation reset. Continuing with deferred data.");
                            document.Bris.Validation.ValidateFormData
                            (
                                DeferredData.FormName,
                                DeferredData.FormData,
                                DeferredData.IgnoreEmptyInputsForErrorMessages,
                                DeferredData.Callback
                            );
                        }
                    }
                );
            }
            if (document.Bris.Validation.CurrentlyRunningValidation !== null)
            {
                document.Bris.Validation.DeferredValidationInput =
                {
                    FormName: FormName,
                    FormData: FormData,
                    IgnoreEmptyInputsForErrorMessages: IgnoreEmptyInputsForErrorMessages,
                    Callback: Callback
                };
            }
            else
            {
                document.Bris.Validation.CurrentlyRunningValidation = setTimeout(DelayedValidate, 100);
                //console.log("Start timeout: " + document.Bris.Validation.CurrentlyRunningValidation);
            }
        };

    //endregion

    //region "UI"

        document.Bris.UI = document.Bris.UI || {};

        //region "Input"

            document.Bris.UI.Input = document.Bris.UI.Input || {};

            document.Bris.UI.Input.SetElementEnabled = function(ElementId, IsEnabled)
            {
                var Element = document.getElementById(ElementId);
                if (IsEnabled)
                {
                    Element.removeAttribute("disabled");
                }
                else
                {
                    Element.setAttribute("disabled", "");
                }
            };

            document.Bris.UI.Input.GetTextInputValue = function(InputId)
            {
                return $("#" + InputId).val();
            };

            document.Bris.UI.Input.GetSelectInputValue = function(InputId)
            {
                return $("#" + InputId).val();
            };

            document.Bris.UI.Input.SetLabelText = function(LabelId, Text)
            {
                document.getElementById(LabelId).innerText = Text || "";
            };

            document.Bris.UI.Input.GetCheckBoxGroupValues = function(GroupName)
            {
                var Selector = $("input[name=\"" + GroupName + "\"]:checked");
                var Values = [];
                Selector.each(function(Index, Item)
                {
                    Values.push($(Item).val());
                });
                return Values;
            };

            document.Bris.UI.Input.GetRadioButtonGroupValue = function(GroupName)
            {
                var Selector = $("input[name=\"" + GroupName + "\"]:checked");
                if (Selector.length === 0) return null;
                return Selector.val();
            };

    //endregion

        //region "UserArea"

            document.Bris.UI.UserArea = document.Bris.UI.UserArea || {};

            document.Bris.UI.UserArea.Selectors = {
                UserName: "Login_UserName",
                Password: "Login_Password",
                UserNameValidationMessage: "Login_UserName_ValidationMessage",
                Error: "Login_Error",
                SubmitButton: "Login_SubmitButton"
            };

            document.Bris.UI.UserArea.LoginFormDataChanged = function()
            {
                document.Bris.UI.Input.SetElementEnabled(document.Bris.UI.UserArea.Selectors.SubmitButton, false);
                document.Bris.Validation.ValidateFormData
                (
                    "Login",
                    {
                        UserName: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.UserArea.Selectors.UserName),
                        Password: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.UserArea.Selectors.Password)
                    },
                    true,
                    function(IsValid, Errors, GeneralErrorMessage)
                    {
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.UserArea.Selectors.Error, GeneralErrorMessage);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.UserArea.Selectors.UserNameValidationMessage, Errors.UserName);
                        if (IsValid)
                        {
                            document.Bris.UI.Input.SetElementEnabled(document.Bris.UI.UserArea.Selectors.SubmitButton, true);
                        }
                    }
                );
            };

            document.Bris.UI.UserArea.BeginLogin = function(DisplayElement, RedirectToMainPageInsteadOfReloading)
            {
                var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                document.Bris.JsonRequest
                (
                    "Login",
                    {
                        UserName: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.UserArea.Selectors.UserName),
                        Password: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.UserArea.Selectors.Password)
                    },
                    function(Valid, Response)
                    {
                        if (Valid)
                        {
                            if (Response.Success)
                            {
                                if (RedirectToMainPageInsteadOfReloading)
                                {
                                    document.Bris.RedirectToIndex();
                                }
                                else
                                {
                                    document.Bris.ReloadCurrentPage();
                                }
                            }
                            else
                            {
                                $("#Login_Error").html(Response.ErrorMessage);
                            }
                        }
                        document.Bris.Tasks.EndTask(Task);
                    }
                );
            };

            document.Bris.UI.UserArea.BeginLogout = function(DisplayElement, RedirectToMainPageInsteadOfReloading)
            {
                var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                document.Bris.JsonRequest
                (
                    "Logout",
                    {},
                    function(Valid, Response)
                    {
                        if (Valid)
                        {
                            if (Response.Success)
                            {
                                if (RedirectToMainPageInsteadOfReloading)
                                {
                                    document.Bris.RedirectToIndex();
                                }
                                else
                                {
                                    document.Bris.ReloadCurrentPage();
                                }
                            }
                            else
                            {
                                $("#Login_Error").html(Response.ErrorMessage);
                            }
                        }
                        document.Bris.Tasks.EndTask(Task);
                    }
                );
            };

        //endregion

        //region "BrowseBananas"

            document.Bris.UI.BrowseBananas = document.Bris.UI.BrowseBananas || {};

            document.Bris.UI.BrowseBananas.BeginDeleteBanana = function(DisplayElement, BananaId)
            {
                if(confirm("The banana will be deleted!"))
                {
                    var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                    document.Bris.JsonRequest
                    (
                        "DeleteBanana",
                        {
                            BananaId: BananaId
                        },
                        function(Valid, Response)
                        {
                            if (Valid)
                            {
                                if (!Response.Success)
                                {
                                    alert(Response.ErrorMessage);
                                }
                                document.Bris.Tasks.EndTask(Task);
                            }
                        }
                    );
                }
            };

            document.Bris.UI.BrowseBananas.BeginAddBananaConsumption = function(DisplayElement, BananaId)
            {
                var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                document.Bris.JsonRequest
                (
                    "AddRemoveBananaConsumption",
                    {
                        BananaId: BananaId,
                        Amount: 1
                    },
                    function(Valid, Response)
                    {
                        if (!Response.Success)
                        {
                            alert(Response.ErrorMessage);
                        }
                        document.Bris.Tasks.EndTask(Task);
                    }
                );
            };

        //endregion

        //region "BrowseBananaManufacturers"

            document.Bris.UI.BrowseBananaManufacturers = document.Bris.UI.BrowseBananaManufacturers || {};

            document.Bris.UI.BrowseBananaManufacturers.BeginDeleteManufacturer = function(DisplayElement, ManufacturerId)
            {
                if(confirm("The manufacturer and all associated bananas will be deleted!"))
                {
                    var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                    document.Bris.JsonRequest
                    (
                        "DeleteBananaManufacturer",
                        {
                            ManufacturerId: ManufacturerId
                        },
                        function(Valid, Response)
                        {
                            if (Valid)
                            {
                                if (!Response.Success)
                                {
                                    alert(Response.ErrorMessage);
                                }
                                document.Bris.Tasks.EndTask(Task);
                            }
                        }
                    );
                }
            };

        //endregion

        //region "Register"

            document.Bris.UI.Register = document.Bris.UI.Register || {};

            document.Bris.UI.Register.Selectors = {
                UserName: "Register_UserName",
                Email: "Register_Email",
                Password: "Register_Password",
                ConfirmPassword: "Register_ConfirmPassword",
                UserNameValidationMessage: "Register_UserName_ValidationMessage",
                EmailValidationMessage: "Register_Email_ValidationMessage",
                PasswordValidationMessage: "Register_Password_ValidationMessage",
                ConfirmPasswordValidationMessage: "Register_ConfirmPassword_ValidationMessage",
                Error: "Register_Error",
                SubmitButton: "Register_SubmitButton",
                DebugEmailContent: "Register_DebugEmailContent"
            };

            document.Bris.UI.Register.FormDataChanged = function()
            {
                document.Bris.UI.Input.SetElementEnabled(document.Bris.UI.Register.Selectors.SubmitButton, false);
                document.Bris.Validation.ValidateFormData
                (
                    "RegisterUser",
                    {
                        UserName: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.Register.Selectors.UserName),
                        Email: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.Register.Selectors.Email),
                        Password: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.Register.Selectors.Password),
                        ConfirmPassword: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.Register.Selectors.ConfirmPassword)
                    },
                    true,
                    function(IsValid, Errors, GeneralErrorMessage)
                    {
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.Register.Selectors.Error, GeneralErrorMessage);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.Register.Selectors.UserNameValidationMessage, Errors.UserName);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.Register.Selectors.EmailValidationMessage, Errors.Email);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.Register.Selectors.PasswordValidationMessage, Errors.Password);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.Register.Selectors.ConfirmPasswordValidationMessage, Errors.ConfirmPassword);
                        if (IsValid)
                        {
                            document.Bris.UI.Input.SetElementEnabled(document.Bris.UI.Register.Selectors.SubmitButton, true);
                        }
                    }
                );
            };

            document.Bris.UI.Register.BeginRegister = function(DisplayElement)
            {
                var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                document.Bris.JsonRequest
                (
                    "RegisterUser",
                    {
                        UserName: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.Register.Selectors.UserName),
                        Email: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.Register.Selectors.Email),
                        Password: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.Register.Selectors.Password),
                        ConfirmPassword: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.Register.Selectors.ConfirmPassword)
                    },
                    function(Valid, Response)
                    {
                        // Helper statement for IntelliJ Idea.
                        if (0 != 0) Response.Data = {ExpirationDateTime: 0, Debug_Mail: ""};

                        if (Valid)
                        {
                            if (Response.Success)
                            {
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.Register.Selectors.Error, null);
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.Register.Selectors.DebugEmailContent, "The E-Mail content:\n" + Response.Data.Debug_Mail);
                            }
                            else
                            {
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.Register.Selectors.Error, Response.ErrorMessage);
                            }
                            document.Bris.Tasks.EndTask(Task);
                        }
                    }
                );
            };

        //endregion

        //region "EditRoles"

            document.Bris.UI.EditRoles = document.Bris.UI.EditRoles || {};

            document.Bris.UI.EditRoles.Selectors = {
                RolePermissionError: "EditRoles_RolePermissionError",
                UserRoleError: "EditRoles_UserRoleError"
            };

            document.Bris.UI.EditRoles.RolePermissionsChanged = function(DisplayElement, RoleId, Permission)
            {
                var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                var SelectedPermissions = document.Bris.UI.Input.GetCheckBoxGroupValues("Role_" + RoleId + "_Permission");
                document.Bris.JsonRequest
                (
                    "UpdateRolePermissions",
                    {
                        RoleId: RoleId,
                        Permissions: SelectedPermissions
                    },
                    function(Valid, Response)
                    {
                        if (Valid)
                        {
                            if (Response.Success)
                            {
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditRoles.Selectors.RolePermissionError, null);
                            }
                            else
                            {
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditRoles.Selectors.RolePermissionError, Response.ErrorMessage);
                            }
                            document.Bris.Tasks.EndTask(Task);
                        }
                    }
                );
            };

            document.Bris.UI.EditRoles.UserRoleChanged = function(DisplayElement, UserId)
            {
                var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                var SelectedRole = document.Bris.UI.Input.GetRadioButtonGroupValue("User_" + UserId + "_Role");
                document.Bris.JsonRequest
                (
                    "UpdateUserRole",
                    {
                        UserId: UserId,
                        RoleId: SelectedRole
                    },
                    function(Valid, Response)
                    {
                        if (Valid)
                        {
                            if (Response.Success)
                            {
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditRoles.Selectors.UserRoleError, null);
                            }
                            else
                            {
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditRoles.Selectors.UserRoleError, Response.ErrorMessage);
                            }
                            document.Bris.Tasks.EndTask(Task);
                        }
                    }
                );
            };

            document.Bris.UI.EditRoles.DeleteRole = function(DisplayElement, RoleId)
            {
                var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                document.Bris.JsonRequest
                (
                    "RoleHasUsers",
                    {
                        RoleId: RoleId
                    },
                    function(Valid, Response)
                    {
                        // Helper statement for IntelliJ Idea.
                        if (0 != 0) Response.Data = false;

                        if (Valid)
                        {
                            if (Response.Success)
                            {
                                if (!Response.Data || confirm("This role is assigned to at least one user. By deleting it those users' roles will be set to the 'Registered User' default role."))
                                {
                                    document.Bris.JsonRequest
                                    (
                                        "DeleteRole",
                                        {
                                            RoleId: RoleId
                                        },
                                        function(Valid, Response)
                                        {
                                            if (Valid)
                                            {
                                                if (Response.Success)
                                                {
                                                    document.Bris.ReloadCurrentPage();
                                                }
                                                else
                                                {
                                                    document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditRoles.Selectors.RolePermissionError, Response.ErrorMessage);
                                                }
                                                document.Bris.Tasks.EndTask(Task);
                                            }
                                        }
                                    );
                                }
                            }
                            else
                            {
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditRoles.Selectors.RolePermissionError, Response.ErrorMessage);
                                document.Bris.Tasks.EndTask(Task);
                            }
                        }
                    }
                );
            };

        //endregion

        //region "BananasByUser"

            document.Bris.UI.BananasByUser = document.Bris.UI.BananasByUser || {};

            document.Bris.UI.BananasByUser.Selectors = {
                Error: "BananasByUser_Error"
            };

            document.Bris.UI.BananasByUser.BeginAddRemoveBananaConsumption = function(DisplayElement, BananaId, Amount)
            {
                var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                document.Bris.JsonRequest
                (
                    "AddRemoveBananaConsumption",
                    {
                        BananaId: BananaId,
                        Amount: Amount
                    },
                    function(Valid, Response)
                    {
                        if (Valid)
                        {
                            if (Response.Success)
                            {
                                document.Bris.ReloadCurrentPage();
                            }
                            else
                            {
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.Register.Selectors.Error, Response.ErrorMessage);
                            }
                            document.Bris.Tasks.EndTask(Task);
                        }
                    }
                );
            };

        //endregion

        //region "EditOrCreateBanana"

            document.Bris.UI.EditOrCreateBanana = document.Bris.UI.EditOrCreateBanana || {};

            document.Bris.UI.EditOrCreateBanana.Selectors = {
                Name: "EditOrCreateBanana_Name",
                ManufacturerId: "EditOrCreateBanana_ManufacturerId",
                Radiation: "EditOrCreateBanana_Radiation",
                Barcode: "EditOrCreateBanana_Barcode",
                NameValidationMessage: "EditOrCreateBanana_Name_ValidationMessage",
                ManufacturerIdValidationMessage: "EditOrCreateBanana_ManufacturerId_ValidationMessage",
                RadiationValidationMessage: "EditOrCreateBanana_Radiation_ValidationMessage",
                BarcodeValidationMessage: "EditOrCreateBanana_Barcode_ValidationMessage",
                SubmitButton: "EditOrCreateBanana_SubmitButton",
                Error: "EditOrCreateBanana_Error"
            };

            document.Bris.UI.EditOrCreateBanana.FormDataChanged = function(ExistingBananaId)
            {
                document.Bris.UI.Input.SetElementEnabled(document.Bris.UI.EditOrCreateBanana.Selectors.SubmitButton, false);
                document.Bris.Validation.ValidateFormData
                (
                    "EditOrCreateBanana",
                    {
                        BananaId: ExistingBananaId,
                        Name: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditOrCreateBanana.Selectors.Name),
                        ManufacturerId: document.Bris.UI.Input.GetSelectInputValue(document.Bris.UI.EditOrCreateBanana.Selectors.ManufacturerId),
                        Radiation: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditOrCreateBanana.Selectors.Radiation),
                        Barcode: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditOrCreateBanana.Selectors.Barcode)
                    },
                    true,
                    function(IsValid, Errors, GeneralErrorMessage)
                    {

                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditOrCreateBanana.Selectors.NameValidationMessage, Errors.Name);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditOrCreateBanana.Selectors.ManufacturerIdValidationMessage, Errors.ManufacturerId);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditOrCreateBanana.Selectors.RadiationValidationMessage, Errors.Radiation);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditOrCreateBanana.Selectors.BarcodeValidationMessage, Errors.Barcode);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditOrCreateBanana.Selectors.Error, GeneralErrorMessage);
                        if (IsValid)
                        {
                            document.Bris.UI.Input.SetElementEnabled(document.Bris.UI.EditOrCreateBanana.Selectors.SubmitButton, true);
                        }
                    }
                );
            };

            document.Bris.UI.EditOrCreateBanana.BeginSave = function(DisplayElement, ExistingBananaId)
            {
                var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                document.Bris.JsonRequest
                (
                    "EditOrCreateBanana",
                    {
                        BananaId: ExistingBananaId,
                        Name: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditOrCreateBanana.Selectors.Name),
                        ManufacturerId: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditOrCreateBanana.Selectors.ManufacturerId),
                        Radiation: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditOrCreateBanana.Selectors.Radiation),
                        Barcode: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditOrCreateBanana.Selectors.Barcode)
                    },
                    function(Valid, Response)
                    {
                        if (Valid)
                        {
                            if (Response.Success)
                            {
                                document.Bris.RedirectTo("/Banana/List");
                            }
                            else
                            {
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditOrCreateBanana.Selectors.Error, Response.ErrorMessage);
                            }
                            document.Bris.Tasks.EndTask(Task);
                        }
                    }
                );
            };

        //endregion


        //region "EditUserSettings"

            document.Bris.UI.EditUserSettings = document.Bris.UI.EditUserSettings || {};
        
            document.Bris.UI.EditUserSettings.Selectors = {
                UserName: "EditUserSettings_UserName",
                Email: "EditUserSettings_Email",
                Password: "EditUserSettings_Password",
                ConfirmPassword: "EditUserSettings_ConfirmPassword",
                UserNameValidationMessage: "EditUserSettings_UserName_ValidationMessage",
                EmailValidationMessage: "EditUserSettings_Email_ValidationMessage",
                PasswordValidationMessage: "EditUserSettings_Password_ValidationMessage",
                ConfirmPasswordValidationMessage: "EditUserSettings_ConfirmPassword_ValidationMessage",
                Error: "EditUserSettings_Error",
                SubmitButton: "EditUserSettings_SubmitButton"
            };
        
            document.Bris.UI.EditUserSettings.FormDataChanged = function(UserId)
            {
                document.Bris.UI.Input.SetElementEnabled(document.Bris.UI.EditUserSettings.Selectors.SubmitButton, false);
                document.Bris.Validation.ValidateFormData
                (
                    "EditUserSettings",
                    {
                        UserId: UserId,
                        UserName: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditUserSettings.Selectors.UserName),
                        Email: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditUserSettings.Selectors.Email),
                        Password: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditUserSettings.Selectors.Password),
                        ConfirmPassword: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditUserSettings.Selectors.ConfirmPassword)
                    },
                    true,
                    function(IsValid, Errors, GeneralErrorMessage)
                    {
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditUserSettings.Selectors.Error, GeneralErrorMessage);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditUserSettings.Selectors.UserNameValidationMessage, Errors.UserName);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditUserSettings.Selectors.EmailValidationMessage, Errors.Email);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditUserSettings.Selectors.PasswordValidationMessage, Errors.Password);
                        document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditUserSettings.Selectors.ConfirmPasswordValidationMessage, Errors.ConfirmPassword);
                        if (IsValid)
                        {
                            document.Bris.UI.Input.SetElementEnabled(document.Bris.UI.EditUserSettings.Selectors.SubmitButton, true);
                        }
                    }
                );
            };
        
            document.Bris.UI.EditUserSettings.BeginSave = function(DisplayElement, UserId)
            {
                var Task = document.Bris.Tasks.BeginTask(DisplayElement);
                document.Bris.JsonRequest
                (
                    "EditUserSettings",
                    {
                        UserId: UserId,
                        UserName: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditUserSettings.Selectors.UserName),
                        Email: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditUserSettings.Selectors.Email),
                        Password: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditUserSettings.Selectors.Password),
                        ConfirmPassword: document.Bris.UI.Input.GetTextInputValue(document.Bris.UI.EditUserSettings.Selectors.ConfirmPassword)
                    },
                    function(Valid, Response)
                    {
                        if (Valid)
                        {
                            if (Response.Success)
                            {
                                document.Bris.RedirectTo("/User/Show?UserId=" + UserId)
                            }
                            else
                            {
                                document.Bris.UI.Input.SetLabelText(document.Bris.UI.EditUserSettings.Selectors.Error, Response.ErrorMessage);
                            }
                            document.Bris.Tasks.EndTask(Task);
                        }
                    }
                );
            };

        //endregion

    //endregion

});