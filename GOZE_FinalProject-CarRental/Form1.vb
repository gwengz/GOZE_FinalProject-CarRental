Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim query As String = "INSERT INTO `car_rental`.`car_rental` 
                            (`ID`, `Car Model`, `Renter Name`, `Start Date`, `End Date`) 
                            VALUES (@ID, @Car Model, @Renter Name, @Start Date, @End Date);"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=car_rental;")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("Car Model", txtCarModel.Text)
                    cmd.Parameters.AddWithValue("Renter Name", txtCarModel.Text)
                    cmd.Parameters.AddWithValue("Start Date", CInt(txtCarModel.Text))
                    cmd.ExecuteNonQuery()
                    MessageBox.Show()
                    MessageBox.Show("Record insert successful!")
                    TextBoxName.Clear()
                    TextBoxAge.Clear()
                    TextBoxEmail.Clear()
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class