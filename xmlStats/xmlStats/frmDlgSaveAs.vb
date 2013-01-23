Imports System.Windows.Forms

Public Class frmDlgSaveAs

    Dim dlgSFD As SaveFileDialog = frmMain.dlgSFD

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnBrowseFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFile.Click
        'salvestamine
        'All files (*.*)|*.*|Text files (*.txt)|*.txt|
        'XML files (*.xml)|*.xml|XSD files (*.xsd)|*.xsd|
        'xmlStats files (*.xmlStats)|*.xmlStats|RTF files (*.rtf)|*.rtf
        dlgSFD.FilterIndex = 3 'XML
        dlgSFD.FileName = String.Empty
        dlgSFD.Title = "Salvesta kui ..."
        Dim dlgResult As DialogResult = dlgSFD.ShowDialog()
        If (dlgResult = Windows.Forms.DialogResult.OK) Then
            tbLocation.Text = dlgSFD.FileName
        End If
    End Sub
End Class
