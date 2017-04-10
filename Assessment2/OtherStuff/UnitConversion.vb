Public Class UnitConversion

    Private Shared ReadOnly SievertUnits As String() = {"nSv", "µSv", "mSv", "Sv", "kSv", "MSv", "GSv", "TSv"}

    Public Shared Function NanoSievertsToString(NanoSieverts As Int64) As String
        Dim Exponent = CInt(Math.Floor(Math.Log10(NanoSieverts)))
        Dim UnitIndex = Exponent \ 3
        Return (NanoSieverts \ CLng(10 ^ (UnitIndex * 3))) & " " & SievertUnits(UnitIndex)
    End Function

End Class
