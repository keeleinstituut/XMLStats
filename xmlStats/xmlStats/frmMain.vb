Imports System.Xml
Imports System.Xml.Schema
Imports System.Globalization
Imports System.IO

Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop

Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Xml.Xsl
Imports System.Net
Imports System.Web


Public Class frmMain

    Dim schemaDom As XmlDocument
    Dim nsm As XmlNamespaceManager
    Dim schema As XmlSchema

    Dim xPr As String = String.Empty
    Dim xUri As String = String.Empty

    Dim boldFont As Font
    Dim rtbBold As Font

    Dim fdir As String = String.Empty
    Dim fne As String = String.Empty
    Dim fn As String = String.Empty
    Dim xmlDom As XmlDocument

    Dim nfi As NumberFormatInfo

    Dim wTitle As String

    Dim tehtud As SortedList

    Dim rtbFindPos As Integer = 0
    Dim rtbf As RichTextBoxFinds
    Dim rtbFindStr As String = String.Empty

    Dim seldRows As Dictionary(Of String, String)

    Dim indCopy As Xsl.XslCompiledTransform

    Dim fileFromCmdLine As String


    Private Shared Sub ValidationCallBack(ByVal sender As Object, ByVal e As ValidationEventArgs)
        Console.WriteLine("Validation Error: {0}", e.Message)
    End Sub


    Private Sub avaXMLFail()

        'All files (*.*)|*.*|TXT files (*.txt)|*.txt|
        'XML files (*.xml)|*.xml|RTF files (*.rtf)|*.rtf|
        'MS Office Word files (*.doc)|*.doc|MS Office Excel files (*.xls)|*.xls|
        'XSD schema files (*.xsd)|*.xsd|XML manager project files (*.xhproj)|*.xhproj|
        'CSV text files (*.csv)|*.csv

        If fileFromCmdLine.Length > 0 Then
            xmlFile = fileFromCmdLine
            fileFromCmdLine = String.Empty
        Else
            Dim dlgResult As DialogResult

            dlgOFD.Multiselect = False
            dlgOFD.FilterIndex = 3 'XML
            'päritav XML
            dlgOFD.FileName = String.Empty
            dlgOFD.Title = "Sisend: Mingi XML-fail"
            dlgResult = dlgOFD.ShowDialog()
            If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
                Exit Sub
            End If
            xmlFile = dlgOFD.FileName
        End If

        fdir = xmlFile.Substring(0, xmlFile.LastIndexOf("\"))
        fne = xmlFile.Substring(xmlFile.LastIndexOf("\") + 1)
        fn = fne.Substring(0, fne.LastIndexOf("."))


        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False

        xmlDom = Nothing
        Call clearInfo() 'staatuse real aeg ja rada tühjaks
        tvSchema.Nodes.Clear()
        rtbInfo.Text = String.Empty
        Me.Text = wTitle


        Dim schemaSet As XmlSchemaSet = New XmlSchemaSet()
        Dim infer As XmlSchemaInference = New XmlSchemaInference()

        Dim xmlRs As New XmlReaderSettings
        xmlRs.CheckCharacters = True
        xmlRs.ConformanceLevel = ConformanceLevel.Auto

        Dim reader As XmlReader = XmlReader.Create(xmlFile, xmlRs)
        Dim xmlEncStr As String = String.Empty 'XML deklaratsioonist
        Dim _nt As NameTable = Nothing
        Dim _xml_Pr As String = String.Empty 'juurelemendi prefiks
        Dim _xml_Uri As String = String.Empty 'juurelemendi nimeruum
        Dim rootQN As String = String.Empty 'juurelemendi QN
        Dim artQN As String = String.Empty 'järgmise taseme elemendi QN (artikkel "x:A")
        While reader.Read
            If reader.Depth = 0 And reader.NodeType = XmlNodeType.XmlDeclaration Then
                Dim dom As New XmlDocument
                Dim xd As XmlDeclaration = dom.CreateXmlDeclaration("1.0", Nothing, Nothing)
                xd.InnerText = reader.Value
                xmlEncStr = xd.Encoding
            ElseIf reader.Depth = 0 And reader.NodeType = XmlNodeType.Element Then
                _nt = reader.NameTable
                _xml_Pr = reader.Prefix
                _xml_Uri = reader.NamespaceURI
                rootQN = reader.Name
            ElseIf reader.Depth = 1 And reader.NodeType = XmlNodeType.Element Then
                artQN = reader.Name
                artLN = reader.LocalName 'public artikli LN ("A")
                Exit While
            End If
        End While
        reader.Close()

        Dim ba() As Byte = File.ReadAllBytes(xmlFile)

        Dim fileOff As Integer = 0
        Dim fileEncStr As String = String.Empty 'kas on faili alguses BOM
        If ba(0) = &HEF And ba(1) = &HBB And ba(2) = &HBF Then 'utf-8
            fileEncStr = "utf-8"
            fileOff = 3
        ElseIf ba(0) = &HFE And ba(1) = &HFF Then 'UTF-16 Big Endian (and ucs-2)
            fileEncStr = "utf-16"
            fileOff = 2
        ElseIf ba(0) = &HFF And ba(1) = &HFE Then 'UTF-16 Little Endian (ucs-2le, ucs-4le, and ucs-16le)
            fileEncStr = "utf-16le"
            fileOff = 2
        ElseIf ba(0) = &H0 And ba(1) = &H0 And ba(2) = &HFE And ba(3) = &HFF Then 'UTF-32 Big Endian
            fileEncStr = "utf-32"
            fileOff = 4
        ElseIf ba(0) = &HFF And ba(1) = &HFE And ba(2) = &H0 And ba(3) = &H0 Then 'UTF-32 Little Endian (ucs4)
            fileEncStr = "utf-32le"
            fileOff = 4
        End If

        'If fileEncStr.Length > 0 Then
        '    enc = System.Text.Encoding.GetEncoding(fileEncStr)
        'Else
        '    If xmlEncStr = "utf-8" Then
        '        'Dim onUTF8 As Boolean = True
        '        'For i As Integer = fileOff To ba.Length - 1
        '        '    If ba(i) > 127 Then
        '        '        If (ba(i) And &HE0) = &HC0 AndAlso (i + 1 < ba.Length) Then
        '        '            If Not ((ba(i + 1) And &HC0) = &H80) Then
        '        '                onUTF8 = False
        '        '            End If
        '        '            i += 1
        '        '        ElseIf (ba(i) And &HF0) = &HE0 AndAlso (i + 2 < ba.Length) Then
        '        '            If Not ((ba(i + 1) And &HC0) = &H80 And (ba(i + 2) And &HC0) = &H80) Then
        '        '                onUTF8 = False
        '        '            End If
        '        '            i += 2
        '        '        ElseIf (ba(i) And &HF8) = &HF0 AndAlso (i + 3 < ba.Length) Then
        '        '            If Not ((ba(i + 1) And &HC0) = &H80 And (ba(i + 2) And &HC0) = &H80 And (ba(i + 3) And &HC0) = &H80) Then
        '        '                onUTF8 = False
        '        '            End If
        '        '            i += 3
        '        '        ElseIf (ba(i) And &HFC) = &HF8 AndAlso (i + 4 < ba.Length) Then
        '        '            If Not ((ba(i + 1) And &HC0) = &H80 And (ba(i + 2) And &HC0) = &H80 And (ba(i + 3) And &HC0) = &H80 And (ba(i + 4) And &HC0) = &H80) Then
        '        '                onUTF8 = False
        '        '            End If
        '        '            i += 4
        '        '        ElseIf (ba(i) And &HFE) = &HFC AndAlso (i + 5 < ba.Length) Then
        '        '            If Not ((ba(i + 1) And &HC0) = &H80 And (ba(i + 2) And &HC0) = &H80 And (ba(i + 3) And &HC0) = &H80 And (ba(i + 4) And &HC0) = &H80 And (ba(i + 5) And &HC0) = &H80) Then
        '        '                onUTF8 = False
        '        '            End If
        '        '            i += 5
        '        '        Else
        '        '            onUTF8 = False
        '        '        End If
        '        '    End If
        '        '    If Not onUTF8 Then
        '        '        MsgBox("Vigane faili kodeering, XML deklaratsioonis on märgitud" & vbCrLf & vbCrLf & _
        '        '                "'<?xml version=""1.0"" encoding=""utf-8""?>'," & vbCrLf & vbCrLf & _
        '        '                "faili kodeering seda ei kinnita." & vbCrLf & _
        '        '                "Salvesta fail 'utf-8' kodeeringus!", MsgBoxStyle.Critical, xmlFile)
        '        '        Me.Enabled = True
        '        '        Exit Sub
        '        '    End If
        '        'Next
        '        enc = New System.Text.UTF8Encoding(False, True)
        '    Else
        '        enc = System.Text.Encoding.GetEncoding(xmlEncStr)
        '    End If
        'End If


        Dim enc As Encoding = New UTF8Encoding(False, True)

        Dim failiSisu As String = String.Empty

        Try
            failiSisu = enc.GetString(ba)
        Catch ex As Exception
            If fileEncStr.Length > 0 Then
                enc = Encoding.GetEncoding(fileEncStr)
            ElseIf xmlEncStr.Length > 0 Then
                enc = Encoding.GetEncoding(xmlEncStr)
            Else
                enc = Encoding.Default
            End If
            failiSisu = enc.GetString(ba)
        End Try

        'mõnikord läheb kommentaari lõpp lausa järgmistele ridadele ...
        Dim kommPtrn As String = "\<!\-\-.*?\-\-\>"
        'Dim mesk As MatchCollection = Regex.Matches(failiSisu, kommPtrn, RegexOptions.Singleline)
        'For Each m As Match In mesk
        '    Console.WriteLine(m.Value)
        'Next

        failiSisu = Regex.Replace(failiSisu, kommPtrn, String.Empty, RegexOptions.Singleline)

        'Võõrsõnade leksikonis algselt olnud muutujad
        '
        'failiSisu = failiSisu.Replace("&aacute;", "á")
        'failiSisu = failiSisu.Replace("&uacute;", "ú")
        'failiSisu = failiSisu.Replace("&agrave;", "à")
        'failiSisu = failiSisu.Replace("&egrave;", "è")
        'failiSisu = failiSisu.Replace("&Egrave;", "È")
        'failiSisu = failiSisu.Replace("&acirc;", "â")
        'failiSisu = failiSisu.Replace("&ecirc;", "ê")
        'failiSisu = failiSisu.Replace("&icirc;", "î")
        'failiSisu = failiSisu.Replace("&ocirc;", "ô")
        'failiSisu = failiSisu.Replace("&ucirc;", "û")
        'failiSisu = failiSisu.Replace("&iuml;", "ï")
        'failiSisu = failiSisu.Replace("&ccedil;", "ç")
        'failiSisu = failiSisu.Replace("&ntilde;", "ň")
        'failiSisu = failiSisu.Replace("&nool;", "→")

        Dim errs As New frmShowParseErrors
        errs.Text &= "'" & xmlFile & "'"
        Dim lv As ListView = errs.lvErrors

        Dim _nsm As New XmlNamespaceManager(_nt)
        _nsm.AddNamespace(_xml_Pr, _xml_Uri)
        Dim xpc As New XmlParserContext(_nt, _nsm, Nothing, XmlSpace.Default, enc)

        Dim artAlgusRexStr As String = "\<" & artQN & "(\s[^<>]+)?\>"
        Dim artLõppStr As String = "</" & artQN & ">"
        Dim artLõppPos As Integer = 0
        Dim vigu As Integer = 0
        Dim artCnt As Integer = 0
        Dim ms As String = String.Empty

        Dim mes As MatchCollection = Regex.Matches(failiSisu, artAlgusRexStr)

        Dim onMuutujaid As Boolean = False

        For Each m As Match In mes
            artCnt += 1
            artLõppPos = failiSisu.IndexOf(artLõppStr, m.Index)
            Dim artStr As String = failiSisu.Substring(m.Index, artLõppPos - m.Index + artLõppStr.Length)
            Dim seeArtikkel As String = artStr
            artStr = Regex.Replace(artStr, "[\r\n]", " ")
            reader = XmlReader.Create(New MemoryStream(enc.GetBytes(artStr)), xmlRs, xpc)
            ms = String.Empty
            Try
                While reader.Read
                    If reader.LocalName = "m" Or reader.LocalName = "ms" Or reader.LocalName = "ter" Then
                        ms = reader.ReadString
                    End If
                End While
            Catch ex As XmlException
                If Not ex.Message.StartsWith("Reference to undeclared entity ") Then
                    vigu += 1
                    Dim off As Integer = enc.GetByteCount(failiSisu.Substring(0, m.Index))
                    Dim off2 As Integer
                    'kui midagi lõpeb ootamatult, siis ex.LinePosition on stringi taga
                    If ex.LinePosition < artStr.Length Then
                        off2 = enc.GetByteCount(artStr.Substring(0, ex.LinePosition))
                        seeArtikkel = seeArtikkel.Insert(ex.LinePosition - 1, "▉")
                    End If
                    artStr = Regex.Replace(artStr, "\s+", " ")
                    'Console.WriteLine()
                    'Console.WriteLine(off)
                    'Console.WriteLine("Offset: {0}: '{1}' - {2}", off + off2, ex.Message, artStr)
                    'Console.WriteLine("Viga: {0} Ms: '{1}' - {2}", ex.Message, ms, artLN)
                    Dim lvi As New ListViewItem(off + off2)
                    lvi.SubItems.Add(vigu)
                    lvi.SubItems.Add(ex.Message)
                    lvi.SubItems.Add(ms)
                    lvi.SubItems.Add(artStr)
                    lvi.Tag = seeArtikkel
                    lv.Items.Add(lvi)
                Else
                    onMuutujaid = True
                End If
            End Try
            reader.Close()
        Next
        mes = Nothing
        Console.WriteLine(artCnt & " artiklit.")

        If vigu > 0 Then
            errs.Text &= ", " & vigu & " viga."
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            lv.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize)
            errs.ShowDialog()
            errs = Nothing
            Me.Enabled = True
            Exit Sub
        End If

        errs = Nothing

        failiSisu = Regex.Replace(failiSisu, "&(?!amp;)", "&amp;")

        Dim memStrm As New MemoryStream(enc.GetBytes(failiSisu))
        reader = XmlReader.Create(memStrm, xmlRs)
        Try
            schemaSet = infer.InferSchema(reader)
        Catch ex As XmlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "XmlException: " & fne)
            reader.Close()
            Me.Enabled = True
            Exit Sub
        End Try
        reader.Close()
        memStrm.Seek(0, SeekOrigin.Begin)

        xmlDom = New XmlDocument
        reader = XmlReader.Create(memStrm, xmlRs)
        xmlDom.Load(reader)

        reader.Close()
        reader = Nothing
        memStrm.Dispose()
        failiSisu = Nothing
        ba = Nothing

        schemaSet.Compile()

        Dim xsdFile As String = String.Empty
        Dim tnPr As String = String.Empty
        Dim xFile As String = String.Empty

        Dim wrs As New XmlWriterSettings
        wrs.Encoding = New System.Text.UTF8Encoding
        wrs.Indent = True
        Dim writer As XmlWriter

        schema = Nothing

        xPr = String.Empty
        xUri = String.Empty

        For Each s As XmlSchema In schemaSet.Schemas()
            If (s.TargetNamespace = xmlUri) Then 'kui oled kasutanud 'xml' prefiksit
                tnPr = "xml"
            ElseIf (s.TargetNamespace Is Nothing) Then 'ilma prefiksiteta elementide kasutamise korral
                tnPr = "{}"
                If xPr = String.Empty Then
                    xPr = "NULL"
                    xsdFile = fdir & "\" & fn & "_" & tnPr & ".xsd"
                    schema = s
                End If
            Else
                Dim xqnid() As XmlQualifiedName = s.Namespaces.ToArray
                For i As Integer = 0 To xqnid.Length - 1
                    If (xqnid(i).Namespace = s.TargetNamespace) Then
                        tnPr = xqnid(i).Name 'siin on prefiksi väärtus
                    End If
                Next i
                xPr = tnPr
                xUri = s.TargetNamespace
                xsdFile = fdir & "\" & fn & "_" & tnPr & ".xsd"
                schema = s
            End If
            xFile = fdir & "\" & fn & "_" & tnPr & ".xsd"
            writer = XmlWriter.Create(xFile, wrs)
            s.Write(writer)
            writer.Close()
        Next


        schemaDom = New XmlDocument
        schemaDom.Load(xsdFile)


        nsm = New XmlNamespaceManager(schemaDom.NameTable)
        nsm.AddNamespace(xPr, xUri)
        nsm.AddNamespace(xsPr, xsUri)

        'Dim ibr As XmlElement = schemaDom.DocumentElement.SelectSingleNode("xs:element[1]", nsm)

        'Dim lNimed As New SortedList

        'Dim sisemised As XmlNodeList = schemaDom.DocumentElement.SelectNodes("xs:element[@name]//xs:element[@name]", nsm)
        'While sisemised.Count > 0
        '    For Each sisemine As XmlElement In sisemised
        '        Dim lNimi As String = sisemine.GetAttribute("name")
        '        Dim rootSchemaDefs As XmlNodeList = schemaDom.DocumentElement.SelectNodes("xs:element[@name = '" & lNimi & "']", nsm)
        '        Dim defLeitud As Boolean = False
        '        For Each rootSchemaDef As XmlElement In rootSchemaDefs
        '            If (rootSchemaDef.InnerXml = sisemine.InnerXml) Then
        '                defLeitud = True
        '                Exit For
        '            End If
        '        Next
        '        If Not (defLeitud) Then
        '            If (rootSchemaDefs.Count = 0) Then
        '                lNimed.Add(lNimi, 1)
        '                ibr = schemaDom.DocumentElement.InsertBefore(sisemine.Clone, ibr)
        '                Call ibr.RemoveAttribute("minOccurs")
        '                Call ibr.RemoveAttribute("maxOccurs")
        '            Else
        '                lNimed(lNimi) += 1
        '                Dim lisatud As XmlElement = schemaDom.DocumentElement.InsertBefore(sisemine.Clone, rootSchemaDefs(rootSchemaDefs.Count - 1).NextSibling)
        '                Call lisatud.RemoveAttribute("minOccurs")
        '                Call lisatud.RemoveAttribute("maxOccurs")
        '            End If
        '        End If
        '        Call sisemine.RemoveAttribute("name")
        '        Call sisemine.SetAttribute("ref", xPr & ":" & lNimi)
        '        Call sisemine.RemoveAttribute("type")
        '        sisemine.IsEmpty = True
        '        Exit For
        '    Next
        '    sisemised = schemaDom.DocumentElement.SelectNodes("xs:element[@name]//xs:element[@name]", nsm)
        'End While

        'writer = XmlWriter.Create(xFile2.Substring(0, xFile2.LastIndexOf(".")) & "_.xml", wrs)
        'schemaDom.Save(writer)
        'writer.Close()

        'For Each lNimi As String In lNimed.Keys
        '    If (lNimed(lNimi) > 1) Then
        '        Console.WriteLine("<{0}>, {1} tk", lNimi, lNimed(lNimi))
        '        Dim korduvad As XmlNodeList = schemaDom.DocumentElement.SelectNodes("xs:element[@name = '" & lNimi & "']", nsm)
        '        Dim esimene As XmlElement = korduvad(0)
        '        For i As Integer = 1 To korduvad.Count - 1
        '            Dim defid As XmlNodeList = korduvad(i).SelectNodes("*", nsm)
        '            For Each def As XmlElement In defid
        '                Call lisaDefid(def, esimene)
        '            Next
        '            Call korduvad(i).ParentNode.RemoveChild(korduvad(i))
        '        Next
        '        If (esimene.HasChildNodes) Then
        '            Call esimene.RemoveAttribute("type")
        '        End If
        '    End If
        'Next

        'writer = XmlWriter.Create(xFile2, wrs)
        'schemaDom.Save(writer)
        'writer.Close()

        Call fillTV()

        Me.Text = wTitle & ": [" & fne & "]"

        tsmiFileSaveAs.Text = "Salvesta '" & fne & "' kui ..."
        tsmiFileSaveAs.Enabled = True
        tsmiToolsXMLSplit.Enabled = True
        tsmiToolsEnumsSschemas.Enabled = True
        tsmiToolsToExcel.Enabled = True



        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)
        Dim timeStr As String = "Ava: " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds)

        Console.WriteLine()
        Console.WriteLine(timeStr)
        Console.WriteLine()
        tsslTime.Text = timeStr
        'MsgBox("OK..." & vbCrLf & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds))

    End Sub

    Private Sub tsmiFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiFileOpen.Click

        Call avaXMLFail()

    End Sub 'tsmiFileOpen_Click


    Private Sub fillTV()
        tvSchema.Nodes.Clear()
        Dim sRoot As XmlElement = schemaDom.DocumentElement
        Dim tRoot As New TreeNode("<" & sRoot.Name & ">")
        tRoot.Tag = sRoot.Name
        For Each att As XmlAttribute In sRoot.Attributes
            Dim rAttNode As New TreeNode("@" & att.OuterXml)
            rAttNode.ForeColor = Color.Red
            tRoot.Nodes.Add(rAttNode)
        Next
        tvSchema.Nodes.Add(tRoot)
        Call fillRecursive(sRoot, tRoot)
        tvSchema.ExpandAll()
        tvSchema.SelectedNode = tRoot
    End Sub 'fillTV

    Private Sub fillRecursive(ByVal xsdEl As XmlElement, ByVal tn As TreeNode)
        For Each chEl As XmlElement In xsdEl.SelectNodes("*", nsm)
            If (chkAllElements.Checked Or (chEl.LocalName = "element" Or chEl.LocalName = "attribute")) Then
                Dim chTekst As String = String.Empty
                Dim chName As String = String.Empty
                chName = chEl.GetAttribute("ref")
                If (chName.Length = 0) Then
                    chName = chEl.GetAttribute("name")
                    If (chName.Length > 0 AndAlso chName.IndexOf(":") < 0) Then
                        chName = xPr & ":" & chName
                    End If
                End If
                If (chkOnlyNames.Checked) Then
                    If (chEl.LocalName = "attribute") Then
                        chTekst = "@" & chName
                    ElseIf (chEl.LocalName = "element") Then
                        chTekst = "<" & chName & ">"
                    End If
                Else
                    chTekst = chEl.Name
                    For Each att As XmlAttribute In chEl.Attributes
                        chTekst &= " " & att.OuterXml
                    Next
                    chTekst = "<" & chTekst & ">"
                End If
                Dim chTn As New TreeNode(chTekst)
                If (chEl.LocalName = "attribute") Then
                    chTn.Tag = "@" & chName
                Else
                    chTn.Tag = chName
                End If
                'frmDlgElemsAsText vasaku ListView jaoks
                If (chName.Length > 0) Then
                    Dim xmlNimi As String = chTn.Tag.ToString
                    If Not (xmlNimed.ContainsKey(xmlNimi)) Then
                        xmlNimed.Add(xmlNimi, 1)
                    Else
                        xmlNimed(xmlNimi) += 1
                    End If
                End If
                If (chEl.LocalName = "element") Then
                    If (chEl.SelectSingleNode("xs:complexType[@mixed = 'true']", nsm) IsNot Nothing) Then
                        chTn.NodeFont = boldFont
                    End If
                    If (chEl.SelectSingleNode("xs:complexType/xs:sequence/xs:choice", nsm) IsNot Nothing) Then
                        chTn.ForeColor = Color.Green
                    Else
                        chTn.ForeColor = Color.Blue
                    End If
                ElseIf (chEl.LocalName = "import") Then
                    chTn.ForeColor = Color.Maroon
                ElseIf (chEl.LocalName = "attribute") Then
                    chTn.ForeColor = Color.Red
                End If
                tn.Nodes.Add(chTn)
                Call fillRecursive(chEl, chTn)
            Else
                Call fillRecursive(chEl, tn)
            End If
        Next
    End Sub 'fillRecursive

    'Private Sub lisaDefid(ByVal sisend As XmlElement, ByVal väljund As XmlElement)
    '    Dim osa As XmlElement = väljund.SelectSingleNode(sisend.Name, nsm)
    '    If (osa Is Nothing) Then
    '        väljund.AppendChild(sisend.Clone)
    '    Else
    '        Dim defid As XmlNodeList = sisend.SelectNodes("*", nsm)
    '        For Each def As XmlElement In defid
    '            Call lisaDefid(def, osa)
    '        Next
    '    End If
    'End Sub 'lisaDefid

    Private Sub tsmiFileQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiFileQuit.Click
        Me.Close()
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        fileFromCmdLine = String.Empty

        wTitle = Me.Text
        Call clearInfo()
        boldFont = New Font(tvSchema.Font, FontStyle.Bold)
        rtbBold = New Font(rtbInfo.Font, FontStyle.Bold)
        nfi = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.Clone
        nfi.NumberDecimalDigits = 0

        excludeNames = My.Settings.excludeNodes
        namesAsText = My.Settings.namesAsText

        indCopy = New Xsl.XslCompiledTransform
        Try
            indCopy.Load(Application.StartupPath & "/xsl/indented_copy.xsl")
        Catch ex As Exception
            indCopy.Load(Application.StartupPath & "/../../xsl/indented_copy.xsl")
        End Try

        If Not (Directory.Exists(Application.StartupPath & "\tmp")) Then
            Directory.CreateDirectory(Application.StartupPath & "\tmp")
        End If
        Dim di As New DirectoryInfo(Application.StartupPath & "\tmp")
        For Each fi As FileInfo In di.GetFiles("*.*")
            fi.Delete()
        Next

        Dim cmdLineArgs As String() = Environment.GetCommandLineArgs()
        If cmdLineArgs.Length > 1 Then
            fileFromCmdLine = cmdLineArgs(1)
            Call avaXMLFail()
        End If

    End Sub 'frmMain_Load

    Private Sub clearInfo()
        tsslTime.Text = String.Empty
        tsslPath.Text = String.Empty
    End Sub 'clearInfo

    Private Sub chkAllElements_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllElements.CheckedChanged
        If (chkAllElements.Checked) Then
            chkOnlyNames.Enabled = False
            chkOnlyNames.Checked = False
        Else
            chkOnlyNames.Enabled = True
        End If
        Call clearInfo()
        If (schemaDom IsNot Nothing) Then
            Me.Enabled = False
            Call fillTV()
            Me.Enabled = True
        End If
    End Sub 'chkAllElements_CheckedChanged

    Private Sub tvSchema_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvSchema.AfterSelect
        'MsgBox("afterselect" & vbCrLf & tvSchema.SelectedNode.Text & vbCrLf & tvSchema.SelectedNode.FullPath)
        If (chkOnlyNames.Checked) Then
            tsslPath.Text = tvSchema.SelectedNode.FullPath
        Else
            tsslPath.Text = String.Empty
        End If
    End Sub 'tvSchema_AfterSelect

    Private Sub tsslTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsslTime.Click
        tsslTime.Text = String.Empty
    End Sub 'tsslTime_Click

    Private Sub chkOnlyNames_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOnlyNames.CheckedChanged
        Call clearInfo()
        If (chkOnlyNames.Enabled) Then
            If (schemaDom IsNot Nothing) Then
                Me.Enabled = False
                Call fillTV()
                Me.Enabled = True
            End If
        End If
    End Sub 'chkOnlyNames_CheckedChanged

    Private Sub btnInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInfo.Click
        If (xmlFile.Length = 0 Or tvSchema.SelectedNode Is Nothing) Then
            Exit Sub
        End If

        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False



        Call showInfo(tvSchema.SelectedNode)

        Dim vaheNimi As String = "Info-" & tvSchema.SelectedNode.Tag.Replace(":", "-") & "- "
        If (chkGlobalInfo.Checked) Then
            vaheNimi &= "glob"
        Else
            vaheNimi &= "lok"
        End If
        If (chkLoendid.Checked) Then
            vaheNimi &= " -loenditega"
        Else
            vaheNimi &= " -loenditeta"
        End If
        vaheNimi &= ".rtf"
        'Dim swr As New StreamWriter(Application.StartupPath & "/tmp/" & vaheNimi, False, New System.Text.UnicodeEncoding)
        'swr.Write(rtbInfo.Text.Replace(vbLf, vbCrLf))
        'swr.Close()
        Dim sfn As String = Application.StartupPath & "/tmp/" & vaheNimi
        'Dim swr As New StreamWriter(sfn, False, New System.Text.UnicodeEncoding)
        'swr.Write(rtbInfo.Text.Replace(vbLf, vbCrLf))
        'swr.Close()
        rtbInfo.SaveFile(sfn, RichTextBoxStreamType.RichText)



        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)
        Dim timeStr As String = "Info: " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds)

        Console.WriteLine()
        Console.WriteLine(timeStr)
        Console.WriteLine()
        tsslTime.Text = timeStr

    End Sub 'btnInfo_Click

    Private Sub showInfo(ByVal fromTn As TreeNode)

        Dim qn As String = fromTn.Tag

        Dim infoPath As String
        If (chkGlobalInfo.Checked) Then
            infoPath = ".//" & qn
        Else
            infoPath = qn
            Dim tn As TreeNode = fromTn
            Do
                If (tn.Tag = "xs:schema") Then
                    Exit Do
                End If
                tn = tn.Parent
                If (tn.Tag = "xs:schema") Then
                    Exit Do
                End If
                If (tn.Tag.ToString.Length > 0) Then
                    infoPath = tn.Tag & "/" & infoPath
                End If
            Loop
        End If

        If (xmlDom Is Nothing) Then
            xmlDom = New XmlDocument
            xmlDom.Load(xmlFile)
        End If

        Dim infod As XmlNodeList = xmlDom.SelectNodes(infoPath, nsm)
        Dim rtbTekst As String = "Silt: " & infoPath
        rtbTekst &= vbCrLf & vbTab & "# kokku " & infod.Count.ToString("N", nfi)
        rtbInfo.AppendText(rtbTekst)

        Dim elStr As String = " "
        'Dim jadad As New Dictionary(Of String, Integer)
        Dim jadad As New SortedList
        Dim jadaMärksõnad As New SortedList
        Dim asetseb As New Dictionary(Of String, Integer)

        Dim lugedaKokku As Boolean = True
        Dim väärtused As Boolean = chkLoendid.Checked
        Dim maxVals As Integer = nudListSize.Value
        Dim slVals As New SortedList

        For Each info As XmlNode In infod
            If (chkGlobalInfo.Checked) Then
                Dim kus As String = info.SelectSingleNode("..", nsm).Name
                If Not (asetseb.ContainsKey(kus)) Then
                    asetseb.Add(kus, 1)
                Else
                    asetseb(kus) += 1
                End If
            End If
            Dim jada As String = String.Empty
            Dim mida As String = String.Empty
            Dim infoTükid As XmlNodeList = info.SelectNodes("node()", nsm)
            For Each infoTükk As XmlNode In infoTükid
                If (infoTükk.NodeType = XmlNodeType.Element) Then
                    lugedaKokku = False
                    If Not (excludeNames.Contains(String.Concat(";", infoTükk.LocalName, ";"))) Then
                        If (elStr.IndexOf(String.Concat(" ", infoTükk.Name, " ")) < 0) Then
                            elStr &= infoTükk.Name & " "
                        End If
                        If (chkElContent.Checked) Then
                            If (infoTükk.InnerText.Length > nudElemLength.Value) Then
                                mida = "<" & infoTükk.Name & ">"
                            Else
                                If (namesAsText.Contains(String.Concat(";", infoTükk.LocalName, ";"))) Then
                                    mida = infoTükk.InnerText
                                Else
                                    mida = "<" & infoTükk.Name & ">"
                                End If
                            End If
                        Else
                            mida = "<" & infoTükk.Name & ">"
                        End If
                        If (jada.EndsWith(mida)) Then
                            jada &= "++"
                        ElseIf (jada.EndsWith(mida & "++")) Then
                        Else
                            jada &= mida
                        End If
                    End If
                ElseIf (infoTükk.NodeType = XmlNodeType.Text) Then
                    If (infoTükk.InnerText.Length > nudTextLength.Value) Then
                        mida = "#"
                    Else
                        mida = infoTükk.InnerText
                    End If
                    jada &= mida
                End If
            Next
            If Not (jadad.ContainsKey(jada)) Then
                jadad.Add(jada, 1)
                'Dim msEl As XmlElement = info.SelectSingleNode("ancestor-or-self::*[local-name() = '" & artLN & "']//*[local-name() = 'm' or local-name() = 'ms'][1]", nsm)
                'Dim msEl As XmlElement = info.SelectSingleNode("ancestor-or-self::" & xPr & ":" & artLN & "//*[local-name() = 'm' or local-name() = 'ms' or local-name() = 'ter'][1]", nsm)
                Dim msEl As XmlElement = info.SelectSingleNode("ancestor-or-self::" & xPr & ":" & artLN & "//" & xPr & ":" & My.Settings.msNimi & "[1]", nsm)
                If Not (msEl Is Nothing) Then
                    jadaMärksõnad.Add(jada, msEl.InnerText)
                Else
                    jadaMärksõnad.Add(jada, "-")
                End If
            Else
                jadad(jada) += 1
            End If
            If (väärtused AndAlso lugedaKokku) Then
                If Not (slVals.ContainsKey(info.InnerText)) Then
                    slVals.Add(info.InnerText, 1)
                    If (slVals.Count > maxVals) Then
                        lugedaKokku = False
                    End If
                Else
                    slVals(info.InnerText) += 1
                End If
            End If
        Next

        rtbInfo.AppendText(vbCrLf & vbTab & "# sisaldab silte: (" & elStr & ")")
        'For Each kvp As KeyValuePair(Of String, Integer) In jadad
        '    rtbInfo.AppendText(vbCrLf & vbTab & kvp.Value.ToString("N", nfi) & vbTab & "|" & kvp.Key & "|")
        'Next
        Dim rtbSelFont = rtbInfo.SelectionFont
        For jix As Integer = 0 To jadad.Count - 1
            Dim jada As String = jadad.GetKey(jix)
            Dim esines As Integer = Integer.Parse(jadad.GetByIndex(jix))
            rtbInfo.AppendText(vbCrLf & vbTab & esines.ToString("N", nfi) & vbTab & "|" & jada & "| ")
            rtbInfo.SelectionColor = Color.Maroon
            rtbInfo.SelectionFont = rtbBold
            For jmsix As Integer = 0 To jadaMärksõnad.Count - 1
                If jadaMärksõnad.GetKey(jmsix) = jada Then
                    rtbInfo.AppendText(jadaMärksõnad.GetByIndex(jmsix))
                    Exit For
                End If
            Next
            rtbInfo.SelectionFont = rtbSelFont
            rtbInfo.SelectionColor = Color.Empty
        Next
        'For Each jada As String In jadad.Keys
        '    Dim esines As Integer = Integer.Parse(jadad(jada))
        'Next

        If (chkGlobalInfo.Checked) Then
            rtbInfo.AppendText(vbCrLf & vbTab & "# asetseb:")
            For Each kvp As KeyValuePair(Of String, Integer) In asetseb
                rtbInfo.AppendText(vbCrLf & vbTab & kvp.Value.ToString("N", nfi) & vbTab & "<" & kvp.Key & ">")
            Next
        End If

        If (väärtused AndAlso lugedaKokku) Then
            rtbTekst = vbCrLf & vbTab & "# väärtused: " & slVals.Count.ToString("N", nfi)
            If (slVals.Count = maxVals) Then
                rtbTekst &= "(+)"
            End If
            rtbTekst &= " erinevat."
            rtbInfo.AppendText(rtbTekst)
            For Each väärtus As String In slVals.Keys
                rtbInfo.AppendText(vbCrLf & vbTab & Integer.Parse(slVals(väärtus)).ToString("N", nfi) & _
                    vbTab & "'" & väärtus & "'")
            Next
        End If

        rtbInfo.AppendText(vbCrLf & vbCrLf)

    End Sub 'showInfo


    Private Sub showQuickInfo(ByVal fromTn As TreeNode)
        Dim qn As String = fromTn.Tag

        Dim infoPath As String
        If (chkGlobalInfo.Checked) Then
            infoPath = ".//" & qn
        Else
            infoPath = qn
            Dim tn As TreeNode = fromTn
            Do
                If (tn.Tag = "xs:schema") Then
                    Exit Do
                End If
                tn = tn.Parent
                If (tn.Tag = "xs:schema") Then
                    Exit Do
                End If
                If (tn.Tag.ToString.Length > 0) Then
                    infoPath = tn.Tag & "/" & infoPath
                End If
            Loop
        End If

        If Not (tehtud.ContainsKey(infoPath)) Then
            tehtud.Add(infoPath, 1)
            If Not (excludeNames.Contains(String.Concat(";", getFindKey(fromTn.Tag), ";"))) Then
                Dim infod As XmlNodeList = xmlDom.SelectNodes(infoPath, nsm)
                Dim rtbTekst As String = "Silt: " & infoPath & ", " & infod.Count.ToString("N", nfi) & vbCrLf
                rtbInfo.AppendText(rtbTekst)
                For Each tn1 As TreeNode In fromTn.Nodes
                    Call showQuickInfo(tn1)
                Next
            End If
        End If

    End Sub 'showQuickInfo

    Private Sub showSeldInfo()
        rtbInfo.Clear()
        Dim kvpNr As Integer = 0
        For Each kvp As KeyValuePair(Of String, String) In seldRows
            If (kvpNr > 0) Then
                Exit For
            End If
            kvpNr += 1
            Dim filter As String = String.Empty
            If kvp.Value.Length > 0 Then
                Dim tükid() As String = Regex.Split(kvp.Value, "(\<[\w:]+?\>)") 'ON "capturing" group, tulevad ise ka sisse
                Dim jnr As Integer = 0
                For tükkJnr As Integer = 0 To tükid.GetUpperBound(0)
                    Dim tükk As String = tükid(tükkJnr)
                    If Not (tükk.Length = 0 Or tükk = "++") Then
                        If (jnr > 0) Then
                            filter &= "/following-sibling::node()[1]"
                        Else
                            filter &= "node()[1]"
                        End If
                        If (tükk.StartsWith("<") And tükk.EndsWith(">")) Then
                            filter &= "[name() = '" & tükk.Substring(1, tükk.Length - 2) & "']"
                        ElseIf (tükk = "#") Then
                            filter &= "[self::text()]"
                        Else
                            filter &= "[self::text()][. = '" & tükk & "']"
                        End If
                        If (tükkJnr < tükid.GetUpperBound(0) AndAlso tükid(tükkJnr + 1) = "++") Then
                            filter &= "/following-sibling::" & tükk.Substring(1, tükk.Length - 2)
                        End If
                        jnr += 1
                    End If
                Next
                filter = "[" & filter & "[not(following-sibling::node())]]"
            Else
                filter = "[not(node())]"
            End If
            Console.WriteLine(filter)
            Dim valimDom As New XmlDocument
            Dim infod As XmlNodeList = xmlDom.SelectNodes(kvp.Key & filter, nsm)
            valimDom.AppendChild(valimDom.CreateComment("- " & kvp.Key & " - |" & kvp.Value & "|: " & infod.Count.ToString("N", nfi) & " tk."))
            Dim valimRoot As XmlElement = valimDom.AppendChild(valimDom.CreateNode(XmlNodeType.Element, xPr, "valim", xUri))
            For Each info As XmlNode In infod
                Dim esita As XmlElement = info.SelectSingleNode("ancestor-or-self::*[local-name() = '" & My.Settings.artNimi & "']", nsm)
                'nt mst.xml
                If esita IsNot Nothing Then
                    Call valimRoot.AppendChild(valimDom.ImportNode(esita, True))
                End If
            Next

            Dim vaheNimi As String = "Valim- " & kvp.Key.Replace(".", "_").Replace("/", "_").Replace(":", "-")
            vaheNimi &= "- '" & kvp.Value.Replace("<", "_").Replace(">", "_").Replace(":", "-") & "'"
            Dim xmlWrS As XmlWriterSettings = indCopy.OutputSettings
            Dim sfn As String = Application.StartupPath & "/tmp/" & vaheNimi & ".txt"
            Dim xmlWr As XmlWriter = XmlWriter.Create(sfn, xmlWrS)
            indCopy.Transform(valimDom, Nothing, xmlWr)
            xmlWr.Close()

            rtbInfo.LoadFile(sfn, RichTextBoxStreamType.UnicodePlainText)
            rtbInfo.AppendText(vbCrLf & vbCrLf)
            sfn = sfn.Substring(0, sfn.LastIndexOf(".")) & ".rtf"
            rtbInfo.SaveFile(sfn, RichTextBoxStreamType.RichText)
        Next
    End Sub 'showSeldInfo

    Private Sub btnClearInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearInfo.Click
        rtbInfo.Clear()
    End Sub 'btnClearInfo_Click

    Private Sub btnAllInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllInfo.Click
        If (xmlFile.Length = 0) Then
            Exit Sub
        End If

        Call clearInfo()

        seldRows = New Dictionary(Of String, String)
        Dim seldKey As String = String.Empty
        Dim seldVal As String
        For Each rida As String In rtbInfo.Lines
            If (rida.StartsWith("Silt: ")) Then
                seldKey = rida.Substring(6)
            End If
            If (rida.StartsWith("+") Or rida.StartsWith("?")) Then
                If (Regex.IsMatch(rida, "\s\d+\s\|.*?\|")) Then
                    seldVal = rida.Substring(rida.IndexOf("|") + 1)
                    seldVal = seldVal.Substring(0, seldVal.IndexOf("|"))
                    seldRows.Add(seldKey, seldVal)
                End If
            End If
        Next
        If (seldRows.Count > 0) Then
            'tegevus algab
            Dim seldstart As DateTime = Now
            Me.Enabled = False



            Call showSeldInfo()



            'tegevus lõppes
            Me.Enabled = True
            Dim ts2 As TimeSpan = Now.Subtract(seldstart)
            Dim timeStr2 As String = "Valim: " & String.Format("{0}h, {1}m, {2}s", ts2.Hours, ts2.Minutes, ts2.Seconds)

            Console.WriteLine()
            Console.WriteLine(timeStr2)
            Console.WriteLine()
            tsslTime.Text = timeStr2
            Return
        End If

        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False



        Dim algus As TreeNode
        'If (tvSchema.SelectedNode Is Nothing OrElse (tvSchema.SelectedNode Is tvSchema.Nodes(0))) Then
        '    algus = tvSchema.Nodes(0)
        'Else
        '    algus = tvSchema.SelectedNode
        '    If (algus.Tag IsNot Nothing) Then
        '        If (algus.Tag.ToString.Length > 0) Then
        '            If Not (tehtud.ContainsKey(algus.Tag)) Then
        '                Call showInfo(algus)
        '                If (chkGlobalInfo.Checked) Then
        '                    tehtud.Add(algus.Tag, 1)
        '                End If
        '            End If
        '        End If
        '    End If
        'End If
        If (tvSchema.SelectedNode Is Nothing OrElse (tvSchema.SelectedNode Is tvSchema.Nodes(0))) Then
            algus = tvSchema.Nodes(0).FirstNode
        Else
            algus = tvSchema.SelectedNode
        End If
        Do While algus.Tag Is Nothing
            algus = algus.NextNode
        Loop

        rtbInfo.Clear()
        tehtud = New SortedList
        Call showQuickInfo(algus)
        rtbInfo.AppendText(vbCrLf & vbCrLf)


        tehtud = New SortedList
        If (algus.Tag IsNot Nothing) Then
            If (algus.Tag.ToString.Length > 0) Then
                If Not (tehtud.ContainsKey(algus.Tag)) Then
                    Call showInfo(algus)
                    If (chkGlobalInfo.Checked) Then
                        tehtud.Add(algus.Tag, 1)
                    End If
                End If
            End If
        End If
        For Each tn1 As TreeNode In algus.Nodes
            Dim naado As Boolean = True
            If (tn1.Tag IsNot Nothing) Then
                If (tn1.Tag.ToString.Length > 0) Then
                    If Not (tehtud.ContainsKey(tn1.Tag)) Then
                        If (excludeNames.Contains(String.Concat(";", getFindKey(tn1.Tag), ";"))) Then
                            naado = False
                        End If
                        If (naado) Then
                            Call showInfo(tn1)
                            If (chkGlobalInfo.Checked) Then
                                tehtud.Add(tn1.Tag, 1)
                            End If
                        End If
                    End If
                End If
            End If
            If (naado) Then
                Call showChildInfo(tn1)
            End If
        Next

        Dim vaheNimi As String = "Kõik-" & algus.Tag.Replace(":", "-") & "- "
        If (chkGlobalInfo.Checked) Then
            vaheNimi &= "glob"
        Else
            vaheNimi &= "lok"
        End If
        If (chkLoendid.Checked) Then
            vaheNimi &= " -loenditega"
        Else
            vaheNimi &= " -loenditeta"
        End If
        vaheNimi &= ".rtf"
        Dim sfn As String = Application.StartupPath & "/tmp/" & vaheNimi
        'Dim swr As New StreamWriter(sfn, False, New System.Text.UnicodeEncoding)
        'swr.Write(rtbInfo.Text.Replace(vbLf, vbCrLf))
        'swr.Close()
        rtbInfo.SaveFile(sfn, RichTextBoxStreamType.RichText)




        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)
        Dim timeStr As String = "Kõik: " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds)

        Console.WriteLine()
        Console.WriteLine(timeStr)
        Console.WriteLine()
        tsslTime.Text = timeStr
        'MsgBox("OK..." & vbCrLf & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds))

    End Sub 'btnAllInfo_Click

    Private Function getFindKey(ByVal tnTag As String) As String
        Dim qn As String = tnTag
        Dim fk As String 'findKey
        If (qn.StartsWith("@")) Then
            fk = qn.Substring(1)
            fk = "@" & fk.Substring(fk.IndexOf(":") + 1)
        Else
            fk = qn.Substring(qn.IndexOf(":") + 1)
        End If
        Return fk
    End Function 'getFindKey

    Private Sub showChildInfo(ByVal tn As TreeNode)
        For Each tn1 As TreeNode In tn.Nodes
            Dim naado As Boolean = True
            If (tn1.Tag IsNot Nothing) Then
                If (tn1.Tag.ToString.Length > 0) Then
                    If Not (tehtud.ContainsKey(tn1.Tag)) Then
                        If (excludeNames.Contains(String.Concat(";", getFindKey(tn1.Tag), ";"))) Then
                            naado = False
                        End If
                        If (naado) Then
                            Call showInfo(tn1)
                            If (chkGlobalInfo.Checked) Then
                                tehtud.Add(tn1.Tag, 1)
                            End If
                        End If
                    End If
                End If
            End If
            If (naado) Then
                Call showChildInfo(tn1)
            End If
        Next
    End Sub 'showChildInfo

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        'salvestamine
        'All files (*.*)|*.*|Text files (*.txt)|*.txt|
        'XML files (*.xml)|*.xml|XSD files (*.xsd)|*.xsd|
        'xmlStats files (*.xmlStats)|*.xmlStats|RTF files (*.rtf)|*.rtf
        If (rtbInfo.Text.Length = 0) Then
            Exit Sub
        End If

        Dim dlgResult As DialogResult

        dlgSFD.Filter = "xmlStats files (*.xmlStats)|*.xmlStats|RTF files (*.rtf)|*.rtf"
        dlgSFD.FilterIndex = 1 'xmlStats
        dlgSFD.FileName = fn
        dlgSFD.Title = "Väljund: salvesta 'xmlStats'"
        dlgResult = dlgSFD.ShowDialog()
        If (dlgResult <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        ''Dim rtbst As RichTextBoxStreamType
        ''If (dlgSFD.FilterIndex = 6) Then 'RTF
        ''    rtbst = RichTextBoxStreamType.RichText
        ''Else
        ''    rtbst = RichTextBoxStreamType.UnicodePlainText
        ''End If
        ''Dim userInput As New MemoryStream
        ''rtbInfo.SaveFile(userInput, rtbst)
        ''Dim fileStream As Stream = dlgSFD.OpenFile
        ''userInput.Position = 0
        ''userInput.WriteTo(fileStream)
        ''fileStream.Close()

        If (dlgSFD.FilterIndex = 2) Then 'RTF
            rtbInfo.SaveFile(dlgSFD.FileName, RichTextBoxStreamType.RichText)
        Else
            Dim swr As New StreamWriter(dlgSFD.FileName, False, New System.Text.UnicodeEncoding)
            swr.Write(rtbInfo.Text.Replace(vbLf, vbCrLf))
            swr.Close()
        End If

    End Sub 'btnSave_Click

    Private Sub tsmiToolsPrefs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiToolsPrefs.Click
        frmDlgPrefs.ShowDialog()
    End Sub

    Private Sub tsmiToolsToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiToolsToExcel.Click

        If (xmlDom Is Nothing) Then
            Exit Sub
        End If

        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False



        Dim dicPr As String = xPr
        Dim dicUri As String = xUri


        Dim mse As New Excel.Application

        mse.Visible = False
        mse.SheetsInNewWorkbook = 13
        'mse.UserControl = False 'Excel objekt kaob pärast ise

        'Vajalik KONFF
        'iga Exceli pakk tahab omakeelset library-t. kui on installitud EN-US Excel ja Regional Settings-is on teised
        'asjad, tekib viga (SUUR JAMA ???)
        '
        'Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")


        Dim wbk As Excel.Workbook = mse.Workbooks.Add()


        'Üldinfo
        Dim wsh1 As Excel.Worksheet = wbk.Worksheets(1)
        wsh1.Name = "Üldinfo"

        Dim wsh As Excel.Worksheet
        wsh = wsh1

        Dim r As Excel.Range = wsh1.Cells(1, 1)
        r.Value = "Köited:"
        r.Font.Bold = True

        Dim reaNr As Integer = 2
        Dim kogum As XmlNodeList
        Dim slKogum As SortedList
        Dim slMaxNr As SortedList

        kogum = xmlDom.DocumentElement.SelectNodes(dicPr & ":A", nsm)
        'Console.WriteLine("Kokku {0} artiklit.", kogum.Count)
        reaNr += 1
        wsh.Cells(reaNr, 1).Value = "Artikleid"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1

        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":m", nsm)
        'Console.WriteLine("Märksõnu:         {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Märksõnu"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":m[@" & dicPr & ":liik = 'z']", nsm)
        'Console.WriteLine("Tsitaatmärksõnu:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Tsitaatmärksõnu (m[@liik = 'z'])"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":m[@" & dicPr & ":liik = 'p']", nsm)
        'Console.WriteLine("Pärisnimesid:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Pärisnimesid (m[@liik = 'p'])"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":m[@" & dicPr & ":liik = 'f']", nsm)
        Dim frCnt As Integer = kogum.Count
        'Console.WriteLine("Fraseologisme:  {0}", frCnt)
        wsh.Cells(reaNr, 1).Value = "Fraseologisme (m[@liik = 'f'])"
        wsh.Cells(reaNr, 2).Value = frCnt
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":m[@" & dicPr & ":liik = 'y']", nsm)
        Dim yCnt As Integer = kogum.Count
        'Console.WriteLine("Ühendeid:  {0}", yCnt)
        wsh.Cells(reaNr, 1).Value = "Ühendeid (m[@liik = 'y'])"
        wsh.Cells(reaNr, 2).Value = yCnt
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":m[@" & dicPr & ":liik = 'l']", nsm)
        Dim lCnt As Integer = kogum.Count
        'Console.WriteLine("Ühendeid:  {0}", lCnt)
        wsh.Cells(reaNr, 1).Value = "Lühendeid (m[@liik = 'l'])"
        wsh.Cells(reaNr, 2).Value = lCnt
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":m[@" & dicPr & ":i]", nsm)
        'Console.WriteLine("Homonüüme:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Homonüümseid (m[@i])"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1

        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//*[name() = '" & dicPr & ":mvt' or name() = '" & dicPr & ":mvty' or name() = '" & dicPr & ":tvt']", nsm)
        'Console.WriteLine("Viiteid:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Viiteid (mvt, mvty, tvt)"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":ld", nsm)
        'Console.WriteLine("Ladina termineid:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Ladina termineid (ld)"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":cg", nsm)
        'Console.WriteLine("Tsitaadigruppe:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Tsitaadigruppe (cg)"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":caut", nsm)
        'Console.WriteLine("Tõlketsitaate:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Tsitaate (caut)"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":caut[@" & dicPr & ":aliik = 'tlk']", nsm)
        'Console.WriteLine("Tõlketsitaate:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Tõlketsitaate (caut[@aliik = 'tlk'])"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":caut[not(@" & dicPr & ":aliik)]", nsm)
        'Console.WriteLine("Tõlketsitaate:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Teisi tsitaate (caut[not(@aliik)])"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":ls", nsm)
        'Console.WriteLine("Liitsõnu:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Liitsõnanäiteid (ls)"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1

        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":v", nsm)
        Dim slVallad As New SortedList
        '<s>, <v>, <sl> on kõik complexType textonly
        For Each vald As XmlElement In kogum
            If Not (slVallad.ContainsKey(vald.InnerText)) Then
                slVallad.Add(vald.InnerText, 1)
            Else
                slVallad(vald.InnerText) += 1
            End If
        Next
        'Console.WriteLine("Erialamärgendeid:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Erialamärgendeid"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":s", nsm)
        Dim slStiilid As New SortedList
        '<s>, <v>, <sl> on kõik complexType textonly
        For Each stiil As XmlElement In kogum
            If Not (slStiilid.ContainsKey(stiil.InnerText)) Then
                slStiilid.Add(stiil.InnerText, 1)
            Else
                slStiilid(stiil.InnerText) += 1
            End If
        Next
        'Console.WriteLine("Stiile:  {0}", kogum.Count)
        wsh.Cells(reaNr, 1).Value = "Stiile"
        wsh.Cells(reaNr, 2).Value = kogum.Count
        reaNr += 1

        wsh.Cells.Columns.AutoFit()
        'wsh.Cells.Columns(2).NumberFormat = "#,##0"


        'homonüümsed märksõnad
        Dim wsh2 As Excel.Worksheet = wbk.Worksheets(2)
        wsh2.Name = "Homonüümsed"
        wsh2.Activate()

        wsh = wsh2
        reaNr = 1

        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "Märksõna"
        wsh.Cells(reaNr, 2).Value = "Mitu korda esines"
        wsh.Cells(reaNr, 3).Value = "Maks. hom. nr."
        reaNr += 1

        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":m[@" & dicPr & ":i]", nsm)
        slKogum = New SortedList
        slMaxNr = New SortedList
        For Each homm As XmlElement In kogum
            Dim msTekst As String = homm.InnerText.Trim
            msTekst = Regex.Replace(msTekst, "[\W]", String.Empty)
            Dim iVal As Integer = Integer.Parse(homm.GetAttribute("i", dicUri))
            If Not (slKogum.ContainsKey(msTekst)) Then
                slKogum.Add(msTekst, 1)
            Else
                slKogum(msTekst) += 1
            End If
            If Not (slMaxNr.ContainsKey(msTekst)) Then
                slMaxNr.Add(msTekst, iVal)
            Else
                If (iVal > slMaxNr(msTekst)) Then
                    slMaxNr(msTekst) = iVal
                End If
            End If
        Next
        For Each homMs As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = homMs
            wsh.Cells(reaNr, 2).Value = slKogum(homMs)
            wsh.Cells(reaNr, 3).Value = slMaxNr(homMs)
            If (slKogum(homMs) = 1 Or slKogum(homMs) <> slMaxNr(homMs)) Then
                r = wsh.Cells.Rows(reaNr)
                r.Font.ColorIndex = 3 'punane
                r.Font.Bold = True
            End If
            reaNr += 1
        Next
        reaNr += 1
        wsh.Cells(reaNr, 1).Value = "Homonüümseid märksõnu"
        wsh.Cells(reaNr, 2).Value = slKogum.Count

        wsh.Cells.Columns.AutoFit()
        With mse.ActiveWindow
            .SplitColumn = 0
            .SplitRow = 1
        End With
        mse.ActiveWindow.FreezePanes = True


        'freaseologismid ja ühendid
        Dim wsh3 As Excel.Worksheet = wbk.Worksheets(3)
        wsh3.Name = "Ms. f, y"
        wsh3.Activate()

        Dim slKogum2 As SortedList
        Dim slKogum3 As SortedList

        wsh = wsh3
        reaNr = 1

        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "Põhisõna (@ps)"
        wsh.Cells(reaNr, 2).Value = "Fraseologisme"
        wsh.Cells(reaNr, 3).Value = "Ühendeid"
        reaNr += 1

        slKogum = New SortedList 'frassid
        slKogum2 = New SortedList 'ühendid
        slKogum3 = New SortedList 'ei kumbki ehk vigased
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":m[@" & dicPr & ":ps]", nsm)
        For Each msPs As XmlElement In kogum
            Dim msTekst As String = msPs.InnerText.Trim
            Dim psVal As String = msPs.GetAttribute("ps", dicUri)
            Dim liikVal As String = msPs.GetAttribute("liik", dicUri)
            If Not (slKogum.ContainsKey(psVal)) Then
                slKogum.Add(psVal, 0)
            End If
            If Not (slKogum2.ContainsKey(psVal)) Then
                slKogum2.Add(psVal, 0)
            End If
            If (liikVal = "f") Then
                slKogum(psVal) += 1
            ElseIf (liikVal = "y") Then
                slKogum2(psVal) += 1
            Else
                'MsgBox("Noo!" & vbNewLine & msPs.InnerText)
                If Not (slKogum3.ContainsKey(psVal)) Then
                    slKogum3.Add(psVal, "m='" & msTekst & "'")
                End If
            End If
        Next
        For Each fr As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = fr
            If (slKogum(fr) > 0) Then
                wsh.Cells(reaNr, 2).Value = slKogum(fr)
            End If
            If (slKogum2(fr) > 0) Then
                wsh.Cells(reaNr, 3).Value = slKogum2(fr)
            End If
            reaNr += 1
        Next
        reaNr += 1
        For Each fr As String In slKogum3.Keys
            wsh.Cells(reaNr, 1).Value = fr
            wsh.Cells(reaNr, 2).Value = slKogum3(fr)
            r = wsh.Cells.Rows(reaNr)
            r.Font.ColorIndex = 3 'punane
            r.Font.Bold = True
            reaNr += 1
        Next
        reaNr += 1
        wsh.Cells(reaNr, 1).Value = "Kokku"
        wsh.Cells(reaNr, 2).Value = frCnt
        wsh.Cells(reaNr, 3).Value = yCnt

        wsh.Cells.Columns.AutoFit()
        With mse.ActiveWindow
            .SplitColumn = 0
            .SplitRow = 1
        End With
        mse.ActiveWindow.FreezePanes = True


        '<m> tekstis sisalduvad mittetähed
        Dim wsh4 As Excel.Worksheet = wbk.Worksheets(4)
        wsh4.Name = "Ms. mittetähed"
        wsh4.Activate()

        wsh = wsh4
        reaNr = 1

        Dim tblAlgus As Integer = reaNr
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "Täht"
        wsh.Cells(reaNr, 2).Value = "Nr"
        wsh.Cells(reaNr, 3).Value = "Hex"
        wsh.Cells(reaNr, 4).Value = "Ms. näidis"
        reaNr += 1

        r = wsh.Cells.Columns(3)
        r.HorizontalAlignment = Excel.Constants.xlRight

        slKogum = New SortedList
        kogum = xmlDom.DocumentElement.SelectNodes(".//s:m/text()", nsm)
        'Console.WriteLine()
        For Each msTekst As XmlNode In kogum
            For Each täht As Char In msTekst.InnerText
                If Not (Char.IsLetter(täht)) Then
                    If Not (slKogum.ContainsKey(täht.ToString)) Then
                        slKogum.Add(täht.ToString, 1)
                        'Console.WriteLine("'{0}' - {2} (0x{3}): '{1}'", täht.ToString, msTekst.InnerText, AscW(täht), Hex(AscW(täht)))
                        wsh.Cells(reaNr, 1).Value = "'" & täht.ToString
                        wsh.Cells(reaNr, 2).Value = AscW(täht)
                        wsh.Cells(reaNr, 3).Value = Hex(AscW(täht))
                        wsh.Cells(reaNr, 4).Value = "'" & msTekst.InnerText
                        reaNr += 1
                    Else
                        slKogum(täht.ToString) += 1
                    End If
                End If
            Next
        Next
        Dim tblLõpp As Integer = reaNr - 1

        'Dim c1 As Excel.Range = wsh.Cells(tblAlgus, 1)
        'Dim c2 As Excel.Range = wsh.Cells(tblLõpp, 3)
        r = wsh.Range("A" & tblAlgus.ToString & ":D" & tblLõpp.ToString)
        With r.Borders
            .LineStyle = Excel.XlLineStyle.xlContinuous
            .ColorIndex = Excel.Constants.xlAutomatic
            .TintAndShade = 0
            .Weight = Excel.XlBorderWeight.xlThin
        End With

        'Console.WriteLine()
        reaNr += 1
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "Täht"
        wsh.Cells(reaNr, 2).Value = "Nr"
        wsh.Cells(reaNr, 3).Value = "Hex"
        wsh.Cells(reaNr, 4).Value = "Sagedus"
        tblAlgus = reaNr
        reaNr += 1
        Dim symbolid As String = String.Empty
        For Each täht As String In slKogum.Keys
            symbolid &= täht
            'Console.WriteLine("'{0}' - {2} (0x{3}), {1} tk kokku", täht, slKogum(täht), AscW(täht), Hex(AscW(täht)))
            wsh.Cells(reaNr, 1).Value = "'" & täht
            wsh.Cells(reaNr, 2).Value = AscW(täht)
            wsh.Cells(reaNr, 3).Value = Hex(AscW(täht))
            wsh.Cells(reaNr, 4).Value = slKogum(täht)
            reaNr += 1
        Next
        tblLõpp = reaNr - 1

        r = wsh.Range("A" & tblAlgus.ToString & ":D" & tblLõpp.ToString)
        With r.Borders
            .LineStyle = Excel.XlLineStyle.xlContinuous
            .ColorIndex = Excel.Constants.xlAutomatic
            .TintAndShade = 0
            .Weight = Excel.XlBorderWeight.xlThin
        End With

        'Console.WriteLine(symbolid)
        reaNr += 1
        wsh.Cells(reaNr, 1).Value = "'" & symbolid
        reaNr += 1

        wsh.Cells.Columns.AutoFit()


        'tsitaatide autorid
        Dim wsh5 As Excel.Worksheet = wbk.Worksheets(5)
        wsh5.Name = "Ts. (kõik)"
        wsh5.Activate()

        wsh = wsh5
        reaNr = 1

        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "Autor"
        wsh.Cells(reaNr, 2).Value = "Tsitaate"
        wsh.Cells(reaNr, 3).Value = "Neist tõlkeid"
        reaNr += 1

        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":caut", nsm)
        slKogum = New SortedList
        slKogum2 = New SortedList
        For Each tsitaadiAutor As XmlElement In kogum
            Dim autor As String = tsitaadiAutor.InnerText.Trim
            Dim aliik As String = tsitaadiAutor.GetAttribute("aliik", dicUri)
            Dim en, pn, an As String 'eesnimi(ed), perenimi, autori nimi
            If (autor.IndexOf(" ") > -1) Then
                en = autor.Substring(0, autor.LastIndexOf(" ")).Trim
                pn = autor.Substring(autor.LastIndexOf(" ") + 1).Trim
                If (en.EndsWith(".")) Then
                    If (pn.EndsWith(".")) Then
                        pn = pn.Substring(0, pn.Length - 1)
                    End If
                    an = pn & ", " & en
                Else 'Eessaare Aadu
                    an = autor
                    If (an.EndsWith(".")) Then
                        an = an.Substring(0, an.Length - 1)
                    End If
                End If
            Else 'Kalevipoeg, Mõistatus
                an = autor
                If (an.EndsWith(".")) Then
                    an = an.Substring(0, an.Length - 1)
                End If
            End If
            If Not (slKogum.ContainsKey(an)) Then
                slKogum.Add(an, 1)
            Else
                slKogum(an) += 1
            End If
            If aliik = "tlk" Then
                If Not (slKogum2.ContainsKey(an)) Then
                    slKogum2.Add(an, 1)
                Else
                    slKogum2(an) += 1
                End If
            End If
        Next
        'Console.WriteLine()
        'Console.WriteLine("Tsitaatide autorid:")
        For Each autor As String In slKogum.Keys
            'Console.WriteLine("{0}{2}{1}", autor, slKogum(autor), vbTab)
            wsh.Cells(reaNr, 1).Value = autor
            wsh.Cells(reaNr, 2).Value = slKogum(autor)
            If slKogum2.ContainsKey(autor) Then
                wsh.Cells(reaNr, 3).Value = slKogum2(autor)
            End If
            reaNr += 1
        Next

        wsh.Cells.Columns.AutoFit()
        With mse.ActiveWindow
            .SplitColumn = 0
            .SplitRow = 1
        End With
        mse.ActiveWindow.FreezePanes = True


        'tsitaatide tõlkijad
        Dim wsh6 As Excel.Worksheet = wbk.Worksheets(6)
        wsh6.Name = "Ts. (tõlkijad)"
        wsh6.Activate()

        wsh = wsh6
        reaNr = 1

        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "Autor"
        wsh.Cells(reaNr, 2).Value = "Tõlkeid"
        reaNr += 1

        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":caut[@" & dicPr & ":aliik = 'tlk']", nsm)
        slKogum = New SortedList
        For Each tsitaadiAutor As XmlElement In kogum
            Dim autor As String = tsitaadiAutor.InnerText.Trim
            Dim en, pn, an As String 'eesnimi(ed), perenimi, autori nimi
            If (autor.IndexOf(" ") > -1) Then
                en = autor.Substring(0, autor.LastIndexOf(" ")).Trim
                pn = autor.Substring(autor.LastIndexOf(" ") + 1).Trim
                If (en.EndsWith(".")) Then
                    If (pn.EndsWith(".")) Then
                        pn = pn.Substring(0, pn.Length - 1)
                    End If
                    an = pn & ", " & en
                Else 'Eessaare Aadu
                    an = autor
                    If (an.EndsWith(".")) Then
                        an = an.Substring(0, an.Length - 1)
                    End If
                End If
            Else 'Kalevipoeg, Mõistatus
                an = autor
                If (an.EndsWith(".")) Then
                    an = an.Substring(0, an.Length - 1)
                End If
            End If
            If Not (slKogum.ContainsKey(an)) Then
                slKogum.Add(an, 1)
            Else
                slKogum(an) += 1
            End If
        Next
        'Console.WriteLine()
        'Console.WriteLine("Tsitaatide autorid:")
        For Each autor As String In slKogum.Keys
            'Console.WriteLine("{0}{2}{1}", autor, slKogum(autor), vbTab)
            wsh.Cells(reaNr, 1).Value = autor
            wsh.Cells(reaNr, 2).Value = slKogum(autor)
            reaNr += 1
        Next

        wsh.Cells.Columns.AutoFit()
        With mse.ActiveWindow
            .SplitColumn = 0
            .SplitRow = 1
        End With
        mse.ActiveWindow.FreezePanes = True


        'tsitaadid (mittetõlkijad)
        Dim wsh7 As Excel.Worksheet = wbk.Worksheets(7)
        wsh7.Name = "Ts. (autorid)"
        wsh7.Activate()

        wsh = wsh7
        reaNr = 1

        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "Autor"
        wsh.Cells(reaNr, 2).Value = "Tsitaate"
        reaNr += 1

        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":caut[not(@" & dicPr & ":aliik)]", nsm)
        slKogum = New SortedList
        For Each tsitaadiAutor As XmlElement In kogum
            Dim autor As String = tsitaadiAutor.InnerText.Trim
            Dim en, pn, an As String 'eesnimi(ed), perenimi, autori nimi
            If (autor.IndexOf(" ") > -1) Then
                en = autor.Substring(0, autor.LastIndexOf(" ")).Trim
                pn = autor.Substring(autor.LastIndexOf(" ") + 1).Trim
                If (en.EndsWith(".")) Then
                    If (pn.EndsWith(".")) Then
                        pn = pn.Substring(0, pn.Length - 1)
                    End If
                    an = pn & ", " & en
                Else 'Eessaare Aadu
                    an = autor
                    If (an.EndsWith(".")) Then
                        an = an.Substring(0, an.Length - 1)
                    End If
                End If
            Else 'Kalevipoeg, Mõistatus
                an = autor
                If (an.EndsWith(".")) Then
                    an = an.Substring(0, an.Length - 1)
                End If
            End If
            If Not (slKogum.ContainsKey(an)) Then
                slKogum.Add(an, 1)
            Else
                slKogum(an) += 1
            End If
        Next
        'Console.WriteLine()
        'Console.WriteLine("Tsitaatide autorid:")
        For Each autor As String In slKogum.Keys
            'Console.WriteLine("{0}{2}{1}", autor, slKogum(autor), vbTab)
            wsh.Cells(reaNr, 1).Value = autor
            wsh.Cells(reaNr, 2).Value = slKogum(autor)
            reaNr += 1
        Next

        wsh.Cells.Columns.AutoFit()
        With mse.ActiveWindow
            .SplitColumn = 0
            .SplitRow = 1
        End With
        mse.ActiveWindow.FreezePanes = True


        'valdkonnad
        Dim wsh8 As Excel.Worksheet = wbk.Worksheets(8)
        wsh8.Name = "Valdkonnad"
        wsh8.Activate()

        wsh = wsh8
        reaNr = 1

        For Each vald As String In slVallad.Keys
            wsh.Cells(reaNr, 1).Value = vald
            wsh.Cells(reaNr, 2).Value = slVallad(vald)
            'If Not (vald.EndsWith(".")) Then
            '    r = wsh.Cells.Rows(reaNr)
            '    r.Font.ColorIndex = 3 'punane
            '    r.Font.Bold = True
            'End If
            reaNr += 1
        Next

        wsh.Cells.Columns.AutoFit()


        'stiilid
        Dim wsh9 As Excel.Worksheet = wbk.Worksheets(9)
        wsh9.Name = "Stiilid"
        wsh9.Activate()

        wsh = wsh9
        reaNr = 1

        For Each stiil As String In slStiilid.Keys
            wsh.Cells(reaNr, 1).Value = stiil
            wsh.Cells(reaNr, 2).Value = slStiilid(stiil)
            'If Not (stiil.EndsWith(".")) Then
            '    r = wsh.Cells.Rows(reaNr)
            '    r.Font.ColorIndex = 3 'punane
            '    r.Font.Bold = True
            'End If
            reaNr += 1
        Next

        wsh.Cells.Columns.AutoFit()


        'vormikoodid
        Dim wsh10 As Excel.Worksheet = wbk.Worksheets(10)
        wsh10.Name = "Vormikoodid"
        wsh10.Activate()

        wsh = wsh10
        reaNr = 1

        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":vk", nsm)
        slKogum = New SortedList
        For Each vk As XmlElement In kogum
            Dim vkTekst As String = vk.InnerText
            If Not (slKogum.ContainsKey(vkTekst)) Then
                slKogum.Add(vkTekst, 1)
            Else
                slKogum(vkTekst) += 1
            End If
        Next
        For Each vkTekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = vkTekst
            wsh.Cells(reaNr, 2).Value = slKogum(vkTekst)
            reaNr += 1
        Next

        wsh.Cells.Columns.AutoFit()


        'sõnaliigid
        Dim wsh11 As Excel.Worksheet = wbk.Worksheets(11)
        wsh11.Name = "Sõnaliigid"
        wsh11.Activate()

        wsh = wsh11
        reaNr = 1

        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":sl", nsm)
        slKogum = New SortedList
        For Each sl As XmlElement In kogum
            Dim tekst As String = sl.InnerText
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next

        wsh.Cells.Columns.AutoFit()


        'rektsioonikoodid
        Dim wsh12 As Excel.Worksheet = wbk.Worksheets(12)
        wsh12.Name = "Rektsioonikoodid"
        wsh12.Activate()

        wsh = wsh12
        reaNr = 1

        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":rk", nsm)
        slKogum = New SortedList
        For Each rk As XmlElement In kogum
            Dim tekst As String = rk.InnerText
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        'wsh.Cells(reaNr, 1).Value = "Nurksulud teksti ümbert ära"

        wsh.Cells.Columns.AutoFit()


        'Atribuudid
        Dim wsh13 As Excel.Worksheet = wbk.Worksheets(13)
        wsh13.Name = "Atribuudid"
        wsh13.Activate()

        wsh = wsh13
        reaNr = 1

        '@Al
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@Al (artikli liik)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(dicPr & ":*[@" & dicPr & ":Al]", nsm)
        slKogum = New SortedList
        For Each art As XmlElement In kogum
            Dim tekst As String = art.GetAttribute("Al", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@src
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@src (allikas)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(dicPr & ":*[@" & dicPr & ":src]", nsm)
        slKogum = New SortedList
        For Each art As XmlElement In kogum
            Dim tekst As String = art.GetAttribute("src", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@liik
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@liik (märksõna liik)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":liik]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("liik", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@mliik
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@mliik"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":mliik]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("mliik", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@l
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@l (kasutuslisand)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":l]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("l", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@mvtl
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@mvtl (m.viite liik)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":mvtl]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("mvtl", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@rnr
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@rnr (rooma number)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":rnr]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("rnr", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@tliik
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@tliik (tähenduse liik)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":tliik]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("tliik", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@tnr
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@tnr (tähendusnumber)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":tnr]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("tnr", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@xml:lang
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@xml:lang"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@xml:lang]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("xml:lang")
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@tvtl
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@tvtl (t.viite liik)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":tvtl]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("tvtl", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@att
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@att (alltähenduse eristus)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":att]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("att", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@nliik
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@nliik (näite liik)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":nliik]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("nliik", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1
        '@aliik
        r = wsh.Cells.Rows(reaNr)
        r.Font.Bold = True
        wsh.Cells(reaNr, 1).Value = "@aliik (ts. autori liik)"
        reaNr += 1
        kogum = xmlDom.DocumentElement.SelectNodes(".//" & dicPr & ":*[@" & dicPr & ":aliik]", nsm)
        slKogum = New SortedList
        For Each elm As XmlElement In kogum
            Dim tekst As String = elm.GetAttribute("aliik", dicUri)
            If Not (slKogum.ContainsKey(tekst)) Then
                slKogum.Add(tekst, 1)
            Else
                slKogum(tekst) += 1
            End If
        Next
        For Each tekst As String In slKogum.Keys
            wsh.Cells(reaNr, 1).Value = tekst
            wsh.Cells(reaNr, 2).Value = slKogum(tekst)
            reaNr += 1
        Next
        reaNr += 1

        wsh.Cells.Columns.AutoFit()


        'KONFF tagasi
        'System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

        mse.Visible = True
        mse = Nothing



        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)
        Dim timeStr As String = "Excel: " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds)

        Console.WriteLine()
        Console.WriteLine(timeStr)
        Console.WriteLine()
        tsslTime.Text = timeStr
    End Sub 'tsmiToolsToExcel_Click

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If rtbInfo.Text.Length = 0 Then
            Exit Sub
        End If

        Dim dlgResult As DialogResult = frmDlgFindText.ShowDialog
        If (dlgResult = Windows.Forms.DialogResult.OK) Then
            rtbFindStr = frmDlgFindText.tbFind.Text
            If (frmDlgFindText.chkCaseSensitive.Checked) Then
                rtbf = RichTextBoxFinds.MatchCase
            Else
                rtbf = 0
            End If
            If (frmDlgFindText.chkFullWord.Checked) Then
                rtbf = rtbf Or RichTextBoxFinds.WholeWord
            End If
            rtbInfo.DeselectAll()
            Dim respos As Integer = rtbInfo.Find(rtbFindStr, 0, rtbf)
            If (respos >= 0) Then
                rtbInfo.Focus()
                rtbFindPos = respos
            End If
        End If
    End Sub

    Private Sub btnFindNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindNext.Click
        If rtbInfo.Text.Length > 0 Then
            Call procFindNext()
        End If
    End Sub

    Private Sub procFindNext()
        Dim respos As Integer = rtbInfo.Find(rtbFindStr, rtbFindPos + 1, rtbf)
        If (respos >= 0) Then
            rtbInfo.Focus()
            rtbFindPos = respos
        End If
    End Sub

    Private Sub btnFindPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindPrev.Click
        Call procFindPrev()
    End Sub

    Private Sub procFindPrev()
        Dim respos As Integer = rtbInfo.Find(rtbFindStr, 0, rtbFindPos, rtbf Or RichTextBoxFinds.Reverse)
        If (respos >= 0) Then
            rtbInfo.Focus()
            rtbFindPos = respos
        End If
    End Sub

    Private Sub rtbInfo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtbInfo.KeyUp
        If (e.KeyCode = Keys.F3) Then
            If (rtbFindStr.Length > 0) Then
                Call procFindNext()
            End If
        ElseIf (e.KeyCode = Keys.F2) Then
            Call procFindPrev()
        End If
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim dlgResult As DialogResult
        Dim frm As New frmDlgLoadFile
        dlgResult = frm.ShowDialog()
        If (dlgResult = Windows.Forms.DialogResult.OK) Then
            Dim lvi As ListViewItem = frm.lvFileNames.SelectedItems(0)
            rtbInfo.LoadFile(lvi.Tag, RichTextBoxStreamType.RichText)
        End If
    End Sub

    Private Sub tsmiToolsXMLSplit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiToolsXMLSplit.Click
        If (xmlDom Is Nothing) Then
            Exit Sub
        End If

        Dim dlgResult As DialogResult = frmDlgSplitXML.ShowDialog
        If (dlgResult <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False



        Dim arts As XmlNodeList = xmlDom.DocumentElement.SelectNodes("*", nsm)
        Dim kokku As Integer = arts.Count
        Dim mitmeks As Integer = frmDlgSplitXML.nudVolCount.Value

        Dim köites As Integer = (kokku \ mitmeks)
        If (kokku Mod mitmeks) > 0 Then
            köites += 1
        End If

        Dim outDom As New XmlDocument
        outDom.AppendChild(outDom.ImportNode(xmlDom.DocumentElement, False))

        Dim wrs As New XmlWriterSettings
        wrs.Encoding = New System.Text.UTF8Encoding(False, True)
        wrs.Indent = frmDlgSplitXML.chkXMLIndented.Checked
        Dim wr As XmlWriter

        Dim artJnr As Integer = 0
        Dim kJnr As Integer = 0
        Dim volFile As String
        For Each art As XmlElement In arts
            outDom.DocumentElement.AppendChild(outDom.ImportNode(art, True))
            artJnr += 1
            If (artJnr = köites) Then
                kJnr += 1
                volFile = fdir & "\" & fn & "_" & kJnr & ".xml"
                Console.WriteLine(volFile & ": " & outDom.DocumentElement.SelectNodes("*", nsm).Count & " artiklit")
                wr = XmlWriter.Create(volFile, wrs)
                outDom.Save(wr)
                wr.Close()
                outDom = New XmlDocument
                outDom.AppendChild(outDom.ImportNode(xmlDom.DocumentElement, False))
                artJnr = 0
            End If
        Next
        If (outDom.DocumentElement.HasChildNodes) Then
            kJnr += 1
            volFile = fdir & "\" & fn & "_" & kJnr & ".xml"
            Console.WriteLine(volFile & ": " & outDom.DocumentElement.SelectNodes("*", nsm).Count & " artiklit")
            wr = XmlWriter.Create(volFile, wrs)
            outDom.Save(wr)
            wr.Close()
        End If



        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)
        Dim timeStr As String = "Tükid: " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds)

        Console.WriteLine()
        Console.WriteLine(timeStr)
        Console.WriteLine()
        tsslTime.Text = timeStr

    End Sub 'tsmiToolsXMLSplit_Click

    Private Sub tsmiToolsXMLJoin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiToolsXMLJoin.Click

        'All files (*.*)|*.*|TXT files (*.txt)|*.txt|
        'XML files (*.xml)|*.xml|RTF files (*.rtf)|*.rtf|
        'MS Office Word files (*.doc)|*.doc|MS Office Excel files (*.xls)|*.xls|
        'XSD schema files (*.xsd)|*.xsd|XML manager project files (*.xhproj)|*.xhproj|
        'CSV text files (*.csv)|*.csv

        Dim dlgResult As DialogResult

        dlgOFD.Multiselect = True
        dlgOFD.FilterIndex = 3 'XML
        dlgOFD.FileName = String.Empty
        dlgOFD.Title = "Sisend: Vali XML-failid, mida ühendada"
        dlgResult = dlgOFD.ShowDialog()
        If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        'salvestamine
        frmDlgSaveAs.chkIndented.Checked = False
        dlgResult = frmDlgSaveAs.ShowDialog
        If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        ''All files (*.*)|*.*|Text files (*.txt)|*.txt|
        ''XML files (*.xml)|*.xml|XSD files (*.xsd)|*.xsd|
        ''xmlStats files (*.xmlStats)|*.xmlStats|RTF files (*.rtf)|*.rtf
        'dlgSFD.FilterIndex = 3 'XML
        'dlgSFD.FileName = String.Empty
        'dlgSFD.Title = "Väljund: ühendatud XML faili nimi"
        'dlgResult = dlgSFD.ShowDialog()
        'If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
        '    Exit Sub
        'End If

        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False



        Dim xmlWrSets As New XmlWriterSettings
        xmlWrSets.Encoding = New System.Text.UTF8Encoding(False, True)
        If (frmDlgSaveAs.chkIndented.Checked) Then
            xmlWrSets.Indent = True
        Else
            xmlWrSets.Indent = False
        End If
        Dim xmlWr As XmlWriter = XmlWriter.Create(frmDlgSaveAs.tbLocation.Text.Trim, xmlWrSets)

        xmlWr.WriteStartDocument()

        Dim tempDom As XmlDocument
        Dim srTehtud As Boolean = False
        Dim xmlFailid() As String = dlgOFD.FileNames
        Array.Sort(xmlFailid) 'XP jaoks ...?
        For i As Byte = 0 To xmlFailid.GetUpperBound(0)
            tempDom = New XmlDocument
            Dim xmlf As String = xmlFailid(i)
            tempDom.Load(xmlf)
            If Not (srTehtud) Then
                xmlWr.WriteStartElement(tempDom.DocumentElement.Prefix, tempDom.DocumentElement.LocalName, tempDom.DocumentElement.NamespaceURI)
                For Each attr As XmlAttribute In tempDom.DocumentElement.Attributes
                    xmlWr.WriteAttributeString(attr.LocalName, attr.NamespaceURI, attr.Value)
                Next
                srTehtud = True
            End If
            tempDom.DocumentElement.WriteContentTo(xmlWr)
        Next

        xmlWr.WriteEndElement() '"sr"
        xmlWr.WriteEndDocument()
        xmlWr.Close()



        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)
        Dim timeStr As String = "Ühend: " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds)

        Console.WriteLine()
        Console.WriteLine(timeStr)
        Console.WriteLine()
        tsslTime.Text = timeStr

    End Sub 'tsmiToolsXMLJoin_Click

    Private Sub tsmiHelpHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiHelpHelp.Click
        frmDlgHelp.ShowDialog()
    End Sub

    Private Sub tsmiToolsEnumsSschemas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiToolsEnumsSschemas.Click
        If (xmlDom Is Nothing) Then
            Exit Sub
        End If

        'All files (*.*)|*.*|TXT files (*.txt)|*.txt|
        'XML files (*.xml)|*.xml|RTF files (*.rtf)|*.rtf|
        'MS Office Word files (*.doc)|*.doc|MS Office Excel files (*.xls)|*.xls|
        'XSD schema files (*.xsd)|*.xsd|XML manager project files (*.xhproj)|*.xhproj|
        'CSV text files (*.csv)|*.csv

        Dim dlgResult As DialogResult

        dlgOFD.Multiselect = False
        dlgOFD.FilterIndex = 7 'XSD
        dlgOFD.FileName = String.Empty
        dlgOFD.Title = "Sisend: Vali skeemi kirjutamise abiskeem"
        dlgResult = dlgOFD.ShowDialog()
        Dim abiDom As XmlDocument
        If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
            abiDom = Nothing
        Else
            abiDom = New XmlDocument
            abiDom.Load(dlgOFD.FileName)
        End If

        Dim frm As New frmDlgElemsAsText
        frm.propWTtitle = "Vali elemendid/atribuudid, millele loendeid soovid"
        frm.propSetting = "enumNames"
        dlgResult = frm.ShowDialog()
        Dim nimed As String
        If (dlgResult = Windows.Forms.DialogResult.OK) Then
            nimed = ";"
            For Each lvi As ListViewItem In frm.lvNamesAsText.Items
                nimed &= lvi.Tag.ToString & ";"
            Next
            My.Settings.enumNames = nimed
            My.Settings.Save()
        Else
            Exit Sub
        End If

        dlgFBD.SelectedPath = Application.StartupPath
        dlgResult = dlgFBD.ShowDialog
        If (dlgResult <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False



        Dim dicId As String = dlgFBD.SelectedPath.Substring(dlgFBD.SelectedPath.LastIndexOf("\") + 1)
        Dim xmlWrSt As New XmlWriterSettings
        xmlWrSt.Encoding = New System.Text.UTF8Encoding(False, True)
        xmlWrSt.Indent = True

        Dim tyybiNimi As String

        nimed = My.Settings.enumNames.Trim
        Dim enumid() As String = nimed.Substring(1, nimed.Length - 2).Split(";")
        For Each nimi As String In enumid
            Dim xPath, ln As String
            If (nimi.StartsWith("@")) Then
                xPath = nimi.Substring(0, 1) & xPr & ":" & nimi.Substring(1)
                ln = nimi.Substring(1)
            Else
                xPath = xPr & ":" & nimi
                ln = nimi
            End If
            Dim noded As XmlNodeList = xmlDom.DocumentElement.SelectNodes(".//" & xPath, nsm)
            Dim slEnumid As New SortedList
            For Each myNode As XmlNode In noded
                If Not (slEnumid.ContainsKey(myNode.InnerText)) Then
                    slEnumid.Add(myNode.InnerText, 1)
                Else
                    slEnumid(myNode.InnerText) += 1
                End If
            Next
            Console.WriteLine()
            Console.WriteLine("nimi: '{0}' {1} erinevat", nimi, slEnumid.Count)
            Dim stDom As New XmlDocument 'simpleType DOM
            'stDom.AppendChild(stDom.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8"""))
            Dim sRoot As XmlElement = stDom.AppendChild(stDom.CreateNode(XmlNodeType.Element, xsPr, "schema", xsUri))
            sRoot.SetAttribute("targetNamespace", "http://www.eki.ee/dict/schemas/" & dicId & "_" & ln)
            sRoot.SetAttribute("elementFormDefault", "qualified")
            sRoot.SetAttribute("attributeFormDefault", "qualified")
            sRoot.SetAttribute("xmlns:" & dicId & "_" & ln, "http://www.eki.ee/dict/schemas/" & dicId & "_" & ln)
            Dim stElem, restrElem, enumElem, annotElem, docnElem As XmlElement
            stElem = sRoot.AppendChild(stDom.CreateNode(XmlNodeType.Element, xsPr, "simpleType", xsUri))
            stElem.SetAttribute("name", ln & "_tyyp")
            restrElem = stElem.AppendChild(stDom.CreateNode(XmlNodeType.Element, xsPr, "restriction", xsUri))
            restrElem.SetAttribute("base", "xs:string")
            If (ln = "v") Then
                tyybiNimi = "vald_tyyp"
            ElseIf (ln = "sl") Then
                tyybiNimi = "sliik_tyyp"
            ElseIf (ln = "s") Then
                tyybiNimi = "stiil_tyyp"
            ElseIf (ln = "vk") Then
                tyybiNimi = "vk_tyyp"
            ElseIf (ln = "rk") Then
                tyybiNimi = "rk_tyyp"
            ElseIf (ln = "liik") Then
                tyybiNimi = "msliik_tyyp"
            ElseIf (ln = "l") Then
                tyybiNimi = "lisand_tyyp"
            ElseIf (ln = "mvtl") Then
                tyybiNimi = "mvtl_tyyp"
            ElseIf (ln = "tvtl") Then
                tyybiNimi = "tvtl_tyyp"
            ElseIf (ln = "att") Then
                tyybiNimi = "att_tyyp"
            ElseIf (ln = "aliik") Then
                tyybiNimi = "aliik_tyyp"
            Else
                tyybiNimi = String.Empty
            End If
            Dim abiElem As XmlElement
            For Each thisKey As String In slEnumid.Keys
                Console.WriteLine(vbTab & "'{0}': '{1}' - {2} tk", nimi, thisKey, slEnumid(thisKey))
                enumElem = restrElem.AppendChild(stDom.CreateNode(XmlNodeType.Element, xsPr, "enumeration", xsUri))
                enumElem.SetAttribute("value", thisKey)
                annotElem = enumElem.AppendChild(stDom.CreateNode(XmlNodeType.Element, xsPr, "annotation", xsUri))
                docnElem = annotElem.AppendChild(stDom.CreateNode(XmlNodeType.Element, xsPr, "documentation", xsUri))
                docnElem.SetAttribute("xml:lang", "et")
                If (abiDom IsNot Nothing) Then
                    abiElem = abiDom.DocumentElement.SelectSingleNode("xs:simpleType[@name = '" & tyybiNimi & "']/xs:restriction/xs:enumeration[@value = '" & thisKey & "']/xs:annotation/xs:documentation[@xml:lang = 'et']", nsm)
                    If (abiElem IsNot Nothing) Then
                        docnElem.InnerText = abiElem.InnerText
                    Else
                        docnElem.InnerText = thisKey
                    End If
                Else
                    docnElem.InnerText = thisKey
                End If
                docnElem = annotElem.AppendChild(stDom.CreateNode(XmlNodeType.Element, xsPr, "documentation", xsUri))
                docnElem.SetAttribute("xml:lang", "en")
                If (abiDom IsNot Nothing) Then
                    abiElem = abiDom.DocumentElement.SelectSingleNode("xs:simpleType[@name = '" & tyybiNimi & "']/xs:restriction/xs:enumeration[@value = '" & thisKey & "']/xs:annotation/xs:documentation[@xml:lang = 'en']", nsm)
                    If (abiElem IsNot Nothing) Then
                        docnElem.InnerText = abiElem.InnerText
                    Else
                        docnElem.InnerText = "-"
                    End If
                Else
                    docnElem.InnerText = "-"
                End If
            Next
            Dim xmlWr As XmlWriter = XmlWriter.Create(dlgFBD.SelectedPath & "\" & dicId & "_" & nimi & ".xsd", xmlWrSt)
            stDom.Save(xmlWr)
            xmlWr.Close()
        Next



        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)
        Dim timeStr As String = "Loendid: " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds)

        Console.WriteLine()
        Console.WriteLine(timeStr)
        Console.WriteLine()
        tsslTime.Text = timeStr

    End Sub

    Private Sub tsmiFileSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiFileSaveAs.Click
        Dim dlgResult As DialogResult = frmDlgSaveAs.ShowDialog
        If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False



        Dim xmlWrSets As New XmlWriterSettings
        If (frmDlgSaveAs.chkIndented.Checked) Then
            xmlWrSets.Indent = True
        Else
            xmlWrSets.Indent = False
        End If
        Dim xmlWr As XmlWriter = XmlWriter.Create(frmDlgSaveAs.tbLocation.Text.Trim, xmlWrSets)
        xmlDom.Save(xmlWr)
        xmlWr.Close()



        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)
        Dim timeStr As String = "Salvest.: " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds)

        Console.WriteLine()
        Console.WriteLine(timeStr)
        Console.WriteLine()
        tsslTime.Text = timeStr
        'MsgBox("OK..." & vbCrLf & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds))
    End Sub

    Sub algSeis()

        '--------------------------- algseis --------------------------------
        xmlFile = String.Empty
        xmlDom = Nothing
        Call clearInfo() 'staatuse real aeg ja rada tühjaks
        tvSchema.Nodes.Clear()
        rtbInfo.Text = String.Empty
        Me.Text = wTitle

        tsmiFileSaveAs.Text = "Salvesta kui ..."
        tsmiFileSaveAs.Enabled = False
        tsmiToolsXMLSplit.Enabled = False
        tsmiToolsEnumsSschemas.Enabled = False
        tsmiToolsToExcel.Enabled = False
        '--------------------------- algseis --------------------------------

    End Sub

    Private Sub SortattrJaSortToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SortattrJaSortToolStripMenuItem.Click

        Dim dlgResult As DialogResult

        dlgOFD.Multiselect = False
        dlgOFD.FilterIndex = 3 'XML
        dlgOFD.FileName = String.Empty
        dlgOFD.Title = "Sisend: Mingi XML-fail"
        dlgResult = dlgOFD.ShowDialog()
        If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If
        dlgSFD.FilterIndex = 3 'XML
        dlgSFD.FileName = String.Empty
        dlgSFD.Title = "Salvesta kui ..."
        dlgResult = dlgSFD.ShowDialog()
        If (dlgResult <> Windows.Forms.DialogResult.OK Or dlgSFD.FileName = dlgOFD.FileName) Then
            Exit Sub
        End If

        Call algSeis()

        xmlFile = dlgOFD.FileName

        fdir = xmlFile.Substring(0, xmlFile.LastIndexOf("\"))
        fne = xmlFile.Substring(xmlFile.LastIndexOf("\") + 1)
        fn = fne.Substring(0, fne.LastIndexOf("."))

        xmlDom = New XmlDocument
        xmlDom.Load(xmlFile)

        Dim dicPr As String = xmlDom.DocumentElement.Prefix
        Dim dicUri As String = xmlDom.DocumentElement.NamespaceURI

        If (dicPr = String.Empty Or dicUri = String.Empty) Then
            MsgBox("Juurika prefiks v URI määramata!", MsgBoxStyle.Critical, xmlFile)
            Exit Sub
        End If

        Dim vals As New frmDlgDicVals
        vals.txtDicCode.Text = fn.Substring(0, 3)
        vals.txtPr.Text = dicPr
        vals.txtURI.Text = dicUri

        vals.chkHomNr.Checked = True
        vals.chkHomNr.Visible = False

        vals.chkYVOrder.Checked = True
        vals.chkYVOrder.Visible = False

        vals.txtDefQuery.Visible = True

        dlgResult = vals.ShowDialog()
        If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        Dim dic_desc As String = vals.txtDicCode.Text.Trim
        If dic_desc.Length = 0 Then
            MsgBox("Sõnastiku kood määramata!", MsgBoxStyle.Critical, xmlFile)
            Exit Sub
        End If
        Dim fakult As String = vals.txtFakult.Text.Trim
        Dim lsp As String = vals.txtLsp.Text.Trim
        Dim inclHoms As Boolean = vals.chkHomNr.Checked
        Dim yvOrder As Boolean = vals.chkYVOrder.Checked
        'x:P/x:mg/x:m, t:kp/t:terg/t:ter
        Dim defQuery As String = vals.txtDefQuery.Text.Trim
        If defQuery.Length = 0 Then
            MsgBox("Märksõna asukoht määramata!", MsgBoxStyle.Critical, xmlFile)
            Exit Sub
        End If

        nsm = New XmlNamespaceManager(xmlDom.NameTable)
        nsm.AddNamespace(dicPr, dicUri)
        nsm.AddNamespace(xslPr, xslUri)

        Dim märkSõnad As XmlNodeList = xmlDom.DocumentElement.SelectNodes(dicPr & ":A/" & defQuery, nsm)
        If märkSõnad.Count = 0 Then
            MsgBox("Märksõna asukoht ilmselt vale, ei leitud ühtegi!", MsgBoxStyle.Critical, xmlFile)
            Exit Sub
        End If



        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False

        Dim mssvNode As XmlAttribute = xmlDom.CreateNode(XmlNodeType.Attribute, dicPr, "O", dicUri)
        For Each m As XmlElement In märkSõnad
            mssvNode.Value = getSortVal(m, fakult, lsp, inclHoms, yvOrder, dicUri, String.Empty)
            m.Attributes.SetNamedItem(mssvNode.Clone)
        Next

        Dim aeg As DateTime = Now
        Dim koostaja As String = "EKI"

        For Each art As XmlNode In xmlDom.DocumentElement.SelectNodes(dicPr & ":A", nsm)
            Dim outEl As XmlElement = art.SelectSingleNode(dicPr & ":G", nsm)
            Dim refNode As XmlElement = Nothing
            If (outEl Is Nothing) Then
                refNode = art.SelectSingleNode(String.Format("{0}:K | {0}:KA | {0}:KL | {0}:T | {0}:TA | {0}:TL | {0}:PT | {0}:PTA | {0}:X | {0}:XA", dicPr), nsm)
                outEl = art.InsertBefore(xmlDom.CreateNode(XmlNodeType.Element, dicPr, "G", dicUri), refNode)
                outEl.InnerText = Guid.NewGuid.ToString
            End If

            outEl = art.SelectSingleNode(dicPr & ":K", nsm)
            If (outEl Is Nothing) Then
                refNode = art.SelectSingleNode(String.Format("{0}:KA | {0}:KL | {0}:T | {0}:TA | {0}:TL | {0}:PT | {0}:PTA | {0}:X | {0}:XA", dicPr), nsm)
                outEl = art.InsertBefore(xmlDom.CreateNode(XmlNodeType.Element, dicPr, "K", dicUri), refNode)
                outEl.InnerText = koostaja
            End If
            outEl = art.SelectSingleNode(dicPr & ":KA", nsm)
            If (outEl Is Nothing) Then
                refNode = art.SelectSingleNode(String.Format("{0}:KL | {0}:T | {0}:TA | {0}:TL | {0}:PT | {0}:PTA | {0}:X | {0}:XA", dicPr), nsm)
                outEl = art.InsertBefore(xmlDom.CreateNode(XmlNodeType.Element, dicPr, "KA", dicUri), refNode)
                outEl.InnerText = aeg
            End If
        Next


        Dim wrs As XmlWriterSettings
        Dim wr As XmlWriter

        Dim xslt As XslCompiledTransform
        Dim xsltFile As String

        xslt = New XslCompiledTransform
        xsltFile = Application.StartupPath & "/xsl/msv(O)order.xslt"

        Dim xsltDom As New XmlDocument
        xsltDom.Load(xsltFile)
        xsltDom.DocumentElement.SetAttribute("xmlns:al", dicUri)
        xsltDom.DocumentElement.SetAttribute("xmlns:" & dicPr, dicUri)
        xsltDom.DocumentElement.SelectSingleNode("xsl:variable[@name = 'dic_desc']", nsm).InnerText = dic_desc
        Dim asendatav As XmlElement = xsltDom.DocumentElement.SelectSingleNode("xsl:template[@match ='al:sr']/xsl:copy/xsl:apply-templates/xsl:sort[@select = 'asendus/trahh/blaah']", nsm)
        Call asendatav.SetAttribute("select", defQuery & "/@al:O")
        If vals.chkIndented.Checked Then
            Dim outPut As XmlElement = xsltDom.DocumentElement.SelectSingleNode("xsl:output", nsm)
            Call outPut.SetAttribute("indent", "yes")
        End If

        xslt.Load(xsltDom)
        wrs = xslt.OutputSettings

        wr = XmlWriter.Create(dlgSFD.FileName, wrs)
        xslt.Transform(xmlDom, wr)
        wr.Close()


        'igaks juhuks kõik tühjaks ...
        xmlDom = Nothing
        nsm = Nothing
        xmlFile = String.Empty
        fdir = String.Empty
        fne = String.Empty
        fn = String.Empty



        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)
        Dim timeStr As String = "Sort: " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds)
        tsslTime.Text = timeStr

    End Sub

    Private Sub XMLiValideerimineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XMLiValideerimineToolStripMenuItem.Click
        Call algSeis()
        frmDlgXMLValidate.ShowDialog()
    End Sub

    Private Sub EksportMySQLAndmefailiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EksportMySQLAndmefailiToolStripMenuItem.Click

        Call algSeis()


        Dim dlgResult As DialogResult

        dlgOFD.FilterIndex = 3 'XML
        dlgOFD.Multiselect = True
        dlgOFD.Title = "Sisend: köited"
        dlgOFD.FileName = String.Empty
        'ief1.xml nt
        dlgResult = dlgOFD.ShowDialog
        If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        Dim xmlFile As String = dlgOFD.FileNames(0)
        Dim fdir As String = String.Empty
        Dim fne As String = String.Empty
        Dim fn As String = String.Empty
        Dim dic_desc As String = String.Empty
        Dim dicPr As String = String.Empty
        Dim dicUri As String = String.Empty

        fdir = xmlFile.Substring(0, xmlFile.LastIndexOf("\"))
        fne = xmlFile.Substring(xmlFile.LastIndexOf("\") + 1)
        fn = fne.Substring(0, fne.LastIndexOf("."))

        Dim tempDom As XmlDocument
        tempDom = New XmlDocument
        tempDom.Load(xmlFile)

        dicPr = tempDom.DocumentElement.Prefix
        dicUri = tempDom.DocumentElement.NamespaceURI

        dic_desc = dicUri.Substring(dicUri.LastIndexOf("/") + 1).PadRight(3, "_")
        If fn.Length < 4 Then
            MsgBox(String.Format("Faili nimi peab algama sõnastiku koodiga ning neljas märk peab olema köite number!"), MsgBoxStyle.Critical, fn)
            Exit Sub
        End If
        Dim fdd As String = fn.Substring(0, 3)
        If fdd <> dic_desc Then
            MsgBox(String.Format("Faili nimi peab algama sõnastiku koodiga ('{0}' != '{1}') ning neljas märk peab olema köite number!", fdd, dic_desc), MsgBoxStyle.Critical, fn)
            Exit Sub
        End If
        If Not Char.IsDigit(fn.Chars(3)) Then
            MsgBox(String.Format("Faili nimi peab algama sõnastiku koodiga ning neljas märk ('{0}') peab olema köite number!", fn.Chars(3)), MsgBoxStyle.Critical, fn)
            Exit Sub
        End If

        If fn.Length = 4 And fn.StartsWith(dic_desc) Then
            dlgResult = MsgBox("Teisenduse käigus kirjutatakse ka algsed XML failid üle. Algfailid võiksid olla nimetatud nt 'psv1_org.xml', 'ss12_org.xml' jne! Kas jätkata?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, "Sisendfailide nimed: " & fn)
            If Not (dlgResult = Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If


        Dim usrName As String = "newuser"
        Dim pw As String = "Newpassword"

        Dim eelexHost As String = "http://yourhost.ee/"

        Dim request As HttpWebRequest = WebRequest.Create(eelexHost)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"
        Dim response As HttpWebResponse
        Try
            response = request.GetResponse()
            If response.StatusDescription = "OK" Then
                eelexHost = request.Address.ToString 'et kas vastuseks on 'eelex.eki.ee' (kodus), v 'eelex.dyn.eki.ee' (tööl)
            End If
            response.Close()
            If Not response.StatusDescription = "OK" Then
                MsgBox(response.StatusCode & " :: " & response.StatusDescription, MsgBoxStyle.Critical, "request.GetResponse()")
                Exit Sub
            End If
        Catch ex As Exception 'Unauthorized, Forbidden ...
            MsgBox(ex.Message, MsgBoxStyle.Critical, "request.GetResponse()")
            Exit Sub
        End Try

        Dim confFile As String = "__shs/shsconfig_" & dic_desc & ".xml"
        Dim eelexAddress As String = eelexHost & confFile

        request = WebRequest.Create(eelexAddress)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"

        Dim wReqCache As New CredentialCache()
        wReqCache.Add(New Uri(eelexAddress), "Digest", New NetworkCredential(usrName, pw, "'SHS'"))
        'wReqCache.Add(New Uri(postSite), "Basic", New NetworkCredential(usrName, pw))

        request.Credentials = wReqCache

        Dim responseFromServer As String = String.Empty

        Try
            response = request.GetResponse()
            If response.StatusDescription = "OK" Then
                'response.Server: Apache/2.2.9 (FreeBSD) mod_ssl/2.2.9 OpenSSL/0.9.7e-p1 DAV/2
                'Console.WriteLine("response.Server: " & response.Server)
                Dim responseStream As Stream = response.GetResponseStream()
                Dim responseReader As New StreamReader(responseStream, Encoding.UTF8)
                responseFromServer = responseReader.ReadToEnd()
                responseReader.Close()
                responseStream.Close()
            End If
            response.Close()
            If Not response.StatusDescription = "OK" Then
                MsgBox(response.StatusCode & " :: " & response.StatusDescription, MsgBoxStyle.Critical, "request.GetResponse()")
                Exit Sub
            End If
        Catch ex As Exception 'Unauthorized, Forbidden, Not Found ...
            MsgBox(ex.Message & vbNewLine & confFile, MsgBoxStyle.Critical, "request.GetResponse()")
            Exit Sub
        End Try

        If responseFromServer.Length = 0 Then
            MsgBox(String.Format("Ei leitud konfiguratsioonifaili '{0}'", confFile), MsgBoxStyle.Critical, eelexAddress)
            Exit Sub
        End If

        '
        'keelekonstantide XML
        Dim lcXmlFile As String = "lexlist.xml" '"__shs/lc.xml"
        Dim lcXmlStr As String = String.Empty

        request = WebRequest.Create(eelexHost & lcXmlFile)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"
        request.Credentials = wReqCache

        Try
            response = request.GetResponse()
            If response.StatusDescription = "OK" Then
                'response.Server: Apache/2.2.9 (FreeBSD) mod_ssl/2.2.9 OpenSSL/0.9.7e-p1 DAV/2
                'Console.WriteLine("response.Server: " & response.Server)
                Dim responseStream As Stream = response.GetResponseStream()
                Dim responseReader As New StreamReader(responseStream, Encoding.UTF8)
                lcXmlStr = responseReader.ReadToEnd()
                responseReader.Close()
                responseStream.Close()
            End If
            response.Close()
            If Not response.StatusDescription = "OK" Then
                MsgBox(response.StatusCode & " :: " & response.StatusDescription, MsgBoxStyle.Critical, "request.GetResponse()")
                Exit Sub
            End If
        Catch ex As Exception 'Unauthorized, Forbidden, Not Found ...
            MsgBox(ex.Message & vbNewLine & lcXmlFile, MsgBoxStyle.Critical, "request.GetResponse()")
            Exit Sub
        End Try

        If lcXmlStr.Length = 0 Then
            MsgBox(String.Format("Ei leitud konfiguratsioonifaili '{0}'", lcXmlFile), MsgBoxStyle.Critical, lcXmlFile)
            Exit Sub
        End If


        'If dic_desc = "ief" Then
        '    default_query = "f:P/f:terg/f:ter"
        '    msLsp = String.Empty
        '    fakOlemas = "()"
        'ElseIf dic_desc = "evs" Then
        '    default_query = "v:P/v:mg/v:m"
        '    msLsp = "+"
        '    fakOlemas = "[]"
        'ElseIf dic_desc = "ex_" Then
        '    default_query = "x:P/x:mg/x:m"
        '    msLsp = "+"
        '    fakOlemas = "[]"
        'ElseIf dic_desc = "har" Then
        '    default_query = "h:P/h:ep/h:terg/h:ter"
        '    msLsp = String.Empty
        '    fakOlemas = String.Empty
        'ElseIf dic_desc = "ss1" Then
        '    default_query = "s:P/s:mg/s:m"
        '    msLsp = "|"
        '    fakOlemas = "()"
        'ElseIf dic_desc = "psv" Then
        '    default_query = "c:P/c:mg/c:m"
        '    msLsp = String.Empty
        '    fakOlemas = String.Empty
        'ElseIf dic_desc = "vsl" Then
        '    default_query = "z:P/z:mg/z:m"
        '    msLsp = String.Empty
        '    fakOlemas = String.Empty
        'ElseIf dic_desc = "ety" Then
        '    default_query = "e:P/e:mg/e:m"
        '    msLsp = String.Empty
        '    fakOlemas = "()"
        'ElseIf dic_desc = "ss_" Then
        '    default_query = "s:P/s:mg/s:m"
        '    msLsp = "|"
        '    fakOlemas = "()"
        'ElseIf dic_desc = "qs_" Then
        '    default_query = "q:P/q:mg/q:m"
        '    msLsp = "/"
        '    fakOlemas = "[]"
        'ElseIf dic_desc = "ies" Then
        '    default_query = "i:P/i:mg/i:m"
        '    msLsp = String.Empty
        '    fakOlemas = String.Empty
        'Else
        '    Dim vals As New frmDlgDicVals
        '    vals.txtDicCode.Text = dic_desc
        '    vals.txtPr.Text = dicPr
        '    vals.txtURI.Text = dicUri

        '    vals.chkHomNr.Checked = True
        '    vals.chkHomNr.Visible = True

        '    vals.chkYVOrder.Checked = True
        '    vals.chkYVOrder.Visible = True

        '    vals.txtDefQuery.Visible = True

        '    dlgResult = vals.ShowDialog()
        '    If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
        '        Me.Enabled = True
        '        Exit Sub
        '    End If

        '    default_query = vals.txtDefQuery.Text.Trim
        '    If (default_query = String.Empty) Then
        '        MsgBox("Sõnastiku andmed määramata!", MsgBoxStyle.Critical, dic_desc)
        '        Me.Enabled = True
        '        Exit Sub
        '    End If

        '    msLsp = vals.txtLsp.Text.Trim
        '    fakOlemas = vals.txtFakult.Text.Trim
        'End If

        Dim nsm As XmlNamespaceManager = New XmlNamespaceManager(tempDom.NameTable)
        nsm.AddNamespace(dicPr, dicUri)

        Dim itm As XmlElement

        Dim default_query, first_default, msNimi, msLsp, fakOlemas As String
        msNimi = Nothing

        Dim vals As New frmDlgDicVals
        vals.Text &= ": " & confFile

        Dim lcXmlDom As New XmlDocument
        lcXmlDom.PreserveWhitespace = False
        lcXmlDom.LoadXml(lcXmlStr)
        'itm = lcXmlDom.DocumentElement.SelectSingleNode("itm[@n = 'APP_DESC'][@l = 'et'][@dd = '" & dic_desc & "']")
        itm = lcXmlDom.DocumentElement.SelectSingleNode("lex[@id = '" & dic_desc & "']/name[@l = 'et']")
        If itm Is Nothing Then
            MsgBox(String.Format("'{1}'-is on '{0}' määramata!", dic_desc, lcXmlFile), MsgBoxStyle.Critical, lcXmlFile)
            Exit Sub
        End If
        Dim dicName As String = itm.InnerText
        vals.lblDict.Visible = True
        vals.lblDict.Text = dicName
        lcXmlDom = Nothing

        Dim configDom As New XmlDocument
        configDom.PreserveWhitespace = False
        configDom.LoadXml(responseFromServer)

        itm = configDom.DocumentElement.Item("VERS_DB")
        If itm IsNot Nothing Then
            vals.lblDict.Text &= ": " & itm.InnerText
        End If

        vals.chkIndented.Checked = False
        vals.chkIndented.Visible = False

        vals.txtDicCode.Text = dic_desc

        itm = configDom.DocumentElement.Item("dicpr")
        If itm Is Nothing Then
            MsgBox(String.Format("Konfiguratsioonifailis puudub 'dicpr'!"), MsgBoxStyle.Critical, eelexAddress)
            Exit Sub
        End If
        If itm.InnerText <> dicPr Then
            MsgBox(String.Format("Konfiguratsioonifailis on 'dicpr' vale ('{0}' != '{1}')!", itm.InnerText, dicPr), MsgBoxStyle.Critical, eelexAddress)
            Exit Sub
        End If
        vals.txtPr.Text = dicPr

        itm = configDom.DocumentElement.Item("dicuri")
        If itm Is Nothing Then
            MsgBox(String.Format("Konfiguratsioonifailis puudub 'dicuri'!"), MsgBoxStyle.Critical, eelexAddress)
            Exit Sub
        End If
        If itm.InnerText <> dicUri Then
            MsgBox(String.Format("Konfiguratsioonifailis on 'dicuri' vale ('{0}' != '{1}')!", itm.InnerText, dicUri), MsgBoxStyle.Critical, eelexAddress)
            Exit Sub
        End If
        vals.txtURI.Text = dicUri

        vals.chkHomNr.Checked = True
        vals.chkHomNr.Visible = True

        vals.chkYVOrder.Checked = True
        vals.chkYVOrder.Visible = True

        vals.txtDefQuery.Visible = True

        itm = configDom.DocumentElement.Item("default_query")
        If itm Is Nothing Then
            MsgBox("Konfiguratsioonifailis puudub 'default_query'", MsgBoxStyle.Critical, eelexAddress)
            Exit Sub
        End If
        default_query = itm.InnerText
        default_query = default_query.Substring(default_query.IndexOf("/") + 1)
        vals.txtDefQuery.Text = default_query

        itm = configDom.DocumentElement.Item("msLsp")
        If itm Is Nothing Then
            MsgBox("Konfiguratsioonifailis puudub 'msLsp'", MsgBoxStyle.Critical, eelexAddress)
            Exit Sub
        End If
        msLsp = itm.InnerText
        vals.txtLsp.Text = msLsp

        itm = configDom.DocumentElement.Item("fakult")
        If itm Is Nothing Then
            MsgBox("Konfiguratsioonifailis puudub 'fakult'", MsgBoxStyle.Critical, eelexAddress)
            Exit Sub
        End If
        fakOlemas = itm.InnerText
        vals.txtFakult.Text = fakOlemas

        Dim mySqlDataVer As String = String.Empty
        itm = configDom.DocumentElement.Item("mySqlDataVer")
        If itm IsNot Nothing Then
            mySqlDataVer = itm.InnerText
        End If
        If Not (mySqlDataVer = "2") Then
            vals.Label9.ForeColor = Color.Red
        End If
        vals.txtMySqlDataVer.Text = mySqlDataVer

        srMsAlpha = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsŠšZzŽžTtUuVvWwÕõÄäÖöÜüXxYy"
        itm = configDom.DocumentElement.Item("msLang")
        If itm IsNot Nothing Then
            srMsLang = itm.InnerText
            itm = configDom.DocumentElement.Item("msAlpha")
            If itm Is Nothing Then
                MsgBox(String.Format("Märksõna keel olemas ('{0}'), kuid puudub tähestik!", srMsLang), MsgBoxStyle.Critical, eelexAddress)
                Exit Sub
            End If
            srMsAlpha = itm.InnerText
            itm = configDom.DocumentElement.Item("msTranslSrc")
            If itm IsNot Nothing Then
                srMsTranslSrc = itm.InnerText
            End If
            itm = configDom.DocumentElement.Item("msTranslDst")
            If itm IsNot Nothing Then
                srMsTranslDst = itm.InnerText
            End If
        End If

        vals.lblAlpha.Text = srMsAlpha

        configDom = Nothing


        dlgResult = vals.ShowDialog()
        If Not (dlgResult = Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        default_query = vals.txtDefQuery.Text.Trim
        If (default_query = String.Empty) Then
            MsgBox("Sõnastiku andmed määramata!", MsgBoxStyle.Critical, dic_desc)
            Exit Sub
        End If

        'tegevus algab
        Dim mystart As DateTime = Now
        Me.Enabled = False



        tsPb.Value = tsPb.Minimum


        Dim xslViewlFile As String = "__shs/xsl/gendView_" & dic_desc & ".xsl"
        Dim xslViewFileStr As String = String.Empty

        request = WebRequest.Create(eelexHost & xslViewlFile)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"
        request.Credentials = wReqCache

        Try
            response = request.GetResponse()
            If response.StatusDescription = "OK" Then
                'response.Server: Apache/2.2.9 (FreeBSD) mod_ssl/2.2.9 OpenSSL/0.9.7e-p1 DAV/2
                'Console.WriteLine("response.Server: " & response.Server)
                Dim responseStream As Stream = response.GetResponseStream()
                Dim responseReader As New StreamReader(responseStream, Encoding.UTF8)
                xslViewFileStr = responseReader.ReadToEnd()
                responseReader.Close()
                responseStream.Close()
            End If
            response.Close()
            If Not response.StatusDescription = "OK" Then
                MsgBox(response.StatusCode & " :: " & response.StatusDescription, MsgBoxStyle.Critical, "request.GetResponse()")
                Exit Sub
            End If
        Catch ex As Exception 'Unauthorized, Forbidden, Not Found ...
            MsgBox(ex.Message & vbNewLine & xslViewlFile, MsgBoxStyle.Critical, "request.GetResponse()")
            Exit Sub
        End Try

        Dim xslViewDom As XmlDocument = Nothing
        Dim xslView As XslCompiledTransform = Nothing

        Dim viewXWS As XmlWriterSettings
        Dim sbView As StringBuilder = Nothing
        Dim artViewWr As XmlWriter = Nothing

        If xslViewFileStr.Length = 0 Then
            MsgBox(String.Format("Ei leitud genereeritud vaadet '{0}'", xslViewlFile), MsgBoxStyle.Exclamation, xslViewlFile)
        Else
            xslViewDom = New XmlDocument
            xslViewDom.PreserveWhitespace = True
            xslViewDom.LoadXml(xslViewFileStr)

            xslView = New XslCompiledTransform(False) 'enableDebug=False
            'XsltSettings: enableDocumentFunction=False, enableScript=True
            xslView.Load(xslViewDom, New XsltSettings(False, True), New XmlUrlResolver)
            viewXWS = xslView.OutputSettings()
            sbView = New StringBuilder()
            artViewWr = XmlWriter.Create(sbView, viewXWS)
        End If


        msLsp = vals.txtLsp.Text.Trim
        fakOlemas = vals.txtFakult.Text.Trim


        Dim fakOsataPtrn As String = String.Empty
        If fakOlemas.Length > 0 Then
            '$fakOsataPtrn = '\\' . substr($fakOlemas, 0, 1) . '.*?\\' . substr($fakOlemas, 1, 1);
            fakOsataPtrn = "\" & fakOlemas.Substring(0, 1) & ".*?\" & fakOlemas.Substring(1, 1)
        End If
        first_default = String.Empty
        If default_query.Length > 0 Then
            first_default = default_query.Replace("/", "[1]/") & "[1]"
            msNimi = default_query.Substring(default_query.LastIndexOf("/") + 1)
        End If

        Dim volNr As Byte = 0
        Dim md, ms_att_O, G, K, KA, KL, T, TA, TL, PT, PTA, X, XA As String
        Dim ms_att_OO As String

        Dim dicWr As New StreamWriter(fdir & "\" & dic_desc & ".txt", False, New System.Text.UTF8Encoding(False))
        Dim msidWr As New StreamWriter(fdir & "\msid_" & dic_desc & ".txt", False, New System.Text.UTF8Encoding(False))
        Dim elemsWr As New StreamWriter(fdir & "\elemendid_" & dic_desc & ".txt", False, New System.Text.UTF8Encoding(False))
        Dim attribsWr As New StreamWriter(fdir & "\atribuudid_" & dic_desc & ".txt", False, New System.Text.UTF8Encoding(False))

        Dim wrs As XmlWriterSettings
        Dim wr As XmlWriter

        Dim SEPR As String = vbTab

        Dim slO As New SortedList
        Dim slG As New SortedList

        Dim artikleid As Integer = 0
        Dim märksõnu As Integer = 0
        Dim elemente As Integer = 0
        Dim atribuute As Integer = 0

        Dim etAlgus As String = "^0123456789_%+/|"
        'Dim etTähestik As String = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsŠšZzŽžTtUuVvWwÕõÄäÖöÜüXxYy"
        Dim etfr As String ' = "^0123456789_%+/|AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsŠšZzŽžTtUuVvWwÕõÄäÖöÜüXxYy"
        Dim etto As String ' = ""

        etfr = etAlgus & srMsAlpha
        etto = ""
        For ixChar As Integer = 0 To etAlgus.Length - 1
            etto &= ChrW(&HE001 + ixChar)
        Next
        For ixChar As Integer = 0 To srMsAlpha.Length - 1 Step 2
            etto &= ChrW(&HE011 + ixChar)
            etto &= ChrW(&HE011 + ixChar)
        Next
        etfr &= "()[]"
        If dic_desc <> "evs" Then
            etfr &= " "
        End If

        Dim failiJnr As Integer = 0
        For Each xf As String In dlgOFD.FileNames
            fne = xf.Substring(xf.LastIndexOf("\") + 1)
            fn = fne.Substring(0, fne.LastIndexOf("."))

            volNr = Byte.Parse(fn.Substring(3, 1))

            failiJnr += 1

            tempDom = New XmlDocument
            tempDom.Load(xf)

            Dim mssvNode As XmlAttribute = tempDom.CreateNode(XmlNodeType.Attribute, dicPr, "O", dicUri)
            Dim mmMall As XmlAttribute = tempDom.CreateNode(XmlNodeType.Attribute, dicPr, "mm", dicUri)

            Dim srLang As String = tempDom.DocumentElement.GetAttribute("xml:lang")
            If srLang = String.Empty Then
                srLang = "et"
            End If

            Dim artJnr As Integer = 0
            Dim arts As XmlNodeList = tempDom.DocumentElement.SelectNodes(dicPr & ":A", nsm)
            For Each art As XmlElement In arts

                Dim gElem As XmlElement = art.Item(dicPr & ":G")
                G = gElem.InnerText
                'unikaalsuse kontrolliks
                'ex_ - basaal+
                Do While slG.ContainsKey(G.ToUpper)
                    G = Guid.NewGuid.ToString
                    gElem.InnerText = G
                Loop
                Call slG.Add(G.ToUpper, 1)

                K = art.Item(dicPr & ":K").InnerText
                KA = art.Item(dicPr & ":KA").InnerText
                KA = KA.Replace("T", " ")

                Dim elem As XmlElement

                KL = "\N"
                elem = art.Item(dicPr & ":KL")
                If elem IsNot Nothing Then
                    KL = elem.InnerText
                    KL = KL.Replace("T", " ")
                    KL = String.Format("{0}", KL)
                End If
                T = "\N"
                elem = art.Item(dicPr & ":T")
                If elem IsNot Nothing Then
                    T = elem.InnerText
                    T = String.Format("{0}", T)
                End If
                TA = "\N"
                elem = art.Item(dicPr & ":TA")
                If elem IsNot Nothing Then
                    TA = elem.InnerText
                    TA = TA.Replace("T", " ")
                    TA = String.Format("{0}", TA)
                End If
                TL = "\N"
                elem = art.Item(dicPr & ":TL")
                If elem IsNot Nothing Then
                    TL = elem.InnerText
                    TL = TL.Replace("T", " ")
                    TL = String.Format("{0}", TL)
                End If
                PT = "\N"
                elem = art.Item(dicPr & ":PT")
                If elem IsNot Nothing Then
                    PT = elem.InnerText
                    PT = String.Format("{0}", PT)
                End If
                PTA = "\N"
                elem = art.Item(dicPr & ":PTA")
                If elem IsNot Nothing Then
                    PTA = elem.InnerText
                    PTA = PTA.Replace("T", " ")
                    PTA = String.Format("{0}", PTA)
                End If
                X = "\N"
                elem = art.Item(dicPr & ":X")
                If elem IsNot Nothing Then
                    X = elem.InnerText
                    X = String.Format("{0}", X)
                End If
                XA = "\N"
                elem = art.Item(dicPr & ":XA")
                If elem IsNot Nothing Then
                    XA = elem.InnerText
                    XA = XA.Replace("T", " ")
                    XA = String.Format("{0}", XA)
                End If

                If (dic_desc = "sp_") Then
                    Dim mlNodes As XmlNodeList = art.SelectNodes(".//" & dicPr & ":ml", nsm)
                    For Each mlNode As XmlNode In mlNodes
                        mmMall.Value = getMall(mlNode)
                        mlNode.Attributes.SetNamedItem(mmMall.Clone)
                    Next
                End If


                '6. juuli 11
                'Nüüd elementide ja atribuutide tabelid ...
                Dim mitteElemendid As String = ";G;K;KA;KL;T;TA;TL;PT;PTA;X;XA;"
                '25. okt 11: kanda ka need siiski ... :-)
                mitteElemendid = ""
                For Each el As XmlElement In art.SelectNodes("descendant::*[text()]", nsm)
                    If Not (mitteElemendid.Contains(";" & el.LocalName & ";")) Then
                        Dim val As String = String.Empty
                        For Each tekstNood As XmlNode In el.SelectNodes("text()", nsm)
                            'InnerText asendab kahjuks &amp; - i palja ampersandiga ...
                            '#text korral on InnerText ja Value samad (st '&amp;' - i pole, on '&'),
                            'InnerXml on tühi,
                            'AGA OuterXml päästab meid ...
                            val &= tekstNood.OuterXml
                        Next
                        Dim val_nos As String = removeSymbols(val)
                        Dim rada As String = getRada(el)
                        Dim nimi As String = el.Name
                        Dim elG As String = Guid.NewGuid.ToString

                        elemente += 1
                        elemsWr.WriteLine( _
                                       String.Format("{0}", elG) & SEPR & _
                                       String.Format("{0}", nimi) & SEPR & _
                                       String.Format("{0}", rada) & SEPR & _
                                       String.Format("{0}", val.Replace("\", "\\")) & SEPR & _
                                       String.Format("{0}", val_nos) & SEPR & _
                                       String.Format("{0}", G))

                        For Each elAtt As XmlAttribute In el.Attributes
                            If Not (elAtt.Prefix = "xmlns" Or elAtt.LocalName = "O") Then
                                Dim elAttVal As String = elAtt.InnerText
                                Dim elAttVal_nos As String = removeSymbols(elAttVal)
                                Dim elAttNimi As String = elAtt.Name

                                atribuute += 1
                                attribsWr.WriteLine( _
                                        String.Format("{0}", elG) & SEPR & _
                                               String.Format("{0}", elAttNimi) & SEPR & _
                                               String.Format("{0}", nimi) & SEPR & _
                                               String.Format("{0}", elAttVal.Replace("\", "\\")) & SEPR & _
                                               String.Format("{0}", elAttVal_nos) & SEPR & _
                                               String.Format("{0}", G))
                            End If
                        Next
                    End If
                Next


                Dim m1Nood As XmlElement = art.SelectSingleNode(first_default, nsm)

                'kas on ikka õige struktuuriga artikkel sattunud
                '(vastupidiselt nt IEF kustutatute köide v on default_query vale)
                If m1Nood Is Nothing Then
                    MsgBox("Märksõna asukoht artiklis ilmselt valesti märgitud!", MsgBoxStyle.Critical, default_query)
                    Me.Enabled = True
                    Exit Sub
                End If
                If m1Nood IsNot Nothing Then
                    ms_att_O = String.Empty
                    ms_att_OO = String.Empty
                    md = String.Empty
                    'vsl-is on märksõnu, mis asuvad veel lisaks nn allartiklis 'AA'
                    For Each msElem As XmlElement In art.SelectNodes(".//" & msNimi, nsm)

                        'If dic_desc = "vsl" Then
                        '    msElem.InnerXml = msElem.InnerXml.Replace("_", String.Empty)
                        'End If

                        Dim oVal As String = getSortVal(msElem, String.Empty, msLsp, True, True, dicUri, String.Empty)
                        mssvNode.Value = oVal
                        msElem.Attributes.SetNamedItem(mssvNode.Clone)

                        ''unikaalsuse kontrolliks
                        ''ief: AC, aerial, CD ja Cd
                        'Do While slO.ContainsKey(oVal)
                        '    msElem.AppendChild(tempDom.CreateTextNode("_"))
                        '    msElem.Normalize()
                        '    oVal = getSortVal(msElem, String.Empty, msLsp, True, True, dicUri, String.Empty)
                        '    mssvNode.Value = oVal
                        '    msElem.Attributes.SetNamedItem(mssvNode.Clone)
                        'Loop
                        'Call slO.Add(oVal, 1)

                        Dim ooVal As String = oVal
                        For ixCh As Integer = 0 To etfr.Length - 1
                            If ixCh < etto.Length Then
                                ooVal = ooVal.Replace(etfr.Chars(ixCh), etto.Chars(ixCh))
                            Else
                                ooVal = ooVal.Replace(etfr.Chars(ixCh), String.Empty)
                            End If
                        Next

                        If ms_att_O = String.Empty Then
                            ms_att_O = oVal
                            ms_att_OO = ooVal
                        End If

                        Dim msLang As String = msElem.GetAttribute("xml:lang")
                        If msLang = String.Empty Then
                            msLang = srLang
                        End If

                        Dim msTekstid As String = String.Empty
                        For Each msTekstNood As XmlNode In msElem.SelectNodes("text()", nsm)
                            'InnerText asendab kahjuks &amp; - i palja ampersandiga ...
                            '#text korral on InnerText ja Value samad (st '&amp;' - i pole, on '&'),
                            'InnerXml on tühi,
                            'AGA OuterXml päästab meid ...
                            msTekstid &= msTekstNood.OuterXml
                        Next

                        Dim att As XmlAttribute
                        Dim ms_att_i As String = "\N"
                        att = msElem.GetAttributeNode("i", dicUri)
                        If att IsNot Nothing Then
                            ms_att_i = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_liik As String = "\N"
                        att = msElem.GetAttributeNode("liik", dicUri)
                        If att IsNot Nothing Then
                            ms_att_liik = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_ps As String = "\N"
                        att = msElem.GetAttributeNode("ps", dicUri)
                        If att IsNot Nothing Then
                            ms_att_ps = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_tyyp As String = "\N"
                        att = msElem.GetAttributeNode("tyyp", dicUri)
                        If att IsNot Nothing Then
                            ms_att_tyyp = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_mliik As String = "\N"
                        att = msElem.GetAttributeNode("mliik", dicUri)
                        If att IsNot Nothing Then
                            ms_att_mliik = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_k As String = "\N"
                        att = msElem.GetAttributeNode("k", dicUri)
                        If att IsNot Nothing Then
                            ms_att_k = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_mm As String = "\N"
                        att = msElem.GetAttributeNode("mm", dicUri)
                        If att IsNot Nothing Then
                            ms_att_mm = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_st As String = "\N"
                        att = msElem.GetAttributeNode("st", dicUri)
                        If att IsNot Nothing Then
                            ms_att_st = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_vm As String = "\N"
                        att = msElem.GetAttributeNode("vm", dicUri)
                        If att IsNot Nothing Then
                            ms_att_vm = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_all As String = "\N"
                        att = msElem.GetAttributeNode("all", dicUri)
                        If att IsNot Nothing Then
                            ms_att_all = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_uus As String = "\N"
                        att = msElem.GetAttributeNode("uus", dicUri)
                        If att IsNot Nothing Then
                            ms_att_uus = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_zs As String = "\N"
                        att = msElem.GetAttributeNode("zs", dicUri)
                        If att IsNot Nothing Then
                            ms_att_zs = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_u As String = "\N"
                        att = msElem.GetAttributeNode("u", dicUri)
                        If att IsNot Nothing Then
                            ms_att_u = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If
                        Dim ms_att_em As String = "\N"
                        att = msElem.GetAttributeNode("em", dicUri)
                        If att IsNot Nothing Then
                            ms_att_em = String.Format("{0}", att.InnerXml.Replace("\", "\\"))
                        End If

                        Dim ms_nos As String = removeSymbols(msTekstid)
                        Dim ms_nos_alt As String = "\N"
                        If fakOlemas.Length > 0 Then
                            If msTekstid.IndexOf(fakOlemas.Substring(0, 1)) > -1 And msTekstid.IndexOf(fakOlemas.Substring(1, 1)) > -1 Then
                                'trim: (nagu ~ kui) ühest suust
                                ms_nos_alt = Regex.Replace(msTekstid, fakOsataPtrn, String.Empty).Trim
                                ms_nos_alt = removeSymbols(ms_nos_alt)
                                '    ms_nos_alt = ms_nos & "," & ms_nos_alt
                                'Else
                                '    ms_nos_alt = ms_nos
                            End If
                        End If

                        märksõnu += 1
                        msidWr.WriteLine(String.Format("{0}", msTekstid.Replace("\", "\\")) & SEPR & _
                                       ms_nos & SEPR & _
                                       ms_nos_alt & SEPR & _
                                       ms_att_i & SEPR & _
                                       ms_att_liik & SEPR & _
                                       ms_att_ps & SEPR & _
                                       ms_att_tyyp & SEPR & _
                                       ms_att_mliik & SEPR & _
                                       ms_att_k & SEPR & _
                                       ms_att_mm & SEPR & _
                                       ms_att_st & SEPR & _
                                       ms_att_vm & SEPR & _
                                       ms_att_all & SEPR & _
                                       ms_att_uus & SEPR & _
                                       ms_att_zs & SEPR & _
                                       ms_att_u & SEPR & _
                                       ms_att_em & SEPR & _
                                       String.Format("{0}", oVal) & SEPR & _
                                       String.Format("{0}", ooVal) & SEPR & _
                                       String.Format("{0}", msLang) & SEPR & _
                                       String.Format("{0}", dic_desc) & SEPR & _
                                       String.Format("{0}", volNr.ToString) & SEPR & _
                                       String.Format("{0}", G))
                        If md.Length > 0 Then
                            md &= " :: "
                        End If
                        md &= msTekstid
                    Next 'for each msElem

                    artikleid += 1
                    Dim artikkel As String = Regex.Replace(art.OuterXml, "[\r\n\t]", String.Empty).Trim
                    Dim artKoopiaDom As New XmlDocument
                    'ImportNode creates a copy of the source node
                    artKoopiaDom.AppendChild(artKoopiaDom.ImportNode(art, True))
                    Dim artKoopia As XmlElement = artKoopiaDom.DocumentElement
                    For Each tekstNood As XmlNode In artKoopia.SelectNodes(".//text()")
                        Dim tekst As String = tekstNood.OuterXml
                        tekst = removeSymbols(tekst)
                        tekstNood.InnerText = tekst
                    Next
                    Dim art_alt As String = Regex.Replace(artKoopia.OuterXml, "[\r\n\t]", String.Empty).Trim

                    Dim artHtml As String = String.Empty
                    If Not (xslView Is Nothing Or artViewWr Is Nothing) Then
                        xslView.Transform(art, artViewWr)
                        artHtml = sbView.ToString()
                        artHtml = Regex.Replace(artHtml, "[\r\n\t]", String.Empty).Trim
                        sbView.Length = 0
                    End If

                    dicWr.WriteLine(String.Format("{0}", md.Replace("\", "\\")) & SEPR & _
                                   String.Format("{0}", ms_att_O) & SEPR & _
                                   String.Format("{0}", ms_att_OO) & SEPR & _
                                   String.Format("{0}", K) & SEPR & _
                                   String.Format("{0}", KA) & SEPR & _
                                   KL & SEPR & _
                                   T & SEPR & _
                                   TA & SEPR & _
                                   TL & SEPR & _
                                   PT & SEPR & _
                                   PTA & SEPR & _
                                   X & SEPR & _
                                   XA & SEPR & _
                                   String.Format("{0}", volNr.ToString) & SEPR & _
                                   String.Format("{0}", G) & SEPR & _
                                   String.Format("{0}", artikkel.Replace("\", "\\")) & SEPR & _
                                   String.Format("{0}", art_alt) & SEPR & _
                                   String.Format("{0}", artHtml.Replace("\", "\\")))
                End If 'If m1Nood IsNot Nothing Then

                artJnr += 1
                If (artJnr Mod 100) = 0 Then
                    tsPb.Value = 100 * (((failiJnr - 1) / (dlgOFD.FileNames.GetUpperBound(0) + 1)) + (1 / (dlgOFD.FileNames.GetUpperBound(0) + 1)) * (artJnr / arts.Count))
                End If

            Next 'for each art

            'Call tempDom.Save(fdir & "\" & fn.Replace("_org", String.Empty) & ".xml")

            'salvestamine:
            Dim xslt As New XslCompiledTransform
            Dim xsltFile As String = Application.StartupPath & "/xsl/msv(O)order.xslt"
            Dim xsltDom As New XmlDocument
            xsltDom.Load(xsltFile)
            xsltDom.DocumentElement.SetAttribute("xmlns:al", dicUri)
            Dim argL As New XsltArgumentList
            argL.AddParam("dic_desc", String.Empty, dic_desc)
            xslt.Load(xsltDom)

            wrs = xslt.OutputSettings 'indent = no
            Dim noIndentFile As String = fdir & "\" & dic_desc & volNr & ".xml"
            wr = XmlWriter.Create(noIndentFile, wrs)
            xslt.Transform(tempDom, argL, wr)
            wr.Close()

            tempDom = New XmlDocument
            tempDom.Load(noIndentFile)

            wrs = New XmlWriterSettings
            wrs.Indent = True
            wr = XmlWriter.Create(fdir & "\" & dic_desc & volNr & "_i.xml", wrs)
            Call tempDom.Save(wr)
            wr.Close()

        Next 'For Each xf As String In dlgOFD.FileNames

        msidWr.Close()
        dicWr.Close()
        elemsWr.Close()
        attribsWr.Close()


        Dim sqlWorkDB As String = "xml_dicts"
        Dim sqlWorkFile As String = fdir & "\" & dic_desc & "." & sqlWorkDB & ".sql"

        Dim sqlTestDB As String = "xml_dicts_test"
        Dim sqlTestFile As String = fdir & "\" & dic_desc & "." & sqlTestDB & ".sql"

        'tööbaasi jaoks
        Call kirjutaSqlFailid(sqlWorkDB, sqlWorkFile, dic_desc, artikleid, märksõnu, elemente, atribuute)
        'testbaasi jaoks
        Call kirjutaSqlFailid(sqlTestDB, sqlTestFile, dic_desc, artikleid, märksõnu, elemente, atribuute)

        tsPb.Value = tsPb.Maximum



        'tegevus lõppes
        Me.Enabled = True
        Dim ts As TimeSpan = Now.Subtract(mystart)

        Console.WriteLine()
        Console.WriteLine("OK ... " & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds))
        Console.WriteLine()
        MsgBox("OK..." & vbCrLf & String.Format("{0}h, {1}m, {2}s", ts.Hours, ts.Minutes, ts.Seconds))

        tsPb.Value = tsPb.Minimum

    End Sub

    Private Sub kirjutaSqlFailid(ByVal dbName As String, ByVal fileName As String, ByVal dd As String, ByVal art As Integer, ByVal ms As Integer, ByVal el As Integer, ByVal att As Integer)

        Dim dicCmdWr As New StreamWriter(fileName, False, New System.Text.UTF8Encoding(False))

        dicCmdWr.WriteLine("use " & dbName & ";")

        '27. okt 11
        'mitme toimetamise lõpu märke jaoks ühes väljas
        Dim TLType As String = "varchar(255) COLLATE utf8_estonian_ci"

        dicCmdWr.WriteLine("drop table if exists " & dd & ";")
        dicCmdWr.WriteLine("CREATE TABLE " & dd & " (")
        dicCmdWr.WriteLine(" `md` varchar(512) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" `ms_att_O` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,")
        dicCmdWr.WriteLine(" `K` varchar(45) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" `KA` datetime NOT NULL,")
        dicCmdWr.WriteLine(" `KL` datetime DEFAULT NULL,")
        dicCmdWr.WriteLine(" `T` varchar(45) COLLATE utf8_estonian_ci DEFAULT NULL,")
        dicCmdWr.WriteLine(" `TA` datetime DEFAULT NULL,")
        dicCmdWr.WriteLine(" `TL` " & TLType & " DEFAULT NULL,")
        dicCmdWr.WriteLine(" `PT` varchar(45) COLLATE utf8_estonian_ci DEFAULT NULL,")
        dicCmdWr.WriteLine(" `PTA` datetime DEFAULT NULL,")
        dicCmdWr.WriteLine(" `X` varchar(45) COLLATE utf8_estonian_ci DEFAULT NULL,")
        dicCmdWr.WriteLine(" `XA` datetime DEFAULT NULL,")
        dicCmdWr.WriteLine(" `vol_nr` tinyint(1) unsigned NOT NULL,")
        dicCmdWr.WriteLine(" `G` varchar(36) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" `art` mediumtext COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" `art_alt` mediumtext COLLATE utf8_estonian_ci,")
        dicCmdWr.WriteLine(" `ms_att_OO` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,")
        dicCmdWr.WriteLine(" `toXML` tinyint(3) unsigned DEFAULT NULL,")
        dicCmdWr.WriteLine(" UNIQUE KEY `G_UNIQUE` (`G`),")
        dicCmdWr.WriteLine(" KEY `ix_OO` (`ms_att_OO`),")
        dicCmdWr.WriteLine(" KEY `ix_O` (`ms_att_O`),")
        dicCmdWr.WriteLine(" KEY `ix_vol_nr` (`vol_nr`)")
        dicCmdWr.WriteLine(") ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_estonian_ci;")
        dicCmdWr.WriteLine("")
        dicCmdWr.WriteLine("/* kokku artikleid {0} */", art)
        dicCmdWr.WriteLine("")
        dicCmdWr.WriteLine("load data local infile 'E:/toMySql/" & dd & "/" & dd & ".txt'")
        dicCmdWr.WriteLine(" into table " & dd & " CHARACTER SET 'utf8'")
        dicCmdWr.WriteLine(" fields terminated by '\t' ENCLOSED BY '' ESCAPED BY '\\' lines terminated by '\r\n'")
        dicCmdWr.WriteLine(" (md, ms_att_O, ms_att_OO, K, KA, KL, T, TA, TL, PT, PTA, X, XA, vol_nr, G, art, art_alt);")
        dicCmdWr.WriteLine("")

        dicCmdWr.WriteLine("drop table if exists elemendid_" & dd & ";")
        dicCmdWr.WriteLine("CREATE TABLE elemendid_" & dd & " (")
        dicCmdWr.WriteLine(" `elG` varchar(36) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" `nimi` varchar(45) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" `rada` varchar(255) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" `val` text COLLATE utf8_estonian_ci,")
        dicCmdWr.WriteLine(" `val_nos` text COLLATE utf8_estonian_ci,")
        dicCmdWr.WriteLine(" `G` varchar(36) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" PRIMARY KEY (`elG`),")
        dicCmdWr.WriteLine(" KEY `ix_nimi` (`nimi`),")
        dicCmdWr.WriteLine(" KEY `ix_rada` (`rada`),")
        dicCmdWr.WriteLine(" KEY `ix_val` (`val`(255)),")
        dicCmdWr.WriteLine(" KEY `ix_val_nos` (`val_nos`(255)),")
        dicCmdWr.WriteLine(" KEY `ix_G` (`G`)")
        dicCmdWr.WriteLine(") ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_estonian_ci;")
        dicCmdWr.WriteLine("")
        dicCmdWr.WriteLine("/* kokku elemente {0} */", el)
        dicCmdWr.WriteLine("")
        dicCmdWr.WriteLine("load data local infile 'E:/toMySql/" & dd & "/elemendid_" & dd & ".txt'")
        dicCmdWr.WriteLine(" into table elemendid_" & dd & " CHARACTER SET 'utf8'")
        dicCmdWr.WriteLine(" fields terminated by '\t' ENCLOSED BY '' ESCAPED BY '\\' lines terminated by '\r\n'")
        dicCmdWr.WriteLine(" (elG, nimi, rada, val, val_nos, G);")
        dicCmdWr.WriteLine("")

        dicCmdWr.WriteLine("drop table if exists atribuudid_" & dd & ";")
        dicCmdWr.WriteLine("CREATE TABLE atribuudid_" & dd & " (")
        dicCmdWr.WriteLine(" `elG` varchar(36) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" `nimi` varchar(45) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" `elNimi` varchar(45) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" `val` varchar(255) COLLATE utf8_estonian_ci,")
        dicCmdWr.WriteLine(" `val_nos` varchar(255) COLLATE utf8_estonian_ci,")
        dicCmdWr.WriteLine(" `G` varchar(36) COLLATE utf8_estonian_ci NOT NULL,")
        dicCmdWr.WriteLine(" KEY `ix_nimi` (`nimi`),")
        dicCmdWr.WriteLine(" KEY `ix_elNimi` (`elNimi`),")
        dicCmdWr.WriteLine(" KEY `ix_val` (`val`),")
        dicCmdWr.WriteLine(" KEY `ix_val_nos` (`val_nos`),")
        dicCmdWr.WriteLine(" KEY `ix_G` (`G`),")
        dicCmdWr.WriteLine(" KEY `ix_elG` (`elG`)")
        dicCmdWr.WriteLine(") ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_estonian_ci;")
        dicCmdWr.WriteLine("")
        dicCmdWr.WriteLine("/* kokku atribuute {0} */", att)
        dicCmdWr.WriteLine("")
        dicCmdWr.WriteLine("load data local infile 'E:/toMySql/" & dd & "/atribuudid_" & dd & ".txt'")
        dicCmdWr.WriteLine(" into table atribuudid_" & dd & " CHARACTER SET 'utf8'")
        dicCmdWr.WriteLine(" fields terminated by '\t' ENCLOSED BY '' ESCAPED BY '\\' lines terminated by '\r\n'")
        dicCmdWr.WriteLine(" (elG, nimi, elNimi, val, val_nos, G);")
        dicCmdWr.WriteLine("")

        dicCmdWr.WriteLine("delete from msid where dic_code='" & dd & "';")
        dicCmdWr.WriteLine("")
        dicCmdWr.WriteLine("/* kokku märksõnu {0} */", ms)
        dicCmdWr.WriteLine("")
        dicCmdWr.WriteLine("load data local infile 'E:/toMySql/" & dd & "/msid_" & dd & ".txt'")
        dicCmdWr.WriteLine(" into table msid CHARACTER SET 'utf8'")
        dicCmdWr.WriteLine(" fields terminated by '\t' ENCLOSED BY '' ESCAPED BY '\\' lines terminated by '\r\n'")
        dicCmdWr.WriteLine(" (ms, ms_nos, ms_nos_alt, ms_att_i, ms_att_liik, ms_att_ps, ms_att_tyyp, ms_att_mliik, ms_att_k, ms_att_mm, ms_att_st, ms_att_vm, ms_att_all, ms_att_uus, ms_att_zs, ms_att_u, ms_att_em, ms_att_O, ms_att_OO, ms_lang, dic_code, vol_nr, G);")
        dicCmdWr.WriteLine("")
        dicCmdWr.Close()
    End Sub
End Class
