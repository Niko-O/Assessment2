Public Class Base16

    Public Shared Function Encode(Bytes As Byte()) As String
        Return BitConverter.ToString(Bytes).Replace("-", "").ToLower
    End Function

    Public Shared Function Decode(Text As String) As Byte()
        If String.IsNullOrEmpty(Text) Then
            Return New Byte() {}
        End If
        If Text.Length Mod 2 <> 0 Then
            Throw New FormatException("The length of text must be a multiple of 2.")
        End If
        Dim Result = New Byte(Text.Length \ 2 - 1) {}
        For i = 0 To Result.Length - 1
            Result(i) = Convert.ToByte(Text.Substring(i * 2, 2), 16)
        Next
        Return Result
    End Function

End Class
