object Grid: TGrid
  Left = 290
  Top = 205
  BorderIcons = [biSystemMenu]
  BorderStyle = bsSizeToolWin
  ClientHeight = 159
  ClientWidth = 193
  Color = 16447443
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object GroupBox1: TGroupBox
    Left = 0
    Top = 5
    Width = 185
    Height = 56
    Caption = #1055#1072#1088#1072#1084#1077#1090#1088#1099' '#1080#1085#1090#1077#1088#1074#1072#1083#1072' '#1087#1086' '#1086#1089#1080' '#1054#1061
    TabOrder = 3
    object Label3: TLabel
      Left = 10
      Top = 15
      Width = 165
      Height = 13
      Caption = #1063#1080#1089#1083#1086' '#1083#1080#1085#1080#1081', '#1086#1073#1088#1072#1079#1091#1102#1097#1080#1093' '#1089#1077#1090#1082#1091
    end
  end
  object EditSX: TEdit
    Left = 60
    Top = 35
    Width = 56
    Height = 21
    Hint = #1063#1080#1089#1083#1086' '#1083#1080#1085#1080#1081' '#1089#1077#1090#1082#1080
    ParentShowHint = False
    ShowHint = True
    TabOrder = 0
    Text = '40'
  end
  object GroupBox2: TGroupBox
    Left = 1
    Top = 65
    Width = 185
    Height = 56
    Caption = #1055#1072#1088#1072#1084#1077#1090#1088#1099' '#1080#1085#1090#1077#1088#1074#1072#1083#1072' '#1087#1086' '#1086#1089#1080' '#1054#1059
    TabOrder = 4
    object Label6: TLabel
      Left = 10
      Top = 15
      Width = 165
      Height = 13
      Hint = #1063#1080#1089#1083#1086' '#1083#1080#1085#1080#1081' '#1089#1077#1090#1082#1080
      Caption = #1063#1080#1089#1083#1086' '#1083#1080#1085#1080#1081', '#1086#1073#1088#1072#1079#1091#1102#1097#1080#1093' '#1089#1077#1090#1082#1091
      ParentShowHint = False
      ShowHint = True
    end
  end
  object EditSY: TEdit
    Left = 60
    Top = 95
    Width = 56
    Height = 21
    TabOrder = 1
    Text = '40'
  end
  object btnOk: TButton
    Left = 55
    Top = 125
    Width = 75
    Height = 21
    Caption = 'OK'
    Default = True
    TabOrder = 2
    OnClick = btnOkClick
  end
end
