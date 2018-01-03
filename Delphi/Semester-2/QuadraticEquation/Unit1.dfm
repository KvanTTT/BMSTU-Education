object Form1: TForm1
  Left = 293
  Top = 192
  Caption = #1056#1077#1096#1077#1085#1080#1077' '#1082#1074#1072#1076#1088#1072#1090#1085#1099#1093' '#1091#1088#1072#1074#1085#1077#1085#1080#1081
  ClientHeight = 266
  ClientWidth = 383
  Color = 9750735
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
    Left = 34
    Top = 95
    Width = 8
    Height = 14
    Caption = 'A'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Times New Roman'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 139
    Top = 95
    Width = 7
    Height = 14
    Caption = 'B'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Times New Roman'
    Font.Style = []
    ParentFont = False
  end
  object Label3: TLabel
    Left = 243
    Top = 95
    Width = 7
    Height = 14
    Caption = 'C'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Times New Roman'
    Font.Style = []
    ParentFont = False
  end
  object lblX1: TLabel
    Left = 26
    Top = 201
    Width = 32
    Height = 16
    Caption = 'X1 ='
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object lblX2: TLabel
    Left = 26
    Top = 223
    Width = 32
    Height = 16
    Caption = 'X2 ='
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label4: TLabel
    Left = 112
    Top = 8
    Width = 133
    Height = 18
    Caption = #1042#1074#1086#1076' '#1082#1086#1101#1092#1092#1080#1094#1080#1077#1085#1090#1086#1074':'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Trebuchet MS'
    Font.Style = []
    ParentFont = False
  end
  object Label5: TLabel
    Left = 26
    Top = 144
    Width = 68
    Height = 16
    Caption = #1059#1088#1072#1074#1085#1077#1085#1080#1077':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    Visible = False
  end
  object Equation: TLabel
    Left = 112
    Top = 144
    Width = 68
    Height = 16
    Caption = #1059#1088#1072#1074#1085#1077#1085#1080#1077':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    Visible = False
  end
  object Label6: TLabel
    Left = 163
    Top = 176
    Width = 39
    Height = 13
    Caption = #1054#1090#1074#1077#1090':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Edit1: TEdit
    Left = 48
    Top = 92
    Width = 61
    Height = 21
    Color = 13229560
    TabOrder = 0
  end
  object Edit2: TEdit
    Left = 152
    Top = 92
    Width = 61
    Height = 21
    Color = 13229560
    TabOrder = 1
  end
  object Edit3: TEdit
    Left = 256
    Top = 92
    Width = 61
    Height = 21
    Color = 13229560
    TabOrder = 2
  end
  object Button1: TButton
    Left = 144
    Top = 41
    Width = 81
    Height = 25
    Caption = 'InputBox'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Times New Roman'
    Font.Style = []
    ParentFont = False
    TabOrder = 3
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 243
    Top = 41
    Width = 81
    Height = 25
    Caption = 'InputQuery'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Times New Roman'
    Font.Style = []
    ParentFont = False
    TabOrder = 4
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 48
    Top = 41
    Width = 81
    Height = 25
    Caption = 'Edit'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Times New Roman'
    Font.Style = []
    ParentFont = False
    TabOrder = 5
    OnClick = Button3Click
  end
end
