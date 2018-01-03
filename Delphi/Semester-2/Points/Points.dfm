object Form1: TForm1
  Left = 0
  Top = 0
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = #1058#1086#1095#1082#1080
  ClientHeight = 611
  ClientWidth = 656
  Color = 13557473
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 16
    Top = 525
    Width = 66
    Height = 16
    Caption = #1062#1074#1077#1090' '#1090#1086#1095#1077#1082
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 18
    Top = 574
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
  object Label8: TLabel
    Left = 328
    Top = 551
    Width = 50
    Height = 13
    Caption = #1056#1072#1079#1085#1086#1089#1090#1100':'
  end
  object stgPoints: TStringGrid
    Left = 479
    Top = 17
    Width = 154
    Height = 256
    BevelInner = bvSpace
    BevelKind = bkTile
    Color = 16055295
    ColCount = 3
    DefaultColWidth = 35
    FixedColor = 14606288
    RowCount = 2
    Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRangeSelect, goEditing, goRowSelect]
    ScrollBars = ssVertical
    TabOrder = 0
    OnSelectCell = stgPointsSelectCell
    ColWidths = (
      35
      55
      55)
  end
  object BitBtn1: TBitBtn
    Left = 479
    Top = 441
    Width = 154
    Height = 25
    Caption = #1044#1086#1073#1072#1074#1080#1090#1100' '#1090#1086#1095#1082#1091
    TabOrder = 1
    OnClick = BitBtn1Click
  end
  object BitBtn2: TBitBtn
    Left = 479
    Top = 472
    Width = 154
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100' '#1090#1086#1095#1082#1091
    TabOrder = 2
    OnClick = BitBtn2Click
  end
  object Panel1: TPanel
    Left = 16
    Top = 16
    Width = 433
    Height = 489
    BevelWidth = 3
    TabOrder = 3
    object imgCanvas: TImage
      Left = 3
      Top = 3
      Width = 427
      Height = 483
      Cursor = crCross
      Align = alClient
      OnMouseMove = imgCanvasMouseMove
      OnMouseUp = imgCanvasMouseUp
      ExplicitLeft = -15
      ExplicitTop = -29
      ExplicitWidth = 383
      ExplicitHeight = 430
    end
  end
  object pnlPntCl: TPanel
    Left = 96
    Top = 516
    Width = 30
    Height = 30
    BevelKind = bkSoft
    Color = clRed
    TabOrder = 4
    OnClick = pnlPntClClick
  end
  object pnlBgCl: TPanel
    Left = 96
    Top = 565
    Width = 30
    Height = 30
    BevelKind = bkSoft
    Color = clCream
    TabOrder = 5
    OnClick = pnlBgClClick
  end
  object BitBtn3: TBitBtn
    Left = 479
    Top = 503
    Width = 154
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100' '#1074#1089#1077' '#1090#1086#1095#1082#1080
    TabOrder = 6
    OnClick = BitBtn3Click
  end
  object Button1: TButton
    Left = 479
    Top = 540
    Width = 154
    Height = 54
    Caption = #1042#1099#1087#1086#1083#1085#1080#1090#1100' '#1079#1072#1076#1072#1095#1091
    TabOrder = 7
  end
  object GroupBox1: TGroupBox
    Left = 479
    Top = 287
    Width = 154
    Height = 58
    Caption = #1050#1086#1086#1088#1076#1080#1085#1072#1090#1099' '#1084#1099#1096#1080
    TabOrder = 8
    object Label3: TLabel
      Left = 14
      Top = 18
      Width = 13
      Height = 13
      Caption = 'X: '
    end
    object Label4: TLabel
      Left = 14
      Top = 37
      Width = 13
      Height = 13
      Caption = 'Y: '
    end
  end
  object StringGrid1: TStringGrid
    Left = 163
    Top = 516
    Width = 128
    Height = 87
    ColCount = 3
    DefaultColWidth = 40
    DefaultRowHeight = 20
    RowCount = 4
    TabOrder = 9
    RowHeights = (
      20
      20
      20
      20)
  end
  object GroupBox2: TGroupBox
    Left = 479
    Top = 351
    Width = 167
    Height = 74
    Caption = #1056#1091#1082#1086#1074#1086#1076#1089#1090#1074#1086
    TabOrder = 10
    object Label5: TLabel
      Left = 12
      Top = 28
      Width = 152
      Height = 13
      Caption = #1051#1077#1074#1099#1081' '#1082#1083#1080#1082' - '#1076#1086#1073#1072#1074#1080#1090#1100' '#1090#1086#1095#1082#1091
    end
    object Label6: TLabel
      Left = 12
      Top = 47
      Width = 152
      Height = 13
      Caption = #1055#1088#1072#1074#1099#1081' '#1082#1083#1080#1082' - '#1091#1076#1072#1083#1080#1090#1100' '#1090#1086#1095#1082#1091
    end
  end
  object ColorDialog1: TColorDialog
    Left = 208
    Top = 456
  end
end
