object Form1: TForm1
  Left = 0
  Top = 0
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = #1040#1085#1080#1084#1072#1094#1080#1103
  ClientHeight = 586
  ClientWidth = 734
  Color = 14736585
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 8
    Top = 548
    Width = 115
    Height = 16
    Caption = #1057#1082#1086#1088#1086#1089#1090#1100' '#1072#1085#1080#1084#1072#1094#1080#1080
    Font.Charset = DEFAULT_CHARSET
    Font.Color = 1851968
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 222
    Top = 546
    Width = 36
    Height = 16
    Caption = '%/'#1089#1077#1082
    Font.Charset = DEFAULT_CHARSET
    Font.Color = 2048285
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label3: TLabel
    Left = 288
    Top = 548
    Width = 99
    Height = 16
    Caption = #1050#1086#1083#1080#1095#1077#1089#1090#1074#1086' '#1077#1083#1086#1082
    Font.Charset = DEFAULT_CHARSET
    Font.Color = 1851968
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Panel1: TPanel
    Left = 8
    Top = 8
    Width = 705
    Height = 513
    BevelInner = bvLowered
    TabOrder = 0
    object Image1: TImage
      Left = 2
      Top = 2
      Width = 701
      Height = 509
      Align = alClient
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 631
    end
  end
  object Edit1: TEdit
    Left = 144
    Top = 545
    Width = 57
    Height = 21
    TabOrder = 1
    Text = '3'
    OnChange = Edit1Change
  end
  object Edit2: TEdit
    Left = 408
    Top = 547
    Width = 57
    Height = 21
    TabOrder = 2
    Text = '5'
    OnChange = Edit2Change
  end
  object UpDown1: TUpDown
    Left = 465
    Top = 547
    Width = 15
    Height = 21
    Associate = Edit2
    Max = 20
    Position = 5
    TabOrder = 3
  end
  object Button1: TButton
    Left = 528
    Top = 541
    Width = 81
    Height = 25
    Caption = #1057#1090#1072#1088#1090
    TabOrder = 4
    OnClick = Button1Click
  end
  object UpDown2: TUpDown
    Left = 201
    Top = 545
    Width = 15
    Height = 21
    Associate = Edit1
    Position = 3
    TabOrder = 5
  end
  object Button2: TButton
    Left = 632
    Top = 541
    Width = 81
    Height = 25
    Caption = #1047#1072#1085#1086#1074#1086
    TabOrder = 6
    OnClick = Button2Click
  end
  object Timer1: TTimer
    Enabled = False
    Interval = 100
    OnTimer = Timer1Timer
    Left = 584
    Top = 72
  end
end
