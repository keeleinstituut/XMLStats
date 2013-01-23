<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.dlgOFD = New System.Windows.Forms.OpenFileDialog()
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFileOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFileSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFileQuit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiToolsPrefs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiToolsXMLSplit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiToolsXMLJoin = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiToolsEnumsSschemas = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiToolsToExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.SortattrJaSortToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.XMLiValideerimineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EksportMySQLAndmefailiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiHelpHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsslTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslPath = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tvSchema = New System.Windows.Forms.TreeView()
        Me.chkAllElements = New System.Windows.Forms.CheckBox()
        Me.chkOnlyNames = New System.Windows.Forms.CheckBox()
        Me.rtbInfo = New System.Windows.Forms.RichTextBox()
        Me.chkGlobalInfo = New System.Windows.Forms.CheckBox()
        Me.btnInfo = New System.Windows.Forms.Button()
        Me.nudTextLength = New System.Windows.Forms.NumericUpDown()
        Me.nudElemLength = New System.Windows.Forms.NumericUpDown()
        Me.chkElContent = New System.Windows.Forms.CheckBox()
        Me.btnClearInfo = New System.Windows.Forms.Button()
        Me.btnAllInfo = New System.Windows.Forms.Button()
        Me.chkLoendid = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.dlgSFD = New System.Windows.Forms.SaveFileDialog()
        Me.nudListSize = New System.Windows.Forms.NumericUpDown()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnFindPrev = New System.Windows.Forms.Button()
        Me.btnFindNext = New System.Windows.Forms.Button()
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.dlgFBD = New System.Windows.Forms.FolderBrowserDialog()
        Me.tsPb = New System.Windows.Forms.ToolStripProgressBar()
        Me.msMain.SuspendLayout()
        Me.ssMain.SuspendLayout()
        CType(Me.nudTextLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudElemLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudListSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dlgOFD
        '
        Me.dlgOFD.Filter = resources.GetString("dlgOFD.Filter")
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile, Me.tsmiTools, Me.tsmiHelp})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(1009, 24)
        Me.msMain.TabIndex = 0
        Me.msMain.Text = "MenuStrip1"
        '
        'tsmiFile
        '
        Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFileOpen, Me.ToolStripSeparator4, Me.tsmiFileSaveAs, Me.ToolStripSeparator1, Me.tsmiFileQuit})
        Me.tsmiFile.Name = "tsmiFile"
        Me.tsmiFile.Size = New System.Drawing.Size(37, 20)
        Me.tsmiFile.Text = "Fail"
        '
        'tsmiFileOpen
        '
        Me.tsmiFileOpen.Name = "tsmiFileOpen"
        Me.tsmiFileOpen.Size = New System.Drawing.Size(147, 22)
        Me.tsmiFileOpen.Text = "Ava"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(144, 6)
        '
        'tsmiFileSaveAs
        '
        Me.tsmiFileSaveAs.Enabled = False
        Me.tsmiFileSaveAs.Name = "tsmiFileSaveAs"
        Me.tsmiFileSaveAs.Size = New System.Drawing.Size(147, 22)
        Me.tsmiFileSaveAs.Text = "Salvesta kui ..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(144, 6)
        '
        'tsmiFileQuit
        '
        Me.tsmiFileQuit.Name = "tsmiFileQuit"
        Me.tsmiFileQuit.Size = New System.Drawing.Size(147, 22)
        Me.tsmiFileQuit.Text = "Välju"
        '
        'tsmiTools
        '
        Me.tsmiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiToolsPrefs, Me.ToolStripSeparator2, Me.tsmiToolsXMLSplit, Me.tsmiToolsXMLJoin, Me.ToolStripSeparator3, Me.tsmiToolsEnumsSschemas, Me.tsmiToolsToExcel, Me.ToolStripSeparator5, Me.SortattrJaSortToolStripMenuItem, Me.XMLiValideerimineToolStripMenuItem, Me.EksportMySQLAndmefailiToolStripMenuItem})
        Me.tsmiTools.Name = "tsmiTools"
        Me.tsmiTools.Size = New System.Drawing.Size(72, 20)
        Me.tsmiTools.Text = "Tööriistad"
        '
        'tsmiToolsPrefs
        '
        Me.tsmiToolsPrefs.Name = "tsmiToolsPrefs"
        Me.tsmiToolsPrefs.Size = New System.Drawing.Size(225, 22)
        Me.tsmiToolsPrefs.Text = "Eelistused"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(222, 6)
        '
        'tsmiToolsXMLSplit
        '
        Me.tsmiToolsXMLSplit.Enabled = False
        Me.tsmiToolsXMLSplit.Name = "tsmiToolsXMLSplit"
        Me.tsmiToolsXMLSplit.Size = New System.Drawing.Size(225, 22)
        Me.tsmiToolsXMLSplit.Text = "XML-i tükeldamine"
        '
        'tsmiToolsXMLJoin
        '
        Me.tsmiToolsXMLJoin.Name = "tsmiToolsXMLJoin"
        Me.tsmiToolsXMLJoin.Size = New System.Drawing.Size(225, 22)
        Me.tsmiToolsXMLJoin.Text = "XML-ide ühendamine"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(222, 6)
        Me.ToolStripSeparator3.Visible = False
        '
        'tsmiToolsEnumsSschemas
        '
        Me.tsmiToolsEnumsSschemas.Enabled = False
        Me.tsmiToolsEnumsSschemas.Name = "tsmiToolsEnumsSschemas"
        Me.tsmiToolsEnumsSschemas.Size = New System.Drawing.Size(225, 22)
        Me.tsmiToolsEnumsSschemas.Text = "Loendite skeemid"
        Me.tsmiToolsEnumsSschemas.Visible = False
        '
        'tsmiToolsToExcel
        '
        Me.tsmiToolsToExcel.Enabled = False
        Me.tsmiToolsToExcel.Name = "tsmiToolsToExcel"
        Me.tsmiToolsToExcel.Size = New System.Drawing.Size(225, 22)
        Me.tsmiToolsToExcel.Text = "Excelisse"
        Me.tsmiToolsToExcel.Visible = False
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(222, 6)
        '
        'SortattrJaSortToolStripMenuItem
        '
        Me.SortattrJaSortToolStripMenuItem.Name = "SortattrJaSortToolStripMenuItem"
        Me.SortattrJaSortToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.SortattrJaSortToolStripMenuItem.Text = "Sort. atr. ja sorteerimine ..."
        '
        'XMLiValideerimineToolStripMenuItem
        '
        Me.XMLiValideerimineToolStripMenuItem.Name = "XMLiValideerimineToolStripMenuItem"
        Me.XMLiValideerimineToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.XMLiValideerimineToolStripMenuItem.Text = "XML-i valideerimine ..."
        '
        'EksportMySQLAndmefailiToolStripMenuItem
        '
        Me.EksportMySQLAndmefailiToolStripMenuItem.Name = "EksportMySQLAndmefailiToolStripMenuItem"
        Me.EksportMySQLAndmefailiToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.EksportMySQLAndmefailiToolStripMenuItem.Text = "Eksport MySQL andmefaili ..."
        '
        'tsmiHelp
        '
        Me.tsmiHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiHelpHelp})
        Me.tsmiHelp.Name = "tsmiHelp"
        Me.tsmiHelp.Size = New System.Drawing.Size(37, 20)
        Me.tsmiHelp.Text = "Abi"
        '
        'tsmiHelpHelp
        '
        Me.tsmiHelpHelp.Name = "tsmiHelpHelp"
        Me.tsmiHelpHelp.Size = New System.Drawing.Size(92, 22)
        Me.tsmiHelpHelp.Text = "Abi"
        '
        'ssMain
        '
        Me.ssMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslTime, Me.tsslPath, Me.tsPb})
        Me.ssMain.Location = New System.Drawing.Point(0, 550)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(1009, 24)
        Me.ssMain.TabIndex = 1
        Me.ssMain.Text = "StatusStrip1"
        '
        'tsslTime
        '
        Me.tsslTime.AutoSize = False
        Me.tsslTime.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslTime.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.tsslTime.Name = "tsslTime"
        Me.tsslTime.Size = New System.Drawing.Size(125, 19)
        Me.tsslTime.Text = "ToolStripStatusLabel1"
        '
        'tsslPath
        '
        Me.tsslPath.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslPath.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.tsslPath.Name = "tsslPath"
        Me.tsslPath.Size = New System.Drawing.Size(736, 19)
        Me.tsslPath.Spring = True
        Me.tsslPath.Text = "ToolStripStatusLabel2"
        Me.tsslPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tvSchema
        '
        Me.tvSchema.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvSchema.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.tvSchema.FullRowSelect = True
        Me.tvSchema.HideSelection = False
        Me.tvSchema.Location = New System.Drawing.Point(12, 27)
        Me.tvSchema.Name = "tvSchema"
        Me.tvSchema.PathSeparator = "/"
        Me.tvSchema.Size = New System.Drawing.Size(390, 522)
        Me.tvSchema.TabIndex = 2
        '
        'chkAllElements
        '
        Me.chkAllElements.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAllElements.AutoSize = True
        Me.chkAllElements.Location = New System.Drawing.Point(408, 27)
        Me.chkAllElements.Name = "chkAllElements"
        Me.chkAllElements.Size = New System.Drawing.Size(98, 17)
        Me.chkAllElements.TabIndex = 3
        Me.chkAllElements.Text = "Kõik elemendid"
        Me.chkAllElements.UseVisualStyleBackColor = True
        '
        'chkOnlyNames
        '
        Me.chkOnlyNames.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkOnlyNames.AutoSize = True
        Me.chkOnlyNames.Checked = True
        Me.chkOnlyNames.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOnlyNames.Location = New System.Drawing.Point(408, 50)
        Me.chkOnlyNames.Name = "chkOnlyNames"
        Me.chkOnlyNames.Size = New System.Drawing.Size(83, 17)
        Me.chkOnlyNames.TabIndex = 4
        Me.chkOnlyNames.Text = "Ainult nimed"
        Me.chkOnlyNames.UseVisualStyleBackColor = True
        '
        'rtbInfo
        '
        Me.rtbInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.rtbInfo.HideSelection = False
        Me.rtbInfo.Location = New System.Drawing.Point(408, 110)
        Me.rtbInfo.Name = "rtbInfo"
        Me.rtbInfo.Size = New System.Drawing.Size(589, 439)
        Me.rtbInfo.TabIndex = 5
        Me.rtbInfo.Text = ""
        '
        'chkGlobalInfo
        '
        Me.chkGlobalInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkGlobalInfo.AutoSize = True
        Me.chkGlobalInfo.Checked = True
        Me.chkGlobalInfo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGlobalInfo.Location = New System.Drawing.Point(685, 56)
        Me.chkGlobalInfo.Name = "chkGlobalInfo"
        Me.chkGlobalInfo.Size = New System.Drawing.Size(72, 17)
        Me.chkGlobalInfo.TabIndex = 6
        Me.chkGlobalInfo.Text = "globaalne"
        Me.ttMain.SetToolTip(Me.chkGlobalInfo, "Linnuke: info leidmisel arvestatakse kõiki elemendi paiknemiskohti")
        Me.chkGlobalInfo.UseVisualStyleBackColor = True
        '
        'btnInfo
        '
        Me.btnInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInfo.Location = New System.Drawing.Point(922, 23)
        Me.btnInfo.Name = "btnInfo"
        Me.btnInfo.Size = New System.Drawing.Size(75, 23)
        Me.btnInfo.TabIndex = 7
        Me.btnInfo.Text = "Info"
        Me.ttMain.SetToolTip(Me.btnInfo, "Info valitud elemendi/atribuudi kohta")
        Me.btnInfo.UseVisualStyleBackColor = True
        '
        'nudTextLength
        '
        Me.nudTextLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nudTextLength.Location = New System.Drawing.Point(685, 26)
        Me.nudTextLength.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudTextLength.Name = "nudTextLength"
        Me.nudTextLength.Size = New System.Drawing.Size(63, 20)
        Me.nudTextLength.TabIndex = 8
        Me.ttMain.SetToolTip(Me.nudTextLength, "maks. otseselt kuvatava teksti pikkus")
        Me.nudTextLength.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'nudElemLength
        '
        Me.nudElemLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nudElemLength.Location = New System.Drawing.Point(853, 26)
        Me.nudElemLength.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudElemLength.Name = "nudElemLength"
        Me.nudElemLength.Size = New System.Drawing.Size(63, 20)
        Me.nudElemLength.TabIndex = 9
        Me.ttMain.SetToolTip(Me.nudElemLength, "'Elemendid tekstina' eelistustes määratud elementide maks teksti pikkus, mis veel" & _
                " tekstina näidatakse")
        Me.nudElemLength.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'chkElContent
        '
        Me.chkElContent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkElContent.AutoSize = True
        Me.chkElContent.Location = New System.Drawing.Point(788, 27)
        Me.chkElContent.Name = "chkElContent"
        Me.chkElContent.Size = New System.Drawing.Size(59, 17)
        Me.chkElContent.TabIndex = 10
        Me.chkElContent.Text = "El. sisu"
        Me.ttMain.SetToolTip(Me.chkElContent, "Linnuke: kuvada eelistuste 'Elemendid tekstina' märgitud elementide korral teatud" & _
                " pikkusega tekst, mitte element")
        Me.chkElContent.UseVisualStyleBackColor = True
        '
        'btnClearInfo
        '
        Me.btnClearInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearInfo.Location = New System.Drawing.Point(841, 81)
        Me.btnClearInfo.Name = "btnClearInfo"
        Me.btnClearInfo.Size = New System.Drawing.Size(75, 23)
        Me.btnClearInfo.TabIndex = 11
        Me.btnClearInfo.Text = "Puhasta"
        Me.ttMain.SetToolTip(Me.btnClearInfo, "Puhasta tulemuste aken")
        Me.btnClearInfo.UseVisualStyleBackColor = True
        '
        'btnAllInfo
        '
        Me.btnAllInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAllInfo.Location = New System.Drawing.Point(922, 52)
        Me.btnAllInfo.Name = "btnAllInfo"
        Me.btnAllInfo.Size = New System.Drawing.Size(75, 23)
        Me.btnAllInfo.TabIndex = 12
        Me.btnAllInfo.Text = "Kõik"
        Me.ttMain.SetToolTip(Me.btnAllInfo, "Info elemendi ja tema allelementide kohta")
        Me.btnAllInfo.UseVisualStyleBackColor = True
        '
        'chkLoendid
        '
        Me.chkLoendid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkLoendid.AutoSize = True
        Me.chkLoendid.Location = New System.Drawing.Point(788, 56)
        Me.chkLoendid.Name = "chkLoendid"
        Me.chkLoendid.Size = New System.Drawing.Size(64, 17)
        Me.chkLoendid.TabIndex = 13
        Me.chkLoendid.Text = "Loendid"
        Me.ttMain.SetToolTip(Me.chkLoendid, "Linnuke: kuvada elementide sisu loendid")
        Me.chkLoendid.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(922, 81)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "Salvesta"
        Me.ttMain.SetToolTip(Me.btnSave, "Salvesta tulemuste jooksev seis")
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'dlgSFD
        '
        Me.dlgSFD.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|XML files (*.xml)|*.xml|XSD files (*" & _
            ".xsd)|*.xsd|xmlStats files (*.xmlStats)|*.xmlStats|RTF files (*.rtf)|*.rtf"
        '
        'nudListSize
        '
        Me.nudListSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nudListSize.Increment = New Decimal(New Integer() {25, 0, 0, 0})
        Me.nudListSize.Location = New System.Drawing.Point(853, 56)
        Me.nudListSize.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudListSize.Name = "nudListSize"
        Me.nudListSize.Size = New System.Drawing.Size(63, 20)
        Me.nudListSize.TabIndex = 15
        Me.ttMain.SetToolTip(Me.nudListSize, "Maks loendite pikkus")
        Me.nudListSize.Value = New Decimal(New Integer() {200, 0, 0, 0})
        '
        'btnFind
        '
        Me.btnFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFind.Location = New System.Drawing.Point(447, 81)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(75, 23)
        Me.btnFind.TabIndex = 16
        Me.btnFind.Text = "Otsi"
        Me.ttMain.SetToolTip(Me.btnFind, "Otsi")
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'btnFindPrev
        '
        Me.btnFindPrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFindPrev.Location = New System.Drawing.Point(408, 81)
        Me.btnFindPrev.Name = "btnFindPrev"
        Me.btnFindPrev.Size = New System.Drawing.Size(32, 23)
        Me.btnFindPrev.TabIndex = 17
        Me.btnFindPrev.Text = "<---"
        Me.ttMain.SetToolTip(Me.btnFindPrev, "Otsi ülespoole (F2)")
        Me.btnFindPrev.UseVisualStyleBackColor = True
        '
        'btnFindNext
        '
        Me.btnFindNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFindNext.Location = New System.Drawing.Point(528, 81)
        Me.btnFindNext.Name = "btnFindNext"
        Me.btnFindNext.Size = New System.Drawing.Size(32, 23)
        Me.btnFindNext.TabIndex = 18
        Me.btnFindNext.Text = "--->"
        Me.ttMain.SetToolTip(Me.btnFindNext, "Otsi allapoole (F3)")
        Me.btnFindNext.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.Location = New System.Drawing.Point(760, 81)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(75, 23)
        Me.btnLoad.TabIndex = 19
        Me.btnLoad.Text = "Lae"
        Me.ttMain.SetToolTip(Me.btnLoad, "Lae tulemuste aknasse käesoleva sessiooni mingi varasema päringu tulemused")
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'tsPb
        '
        Me.tsPb.Name = "tsPb"
        Me.tsPb.Size = New System.Drawing.Size(100, 18)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1009, 574)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnFindNext)
        Me.Controls.Add(Me.btnFindPrev)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.nudListSize)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.chkLoendid)
        Me.Controls.Add(Me.btnAllInfo)
        Me.Controls.Add(Me.btnClearInfo)
        Me.Controls.Add(Me.chkElContent)
        Me.Controls.Add(Me.nudElemLength)
        Me.Controls.Add(Me.nudTextLength)
        Me.Controls.Add(Me.btnInfo)
        Me.Controls.Add(Me.chkGlobalInfo)
        Me.Controls.Add(Me.rtbInfo)
        Me.Controls.Add(Me.chkOnlyNames)
        Me.Controls.Add(Me.chkAllElements)
        Me.Controls.Add(Me.tvSchema)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.msMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.msMain
        Me.Name = "frmMain"
        Me.Text = "xmlStats"
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        CType(Me.nudTextLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudElemLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudListSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dlgOFD As System.Windows.Forms.OpenFileDialog
    Friend WithEvents msMain As System.Windows.Forms.MenuStrip
    Friend WithEvents tsmiFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiFileOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiFileQuit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ssMain As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tvSchema As System.Windows.Forms.TreeView
    Friend WithEvents chkAllElements As System.Windows.Forms.CheckBox
    Friend WithEvents tsslPath As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents chkOnlyNames As System.Windows.Forms.CheckBox
    Friend WithEvents rtbInfo As System.Windows.Forms.RichTextBox
    Friend WithEvents chkGlobalInfo As System.Windows.Forms.CheckBox
    Friend WithEvents btnInfo As System.Windows.Forms.Button
    Friend WithEvents nudTextLength As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudElemLength As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkElContent As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearInfo As System.Windows.Forms.Button
    Friend WithEvents btnAllInfo As System.Windows.Forms.Button
    Friend WithEvents chkLoendid As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents dlgSFD As System.Windows.Forms.SaveFileDialog
    Friend WithEvents tsmiTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiToolsPrefs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents nudListSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiToolsToExcel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnFindPrev As System.Windows.Forms.Button
    Friend WithEvents btnFindNext As System.Windows.Forms.Button
    Friend WithEvents ttMain As System.Windows.Forms.ToolTip
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents tsmiToolsXMLSplit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiToolsXMLJoin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiHelpHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiToolsEnumsSschemas As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dlgFBD As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiFileSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SortattrJaSortToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents XMLiValideerimineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EksportMySQLAndmefailiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsPb As System.Windows.Forms.ToolStripProgressBar

End Class
