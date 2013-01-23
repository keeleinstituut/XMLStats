Public Class frmShowParseErrors

    Private Sub lvErrors_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvErrors.DoubleClick
        Call showThisError(lvErrors.SelectedItems(0))
    End Sub

    Private Sub showThisError(ByVal lvi As ListViewItem)
        Dim myErr As New frmShowParseError
        myErr.Text &= "nr " & lvi.SubItems(1).Text & ", '" & xmlFile & "'"
        myErr.tbErrOffset.Text = lvi.SubItems(0).Text
        myErr.tbHeadWord.Text = lvi.SubItems(3).Text
        myErr.tbError.Text = lvi.SubItems(2).Text
        'myErr.tbArtikkel.Text = lvi.SubItems(4).Text
        myErr.tbArtikkel.Text = lvi.Tag
        myErr.ShowDialog()
    End Sub
End Class