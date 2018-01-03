object Form2: TForm2
  Left = 352
  Top = 346
  Caption = #1041#1086#1083#1100#1096#1080#1077' '#1095#1080#1089#1083#1072
  ClientHeight = 362
  ClientWidth = 363
  Color = 12048870
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
  object Label1: TLabel
    Left = 24
    Top = 39
    Width = 108
    Height = 13
    Caption = #1042#1074#1077#1076#1080#1090#1077' '#1074#1099#1088#1072#1078#1077#1085#1080#1077':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNavy
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Memo1: TMemo
    Left = 24
    Top = 58
    Width = 305
    Height = 145
    ScrollBars = ssVertical
    TabOrder = 0
    OnKeyPress = Memo1KeyPress
  end
  object StaticText1: TStaticText
    Left = 24
    Top = 16
    Width = 182
    Height = 17
    Caption = #1044#1086#1087#1091#1089#1090#1080#1084#1099#1077' '#1086#1087#1077#1088#1072#1094#1080#1080': +, -, *, /, ! '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNavy
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 1
  end
  object LabeledEdit1: TLabeledEdit
    Left = 24
    Top = 272
    Width = 305
    Height = 21
    EditLabel.Width = 36
    EditLabel.Height = 13
    EditLabel.Caption = #1054#1090#1074#1077#1090':'
    EditLabel.Font.Charset = DEFAULT_CHARSET
    EditLabel.Font.Color = 7281199
    EditLabel.Font.Height = -11
    EditLabel.Font.Name = 'Tahoma'
    EditLabel.Font.Style = []
    EditLabel.ParentFont = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = 11800169
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    ReadOnly = True
    TabOrder = 2
  end
  object Button1: TButton
    Left = 123
    Top = 225
    Width = 118
    Height = 25
    Caption = #1055#1086#1089#1095#1080#1090#1072#1090#1100
    TabOrder = 3
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 123
    Top = 320
    Width = 118
    Height = 25
    Caption = #1042#1099#1076#1077#1083#1080#1090#1100' '#1074#1089#1077' '#1095#1080#1089#1083#1086
    TabOrder = 4
    OnClick = Button2Click
  end
end
