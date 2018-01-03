object Form1: TForm1
  Left = 37
  Top = 0
  Caption = 'Form1'
  ClientHeight = 687
  ClientWidth = 866
  Color = 13230069
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poDesigned
  OnClose = FormClose
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Label10: TLabel
    Left = 424
    Top = 376
    Width = 37
    Height = 13
    Caption = 'Label10'
  end
  object Button1: TButton
    Left = 8
    Top = 551
    Width = 194
    Height = 25
    Caption = #1055#1086#1080#1089#1082' '#1089#1083#1086#1074#1072
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
    Left = 8
    Top = 582
    Width = 194
    Height = 24
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNone
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 1
    OnKeyPress = Edit1KeyPress
  end
  object Button2: TButton
    Left = 8
    Top = 648
    Width = 194
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
  object PageControl1: TPageControl
    Left = 0
    Top = 0
    Width = 866
    Height = 545
    ActivePage = TabSheet3
    Align = alTop
    TabOrder = 3
    object TabSheet1: TTabSheet
      Caption = #1044#1077#1088#1077#1074#1086
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 0
      object imgTree: TImage
        Left = 0
        Top = 0
        Width = 858
        Height = 517
        Align = alClient
        ExplicitLeft = 424
        ExplicitTop = 280
        ExplicitWidth = 105
        ExplicitHeight = 105
      end
    end
    object TabSheet2: TTabSheet
      Caption = #1057#1073#1072#1083#1072#1085#1089#1080#1088#1086#1074#1072#1085#1085#1086#1077' '#1076#1077#1088#1077#1074#1086
      ImageIndex = 1
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 0
      object imgBalanceTree: TImage
        AlignWithMargins = True
        Left = 3
        Top = 3
        Width = 852
        Height = 511
        Align = alClient
        ExplicitTop = 39
        ExplicitWidth = 893
        ExplicitHeight = 599
      end
    end
    object TabSheet3: TTabSheet
      Caption = #1061#1101#1096' '#1090#1072#1083#1073#1083#1080#1094#1072' '#1089#1086' '#1089#1087#1080#1089#1082#1072#1084#1080
      ImageIndex = 2
      object imgHashTableList: TImage
        Left = 0
        Top = 0
        Width = 858
        Height = 517
        Align = alClient
        ExplicitLeft = 496
        ExplicitTop = 336
        ExplicitWidth = 105
        ExplicitHeight = 105
      end
    end
    object TabSheet4: TTabSheet
      Caption = #1061#1077#1096' '#1090#1072#1073#1083#1080#1094#1072' '#1089' '#1079#1072#1082#1088#1099#1090#1086#1081' '#1072#1076#1088#1077#1089#1072#1094#1080#1077#1081
      ImageIndex = 3
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 0
      object imgHashTableClosed: TImage
        Left = 0
        Top = 0
        Width = 858
        Height = 517
        Align = alClient
        ExplicitLeft = 256
        ExplicitTop = 200
        ExplicitWidth = 105
        ExplicitHeight = 105
      end
    end
  end
  object SG: TStringGrid
    Left = 208
    Top = 551
    Width = 654
    Height = 132
    Color = 14806983
    ColCount = 4
    DefaultColWidth = 250
    FixedColor = 8710790
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Verdana'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 4
    OnClick = SGClick
    ColWidths = (
      250
      121
      134
      126)
    RowHeights = (
      24
      24
      24
      24
      23)
  end
  object Button3: TButton
    Left = 8
    Top = 617
    Width = 194
    Height = 25
    Caption = #1056#1077#1089#1090#1088#1091#1082#1090#1091#1088#1080#1079#1072#1094#1080#1103
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 5
    OnClick = Button3Click
  end
  object OpenDialog1: TOpenDialog
    Left = 16
    Top = 728
  end
end
