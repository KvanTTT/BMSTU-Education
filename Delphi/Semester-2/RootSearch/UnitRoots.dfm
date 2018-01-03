object Form1: TForm1
  Left = 0
  Top = 0
  BorderIcons = [biSystemMenu, biMinimize]
  Caption = #1055#1086#1080#1089#1082' '#1082#1086#1088#1085#1077#1081' '#1084#1077#1090#1086#1076#1086#1084' '#1093#1086#1088#1076
  ClientHeight = 366
  ClientWidth = 551
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnResize = FormResize
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 8
    Top = 56
    Width = 79
    Height = 13
    Caption = #1051#1077#1074#1072#1103' '#1075#1088#1072#1085#1080#1094#1072':'
  end
  object Label2: TLabel
    Left = 128
    Top = 56
    Width = 85
    Height = 13
    Caption = #1055#1088#1072#1074#1072#1103' '#1075#1088#1072#1085#1080#1094#1072':'
  end
  object Label3: TLabel
    Left = 248
    Top = 56
    Width = 84
    Height = 26
    Caption = #1052#1072#1082#1089#1080#1084#1072#1083#1100#1085#1086#1077' '#1095#1080#1089#1083#1086' '#1080#1090#1077#1088#1072#1094#1080#1081':'
    WordWrap = True
  end
  object Label4: TLabel
    Left = 8
    Top = 144
    Width = 75
    Height = 13
    Caption = #1058#1086#1095#1085#1086#1089#1090#1100' '#1087#1086' Y:'
  end
  object Label5: TLabel
    Left = 8
    Top = 24
    Width = 48
    Height = 13
    Caption = #1060#1091#1085#1082#1094#1080#1103':'
  end
  object Label6: TLabel
    Left = 8
    Top = 110
    Width = 81
    Height = 13
    Caption = #1064#1072#1075' '#1088#1072#1079#1073#1080#1077#1085#1080#1103':'
  end
  object edtLb: TEdit
    Left = 8
    Top = 75
    Width = 90
    Height = 21
    TabOrder = 0
    Text = '0'
  end
  object edtRb: TEdit
    Left = 128
    Top = 75
    Width = 89
    Height = 21
    TabOrder = 1
    Text = '10'
  end
  object ComboBox1: TComboBox
    Left = 72
    Top = 21
    Width = 129
    Height = 21
    ItemHeight = 13
    TabOrder = 2
  end
  object strg: TStringGrid
    Left = 8
    Top = 176
    Width = 532
    Height = 169
    FixedCols = 0
    RowCount = 2
    Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRangeSelect, goColSizing, goRowSelect]
    TabOrder = 3
    ColWidths = (
      80
      98
      105
      100
      77)
  end
  object edtMaxIt: TEdit
    Left = 248
    Top = 107
    Width = 121
    Height = 21
    TabOrder = 4
    Text = '50'
  end
  object edtPrec: TEdit
    Left = 128
    Top = 141
    Width = 89
    Height = 21
    TabOrder = 5
    Text = '1E-7'
  end
  object Button1: TButton
    Left = 390
    Top = 127
    Width = 150
    Height = 37
    Caption = #1053#1072#1081#1090#1080' '#1082#1086#1088#1085#1080
    TabOrder = 6
    OnClick = Button1Click
  end
  object edtStep: TEdit
    Left = 128
    Top = 107
    Width = 89
    Height = 21
    TabOrder = 7
    Text = '3'
  end
  object GroupBox1: TGroupBox
    Left = 390
    Top = 21
    Width = 153
    Height = 100
    Caption = #1050#1086#1076#1099' '#1086#1096#1080#1073#1086#1082
    TabOrder = 8
    object Label7: TLabel
      Left = 16
      Top = 22
      Width = 96
      Height = 26
      Caption = '0 - '#1050#1086#1088#1077#1085#1100' '#1085#1072#1081#1076#1077#1085' '#1084#1077#1090#1086#1076#1086#1084' '#1093#1086#1088#1076
      WordWrap = True
    end
    object Label8: TLabel
      Left = 16
      Top = 59
      Width = 122
      Height = 26
      Caption = '1 - '#1050#1086#1088#1077#1085#1100' '#1085#1072#1081#1076#1077#1085#1100' '#1087#1086#1076#1089#1090#1072#1085#1086#1074#1082#1086#1081' '#1079#1085#1072#1095#1077#1085#1080#1103
      WordWrap = True
    end
  end
end
