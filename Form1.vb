Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.data.MySqlClient
Imports Microsoft.Win32
Public Class Form1
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

    Private bitmap As Bitmap   'print
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

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint, Panel4.Paint

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            For Each txt In Panel4.Controls
                If TypeOf txt Is TextBox Then
                    txt.text = ""

                End If
            Next
            TextBox3.Text = ""

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)
        Next
        updateTable()
        LoadDataInGrid()

    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim iExit As DialogResult

        iExit = MessageBox.Show("Confirm if you want to logout.", "Rongo Patients Records.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If iExit = DialogResult.Yes Then
            Application.Exit()

        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateTable()
        LoadDataInGrid()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" + "password=" + password + ";" + "database=" + database
        Try
            sqlConn.Open()
            sqlQuery = "Insert into patiensdb.patiensdbtable(CaseNumber,IdNumber,Name,Contact,Address,Age,Gender,Diagnosis,Prescription,Bill,DateAdded)
                value('" & TextBox4.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text _
                & "','" & TextBox8.Text & "','" & TextBox9.Text & "','" & TextBox10.Text & "','" & TextBox11.Text & "','" & TextBox13.Text & "')"

            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
            sqlRd = sqlCmd.ExecuteReader
            MessageBox.Show("Record Added.")
            sqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MySQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            sqlConn.Dispose()
        End Try

        updateTable()
        LoadDataInGrid()



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

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim con As New MySqlConnection("server=" + server + ";" + "user id=" + username + ";" _
            + "password=" + password + ";" + "database=" + database)
        con.Open()
        Dim command As New MySqlCommand("update patiensdb.patiensdbtable set IdNumber='" & TextBox1.Text & "',Name='" & TextBox2.Text & "',Contact='" & TextBox5.Text & "',Address='" & TextBox6.Text & "',Age='" & TextBox7.Text & "',Gender='" & TextBox8.Text & "',Diagnosis='" & TextBox9.Text & "',Prescription='" & TextBox10.Text & "',Bill='" & TextBox11.Text & "',DateAdded='" & TextBox13.Text & "' where CaseNumber= '" & TextBox4.Text & "'", con)

        command.ExecuteNonQuery()
        MessageBox.Show("Successfully updated.")
        LoadDataInGrid()
        updateTable()

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try

            TextBox4.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
            TextBox1.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
            TextBox2.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
            TextBox5.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
            TextBox6.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()
            TextBox7.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString()
            TextBox8.Text = DataGridView1.SelectedRows(0).Cells(6).Value.ToString()
            TextBox9.Text = DataGridView1.SelectedRows(0).Cells(7).Value.ToString()
            TextBox10.Text = DataGridView1.SelectedRows(0).Cells(8).Value.ToString()
            TextBox11.Text = DataGridView1.SelectedRows(0).Cells(9).Value.ToString()
            '    TextBox13.Text = DataGridView1.SelectedRows(0).Cells(10).Value.ToString()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        Try
            If Asc(e.KeyChar) = 13 Then
                Dim dv As DataView
                dv = sqlDt.DefaultView
                dv.RowFilter = String.Format("Name like '%{0}%'", TextBox3.Text)
                DataGridView1.DataSource = dv.ToTable()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawImage(bitmap, 0, 0)
        Dim recp As RectangleF = e.PageSettings.PrintableArea

        If Me.DataGridView1.Height - recp.Height > 0 Then e.HasMorePages = True

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim height As Integer = DataGridView1.Height
        DataGridView1.Height = DataGridView1.RowCount * DataGridView1.RowTemplate.Height
        bitmap = New Bitmap(Me.DataGridView1.Width, Me.DataGridView1.Height)
        DataGridView1.DrawToBitmap(bitmap, New Rectangle(0, 0, Me.DataGridView1.Width, Me.DataGridView1.Height))
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
        PrintPreviewDialog1.PrintPreviewControl.Zoom = 1
        DataGridView1.Height = height

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
