Imports MySql.Data.MySqlClient

Public Class Login
    Dim constring As New MySqlConnection

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


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged





    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        constring = New MySqlConnection("server=localhost;database=Patiensdb;username=root;password=p@$$w&Rd1.")
        Dim cmd As New MySqlCommand("Select * From login where UserType ='" & ComboBox1.SelectedItem & "'and UserId='" & txtId.Text & "'and Password='" & txtPass.Text & "'", constring)
        Dim adap As MySqlDataAdapter = New MySqlDataAdapter(cmd)
        Dim dt As DataTable = New DataTable()
        adap.Fill(dt)
        Try
            If ComboBox1.SelectedItem = "Admin" Then
                MessageBox.Show("Login As an " + dt.Rows(0)(0) + ". Click ok to continue.")
                Dim a As New Form1
                a.Show()
                Me.Hide()
            ElseIf ComboBox1.SelectedItem = "Accounts" Then
                MessageBox.Show("Login As an " + ComboBox1.SelectedItem + ". Click ok to continue.")
                Dim a As New Accounts
                a.Show()
                Me.Hide()
            ElseIf ComboBox1.SelectedItem = "Detailing" Then
                MessageBox.Show("Login As an " + ComboBox1.SelectedItem + ". Click ok to continue.")
                Dim a As New Detailing
                a.Show()
                Me.Hide()
            ElseIf ComboBox1.SelectedItem = "Diagnosis" Then
                MessageBox.Show("Login As an " + ComboBox1.SelectedItem + ". Click ok to continue.")
                Dim a As New Diagnosis
                a.Show()
                Me.Hide()
            ElseIf ComboBox1.SelectedItem = "Pharmacy" Then
                MessageBox.Show("Login As an " + ComboBox1.SelectedItem + ". Click ok to continue.")
                Dim a As New Pharmacy
                a.Show()
                Me.Hide()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try




    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            constring = New MySqlConnection("server=" + server + ";" + "database=" + database + ";" + "user id=" + username + ";" + "password=" + password)
            constring.Open()
            Label1.Text = "You Are Connected to Server. Please Select the User Type."

        Catch ex As Exception
            Label1.Text = "Connection to server failed."
        End Try

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim iExit As DialogResult

        iExit = MessageBox.Show("Confirm if you want to exit.", "Rongo Patients Records.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If iExit = DialogResult.Yes Then
            Application.Exit()

        End If
    End Sub
End Class