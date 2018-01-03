object Form2: TForm2
  Left = 0
  Top = 0
  Caption = 'Form2'
  ClientHeight = 561
  ClientWidth = 449
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 49
    Top = 482
    Width = 60
    Height = 13
    Caption = #1050#1086#1084#1087#1086#1079#1080#1090#1086#1088
  end
  object ListView1: TListView
    Left = 26
    Top = 32
    Width = 383
    Height = 425
    Columns = <
      item
        Width = 0
      end
      item
        Caption = 'Caption'
        Width = 70
      end
      item
        Caption = 'Perfromance'
        Width = 100
      end
      item
        Caption = 'Performance type'
        Width = 100
      end
      item
        Width = 70
      end>
    GridLines = True
    ReadOnly = True
    RowSelect = True
    ShowWorkAreas = True
    TabOrder = 0
    ViewStyle = vsReport
  end
  object Button1: TButton
    Left = 230
    Top = 499
    Width = 75
    Height = 25
    Caption = #1048#1089#1082#1072#1090#1100'!'
    TabOrder = 1
    OnClick = Button1Click
  end
  object Edit1: TEdit
    Left = 49
    Top = 501
    Width = 121
    Height = 21
    TabOrder = 2
  end
end
