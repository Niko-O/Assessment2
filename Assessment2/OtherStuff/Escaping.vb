Public Class Escaping

    Public Shared Function GuidAsJavascrptFunctionParameterInAttribute(Value As Guid) As String
        Return """" & Value.ToString & """"
    End Function

End Class
