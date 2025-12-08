Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        If String.IsNullOrWhiteSpace(txtCarModel.Text) Then
            MessageBox.Show("Please enter a valid Car Model.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCarModel.Focus()
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtRenterName.Text) Then
            MessageBox.Show("Please enter the Renter Name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtRenterName.Focus()
            Exit Sub
        End If

        Dim query As String = "INSERT INTO car_rental 
                           (car_model, renter_name, start_date, end_date)
                           VALUES (@car_model, @renter_name, @start_date, @end_date);"

        Try
            Using conn As New MySqlConnection("server=localhost;userid=root;password=root;database=car_rental;")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@car_model", txtCarModel.Text)
                    cmd.Parameters.AddWithValue("@renter_name", txtRenterName.Text)
                    cmd.Parameters.AddWithValue("@start_date", dtpStartDate.Value.Date)
                    cmd.Parameters.AddWithValue("@end_date", dtpEndDate.Value.Date)

                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record added successfully!")
                End Using
            End Using

            LoadData()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim query As String = "UPDATE car_rental SET 
                           car_model=@car_model,
                           renter_name=@renter_name,
                           start_date=@start_date,
                           end_date=@end_date
                           WHERE id=@id;"

        Try
            Using conn As New MySqlConnection("server=localhost;userid=root;password=root;database=car_rental;")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@car_model", txtCarModel.Text)
                    cmd.Parameters.AddWithValue("@renter_name", txtRenterName.Text)
                    cmd.Parameters.AddWithValue("@start_date", dtpStartDate.Value.Date)
                    cmd.Parameters.AddWithValue("@end_date", dtpEndDate.Value.Date)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))

                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record updated successfully!")
                End Using
            End Using

            LoadData()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim query As String = "DELETE FROM car_rental WHERE id=@id;"

        Try
            Using conn As New MySqlConnection("server=localhost;userid=root;password=root;database=car_rental;")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))
                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Record deleted successfully!")
                End Using
            End Using

            LoadData()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim query As String = "SELECT * FROM car_rental;"

        Try
            Using conn As New MySqlConnection("server=localhost;userid=root;password=root;database=car_rental;")
                conn.Open()
                Using da As New MySqlDataAdapter(query, conn)
                    Dim dt As New DataTable
                    da.Fill(dt)
                    dgvRentals.DataSource = dt
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dtgRentals_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRentals.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvRentals.Rows(e.RowIndex)

            txtID.Text = row.Cells("id").Value.ToString()
            txtCarModel.Text = row.Cells("car_model").Value.ToString()
            txtRenterName.Text = row.Cells("renter_name").Value.ToString()
            dtpStartDate.Value = CDate(row.Cells("start_date").Value)
            dtpEndDate.Value = CDate(row.Cells("end_date").Value)
        End If
    End Sub
End Class