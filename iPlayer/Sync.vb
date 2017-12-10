Imports System.Management
Public Class FormSync
    'Private WithEvents EventWatcher As ManagementEventWatcher

    'Private Sub FormSync_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    StartWatcher()
    'End Sub

    'Public Sub StartWatcher()
    '    Dim _Query As New WqlEventQuery("select * from __InstanceOperationEvent within 1 where TargetInstance ISA 'Win32_LogicalDisk'")
    '    EventWatcher = New ManagementEventWatcher(_Query)
    '    Try
    '        EventWatcher.Start()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub DeviceEvent(ByVal sender As Object, ByVal e As System.Management.EventArrivedEventArgs) Handles EventWatcher.EventArrived   
    '    If CType(e.NewEvent, ManagementBaseObject)("__Class").ToString = "__InstanceDeletionEvent" Then
    '        'Sync = False
    '        Me.Close()
    '    End If
    'End Sub

  
    Private Sub FormSync_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        Me.Focus()
    End Sub
End Class