object Form1: TForm1
  Left = 146
  Top = 0
  Caption = 'Form1'
  ClientHeight = 691
  ClientWidth = 804
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poDesigned
  OnCloseQuery = FormCloseQuery
  OnCreate = FormCreate
  OnResize = FormResize
  PixelsPerInch = 96
  TextHeight = 13
  object Label2: TLabel
    Left = 23
    Top = 135
    Width = 34
    Height = 13
    Caption = #1042#1088#1077#1084#1103':'
  end
  object Label3: TLabel
    Left = 23
    Top = 213
    Width = 34
    Height = 13
    Caption = #1042#1088#1077#1084#1103':'
  end
  object Label4: TLabel
    Left = 16
    Top = 448
    Width = 110
    Height = 13
    Caption = #1050#1086#1083#1080#1095#1077#1089#1090#1074#1086' '#1079#1072#1087#1080#1089#1077#1081': '
    Visible = False
  end
  object Label5: TLabel
    Left = 132
    Top = 448
    Width = 3
    Height = 13
    Visible = False
  end
  object Label6: TLabel
    Left = 23
    Top = 232
    Width = 81
    Height = 13
    Caption = #1057#1088#1077#1076#1085#1077#1077' '#1074#1088#1077#1084#1103':'
    Visible = False
  end
  object Label7: TLabel
    Left = 23
    Top = 154
    Width = 81
    Height = 13
    Caption = #1057#1088#1077#1076#1085#1077#1077' '#1074#1088#1077#1084#1103':'
    Visible = False
  end
  object Label8: TLabel
    Left = 74
    Top = 135
    Width = 6
    Height = 13
    Caption = '0'
  end
  object Label9: TLabel
    Left = 110
    Top = 154
    Width = 6
    Height = 13
    Caption = '0'
    Visible = False
  end
  object Label10: TLabel
    Left = 73
    Top = 213
    Width = 6
    Height = 13
    Caption = '0'
  end
  object Label11: TLabel
    Left = 122
    Top = 230
    Width = 6
    Height = 13
    Caption = '0'
    Visible = False
  end
  object Button1: TButton
    Left = 16
    Top = 24
    Width = 153
    Height = 25
    Caption = #1044#1086#1073#1072#1074#1080#1090#1100' '
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 16
    Top = 55
    Width = 153
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100
    TabOrder = 1
    OnClick = Button2Click
  end
  object Button4: TButton
    Left = 16
    Top = 104
    Width = 153
    Height = 25
    Caption = #1057#1086#1088#1090#1080#1088#1086#1074#1082#1072
    TabOrder = 2
    OnClick = Button4Click
  end
  object Button5: TButton
    Left = 16
    Top = 281
    Width = 153
    Height = 25
    Caption = #1055#1086#1080#1089#1082' '#1087#1086' '#1082#1086#1084#1087#1086#1079#1080#1090#1086#1088#1091
    TabOrder = 3
    OnClick = Button5Click
  end
  object Button6: TButton
    Left = 16
    Top = 182
    Width = 153
    Height = 25
    Caption = #1057#1086#1088#1090#1080#1088#1086#1074#1082#1072' '#1087#1086' '#1082#1083#1102#1095#1072#1084
    TabOrder = 4
    OnClick = Button6Click
  end
  object ListView1: TListView
    Left = 192
    Top = 5
    Width = 593
    Height = 676
    Align = alCustom
    Columns = <
      item
        Caption = 'Caption'
        Width = 0
      end
      item
        Caption = 'Caption'
        Width = 160
      end
      item
        Caption = 'Perfromance'
        Width = 210
      end
      item
        Caption = 'Perform Type'
        Width = 80
      end
      item
        Width = 100
      end>
    GridLines = True
    ReadOnly = True
    RowSelect = True
    ShowWorkAreas = True
    TabOrder = 5
    ViewStyle = vsReport
  end
  object GroupBox1: TGroupBox
    Left = 8
    Top = 328
    Width = 170
    Height = 89
    TabOrder = 6
    object Label1: TLabel
      Left = 18
      Top = 63
      Width = 59
      Height = 13
      Caption = #1082#1086#1083#1080#1095#1077#1089#1090#1074#1086
    end
    object btnGenerate: TButton
      Left = 15
      Top = 24
      Width = 138
      Height = 25
      Caption = #1043#1077#1085#1077#1088#1080#1088#1086#1074#1072#1090#1100' '#1089#1083#1091#1095#1072#1081#1085#1086
      TabOrder = 0
      OnClick = btnGenerateClick
    end
    object SpinEdit1: TEdit
      Left = 83
      Top = 55
      Width = 54
      Height = 21
      TabOrder = 1
      Text = '45'
    end
    object UpDown1: TUpDown
      Left = 137
      Top = 55
      Width = 15
      Height = 21
      Associate = SpinEdit1
      Position = 45
      TabOrder = 2
    end
  end
end
