Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports System
Imports System.Xml
Imports System.Configuration
Imports System.Collections.Specialized
Imports System.Security
Imports System.Security.Cryptography
Module PDF

    Sub CreatePdf(d As Dictionary(Of String, String), pdfFileName As String, xmlFileName As String, dccFolderName As String)

        Dim fileName As String
        Dim xmlConfigFile As String
        Dim pdfDoc As New Document()
        Dim maxTableItems As Integer = 5
        Dim globalCurTableItemIndex As Integer = 0
        Dim localCurTableItemIndex As Integer = 0
        Dim numOfPages = 5
        Dim startX, startY, endX, endY, imgFileName, imgScalePercent, text2write, fontSize, txtNodeType
        'Dim borderColor, fillColor As iTextSharp.text.Color

        fileName = pdfFileName
        xmlConfigFile = xmlFileName

        Console.WriteLine("================= CREATE PDF ========================")
        Console.WriteLine("pdfFileName   :  {0}", pdfFileName)
        Console.WriteLine("xmlFileName   :  {0}", xmlFileName)
        Console.WriteLine("dccFolderName :  {0}", dccFolderName)

        Dim pdfWrite As PdfWriter
        Dim cb As PdfContentByte
        Dim fnt As BaseFont

        Try
            pdfWrite = PdfWriter.GetInstance(pdfDoc, New FileStream(fileName, FileMode.Create))
            Console.WriteLine("Open PDF : " + fileName)
            pdfDoc.Open()
            cb = pdfWrite.DirectContent
            'Dim cb As iTextSharp.text.pdf.PdfContentByte
            fnt = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            'cb.SaveState()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ' puntatore ad elemento della tabella da visualizzare nel PDF

        maxTableItems = My.Settings.MAXTABLEITEMS

        numOfPages = Int(d.Count / maxTableItems) + 1
        globalCurTableItemIndex = 0
        localCurTableItemIndex = 0

        Console.WriteLine("Numero di pagine da generare {0}", numOfPages)


        For i As Integer = 1 To numOfPages

            Try

                Console.WriteLine("Open XML : " + xmlConfigFile)
                If My.Computer.FileSystem.FileExists(xmlConfigFile) = False Then
                    MsgBox("ERROR: File not found. " + xmlConfigFile)
                    Exit Sub
                End If

                Dim reader As XmlTextReader = New XmlTextReader(xmlConfigFile)

                Do While (reader.Read())
                    Select Case reader.NodeType
                        Case XmlNodeType.Element 'Display beginning of element.
                            'Console.Write("Element: " + reader.Name)
                            txtNodeType = "XITEMX"
                            If reader.Name = "text" Then
                                'Console.Write("TEXT")
                                txtNodeType = "TEXT"
                                If reader.HasAttributes Then 'If attributes exist
                                    While reader.MoveToNextAttribute()

                                        If reader.Name = "startX" Then
                                            startX = reader.Value
                                            'Console.Write("STARTX:{0}", reader.Value)
                                        End If

                                        If reader.Name = "startY" Then
                                            startY = reader.Value
                                            'Console.Write("STARTY:{0}", reader.Value)
                                        End If

                                        If reader.Name = "fontSize" Then
                                            fontSize = reader.Value
                                            'Console.Write("fontSize:{0}", reader.Value)
                                        End If

                                        'Display attribute name and value.
                                        'Console.Write(" {0}='{1}'", reader.Name, reader.Value)
                                    End While
                                End If
                            End If

                            If reader.Name = "img" Then
                                'Console.Write("IMG")
                                txtNodeType = "IMG"
                                If reader.HasAttributes Then 'If attributes exist
                                    While reader.MoveToNextAttribute()

                                        If reader.Name = "startX" Then
                                            startX = reader.Value
                                            'Console.Write("STARTX:{0}", reader.Value)
                                        End If

                                        If reader.Name = "startY" Then
                                            startY = reader.Value
                                            'Console.Write("STARTY:{0}", reader.Value)
                                        End If

                                        If reader.Name = "imgFileName" Then
                                            imgFileName = reader.Value
                                            ' TEST if exist!
                                            If My.Computer.FileSystem.FileExists(imgFileName) Then
                                                'MsgBox("File found.")
                                            Else
                                                MsgBox("ERROR: File not found. " + imgFileName)
                                            End If
                                            'Console.Write("imgFileName:{0}", reader.Value)
                                        End If

                                        If reader.Name = "imgScalePercent" Then
                                            imgScalePercent = reader.Value
                                            'Console.Write("imgScalePercent:{0}", reader.Value)
                                        End If

                                        'Display attribute name and value.
                                        'Console.Write(" {0}='{1}'", reader.Name, reader.Value)
                                    End While
                                End If
                            End If

                            If reader.Name = "line" Then
                                'Console.Write("LINE")
                                txtNodeType = "LINE"
                                If reader.HasAttributes Then 'If attributes exist
                                    While reader.MoveToNextAttribute()

                                        If reader.Name = "startX" Then
                                            startX = reader.Value
                                            'Console.Write("STARTX:{0}", reader.Value)
                                        End If

                                        If reader.Name = "startY" Then
                                            startY = reader.Value
                                            'Console.Write("STARTY:{0}", reader.Value)
                                        End If

                                        If reader.Name = "endX" Then
                                            endX = reader.Value
                                            'Console.Write("ENDX:{0}", reader.Value)
                                        End If

                                        If reader.Name = "endY" Then
                                            endY = reader.Value
                                            'Console.Write("ENDY:{0}", reader.Value)
                                        End If

                                    End While
                                End If
                            End If


                            If reader.HasAttributes Then 'If attributes exist
                                While reader.MoveToNextAttribute()
                                    'Display attribute name and value.
                                    'Console.Write(" {0}='{1}'", reader.Name, reader.Value)
                                End While
                            End If
                            'Console.WriteLine(">")
                        Case XmlNodeType.Text 'Display the text in each element.

                            text2write = reader.Value
                            Console.WriteLine("{0}:{1}", txtNodeType, reader.Value)

                        Case XmlNodeType.EndElement 'Display end of element.
                            'Console.WriteLine("EndElement")
                            If txtNodeType = "TEXT" Then
                                Console.WriteLine("TEXT {0} {1} {2} {3} ", startX, startY, fontSize, text2write)
                                cb.SetFontAndSize(fnt, fontSize)
                                cb.BeginText()
                                cb.MoveText(startX, startY) 'Move the current point to this position
                                cb.ShowText(text2write) 'Then write the text
                                cb.EndText()
                            End If

                            If txtNodeType = "LINE" Then
                                Console.WriteLine("LINE {0} {1} {2} {3} ", startX, startY, endX, endY)
                                'cb.SaveState()
                                cb.FillStroke()

                                cb.MoveTo(CDbl(startX), CDbl(startY)) ' : Move the current point to cordinates(x,y)
                                cb.LineTo(CDbl(endX), CDbl(endY))
                                'cb.Rectangle(CDbl(startX), CDbl(startY), CDbl(endX), CDbl(endY))
                                'cb.RestoreState()
                                cb.Stroke()
                            End If

                            If txtNodeType = "IMG" Then
                                Console.WriteLine("IMG {0} {1} {2} {3} ", startX, startY, imgFileName, imgScalePercent)
                                'cb.SaveState()
                                cb.MoveTo(CDbl(startX), CDbl(startY)) ' : Move the current point to cordinates(x,y)
                                Dim img As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(CStr(imgFileName)) ' double slash ?
                                img.ScalePercent(CDbl(imgScalePercent))
                                img.SetAbsolutePosition(CDbl(startX), CDbl(startY))
                                'cb.AddImage(img)
                                pdfDoc.Add(img)
                                'cb.RestoreState()
                            End If

                            ' Console.Write("</" + reader.Name)
                            'Console.WriteLine(">")
                    End Select
                Loop

                ' Console.ReadLine()

                Console.WriteLine("Add Table data ... ")

                'http://jwcooney.com/2013/08/23/itextsharp-pdfptable-basic-example/
                Dim paragraphTable1 As Paragraph = New Paragraph()
                paragraphTable1.SpacingBefore = 300.0F
                paragraphTable1.SpacingAfter = 150.0F

                Dim paragraphTable2 As Paragraph = New Paragraph()
                paragraphTable2.SpacingBefore = 100.0F
                paragraphTable2.SpacingAfter = 150.0F

                cb.MoveTo(100.0F, 300.0F)
                Dim table As New PdfPTable(2)
                Dim intTblWidth(1) As Single

                intTblWidth(0) = 150.0F
                intTblWidth(1) = 200.0F

                table.SetWidths(intTblWidth)

                'FontFactory.GetFont()

                Dim fntTableFontHdr As iTextSharp.text.Font = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
                Dim fntTableFontHdr6 As iTextSharp.text.Font = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
                'fntTableFontHdr()
                'iTextSharp.text.Font fntTableFontHdr as new FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                'iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                'Dim cell As New PdfPCell(New Phrase("TAbella di 3 colonne"))

                'table.TotalWidth = 400.0F
                table.WidthPercentage = 100.0F

                'cell.Colspan
                'cell.HorizontalAlignment(1)
                'table.AddCell(cell)

                Dim CellOneHdr1 As PdfPCell = New PdfPCell(New Phrase("Nome file ", fntTableFontHdr))
                Dim CellOneHdr2 As PdfPCell = New PdfPCell(New Phrase("Impronta SHA-256", fntTableFontHdr))

                table.AddCell(CellOneHdr1)
                table.AddCell(CellOneHdr2)

                Dim j As Integer = 0
                localCurTableItemIndex = 0

                For Each kvp As KeyValuePair(Of String, String) In d
                    If j >= globalCurTableItemIndex Then

                        If localCurTableItemIndex < maxTableItems Then

                            Dim fName As String = kvp.Key
                            Dim hashValue As String = kvp.Value

                            Dim CellName As PdfPCell = New PdfPCell(New Phrase(fName, fntTableFontHdr))
                            Dim CellValue As PdfPCell = New PdfPCell(New Phrase(hashValue, fntTableFontHdr6))

                            table.AddCell(CellName)
                            table.AddCell(CellValue)

                            localCurTableItemIndex = localCurTableItemIndex + 1
                            globalCurTableItemIndex = globalCurTableItemIndex + 1
                        End If

                    End If
                    j = j + 1
                Next

                paragraphTable1.Add(table)
                pdfDoc.Add(paragraphTable1)

                'Page number
                cb.BeginText()
                cb.MoveText(250, 30) 'Move the current point to this position
                cb.ShowText("Pagina n. " + CStr(i) + " di " + CStr(numOfPages)) 'Then write the text
                cb.EndText()

                pdfDoc.SetPageSize(New iTextSharp.text.Rectangle(PageSize.A4))
                Console.WriteLine("NEWPAGE " + CStr(i))
                pdfDoc.NewPage()

                Console.WriteLine("CLOSE READER ..")
                reader.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)

            End Try

        Next

        pdfDoc.Close()
        Console.WriteLine("CreatePdf : Creato: {0}", fileName)

    End Sub

    Function getTimeStamp()

        Console.WriteLine(CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds))
        getTimeStamp = CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds)
        Console.WriteLine(Format(Now, "yyyy_MM_dd_hh_mm"))

    End Function


    Function CreateMD5StringFromFile(ByVal Filename As String) As String

        Dim MD5 = System.Security.Cryptography.MD5.Create
        Dim Hash As Byte()
        Dim sb As New System.Text.StringBuilder

        Using st As New FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.Read)
            Hash = MD5.ComputeHash(st)
        End Using

        For Each b In Hash
            sb.Append(b.ToString("X2"))
        Next

        Return sb.ToString

    End Function

    ' Print the byte array in a readable format.
    Public Function PrintByteArray(ByVal array() As Byte)
        Dim strHASH256 As String
        Dim i As Integer
        strHASH256 = ""
        'Console.WriteLine("PrintByteArray")
        For i = 0 To array.Length - 1
            'Console.Write(String.Format("{0:X2}", array(i)))
            strHASH256 = strHASH256 + String.Format("{0:X2}", array(i))
            If i Mod 4 = 3 Then
                'Console.Write(" ")
            End If
        Next i
        'Console.WriteLine()
        'Console.WriteLine("-------------------------------------------------")
        'Console.WriteLine(strHASH256)
        'Console.WriteLine("-------------------------------------------------")

        Return strHASH256


    End Function 'PrintByteArray


    Function Hash256File(ByVal Filename As String) As String
        ' https://msdn.microsoft.com/it-it/library/system.security.cryptography.sha256(v=vs.110).aspx
        ' Test offline http://sourceforge.net/projects/quickhash/files/latest/download
        ' Test online https://defuse.ca/checksums.htm#checksums

        Console.WriteLine("Hash256File")

        Dim mySHA256 As SHA256 = SHA256Managed.Create()
        Dim hashValue() As Byte
        Dim strReturn As String
        ' Compute and print the hash values for each file in directory.
        ' Dim fInfo As FileInfo

        ' Create a fileStream for the file.

        Dim fileStream As FileStream = New FileStream(Filename, FileMode.Open)

        ' Be sure it's positioned to the beginning of the stream.
        fileStream.Position = 0
        ' Compute the hash of the fileStream.
        hashValue = mySHA256.ComputeHash(fileStream)
        ' Write the name of the file to the Console.
        ' Console.Write(fInfo.Name + ": ")
        ' Write the hash value to the Console.
        strReturn = PrintByteArray(hashValue)
        ' Close the file.
        fileStream.Close()


        Return strReturn


    End Function

    Sub ReadXML()

        Dim reader As XmlTextReader = New XmlTextReader("d:\tmp\books.xml")

        Try

            Do While (reader.Read())
                Select Case reader.NodeType
                    Case XmlNodeType.Element 'Display beginning of element.
                        Console.Write("<" + reader.Name)

                        If reader.Name = "img" Then
                            Console.Write("IMG")
                            If reader.HasAttributes Then 'If attributes exist
                                While reader.MoveToNextAttribute()

                                    If reader.Name = "h" Then
                                        Console.Write("h:{0}", reader.Value)
                                    End If

                                    If reader.Name = "w" Then
                                        Console.Write("w:{0}", reader.Value)
                                    End If

                                    'Display attribute name and value.
                                    'Console.Write(" {0}='{1}'", reader.Name, reader.Value)
                                End While
                            End If
                        End If

                        If reader.Name = "line" Then
                            Console.Write("LINE")
                        End If

                        If reader.Name = "text" Then
                            Console.Write("TEXT")
                        End If

                        If reader.HasAttributes Then 'If attributes exist
                            While reader.MoveToNextAttribute()
                                'Display attribute name and value.
                                Console.Write(" {0}='{1}'", reader.Name, reader.Value)
                            End While
                        End If
                        Console.WriteLine(">")
                    Case XmlNodeType.Text 'Display the text in each element.
                        Console.WriteLine("-------------TEXT-----------------")
                        Console.WriteLine(reader.Value)
                        Console.WriteLine("-------------TEXT-----------------")
                    Case XmlNodeType.EndElement 'Display end of element.
                        Console.Write("</" + reader.Name)
                        Console.WriteLine(">")
                End Select
            Loop
            Console.ReadLine()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub


End Module
