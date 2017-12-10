Imports System
Imports System.Data
Imports System.Data.OleDb

' Funcoes de Data Abstract Layer


Public Class DAL

    Public Shared Connection As OleDbConnection = Nothing

    'Public Shared Connection As New OleDbConnection( _
    '  "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database L" & _
    '  "ocking Mode=1;Data Source=""" & Application.StartupPath() + "\minhabasedados.mdb"";Mode=S" & _
    '  "hare Deny None;Jet OLEDB:Engine Type=5;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OL" & _
    '  "EDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Pr" & _
    '  "operties=;Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:Encrypt Datab" & _
    '  "ase=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on " & _
    '  "Compact=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1" _
    ')

    ' Load do form principal
    ' CreateConnection(Application.StartupPath, "minhaBasedados.mdb")
    Shared Sub CreateConnection(ByVal dbPath As String, ByVal dbName As String)
        Connection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + _
        dbPath + "\" & dbName)
    End Sub


    Shared Sub OpenConnection()
        If Connection.State <> ConnectionState.Open Then
            Connection.Open()
        End If
    End Sub

    Shared Sub CloseConnection()
        If Connection.State <> ConnectionState.Closed Then
            Connection.Close()
        End If
    End Sub

    Shared Function ExecuteScalar(ByVal SQLText As String, ByRef Params As ArrayList) As String
        Dim cmdSQL As New OleDb.OleDbCommand(SQLText, Connection)
        Try
            If Not (Params Is Nothing) Then
                For Each Param As OleDb.OleDbParameter In Params
                    cmdSQL.Parameters.Add(Param)
                Next
            End If
            OpenConnection()
            Return (cmdSQL.ExecuteScalar)
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            'Return -1
        Finally
            cmdSQL.Dispose()
            CloseConnection()
        End Try
    End Function

    Shared Function ExecuteNonQuery(ByVal SQLText As String, ByRef Params As ArrayList) As Integer
        Dim cmdSQL As New OleDb.OleDbCommand(SQLText, Connection)
        Try
            For Each Param As OleDb.OleDbParameter In Params
                cmdSQL.Parameters.Add(Param)
            Next
            OpenConnection()
            Return CInt(cmdSQL.ExecuteNonQuery)
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            'Return -1
        Finally
            CloseConnection()
            cmdSQL.Dispose()
        End Try
    End Function

    Shared Function ExecuteQuery(ByVal SQLText As String, ByRef Params As ArrayList) As ArrayList
        Dim cmdSQL As New OleDb.OleDbCommand(SQLText, Connection)
        Dim Result As New ArrayList
        Try
            If Not (Params Is Nothing) Then
                For Each Param As OleDb.OleDbParameter In Params
                    cmdSQL.Parameters.Add(Param)
                Next
            End If
            OpenConnection()
            Dim dr As OleDbDataReader = cmdSQL.ExecuteReader
            While dr.Read()
                For i As Integer = 0 To dr.FieldCount - 1
                    Result.Add(dr(i))
                Next
            End While
            Return Result
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            'Return Nothing

        Finally
            cmdSQL.Dispose()
            CloseConnection()
        End Try
    End Function

    Shared Function ExecuteQueryDT(ByVal SQLText As String, ByRef Params As ArrayList) As DataTable
        Dim cmdSQL As New OleDb.OleDbDataAdapter(SQLText, Connection)
        Dim Result As New DataTable
        Try
            If Not (Params Is Nothing) Then
                For Each Param As OleDb.OleDbParameter In Params
                    cmdSQL.SelectCommand.Parameters.Add(Param)
                Next
            End If
            OpenConnection()
            'Result.Clear()
            cmdSQL.AcceptChangesDuringFill = True
            cmdSQL.Fill(Result)
            Return Result
        Catch
            Return Nothing
        Finally
            cmdSQL.Dispose()
            CloseConnection()
        End Try
    End Function

    'Shared Function ExecuteQuery(ByVal SQLText As String, ByRef Params As ArrayList) As DataTable
    '    Dim cmdSQL As New OleDb.OleDbCommand(SQLText, Connection)
    '    Dim Result As New DataTable
    '    Try
    '        For Each Param As OleDb.OleDbParameter In Params
    '            cmdSQL.Parameters.Add(Param)
    '        Next
    '        OpenConnection()
    '        Dim dr As OleDbDataReader = cmdSQL.ExecuteQuery()
    '        If dr.Read() Then
    '            For i As Integer = 0 To dr.FieldCount - 1
    '                Result.Add(dr(i))
    '            Next
    '        End If
    '        Return Result
    '    Catch
    '        Return Nothing
    '    Finally
    '        cmdSQL.Dispose()
    '        CloseConnection()
    '    End Try
    'End Function

    ' Funcoes de Business Layer

    'Procedimento que cria OleDbCommands
    Private Shared Function CreateCommand(ByVal sqlText As String, ByVal params As ArrayList) As OleDbCommand
        Try
            Dim cmd As New OleDbCommand(sqlText, Connection)
            If Not (params Is Nothing) Then
                For Each Param As OleDb.OleDbParameter In params
                    cmd.Parameters.Add(Param)
                Next
            End If
            Return cmd
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Shared Function CreateDataAdapter(ByVal cmdIns As OleDbCommand, ByVal cmdUpd As OleDbCommand, ByVal cmdDel As OleDbCommand, ByVal dataSetTable As String) As OleDbDataAdapter
        Try
            Dim odbDa As New OleDbDataAdapter
            odbDa.AcceptChangesDuringFill = True
            odbDa.ContinueUpdateOnError = True
            odbDa.DeleteCommand = cmdDel
            odbDa.InsertCommand = cmdIns
            odbDa.UpdateCommand = cmdUpd
            odbDa.TableMappings.Add("Table", dataSetTable)
            Return odbDa
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    'Procedimento que cria OleDbParameters
    Private Shared Function CreateParam(ByVal name As String, ByVal type As OleDbType, ByVal value As Object) As OleDbParameter
        Try
            Dim p As New OleDbParameter(name, type)
            If Not (value Is Nothing) Then p.Value = value
            Return p
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
