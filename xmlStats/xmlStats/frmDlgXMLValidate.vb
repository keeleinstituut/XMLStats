Imports System.Windows.Forms
Imports System.Xml
Imports System.Xml.Schema
Imports System.IO


Public Class frmDlgXMLValidate

    Dim dlgOFD As OpenFileDialog = frmMain.dlgOFD

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Not (tbXMLFile.Text = String.Empty Or tbXSDFile.Text = String.Empty) Then
            Call validateXML()
        End If

        Exit Sub

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnBrowseXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseXML.Click

        'All files (*.*)|*.*|TXT files (*.txt)|*.txt|XML files (*.xml)|*.xml|
        'RTF files (*.rtf)|*.rtf|MS Office Word files (*.doc)|*.doc|MS Office Excel files (*.xls)|*.xls|
        'XSD schema files (*.xsd)|*.xsd|XML manager project files (*.xhproj)|*.xhproj

        Dim dlgResult As DialogResult

        dlgOFD.Multiselect = False
        dlgOFD.FilterIndex = 3 'XML
        dlgOFD.FileName = String.Empty
        dlgOFD.Title = "Sisend: XML fail"
        dlgResult = dlgOFD.ShowDialog()
        If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If
        tbXMLFile.Text = dlgOFD.FileName

    End Sub

    Private Sub btnBrowseXSD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseXSD.Click

        'All files (*.*)|*.*|TXT files (*.txt)|*.txt|XML files (*.xml)|*.xml|
        'RTF files (*.rtf)|*.rtf|MS Office Word files (*.doc)|*.doc|MS Office Excel files (*.xls)|*.xls|
        'XSD schema files (*.xsd)|*.xsd|XML manager project files (*.xhproj)|*.xhproj

        Dim dlgResult As DialogResult

        dlgOFD.Multiselect = False
        dlgOFD.FilterIndex = 7 'XSD
        dlgOFD.FileName = String.Empty
        dlgOFD.Title = "Sisend: XSD fail"
        dlgResult = dlgOFD.ShowDialog()
        If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If
        tbXSDFile.Text = dlgOFD.FileName

    End Sub

    Dim vigu As Integer = 0
    Dim veaDialoog As frmDlgValidationErrors

    Private Sub validateXML()

        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False



        'Dim xsdDom As XmlDocument
        Dim xsdSchema As XmlSchema
        Dim xsdSchemaSet As XmlSchemaSet
        Dim xrs As XmlReaderSettings

        'xsdDom = New XmlDocument
        'xsdDom.Load(tbXSDFile.Text)

        Try
            xsdSchema = XmlSchema.Read(New XmlTextReader(tbXSDFile.Text), Nothing)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, tbXSDFile.Text)
            Me.Enabled = True
            Exit Sub
        End Try

        vigu = 0
        veaDialoog = New frmDlgValidationErrors
        veaDialoog.lvVead.Items.Clear()

        'xsd XmlSchemaSet
        xsdSchemaSet = New XmlSchemaSet()
        'skeemide laadimise vead
        AddHandler xsdSchemaSet.ValidationEventHandler, AddressOf xmlReaderValidationEventHandler
        xsdSchemaSet.Add(xsdSchema)
        xsdSchemaSet.Compile()

        If vigu > 0 Then
            veaDialoog.Text = vigu & " viga."
            Call veaDialoog.ShowDialog()
            Me.Enabled = True
            Exit Sub
        End If

        ' Create a schema validating XmlReader.
        xrs = New XmlReaderSettings()
        xrs.Schemas.Add(xsdSchema)
        AddHandler xrs.ValidationEventHandler, New ValidationEventHandler(AddressOf xmlReaderValidationEventHandler)
        xrs.ValidationFlags = xrs.ValidationFlags Or XmlSchemaValidationFlags.ProcessSchemaLocation Or XmlSchemaValidationFlags.ReportValidationWarnings
        xrs.ValidationType = ValidationType.Schema

        Dim xmlDom As New XmlDocument

        Try
            xmlDom.Load(XmlReader.Create(tbXMLFile.Text, xrs))
        Catch ex As Exception
            'Console.WriteLine(ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Critical, tbXMLFile.Text)
        End Try

        If vigu > 0 Then
            veaDialoog.Text = vigu & " viga."
            Call veaDialoog.ShowDialog()
        End If



        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)

        Console.WriteLine()
        Console.WriteLine("OK ... " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds))
        Console.WriteLine()
        MsgBox("OK..." & vbCrLf & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds))

    End Sub

    Private Sub xmlReaderValidationEventHandler(ByVal sender As Object, ByVal args As ValidationEventArgs)

        Dim valStr As String = String.Empty
        Dim tüüp As String = String.Empty

        If args.Severity = XmlSeverityType.Warning Then
            valStr = "WARNING: "
        Else
            If args.Severity = XmlSeverityType.Error Then
                valStr = "ERROR: "
            End If
        End If
        tüüp = valStr

        vigu += 1
        Dim lvi As New ListViewItem(vigu)
        lvi.SubItems.Add(args.Exception.LineNumber)
        lvi.SubItems.Add(args.Exception.LinePosition)
        lvi.SubItems.Add(tüüp & args.Message)
        veaDialoog.lvVead.Items.Add(lvi)

        'valStr &= args.Exception.LineNumber & ", " & args.Exception.LinePosition & ": " & args.Message

        'Dim praegu As DateTime = Now
        'Console.WriteLine(vbCrLf & praegu & " " & valStr)

    End Sub

End Class
