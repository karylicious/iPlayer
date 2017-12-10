Public Class Acerca
    Dim n1, n2, n3, n4, lb As Integer
    Dim local1 As Integer = 220
    Dim local2 As Integer = 220
    Dim local4 As Integer = 220

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Sub SemNome(ByVal label As Integer)
        Select Case label
            Case 1
                If Not n1 = 80 Then
                    Label3.Visible = True
                    local1 -= 10
                    Label3.Location = New Point(265, local1)
                    n1 = local1
                    If local1 = 190 Then
                        Timer2.Start()
                    End If
                Else
                    Timer1.Stop()
                    Label3.Visible = False
                    local1 = 220
                    n1 = local1

                End If
            Case 2
                If Not n2 = 80 Then
                    Label4.Visible = True
                    Label4.Location = New Point(265, 220)
                    local2 -= 10
                    Label4.Location = New Point(265, local2)
                    n2 = local2
                    If local2 = 190 Then
                        Timer3.Start()
                    End If
                Else
                    Timer2.Stop()
                    Label4.Visible = False
                    local2 = 220
                    n2 = local2

                End If
            Case 3
                If Not n4 = 80 Then
                    Label2.Visible = True
                    Label2.Location = New Point(265, 220)
                    local4 -= 10
                    Label2.Location = New Point(265, local4)
                    n4 = local4
                Else
                    Timer2.Stop()
                    Label2.Visible = False
                    local4 = 220
                    n4 = local4
                    Timer1.Start()
                    Timer3.Stop()
                End If
        End Select
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lb = 1
        SemNome(lb)
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        lb = 2
        SemNome(lb)
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        lb = 3
        SemNome(lb)
    End Sub

End Class