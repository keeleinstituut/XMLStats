Imports System.Xml
Imports System.Text.RegularExpressions

Module mod1

    Public xmlFile As String = String.Empty

    Public xmlUri As String = "http://www.w3.org/XML/1998/namespace"

    Public xsPr As String = "xs"
    Public xsUri As String = "http://www.w3.org/2001/XMLSchema"

    Friend xslPr As String = "xsl"
    Friend xslUri As String = "http://www.w3.org/1999/XSL/Transform"

    Public xmlNimed As New Dictionary(Of String, Integer)
    Public excludeNames As String
    Public namesAsText As String
    Public artLN As String 'teise taseme elemendi LN (artikkel "A")


    Const CAP_LETT_LA = "AÀÁÂÃÅĀĂĄǍǠǺȀȂḀẠẢẤẦẨẪẬẮẰẲẴẶBƂƄḂḄḆCÇĆĈĊČƇḈDÐĎĐƋḊḌḎḐḒEÈÉÊËĒĔĖĘĚȄȆḔḖḘḚḜẸẺẼẾỀỂỄỆFƑḞGĜĞĠĢǤǦǴḠHĤĦḢḤḦḨḪIÌÍÎÏĨĪĬĮİǏȈȊḬḮỈỊJĴKĶƘǨḰḲḴLĹĻĽĿŁḶḸḺḼMḾṀṂNÑŃŅŇŊṄṆṈṊOÒÓÔØŌŎŐƠǑǪǬǾȌȎṐṒỌỎỐỒỔỖỘỚỜỞỠỢPƤṔṖQRŔŖŘȐȒṘṚṜṞSŚŜŞṠṢṤṨŠṦZŹŻƵẐẒẔŽTŢŤŦƬṪṬṮṰUÙÚÛŨŪŬŮŰŲƯǓȔȖṲṴṶṸṺỤỦỨỪỬỮỰVṼṾWŴẀẂẄẆẈÕṌṎÄǞÖÜǕǗǙǛXẊẌYÝŶŸƳẎỲỴỶỸ"
    Const REG_LETT_LA = "aàáâãåāăąǎǡǻȁȃḁạảấầẩẫậắằẳẵặbƃƅḃḅḇcçćĉċčƈḉdðďđƌḋḍḏḑḓeèéêëēĕėęěȅȇḕḗḙḛḝẹẻẽếềểễệfƒḟgĝğġģǥǧǵḡhĥħḣḥḧḩḫiìíîïĩīĭįıǐȉȋḭḯỉịjĵkķƙǩḱḳḵlĺļľŀłḷḹḻḽmḿṁṃnñńņňŋṅṇṉṋoòóôøōŏőơǒǫǭǿȍȏṑṓọỏốồổỗộớờởỡợpƥṕṗqrŕŗřȑȓṙṛṝṟsśŝşṡṣṥṩšṧzźżƶẑẓẕžtţťŧƭṫṭṯṱuùúûũūŭůűųưǔȕȗṳṵṷṹṻụủứừửữựvṽṿwŵẁẃẅẇẉõṍṏäǟöüǖǘǚǜxẋẍyýŷÿƴẏỳỵỷỹ"
    Const BASIC_LA = "AAAAAAAAAAAAAAAAAAAAAAAAAAABBBBBBCCCCCCCCDDDDDDDDDDEEEEEEEEEEEEEEEEEEEEEEEEEFFFGGGGGGGGGHHHHHHHHIIIIIIIIIIIIIIIIIJJKKKKKKKLLLLLLLLLLMMMMNNNNNNNNNNOOOOOOOOOOOOOOOOOOOOOOOOOOOOOPPPPQRRRRRRRRRRSSSSSSSSŠŠZZZZZZZŽTTTTTTTTTUUUUUUUUUUUUUUUUUUUUUUUUUUVVVWWWWWWWÕÕÕÄÄÖÜÜÜÜÜXXXYYYYYYYYYYaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbccccccccddddddddddeeeeeeeeeeeeeeeeeeeeeeeeefffggggggggghhhhhhhhiiiiiiiiiiiiiiiiijjkkkkkkkllllllllllmmmmnnnnnnnnnnoooooooooooooooooooooooooooooppppqrrrrrrrrrrssssssssššzzzzzzzžtttttttttuuuuuuuuuuuuuuuuuuuuuuuuuuvvvwwwwwwwõõõääöüüüüüxxxyyyyyyyyyy"

    Const CAP_LETT_RU = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"
    Const REG_LETT_RU = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя"
    Public Const CAP_LETT_ET = "ABCDEFGHIJKLMNOPQRSŠZŽTUVWÕÄÖÜXY"
    Public Const REG_LETT_ET = "abcdefghijklmnopqrsšzžtuvwõäöüxy"

    Friend srMsLang As String = String.Empty
    Friend srMsAlpha As String = String.Empty
    Friend srMsTranslSrc As String = String.Empty
    Friend srMsTranslDst As String = String.Empty


    Public Function getMall(mlNode As XmlNode) As String
        Dim isLetterLaRuOrDigit As String = "0123456789" & REG_LETT_LA & REG_LETT_RU
        Dim mitteMall As String = ".'(){}"

        Dim sText As String = mlNode.InnerText.Trim
        'kui inp on outerXml, siis on '&amp;ema;' jne
        'sText = Regex.Replace(sText, "&amp;", "&")

        sText = Regex.Replace(sText, "(&\w+;)", String.Empty)
        sText = sText.Replace(" ", "_")

        Dim mall As String = String.Empty

        For ix As Integer = 0 To sText.Length - 1
            If (isLetterLaRuOrDigit.IndexOf(Char.ToLower(sText.Chars(ix))) < 0) Then
                If (mitteMall.IndexOf(sText.Chars(ix)) < 0) Then
                    mall &= sText.Chars(ix)
                End If
            End If
        Next

        Return mall

    End Function


    Public Function getSortVal(ByVal oMNode As XmlElement, ByVal fakult As String, ByVal lsp As String, ByVal inclHoms As Boolean, ByVal yvOrder As Boolean, ByVal dicUri As String, ByVal lisaSymbolid As String) As String

        Dim nmVal As String = String.Empty

        'nmVal = oMNode.text
        'nmVal = oMNode.InnerText
        '*** <r> märksõna sees peab jääma järjestusest välja ([kedagi] risti lööma)
        For Each tekstNood As XmlNode In oMNode.SelectNodes("text()")
            nmVal &= tekstNood.InnerText
        Next
        nmVal = nmVal.Trim

        'muutujad (entities) maha
        'Dim rex
        'rex.Pattern = "&\w+?;"
        'nmVal = rex.Replace(nmVal, "")
        nmVal = Regex.Replace(nmVal, "&\w+;", String.Empty)

        'Unicode koodid maha
        'rex.Pattern = "\#U\d{4}"
        'nmVal = rex.Replace(nmVal, "")
        '' '' ''nmVal = Regex.Replace(nmVal, "\#U([\dA-Fa-f]{4})", ChrW("&H\1"))
        '' '' ''nmVal = Regex.Replace(nmVal, "\\u([\dA-Fa-f]{4})", ChrW("&H\1"))

        'ASCII koodid maha
        'rex.Pattern = "\#\d{2}"
        'nmVal = rex.Replace(nmVal, "")
        '' '' ''nmVal = Regex.Replace(nmVal, "\#([\dA-Fa-f]{2})", ChrW("&H\1"))

        'Ligatuurid kaheks
        nmVal = Replace(nmVal, "Æ", "AE")
        nmVal = Replace(nmVal, "æ", "ae")

        nmVal = Replace(nmVal, "Ĳ", "IJ")
        nmVal = Replace(nmVal, "ĳ", "ij")
        nmVal = Replace(nmVal, "Œ", "OE")
        nmVal = Replace(nmVal, "œ", "oe")

        nmVal = Replace(nmVal, "Ǆ", "DŽ")
        nmVal = Replace(nmVal, "ǅ", "Dž")
        nmVal = Replace(nmVal, "ǆ", "dž")
        nmVal = Replace(nmVal, "Ǉ", "LJ")
        nmVal = Replace(nmVal, "ǈ", "Lj")
        nmVal = Replace(nmVal, "ǉ", "lj")
        nmVal = Replace(nmVal, "Ǌ", "NJ")
        nmVal = Replace(nmVal, "ǋ", "Nj")
        nmVal = Replace(nmVal, "ǌ", "nj")

        nmVal = Replace(nmVal, "Ǣ", "AE")
        nmVal = Replace(nmVal, "ǣ", "ae")

        nmVal = Replace(nmVal, "Ǳ", "DZ")
        nmVal = Replace(nmVal, "ǲ", "Dz")
        nmVal = Replace(nmVal, "ǳ", "dz")

        nmVal = Replace(nmVal, "Ǽ", "AE")
        nmVal = Replace(nmVal, "ǽ", "ae")

        Dim i As Integer
        If dicUri.EndsWith("/evs") Then
            'numbrite ees olevate alakriipsude hulka vähendatakse ühe võrra
            'rex.Pattern = "_(?=\d)"
            'nmVal = rex.Replace(nmVal, "")
            nmVal = Regex.Replace(nmVal, "_(?=\d)", String.Empty) 'ainult EVS-is

            '[+] (EVS-is) on ainult ms alguses (valikuline liitsõnapiir)
            'rex.Pattern = "\[\+\]"
            'nmVal = rex.Replace(nmVal, "+")
            nmVal = Regex.Replace(nmVal, "(\[\" & lsp & "\])", lsp)

            'viidete korral toimib järjestus ainult kuni alakriipsudeni
            Dim vt As String = "%" 'EVS
            If (Mid(nmVal, 1, 1) = vt) Then 'EVS, viide vt
                i = InStr(1, nmVal, "_")
                If (i > 0) Then
                    nmVal = Mid(nmVal, 2, i - 2) & vt
                Else
                    nmVal = Mid(nmVal, 2) & vt
                End If
            End If

        End If

        'Õpilase ÕS-is on nn alakaareke (failis "_"), mis sai asendatud "/" - ga ning see on liitsõnapiiriks,
        'fakultatiivset teksti Õpilase ÕS-is ei ole. Tavaliste sulgudega on märgitud valikuline kolmas välde.

        If (lsp.Length > 0) Then
            'lsp - dest jäetakse viimane ja esimene alles, kuna määravad järjestust (liitsõnapiiridega sõnad on viimased)
            'rex.Pattern = "(?!^)" & "\" & lsp & "(?!($|_))"
            'nmVal = rex.Replace(nmVal, "")
            nmVal = Regex.Replace(nmVal, "(?!^)" & "\" & lsp & "(?!($|_))", String.Empty)
            'märksõna alguse liitsõnapiir viiakse lõppu (vajadusel _ ette)
            If (Mid(nmVal, 1, 1) = lsp) Then
                i = InStr(1, nmVal, "_")
                If (i > 0) Then
                    nmVal = Mid(nmVal, 2, i - 2) & lsp & Mid(nmVal, i)
                Else
                    nmVal = Mid(nmVal, 2) & lsp
                End If
            End If
        End If


        '_ ei võeta maha, kuna määrab järjestust
        Dim MSSV_PUNCT As String = "_" & lsp
        If dicUri.EndsWith("/evs") Then
            MSSV_PUNCT &= " "
        End If
        'If dicUri.EndsWith("/od") Then
        '    MSSV_PUNCT &= "0123456789"
        'End If

        If srMsLang.Length > 0 Then
            For ixCh As Integer = 0 To srMsTranslSrc.Length - 1
                If ixCh < srMsTranslDst.Length Then
                    nmVal = nmVal.Replace(srMsTranslSrc.Chars(ixCh), srMsTranslDst.Chars(ixCh))
                Else
                    nmVal = nmVal.Replace(srMsTranslSrc.Chars(ixCh), String.Empty)
                End If
            Next
            'Else
            '    'transliteration
            '    Dim tr_from, tr_to As String
            '    tr_from = CAP_LETT_LA & REG_LETT_LA
            '    tr_to = BASIC_LA '"tõlgib eesti keelde" ... (Š, Ž, ÕÄÖÜ jäävad nendeks samadeks)
            '    For i = 1 To Len(tr_from)
            '        nmVal = Replace(nmVal, Mid(tr_from, i, 1), Mid(tr_to, i, 1))
            '    Next

            '    Dim eriT6lk, eriVaste As String 'need, mida pole 'CAP_LETT_LA'-s
            '    eriT6lk = "Å" 'ongström
            '    eriVaste = "A"
            '    For i = 1 To Len(eriT6lk)
            '        nmVal = Replace(nmVal, Mid(eriT6lk, i, 1), Mid(eriVaste, i, 1))
            '    Next

            'lubatud = MSSV_PUNCT & CAP_LETT_ET & REG_LETT_ET & CAP_LETT_RU & REG_LETT_RU
        End If

        Dim lubatud As String = MSSV_PUNCT & srMsAlpha

        'Nüüd kõik liigsed sümbolid maha ...
        Dim newVal As String = ""
        For i = 1 To Len(nmVal)
            If (InStr(1, lubatud, Mid(nmVal, i, 1)) > 0) Then
                newVal = newVal & Mid(nmVal, i, 1)
            End If
        Next
        If (newVal = "") Then 'polnud ühtegi lubatud tähte
            newVal = "A" 'et midagi oleks ja oleks kohe ees nähtaval
        End If
        nmVal = newVal


        'f - fraseologismid
        If (oMNode.GetAttribute("liik", dicUri) = "f") Then
            '@ps - põhisõna
            Dim psVal As String = oMNode.GetAttribute("ps", dicUri).Trim
            If (psVal.Length > 0) Then
                nmVal = psVal & "^" & nmVal
            End If
        End If

        'y - ühendid
        If (yvOrder And oMNode.GetAttribute("liik", dicUri) = "y") Then
            '@ps - põhisõna
            Dim psVal As String = oMNode.GetAttribute("ps", dicUri).Trim
            If (psVal.Length > 0) Then
                nmVal = psVal & "^^" & nmVal
            Else
                If dicUri.EndsWith("/evs") Then
                    nmVal = nmVal.Substring(nmVal.IndexOf(" ") + 1) & " " & nmVal.Substring(0, nmVal.IndexOf(" "))
                End If
            End If
        End If


        'hom-nr MSSV lõppu
        Dim homNr As String = oMNode.GetAttribute("i", dicUri).Trim
        If (inclHoms And homNr.Length > 0) Then
            If dicUri.EndsWith("/knr") Then 'kohanimeraamat
                homNr = homNr.PadLeft(2, "0")
            End If
            If ((lsp.Length > 0) AndAlso nmVal.EndsWith(lsp)) Then
                nmVal = nmVal.Substring(0, nmVal.Length - 1) & homNr & lsp
            Else
                nmVal = nmVal & homNr
            End If
        End If

        Return nmVal

    End Function 'getSortVal

    Friend Function removeSymbols(ByVal inp As String) As String
        'inp on outerXml, siis on '&amp;ema;' jne
        Dim ret As String = inp.Replace("&amp;", "&")
        'Regex.Replace(inp, "&(amp|lt|gt|((em|b|sub|sup|l)(a|l)));|[^\p{L}\s]", String.Empty)
        ret = Regex.Replace(ret, "(&\w+;)|[^\p{L}\s]", String.Empty)
        Dim eriT6lk As String = "ÀÁÅÅàáÈÉèé"
        Dim eriVaste As String = "AAAAaaEEee"
        For ixChar As Integer = 0 To eriT6lk.Length - 1
            ret = ret.Replace(eriT6lk.Chars(ixChar), eriVaste.Chars(ixChar))
        Next
        Return ret
    End Function

    Friend Function getRada(ByVal elm As XmlElement) As String
        If elm.LocalName = "A" Then
            Return "self::node()"
        End If
        Dim r As String = elm.Name
        Dim el As XmlElement = elm.ParentNode
        Do Until el.LocalName = "A"
            r = el.Name & "/" & r
            el = el.ParentNode
        Loop
        Return r
    End Function

End Module
