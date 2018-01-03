object Form1: TForm1
  Left = 177
  Top = 233
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = #1055#1086#1089#1090#1088#1086#1077#1085#1080#1077' '#1075#1088#1072#1092#1080#1082#1086#1074
  ClientHeight = 538
  ClientWidth = 731
  Color = 14013917
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poDesigned
  OnActivate = FormActivate
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Label5: TLabel
    Left = 521
    Top = 18
    Width = 57
    Height = 13
    Caption = #1060#1091#1085#1082#1094#1080#1103' 1:'
  end
  object Label1: TLabel
    Left = 521
    Top = 140
    Width = 79
    Height = 13
    Caption = #1051#1077#1074#1072#1103' '#1075#1088#1072#1085#1080#1094#1072':'
  end
  object Label2: TLabel
    Left = 521
    Top = 172
    Width = 85
    Height = 13
    Caption = #1055#1088#1072#1074#1072#1103' '#1075#1088#1072#1085#1080#1094#1072':'
  end
  object Label3: TLabel
    Left = 521
    Top = 315
    Width = 53
    Height = 13
    Caption = #1064#1072#1075' '#1089#1077#1090#1082#1080
  end
  object Label4: TLabel
    Left = 521
    Top = 338
    Width = 79
    Height = 13
    Caption = #1087#1086' '#1075#1086#1088#1080#1079#1086#1085#1090#1072#1083#1080
  end
  object Label6: TLabel
    Left = 521
    Top = 365
    Width = 69
    Height = 13
    Caption = #1087#1086' '#1074#1077#1088#1090#1080#1082#1072#1083#1080
  end
  object Label7: TLabel
    Left = 521
    Top = 74
    Width = 57
    Height = 13
    Caption = #1060#1091#1085#1082#1094#1080#1103' 2:'
  end
  object Label10: TLabel
    Left = 45
    Top = 455
    Width = 117
    Height = 16
    Caption = #1062#1074#1077#1090' 1-'#1086#1075#1086' '#1075#1088#1072#1092#1080#1082#1072
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label11: TLabel
    Left = 245
    Top = 455
    Width = 64
    Height = 16
    Caption = #1062#1074#1077#1090' '#1092#1086#1085#1072
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label12: TLabel
    Left = 244
    Top = 495
    Width = 65
    Height = 16
    Caption = #1062#1074#1077#1090' '#1089#1077#1090#1082#1080
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label8: TLabel
    Left = 45
    Top = 495
    Width = 117
    Height = 16
    Caption = #1062#1074#1077#1090' 2-'#1086#1075#1086' '#1075#1088#1072#1092#1080#1082#1072
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object cmbFunc1: TComboBox
    Left = 521
    Top = 37
    Width = 137
    Height = 21
    ItemHeight = 13
    TabOrder = 0
    OnChange = cmbFunc2Change
  end
  object edtLb: TEdit
    Left = 622
    Top = 137
    Width = 46
    Height = 21
    TabOrder = 1
    Text = '-5'
    OnExit = edtLbExit
  end
  object edtRb: TEdit
    Left = 622
    Top = 169
    Width = 46
    Height = 21
    TabOrder = 2
    Text = '5'
    OnExit = edtLbExit
  end
  object ckbGrid: TCheckBox
    Left = 521
    Top = 292
    Width = 57
    Height = 17
    Caption = #1057#1077#1090#1082#1072
    Checked = True
    State = cbChecked
    TabOrder = 3
    OnClick = ckbScaleClick
  end
  object edtGridHorz: TEdit
    Left = 620
    Top = 335
    Width = 33
    Height = 21
    ReadOnly = True
    TabOrder = 4
    Text = '30'
    OnChange = edtGridHorzChange
  end
  object UpDown1: TUpDown
    Left = 653
    Top = 335
    Width = 15
    Height = 21
    Associate = edtGridHorz
    Min = 30
    Max = 50
    Position = 30
    TabOrder = 5
  end
  object edtGridVert: TEdit
    Left = 620
    Top = 362
    Width = 33
    Height = 21
    ReadOnly = True
    TabOrder = 6
    Text = '30'
    OnChange = edtGridHorzChange
  end
  object UpDown2: TUpDown
    Left = 653
    Top = 362
    Width = 15
    Height = 21
    Associate = edtGridVert
    Min = 30
    Max = 50
    Position = 30
    TabOrder = 7
  end
  object cmbFunc2: TComboBox
    Left = 521
    Top = 93
    Width = 137
    Height = 21
    ItemHeight = 13
    TabOrder = 8
    OnChange = cmbFunc2Change
  end
  object pnlGrp1Cl: TPanel
    Left = 181
    Top = 449
    Width = 30
    Height = 30
    BevelKind = bkSoft
    Color = clMaroon
    TabOrder = 9
    OnClick = pnlGrp1ClClick
  end
  object pnlBgCl: TPanel
    Left = 323
    Top = 449
    Width = 30
    Height = 30
    BevelKind = bkSoft
    Color = clCream
    TabOrder = 10
    OnClick = pnlBgClClick
  end
  object Button1: TButton
    Left = 521
    Top = 422
    Width = 147
    Height = 29
    Caption = #1057#1090#1088#1086#1080#1090#1100' '#1075#1088#1072#1092#1080#1082
    TabOrder = 11
    OnClick = Button1Click
  end
  object pnlGridCl: TPanel
    Left = 323
    Top = 489
    Width = 30
    Height = 30
    BevelKind = bkSoft
    Color = clMedGray
    TabOrder = 12
    OnClick = pnlGridClClick
  end
  object ckbScale: TCheckBox
    Left = 521
    Top = 234
    Width = 137
    Height = 17
    Caption = #1054#1076#1080#1085#1072#1082#1086#1074#1099#1081' '#1084#1072#1089#1096#1090#1072#1073
    Checked = True
    State = cbChecked
    TabOrder = 13
    OnClick = ckbScaleClick
  end
  object pnlGrp2Cl: TPanel
    Left = 181
    Top = 489
    Width = 30
    Height = 30
    BevelKind = bkSoft
    Color = 8404992
    TabOrder = 14
    OnClick = pnlGrp2ClClick
  end
  object Panel1: TPanel
    Left = 42
    Top = 18
    Width = 415
    Height = 395
    BevelKind = bkFlat
    BevelWidth = 3
    TabOrder = 15
    object imgGraphic: TImage
      Left = 3
      Top = 3
      Width = 405
      Height = 385
      Cursor = crCross
      Align = alClient
      ExplicitLeft = -5
      ExplicitTop = 0
    end
  end
  object RadioGroup1: TRadioGroup
    Left = 515
    Top = 196
    Width = 153
    Height = 74
    Caption = #1052#1072#1089#1096#1090#1072#1073
    Items.Strings = (
      #1054#1076#1080#1085#1072#1082#1086#1074#1099#1081
      #1056#1072#1079#1085#1099#1081)
    TabOrder = 16
    OnClick = RadioGroup1Click
  end
  object ColorDialog1: TColorDialog
    Left = 387
    Top = 472
  end
end
