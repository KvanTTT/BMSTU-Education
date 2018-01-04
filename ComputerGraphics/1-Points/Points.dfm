object Form1: TForm1
  Left = 110
  Top = 38
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = #1058#1086#1095#1082#1080
  ClientHeight = 687
  ClientWidth = 744
  Color = 13557473
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
    Left = 527
    Top = 382
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
    Left = 527
    Top = 418
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
  object Label7: TLabel
    Left = 531
    Top = 279
    Width = 173
    Height = 16
    Caption = #1044#1086#1073#1072#1074#1080#1090#1100' '#1090#1086#1095#1082#1091' '#1074' '#1084#1085#1086#1078#1077#1089#1090#1074#1086
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label3: TLabel
    Left = 529
    Top = 461
    Width = 118
    Height = 16
    Caption = #1050#1086#1086#1088#1076#1080#1085#1072#1090#1099' '#1084#1099#1096#1080': '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label4: TLabel
    Left = 664
    Top = 461
    Width = 4
    Height = 16
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label8: TLabel
    Left = 529
    Top = 489
    Width = 64
    Height = 16
    Caption = #1058#1086#1095#1085#1086#1089#1090#1100': '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label13: TLabel
    Left = 16
    Top = 659
    Width = 82
    Height = 13
    Caption = #1042#1089#1077#1075#1086' '#1088#1077#1096#1077#1085#1080#1081': '
  end
  object Label14: TLabel
    Left = 120
    Top = 659
    Width = 3
    Height = 13
  end
  object stgPoints: TStringGrid
    Left = 527
    Top = 10
    Width = 210
    Height = 255
    BevelInner = bvSpace
    BevelKind = bkTile
    Color = 16055295
    ColCount = 3
    DefaultColWidth = 35
    FixedColor = 13546899
    RowCount = 21
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goColSizing, goThumbTracking]
    ParentFont = False
    ScrollBars = ssVertical
    TabOrder = 0
    OnSelectCell = stgPointsSelectCell
    ColWidths = (
      35
      69
      75)
  end
  object BitBtn1: TBitBtn
    Left = 527
    Top = 298
    Width = 96
    Height = 25
    Caption = '1'
    TabOrder = 1
    OnClick = BitBtn1Click
  end
  object BitBtn2: TBitBtn
    Left = 527
    Top = 337
    Width = 96
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100' '#1090#1086#1095#1082#1091
    TabOrder = 2
    OnClick = BitBtn2Click
  end
  object pnlPntCl: TPanel
    Left = 607
    Top = 376
    Width = 30
    Height = 30
    BevelKind = bkSoft
    Color = 640927
    TabOrder = 3
    OnClick = pnlPntClClick
  end
  object pnlBgCl: TPanel
    Left = 607
    Top = 412
    Width = 30
    Height = 30
    BevelKind = bkSoft
    Color = clCream
    TabOrder = 4
    OnClick = pnlBgClClick
  end
  object BitBtn3: TBitBtn
    Left = 629
    Top = 337
    Width = 108
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100' '#1074#1089#1077' '#1090#1086#1095#1082#1080
    TabOrder = 5
    OnClick = BitBtn3Click
  end
  object sgDecision: TStringGrid
    Left = 8
    Top = 519
    Width = 513
    Height = 130
    ColCount = 7
    DefaultColWidth = 71
    DefaultRowHeight = 20
    RowCount = 2
    Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRangeSelect, goColSizing, goRowSelect, goThumbTracking]
    TabOrder = 6
    RowHeights = (
      20
      20)
  end
  object GroupBox2: TGroupBox
    Left = 527
    Top = 519
    Width = 210
    Height = 130
    Caption = #1056#1091#1082#1086#1074#1086#1076#1089#1090#1074#1086
    TabOrder = 7
    object Label5: TLabel
      Left = 19
      Top = 19
      Width = 136
      Height = 26
      Caption = #1051#1077#1074#1099#1081' '#1082#1083#1080#1082' - '#1076#1086#1073#1072#1074#1080#1090#1100' '#1090#1086#1095#1082#1091' '#1074' '#1087#1077#1088#1074#1086#1077' '#1084#1085#1086#1078#1077#1089#1090#1074#1086
      WordWrap = True
    end
    object Label6: TLabel
      Left = 19
      Top = 51
      Width = 142
      Height = 26
      Caption = #1055#1088#1072#1074#1099#1081' '#1082#1083#1080#1082' - '#1076#1086#1073#1072#1074#1080#1090#1100' '#1090#1086#1095#1082#1091' '#1074#1086' '#1074#1090#1086#1088#1086#1077' '#1084#1085#1086#1078#1077#1089#1090#1074#1086
      WordWrap = True
    end
    object Label9: TLabel
      Left = 19
      Top = 86
      Width = 111
      Height = 26
      Caption = 'Ctrl + '#1082#1083#1080#1082' - '#1091#1076#1072#1083#1080#1090#1100' '#1090#1086#1095#1082#1091
      WordWrap = True
    end
    object Label10: TLabel
      Left = 19
      Top = 51
      Width = 76
      Height = 13
      Caption = #1055#1088#1072#1074#1099#1081' '#1082#1083#1080#1082' - '
      Color = 13557473
      Font.Charset = DEFAULT_CHARSET
      Font.Color = 183
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentColor = False
      ParentFont = False
      WordWrap = True
    end
    object Label11: TLabel
      Left = 19
      Top = 86
      Width = 65
      Height = 13
      Caption = 'Ctrl + '#1082#1083#1080#1082' - '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = 183
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      WordWrap = True
    end
    object Label12: TLabel
      Left = 19
      Top = 19
      Width = 70
      Height = 13
      Caption = #1051#1077#1074#1099#1081' '#1082#1083#1080#1082' - '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = 183
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      WordWrap = True
    end
  end
  object pnlPntCl2: TPanel
    Left = 651
    Top = 376
    Width = 30
    Height = 30
    BevelKind = bkSoft
    Color = 877296
    TabOrder = 8
    OnClick = pnlPntCl2Click
  end
  object BitBtn4: TBitBtn
    Left = 629
    Top = 298
    Width = 108
    Height = 25
    Caption = '2'
    TabOrder = 9
    OnClick = BitBtn4Click
  end
  object ScrollBox1: TScrollBox
    Left = 9
    Top = 11
    Width = 505
    Height = 496
    BevelInner = bvNone
    BevelOuter = bvRaised
    BevelKind = bkSoft
    BevelWidth = 2
    TabOrder = 10
    object imgCanvas: TImage
      Left = 0
      Top = 0
      Width = 497
      Height = 488
      Cursor = crCross
      Align = alClient
      OnMouseMove = imgCanvasMouseMove
      OnMouseUp = imgCanvasMouseUp
      ExplicitLeft = 1
      ExplicitTop = 1
    end
  end
  object Edit1: TEdit
    Left = 667
    Top = 485
    Width = 70
    Height = 21
    TabOrder = 11
    Text = '1E'
    OnChange = Edit1Change
    OnExit = Edit1Exit
  end
  object Button1: TButton
    Left = 311
    Top = 655
    Width = 210
    Height = 25
    Caption = #1055#1086#1082#1072#1079#1072#1090#1100' '#1088#1077#1096#1077#1085#1080#1077
    TabOrder = 12
    OnClick = Button1Click
  end
  object ColorDialog1: TColorDialog
    Left = 41
    Top = 48
  end
end
