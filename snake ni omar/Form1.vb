Public Class Form1
#Region "Snake Stuff"
    Dim snake(1000) As PictureBox
    Dim length_of_snake As Integer = -1
    Dim left_right_mover As Integer = 0
    Dim up_down_mover As Integer = 0
    Dim score As Integer = 0
    Dim tt, vv As Integer

    Dim r As New Random
    Private Sub create_head()
        length_of_snake += 1

        snake(length_of_snake) = New PictureBox
        With snake(length_of_snake)
            .Height = 10
            .Width = 10
            .BackColor = Color.White
            .Top = (picb_Field.Top + picb_Field.Bottom) / 2
            .Left = (picb_Field.Left + picb_Field.Right) / 2
        End With
        Me.Controls.Add(snake(length_of_snake))
        snake(length_of_snake).BringToFront()

        LengthenSnake()
        LengthenSnake()

    End Sub

    Private Sub LengthenSnake()
        length_of_snake += 1
        snake(length_of_snake) = New PictureBox
        With snake(length_of_snake)
            .Height = 10
            .Width = 10
            .BackColor = Color.White
            .Top = snake(length_of_snake - 1).Top
            .Left = snake(length_of_snake - 1).Left + 10
        End With
        Me.Controls.Add(snake(length_of_snake))
        snake(length_of_snake).BringToFront()
    End Sub
    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Tm_snakeMover.Start()
        Timer1.Enabled = True
        Select Case e.KeyChar
            Case "a"
                left_right_mover = -10
                up_down_mover = 0
            Case "d"
                left_right_mover = 10
                up_down_mover = 0
            Case "w"
                up_down_mover = -10
                left_right_mover = 0
            Case "s"
                up_down_mover = 10
                left_right_mover = 0
        End Select
    End Sub
#End Region
#Region "collission"
    Private Sub collide_with_walls()
        If snake(0).Left < picb_Field.Left Then
            Tm_snakeMover.Stop()
            Timer1.Stop()
            MsgBox("You Lose")

        End If

        If snake(0).Right > picb_Field.Right Then
            Tm_snakeMover.Stop()
            Timer1.Stop()
            MsgBox("You Lose")

        End If

        If snake(0).Top < picb_Field.Top Then
            Tm_snakeMover.Stop()
            Timer1.Stop()
            MsgBox("You Lose")
        End If

        If snake(0).Bottom > picb_Field.Bottom Then
            Tm_snakeMover.Stop()
            Timer1.Stop()
            MsgBox("You Lose")
        End If
    End Sub
    Private Sub collide_with_mouse()
        If snake(0).Bounds.IntersectsWith(mouse.Bounds) Then
            LengthenSnake()
            mouse.Top = r.Next(picb_Field.Top, picb_Field.Bottom - 10)
            mouse.Left = r.Next(picb_Field.Left, picb_Field.Right - 10)
            score = Convert.ToInt64(Label2.Text)
            Label2.Text = Convert.ToString(score + 1)
        End If

    End Sub
    Private Sub collide_with_self()
        For i = 1D To length_of_snake
            If snake(0).Bounds.IntersectsWith(snake(i).Bounds) Then
                Tm_snakeMover.Stop()
                MsgBox("You Lose!")
            End If
        Next
    End Sub
#End Region
#Region "Mouse Stuff"
    Dim mouse As PictureBox

    Private Sub create_mouse()
        mouse = New PictureBox

        With mouse
            .Width = 10
            .Height = 10
            .BackColor = Color.Red
            .Top = r.Next(picb_Field.Top, picb_Field.Bottom - 10)
            .Left = r.Next(picb_Field.Left, picb_Field.Right - 10)
        End With
        Me.Controls.Add(mouse)
        mouse.BringToFront()

    End Sub

#End Region
    Private Sub Form1_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        create_head()
        create_mouse()
        collide_with_self()
    End Sub

    Private Sub Tm_snakeMover_Tick(sender As Object, e As EventArgs) Handles Tm_snakeMover.Tick
        For i = length_of_snake To 1 Step -1
            snake(i).Top = snake(i - 1).Top
            snake(i).Left = snake(i - 1).Left
        Next

        snake(0).Top += up_down_mover
        snake(0).Left += left_right_mover

        collide_with_walls()
        collide_with_mouse()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label5.Text = Format(tt, "00:") & Format(vv, "00")
        vv = vv + 1
        If vv > 59 Then
            vv = 0
            tt = tt + 1
        End If
        If tt = 2 Then
            vv = 0
            tt = 0
            Label5.Text = "00:00"
            Timer1.Enabled = False

        End If
    End Sub
End Class