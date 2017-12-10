Public Class aCarregar

    Private Sub aCarregar_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        JanelaPrincipal.PreencherPlaylist(1)
    End Sub

    Private Sub aCarregar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetTopLevel(True)
    End Sub

    Private Sub aCarregar_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        Me.Focus()
    End Sub
End Class