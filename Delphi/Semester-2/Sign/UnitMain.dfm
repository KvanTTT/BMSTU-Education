object Form1: TForm1
  Left = 88
  Top = 140
  Caption = #1047#1085#1072#1082
  ClientHeight = 464
  ClientWidth = 671
  Color = clBtnFace
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
    Left = 497
    Top = 70
    Width = 62
    Height = 13
    Caption = #1059#1075#1086#1083' '#1089#1082#1083#1086#1085#1072
  end
  object Label2: TLabel
    Left = 496
    Top = 185
    Width = 120
    Height = 13
    Caption = #1057#1082#1086#1088#1086#1089#1090#1100' '#1087#1077#1088#1077#1084#1077#1097#1077#1085#1080#1103
  end
  object Label3: TLabel
    Left = 560
    Top = 149
    Width = 44
    Height = 13
    Caption = #1087#1080#1082#1089'/'#1089#1077#1082
  end
  object Label4: TLabel
    Left = 560
    Top = 92
    Width = 47
    Height = 13
    Caption = #1075#1088#1072#1076#1091#1089#1086#1074
  end
  object Label5: TLabel
    Left = 499
    Top = 240
    Width = 55
    Height = 13
    Caption = #1062#1074#1077#1090' '#1092#1086#1085#1072
  end
  object SpeedButton1: TSpeedButton
    Left = 496
    Top = 376
    Width = 137
    Height = 33
    AllowAllUp = True
    GroupIndex = 1
    Caption = #1057#1090#1072#1088#1090
    OnClick = SpeedButton1Click
  end
  object Label6: TLabel
    Left = 499
    Top = 284
    Width = 64
    Height = 13
    Caption = #1062#1074#1077#1090' '#1089#1082#1083#1086#1085#1072
  end
  object Label7: TLabel
    Left = 497
    Top = 24
    Width = 45
    Height = 13
    Caption = #1052#1072#1089#1096#1090#1072#1073
  end
  object Label8: TLabel
    Left = 497
    Top = 124
    Width = 102
    Height = 13
    Caption = #1057#1082#1086#1088#1086#1089#1090#1100' '#1074#1088#1072#1097#1077#1085#1080#1103
  end
  object Label9: TLabel
    Left = 499
    Top = 204
    Width = 44
    Height = 13
    Caption = #1087#1080#1082#1089'/'#1089#1077#1082
  end
  object Panel1: TPanel
    Left = 8
    Top = 8
    Width = 473
    Height = 448
    BevelInner = bvRaised
    BevelKind = bkTile
    BevelWidth = 2
    TabOrder = 0
    object Image: TImage
      Left = 4
      Top = 4
      Width = 461
      Height = 436
      Align = alClient
      ExplicitLeft = 12
      ExplicitTop = 16
    end
  end
  object pnlBkgCl: TPanel
    Left = 574
    Top = 232
    Width = 30
    Height = 30
    BevelKind = bkTile
    BevelOuter = bvLowered
    BevelWidth = 2
    Color = 15134149
    TabOrder = 3
    OnClick = pnlBkgClClick
  end
  object Panel2: TPanel
    Left = 574
    Top = 276
    Width = 30
    Height = 30
    BevelKind = bkTile
    BevelOuter = bvLowered
    BevelWidth = 2
    Color = 3627887
    TabOrder = 1
    OnClick = Panel2Click
  end
  object Edit1: TEdit
    Left = 499
    Top = 43
    Width = 55
    Height = 21
    TabOrder = 2
    Text = '1'
    OnExit = Edit1Exit
    OnKeyPress = Edit1KeyPress
  end
  object edtAngle: TEdit
    Left = 499
    Top = 89
    Width = 55
    Height = 21
    TabOrder = 4
    Text = '70'
    OnKeyPress = edtAngleKeyPress
  end
  object edtSpeed: TEdit
    Left = 499
    Top = 143
    Width = 55
    Height = 21
    TabOrder = 5
    Text = '50'
    OnChange = edtSpeedChange
    OnKeyPress = edtSpeedKeyPress
  end
  object Button1: TButton
    Left = 496
    Top = 431
    Width = 137
    Height = 25
    Caption = #1053#1072#1095#1072#1083#1100#1085#1086#1077' '#1087#1086#1083#1086#1078#1077#1085#1080#1077
    TabOrder = 6
    OnClick = Button1Click
  end
  object Timer1: TTimer
    Enabled = False
    Interval = 50
    OnTimer = Timer1Timer
    Left = 428
    Top = 48
  end
  object ColorDialog1: TColorDialog
    Left = 429
    Top = 96
  end
end
