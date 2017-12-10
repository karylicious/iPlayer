Imports System
Imports System.Data
Imports System.Data.OleDb

' Classe das regras de negocio
Public Class iPlayer_DAL

    Public Class Musica

        Shared Sub Adicionar(ByVal NumFormato As Integer, ByVal NomeMusica As String, ByVal Ano As Integer, ByVal Duracao As String, ByVal DataAdicao As Date, ByVal Url As String, ByVal Tamanho As String, ByVal TaxaBits As Integer, ByVal Frequencia As String, ByVal Copyright As String, ByVal Canal As String, ByVal Comentarios As String)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()


                parametro = New OleDbParameter("NumFormato", OleDb.OleDbType.Integer)
                parametro.Value = NumFormato
                Parametros.Add(parametro)



                parametro = New OleDbParameter("NomeMusica", OleDb.OleDbType.VarChar)
                parametro.Value = NomeMusica
                Parametros.Add(parametro)


                parametro = New OleDbParameter("Ano", OleDb.OleDbType.Integer)
                parametro.Value = Ano
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Duracao", OleDb.OleDbType.VarChar)
                parametro.Value = Duracao
                Parametros.Add(parametro)


                parametro = New OleDbParameter("DataAdicao", OleDb.OleDbType.Date)
                parametro.Value = DataAdicao
                Parametros.Add(parametro)
                parametro = New OleDbParameter("Url", OleDb.OleDbType.VarChar)
                parametro.Value = Url
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Tamanho", OleDb.OleDbType.VarChar)
                parametro.Value = Tamanho
                Parametros.Add(parametro)

                parametro = New OleDbParameter("TaxaBits", OleDb.OleDbType.Integer)
                parametro.Value = TaxaBits
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Frequencia", OleDb.OleDbType.VarChar)
                parametro.Value = Frequencia
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Copyright", OleDb.OleDbType.VarChar)
                parametro.Value = Copyright
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Canal", OleDb.OleDbType.VarChar)
                parametro.Value = Canal
                Parametros.Add(parametro)


                parametro = New OleDbParameter("Comentarios", OleDb.OleDbType.VarChar)
                parametro.Value = Comentarios
                Parametros.Add(parametro)




                DAL.ExecuteNonQuery("Insert into Musica ( NumFormato,NomeMusica, Ano, Duracao, DataAdicao, Url, Tamanho, TaxaBits, Frequencia, Copyright, Canal, Comentarios ) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)", Parametros)

                Dim nummusica As Integer = DAL.ExecuteScalar("Select max(nummusica) from Musica", Nothing)

                '--------------------------
                ' Interprete

                Dim numInterprete As Integer = DAL.ExecuteScalar("SELECT MAX(numInterprete) FROM interprete", Nothing)


                Parametros.Clear()

                parametro = New OleDbParameter("numInterprete", OleDb.OleDbType.Integer)
                parametro.Value = numInterprete
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = nummusica
                Parametros.Add(parametro)



                DAL.ExecuteNonQuery("Insert into MusicaInterprete (numInterprete,NumMusica) VALUES (?,?)", Parametros)

                '--------------------------
                ' Album

                Dim numAlbum As Integer = DAL.ExecuteScalar("SELECT MAX(numAlbum) FROM Album", Nothing)


                Parametros.Clear()

                parametro = New OleDbParameter("numAlbum", OleDb.OleDbType.Integer)
                parametro.Value = numAlbum
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = nummusica
                Parametros.Add(parametro)



                DAL.ExecuteNonQuery("Insert into MusicaAlbum (numAlbum,NumMusica) VALUES (?,?)", Parametros)


                '--------------------------
                ' Genero

                Dim numGenero As Integer = DAL.ExecuteScalar("SELECT MAX(numGenero) FROM Genero", Nothing)


                Parametros.Clear()

                parametro = New OleDbParameter("numGenero", OleDb.OleDbType.Integer)
                parametro.Value = numGenero
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = nummusica
                Parametros.Add(parametro)




                DAL.ExecuteNonQuery("Insert into MusicaGenero (numGenero,NumMusica) VALUES (?,?)", Parametros)

                '---------------------------
                ' Playlist Biblioteca

                Parametros.Clear()
                parametro = New OleDbParameter("NumPlaylist", OleDb.OleDbType.Integer)
                parametro.Value = 1
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = nummusica
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Insert into MusicaPlaylist(NumPlaylist,NumMusica) VALUES (?,?)", Parametros)

                '----------------------------
                ' Playlist Acrescentadas Recentemente

                Parametros.Clear()
                parametro = New OleDbParameter("NumPlaylist", OleDb.OleDbType.Integer)
                parametro.Value = 3
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = nummusica
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Insert into MusicaPlaylist(NumPlaylist,NumMusica) VALUES (?,?)", Parametros)

                '----------------------------
                ' Playlist Anos 2000-2010

                If Ano >= 2000 And Ano <= 2010 Then

                    Parametros.Clear()
                    parametro = New OleDbParameter("NumPlaylist", OleDb.OleDbType.Integer)
                    parametro.Value = 4
                    Parametros.Add(parametro)

                    parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                    parametro.Value = nummusica
                    Parametros.Add(parametro)

                    DAL.ExecuteNonQuery("Insert into MusicaPlaylist(NumPlaylist,NumMusica) VALUES (?,?)", Parametros)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        Public Shared Sub Alterar(ByVal NomeMusica As String, ByVal Ano As Integer, ByVal Comentarios As String, ByVal Letra As String, ByVal NumMusica As Integer, ByVal Url As String)

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                ''Inicio do tratatamento de dados
                'Parametros.Clear()

                ''If NomeMusica <> Nothing Then
                'parametro = New OleDbParameter("NomeMusica", OleDb.OleDbType.VarChar)
                'parametro.Value = NomeMusica
                'Parametros.Add(parametro)
                ''Else
                ''    Parametros.Clear()
                ''    parametro = New OleDbParameter("numMusica", OleDb.OleDbType.Integer)
                ''    parametro.Value = NumMusica
                ''    Parametros.Add(parametro)

                ''    NomeMusica = DAL.ExecuteScalar("Select NomeMusica from musica where numMusica=?", Parametros)

                ''End If


                ''If Ano <> Nothing Then
                'Parametros.Clear()
                'parametro = New OleDbParameter("Ano", OleDb.OleDbType.Integer)
                'parametro.Value = Ano
                'Parametros.Add(parametro)
                ''Else
                ''    Parametros.Clear()
                ''    parametro = New OleDbParameter("numMusica", OleDb.OleDbType.Integer)
                ''    parametro.Value = NumMusica
                ''    Parametros.Add(parametro)

                ''    Ano = DAL.ExecuteScalar("Select ano from musica where numMusica=?", Parametros)
                ''    If Ano = Nothing Then
                ''        Ano = 0
                ''    End If
                ''End If


                ''If Faixa <> Nothing Then
                'Parametros.Clear()
                'parametro = New OleDbParameter("Faixa", OleDb.OleDbType.Integer)
                'parametro.Value = Faixa
                'Parametros.Add(parametro)
                ''Else
                ''    Parametros.Clear()
                ''    parametro = New OleDbParameter("numMusica", OleDb.OleDbType.Integer)
                ''    parametro.Value = NumMusica
                ''    Parametros.Add(parametro)

                ''    Faixa = DAL.ExecuteScalar("Select Faixa from musica where numMusica=?", Parametros)
                ''    If Faixa = Nothing Then
                ''        Faixa = 0
                ''    End If
                ''End If

                ''If Comentarios <> Nothing Then
                'Parametros.Clear()
                'parametro = New OleDbParameter("Comentarios", OleDb.OleDbType.VarChar)
                'parametro.Value = Comentarios
                'Parametros.Add(parametro)
                ''Else
                ''    Parametros.Clear()
                ''    parametro = New OleDbParameter("numMusica", OleDb.OleDbType.Integer)
                ''    parametro.Value = NumMusica
                ''    Parametros.Add(parametro)

                ''    Comentarios = DAL.ExecuteScalar("Select Comentarios from musica where numMusica=?", Parametros)
                ''    If Comentarios = Nothing Then
                ''        Comentarios = ""
                ''    End If
                ''End If



                ''If Letra <> Nothing Then
                'Parametros.Clear()
                'parametro = New OleDbParameter("Letra", OleDb.OleDbType.VarChar)
                'parametro.Value = Letra
                'Parametros.Add(parametro)
                ''Else
                ''    Parametros.Clear()
                ''    parametro = New OleDbParameter("numMusica", OleDb.OleDbType.Integer)
                ''    parametro.Value = NumMusica
                ''    Parametros.Add(parametro)

                ''    Letra = DAL.ExecuteScalar("Select Letra from musica where numMusica=?", Parametros)
                ''    If Letra = Nothing Then
                ''        Letra = ""
                ''    End If
                ''End If

                ''If FaixaTotal <> Nothing Then
                'Parametros.Clear()
                'parametro = New OleDbParameter("FaixaTotal", OleDb.OleDbType.Integer)
                'parametro.Value = FaixaTotal
                'Parametros.Add(parametro)
                ''Else
                ''    Parametros.Clear()
                ''    parametro = New OleDbParameter("numMusica", OleDb.OleDbType.Integer)
                ''    parametro.Value = NumMusica
                ''    Parametros.Add(parametro)

                ''    FaixaTotal = DAL.ExecuteScalar("Select FaixaTotal from musica where numMusica=?", Parametros)
                ''    If FaixaTotal = Nothing Then
                ''        FaixaTotal = 0
                ''    End If
                ''End If


                'Parametros.Clear()
                'parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                'parametro.Value = NumMusica
                'Parametros.Add(parametro)

                If Url <> Nothing Then
                    Parametros.Clear()
                    parametro = New OleDbParameter("Url", OleDb.OleDbType.VarChar)
                    parametro.Value = Url
                    Parametros.Add(parametro)
                Else
                    Parametros.Clear()
                    parametro = New OleDbParameter("numMusica", OleDb.OleDbType.Integer)
                    parametro.Value = NumMusica
                    Parametros.Add(parametro)

                    Url = DAL.ExecuteScalar("Select Url from musica where numMusica=?", Parametros)
                End If

                'Fim d0 tratatamento de dados

                Parametros.Clear()

                parametro = New OleDbParameter("NomeMusica", OleDb.OleDbType.VarChar)
                parametro.Value = NomeMusica
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Url", OleDb.OleDbType.VarChar)
                parametro.Value = Url
                Parametros.Add(parametro)


                parametro = New OleDbParameter("Ano", OleDb.OleDbType.Integer)
                parametro.Value = Ano
                Parametros.Add(parametro)


                parametro = New OleDbParameter("Comentarios", OleDb.OleDbType.VarChar)
                parametro.Value = Comentarios
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Letra", OleDb.OleDbType.VarChar)
                parametro.Value = Letra
                Parametros.Add(parametro)


                parametro = New OleDbParameter("numMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)


                DAL.ExecuteNonQuery("Update Musica set NomeMusica=?, Url=?, Ano=?, Comentarios=? , Letra=? WHERE NumMusica=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try



        End Sub

        Public Shared Sub AlterarClassificacao(ByVal NumMusica As Integer, ByVal Classificacao As String)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()
              
                parametro = New OleDbParameter("Classificacao", OleDb.OleDbType.VarChar)
                parametro.Value = Classificacao
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Update Musica set Classificacao=? where nummusica=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Shared Sub AlterarCapa(ByVal NumMusica As Integer, ByVal Capa As String)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()

                parametro = New OleDbParameter("Capa", OleDb.OleDbType.VarChar)
                parametro.Value = Capa
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Update Musica set Capa=? where nummusica=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Shared Sub Remover(ByVal NumMusica As Integer)

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try



                Parametros.Clear()
                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                Dim numinter As Integer = DAL.ExecuteScalar("SELECT numinterprete from musicainterprete where nummusica=?", Parametros)

                Parametros.Clear()
                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                Dim numAlbum As Integer = DAL.ExecuteScalar("SELECT numAlbum from musicaAlbum where nummusica=?", Parametros)

                Parametros.Clear()
                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                Dim numGenero As Integer = DAL.ExecuteScalar("SELECT numGenero from musicaGenero where nummusica=?", Parametros)

                Parametros.Clear()
                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)
                Dim numHist As Integer = DAL.ExecuteScalar("SELECT numHistorico from musicaHistorico where nummusica=?", Parametros)

                Parametros.Clear()
                parametro = New OleDbParameter("Numinterprete", OleDb.OleDbType.Integer)
                parametro.Value = numinter
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Delete from interprete  WHERE Numinterprete=?", Parametros)

                Parametros.Clear()
                parametro = New OleDbParameter("NumAlbum", OleDb.OleDbType.Integer)
                parametro.Value = numAlbum
                Parametros.Add(parametro)
                DAL.ExecuteNonQuery("Delete from Album  WHERE NumAlbum=?", Parametros)


                Parametros.Clear()
                parametro = New OleDbParameter("NumGenero", OleDb.OleDbType.Integer)
                parametro.Value = numGenero
                Parametros.Add(parametro)
                DAL.ExecuteNonQuery("Delete from Genero  WHERE NumGenero=?", Parametros)


                Parametros.Clear()
                parametro = New OleDbParameter("numHistorico", OleDb.OleDbType.Integer)
                parametro.Value = numHist
                Parametros.Add(parametro)
                DAL.ExecuteNonQuery("Delete from Historico  WHERE numHistorico=?", Parametros)


                Parametros.Clear()
                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Delete from Musica  WHERE NumMusica=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Shared Sub RemoverUsandoTag(ByVal Tag As Integer, ByVal nummusica As Integer)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()

                parametro = New OleDbParameter("tag", OleDb.OleDbType.Integer)
                parametro.Value = Tag
                Parametros.Add(parametro)

                Dim numPlaylist As Integer = DAL.ExecuteScalar("SELECT numPlaylist FROM playlist WHERE tag=?", Parametros)


                Parametros.Clear()
                parametro = New OleDbParameter("numPlaylist", OleDb.OleDbType.Integer)
                parametro.Value = numPlaylist
                Parametros.Add(parametro)

                parametro = New OleDbParameter("nummusica", OleDb.OleDbType.Integer)
                parametro.Value = nummusica
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Delete numPlaylist,nummusica from Musicaplaylist  WHERE numPlaylist=? AND nummusica=?", Parametros)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Shared Function MostrarUsandoNumMusica(ByVal NumMusica As Integer) As DataTable


            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                Return DAL.ExecuteQueryDT("SELECT Capa, NomeMusica, NumFormato, Classificacao, Ano,Duracao, DataAdicao, Tamanho, TaxaBits, Frequencia, Copyright, Canal, Comentarios, Letra, url , NomeInterprete, NomeAlbum, NomeGenero, DataAdicao, NumReproducao, UltimaReproducao, Musica.NumMusica FROM (((((((Interprete Left JOIN MusicaInterprete ON musicaInterprete.numinterprete=Interprete.numinterprete) Left JOIN Musica ON musicainterprete.numMusica=Musica.numMusica) Left JOIN MusicaAlbum ON MusicaAlbum.numMusica=musica.numMusica) Left JOIN Album ON musicaAlbum.numAlbum=Album.numAlbum) Left JOIN MusicaGenero ON MusicaGenero.numMusica=musica.numMusica) Left JOIN Genero ON musicaGenero.numGenero=Genero.numGenero) Left JOIN MusicaHistorico ON MusicaHistorico.nummusica=musica.nummusica) Left JOIN Historico ON MusicaHistorico.numhistorico=Historico.numhistorico where Musica.NumMusica=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

        Public Shared Sub ReiniciarContagem(ByVal NumMusica As Integer)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()
                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                Dim NumHistorico As Integer = DAL.ExecuteScalar("Select NumHistorico from MusicaHistorico where numMusica=? ", Parametros)

                Parametros.Clear()

                parametro = New OleDbParameter("NumReproducao", OleDb.OleDbType.Integer)
                parametro.Value = 0
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumHistorico", OleDb.OleDbType.Integer)
                parametro.Value = NumHistorico
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Update historico set NumReproducao=? where numhistorico=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Shared Function Procurar(ByVal Pro As String, ByVal Por As String, ByVal Tag As Integer) As DataTable

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter
            Try
                Parametros.Clear()

                parametro = New OleDbParameter("Tag", OleDb.OleDbType.VarChar)
                parametro.Value = Tag
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Procurar", OleDb.OleDbType.VarChar)
                parametro.Value = "%" & Pro & "%"
                Parametros.Add(parametro)

                Return DAL.ExecuteQueryDT("SELECT Capa, Nomemusica, Duracao,Tamanho, NomeInterprete, NomeAlbum,  Ano, NomeGenero, DataAdicao, NumReproducao, UltimaReproducao,  Classificacao, Musica.NumMusica FROM (((((((((Interprete Left JOIN MusicaInterprete ON musicaInterprete.numinterprete=Interprete.numinterprete) Left JOIN Musica ON musicainterprete.numMusica=Musica.numMusica) Left JOIN MusicaAlbum ON MusicaAlbum.numMusica=musica.numMusica) Left JOIN Album ON musicaAlbum.numAlbum=Album.numAlbum) Left JOIN MusicaGenero ON MusicaGenero.numMusica=musica.numMusica) Left JOIN Genero ON musicaGenero.numGenero=Genero.numGenero) Left JOIN MusicaHistorico ON MusicaHistorico.nummusica=musica.nummusica) Left JOIN Historico ON MusicaHistorico.numhistorico=Historico.numhistorico) left join MusicaPlaylist on MusicaPlaylist.NumMusica=Musica.NumMusica) left join Playlist on MusicaPlaylist.NumPlaylist=Playlist.NumPlaylist WHERE Tag=? and " & Por & " like ?", Parametros)
            Catch ex As Exception

            End Try
        End Function

        Shared Function ProcurarTodos(ByVal Str As String, ByVal Tag As Integer) As DataTable

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter
            Dim datatableAux As DataTable
            Try
                Parametros.Clear()

                parametro = New OleDbParameter("Tag", OleDb.OleDbType.VarChar)
                parametro.Value = Tag
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Procurar", OleDb.OleDbType.VarChar)
                parametro.Value = "%" & Str & "%"
                Parametros.Add(parametro)
                datatableAux = DAL.ExecuteQueryDT("SELECT Capa, Nomemusica, Duracao,Tamanho, NomeInterprete, NomeAlbum,  Ano, NomeGenero, DataAdicao, NumReproducao, UltimaReproducao,  Classificacao, Musica.NumMusica FROM (((((((((Interprete Left JOIN MusicaInterprete ON musicaInterprete.numinterprete=Interprete.numinterprete) Left JOIN Musica ON musicainterprete.numMusica=Musica.numMusica) Left JOIN MusicaAlbum ON MusicaAlbum.numMusica=musica.numMusica) Left JOIN Album ON musicaAlbum.numAlbum=Album.numAlbum) Left JOIN MusicaGenero ON MusicaGenero.numMusica=musica.numMusica) Left JOIN Genero ON musicaGenero.numGenero=Genero.numGenero) Left JOIN MusicaHistorico ON MusicaHistorico.nummusica=musica.nummusica) Left JOIN Historico ON MusicaHistorico.numhistorico=Historico.numhistorico) left join MusicaPlaylist on MusicaPlaylist.NumMusica=Musica.NumMusica) left join Playlist on MusicaPlaylist.NumPlaylist=Playlist.NumPlaylist WHERE Tag=? and NomeMusica like ?", Parametros)
                If datatableAux.Rows.Count <> Nothing Then
                    Return datatableAux
                    Exit Function
                Else
                    Parametros.Clear()

                    parametro = New OleDbParameter("Tag", OleDb.OleDbType.VarChar)
                    parametro.Value = Tag
                    Parametros.Add(parametro)

                    parametro = New OleDbParameter("Procurar", OleDb.OleDbType.VarChar)
                    parametro.Value = "%" & Str & "%"
                    Parametros.Add(parametro)
                    datatableAux = DAL.ExecuteQueryDT("SELECT Capa, Nomemusica, Duracao,Tamanho, NomeInterprete, NomeAlbum,  Ano, NomeGenero, DataAdicao, NumReproducao, UltimaReproducao,  Classificacao, Musica.NumMusica FROM (((((((((Interprete Left JOIN MusicaInterprete ON musicaInterprete.numinterprete=Interprete.numinterprete) Left JOIN Musica ON musicainterprete.numMusica=Musica.numMusica) Left JOIN MusicaAlbum ON MusicaAlbum.numMusica=musica.numMusica) Left JOIN Album ON musicaAlbum.numAlbum=Album.numAlbum) Left JOIN MusicaGenero ON MusicaGenero.numMusica=musica.numMusica) Left JOIN Genero ON musicaGenero.numGenero=Genero.numGenero) Left JOIN MusicaHistorico ON MusicaHistorico.nummusica=musica.nummusica) Left JOIN Historico ON MusicaHistorico.numhistorico=Historico.numhistorico) left join MusicaPlaylist on MusicaPlaylist.NumMusica=Musica.NumMusica) left join Playlist on MusicaPlaylist.NumPlaylist=Playlist.NumPlaylist WHERE Tag=? and nomeinterprete like ?", Parametros)
                    If datatableAux.Rows.Count <> Nothing Then
                        Return datatableAux
                        Exit Function
                    Else
                        Parametros.Clear()

                        parametro = New OleDbParameter("Tag", OleDb.OleDbType.VarChar)
                        parametro.Value = Tag
                        Parametros.Add(parametro)

                        parametro = New OleDbParameter("Procurar", OleDb.OleDbType.VarChar)
                        parametro.Value = "%" & Str & "%"
                        Parametros.Add(parametro)
                        datatableAux = DAL.ExecuteQueryDT("SELECT Capa, Nomemusica, Duracao,Tamanho, NomeInterprete, NomeAlbum,  Ano, NomeGenero, DataAdicao, NumReproducao, UltimaReproducao,  Classificacao, Musica.NumMusica FROM (((((((((Interprete Left JOIN MusicaInterprete ON musicaInterprete.numinterprete=Interprete.numinterprete) Left JOIN Musica ON musicainterprete.numMusica=Musica.numMusica) Left JOIN MusicaAlbum ON MusicaAlbum.numMusica=musica.numMusica) Left JOIN Album ON musicaAlbum.numAlbum=Album.numAlbum) Left JOIN MusicaGenero ON MusicaGenero.numMusica=musica.numMusica) Left JOIN Genero ON musicaGenero.numGenero=Genero.numGenero) Left JOIN MusicaHistorico ON MusicaHistorico.nummusica=musica.nummusica) Left JOIN Historico ON MusicaHistorico.numhistorico=Historico.numhistorico) left join MusicaPlaylist on MusicaPlaylist.NumMusica=Musica.NumMusica) left join Playlist on MusicaPlaylist.NumPlaylist=Playlist.NumPlaylist WHERE Tag=? and NomeAlbum like ?", Parametros)
                        If datatableAux.Rows.Count <> Nothing Then
                            Return datatableAux
                            Exit Function
                        Else
                            Parametros.Clear()

                            parametro = New OleDbParameter("Tag", OleDb.OleDbType.VarChar)
                            parametro.Value = Tag
                            Parametros.Add(parametro)

                            parametro = New OleDbParameter("Procurar", OleDb.OleDbType.VarChar)
                            parametro.Value = "%" & Str & "%"
                            Parametros.Add(parametro)
                            datatableAux = DAL.ExecuteQueryDT("SELECT Capa, Nomemusica, Duracao,Tamanho, NomeInterprete, NomeAlbum,  Ano, NomeGenero, DataAdicao, NumReproducao, UltimaReproducao,  Classificacao, Musica.NumMusica FROM (((((((((Interprete Left JOIN MusicaInterprete ON musicaInterprete.numinterprete=Interprete.numinterprete) Left JOIN Musica ON musicainterprete.numMusica=Musica.numMusica) Left JOIN MusicaAlbum ON MusicaAlbum.numMusica=musica.numMusica) Left JOIN Album ON musicaAlbum.numAlbum=Album.numAlbum) Left JOIN MusicaGenero ON MusicaGenero.numMusica=musica.numMusica) Left JOIN Genero ON musicaGenero.numGenero=Genero.numGenero) Left JOIN MusicaHistorico ON MusicaHistorico.nummusica=musica.nummusica) Left JOIN Historico ON MusicaHistorico.numhistorico=Historico.numhistorico) left join MusicaPlaylist on MusicaPlaylist.NumMusica=Musica.NumMusica) left join Playlist on MusicaPlaylist.NumPlaylist=Playlist.NumPlaylist WHERE Tag=? and NomeGenero like ?", Parametros)
                            If datatableAux.Rows.Count <> Nothing Then
                                Return datatableAux
                                Exit Function
                            Else
                                Return Nothing
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
        End Function

        Public Shared Sub AlterarDuracao(ByVal NumMusica As Integer, ByVal Duracao As String)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()

                parametro = New OleDbParameter("Duracao", OleDb.OleDbType.VarChar)
                parametro.Value = Duracao
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Update Musica set Duracao=? where nummusica=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub
    End Class

    Public Class Playlist

        Shared Sub Adicionar(ByVal NomePlaylist As String, ByVal Tag As Integer, ByVal TagContextMenuStrip As Integer, ByVal NomeAux As String)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()

                parametro = New OleDbParameter("NomePlaylist", OleDb.OleDbType.VarChar)
                parametro.Value = NomePlaylist
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Tag", OleDb.OleDbType.Integer)
                parametro.Value = Tag
                Parametros.Add(parametro)


                parametro = New OleDbParameter("TagContextMenuStrip", OleDb.OleDbType.Integer)
                parametro.Value = TagContextMenuStrip
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NomeAux", OleDb.OleDbType.VarChar)
                parametro.Value = NomeAux
                Parametros.Add(parametro)


                DAL.ExecuteNonQuery("Insert into Playlist (NomePlaylist, Tag, TagContextMenuStrip,NomeAux) VALUES (?,?,?,?)", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        Public Shared Sub Alterar(ByVal NomeAux As String, ByVal Tag As Integer)

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()

                parametro = New OleDbParameter("NomeAux", OleDb.OleDbType.VarChar)
                parametro.Value = NomeAux
                Parametros.Add(parametro)

                parametro = New OleDbParameter("Tag", OleDb.OleDbType.Integer)
                parametro.Value = Tag
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Update Playlist set NomeAux=?  WHERE Tag=?", Parametros)


            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        Public Shared Sub CriarRelacao(ByVal Tag As Integer, ByVal NumMusica As Integer)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()

                parametro = New OleDbParameter("Tag", OleDb.OleDbType.Integer)
                parametro.Value = Tag
                Parametros.Add(parametro)

                Dim NumPlaylist = DAL.ExecuteScalar("Select numplaylist from playlist where tag=?", Parametros)

                Parametros.Clear()

                parametro = New OleDbParameter("NumPlaylist", OleDb.OleDbType.Integer)
                parametro.Value = NumPlaylist
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Insert into MusicaPlaylist (NumPlaylist, NumMusica) VALUES (?,?)", Parametros)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        Public Shared Sub Remover(ByVal Tag As Integer)

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()

                parametro = New OleDbParameter("Tag", OleDb.OleDbType.Integer)
                parametro.Value = Tag
                Parametros.Add(parametro)
                Dim NumPlaylist = DAL.ExecuteScalar("Select NumPlaylist from Playlist  WHERE Tag=?", Parametros)


                Parametros.Clear()

                parametro = New OleDbParameter("NumPlaylist", OleDb.OleDbType.Integer)
                parametro.Value = NumPlaylist
                Parametros.Add(parametro)
                DAL.ExecuteNonQuery("Delete from MusicaPlaylist  WHERE NumPlaylist=?", Parametros)


                Parametros.Clear()

                parametro = New OleDbParameter("Tag", OleDb.OleDbType.Integer)
                parametro.Value = Tag
                Parametros.Add(parametro)
                DAL.ExecuteNonQuery("Delete from Playlist  WHERE Tag=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Shared Sub Remover2(ByVal Tag As Integer)

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()

                parametro = New OleDbParameter("NumPlaylist", OleDb.OleDbType.Integer)
                parametro.Value = Tag
                Parametros.Add(parametro)
                DAL.ExecuteNonQuery("Delete from MusicaPlaylist  WHERE NumPlaylist=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Shared Function MostrarUsandoNumPlaylist(ByVal Tag As Integer) As DataTable
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                If Tag <> 5 Then
                    parametro = New OleDbParameter("Tag", OleDb.OleDbType.Integer)
                    parametro.Value = Tag
                    Parametros.Add(parametro)
                    Dim NumPlaylist As Integer = DAL.ExecuteScalar("Select NumPlaylist from Playlist where Tag=?", Parametros)

                    Parametros.Clear()
                    parametro = New OleDbParameter("NumPlaylist", OleDb.OleDbType.Integer)
                    parametro.Value = NumPlaylist
                    Parametros.Add(parametro)
                    Dim drNumMusica As New ArrayList
                    Return DAL.ExecuteQueryDT("SELECT Url,Capa, Nomemusica, Duracao,Tamanho, NomeInterprete, NomeAlbum,  Ano, NomeGenero, DataAdicao, NumReproducao, UltimaReproducao,  Classificacao, Musica.NumMusica FROM (((((((((Interprete Left JOIN MusicaInterprete ON musicaInterprete.numinterprete=Interprete.numinterprete) Left JOIN Musica ON musicainterprete.numMusica=Musica.numMusica) Left JOIN MusicaAlbum ON MusicaAlbum.numMusica=musica.numMusica) Left JOIN Album ON musicaAlbum.numAlbum=Album.numAlbum) Left JOIN MusicaGenero ON MusicaGenero.numMusica=musica.numMusica) Left JOIN Genero ON musicaGenero.numGenero=Genero.numGenero) Left JOIN MusicaHistorico ON MusicaHistorico.nummusica=musica.nummusica) Left JOIN Historico ON MusicaHistorico.numhistorico=Historico.numhistorico) left join MusicaPlaylist on MusicaPlaylist.NumMusica=Musica.NumMusica) left join Playlist on MusicaPlaylist.NumPlaylist=Playlist.NumPlaylist WHERE Playlist.NumPlaylist=?", Parametros)
                Else
                    Try
                        Return DAL.ExecuteQueryDT("SELECT Musica.NumMusica, Tamanho, Nomemusica, Duracao, NomeInterprete, NomeAlbum, Ano, NomeGenero, DataAdicao, Classificacao, NumReproducao, UltimaReproducao, Url FROM (((((((Musica INNER JOIN MusicaAlbum ON musicaAlbum.nummusica=musica.nummusica) INNER JOIN Album ON musicaAlbum.numAlbum=Album.numAlbum) INNER JOIN MusicaInterprete ON musicaInterprete.nummusica=musica.nummusica) INNER JOIN interprete ON musicainterprete.numinterprete=interprete.numinterprete) INNER JOIN MusicaGenero ON MusicaGenero.numMusica=musica.numMusica) INNER JOIN Genero ON musicaGenero.numGenero=Genero.numGenero) INNER JOIN MusicaHistorico ON MusicaHistorico.nummusica=musica.nummusica) INNER JOIN Historico ON MusicaHistorico.numhistorico=Historico.numhistorico ORDER BY Historico.NumReproducao DESC", Nothing)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

        Public Shared Function MostrarUsandoTag2(ByVal Tag As Integer) As DataTable

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()
                parametro = New OleDbParameter("Tag", OleDb.OleDbType.Integer)
                parametro.Value = Tag
                Parametros.Add(parametro)
                Return DAL.ExecuteQueryDT("Select NumPlaylist,NomePlaylist, NomeAux,TagContextMenuStrip from Playlist  WHERE Tag=?", Parametros)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

        Public Shared Function CountFromPlaylist() As Integer
            Try
                Return DAL.ExecuteScalar("Select count(*) from Playlist", Nothing)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

        Public Shared Function MostrarTagUsandotagcontextmenustrip(ByVal tagcontextmenustrip As Integer) As Integer

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()

                parametro = New OleDbParameter("tagcontextmenustrip", OleDb.OleDbType.Integer)
                parametro.Value = tagcontextmenustrip
                Parametros.Add(parametro)

                Return DAL.ExecuteScalar("Select Tag from Playlist  WHERE tagcontextmenustrip=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

        Public Shared Function MaiorTag() As Integer

            Try
                Return DAL.ExecuteScalar("Select max(Tag) from Playlist", Nothing)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

        Public Shared Function MaiorTagContextMenuStrip() As Integer
            Try
                Return DAL.ExecuteScalar("Select max(TagContextMenuStrip) from Playlist", Nothing)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

        Public Shared Function VerificaNomeExistente(ByVal NomePlaylist As String) As String


            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()
                parametro = New OleDbParameter("NomeAux", OleDb.OleDbType.VarChar)
                parametro.Value = NomePlaylist
                Parametros.Add(parametro)
                Return DAL.ExecuteScalar("Select NomeAux from Playlist  WHERE NomeAux=?", Parametros)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

        Public Shared Function CountPlaylist25() As Integer
            Try
                Return DAL.ExecuteScalar("SELECT count(*) from (musica inner join Musicaplaylist on  musica.nummusica=musicaplaylist.nummusica) inner join playlist on musicaplaylist.numplaylist=playlist.numplaylist where playlist.numplaylist=5", Nothing)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

        Public Shared Function Mostrar25maisReprod() As DataTable

            Try
                Return DAL.ExecuteQueryDT("SELECT Musica.NumMusica, Tamanho, Nomemusica, Duracao, NomeInterprete, NomeAlbum, Ano, NomeGenero, DataAdicao, Classificacao, NumReproducao, UltimaReproducao, Url FROM (((((((Musica INNER JOIN MusicaAlbum ON musicaAlbum.nummusica=musica.nummusica) INNER JOIN Album ON musicaAlbum.numAlbum=Album.numAlbum) INNER JOIN MusicaInterprete ON musicaInterprete.nummusica=musica.nummusica) INNER JOIN interprete ON musicainterprete.numinterprete=interprete.numinterprete) INNER JOIN MusicaGenero ON MusicaGenero.numMusica=musica.numMusica) INNER JOIN Genero ON musicaGenero.numGenero=Genero.numGenero) INNER JOIN MusicaHistorico ON MusicaHistorico.nummusica=musica.nummusica) INNER JOIN Historico ON MusicaHistorico.numhistorico=Historico.numhistorico ORDER BY Historico.NumReproducao DESC", Nothing)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

    End Class

    Public Class Interprete

        Shared Sub Adicionar(ByVal nomeInterprete As String)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()


                parametro = New OleDbParameter("nomeInterprete", OleDb.OleDbType.VarChar)
                parametro.Value = nomeInterprete
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Insert into Interprete (nomeInterprete) VALUES (?)", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        Public Shared Sub Alterar(ByVal nummusica As Integer, ByVal nomeinterprete As String, ByVal nomegrupo As String)

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()

                parametro = New OleDbParameter("nummusica", OleDb.OleDbType.Integer)
                parametro.Value = nummusica
                Parametros.Add(parametro)

                Dim numinterprete As Integer = DAL.ExecuteScalar("SELECT numinterprete FROM musicainterprete WHERE nummusica=?", Parametros)

                Parametros.Clear()
                parametro = New OleDbParameter("nomeinterprete", OleDb.OleDbType.VarChar)
                parametro.Value = nomeinterprete
                Parametros.Add(parametro)

                parametro = New OleDbParameter("nomegrupo", OleDb.OleDbType.VarChar)
                parametro.Value = nomegrupo
                Parametros.Add(parametro)

                parametro = New OleDbParameter("numinterprete", OleDb.OleDbType.Integer)
                parametro.Value = numinterprete
                Parametros.Add(parametro)


                DAL.ExecuteNonQuery("Update Interprete set nomeinterprete=?, nomegrupo=? WHERE numinterprete=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Shared Function MostrarUsandoNumMusica(ByVal NumMusica As Integer) As DataTable


            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()
                parametro = New OleDbParameter("nummusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                Dim numinterprete As Integer = DAL.ExecuteScalar("SELECT numinterprete FROM musicainterprete WHERE nummusica=?", Parametros)

                If numinterprete = Nothing Then
                    Exit Function
                Else

                    Parametros.Clear()
                    parametro = New OleDbParameter("numinterprete", OleDb.OleDbType.Integer)
                    parametro.Value = numinterprete
                    Parametros.Add(parametro)

                End If

                Return DAL.ExecuteQueryDT("SELECT nomeinterprete, nomeGrupo from interprete where numinterprete=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

        Public Shared Function MostrarUltimoNum() As Integer
            Try
                Return DAL.ExecuteScalar("Select max(numInterprete) from Interprete", Nothing)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

    End Class

    Public Class Album

        Shared Sub Adicionar(ByVal nomeAlbum As String)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()

                parametro = New OleDbParameter("nomeAlbum", OleDb.OleDbType.VarChar)
                parametro.Value = nomeAlbum
                Parametros.Add(parametro)



                DAL.ExecuteNonQuery("Insert into Album (nomeAlbum) VALUES (?)", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        Public Shared Sub Alterar(ByVal nummusica As Integer, ByVal nomeAlbum As String)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()

                parametro = New OleDbParameter("nummusica", OleDb.OleDbType.Integer)
                parametro.Value = nummusica
                Parametros.Add(parametro)


                Dim numalbum As Integer = DAL.ExecuteScalar("SELECT numalbum FROM musicaalbum WHERE nummusica=?", Parametros)

                Parametros.Clear()

                parametro = New OleDbParameter("nomealbum", OleDb.OleDbType.VarChar)
                parametro.Value = nomeAlbum
                Parametros.Add(parametro)

                parametro = New OleDbParameter("numAlbum", OleDb.OleDbType.Integer)
                parametro.Value = numalbum
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Update album set nomealbum=?  WHERE numalbum=?", Parametros)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Shared Function MostrarUsandoNumMusica(ByVal NumMusica As Integer) As DataTable


            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try


                Parametros.Clear()

                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                Dim numAlbum As Integer = DAL.ExecuteScalar("SELECT numAlbum FROM MusicaAlbum WHERE nummusica=?", Parametros)

                If numAlbum = Nothing Then
                    Exit Function
                Else
                    Parametros.Clear()
                    parametro = New OleDbParameter("numAlbum", OleDb.OleDbType.Integer)
                    parametro.Value = numAlbum
                    Parametros.Add(parametro)

                End If

                Return DAL.ExecuteQueryDT("SELECT NomeAlbum, NumCD, NumCDTotal from Album where numAlbum=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

        Public Shared Function MostrarUltimoNumAlbum() As Integer
            Try
                Return DAL.ExecuteScalar("Select max(NumAlbum) from Album", Nothing)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

    End Class

    Public Class Genero

        Shared Sub Adicionar(ByVal nomeGenero As String)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()

                parametro = New OleDbParameter("nomeGenero", OleDb.OleDbType.VarChar)
                parametro.Value = nomeGenero
                Parametros.Add(parametro)

                DAL.ExecuteNonQuery("Insert into Genero (nomeGenero) VALUES (?)", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        Public Shared Sub Alterar(ByVal nummusica As Integer, ByVal NomeGenero As String)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()

                If nummusica <> Nothing Then
                    parametro = New OleDbParameter("nummusica", OleDb.OleDbType.Integer)
                    parametro.Value = nummusica
                    Parametros.Add(parametro)
                End If


                Dim numGenero As ArrayList = DAL.ExecuteQuery("SELECT numGenero FROM musicaGenero WHERE nummusica=?", Parametros)

                If numGenero.Count = Nothing Then
                    Exit Sub
                Else
                    For j As Integer = 0 To numGenero.Count - 1
                        Parametros.Clear()
                        parametro = New OleDbParameter("nomeGenero", OleDb.OleDbType.VarChar)
                        parametro.Value = NomeGenero
                        Parametros.Add(parametro)

                        parametro = New OleDbParameter("numGenero", OleDb.OleDbType.Integer)
                        parametro.Value = numGenero(j)
                        Parametros.Add(parametro)
                    Next
                    DAL.ExecuteNonQuery("Update Genero set nomeGenero=? WHERE numGenero=?", Parametros)
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Shared Function MostrarUsandoNumMusica(ByVal NumMusica As Integer) As DataTable

            'vai buscar o numero do Genero relacionado se tiver

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()

                parametro = New OleDbParameter("nummusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                Dim numGenero As ArrayList = DAL.ExecuteQuery("SELECT numGenero FROM musicaGenero WHERE nummusica=?", Parametros)

                If numGenero.Count = Nothing Then
                    Exit Function
                Else

                    For i As Integer = 0 To numGenero.Count - 1
                        Dim NomeGenero As String = numGenero(i)
                        Parametros.Clear()
                        parametro = New OleDbParameter("NomeGenero", OleDb.OleDbType.VarChar)
                        parametro.Value = NomeGenero
                        Parametros.Add(parametro)

                        Return DAL.ExecuteQueryDT("Select NomeGenero from Genero WHERE numGenero=?", Parametros)
                    Next

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

        Public Shared Function MostrarUltimoNum() As Integer
            Try
                Return DAL.ExecuteScalar("Select max(NumGenero) from Genero", Nothing)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

    End Class

    Public Class Formato

        Public Shared Function MostrarNumFormato(ByVal NomeFormato As String) As Integer

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()

                parametro = New OleDbParameter("NomeFormato", OleDb.OleDbType.VarChar)
                parametro.Value = NomeFormato
                Parametros.Add(parametro)

                Return DAL.ExecuteScalar("SELECT numFormato from Formato where NomeFormato=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

        Public Shared Function MostrarNomeFormato(ByVal nummusica As Integer) As String

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()
                parametro = New OleDbParameter("nummusica", OleDb.OleDbType.Integer)
                parametro.Value = nummusica
                Parametros.Add(parametro)

                Dim numFormato As Integer = DAL.ExecuteScalar("Select numformato from musica where nummusica=?", Parametros)

                Parametros.Clear()
                parametro = New OleDbParameter("numFormato", OleDb.OleDbType.Integer)
                parametro.Value = numFormato
                Parametros.Add(parametro)

                Return DAL.ExecuteScalar("SELECT NomeFormato  from Formato where numFormato=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

    End Class

    Public Class Historico

        Shared Sub Adicionar(ByVal numReproducao As Integer, ByVal ultimaReproducao As Date)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try
                Parametros.Clear()

                If numReproducao <> Nothing Then
                    parametro = New OleDbParameter("numReproducao", OleDb.OleDbType.Integer)
                    parametro.Value = numReproducao
                    Parametros.Add(parametro)
                End If

                If ultimaReproducao <> Nothing Then
                    parametro = New OleDbParameter("ultimaReproducao", OleDb.OleDbType.Date)
                    parametro.Value = ultimaReproducao
                    Parametros.Add(parametro)
                End If

                DAL.ExecuteNonQuery("Insert into Historico (numReproducao, ultimaReproducao ) VALUES (?,?)", Parametros)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        Public Shared Sub CriarRelacao(ByVal NumMusica As Integer, ByVal NumHistorico As Integer)
            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                Parametros.Clear()
                parametro = New OleDbParameter("NumMusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                parametro = New OleDbParameter("NumHistorico", OleDb.OleDbType.Integer)
                parametro.Value = NumHistorico
                Parametros.Add(parametro)


                DAL.ExecuteNonQuery("Insert into MusicaHistorico (NumMusica, NumHistorico) VALUES (?,?)", Parametros)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        Public Shared Sub Alterar(ByVal numReproducao As Integer, ByVal ultimaReproducao As Date, ByVal numHistorico As Integer)
            'vai buscar o numero do historico relacionado se tiver

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try

                If numHistorico = Nothing Then
                    Exit Sub
                Else
                    Parametros.Clear()

                    parametro = New OleDbParameter("numreproducao", OleDb.OleDbType.Integer)
                    parametro.Value = numReproducao
                    Parametros.Add(parametro)


                    parametro = New OleDbParameter("ultimaReproducao", OleDb.OleDbType.Date)
                    parametro.Value = ultimaReproducao
                    Parametros.Add(parametro)


                    parametro = New OleDbParameter("NumHistorico", OleDb.OleDbType.Integer)
                    parametro.Value = numHistorico
                    Parametros.Add(parametro)


                    DAL.ExecuteNonQuery("Update Historico set numreproducao=? , ultimaReproducao=? WHERE numHistorico=?", Parametros)
                End If



            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try



        End Sub

        Public Shared Function MostrarUsandoNumMusica(ByVal NumMusica As Integer) As DataTable

            Dim Parametros As New ArrayList
            Dim parametro As OleDbParameter

            Try


                Parametros.Clear()

                parametro = New OleDbParameter("nummusica", OleDb.OleDbType.Integer)
                parametro.Value = NumMusica
                Parametros.Add(parametro)

                Dim numHistorico As Integer = DAL.ExecuteScalar("SELECT numHistorico FROM musicaHistorico WHERE nummusica=?", Parametros)

                If numHistorico <> Nothing Then
                    Parametros.Clear()

                    parametro = New OleDbParameter("numHistorico", OleDb.OleDbType.Integer)
                    parametro.Value = numHistorico
                    Parametros.Add(parametro)
                Else
                    Exit Function
                End If

                Return DAL.ExecuteQueryDT("SELECT numHistorico, numreproducao,ultimaReproducao from Historico where numHistorico=?", Parametros)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Function

        Public Shared Function UltimoNumPlaylist() As Integer
            Try
                Return DAL.ExecuteScalar("Select max(NumHistorico) from Historico", Nothing)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

    End Class

End Class
