object Form1: TForm1
  Left = 0
  Top = 0
  Caption = 'Form1'
  ClientHeight = 697
  ClientWidth = 829
  Color = 13230069
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnKeyPress = FormKeyPress
  PixelsPerInch = 96
  TextHeight = 13
  object Image1: TImage
    Left = 8
    Top = 8
    Width = 808
    Height = 569
  end
  object Label1: TLabel
    Left = 304
    Top = 649
    Width = 121
    Height = 16
    Caption = #1050#1086#1083#1080#1095#1077#1089#1090#1074#1086' '#1089#1083#1086#1074' '#1085#1072' '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNone
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 431
    Top = 647
    Width = 4
    Height = 16
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNone
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label3: TLabel
    Left = 504
    Top = 644
    Width = 96
    Height = 16
    Caption = #1055#1086#1080#1089#1082' '#1074' '#1076#1077#1088#1077#1074#1077':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNone
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label4: TLabel
    Left = 504
    Top = 663
    Width = 92
    Height = 16
    Caption = #1055#1086#1080#1089#1082' '#1074' '#1092#1072#1081#1083#1077':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNone
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label5: TLabel
    Left = 624
    Top = 644
    Width = 37
    Height = 16
    Caption = 'Label5'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNone
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label6: TLabel
    Left = 624
    Top = 663
    Width = 37
    Height = 16
    Caption = 'Label6'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNone
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Button1: TButton
    Left = 16
    Top = 644
    Width = 194
    Height = 25
    Caption = #1055#1086#1080#1089#1082' '#1089#1083#1086#1074' '#1085#1072' '#1079#1072#1076#1072#1085#1085#1091#1102' '#1073#1091#1082#1074#1091
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNone
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 0
    OnClick = Button1Click
  end
  object Edit1: TEdit
    Left = 216
    Top = 646
    Width = 57
    Height = 24
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNone
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    ReadOnly = True
    TabOrder = 1
    Text = 'a'
    OnKeyPress = Edit1KeyPress
  end
  object Button2: TButton
    Left = 712
    Top = 644
    Width = 104
    Height = 25
    Caption = #1047#1072#1075#1088#1091#1079#1080#1090#1100' '#1092#1072#1081#1083
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNone
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 2
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 224
    Top = 599
    Width = 121
    Height = 25
    Caption = #1044#1086#1073#1072#1074#1080#1090#1100' '#1089#1083#1086#1074#1086
    TabOrder = 3
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 368
    Top = 599
    Width = 121
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100' '#1089#1083#1086#1074#1086
    TabOrder = 4
    OnClick = Button4Click
  end
  object LE: TLabeledEdit
    Left = 16
    Top = 601
    Width = 194
    Height = 21
    EditLabel.Width = 31
    EditLabel.Height = 13
    EditLabel.Caption = #1057#1083#1086#1074#1086
    TabOrder = 5
  end
  object OpenDialog1: TOpenDialog
    Left = 760
    Top = 512
  end
end
