Public Class AlterarNomePlaylist
    Dim DataTablePlaylist As DataTable
    Public nomeAlterado As String
    Private Sub AlterarNomePlaylist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBoxAlterarPara.Text = ""
        TextBoxNomeActual.Text = JanelaPrincipal.NomeBtnActual

    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click

        If TextBoxAlterarPara.Text <> "" Then
            Dim s As String = TextBoxAlterarPara.Text
            If s.Contains("Lista sem nome") Then
                System.Windows.Forms.MessageBox.Show("Nome inválido! Introduza um nome diferente!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim nomeAux As String = iPlayer_DAL.Playlist.VerificaNomeExistente(TextBoxAlterarPara.Text)
                If nomeAux = "" Then
                    iPlayer_DAL.Playlist.Alterar(TextBoxAlterarPara.Text, JanelaPrincipal.TagBtnActual)
                    nomeAlterado = TextBoxAlterarPara.Text
                    Me.Close()
                Else
                    System.Windows.Forms.MessageBox.Show("Já existe uma lista pessoal com esse nome. Insira um nome diferente.", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Else
            System.Windows.Forms.MessageBox.Show("Introduza um nome antes de confirmar!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        DataTablePlaylist = iPlayer_DAL.Playlist.MostrarUsandoTag2(JanelaPrincipal.TagBtnActual)
        nomeAlterado = DataTablePlaylist.Rows(0).Item("NomeAux")
        Me.Close()
    End Sub
End Class