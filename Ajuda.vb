Public Class Ajuda

    Private Sub Ajuda_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AxAcroPDF1.src = Application.StartupPath & "\Manual de Utilizador do iPlayer.pdf"
    End Sub
End Class