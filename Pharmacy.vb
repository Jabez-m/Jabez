Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient
Imports Microsoft.Win32
Public Class Pharmacy

    Dim sqlConn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim DtA As New MySqlDataAdapter
    Dim sqlQuery As String

    Dim server As String = "localhost"
    Dim username As String = "root"
    Dim password As String = "p@$$w&Rd1."
    Dim database As String = "Patiensdb"

    Private Sub updateTable()
        sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" _
            + "password=" + password + ";" + "database=" + database

        sqlConn.Open()
        sqlCmd.Connection = sqlConn
        sqlCmd.CommandText = "SELECT * From Patiensdb.patiensdbtable"

        sqlRd = sqlCmd.ExecuteReader
        sqlDt.Load(sqlRd)
        sqlRd.Close()
        sqlConn.Close()
        DataGridView1.DataSource = sqlDt
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim iExit As DialogResult

        iExit = MessageBox.Show("Confirm if you want to logout.", "Rongo Patients' Records.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If iExit = DialogResult.Yes Then
            Application.Exit()

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            For Each txt In Panel3.Controls
                If TypeOf txt Is TextBox Then
                    txt.text = ""

                End If
            Next
            TextBox8.Text = ""

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim con As New MySqlConnection("server=" + server + ";" + "user id=" + username + ";" _
            + "password=" + password + ";" + "database=" + database)
        con.Open()
        Dim command As New MySqlCommand("update patiensdb.patiensdbtable set Prescription='" & TextBox1.Text & "' where CaseNumber='" & TextBox2.Text & "'", con)

        command.ExecuteNonQuery()
        MessageBox.Show("Successfully updated.")
        LoadDataInGrid()
        updateTable()


    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label8_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub LoadDataInGrid()
        Dim con As New MySqlConnection("server=" + server + ";" + "user id=" + username + ";" _
            + "password=" + password + ";" + "database=" + database)
        Dim command As New MySqlCommand("select * from patiensdb.patiensdbtable ", con)
        Dim sda As New MySqlDataAdapter(command)
        Dim dt As New DataTable
        sda.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub Detailing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateTable()
        LoadDataInGrid()

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try

            TextBox1.Text = DataGridView1.SelectedRows(0).Cells(8).Value.ToString()
            TextBox2.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        Try
            If Asc(e.KeyChar) = 13 Then
                Dim dv As DataView
                dv = sqlDt.DefaultView
                dv.RowFilter = String.Format("Name like '%{0}%'", TextBox8.Text)
        DataGridView1.DataSource = dv.ToTable()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged

    End Sub
End Class