object Form1: TForm1
  Left = 0
  Top = 0
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = 'Form1'
  ClientHeight = 657
  ClientWidth = 910
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Image1: TImage
    Left = 243
    Top = 0
    Width = 667
    Height = 657
    Align = alRight
    ExplicitLeft = 242
    ExplicitHeight = 655
  end
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 241
    Height = 657
    Align = alLeft
    Color = 12770007
    TabOrder = 0
    object Label1: TLabel
      Left = 16
      Top = 8
      Width = 37
      Height = 19
      Caption = #1043#1088#1072#1092
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -16
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
    end
    object Label2: TLabel
      Left = 16
      Top = 95
      Width = 147
      Height = 19
      Caption = #1052#1072#1090#1088#1080#1094#1072' '#1089#1084#1077#1078#1085#1086#1089#1090#1080
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -16
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
    end
    object Label3: TLabel
      Left = 16
      Top = 324
      Width = 161
      Height = 38
      Caption = #1052#1072#1090#1088#1080#1094#1072' '#1082#1088#1072#1090#1095#1072#1081#1096#1080#1093' '#1088#1072#1089#1089#1090#1086#1103#1085#1080#1081
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -16
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      WordWrap = True
    end
    object lbcConnect: TCheckListBox
      Left = 8
      Top = 33
      Width = 220
      Height = 56
      OnClickCheck = lbcConnectClickCheck
      Color = 14541804
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = []
      ItemHeight = 16
      Items.Strings = (
        #1057#1083#1072#1073#1086#1089#1074#1103#1079#1085#1099#1081
        #1057#1074#1103#1079#1085#1099#1081
        #1057#1080#1083#1100#1085#1086#1089#1074#1103#1079#1085#1099#1081)
      ParentFont = False
      TabOrder = 0
    end
    object Button1: TButton
      Left = 16
      Top = 610
      Width = 212
      Height = 33
      Caption = #1047#1072#1075#1088#1091#1079#1080#1090#1100' '#1075#1088#1072#1092
      TabOrder = 1
      OnClick = Button1Click
    end
  end
  object StringGrid1: TStringGrid
    Left = 8
    Top = 120
    Width = 220
    Height = 188
    BevelInner = bvSpace
    BevelOuter = bvNone
    Color = 15000815
    DefaultColWidth = 27
    DefaultRowHeight = 20
    FixedColor = 12572125
    Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRowSelect]
    TabOrder = 1
  end
  object StringGrid2: TStringGrid
    Left = 8
    Top = 368
    Width = 220
    Height = 188
    BevelInner = bvSpace
    BevelOuter = bvNone
    Color = 14342897
    DefaultColWidth = 27
    DefaultRowHeight = 20
    FixedColor = 12572125
    Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRowSelect]
    TabOrder = 2
  end
  object OpenDialog1: TOpenDialog
    Left = 24
    Top = 616
  end
end
