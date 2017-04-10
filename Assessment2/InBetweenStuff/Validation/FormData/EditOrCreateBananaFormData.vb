
Namespace Validation.FormData

    <Serializable()>
    Public Class EditOrCreateBananaFormData
        Inherits FormData

        Private Shared ReadOnly BarcodeRegex As New System.Text.RegularExpressions.Regex("^[A-Z0-9]{1,}$", RegexOptions.Compiled)

        Public Property ExistingBananaId As Guid? = Nothing
        Public Property Name As String
        Public Property ManufacturerId As Guid
        Public Property RadiationString As String
        Public Property Barcode As String

        Public Overrides Function Validate() As ValidationResult
            Dim Result As New ValidationResult
            Using Entities As New Models.BrisDataEntities
                If ExistingBananaId Is Nothing Then
                    If Entities.Banana.SingleOrDefault(Function(i) i.Name = Name AndAlso i.FK_Manufacturer = ManufacturerId) IsNot Nothing Then
                        Result.AddError("Name", "This banana already exists for this manufacturer.")
                    End If
                End If

                If String.IsNullOrWhiteSpace(Name) Then
                    Result.AddError("Name", "The name must not be empty.")
                End If

                Dim Radiation As Int64
                If Int64.TryParse(RadiationString, Radiation) Then
                    If Radiation = 0 Then
                        Result.AddError("Radiation", "Bananas are radioactive. That's the whole point of this website.")
                    ElseIf Radiation < 0 Then
                        Result.AddError("Radiation", "Wat.")
                    End If
                Else
                    Result.AddError("Radiation", "This is not a number.")
                End If

                If Not String.IsNullOrEmpty(Barcode) AndAlso Not BarcodeRegex.IsMatch(Barcode) Then
                    Result.AddError("Barcode", "This is not a valid barcode. Barcodes only consist of numbers and sometimes uppercase letters.")
                End If
            End Using
            Return Result
        End Function

    End Class

End Namespace
