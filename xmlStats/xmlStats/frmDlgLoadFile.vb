Imports System.Windows.Forms
Imports System.IO


Public Class frmDlgLoadFile

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If (lvFileNames.SelectedItems.Count <> 1) Then
            Exit Sub
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmDlgLoadFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim di As New DirectoryInfo(Application.StartupPath & "\tmp")
        For Each fi As FileInfo In di.GetFiles("*.rtf")
            Dim lvi As New ListViewItem(fi.Name)
            lvi.Tag = fi.FullName
            lvFileNames.Items.Add(lvi)
        Next
        For Each chdr As ColumnHeader In lvFileNames.Columns
            chdr.Width = -2
        Next
    End Sub

    Private Sub lvFileNames_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvFileNames.DoubleClick
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class
