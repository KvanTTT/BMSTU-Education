object Form1: TForm1
  Left = 88
  Top = 140
  BorderIcons = [biSystemMenu, biMinimize]
  Caption = #1040#1092#1092#1080#1085#1085#1099#1077' '#1087#1088#1077#1086#1073#1088#1072#1079#1086#1074#1072#1085#1080#1103
  ClientHeight = 630
  ClientWidth = 842
  Color = 13556179
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poDesigned
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Panel1: TPanel
    Left = 8
    Top = 8
    Width = 593
    Height = 609
    BevelInner = bvRaised
    BevelKind = bkTile
    TabOrder = 0
    object Image: TImage
      Left = 2
      Top = 2
      Width = 585
      Height = 601
      Align = alClient
      OnMouseDown = ImageMouseDown
      OnMouseMove = ImageMouseMove
      OnMouseUp = ImageMouseUp
      ExplicitLeft = 12
      ExplicitTop = 16
      ExplicitWidth = 461
      ExplicitHeight = 436
    end
  end
  object Panel2: TPanel
    Left = 616
    Top = 8
    Width = 217
    Height = 609
    Color = 13556179
    TabOrder = 1
    object GroupBox1: TGroupBox
      Left = 32
      Top = 16
      Width = 169
      Height = 89
      Caption = #1055#1077#1088#1077#1084#1077#1097#1077#1085#1080#1077':'
      Color = 11979748
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentColor = False
      ParentFont = False
      TabOrder = 1
      object Label1: TLabel
        Left = 14
        Top = 29
        Width = 28
        Height = 16
        Caption = 'dx ='
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label2: TLabel
        Left = 14
        Top = 51
        Width = 27
        Height = 16
        Caption = 'dy ='
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object edtDX: TEdit
        Left = 71
        Top = 24
        Width = 81
        Height = 22
        Color = clWhite
        TabOrder = 0
        Text = '0'
        OnKeyPress = edtROXKeyPress
      end
      object edtDY: TEdit
        Left = 71
        Top = 51
        Width = 81
        Height = 22
        TabOrder = 1
        Text = '0'
        OnKeyPress = edtROXKeyPress
      end
    end
    object GroupBox2: TGroupBox
      Left = 32
      Top = 111
      Width = 169
      Height = 115
      Caption = #1055#1086#1074#1086#1088#1086#1090'('#1088#1072#1076'):'
      Color = 12638917
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentColor = False
      ParentFont = False
      TabOrder = 3
      object Label3: TLabel
        Left = 14
        Top = 53
        Width = 28
        Height = 16
        Caption = 'ox ='
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label4: TLabel
        Left = 14
        Top = 75
        Width = 27
        Height = 16
        Caption = 'oy ='
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label7: TLabel
        Left = 14
        Top = 26
        Width = 48
        Height = 16
        Caption = 'angle ='
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object edtROX: TEdit
        Left = 72
        Top = 52
        Width = 81
        Height = 22
        TabOrder = 1
        Text = '0'
        OnExit = edtROXExit
        OnKeyPress = edtROXKeyPress
      end
      object edtROY: TEdit
        Left = 72
        Top = 79
        Width = 81
        Height = 22
        TabOrder = 2
        Text = '0'
        OnExit = edtROXExit
        OnKeyPress = edtROXKeyPress
      end
      object edtAng: TEdit
        Left = 72
        Top = 24
        Width = 81
        Height = 22
        TabOrder = 0
        Text = '0'
        OnKeyPress = edtROXKeyPress
      end
    end
    object GroupBox3: TGroupBox
      Left = 32
      Top = 232
      Width = 169
      Height = 145
      Caption = #1052#1072#1089#1096#1090#1072#1073#1080#1088#1086#1074#1072#1085#1080#1077':'
      Color = 14733517
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentColor = False
      ParentFont = False
      TabOrder = 5
      object Label8: TLabel
        Left = 14
        Top = 29
        Width = 26
        Height = 16
        Caption = 'sx ='
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label9: TLabel
        Left = 14
        Top = 57
        Width = 25
        Height = 16
        Caption = 'sy ='
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label10: TLabel
        Left = 14
        Top = 84
        Width = 28
        Height = 16
        Caption = 'ox ='
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label11: TLabel
        Left = 14
        Top = 111
        Width = 27
        Height = 16
        Caption = 'oy ='
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object edtSx: TEdit
        Left = 72
        Top = 27
        Width = 81
        Height = 22
        TabOrder = 0
        Text = '1'
        OnKeyPress = edtROXKeyPress
      end
      object edtSy: TEdit
        Left = 72
        Top = 55
        Width = 81
        Height = 22
        TabOrder = 1
        Text = '1'
        OnKeyPress = edtROXKeyPress
      end
      object edtSox: TEdit
        Left = 72
        Top = 82
        Width = 81
        Height = 22
        TabOrder = 2
        Text = '0'
        OnExit = edtSoxExit
        OnKeyPress = edtROXKeyPress
      end
      object edtSoy: TEdit
        Left = 72
        Top = 109
        Width = 81
        Height = 22
        TabOrder = 3
        Text = '0'
        OnExit = edtSoxExit
        OnKeyPress = edtROXKeyPress
      end
    end
    object BitBtn2: TBitBtn
      Left = 32
      Top = 393
      Width = 169
      Height = 32
      Caption = #1055#1088#1077#1086#1073#1088#1072#1079#1086#1074#1072#1090#1100
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 6
      OnClick = BitBtn2Click
    end
    object cbTranslate: TCheckBox
      Left = 8
      Top = 16
      Width = 18
      Height = 17
      TabOrder = 0
      OnClick = cbTranslateClick
    end
    object cbRotate: TCheckBox
      Left = 8
      Top = 111
      Width = 18
      Height = 17
      TabOrder = 2
      OnClick = cbRotateClick
    end
    object cbScale: TCheckBox
      Left = 8
      Top = 232
      Width = 18
      Height = 17
      TabOrder = 4
      OnClick = cbScaleClick
    end
    object BitBtn1: TBitBtn
      Left = 32
      Top = 431
      Width = 169
      Height = 27
      Caption = #1054#1090#1084#1077#1085#1080#1090#1100' '#1087#1086#1089#1083#1077#1076#1085#1077#1077' '#1076#1077#1081#1089#1090#1074#1080#1077
      TabOrder = 7
      OnClick = BitBtn1Click
    end
    object Button1: TButton
      Left = 31
      Top = 464
      Width = 170
      Height = 25
      Caption = #1053#1072#1095#1072#1083#1100#1085#1086#1077' '#1087#1086#1083#1086#1078#1077#1085#1080#1077
      TabOrder = 8
      OnClick = Button1Click
    end
    object GroupBox4: TGroupBox
      Left = 16
      Top = 495
      Width = 185
      Height = 98
      Caption = #1057#1087#1088#1072#1074#1082#1072
      TabOrder = 9
      object Label5: TLabel
        Left = 17
        Top = 16
        Width = 150
        Height = 26
        Caption = #1051#1077#1074#1099#1081' '#1082#1083#1080#1082' - '#1091#1089#1090#1072#1085#1086#1074#1080#1090#1100' '#1082#1086#1086#1088#1076#1080#1085#1072#1090#1099' '#1090#1086#1095#1082#1080' '#1074#1088#1072#1097#1077#1085#1080#1103
        Font.Charset = DEFAULT_CHARSET
        Font.Color = 4142365
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = []
        ParentFont = False
        WordWrap = True
      end
      object Label6: TLabel
        Left = 20
        Top = 48
        Width = 138
        Height = 39
        Caption = #1055#1088#1072#1074#1099#1081' '#1082#1083#1080#1082' - '#1091#1089#1090#1072#1085#1086#1074#1080#1090#1100' '#1082#1086#1086#1088#1076#1080#1085#1072#1090#1099' '#1090#1086#1095#1082#1080' '#1084#1072#1089#1096#1090#1072#1073#1080#1088#1086#1074#1072#1085#1080#1103
        Font.Charset = DEFAULT_CHARSET
        Font.Color = 4142365
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = []
        ParentFont = False
        WordWrap = True
      end
    end
  end
  object Timer1: TTimer
    Enabled = False
    Interval = 50
    Left = 428
    Top = 48
  end
  object ColorDialog1: TColorDialog
    Left = 429
    Top = 96
  end
end
