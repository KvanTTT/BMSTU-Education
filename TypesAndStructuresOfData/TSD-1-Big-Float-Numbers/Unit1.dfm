object Form1: TForm1
  Left = -1015
  Top = 465
  Caption = 'Big float numbers'
  ClientHeight = 287
  ClientWidth = 427
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
    Left = 24
    Top = 24
    Width = 241
    Height = 16
    Caption = 'Enter First Huge Number ('#177'm.n E '#177'K)'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label2: TLabel
    Left = 24
    Top = 93
    Width = 260
    Height = 16
    Caption = 'Enter Second Huge Number ('#177'm.n E '#177'K)'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label3: TLabel
    Left = 24
    Top = 157
    Width = 41
    Height = 16
    Caption = 'Result'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label4: TLabel
    Left = 344
    Top = 59
    Width = 3
    Height = 13
  end
  object Label5: TLabel
    Left = 344
    Top = 118
    Width = 3
    Height = 13
  end
  object Edit1: TEdit
    Left = 24
    Top = 56
    Width = 305
    Height = 21
    Color = 12578043
    TabOrder = 0
    OnChange = Edit1Change
    OnEnter = Edit1Change
    OnKeyPress = Edit1KeyPress
  end
  object Button1: TButton
    Left = 80
    Top = 232
    Width = 176
    Height = 33
    Caption = 'Divide!'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Times New Roman'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 3
    OnClick = Button1Click
  end
  object Edit2: TEdit
    Left = 24
    Top = 115
    Width = 305
    Height = 21
    Color = 12578043
    TabOrder = 1
    OnChange = Edit2Change
    OnEnter = Edit2Change
    OnKeyPress = Edit2KeyPress
  end
  object Edit3: TEdit
    Left = 24
    Top = 179
    Width = 305
    Height = 21
    Color = 12310783
    TabOrder = 2
  end
end
